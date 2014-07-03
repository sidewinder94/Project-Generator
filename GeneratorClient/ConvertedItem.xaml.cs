using GeneratorServiceContracts;
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
    /// <summary>
    /// Logique d'interaction pour ConvertedItem.xaml
    /// </summary>
    public partial class ConvertedItem : UserControl
    {

        private String _fileName;

        public String FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private String _mailFound;

        public String MailFound
        {
            get { return _mailFound; }
            set { _mailFound = value; }
        }

        private String decodedText;
        private byte[] TrustLevelPdf;

        private int FileID;
        private MainWindow mainWindow;
        public ConvertedItem(int FileID, String fileName, String mailFound, MainWindow main)
        {
            this.DataContext = this;
            this._fileName = fileName;
            this._mailFound = mailFound;
            this.FileID = FileID;
            this.mainWindow = main;
            WorkServiceClient client = new WorkServiceClient();
            var msg = client.ServiceOperation(new Message(applicationToken: main.applicationToken,
                                                userToken: main.userToken,
                                                operation: Operations.GetDecrypted));
            this.decodedText = (String)msg.Data[0];
            this.TrustLevelPdf = (byte[])msg.Data[1];
            InitializeComponent();

        }

        private void TextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PDFButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
