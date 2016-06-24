using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PawcioreX
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
            CenterToParent();
        }

        private void Info_Load(object sender, EventArgs e)
        {
            try
            {
                string text = Properties.Resources.about;
                this.fixedRichTextBoxcs1.Rtf = text;
                this.fixedRichTextBoxcs1.SelectionStart = this.fixedRichTextBoxcs1.TextLength;
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("System plików został manualnie zmieniony, konieczna reinstalacja programu.");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Plik jest używany przez inny proces!");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
