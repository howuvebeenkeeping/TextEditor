using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TextEditor 
{
    internal static class TextFormatter
    {
        public static RichTextBox RichTextBox;
        public static TextRange TextRange => new TextRange(RichTextBox.Selection.Start, RichTextBox.Selection.End);
        public static TextRange DocumentRange => new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
        private static object FontWeight => TextRange.GetPropertyValue(TextElement.FontWeightProperty);
        private static object FontStyle => TextRange.GetPropertyValue(TextElement.FontStyleProperty);
        public static object FontColor
        {
            get => TextRange.GetPropertyValue(TextElement.ForegroundProperty);
            set => TextRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush((Color) value));
        }
        public static object FontSize
        {
            get => TextRange.GetPropertyValue(TextElement.FontSizeProperty);
            set => TextRange.ApplyPropertyValue(TextElement.FontSizeProperty, value);
        }
        public static object FontFamily
        {
            get => TextRange.GetPropertyValue(TextElement.FontFamilyProperty);
            set => TextRange.ApplyPropertyValue(TextElement.FontFamilyProperty, value);
        }
        public static bool IsItalicEnabled => FontStyle != DependencyProperty.UnsetValue && FontStyle.Equals(FontStyles.Italic);
        public static bool IsBoldEnabled => FontWeight != DependencyProperty.UnsetValue && FontWeight.Equals(FontWeights.Bold);
        public static bool IsUnderlineEnabled
        {
            get
            {
                TextPointer caret = RichTextBox.CaretPosition;
                var paragraph = RichTextBox.Document.Blocks.FirstOrDefault(x =>
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

        public static void ClearDocument()
        {
            RichTextBox.Document.Blocks.Clear();
        }
        
        public static void ClearUndoStack()
        {
            RichTextBox.IsUndoEnabled = false;
            RichTextBox.IsUndoEnabled = true;
        }
    }
}
