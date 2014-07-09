using GeneratorClient.GeneratorServiceContracts;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
            set { _mailFound = value.TrimEnd('|'); }
        }

        private String decodedText;
        private byte[] TrustLevelPdf;

        private string FileID;
        private MainWindow mainWindow;
        public ConvertedItem(string FileID, String fileName, String mailFound, MainWindow main)
        {
            this.DataContext = this;
            this.FileName = fileName;
            this.MailFound = mailFound;
            this.FileID = FileID;
            this.mainWindow = main;
            WorkServiceClient client = new WorkServiceClient();
            var msg = new Message();
            msg.ApplicationToken = main.applicationToken;
            msg.UserToken = main.userToken;
            msg.Operation = Operations.GetDecrypted;
            msg.Data = new Object[] { FileID };
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
