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

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
	{
		private readonly TextFormatter _textFormatter;
		
        public MainWindow() 
		{
            InitializeComponent();
			
			_textFormatter = new TextFormatter(RichTextBox);
            CmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			CmbFontSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

			// setting defaults
			RichTextBox.FontFamily = new FontFamily("Times New Roman");
			RichTextBox.FontSize = 16;
			ColorPicker.SelectedColor = Colors.Black;
        }

		private bool CheckFontDecoration(object fontDecoration) 
		{
			switch (fontDecoration) 
			{
				case FontWeight fontWeight:
					return fontDecoration != DependencyProperty.UnsetValue && fontWeight.Equals(FontWeights.Bold);
				case FontStyle fontStyle:
					return fontDecoration != DependencyProperty.UnsetValue && fontStyle.Equals(FontStyles.Italic);
			}

			return false;
		}
		
		private bool CheckUnderline()
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

		private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e) 
		{
			try 
			{
				BtnBold.IsChecked = CheckFontDecoration(_textFormatter.FontWeight);  
				BtnItalic.IsChecked = CheckFontDecoration(_textFormatter.FontStyle);
				BtnUnderline.IsChecked = CheckUnderline();

				// object textDecoration = textRange.GetPropertyValue(Inline.TextDecorationsProperty);
				// BtnUnderline.IsChecked = textDecoration != DependencyProperty.UnsetValue && textDecoration.Equals(TextDecorations.Underline);

				// check color pick
				if (_textFormatter.FontColor is SolidColorBrush colorBrush) 
				{
					ColorPicker.SelectedColor = Color.FromArgb(
						colorBrush.Color.A,
						colorBrush.Color.R,
						colorBrush.Color.G,
						colorBrush.Color.B);
				}

				// font family check
				CmbFontFamily.SelectedItem = _textFormatter.FontFamily ?? CmbFontFamily.SelectedItem;
				// font size check
				CmbFontSize.Text = _textFormatter.FontSize?.ToString() ?? CmbFontSize.Text;
				
				// undo/redo button activation
				UndoBtn.IsEnabled = RichTextBox.CanUndo;
				RedoBtn.IsEnabled = RichTextBox.CanRedo;
			} 
			catch (Exception ex) 
			{
				Debug.WriteLine(ex.Message);
			}

			RichTextBox.Focus();
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e) 
		{
			DataStorage.Open(new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd));
		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e) 
		{
            DataStorage.Save(new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd));
		}

		private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (CmbFontFamily.SelectedItem != null) 
			{
				_textFormatter.FontFamily = CmbFontFamily.SelectedValue;
				RichTextBox.Focus();
			}
		}

		private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e) 
		{
			if (_textFormatter.FontColor != null && ColorPicker.SelectedColor != null) 
			{
				_textFormatter.FontColor = ColorPicker.SelectedColor;
				RichTextBox.Focus();
			}
		}

		private void CmbFontSize_OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
		{
			if (_textFormatter.FontSize != null && CmbFontSize.SelectedItem != null) 
			{
				_textFormatter.FontSize = CmbFontSize.SelectedItem;
				RichTextBox.Focus();
			}
		}

		private void UndoBtn_OnClick(object sender, RoutedEventArgs e) {
			if (RichTextBox.CanUndo) 
			{
				RichTextBox.Undo();
			}
		}

		private void RedoBtn_OnClick(object sender, RoutedEventArgs e) {
			if (RichTextBox.CanRedo) 
			{
				RichTextBox.Redo();
			}
		}
	}
}
