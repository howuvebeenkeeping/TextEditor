using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;

namespace TextEditor
{
    internal class DataStorage 
    {
        private static readonly string DocumentFilter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
        private readonly RichTextBox _richTextBox;
        private TextRange TextRange =>
            new TextRange(_richTextBox.Document.ContentStart, _richTextBox.Document.ContentEnd);

        public DataStorage(RichTextBox richTextBox)
        {
            _richTextBox = richTextBox;
        }

        public void Save() 
        {
            var saveDialog = new SaveFileDialog {Filter = DocumentFilter};

            if (saveDialog.ShowDialog() == true)
            {
                TextRange.Save(new FileStream(saveDialog.FileName, FileMode.Create), DataFormats.Rtf);
            }
        }

        public void Open() 
        {
            var openDialog = new OpenFileDialog {Filter = DocumentFilter};
			
            if (openDialog.ShowDialog() == true)
            {
                TextRange.Load(new FileStream(openDialog.FileName, FileMode.Open), DataFormats.Rtf);
            }
        }
    }
}