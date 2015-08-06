using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ocDownloader.Screens
{
    /// <summary>
    /// Interaction logic for EnterPassword.xaml
    /// </summary>
    public partial class EnterPassword : Window
    {
        private Boolean FormClosed = false;
        private Boolean CPasswordHasChanged = false;
        private String CUserPassword = null;

        public EnterPassword(Window Owner, String Name)
        {
            InitializeComponent();

            this.Owner = Owner;
            this.LabelEnvironment.Text = String.Format("Please, enter your password to connect to the ownCloud environment {0} !", Name);
            this.ConnectionPassword.Focus();
        }

        /// <summary>
        /// Property : retrieve the password the user just enter
        /// </summary>
        public String GetPassword
        {
            get { return this.CUserPassword; }
        }

        /// <summary>
        /// Property : To know if the form has been closed or if the user clicked on the OK button
        /// </summary>
        public Boolean FormHasBeenClosed
        {
            get { return this.FormClosed; }
        }

        /// <summary>
        /// New connection form close button event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void EnterPasswordClose_Click(Object Sender, RoutedEventArgs E)
        {
            this.FormClosed = true;
            this.Close();
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
            }
        }

        /// <summary>
        /// Form password key down event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void FormPasswdBox_KeyDown(Object Sender, KeyEventArgs E)
        {
            if (E.Key == Key.Enter)
            {
                this.CUserPassword = this.ConnectionPassword.Password;
                this.Close();
            }
        }

        /// <summary>
        /// Ok button click event
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void OkButton_Click(Object Sender, RoutedEventArgs E)
        {
            this.CUserPassword = this.ConnectionPassword.Password;
            this.Close();
        }
    }
}
