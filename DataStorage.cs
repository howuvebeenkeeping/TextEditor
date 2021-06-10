using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;

namespace TextEditor
{
    internal static class DataStorage 
    {
        private static readonly string DocumentFilter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";

        public static void Save(TextRange textRange) 
        {
            var saveDialog = new SaveFileDialog {Filter = DocumentFilter};

            if (saveDialog.ShowDialog() == true)
            {
                textRange.Save(new FileStream(saveDialog.FileName, FileMode.Create), DataFormats.Rtf);
            }
        }

        public static FileStream Open() 
        {
            var openDialog = new OpenFileDialog {Filter = DocumentFilter};
			
            return openDialog.ShowDialog() == true 
                ? new FileStream(openDialog.FileName, FileMode.Open) 
                : null;
        }
    }
}