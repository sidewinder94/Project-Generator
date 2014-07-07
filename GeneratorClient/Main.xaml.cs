using GeneratorClient.GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
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
        private WorkServiceClient client;
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
        }

        void client_ServiceOperationCompleted(object sender, ServiceOperationCompletedEventArgs e)
        {
            messageReceived(e.Result);
        }

        void messageReceived(Message msg)
        {
            if (msg != null)
            {
                MessageBox.Show("Le fichier {0} est décodé", msg.Info);
                Action action = delegate()
                {
                    WorkServiceClient client = new WorkServiceClient();
                    var mesg = new Message();
                    mesg.Operation = Operations.GetCompleted;
                    mesg.ApplicationToken = this.mainWindow.applicationToken;
                    mesg.UserToken = this.mainWindow.applicationToken;
                    var data = (List<Tuple<int, string, string>>)client.ServiceOperation(msg).Data[0];
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
