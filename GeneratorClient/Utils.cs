using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorClient
{
    public static class Utils
    {
        public static System.Windows.Forms.SaveFileDialog createSaveDialog(String title, String defaultExtension)
        {
            var saveDialog = new System.Windows.Forms.SaveFileDialog();
            saveDialog.CheckFileExists = true;
            saveDialog.CheckPathExists = true;
            saveDialog.DereferenceLinks = true;
            saveDialog.OverwritePrompt = true;
            saveDialog.ValidateNames = true;
            saveDialog.Title = title;
            saveDialog.AddExtension = true;
            saveDialog.DefaultExt = defaultExtension;
            return saveDialog;
        }
    }
}
