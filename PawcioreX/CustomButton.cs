using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace PawcioreX
{
    //Tworzymy własny przycisk, aby spełniał nasze wymagania do wykorzystania, jako przycisk wyboru koloru na palecie kolorów
    //czyli:
    //-> po najechaniu myszką na przycisk, ma się pojawić wokół przycisku ramka
    //-> po zjechaniu myszką z przycisku, ma ona zniknąć
    //-> po naciśnięciu przycisku, ma się pojawić wokół niego ramka i ma ona nie znikać, póki przycisk jest ostatnio wybranym
    //
    //Nasz przycisk będzie wyprowadzony z klasy bazowej Button, wyłączymy domyślne obramowanie tego typu, aby pozbyć się automatycznego
    //podwójnego obramowania, które pojawia się, gdy klikniemy przycisk i ustawimy mu Border Color.
    //Kwestia ostatnio wybranego przycisku pozostaje w kwestii użytkownika, aby to ułatwić tworzymy właściwość "Checked", która
    //jest typu bool i zwraca true, gdy przycisk zostanie wciśnięty
    //
    // Ważne! BorderColor może być włączone/wyłączony wyłącznie, gdy przycisk ma FlatStyle: Flat za pomocą właściwości FlatAppearance
    public class CustomButton : Button
    {
        public bool ShowBorder { get; set; } 
        public bool Checked { get; set; } 

        public bool IsBordered { get; set; } //dla przycisku z białym kolorem- można ustawić, żeby polepszyć wygląd
        public CustomButton() : base() 
        {

            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat; 
            this.FlatAppearance.BorderSize = 0; 
            this.BackColor = Color.Gray; 
            
            //teraz ustawienia dotyczące koloru ramki, a także jego tła podczas kliknięcia/najechania/zjechania myszką na przycisk
            //tylko tę ramkę będziemy rysować (domyślna jest wyłączona) po najechaniu myszką/kliknięciu na przycisk
            this.FlatAppearance.BorderColor = Color.Orange;
            this.FlatAppearance.CheckedBackColor = this.BackColor;
            this.FlatAppearance.MouseDownBackColor = this.BackColor;
            this.FlatAppearance.MouseOverBackColor = this.BackColor;

            Size = new System.Drawing.Size(17, 17); 
        }

        protected override void OnMouseEnter(EventArgs e) 
        {
            base.OnMouseEnter(e); 

            ShowBorder = true; 
        }
        protected override void OnMouseLeave(EventArgs e) //j.w
        {
            base.OnMouseLeave(e);

            if(!this.Checked)
                ShowBorder = false;
        }

        protected override void OnPaint(PaintEventArgs pevent) 
        {
            base.OnPaint(pevent); 

            if (DesignMode || ShowBorder)
            {
                Pen pen = new Pen(this.FlatAppearance.BorderColor, 2);
                Rectangle rect = new Rectangle(1, 1, this.Size.Width - 2, this.Size.Height - 2);
                pevent.Graphics.DrawRectangle(pen, rect);
            }
            //opcja dodatkowa (przydatna dla białego przycisku, żeby go lepiej było widać)
            if(IsBordered)
            {
                pevent.Graphics.DrawRectangle(Pens.Gray,0,0,this.Size.Width-1,this.Size.Height-1);
            }
        }

        protected override void OnClick(EventArgs e) 
        {
            base.OnClick(e); 

            this.Checked = true; 
            ShowBorder = true;
        }

        //nadpiszemy teraz właściwości Text i TextAlign, ponieważ nie chcemy, by na przyciskach mógł być wyświetlany tekst
        //mają one służyć tylko jako elementy wyboru na palecie kolorów
        [Browsable(false)] //właściwość Text nie będzie dostępna na liście właściwości
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = ""; }               
        }

        [Browsable(false)]
        public override ContentAlignment TextAlign //j.w
        {
            get { return base.TextAlign; }
            set { base.TextAlign = value; }
        }
    }
}
