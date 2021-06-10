using System.IO;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;

namespace TextEditor {
    internal static class DataStorage {
        private static readonly string DocumentFilter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";

        public static void Save(TextRange textRange) {
            var saveDialog = new SaveFileDialog {Filter = DocumentFilter};

            if (saveDialog.ShowDialog() == true) {
                textRange.Save(new FileStream(saveDialog.FileName, FileMode.Create), DataFormats.Rtf);
            }
        }

        public static void Open(TextRange textRange) {
            var openDialog = new OpenFileDialog {Filter = DocumentFilter};
			
            if (openDialog.ShowDialog() == true) {
                textRange.Load(new FileStream(openDialog.FileName, FileMode.Open), DataFormats.Rtf);
            }
        }
    }
}