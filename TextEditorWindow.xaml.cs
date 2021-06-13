using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TextEditor
{
    public partial class TextEditorWindow : Window
    {
	    private readonly TextFormatter _textFormatter;
	    private readonly DataStorage _dataStorage;
	    
		public TextEditorWindow() 
		{
			InitializeComponent();
            _textFormatter = new TextFormatter(RichTextBox);
            _dataStorage = DataStorage.GetInstance();
            
			// setting editor defaults
            FontFamilyCmb.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			FontSizeCmb.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
			RichTextBox.FontFamily = new FontFamily("Times New Roman");
			RichTextBox.FontSize = 20;
			ColorPicker.SelectedColor = Colors.Black;
        }

		private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e) 
		{
			try 
			{
				// update toggle buttons
				BoldBtn.IsChecked = _textFormatter.IsBold;
				ItalicBtn.IsChecked = _textFormatter.IsItalic;
				UnderlineBtn.IsChecked = _textFormatter.IsUnderline;

				// update color picker
				if (_textFormatter.FontColor is SolidColorBrush colorBrush) 
				{
					ColorPicker.SelectedColor = Color.FromArgb(
						colorBrush.Color.A,
						colorBrush.Color.R,
						colorBrush.Color.G,
						colorBrush.Color.B);
				}

				// update font family 
				FontFamilyCmb.SelectedItem = _textFormatter.FontFamily ?? FontFamilyCmb.SelectedItem;
				_textFormatter.FontFamily = FontFamilyCmb.SelectedItem;
				
				// update font size 
				FontSizeCmb.Text = _textFormatter.FontSize?.ToString() ?? FontSizeCmb.Text;
				_textFormatter.FontSize = FontSizeCmb.Text;

				// update undo/redo buttons
				UndoBtn.IsEnabled = RichTextBox.CanUndo;
				SaveBtn.IsEnabled = RichTextBox.CanUndo;
				RedoBtn.IsEnabled = RichTextBox.CanRedo;
				
				// update title
				Title = _dataStorage.DocumentName + (RichTextBox.CanUndo ? " [unsaved]" : "");
			} 
			catch (Exception ex) 
			{
				Debug.WriteLine(ex.Message);
			}

			RichTextBox.Focus();
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			_textFormatter.ClearDocument();
			try
			{
				using (var fileStream = new FileStream(_dataStorage.OpenDocument(), FileMode.Open))
				{
					_textFormatter.DocumentRange.Load(fileStream, DataFormats.Rtf);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			SetEditingDefaults();
		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e) 
		{
			try
			{
				_dataStorage.SaveDocument(_textFormatter.DocumentRange);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			SetEditingDefaults();
		}

		private void SetEditingDefaults()
		{
			Title = _dataStorage.DocumentName;
			_textFormatter.ClearUndoStack();
			SaveBtn.IsEnabled = UndoBtn.IsEnabled = RedoBtn.IsEnabled = false;
		}

		private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (FontFamilyCmb.SelectedItem == null) return;
			_textFormatter.FontFamily = FontFamilyCmb.SelectedValue;
			RichTextBox.Focus();
		}

		private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
		{
			if (_textFormatter.FontColor == null || ColorPicker.SelectedColor == null) return;
			_textFormatter.FontColor = ColorPicker.SelectedColor;
			RichTextBox.Focus();
		}

		private void CmbFontSize_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_textFormatter.FontSize == null || FontSizeCmb.SelectedItem == null) return;
			_textFormatter.FontSize = FontSizeCmb.SelectedItem;
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
