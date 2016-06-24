using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PawcioreX
{
    public partial class PaletteFontColor : Panel
    {
        private Label lblAutomatic;
        private Label lblMotive;
        private Label lblStandards;
        private Label lblMoreColors;
        private Label lbldivider1;
        private Label lbldivider2;
        private CustomButton mainColorBtn;
        private CustomButton[] custBtnArray;
        private CustomButton helperCustomBtn;
        private Color[] colorArray;
        private ColorDialog colorDialog;

        public delegate void ColorHandler();
        public event ColorHandler ColorChanged;
        public Color RecentColor { get; private set; }

        public PaletteFontColor() : base()
        {
            //inicjalizacja
            InitializeComponent();
            this.lblAutomatic = new Label();
            this.lblMotive = new Label();
            this.lblStandards = new Label();
            this.lblMoreColors = new Label();
            this.lbldivider1 = new Label();
            this.lbldivider2 = new Label();
            this.mainColorBtn = new CustomButton();
            this.custBtnArray = new CustomButton[70];
            this.colorArray = new Color[70];
            this.helperCustomBtn = new CustomButton();
            this.colorDialog = new ColorDialog();

            //obsługa animacji
            //
            //przycisk główny (etykieta)
            this.lblAutomatic.MouseEnter += LblAutomatic_MouseEnter;
            this.lblAutomatic.Click += LblAutomatic_Click;
            this.lblAutomatic.MouseLeave += LblAutomatic_MouseLeave;
            //przycisk dolny (etykieta)
            this.lblMoreColors.MouseEnter += LblMoreColors_MouseEnter;
            this.lblMoreColors.MouseLeave += LblMoreColors_MouseLeave;
            this.lblMoreColors.Click += LblMoreColors_Click;

            //wizualizacja przycisków
            #region Dobranie kolorow
            //rzad 1
            this.colorArray[0] = Color.FromArgb(0, 0, 0);
            this.colorArray[1] = Color.FromArgb(32, 32, 32);
            this.colorArray[2] = Color.FromArgb(64, 64, 64);
            this.colorArray[3] = Color.FromArgb(96, 96, 96);
            this.colorArray[4] = Color.FromArgb(160, 160, 160);
            this.colorArray[5] = Color.FromArgb(255, 255, 255);
            //rząd 2
            this.colorArray[11] = Color.FromArgb(255, 102, 255);
            this.colorArray[10] = Color.FromArgb(255, 51, 255);
            this.colorArray[9] = Color.FromArgb(255, 0, 255);
            this.colorArray[8] = Color.FromArgb(204, 0, 204);
            this.colorArray[7] = Color.FromArgb(102, 0, 102);
            this.colorArray[6] = Color.FromArgb(51, 0, 51);
            //rząd 3
            this.colorArray[17] = Color.FromArgb(102, 102, 255);
            this.colorArray[16] = Color.FromArgb(51, 51, 255);
            this.colorArray[15] = Color.FromArgb(0, 0, 255);
            this.colorArray[14] = Color.FromArgb(0, 0, 204);
            this.colorArray[13] = Color.FromArgb(0, 0, 102);
            this.colorArray[12] = Color.FromArgb(0, 0, 51);
            //rząd 4
            this.colorArray[23] = Color.FromArgb(153, 204, 255);
            this.colorArray[22] = Color.FromArgb(102, 178, 255);
            this.colorArray[21] = Color.FromArgb(0, 128, 255);
            this.colorArray[20] = Color.FromArgb(0, 102, 204);
            this.colorArray[19] = Color.FromArgb(0, 76, 153);
            this.colorArray[18] = Color.FromArgb(0, 51, 102);
            //rząd 5
            this.colorArray[29] = Color.FromArgb(153, 255, 255);
            this.colorArray[28] = Color.FromArgb(102, 255, 255);
            this.colorArray[27] = Color.FromArgb(51, 255, 255);
            this.colorArray[26] = Color.FromArgb(0, 204, 204);
            this.colorArray[25] = Color.FromArgb(0, 153, 153);
            this.colorArray[24] = Color.FromArgb(0, 51, 51);
            //rząd 6
            this.colorArray[35] = Color.FromArgb(153, 255, 204);
            this.colorArray[34] = Color.FromArgb(102, 255, 178);
            this.colorArray[33] = Color.FromArgb(0, 255, 128);
            this.colorArray[32] = Color.FromArgb(0, 204, 102);
            this.colorArray[31] = Color.FromArgb(0, 102, 51);
            this.colorArray[30] = Color.FromArgb(0, 51, 25);
            //rząd 7
            this.colorArray[41] = Color.FromArgb(153, 255, 153);
            this.colorArray[40] = Color.FromArgb(102, 255, 102);
            this.colorArray[39] = Color.FromArgb(0, 255, 0);
            this.colorArray[38] = Color.FromArgb(0, 204, 0);
            this.colorArray[37] = Color.FromArgb(0, 102, 0);
            this.colorArray[36] = Color.FromArgb(0, 51, 0);
            //rząd 8
            this.colorArray[47] = Color.FromArgb(255, 255, 153);
            this.colorArray[46] = Color.FromArgb(255, 255, 102);
            this.colorArray[45] = Color.FromArgb(255, 255, 0);
            this.colorArray[44] = Color.FromArgb(204, 204, 0);
            this.colorArray[43] = Color.FromArgb(102, 102, 0);
            this.colorArray[42] = Color.FromArgb(51, 51, 0);
            //rząd 9
            this.colorArray[53] = Color.FromArgb(255, 204, 153);
            this.colorArray[52] = Color.FromArgb(255, 153, 51);
            this.colorArray[51] = Color.FromArgb(255, 128, 0);
            this.colorArray[50] = Color.FromArgb(204, 102, 0);
            this.colorArray[49] = Color.FromArgb(153, 76, 0);
            this.colorArray[48] = Color.FromArgb(51, 25, 0);
            //rząd 10
            this.colorArray[59] = Color.FromArgb(255, 153, 153);
            this.colorArray[58] = Color.FromArgb(255, 51, 51);
            this.colorArray[57] = Color.FromArgb(255, 0, 0);
            this.colorArray[56] = Color.FromArgb(204, 0, 0);
            this.colorArray[55] = Color.FromArgb(153, 0, 0);
            this.colorArray[54] = Color.FromArgb(51, 0, 0);
            //standardowe
            this.colorArray[60] = Color.FromArgb(102, 0, 102);
            this.colorArray[61] = Color.FromArgb(0, 0, 102);
            this.colorArray[62] = Color.FromArgb(0, 102, 204);
            this.colorArray[63] = Color.FromArgb(0, 204, 204);
            this.colorArray[64] = Color.FromArgb(51, 255, 51);
            this.colorArray[65] = Color.FromArgb(153, 255, 51);
            this.colorArray[66] = Color.FromArgb(255, 255, 0);
            this.colorArray[67] = Color.FromArgb(255, 204, 0);
            this.colorArray[68] = Color.FromArgb(255, 0, 0);
            this.colorArray[69] = Color.FromArgb(153, 0, 0);
            #endregion
            #region Ustawienie guzików
            //główny
            this.mainColorBtn.Size = new Size(17, 17);
            this.mainColorBtn.Location = new Point(4, 2);
            this.mainColorBtn.ForeColor = SystemColors.ControlText;
            this.mainColorBtn.BackColor = Color.FromArgb(0, 0, 0);
            this.mainColorBtn.UseVisualStyleBackColor = false;

            //paleta (mieszanka + standardowe)
            int n = 3, k = 39, g = 5, n1 = 165;
            for (int i = 0; i < this.custBtnArray.Length; i++)
            {
                this.custBtnArray[i] = (CustomButton)Activator.CreateInstance(typeof(CustomButton));
                this.custBtnArray[i].Size = new Size(12, 12);
                this.custBtnArray[i].ForeColor = SystemColors.ControlText;
                this.custBtnArray[i].BackColor = this.colorArray[i];
                this.custBtnArray[i].UseVisualStyleBackColor = false;
                this.custBtnArray[i].FlatStyle = FlatStyle.Flat;
                this.custBtnArray[i].ShowBorder = false;
                this.custBtnArray[i].Checked = false;

                if (i < 60)
                {
                    if (i % 6 == 0)
                    {
                        this.custBtnArray[i].Location = new Point(n, k);
                        k += 23;
                    }
                    else if (i < g)
                    {
                        this.custBtnArray[i].Location = new Point(n, k);
                        k += 13;
                    }
                    else
                    {
                        this.custBtnArray[i].Location = new Point(n, k);
                        k = 39;
                        n += 18;
                        g += 6;
                    }
                }
                else
                {
                    this.custBtnArray[i].Location = new Point(n1, 148);
                    n1 -= 18;
                }
            }
            //specjalne obramowanie dla białego guzika
            this.custBtnArray[5].IsBordered = true;
            #endregion
            #region Guzik pomocniczy
            this.helperCustomBtn = this.mainColorBtn; //domyślnie przypisany jest color "Black"
            this.helperCustomBtn.Checked = true;
            this.helperCustomBtn.ShowBorder = true;
            #endregion

            //konfiguracja etykiet
            #region Etykiety
            this.lblAutomatic.Size = new Size(182, 21);
            this.lblAutomatic.Location = new Point(0, 0);
            this.lblAutomatic.ForeColor = Color.FromArgb(0, 0, 64);
            this.lblAutomatic.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAutomatic.Text = "Automatycznie";
            this.lblAutomatic.Font = new Font("Microsoft Sans Serif", 8);

            this.lblMotive.Size = new Size(182, 15);
            this.lblMotive.Location = new Point(-1, 21);
            this.lblMotive.ForeColor = Color.Navy;
            this.lblMotive.BackColor = Color.FromArgb(192, 255, 255);
            this.lblMotive.TextAlign = ContentAlignment.MiddleCenter;
            this.lblMotive.Text = "Kolory motywu";
            this.lblMotive.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            this.lblMotive.BorderStyle = BorderStyle.FixedSingle;

            this.lblStandards.Size = new Size(182, 15);
            this.lblStandards.Location = new Point(-1, 129);
            this.lblStandards.ForeColor = Color.Navy;
            this.lblStandards.BackColor = Color.FromArgb(192, 255, 255);
            this.lblStandards.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStandards.Text = "Kolory standardowe";
            this.lblStandards.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            this.lblStandards.BorderStyle = BorderStyle.FixedSingle;

            this.lblMoreColors.Size = new Size(182, 20);
            this.lblMoreColors.Location = new Point(-1, 163);
            this.lblMoreColors.ForeColor = Color.FromArgb(0, 0, 64);
            this.lblMoreColors.TextAlign = ContentAlignment.MiddleCenter;
            this.lblMoreColors.Text = "Więcej kolorów";
            this.lblMoreColors.BackColor = SystemColors.ButtonHighlight;
            this.lblMoreColors.BorderStyle = BorderStyle.FixedSingle;
            this.lblMoreColors.Image = Properties.Resources.palette__2_;
            this.lblMoreColors.ImageAlign = ContentAlignment.MiddleLeft;

            this.lbldivider1.Size = new Size(2, 22);
            this.lbldivider1.Location = new Point(26, 0);
            this.lbldivider1.ForeColor = SystemColors.ControlText;
            this.lbldivider1.BackColor = SystemColors.ButtonHighlight;
            this.lbldivider1.BorderStyle = BorderStyle.Fixed3D;
            this.lbldivider1.BringToFront();

            this.lbldivider2.Size = new Size(2, 22);
            this.lbldivider2.Location = new Point(24, 164);
            this.lbldivider2.ForeColor = SystemColors.ControlText;
            this.lbldivider2.BackColor = SystemColors.ButtonHighlight;
            this.lbldivider2.BorderStyle = BorderStyle.Fixed3D;
            this.lbldivider2.BringToFront();
            #endregion

            //obsługa palety kolorów
            this.mainColorBtn.Click += new EventHandler(this.paletteBtnClicked);
            this.mainColorBtn.FlatAppearance.CheckedBackColor = this.mainColorBtn.BackColor;
            this.mainColorBtn.FlatAppearance.MouseDownBackColor = this.mainColorBtn.BackColor;
            this.mainColorBtn.FlatAppearance.MouseOverBackColor = this.mainColorBtn.BackColor;
            foreach (CustomButton cb in custBtnArray)
            {
                cb.Click += new EventHandler(this.paletteBtnClicked);
                cb.FlatAppearance.CheckedBackColor = cb.BackColor;
                cb.FlatAppearance.MouseDownBackColor = cb.BackColor;
                cb.FlatAppearance.MouseOverBackColor = cb.BackColor;
            }
            //konfiguracja panelu głównego
            #region Panel Główny
            this.Size = new Size(182, 184);
            this.BackColor = SystemColors.ButtonHighlight;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.Controls.AddRange(new Control[] { this.lblAutomatic, this.lblMotive,this.lblStandards,
            this.lblMoreColors,this.lbldivider1,this.lbldivider2,this.mainColorBtn});
            this.Controls.AddRange(this.custBtnArray);

            this.lbldivider1.BringToFront();
            this.lbldivider2.BringToFront();
            this.mainColorBtn.BringToFront();
            #endregion
        }

        //obsługa zdarzeń
        //
        //paleta przycisków
        private void paletteBtnClicked(object sender, EventArgs e)
        {
            CustomButton cb = sender as CustomButton;
            if (sender != null)
            {
                this.helperCustomBtn.Checked = false;
                this.helperCustomBtn.ShowBorder = false;
                this.helperCustomBtn.Invalidate();
                this.helperCustomBtn = cb;
                this.helperCustomBtn.Checked = true;
                this.Visible = false;
                this.RecentColor = cb.BackColor;
                this.ColorChanged();
            }
        }

        //paleta standardowa ("Więcej kolorów")
        private void LblMoreColors_Click(object sender, EventArgs e)
        {
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.helperCustomBtn.Checked = false;
                this.helperCustomBtn.ShowBorder = false;
                this.helperCustomBtn.Invalidate();
                this.Visible = false;
                this.RecentColor = colorDialog.Color;
                this.ColorChanged();
            }
        }

        //zachowania etykiet
        #region lblMoreColors
        private void LblMoreColors_MouseLeave(object sender, EventArgs e)
        {
            this.lblMoreColors.BackColor = SystemColors.ButtonHighlight;
        }

        private void LblMoreColors_MouseEnter(object sender, EventArgs e)
        {
            this.lblMoreColors.BackColor = Color.Khaki;
        }
        #endregion
        #region lblAutomatic
        private void LblAutomatic_MouseLeave(object sender, EventArgs e)
        {
            this.lblAutomatic.BackColor = SystemColors.ButtonHighlight;
        }

        private void LblAutomatic_Click(object sender, EventArgs e)
        {
            this.mainColorBtn.PerformClick();
            this.mainColorBtn.Invalidate();
        }

        private void LblAutomatic_MouseEnter(object sender, EventArgs e)
        {
            this.lblAutomatic.BackColor = Color.Khaki;
        }
        #endregion
    }
}
