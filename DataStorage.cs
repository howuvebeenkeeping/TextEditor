using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;

namespace TextEditor {
    internal static class DataStorage {
        private static readonly string DocumentFilter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
        public static RichTextBox RichTextBox { get; set; }
        private static TextRange TextRange => new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd); 

        public static void Save() {
            var fileDialog = new SaveFileDialog {
                Filter = DocumentFilter
            };

            if (fileDialog.ShowDialog() == true) {
                TextRange.Save(new FileStream(fileDialog.FileName, FileMode.Create), DataFormats.Rtf);
            }
        }

        public static void Open() {
            var fileDialog = new OpenFileDialog {
                Filter = DocumentFilter
            };
			
            if (fileDialog.ShowDialog() == true) {
                TextRange.Load(new FileStream(fileDialog.FileName, FileMode.Open), DataFormats.Rtf);
            }
        }
    }
}