using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinInterop = System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ocDownloader.Libs;
using ocDownloader.Models;
using ocDownloader.Models.APIResponses;

using Newtonsoft.Json;

namespace ocDownloader.Screens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserProfile UserProfile;
        private List<OCConnection> MyOCConnections;
        private List<Download> DLQueue;
        private OCConnection CurrentConnection;
        private Int32 CurrentConnectionIndex = 0;
        private Boolean IsConnectedToOC = false;

        /// <summary>
        /// Main window constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.UserProfile = new UserProfile();
            this.UserProfile.PrepareAppDataFolder();
            this.MyOCConnections = this.UserProfile.GetAllUserOCConnections();
        }

        /// <summary>
        /// Main window loaded event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void MainWindow_Loaded(Object Sender, RoutedEventArgs E)
        {
            this.UserProfile = new UserProfile();
            this.UserProfile.PrepareAppDataFolder();
            this.MyOCConnections = this.UserProfile.GetAllUserOCConnections();

            if (this.MyOCConnections.Count == 0)
            {
                return;
            }

            this.BuildExistingConnectionsMenu(true, false);

            this.CurrentConnection = this.MyOCConnections[this.CurrentConnectionIndex];
            if (this.AskPassword())
            {
                this.GetQueue();
            }
        }

        /// <summary>
        /// Saved connection click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void SavedConnection_Click(Object Sender, RoutedEventArgs E)
        {
            MenuItem ClickedMI = (MenuItem)Sender;
            String[] Name = ClickedMI.Name.Split('_');
            Int32 ConnectionIndex = Convert.ToInt32(Name[1]);

            if (this.CurrentConnectionIndex != ConnectionIndex || (this.CurrentConnectionIndex == ConnectionIndex && !this.IsConnectedToOC))
            {
                this.DownloadsContainer.Children.Clear();
                this.CurrentConnectionIndex = ConnectionIndex;
                this.CurrentConnection = this.MyOCConnections[this.CurrentConnectionIndex];
                if (this.AskPassword())
                {
                    this.GetQueue();
                }
            }
        }

        /// <summary>
        /// New connection menu item click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void NewConnection_Click(Object Sender, RoutedEventArgs E)
        {
            NewOCConnection NewConnectionForm = new NewOCConnection(this, this.UserProfile);
            if (NewConnectionForm.ShowDialog() == false)
            {
                if (NewConnectionForm.GetUserPassword != null && NewConnectionForm.GetConnection != null)
                {
                    NewConnectionForm.GetConnection.OCPassword = NewConnectionForm.GetUserPassword;
                    this.MyOCConnections.Add(NewConnectionForm.GetConnection);
                    this.BuildExistingConnectionsMenu(false, true);

                    this.DownloadsContainer.Children.Clear();

                    this.CurrentConnection = NewConnectionForm.GetConnection;
                    this.CurrentConnectionIndex = this.MyOCConnections.Count - 1;
                    this.GetQueue();
                }
            }
        }

        /// <summary>
        /// Manage connections menu item click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ManageConnections_Click(Object Sender, RoutedEventArgs E)
        {
            ConnectionsManager CManager = new ConnectionsManager(this, this.MyOCConnections);
            if (CManager.ShowDialog() == false)
            {
                this.BuildExistingConnectionsMenu(false, false);
            }
        }

        /// <summary>
        /// New download click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void NewDownload_Click(Object Sender, RoutedEventArgs E)
        {
            if (!this.IsConnectedToOC)
            {
                if (this.AskPassword())
                {
                    this.GetQueue();
                }
            }
            else
            {
                AddNewDownload NewDLWin = new AddNewDownload();
                if (NewDLWin.ShowDialog() == false)
                {

                }
            }
        }

        /// <summary>
        /// Exit menu item event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void Exit_Click(Object Sender, RoutedEventArgs E)
        {
            Application.Current.Shutdown ();
        }

        /// <summary>
        /// Check updates menu item event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void CheckUpdates_Click(Object Sender, RoutedEventArgs E)
        {

        }

        /// <summary>
        /// About menu item event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void About_Click(Object Sender, RoutedEventArgs E)
        {
            
        }

        /// <summary>
        /// Application title label mouse left button down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationTitle_MouseLeftButtonDown(Object Sender, MouseButtonEventArgs E)
        {
            this.DragMove();
        }

        /// <summary>
        /// Refresh top menu item click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void Refresh_Click(Object Sender, RoutedEventArgs E)
        {
            this.DownloadsContainer.Children.Clear();
            if (this.AskPassword())
            {
                this.GetQueue();
            }
        }

        /// <summary>
        /// Main shutdown button click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void MainShutdown_Click(Object Sender, RoutedEventArgs E)
        {
            Application.Current.Shutdown();
        }

        #region PrivateMethods
        private void BuildExistingConnectionsMenu(Boolean AppJustLaunched, Boolean ConnectionJustCreated)
        {
            if (AppJustLaunched || (ConnectionJustCreated && this.MyOCConnections.Count == 1))
            {
                this.TopMenuMain.Items.Insert(1, new MenuItem() { Name = "SavedConnectionsMenu", Header = "_Open Connection" });

                MenuItem MI = new MenuItem() { Name = "SavedConnectionsMenu", Header = "_Connections Manager" };
                MI.Click += ManageConnections_Click;
                this.TopMenuMain.Items.Insert(2, MI);
            }
            else if (this.MyOCConnections.Count == 0)
            {
                this.TopMenuMain.Items.RemoveAt(1);
                this.TopMenuMain.Items.RemoveAt(1);
            }

            MenuItem SavedConnectionsMenu = (MenuItem)this.TopMenuMain.Items[1];
            SavedConnectionsMenu.Items.Clear();
            for (Int32 Index = 0; Index < this.MyOCConnections.Count; Index++)
            {
                MenuItem SavedConnection = new MenuItem() { Name = "SavedConnection_" + Index, Header = "_" + this.MyOCConnections[Index].Name };
                SavedConnection.Click += SavedConnection_Click;

                SavedConnectionsMenu.Items.Insert(Index, SavedConnection);
            }
        }

        private Boolean AskPassword()
        {
            if (this.CurrentConnection.OCPassword == null)
            {
                EnterPassword EnterPasswordWin = new EnterPassword(this, this.CurrentConnection.Name);
                if (EnterPasswordWin.ShowDialog() == false)
                {
                    if (EnterPasswordWin.FormHasBeenClosed)
                    {
                        return false;
                    }

                    this.CurrentConnection.OCPassword = null;
                    if (EnterPasswordWin.GetPassword != null && EnterPasswordWin.GetPassword.Trim().Length > 0)
                    {
                        this.CurrentConnection.OCPassword = EnterPasswordWin.GetPassword;
                    }
                }
            }

            return true;
        }

        private async void GetQueue()
        {
            this.Title = "OCDOWNLOADER - " + this.CurrentConnection.Name;

            this.MainLoader.Opacity = 1;
            Int32 StatusCodeNumber = 0;

            try
            {
                if (this.CurrentConnection.OCPassword == null)
                {
                    throw new Exception("Please, enter your password !");
                }

                HttpResponseMessage OCDQueue = await APICalls.GetQueue(this.CurrentConnection);

                if (!OCDQueue.IsSuccessStatusCode)
                {
                    StatusCodeNumber = ocDownloader.Libs.Tools.GetStatusCode(OCDQueue.StatusCode);
                    throw new Exception("(" + StatusCodeNumber + ") " + ocDownloader.Libs.Tools.GetHttpStatusCodeMessage(StatusCodeNumber));
                }

                String ResponseContent = await OCDQueue.Content.ReadAsStringAsync();
                QueueResponse Response = JsonConvert.DeserializeObject<QueueResponse>(ResponseContent);

                if (Response.ocs == null)
                {
                    throw new Exception("An error occured while calling ocDownloader API (Unknown API function). Make sure your ocDownloader version is greater than or equals to the desktop client version");
                }

                if (Response.ocs.meta.statuscode != 100)
                {
                    throw new Exception("(" + Response.ocs.meta.statuscode + ") " + Response.ocs.meta.message);
                }
                else if (Response.ocs.data.ERROR)
                {
                    throw new Exception(Response.ocs.data.MESSAGE);
                }
                else
                {
                    this.IsConnectedToOC = true;
                    this.DLQueue = new List<Download>();
                    if (Response.ocs.data.COUNTER.ALL > 0)
                    {
                        foreach (Download DL in Response.ocs.data.QUEUE)
                        {
                            this.DLQueue.Add(DL);
                        }

                        this.DLQueue.Reverse();
                        foreach (Download DL in this.DLQueue)
                        {
                            this.CreateDownloadStackPanel(DL);
                        }
                    }
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                if (StatusCodeNumber == 401)
                {
                    this.CurrentConnection.OCPassword = null;
                    if (this.AskPassword())
                    {
                        this.GetQueue();
                    }
                    return;
                }
            }

            this.MainLoader.Opacity = 0;
        }

        /// <summary>
        /// Create download stackpanel
        /// </summary>
        /// <param name="DL"></param>
        private void CreateDownloadStackPanel(Download DL)
        {
            StackPanel DownloadSP = new StackPanel()
            {
                Style = Resources["DownloadSP"] as Style,
                Name = "GID_" + DL.GID
            };
            Canvas SPCanvas = new Canvas();
            SPCanvas.Children.Add(new Image()
            {
                Style = Resources["DownloadSPImage"] as Style,
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/DLTypes/" + Tools.GetDownloadTypeImageName(DL.PROTO), UriKind.Absolute))
            });
            SPCanvas.Children.Add(new TextBlock()
            {
                Style = Resources["DownloadSPTextBlockName"] as Style,
                Text = DL.FILENAME
            });
            SPCanvas.Children.Add(new TextBlock()
            {
                Style = Resources["DownloadSPTextBlockDetails"] as Style,
                Text = (DL.PROGRESS.ProgressString == null ? DL.PROGRESS.Message : DL.PROGRESS.ProgressString) + (DL.ISTORRENT && DL.PROGRESSVAL < 100 ? " - Seeders: " + DL.PROGRESS.NumSeeders : (DL.ISTORRENT && DL.PROGRESSVAL == 100 ? " - Uploaded: " + DL.PROGRESS.UploadLength + " - Ratio: " + DL.PROGRESS.Ratio : ""))
            });
            DownloadSP.Children.Add(SPCanvas);
            DownloadSP.Children.Add(new ProgressBar()
            {
                Style = Resources["DownloadSPProgressBar"] as Style,
                Minimum = 0,
                Maximum = 100,
                Value = DL.PROGRESSVAL
            });
            this.DownloadsContainer.Children.Add(DownloadSP);
            this.DownloadsContainer.Children.Add(new Line()
            {
                Style = Resources["DownloadSPSeparator"] as Style
            });
        }
        #endregion
    }
}
