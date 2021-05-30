using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TextEditor {
    public class TextFormatter {
        private readonly RichTextBox _richTextBox;
        public TextRange TextRange => new TextRange(_richTextBox.Selection.Start, _richTextBox.Selection.End);
        public object FontWeight => TextRange.GetPropertyValue(TextElement.FontWeightProperty);
        public object FontStyle => TextRange.GetPropertyValue(TextElement.FontStyleProperty);
        public object FontColor {
            get => TextRange.GetPropertyValue(TextElement.ForegroundProperty);
            set => TextRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush((Color)value));
        }

        public object FontSize { 
            get => TextRange.GetPropertyValue(TextElement.FontSizeProperty);
            set => TextRange.ApplyPropertyValue(TextElement.FontSizeProperty, value);
        }
        public object FontFamily { 
            get => TextRange.GetPropertyValue(TextElement.FontFamilyProperty);
            set => TextRange.ApplyPropertyValue(TextElement.FontFamilyProperty, value);
        }

        public TextFormatter(RichTextBox richTextBox) {
            this._richTextBox = richTextBox;
        }

    }
}
