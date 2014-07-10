using System;
using System.Windows;

namespace GeneratorClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String userToken;
        public readonly String applicationToken = Guid.NewGuid().ToString();
        public MainWindow()
        {
            InitializeComponent();
            this.Content = new Login();
        }
        public void loggedOn()
        {
            this.Content = null;
            this.Content = new Main(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Content = null;
        }
    }
}
