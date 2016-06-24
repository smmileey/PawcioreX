using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PawcioreX
{
    //formatka do obsługi przycisku "Data/godz."
    public partial class DateTimeMod : Form
    {
        List<string> dtTable = new List<string>(); 
        private string chosenDateTimeFormat; 
        public string ChosenDateTimeFormat
        {
            get { return this.chosenDateTimeFormat; }
        }

        public DateTimeMod()
        {
            InitializeComponent();
            this.CenterToParent();
            DateTime dt = DateTime.Now;
                
            dtTable.Add(dt.ToString()); //2015-09-28 15:10
            dtTable.Add(dt.Date.ToShortDateString()); //2015-09-28
            dtTable.Add(string.Format("{0}-{1}-{2}", DateTime.Now.Year.ToString().Remove(0,2), 
                DateTime.Now.Month.ToString().PadLeft(2,'0'), DateTime.Now.Day)); //15-09-2015
            dtTable.Add(dt.Date.ToLongDateString()); //28 września 2015
            dtTable.Add(dt.ToLongTimeString()); //15:25:11
            dtTable.Add(dt.ToShortTimeString()); //15:26

            foreach(string s in dtTable)
            {
                this.listBoxDateTime.Items.Add(s);
            }
        }

        #region Przycisk "OK"
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBoxDateTime.SelectedItem != null)
            {
                this.chosenDateTimeFormat = (string)this.listBoxDateTime.SelectedItem;
            }
            else
                this.chosenDateTimeFormat = "";
        }
        #endregion
    }
}
