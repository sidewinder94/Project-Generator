﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeneratorServiceContracts;

namespace GeneratorClient
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private String _username;

        public String Username
        {
            get { return _username; }
            set { _username = value; }
        }


        public Login()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            WorkServiceClient client = new WorkServiceClient("NetTcpBinding_IWorkService");
            Message msg = new Message(applicationToken: Guid.NewGuid().ToString(),
                                      operation: Operations.Authenticate,
                                      status: Status.Sent);
            msg = client.ServiceOperation(msg);
            ((MainWindow)this.Parent).userToken = msg.UserToken;
            ((MainWindow)this.Parent).applicationToken = msg.ApplicationToken;
            ((MainWindow)this.Parent).loggedOn();
            client.Close();
        }
    }
}
