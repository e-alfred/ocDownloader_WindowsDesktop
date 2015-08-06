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
        private OCConnection CurrentConnection;
        private Int32 CurrentConnectionIndex = 0;

        /// <summary>
        /// Main window constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += new EventHandler(Window_SourceInitialized);

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

            if (this.CurrentConnectionIndex != ConnectionIndex)
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
        /// Application title label mouse double click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ApplicationTitle_MouseDoubleClick(Object Sender, MouseButtonEventArgs E)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.MainWindowBorder.BorderThickness = new Thickness(0);
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.MainWindowBorder.BorderThickness = new Thickness(3);
                this.WindowState = WindowState.Normal;
            }
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

        /// <summary>
        /// Main maximize button click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void MainMaximize_Click(Object Sender, RoutedEventArgs E)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.MainWindowBorder.BorderThickness = new Thickness(0);
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.MainWindowBorder.BorderThickness = new Thickness(3);
                this.WindowState = WindowState.Normal;
            }
        }

        /// <summary>
        /// Main minimize button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="E"></param>
        private void MainMinimize_Click(Object sender, RoutedEventArgs E)
        {
            this.WindowState = WindowState.Minimized;
        }

        #region PrivateMethods
        private void BuildExistingConnectionsMenu(Boolean AppJustLaunched, Boolean ConnectionJustCreated)
        {
            if (AppJustLaunched || (ConnectionJustCreated && this.MyOCConnections.Count == 1))
            {
                this.TopMenuMain.Items.Insert(1, new MenuItem() { Name = "SavedConnectionsMenu", Header = "_Open Connection", Foreground = Brushes.White });

                MenuItem MI = new MenuItem() { Name = "SavedConnectionsMenu", Header = "_Connections Manager", Foreground = Brushes.White };
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
                MenuItem SavedConnection = new MenuItem() { Name = "SavedConnection_" + Index, Header = "_" + this.MyOCConnections[Index].Name, Foreground = Brushes.White };
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
                    if (Response.ocs.data.COUNTER.ALL > 0)
                    {
                        foreach (Download DL in Response.ocs.data.QUEUE)
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
                Maximum = 100
            });
            this.DownloadsContainer.Children.Add(DownloadSP);
            this.DownloadsContainer.Children.Add(new Line()
            {
                Style = Resources["DownloadSPSeparator"] as Style
            });
        }
        #endregion

        #region MaximizeHack
        private void Window_SourceInitialized(Object Sender, EventArgs E)
        {
            System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(WindowProc));
        }

        private static System.IntPtr WindowProc(System.IntPtr hwnd, int msg, System.IntPtr wParam, System.IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (System.IntPtr)0;
        }

        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {

            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            System.IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != System.IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }


        /// <summary>
        /// POINT aka POINTAPI
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;
            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            /// <summary>
            /// </summary>            
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// </summary>            
            public RECT rcMonitor = new RECT();

            /// <summary>
            /// </summary>            
            public RECT rcWork = new RECT();

            /// <summary>
            /// </summary>            
            public int dwFlags = 0;
        }


        /// <summary> Win32 </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            /// <summary> Win32 </summary>
            public int left;
            /// <summary> Win32 </summary>
            public int top;
            /// <summary> Win32 </summary>
            public int right;
            /// <summary> Win32 </summary>
            public int bottom;

            /// <summary> Win32 </summary>
            public static readonly RECT Empty = new RECT();

            /// <summary> Win32 </summary>
            public int Width
            {
                get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
            }
            /// <summary> Win32 </summary>
            public int Height
            {
                get { return bottom - top; }
            }

            /// <summary> Win32 </summary>
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }


            /// <summary> Win32 </summary>
            public RECT(RECT rcSrc)
            {
                this.left = rcSrc.left;
                this.top = rcSrc.top;
                this.right = rcSrc.right;
                this.bottom = rcSrc.bottom;
            }

            /// <summary> Win32 </summary>
            public bool IsEmpty
            {
                get
                {
                    // BUGBUG : On Bidi OS (hebrew arabic) left > right
                    return left >= right || top >= bottom;
                }
            }
            /// <summary> Return a user friendly representation of this struct </summary>
            public override string ToString()
            {
                if (this == RECT.Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            /// <summary> Determine if 2 RECT are equal (deep compare) </summary>
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }

            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }


            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2)
            {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2)
            {
                return !(rect1 == rect2);
            }
        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);
        #endregion
    }
}
