using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        private string CurrentFileDir = null;
        private bool TextUnsaved = false;

        public Form1()
        {
            InitializeComponent();
            ChangeTitle(TextUnsaved);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (UnsavedWarning())
            {
                e.Cancel = true;
            }
        }

        private bool UnsavedWarning()
        {
            if (TextUnsaved)
            {
                if (MessageBox.Show("Are you sure? Unsaved data will be lost!", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return true;
                }
            }
            return false;
        }

        private void ChangeTitle(bool isUnsaved)
        {
            if (CurrentFileDir != null)
            {
                this.Text = CurrentFileDir + " - Notepadzik";
            }
            else
            {
                this.Text = "No title - Notepadzik";
            }

            if (isUnsaved)
            {
                this.Text += " !UNSAVED!";
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            if (UnsavedWarning())
                return;

            CurrentFileDir = null;
            richTextBox1.Text = "";
            TextUnsaved = false;
            ChangeTitle(TextUnsaved);
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (UnsavedWarning())
                return;

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open";
            openFile.InitialDirectory = Application.StartupPath;
            openFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                CurrentFileDir = openFile.FileName;
                richTextBox1.LoadFile(CurrentFileDir, RichTextBoxStreamType.PlainText);
                TextUnsaved = false;
                ChangeTitle(TextUnsaved);
            }
        }

        private void SaveAsWindow()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Save as";
            saveFile.InitialDirectory = Application.StartupPath;
            saveFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                CurrentFileDir = saveFile.FileName;
                richTextBox1.SaveFile(CurrentFileDir, RichTextBoxStreamType.PlainText);
            }
        }

        private void Save_as_Click(object sender, EventArgs e)
        {
            SaveAsWindow();
            TextUnsaved = false;
            ChangeTitle(TextUnsaved);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (CurrentFileDir != null)
            {
                richTextBox1.SaveFile(CurrentFileDir, RichTextBoxStreamType.PlainText);
            }
            else
            {
                SaveAsWindow();
            }

            TextUnsaved = false;
            ChangeTitle(TextUnsaved);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            TextUnsaved = true;
            ChangeTitle(TextUnsaved);
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontwindow = new FontDialog();
            fontwindow.Font = richTextBox1.Font;
            
            if (fontwindow.ShowDialog()==DialogResult.OK)
            {
                richTextBox1.Font = fontwindow.Font;
            }
        }

        private void darkThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkThemeToolStripMenuItem.Checked = !darkThemeToolStripMenuItem.Checked;

            if(darkThemeToolStripMenuItem.Checked)
            {
                richTextBox1.BackColor = Color.FromArgb(255,46, 46, 46);
                richTextBox1.ForeColor = Color.FromArgb(255, 199, 199, 199);
            }
            else
            {
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
            }

            TextUnsaved = false;
            ChangeTitle(TextUnsaved);
        }
    }
}
