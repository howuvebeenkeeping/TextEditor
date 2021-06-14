﻿#pragma checksum "..\..\TextEditorWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3145E20C33649AFC87F873C7666F104D3F640A885B596E5B12953DE182C42DE9"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Converters;
using Xceed.Wpf.Toolkit.Core;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Mag.Converters;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace TextEditor {
    
    
    /// <summary>
    /// TextEditorWindow
    /// </summary>
    public partial class TextEditorWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OpenBtn;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveBtn;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UndoBtn;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RedoBtn;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton BoldBtn;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton ItalicBtn;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton UnderlineBtn;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FontFamilyCmb;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FontSizeCmb;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.ColorPicker ColorPicker;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\TextEditorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox RichTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TextEditor;component/texteditorwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TextEditorWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\TextEditorWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Open_Executed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 12 "..\..\TextEditorWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Save_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.OpenBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.SaveBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.UndoBtn = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\TextEditorWindow.xaml"
            this.UndoBtn.Click += new System.Windows.RoutedEventHandler(this.UndoBtn_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RedoBtn = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\TextEditorWindow.xaml"
            this.RedoBtn.Click += new System.Windows.RoutedEventHandler(this.RedoBtn_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BoldBtn = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 8:
            this.ItalicBtn = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 9:
            this.UnderlineBtn = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 10:
            this.FontFamilyCmb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 39 "..\..\TextEditorWindow.xaml"
            this.FontFamilyCmb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbFontFamily_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.FontSizeCmb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 40 "..\..\TextEditorWindow.xaml"
            this.FontSizeCmb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbFontSize_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ColorPicker = ((Xceed.Wpf.Toolkit.ColorPicker)(target));
            
            #line 42 "..\..\TextEditorWindow.xaml"
            this.ColorPicker.SelectedColorChanged += new System.Windows.RoutedPropertyChangedEventHandler<System.Nullable<System.Windows.Media.Color>>(this.ColorPicker_SelectedColorChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            this.RichTextBox = ((System.Windows.Controls.RichTextBox)(target));
            
            #line 44 "..\..\TextEditorWindow.xaml"
            this.RichTextBox.SelectionChanged += new System.Windows.RoutedEventHandler(this.RichTextBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
