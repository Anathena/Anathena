namespace Ana.View
{
    using ICSharpCode.AvalonEdit.CodeCompletion;
    using ICSharpCode.AvalonEdit.Document;
    using ICSharpCode.AvalonEdit.Editing;
    using ICSharpCode.AvalonEdit.Highlighting;
    using ICSharpCode.AvalonEdit.Highlighting.Xshd;
    using Source.LuaEngine;
    using Source.Utils.ScriptEditor;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Xml;

    /// <summary>
    /// Interaction logic for ScriptEditor.xaml
    /// </summary>
    internal partial class ScriptEditor : Window
    {
        private const String ScriptSyntaxHighlightingResource = "Ana.Content.Lua.xhsd";

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptEditor" /> class
        /// </summary>
        public ScriptEditor(String script = null)
        {
            this.InitializeComponent();
            this.LoadHightLightRule();
            this.InitializeCompleteionWindow();
            //// this.ScriptEditorTextEditor.TextArea.TextEntering += ScriptEditorTextEditorTextAreaTextEntering;
            //// this.ScriptEditorTextEditor.TextArea.TextEntered += ScriptEditorTextEditorTextAreaTextEntered;
            this.ScriptEditorTextEditor.TextChanged += ScriptEditorTextEditorTextChanged;
            this.ScriptEditorTextEditor.Text = script == null ? String.Empty : script;
        }

        private void ScriptEditorTextEditorTextChanged(Object sender, EventArgs e)
        {
            this.ScriptEditorViewModel.UpdateScriptCommand.Execute(this.ScriptEditorTextEditor.Text);
        }

        public ScriptEditorViewModel ScriptEditorViewModel
        {
            get
            {
                return this.DataContext as ScriptEditorViewModel;
            }
        }

        private IList<ICompletionData> CompletionData { get; set; }

        private void InitializeCompleteionWindow()
        {
            this.CompletionData = new List<ICompletionData>();
            this.CompletionData.Add(new MyCompletionData("Memory"));
            this.CompletionData.Add(new MyCompletionData("Engine"));
            this.CompletionData.Add(new MyCompletionData("Graphics"));
        }

        private void LoadHightLightRule()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ScriptEditor.ScriptSyntaxHighlightingResource))
            {
                if (stream != null)
                {
                    using (XmlTextReader reader = new XmlTextReader(stream))
                    {
                        this.ScriptEditorTextEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                    }
                }
            }
        }

        private CompletionWindow CompletionWindow { get; set; }

        void ScriptEditorTextEditorTextAreaTextEntered(Object sender, TextCompositionEventArgs e)
        {
            //// if (e.Text == ".")
            {
                //// Open code completion after the user has pressed dot:
                this.CompletionWindow = new CompletionWindow(this.ScriptEditorTextEditor.TextArea);
                this.CompletionWindow.Closed += delegate
                {
                    this.CompletionWindow = null;
                };

                foreach (ICompletionData data in CompletionData)
                {
                    this.CompletionWindow.CompletionList.CompletionData.Add(data);
                }

                this.CompletionWindow.Show();
            }
        }

        void ScriptEditorTextEditorTextAreaTextEntering(Object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && this.CompletionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open, insert the currently selected element
                    this.CompletionWindow.CompletionList.RequestInsertion(e);
                }
            }
        }

        private void CancelButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void AcceptButtonClick(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /// Implements AvalonEdit ICompletionData interface to provide the entries in the completion drop down
        internal class MyCompletionData : ICompletionData
        {
            public MyCompletionData(string text)
            {
                this.Text = text;
            }

            public ImageSource Image
            {
                get { return null; }
            }

            public String Text { get; private set; }

            // Use this property if you want to show a fancy UIElement in the list
            public Object Content
            {
                get
                {
                    return this.Text;
                }
            }

            public Object Description
            {
                get
                {
                    //// return "Description for " + this.Text;
                    return String.Empty;
                }
            }

            public Double Priority
            {
                get
                {
                    return 1.0;
                }
            }

            public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
            {
                textArea.Document.Replace(completionSegment, this.Text);
            }
        }

        private void ExitFileMenuItemClick(Object sender, RoutedEventArgs e)
        {
            this.ScriptEditorViewModel.ExitCommand.Execute(null);
            this.Close();
        }

        private void CodeInjectionFileMenuItemClick(Object sender, RoutedEventArgs e)
        {
            this.ScriptEditorTextEditor.Text = LuaTemplates.GetCodeInjectionTemplate() + this.ScriptEditorTextEditor.Text;
        }

        private void GraphicsOverlayFileMenuItemClick(Object sender, RoutedEventArgs e)
        {
            this.ScriptEditorTextEditor.Text = LuaTemplates.GetGraphicsOverlayTemplate() + this.ScriptEditorTextEditor.Text;
        }
    }
    //// End class
}
//// End namespace