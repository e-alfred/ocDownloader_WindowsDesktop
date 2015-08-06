using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ocDownloader.Libs;
using ocDownloader.Models;

namespace ocDownloader.Screens
{
    /// <summary>
    /// Interaction logic for ConnectionsManager.xaml
    /// </summary>
    public partial class ConnectionsManager : Window
    {
        private List<OCConnection> MyOCConnections;

        public ConnectionsManager(Window Owner, List<OCConnection> MyOCConnections)
        {
            InitializeComponent();

            this.MyOCConnections = MyOCConnections;
            this.Owner = Owner;

            foreach (OCConnection OCConnection in this.MyOCConnections)
            {
                this.CreateConnectionStackPanel(OCConnection);
            }
        }

        public List<OCConnection> GetMyOCConnections
        {
            get { return this.MyOCConnections;  }
        }

        private void ConnectionsManagerClose_Click(Object Sender, RoutedEventArgs E)
        {
            this.Close();
        }

        /// <summary>
        /// Create connection stackpanel
        /// </summary>
        /// <param name="DL"></param>
        private void CreateConnectionStackPanel(OCConnection OCConnection)
        {
            StackPanel ConnectionSP = new StackPanel()
            {
                Style = Resources["ConnectionSP"] as Style
            };
            Border TextBorder = new Border()
            {
                Style = Resources["ConnectionSPTextBorder"] as Style
            };
            TextBorder.Child = new TextBlock()
            {
                Style = Resources["ConnectionSPTextBlockName"] as Style,
                Text = OCConnection.Name
            };
            Canvas SPCanvas = new Canvas();
            SPCanvas.Children.Add(TextBorder);
            Button TrashButton = new Button()
            {
                Style = Resources["ConnectionSPImageButton"] as Style
            };
            TrashButton.Content = new Image()
            {
                Style = Resources["ConnectionSPImage"] as Style,
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/trash32_black.png", UriKind.Absolute))
            };
            TrashButton.Click += (S, E) =>
            {
                UserProfile UP = new UserProfile();
                if (UP.RemoveOCConnection(OCConnection))
                {
                    Canvas ButtonParent = (Canvas)TrashButton.Parent;
                    this.ConnectionsContainer.Children.Remove((StackPanel)ButtonParent.Parent);

                    this.MyOCConnections.Remove(OCConnection);
                }
                else
                {
                    MessageBox.Show("Unable to remove OCConnection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
            SPCanvas.Children.Add(TrashButton);
            ConnectionSP.Children.Add(SPCanvas);
            this.ConnectionsContainer.Children.Add(ConnectionSP);
            this.ConnectionsContainer.Children.Add(new Line()
            {
                Style = Resources["ConnectionSPSeparator"] as Style
            });
        }
    }
}
