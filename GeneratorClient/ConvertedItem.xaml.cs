using GeneratorClient.GeneratorServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
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
            var msg = new Message();
            msg.ApplicationToken = main.applicationToken;
            msg.UserToken = main.userToken;
            msg.Operation = Operations.GetDecrypted;
            msg = client.ServiceOperation(msg);
            this.decodedText = (String)msg.Data[0];
            this.TrustLevelPdf = (byte[])msg.Data[1];
            InitializeComponent();

        }

        private void TextButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = Utils.createSaveDialog("Sauvegarder le texte décrypté", "txt");
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, this.decodedText);
                }
                catch (IOException ex)
                {
                    Console.WriteLine("couldn't write due to {0}", ex.Message);
                }
            }
        }

        private void PDFButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = Utils.createSaveDialog("Sauvegarder le rapport de confiance", "pdf");
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(saveFileDialog.FileName, this.TrustLevelPdf);
                }
                catch (IOException ex)
                {
                    Console.WriteLine("couldn't write due to {0}", ex.Message);
                }
            }
        }


    }
}
