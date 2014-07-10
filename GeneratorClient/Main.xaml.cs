using GeneratorClient.GeneratorServiceContracts;
using GeneratorService;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace GeneratorClient
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    class Callback : IClientCallback
    {

        public event EventHandler finishedEvent;
        public void NotifyClients()
        {
            finishedEvent(this, new EventArgs());
        }
    }
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        private WorkServiceClient client;
        private MainWindow mainWindow;
        private Utils.Timer resetConnectionTimer = new Utils.Timer(new TimeSpan(0, 0, 30));
        #region callback connection
        private DuplexChannelFactory<ISubService> channel;
        private ISubService service;
        #endregion


        ~Main()
        {
            resetConnectionTimer.Stop();
            client.Close();
        }
        public Main()
        {
            Callback callback = new Callback();
            callback.finishedEvent += callback_finishedEvent;
            channel = new DuplexChannelFactory<ISubService>(callback, "callBackEndpoint");
            service = channel.CreateChannel();
            resetConnectionTimer.CountdownEnd += resetConnectionTimer_CountdownEnd;
            resetConnectionTimer.Start();

            InitializeComponent();
        }

        void resetConnectionTimer_CountdownEnd(object sender, EventArgs e)
        {
            if (channel.State == CommunicationState.Faulted)
            {
                channel.Abort();
                service = channel.CreateChannel();
                messageReceived();
            }
            resetConnectionTimer.Restart();
        }

        void callback_finishedEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Fichier décodé");
            messageReceived();
        }

        public Main(MainWindow mainWindow)
            : this()
        {
            if (client == null)
            {
                client = new WorkServiceClient("NetTcpBinding_IWorkService");
            }
            if (client.State != CommunicationState.Opened)
            {
                client.Open();
            }
            this.mainWindow = mainWindow;
            service.SubscribeClient(mainWindow.userToken);
            messageReceived();
        }

        void client_ServiceOperationCompleted(object sender, ServiceOperationCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                messageReceived();
            }
        }

        void messageReceived()
        {
            WorkServiceClient client = new WorkServiceClient("NetTcpBinding_IWorkService");
            var mesg = new Message();
            mesg.Operation = Operations.GetCompleted;
            mesg.ApplicationToken = this.mainWindow.applicationToken;
            mesg.UserToken = this.mainWindow.userToken;
            var data = (object[])client.ServiceOperation(mesg).Data;
            client.Close();
            Action action = delegate()
            {
                this.convertedItems.Children.Clear();
                for (int i = 0; i < data.Length; i += 3)
                {
                    string fileID = (string)data[i];
                    string fileName = (string)data[i + 1];
                    string mailFound = (string)data[i + 2];
                    ConvertedItem item = new ConvertedItem(fileID, fileName, mailFound, this.mainWindow);
                    Thickness margin = new Thickness(0, 10, 5, 0);
                    item.Margin = margin;
                    this.convertedItems.Children.Add(item);
                }

            };
            Dispatcher.Invoke(action);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.AddExtension = true;
            ofd.AutoUpgradeEnabled = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = true;
            ofd.Title = "Ouvrir un fichier a décoder";
            ofd.ValidateNames = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String path in ofd.FileNames)
                {
                    try
                    {
                        var file = File.ReadAllText(path);
                        var msg = new Message();

                        msg.Data = new object[] { file, (object)path, null, null };
                        msg.Info = file.GetHashCode().ToString();
                        msg.Operation = Operations.Decode;
                        msg.ApplicationToken = this.mainWindow.applicationToken;
                        msg.UserToken = this.mainWindow.userToken;
                        this.client.ServiceOperationCompleted += client_ServiceOperationCompleted;
                        this.client.ServiceOperationAsync(msg);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("couldn't open {0} for reason : {1}",
                                          path,
                                          ex.Message);
                    }
                }
            }
        }
    }
}
