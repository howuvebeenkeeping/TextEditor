using System.IO;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;

namespace TextEditor
{
    internal class DataStorage 
    {
        private bool _isDocumentOpened;
        private static readonly string DocumentFilter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
        private static DataStorage _dataStorage;

        public static DataStorage GetInstance()
        {
            return _dataStorage ?? (_dataStorage = new DataStorage());
        }

        public string DocumentName { get; private set; }

        public void SaveDocument(TextRange documentRange)
        {
            if (!_isDocumentOpened)
            {
                var saveDialog = new SaveFileDialog {Filter = DocumentFilter};
                saveDialog.ShowDialog();
                DocumentName = saveDialog.FileName;
                _isDocumentOpened = true;
            }
            documentRange.Save(new FileStream(DocumentName, FileMode.Create), DataFormats.Rtf);
        }

        public string OpenDocumentWithDialog() 
        {
            var openDialog = new OpenFileDialog {Filter = DocumentFilter};
            openDialog.ShowDialog();
            _isDocumentOpened = true;
            return DocumentName = openDialog.FileName;
        }
    }
}