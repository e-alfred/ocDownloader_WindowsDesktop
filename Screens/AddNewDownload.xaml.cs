using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Microsoft.Win32;

using ocDownloader.Models.APIResponses;

namespace ocDownloader.Screens
{
    /// <summary>
    /// Interaction logic for AddNewDownload.xaml
    /// </summary>
    public partial class AddNewDownload : Window
    {
        private Download DL = null;
        private Boolean HTTPDownloadURLHasChanged = false;
        private Boolean BasicAuthUsernameHasChanged = false;
        private Boolean BasicAuthPasswordHasChanged = false;
        private Boolean FTPDownloadURLHasChanged = false;
        private Boolean FTPUsernameHasChanged = false;
        private Boolean FTPPasswordHasChanged = false;

        public AddNewDownload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add new download close button event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void AddNewDownloadClose_Click(Object Sender, RoutedEventArgs E)
        {
            this.Close();
        }

        /// <summary>
        /// Window title mouse left button down event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void WindowTitle_MouseLeftButtonDown(Object Sender, MouseButtonEventArgs E)
        {
            this.DragMove();
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
                case "HTTPDownloadURL":
                    TB.Text = this.HTTPDownloadURLHasChanged ? TB.Text : "";
                    break;
                case "BasicAuthUsername":
                    TB.Text = this.BasicAuthUsernameHasChanged ? TB.Text : "";
                    break;
                case "FTPDownloadURL":
                    TB.Text = this.FTPDownloadURLHasChanged ? TB.Text : "";
                    break;
                case "FTPUsername":
                    TB.Text = this.FTPUsernameHasChanged ? TB.Text : "";
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
                case "HTTPDownloadURL":
                    if (TB.Text.Trim().Length > 0)
                    {
                        this.HTTPDownloadURLHasChanged = true;
                    }
                    else
                    {
                        this.HTTPDownloadURLHasChanged = false;
                        TB.Text = "http(s)://";
                        TB.Foreground = Brushes.LightGray;
                    }
                    break;
                case "BasicAuthUsername":
                    if (TB.Text.Trim().Length > 0)
                    {
                        this.BasicAuthUsernameHasChanged = true;
                    }
                    else
                    {
                        this.BasicAuthUsernameHasChanged = false;
                        TB.Text = "Username";
                        TB.Foreground = Brushes.LightGray;
                    }
                    break;
                case "FTPDownloadURL":
                    if (TB.Text.Trim().Length > 0)
                    {
                        this.FTPDownloadURLHasChanged = true;
                    }
                    else
                    {
                        this.FTPDownloadURLHasChanged = false;
                        TB.Text = "ftp(s)://";
                        TB.Foreground = Brushes.LightGray;
                    }
                    break;
                case "FTPUsername":
                    if (TB.Text.Trim().Length > 0)
                    {
                        this.FTPUsernameHasChanged = true;
                    }
                    else
                    {
                        this.FTPUsernameHasChanged = false;
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
            switch (PB.Name)
            {
                case "BasicAuthPassword":
                    PB.Password = this.BasicAuthPasswordHasChanged ? PB.Password : "";
                    break;
                case "FTPPassword":
                    PB.Password = this.FTPPasswordHasChanged ? PB.Password : "";
                    break;
            }

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
            switch (PB.Name)
            {
                case "BasicAuthPassword":
                    if (PB.Password.Trim().Length > 0)
                    {
                        this.BasicAuthPasswordHasChanged = true;
                    }
                    else
                    {
                        this.BasicAuthPasswordHasChanged = false;
                        PB.Password = "Password";
                        PB.Foreground = Brushes.LightGray;
                    }
                    break;
                case "FTPPassword":
                    if (PB.Password.Trim().Length > 0)
                    {
                        this.FTPPasswordHasChanged = true;
                    }
                    else
                    {
                        this.FTPPasswordHasChanged = false;
                        PB.Password = "Password";
                        PB.Foreground = Brushes.LightGray;
                    }
                    break;
            }
        }

        /// <summary>
        /// Open dialog bittorrent files click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void OpenDialogBTFiles_Click(Object Sender, RoutedEventArgs E)
        {
            OpenFileDialog DLG = new OpenFileDialog();
            DLG.DefaultExt = "*.torrent";
            DLG.Filter = "Torrent Files (*.torrent)|*.torrent";
            DLG.Multiselect = true;
            
            if (DLG.ShowDialog() == true)
            {
                this.BTDownloadFilePath.Text = String.Join(", ", DLG.FileNames);
            }
        }
    }
}
