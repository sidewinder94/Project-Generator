using GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace GeneratorClient
{
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        private static WorkServiceClient client;
        private MainWindow mainWindow;

        public Main()
        {
            InitializeComponent();
        }

        ~Main()
        {
            client.Close();
        }

        public Main(MainWindow mainWindow)
        {
            if (client == null)
            {
                client = new WorkServiceClient("NetTcpBinding_IWorkService");
                client.ServiceOperationCompleted += client_ServiceOperationCompleted;
            }
            if (client.State != CommunicationState.Opened)
            {
                client.Open();
            }
            this.mainWindow = mainWindow;
        }

        void client_ServiceOperationCompleted(object sender, ServiceOperationCompletedEventArgs e)
        {
            messageReceived(e.Result);
        }

        void messageReceived(Message msg)
        {
            MessageBox.Show("Le fichier {0} est décodé", msg.Info);
            Action action = delegate()
            {
                WorkServiceClient client = new WorkServiceClient();
                var data = (List<Tuple<int, string, string>>)client.ServiceOperation(new Message(operation: Operations.GetCompleted,
                                                                                            applicationToken: this.mainWindow.applicationToken,
                                                                                            userToken: this.mainWindow.userToken)).Data[0];
                this.convertedItems.Children.Clear();
                foreach (Tuple<int, string, string> tuple in data)
                {
                    this.convertedItems.Children.Add(new ConvertedItem(tuple.Item1, tuple.Item2, tuple.Item3, this.mainWindow));
                }
                client.Close();
            };

            Dispatcher.Invoke(action);
        }
    }
}
