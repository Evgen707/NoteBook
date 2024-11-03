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
        public string fileName;
        public bool isfileChange;

        public Form1()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            fileName = "";
            isfileChange = false;
        }

        public void CreateNewDocument(object sender, EventArgs e) 
        {
            textBox1.Text = "";
            fileName = "";
        }

        public void OpenFile(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                    {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    fileName = openFileDialog1.FileName;
                }
                catch 
                {
                    MessageBox.Show("Невозможно открыть файл!");
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
