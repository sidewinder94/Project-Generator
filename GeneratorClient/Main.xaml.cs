using GeneratorServiceCallBackContracts;
using System;
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

namespace GeneratorClient
{


    class Callback : ICallBackClient
    {
        Main Parent;
        public delegate void ReceivedMessage(CallBackMessage msg);
        public event ReceivedMessage receivedEvent;
        public Callback(Main parent)
        {
            this.Parent = parent;
        }


        public void NotifyClient(CallBackMessage msg)
        {
            this.receivedEvent(msg);
        }
    }
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        private MainWindow mainWindow;

        public Main()
        {
            InitializeComponent();
        }

        public Main(MainWindow mainWindow)
        {
            Callback callback = new Callback(this);

            callback.receivedEvent += callback_receivedEvent;
            this.mainWindow = mainWindow;
        }

        void callback_receivedEvent(CallBackMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}
