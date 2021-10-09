using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TextEditor 
{
    internal class TextFormatter
    {
        private readonly RichTextBox _richTextBox;

        public TextFormatter(RichTextBox richTextBox)
        {
            _richTextBox = richTextBox;
        }

        private TextRange SelectionRange => new TextRange(_richTextBox.Selection.Start, _richTextBox.Selection.End);

        public TextRange DocumentRange => new TextRange(_richTextBox.Document.ContentStart, _richTextBox.Document.ContentEnd);

        private object FontWeight => SelectionRange.GetPropertyValue(TextElement.FontWeightProperty);

        private object FontStyle => SelectionRange.GetPropertyValue(TextElement.FontStyleProperty);

        public object FontColor
        {
            get => SelectionRange.GetPropertyValue(TextElement.ForegroundProperty);
            set => SelectionRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush((Color) value));
        }

        public object FontSize
        {
            get => SelectionRange.GetPropertyValue(TextElement.FontSizeProperty);
            set => SelectionRange.ApplyPropertyValue(TextElement.FontSizeProperty, value);
        }

        public object FontFamily
        {
            get => SelectionRange.GetPropertyValue(TextElement.FontFamilyProperty);
            set => SelectionRange.ApplyPropertyValue(TextElement.FontFamilyProperty, value);
        }

        public bool IsItalic => FontStyle != DependencyProperty.UnsetValue && FontStyle.Equals(FontStyles.Italic);

        public bool IsBold => FontWeight != DependencyProperty.UnsetValue && FontWeight.Equals(FontWeights.Bold);

        public bool IsUnderline
        {
            get
            {
                TextPointer caret = _richTextBox.CaretPosition;
                var paragraph = _richTextBox.Document.Blocks.FirstOrDefault(x =>
                    x.ContentStart.CompareTo(caret) == -1 && x.ContentEnd.CompareTo(caret) == 1) as Paragraph;

                if (paragraph?.Inlines.FirstOrDefault(x =>
                    x.ContentStart.CompareTo(caret) == -1 && x.ContentEnd.CompareTo(caret) == 1) is Inline inline)
                {
                    TextDecorationCollection decorations = inline.TextDecorations;
                    return decorations != DependencyProperty.UnsetValue && decorations != null &&
                           decorations.Contains(TextDecorations.Underline[0]);
                }

                return false;
            }
        }

        public void ClearDocument()
        {
            _richTextBox.Document.Blocks.Clear();
        }
        
        public void ClearUndoStack()
        {
            _richTextBox.IsUndoEnabled = false;
            _richTextBox.IsUndoEnabled = true;
        }
    }
}
