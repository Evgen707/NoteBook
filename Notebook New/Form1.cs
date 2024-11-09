using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Notebook_New
{
    public partial class Form1 : Form
    {
        public int fontSize = 0;
        public System.Drawing.FontStyle fs = FontStyle.Regular;

        public string fileName;
        public bool isfileChange;

        public FontSettings FontSets;

        public Form1()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            fileName = "";
            isfileChange = false;
            UpdateTextWithTitle();
        }

        public void CreateNewDocument(object sender, EventArgs e) 
        {
            SaveUnsavedFile();
            textBox1.Text = "";
            fileName = "";
            isfileChange |= false;
            UpdateTextWithTitle();
        }

        public void OpenFile(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            openFileDialog1.FileName = "";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                    {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    sr.Close();
                    fileName = openFileDialog1.FileName;
                    isfileChange = false;
                }
                catch 
                {
                    MessageBox.Show("Невозможно открыть файл!");
                }
            }
            UpdateTextWithTitle();
        }

        public void SaveFile (string _fileName)
        {
            if (_fileName == "")
            {
                if(saveFileDialog1.ShowDialog() == DialogResult.OK) 
                { 
                    _fileName = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter sw = new StreamWriter(_fileName + ".txt");
                sw.WriteLine(textBox1.Text);
                sw.Close();
                fileName = _fileName;
                isfileChange = false;
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить файл");
            }
            UpdateTextWithTitle();
        }

        public void Save (object sender, EventArgs e)
        {
            SaveFile(fileName);
        }

        public void SaveAs (object sender, EventArgs e)
        {
            SaveFile("");
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (!isfileChange)
            {
                this.Text = this.Text.Replace('*', ' ');
                isfileChange = true;
                this.Text = "*" + this.Text;
            }
        }

        public void UpdateTextWithTitle()
        {
            if (fileName != "")
            {
                this.Text = fileName + " - Блокнот";
            }
            else
            {
                this.Text = "Безsмянный - Блокнот";
            }
            
        }

        public void SaveUnsavedFile()
        {
            if (isfileChange)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения в файле?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveFile(fileName);
                }
            }
        }

        public void CopyText()
        {
            Clipboard.SetText(textBox1.SelectedText);
        }

        public void CutText()
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
        }

        public void PasteText()
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.SelectionStart) + Clipboard.GetText() + textBox1.Text.Substring(textBox1.SelectionStart, textBox1.Text.Length - textBox1.SelectionStart);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void OneCopyClick(object sender, EventArgs e)
        {
            CopyText();
        }

        private void OneCutClick(object sender, EventArgs e)
        {
            CutText();
        }

        private void OnePastClick(object sender, EventArgs e)
        {
            PasteText();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUnsavedFile();
        }

        private void OnFontClick(object sender, EventArgs e)
        {
            FontSets = new FontSettings();
            FontSets.Show();
        }

        private void OnFocws(object sender, EventArgs e)
        {
            if(FontSets != null)
            {
                fontSize = FontSets.fontSize;
                fs = FontSets.fs;
                textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize, fs);
                FontSets.Close();
            }
        }
    }
}
