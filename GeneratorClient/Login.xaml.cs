﻿using GeneratorClient.GeneratorServiceContracts;
using System;
using System.Windows;
using System.Windows.Controls;

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
            Message msg = new Message();
            msg.ApplicationToken = Guid.NewGuid().ToString();
            msg.Operation = Operations.Authenticate;
            msg.Status = Status.Sent;
            msg = client.ServiceOperation(msg);
            ((MainWindow)this.Parent).userToken = msg.UserToken;
            ((MainWindow)this.Parent).applicationToken = msg.ApplicationToken;
            ((MainWindow)this.Parent).loggedOn();
            client.Close();
        }
    }
}
