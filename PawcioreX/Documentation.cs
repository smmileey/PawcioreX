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
    public partial class Documentation : Form
    {
        public string PathToFile { get; set; }
        public string TiteLabel { get; set; }
        public string RtfContent { get; set; }
        public bool UseRtf { get; set; }

        public Documentation()
        {
            InitializeComponent();
            CenterToParent();

            this.rtf.GotFocus += Rtf_GotFocus;
        }

        private void Rtf_GotFocus(object sender, EventArgs e)
        {
            this.label_title.Focus();
        }

        private void UserDocumentation_Load(object sender, EventArgs e)
        {
            try
            {
                string path = PathToFile;
                this.label_title.Text = TiteLabel;
                if (UseRtf)
                    this.rtf.Rtf = RtfContent;
                else
                    this.rtf.LoadFile(path);
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("System plików został manualnie zmieniony, konieczna reinstalacja programu.");
                this.Close();
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Plik jest używany przez inny proces!");
                this.Close();
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Błąd implementacji, nieprawidłowy format pliku.");
            }
           
        }

    }
}
