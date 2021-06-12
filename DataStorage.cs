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
        public static bool IsDocumentSaved { get; set; }
        private static bool IsDocumentOpened { get; set; }
        public static string FileName { get; private set; }

        public static void Save(TextRange documentRange)
        {
            if (!IsDocumentOpened)
            {
                var saveDialog = new SaveFileDialog {Filter = DocumentFilter};
                saveDialog.ShowDialog();
                FileName = saveDialog.FileName;
                IsDocumentOpened = true;
            }
            documentRange.Save(new FileStream(FileName, FileMode.Create), DataFormats.Rtf);
            IsDocumentSaved = true;
        }

        public static string Open() 
        {
            var openDialog = new OpenFileDialog {Filter = DocumentFilter};
            openDialog.ShowDialog();
            IsDocumentOpened = true;
            IsDocumentSaved = true;
            return FileName = openDialog.FileName;
        }
    }
}