using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
		private readonly bool _defaultColorSetted;
		
        public MainWindow() {
            InitializeComponent();
            CmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			CmbFontSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

			// Times New Roman
			CmbFontFamily.SelectedIndex = 185;
			// 16
			CmbFontSize.SelectedIndex = 6;     

			RtbEditor.FontSize = 16;
			RtbEditor.FontFamily = new FontFamily("Times New Roman");

			ColorPicker.SelectedColor = Colors.Black;
			_defaultColorSetted = true;

		}

		private void RtbEditor_SelectionChanged(object sender, RoutedEventArgs e) {
			// check weight button
			object fontWeight = RtbEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty);
			BtnBold.IsChecked = fontWeight != DependencyProperty.UnsetValue && fontWeight.Equals(FontWeights.Bold);

			// check italic button
			object fontStyle = RtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
			BtnItalic.IsChecked = fontStyle != DependencyProperty.UnsetValue && fontStyle.Equals(FontStyles.Italic);

			// check underline button
			TextPointer caret = RtbEditor.CaretPosition;
			var paragraph = RtbEditor.Document.Blocks.FirstOrDefault(x => x.ContentStart.CompareTo(caret) == -1 && x.ContentEnd.CompareTo(caret) == 1) as Paragraph;

			if (paragraph?.Inlines.FirstOrDefault(x => x.ContentStart.CompareTo(caret) == -1 && x.ContentEnd.CompareTo(caret) == 1) is Inline inline) {
				TextDecorationCollection decorations = inline.TextDecorations;
				BtnUnderline.IsChecked = decorations != DependencyProperty.UnsetValue && decorations != null && decorations.Contains(TextDecorations.Underline[0]);
			}
			
			// check color pick
			var colorBrush = (SolidColorBrush) RtbEditor.Selection.GetPropertyValue(TextElement.ForegroundProperty);
			ColorPicker.SelectedColor = Color.FromArgb(
				colorBrush.Color.A,
				colorBrush.Color.R,
				colorBrush.Color.G,
				colorBrush.Color.B);

			CmbFontFamily.SelectedItem = RtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
			CmbFontSize.Text = RtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty).ToString();

			RtbEditor.Focus();
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e) {
			var fileDialog = new OpenFileDialog {Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"};
			
			if (fileDialog.ShowDialog() == true) {
				var fileStream = new FileStream(fileDialog.FileName, FileMode.Open);
				var range = new TextRange(RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
				range.Load(fileStream, DataFormats.Rtf);
			}
		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e) {
            var fileDialog = new SaveFileDialog {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"
            };

            if (fileDialog.ShowDialog() == true)
			{
				var fileStream = new FileStream(fileDialog.FileName, FileMode.Create);
				var range = new TextRange(RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
				range.Save(fileStream, DataFormats.Rtf);
			}
		}

		private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (CmbFontFamily.SelectedItem != null)
			{
				RtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, CmbFontFamily.SelectedItem);
			}
		}

		private void CmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (CmbFontSize.SelectedItem != null)
			{
				RtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, CmbFontSize.Text);
			}
		}

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
			if (_defaultColorSetted) {
				if (ColorPicker.SelectedColor != null) {
					RtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty,
						new SolidColorBrush((Color) ColorPicker.SelectedColor));
				}
			}
		}
    }
}
