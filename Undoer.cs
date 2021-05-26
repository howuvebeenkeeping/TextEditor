using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TextEditor {
    public class Undoer
    {
        protected RichTextBox txtBox;
        protected List<string> LastData = new List<string>();
        protected int  undoCount = 0;

        protected bool undoing   = false;
        protected bool redoing   = false;


        public Undoer(ref RichTextBox txtBox)
        {
            this.txtBox = txtBox;
            LastData.Add(new TextRange(txtBox.Document.ContentStart, txtBox.Document.ContentEnd).Text);
        }

        public void undo_Click(object sender, EventArgs e)
        {
            this.Undo();
        }
        public void redo_Click(object sender, EventArgs e)
        {
            this.Redo();
        }

        public void Undo()
        {
            try
            {
                undoing = true;
                ++undoCount;
                txtBox.Document.Blocks.Add(new Paragraph(new Run(LastData[LastData.Count - undoCount - 1])));
                // txtBox.Text = LastData[LastData.Count - undoCount - 1];
            }
            catch { }
            finally{ this.undoing = false; }
        }
        public void Redo()
        {
            try
            {
                if (undoCount == 0)
                    return;

                redoing = true;
                --undoCount;
                txtBox.Document.Blocks.Add(new Paragraph(new Run(LastData[LastData.Count - undoCount - 1])));
                // txtBox.Text = LastData[LastData.Count - undoCount - 1];
            }
            catch { }
            finally{ this.redoing = false; }
        }

        public void Save()
        {
            if (undoing || redoing)
                return;

            if (LastData[LastData.Count - 1] == new TextRange(txtBox.Document.ContentStart, txtBox.Document.ContentEnd).Text)
                return;

            LastData.Add(new TextRange(txtBox.Document.ContentStart, txtBox.Document.ContentEnd).Text);
            undoCount = 0;
        }
    }
}