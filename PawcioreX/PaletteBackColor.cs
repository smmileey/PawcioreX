using System;
using System.Drawing;
using System.Windows.Forms;

namespace PawcioreX
{
    public partial class PaletteBackColor : Panel
    {
        private Label lblNoColor;
        private Color[] colorBackArray;
        private CustomButton mainBackColorBtn;
        private CustomButton[] custBtnArray;
        private CustomButton helperCustomBtn;

        public delegate void ColorHandler();
        public event ColorHandler ColorChanged;
        public Color RecentColor { get; private set; }

        public PaletteBackColor()
        {
            //inicjalizacja
            InitializeComponent();
            this.lblNoColor = new Label();
            this.colorBackArray = new Color[15];
            this.mainBackColorBtn = new CustomButton();
            this.custBtnArray = new CustomButton[15];
            this.helperCustomBtn = new CustomButton();

            //obsługa animiacji
            //
            //przycisk dolny (etykieta)
            this.lblNoColor.MouseEnter += LblNoColor_MouseEnter;
            this.lblNoColor.MouseLeave += LblNoColor_MouseLeave;
            this.lblNoColor.MouseClick += LblNoColor_MouseClick;
            //wizualizacja przycisków
            #region Dobranie kolorów
            //1 wiersz
            this.colorBackArray[0] = Color.FromArgb(255, 255, 0);
            this.colorBackArray[1] = Color.FromArgb(0, 255, 0);
            this.colorBackArray[2] = Color.FromArgb(0, 255, 255);
            this.colorBackArray[3] = Color.FromArgb(255, 0, 255);
            this.colorBackArray[4] = Color.FromArgb(0, 0, 255);
            //2 wiersz
            this.colorBackArray[9] = Color.FromArgb(102, 0, 102);
            this.colorBackArray[8] = Color.FromArgb(0, 102, 0);
            this.colorBackArray[7] = Color.FromArgb(0, 102, 102);
            this.colorBackArray[6] = Color.FromArgb(0, 0, 51);
            this.colorBackArray[5] = Color.FromArgb(255, 0, 0);
            //3 wiersz
            this.colorBackArray[14] = Color.FromArgb(0, 0, 0);
            this.colorBackArray[13] = Color.FromArgb(160, 160, 160);
            this.colorBackArray[12] = Color.FromArgb(96, 96, 96);
            this.colorBackArray[11] = Color.FromArgb(102, 102, 0);
            this.colorBackArray[10] = Color.FromArgb(102, 0, 0);
            #endregion
            #region Ustawienie guzików
            //główny
            this.mainBackColorBtn.Size = new Size(27, 28);
            this.mainBackColorBtn.Location = new Point(10, 121);
            this.mainBackColorBtn.ForeColor = SystemColors.ControlText;
            this.mainBackColorBtn.BackColor = Color.White;
            this.mainBackColorBtn.FlatStyle = FlatStyle.Flat;
            this.mainBackColorBtn.UseVisualStyleBackColor = false;
            this.mainBackColorBtn.IsBordered = true;
            ////paleta
            int n = 10, k = 10, g = 4;
            for (int i = 0; i < this.custBtnArray.Length; i++)
            {
                this.custBtnArray[i] = (CustomButton)Activator.CreateInstance(typeof(CustomButton));
                this.custBtnArray[i].Size = new Size(27, 28);
                this.custBtnArray[i].ForeColor = SystemColors.ControlText;
                this.custBtnArray[i].BackColor = this.colorBackArray[i];
                this.custBtnArray[i].FlatStyle = FlatStyle.Flat;
                this.custBtnArray[i].UseVisualStyleBackColor = false;

                if (i < g)
                {
                    this.custBtnArray[i].Location = new Point(n, k);
                    n += 33;
                }
                else
                {
                    this.custBtnArray[i].Location = new Point(n, k);
                    n = 10;
                    k += 34;
                    g += 5;
                }
            }
            #endregion
            #region Guzik pomocniczy
            this.helperCustomBtn = this.mainBackColorBtn; //domyślnie przypisany jest color "Black"
            this.helperCustomBtn.Checked = true;
            this.helperCustomBtn.ShowBorder = true;
            #endregion

            //konfiguracja etykiet
            this.lblNoColor.Size = new Size(186, 37);
            this.lblNoColor.Location = new Point(-1, 116);
            this.lblNoColor.ForeColor = SystemColors.ControlText;
            this.lblNoColor.BackColor = SystemColors.ButtonHighlight;
            this.lblNoColor.Text = "Brak koloru...";
            this.lblNoColor.TextAlign = ContentAlignment.MiddleCenter;
            this.lblNoColor.Font = new Font("Microsoft Sans Serif", 8);
            this.lblNoColor.BorderStyle = BorderStyle.FixedSingle;

            //obsługa palety kolorów
            this.mainBackColorBtn.Click += new EventHandler(this.paletteBtnClicked);
            this.mainBackColorBtn.FlatAppearance.CheckedBackColor = this.mainBackColorBtn.BackColor;
            this.mainBackColorBtn.FlatAppearance.MouseDownBackColor = this.mainBackColorBtn.BackColor;
            this.mainBackColorBtn.FlatAppearance.MouseOverBackColor = this.mainBackColorBtn.BackColor;
            foreach (CustomButton cb in custBtnArray)
            {
                cb.Click += new EventHandler(this.paletteBtnClicked);
                cb.FlatAppearance.CheckedBackColor = cb.BackColor;
                cb.FlatAppearance.MouseDownBackColor = cb.BackColor;
                cb.FlatAppearance.MouseOverBackColor = cb.BackColor;
            }

            //konfiguracja Panelu Głównego
            this.Size = new Size(185, 154);
            this.BackColor = SystemColors.ButtonHighlight;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.Controls.AddRange(new Control[] { this.lblNoColor, this.mainBackColorBtn });
            this.Controls.AddRange(this.custBtnArray);

            this.mainBackColorBtn.BringToFront();
        }

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

        private void LblNoColor_MouseClick(object sender, MouseEventArgs e)
        {
            this.mainBackColorBtn.PerformClick();
            this.mainBackColorBtn.Invalidate();
        }

        private void LblNoColor_MouseLeave(object sender, EventArgs e)
        {
            this.lblNoColor.BackColor = SystemColors.ButtonHighlight;
        }

        private void LblNoColor_MouseEnter(object sender, EventArgs e)
        {
            this.lblNoColor.BackColor = Color.Khaki;
        }
    }
}
