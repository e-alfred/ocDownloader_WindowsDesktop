using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Newtonsoft.Json;

using ocDownloader.Libs;
using ocDownloader.Models;
using ocDownloader.Models.APIResponses;

namespace ocDownloader.Screens
{
    /// <summary>
    /// Interaction logic for NewOCConnection.xaml
    /// </summary>
    public partial class NewOCConnection : Window
    {
        private Boolean CNameHasChanged = false;
        private Boolean CURLHasChanged = false;
        private Boolean CUsernameHasChanged = false;
        private Boolean CPasswordHasChanged = false;
        private UserProfile UserProfileInfo = null;
        private OCConnection NewConnection = null;
        private String UserPassword = null;

        /// <summary>
        /// New OCConnection constructor
        /// </summary>
        /// <param name="UserProfileInfo"></param>
        public NewOCConnection(UserProfile UserProfileInfo)
        {
            InitializeComponent();
            this.UserProfileInfo = UserProfileInfo;
        }

        /// <summary>
        /// Allow the retrieval of the user password from the main window
        /// </summary>
        public String GetUserPassword
        {
            get { return this.UserPassword;  }
        }

        /// <summary>
        /// Allow the retrieval of the new owncloud connection from the main window
        /// </summary>
        public OCConnection GetConnection
        {
            get { return this.NewConnection; }
        }

        /// <summary>
        /// New connection form close button event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void NewConnectionClose_Click(Object Sender, RoutedEventArgs E)
        {
            this.UserPassword = null;
            this.NewConnection = null;
            this.Close();
        }

        /// <summary>
        /// Form text boxes got focus event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void FormTextBoxes_GotFocus(Object Sender, EventArgs E)
        {
            TextBox TB = (TextBox)Sender;
            switch (TB.Name)
            {
                case "ConnectionName":
                    TB.Text = this.CNameHasChanged ? TB.Text : "";
                    break;
                case "ConnectionURL":
                    TB.Text = this.CURLHasChanged ? TB.Text : "";
                    break;
                case "ConnectionUsername":
                    TB.Text = this.CUsernameHasChanged ? TB.Text : "";
                    break;
            }

            TB.Foreground = Brushes.Black;
        }

        /// <summary>
        /// Form text boxes lost focus event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void FormTextBoxes_LostFocus(Object Sender, EventArgs E)
        {
            TextBox TB = (TextBox)Sender;
            switch (TB.Name)
            {
                case "ConnectionName":
                    if (TB.Text.Trim().Length > 0)
                    {
                        this.CNameHasChanged = true;
                    }
                    else
                    {
                        this.CNameHasChanged = false;
                        TB.Text = "Name";
                        TB.Foreground = Brushes.LightGray;
                    }
                    break;
                case "ConnectionURL":
                    if (TB.Text.Trim().Length > 0)
                    {
                        this.CURLHasChanged = true;
                    }
                    else
                    {
                        this.CURLHasChanged = false;
                        TB.Text = "ownCloud URL";
                        TB.Foreground = Brushes.LightGray;
                    }
                    break;
                case "ConnectionUsername":
                    if (TB.Text.Trim().Length > 0)
                    {
                        this.CUsernameHasChanged = true;
                    }
                    else
                    {
                        this.CUsernameHasChanged = false;
                        TB.Text = "Username";
                        TB.Foreground = Brushes.LightGray;
                    }
                    break;
            }
        }

        /// <summary>
        /// Form password box got focus event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void FormPasswdBox_GotFocus(Object Sender, EventArgs E)
        {
            PasswordBox PB = (PasswordBox)Sender;
            PB.Password = this.CPasswordHasChanged ? PB.Password : "";

            PB.Foreground = Brushes.Black;
        }

        /// <summary>
        /// Form password box lost focus event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void FormPasswdBox_LostFocus(Object Sender, EventArgs E)
        {
            PasswordBox PB = (PasswordBox)Sender;
            if (PB.Password.Trim().Length > 0)
            {
                this.CPasswordHasChanged = true;
            }
            else
            {
                this.CPasswordHasChanged = false;
                PB.Password = "Password";
                PB.Foreground = Brushes.LightGray;
            }
        }

        /// <summary>
        /// Save button click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private async void SaveButton_Click(Object Sender, RoutedEventArgs E)
        {
            if (this.ConnectionName.Text.Trim().Length == 0 || this.ConnectionName.Text.CompareTo("Name") == 0 || this.ConnectionURL.Text.Trim().Length == 0 || this.ConnectionURL.Text.CompareTo("ownCloud URL") == 0 || this.ConnectionUsername.Text.Trim().Length == 0 || this.ConnectionUsername.Text.CompareTo("ownCloud URL") == 0 || this.ConnectionPassword.Password.Trim().Length == 0 || this.ConnectionPassword.Password.CompareTo("ownCloud URL") == 0)
            {
                MessageBox.Show("Please check that all fields are correctly filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.SaveButton.Opacity = 0;
                this.NewConnectionLoader.Opacity = 1;

                if (!this.ConnectionURL.Text.EndsWith("/"))
                {
                    this.ConnectionURL.Text += "/";
                }

                try
                {
                    HttpResponseMessage Version = await APICalls.CheckVersion(this.ConnectionURL.Text, this.ConnectionUsername.Text, this.ConnectionPassword.Password);

                    if (!Version.IsSuccessStatusCode)
                    {
                        Int32 StatusCodeNumber = ocDownloader.Libs.Tools.GetStatusCode(Version.StatusCode);
                        throw new Exception("(" + StatusCodeNumber + ") " + ocDownloader.Libs.Tools.GetHttpStatusCodeMessage(StatusCodeNumber));
                    }

                    String ResponseContent = await Version.Content.ReadAsStringAsync();
                    VersionResponse Response = JsonConvert.DeserializeObject<VersionResponse>(ResponseContent);

                    if (Response.ocs.meta.statuscode != 100)
                    {
                        throw new Exception("(" + Response.ocs.meta.statuscode + ") " + Response.ocs.meta.message);
                    }
                    else if (!Response.ocs.data.RESULT)
                    {
                        throw new Exception("Your ocDownloader installed version may not work with this Addon version. Please update the ocDownloader version on your server.");
                    }
                    else
                    {
                        this.NewConnection = new OCConnection() { Name = this.ConnectionName.Text, OCUrl = this.ConnectionURL.Text, OCUsername = this.ConnectionUsername.Text };
                        this.UserPassword = this.ConnectionPassword.Password;
                        this.UserProfileInfo.SaveConnection(this.NewConnection);
                        
                        this.Close();
                    }
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.SaveButton.Opacity = 1;
                    this.NewConnectionLoader.Opacity = 0;
                }
            }
        }
    }
}
