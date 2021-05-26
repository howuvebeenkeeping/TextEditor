using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		private readonly bool _defaultColorSet;
		private readonly bool _defaultFontSizeSet;
		
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
			_defaultColorSet = true;
			_defaultFontSizeSet = true;
        }

		private void RtbEditor_SelectionChanged(object sender, RoutedEventArgs e) {
			try {
				var textRange = new TextRange(RtbEditor.Selection.Start, RtbEditor.Selection.End);
				
				// check weight button
				object fontWeight = textRange.GetPropertyValue(TextElement.FontWeightProperty);
				BtnBold.IsChecked = fontWeight != DependencyProperty.UnsetValue && fontWeight.Equals(FontWeights.Bold);

				// check italic button
				object fontStyle = textRange.GetPropertyValue(TextElement.FontStyleProperty);
				BtnItalic.IsChecked = fontStyle != DependencyProperty.UnsetValue && fontStyle.Equals(FontStyles.Italic);

				// check underline button
				TextPointer caret = RtbEditor.CaretPosition;
				var paragraph = RtbEditor.Document.Blocks.FirstOrDefault(x =>
					x.ContentStart.CompareTo(caret) == -1 && x.ContentEnd.CompareTo(caret) == 1) as Paragraph;
				
				if (paragraph?.Inlines.FirstOrDefault(x =>
					x.ContentStart.CompareTo(caret) == -1 && x.ContentEnd.CompareTo(caret) == 1) is Inline inline) {
					TextDecorationCollection decorations = inline.TextDecorations;
					BtnUnderline.IsChecked = decorations != DependencyProperty.UnsetValue && decorations != null &&
					                         decorations.Contains(TextDecorations.Underline[0]);
				}
				
				// object textDecoration = textRange.GetPropertyValue(Inline.TextDecorationsProperty);
				// BtnUnderline.IsChecked = textDecoration != DependencyProperty.UnsetValue && textDecoration.Equals(TextDecorations.Underline);
				
				if (!textRange.IsEmpty) {
					// check color pick
					if (textRange.GetPropertyValue(TextElement.ForegroundProperty) is SolidColorBrush colorBrush) {
						ColorPicker.SelectedColor = Color.FromArgb(
							colorBrush.Color.A,
							colorBrush.Color.R,
							colorBrush.Color.G,
							colorBrush.Color.B);
					}
					
					// font family check
					object fontFamily = textRange.GetPropertyValue(TextElement.FontFamilyProperty);
					if (fontFamily != null) {
						CmbFontFamily.SelectedItem = fontFamily;
					}
					
					// font size check
					object fontSize = textRange.GetPropertyValue(TextElement.FontSizeProperty);
					if (fontSize != null) {
						CmbFontSize.Text = fontSize.ToString();
					}
				}
				
			} catch (Exception ex) {
				Debug.WriteLine(ex.Message);
			}

			RtbEditor.Focus();
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e) {
			var fileDialog = new OpenFileDialog {
				Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"
			};
			
			if (fileDialog.ShowDialog() == true) {
				var fileStream = new FileStream(fileDialog.FileName, FileMode.Open);
				var textRange = new TextRange(RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
				textRange.Load(fileStream, DataFormats.Rtf);
			}
		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e) {
            var fileDialog = new SaveFileDialog {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"
            };

            if (fileDialog.ShowDialog() == true) {
				var fileStream = new FileStream(fileDialog.FileName, FileMode.Create);
				var textRange = new TextRange(RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
				textRange.Save(fileStream, DataFormats.Rtf);
			}
		}

		private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (CmbFontFamily.SelectedItem != null) {
				RtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, CmbFontFamily.SelectedValue);
			}

			RtbEditor.Focus();
		}

		private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e) {
			if (_defaultColorSet) {
				if (ColorPicker.SelectedColor != null) {
					RtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty,
						new SolidColorBrush((Color) ColorPicker.SelectedColor));
				}
			}
		}

        private void CmbFontSize_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
	        if (_defaultFontSizeSet) {
		        var textRange = new TextRange(RtbEditor.Selection.Start, RtbEditor.Selection.End);
		        textRange.ApplyPropertyValue(TextElement.FontSizeProperty, CmbFontSize.SelectedItem);
		        
		        RtbEditor.Focus();
	        }
        }

        private void UndoBtn_OnClick(object sender, RoutedEventArgs e) {
	        if (RtbEditor.CanUndo) {
		        RtbEditor.Undo();
	        }
        }

        private void RedoBtn_OnClick(object sender, RoutedEventArgs e) {
	        if (RtbEditor.CanRedo) {
		        RtbEditor.Redo();
	        }
        }
    }
}
