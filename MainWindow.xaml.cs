using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace TextEditor
{
    public partial class MainWindow : Window 
	{
		public MainWindow() 
		{
            InitializeComponent();
			
			TextFormatter.RichTextBox = RichTextBox;
            CmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			CmbFontSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

			// setting defaults
			RichTextBox.FontFamily = new FontFamily("Times New Roman");
			RichTextBox.FontSize = 20;
			ColorPicker.SelectedColor = Colors.Black;
        }

		private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e) 
		{
			try 
			{
				// check toggle buttons
				BtnBold.IsChecked = TextFormatter.IsBoldEnabled;
				BtnItalic.IsChecked = TextFormatter.IsItalicEnabled;
				BtnUnderline.IsChecked = TextFormatter.IsUnderlineEnabled;

				// check color pick
				if (TextFormatter.FontColor is SolidColorBrush colorBrush) 
				{
					ColorPicker.SelectedColor = Color.FromArgb(
						colorBrush.Color.A,
						colorBrush.Color.R,
						colorBrush.Color.G,
						colorBrush.Color.B);
				}

				// check font family 
				CmbFontFamily.SelectedItem = TextFormatter.FontFamily ?? CmbFontFamily.SelectedItem;
				// check font size 
				CmbFontSize.Text = TextFormatter.FontSize?.ToString() ?? CmbFontSize.Text;
				
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
			TextFormatter.Clear();
			using (FileStream fileStream = DataStorage.Open())
			{
				TextFormatter.TextRange.Load(fileStream, DataFormats.Rtf);
			}
		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e) 
		{
			DataStorage.Save(TextFormatter.TextRange);
		}

		private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (CmbFontFamily.SelectedItem == null) return;
			TextFormatter.FontFamily = CmbFontFamily.SelectedValue;
			RichTextBox.Focus();
		}

		private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
		{
			if (TextFormatter.FontColor == null || ColorPicker.SelectedColor == null) return;
			TextFormatter.FontColor = ColorPicker.SelectedColor;
			RichTextBox.Focus();
		}

		private void CmbFontSize_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (TextFormatter.FontSize == null || CmbFontSize.SelectedItem == null) return;
			TextFormatter.FontSize = CmbFontSize.SelectedItem;
			RichTextBox.Focus();
		}

		private void UndoBtn_OnClick(object sender, RoutedEventArgs e) 
		{
			if (RichTextBox.CanUndo) 
			{
				RichTextBox.Undo();
			}
		}

		private void RedoBtn_OnClick(object sender, RoutedEventArgs e)
		{
			if (RichTextBox.CanRedo) 
			{
				RichTextBox.Redo();
			}
		}
	}
}
