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
    public partial class Search : Form
    {
        public delegate void TextHandler(string text);
        public delegate void DoubleTextHandler(string text, string text2);
        public delegate void ButtonClickHandler();
        public event TextHandler findStarted; //do powiadomienia, że wpisano tekst do textBox'a
        public event TextHandler lastTextSaved; //do zapamiętania ostatnio wyszukiwanego tekstu
        public event DoubleTextHandler changeClicked; //do powiadomienia, że kliknięto przycisk "Znajdź" (+ przesył akt. szukanego txt)
        public event DoubleTextHandler changeAllTextClicked; //j.w
        public event ButtonClickHandler nextClicked; //do powiadomienia, że kliknięto "Następny..."
        public event ButtonClickHandler previousClicked; //do powiadomienia, że kliknięto "Poprzedni..."
        public event ButtonClickHandler firstClicked;
        public event ButtonClickHandler lastClicked;
        private bool isTextFound; //do sprawdzenia, czy znaleziono szukaną frazę
        public bool IsTextFound
        {
            get { return this.isTextFound; }
        }
        private bool btnMoreState; //do sprawdzenia stanu przycsiku "Więcej..." (rozwinięty/zwinięty)
        private string baseFindText; //zmienna pomocnicza przy "wygaszaniu" przycisków, gdy zmieni się wpisywany tekst, 
                                     //a użytkownik nie kliknie "Szukaj"-> wówczas reszta przycisków powinna być "Disabled"

        //konstruktor domyślny
        public Search()
        {
            InitializeComponent();
            this.CenterToParent();
            this.btn_More.Text = "Więcej... " + char.ConvertFromUtf32(9662);

            this.modalDialogTxtBox.TextChanged += new EventHandler(this.modalDialogTxtBox_TextChanged);
        }
        //koniec konstruktora domyślnego

        #region Przycisk "Znajdź"
        private void modalDialogbtnFindText_Click(object sender, EventArgs e)
        {
            this.findStarted(this.modalDialogTxtBox.Text); //wywołanie zdarzenia wprowadzenia tekstu do wyszukania
            if (this.isTextFound) //jeżeli znaleziono szukaną frazę
            {
                this.modalDialogBtnFindLast.Enabled = true;
                this.modalDialogBtnFindFirst.Enabled = true;
                this.baseFindText = this.modalDialogTxtBox.Text; //powiadom o aktualnym tekście odniesienia przy szukaniu
                this.lastTextSaved(this.modalDialogTxtBox.Text);
            }   
            else
            {
                this.modalDialogBtnFindLast.Enabled = false;
                this.modalDialogBtnFindFirst.Enabled = false;
            }
        }
        #endregion
        #region Przycisk "Następny.."
        private void modalDialogbtnFindTextNext_Click(object sender, EventArgs e)
        {
            this.nextClicked();
        }
        #endregion
        #region Przycisk "Poprzedni"
        private void modalDialogButtonFindTextPrevious_Click(object sender, EventArgs e)
        {
            this.previousClicked();
        }
        #endregion
        #region Przycisk "Pierwszy"
        private void modalDialogBtnFindFirst_Click(object sender, EventArgs e)
        {
            this.firstClicked();
        }
        #endregion
        #region Przycisk "Ostatni"
        private void modalDialgoBtnFindLast_Click(object sender, EventArgs e)
        {
            this.lastClicked();
        }
        #endregion
        #region Przycisk "Zamień.."
        private void modalDialogbtnChangeText_Click(object sender, EventArgs e)
        {
            this.changeClicked(this.modalDialogTxtBox.Text, this.modalDialogTxtBoxChange.Text);
        }
        #endregion
        #region Przycisk "Zamień wszystko..."
        private void modalDialogbtnChangeAllText_Click(object sender, EventArgs e)
        {
            this.changeAllTextClicked(this.modalDialogTxtBox.Text, this.modalDialogTxtBoxChange.Text);
        }
        #endregion
        #region Przycisk "Więcej>> / <<Mniej "
        private void btn_More_Click(object sender, EventArgs e)
        {
            this.btnMoreState = !this.btnMoreState; //z każdym kliknięciem zmienia się wygląd (rozwinięcie/kliknięcie)
            if (this.btnMoreState)
            {
                this.ClientSize = new Size(400, 215);
                this.btn_More.Text = "Mniej... " + char.ConvertFromUtf32(9652); //zamiana z tablicy kodów Unicode na strzałkę
            }                                                                    //http://unicode-table.com/en/
            else
            {
                this.ClientSize = new Size(400, 165);
                this.btn_More.Text = "Więcej... " + char.ConvertFromUtf32(9662); //j.w.
            }
        }
        #endregion

        #region Widoczność/aktywność przycisku "Poprzedni.."
        public void ChangeFindPreviousState(SearchVisibilityParameters state)
        {
            switch (state)
            {
                case SearchVisibilityParameters.PREVIOUS_NOT_VISIBLE:
                    this.modalDialogbtnFindTextPrevious.Visible = false;
                    break;
                case SearchVisibilityParameters.PREVIOUS_ENABLED_VISIBLE:
                    this.modalDialogbtnFindTextPrevious.Enabled = true;
                    this.modalDialogbtnFindTextPrevious.Visible = true;
                    break;
                case SearchVisibilityParameters.PREVOIUS_NOT_ENABLED:
                    this.modalDialogbtnFindTextPrevious.Enabled = false;
                    break;
                default:
                    MessageBox.Show("Błędny parametr wejściowy!", "Błąd!");
                    break;
            }
        }
        #endregion
        #region Widoczność/aktywność przycisku "Następny.."
        public void ChangeFindNextState(SearchVisibilityParameters state)
        {
            switch (state)
            {
                case SearchVisibilityParameters.NEXT_NOT_VISIBLE:
                    this.modalDialogbtnFindTextNext.Visible = false;
                    break;
                case SearchVisibilityParameters.NEXT_ENABLED_VISIBLE:
                    this.modalDialogbtnFindTextNext.Enabled = true;
                    this.modalDialogbtnFindTextNext.Visible = true;
                    break;
                case SearchVisibilityParameters.NEXT_NOT_ENABLED:
                    this.modalDialogbtnFindTextNext.Enabled = false;
                    break;
                default:
                    MessageBox.Show("Błędny parametr wejściowy!", "Błąd!");
                    break;
            }
        }
        #endregion
        #region Widoczność/aktywność przycisków "Pierwszy" i "Ostatni"
        public void ChangeFirstLastState(SearchVisibilityParameters state)
        {
            switch (state)
            {
                case SearchVisibilityParameters.FIRST_AND_LAST_NOT_VISIBLE:
                    this.modalDialogBtnFindFirst.Visible = false;
                    this.modalDialogBtnFindLast.Visible = false;
                    break;
                case SearchVisibilityParameters.FIRST_AND_LAST_ENABLED_VISIBLE:
                    this.modalDialogBtnFindFirst.Enabled = true;
                    this.modalDialogBtnFindFirst.Visible = true;
                    this.modalDialogBtnFindLast.Enabled = true;
                    this.modalDialogBtnFindLast.Visible = true;
                    break;
                case SearchVisibilityParameters.FIRST_AND_LAST_NOT_ENABLED:
                    this.modalDialogBtnFindFirst.Enabled = false;
                    this.modalDialogBtnFindLast.Enabled = false;
                    break;
                case SearchVisibilityParameters.FIRST_ENABLED_LAST_NOT_ENABLED:
                    this.modalDialogBtnFindLast.Enabled = false;
                    this.modalDialogBtnFindFirst.Enabled = true;
                    break;
                case SearchVisibilityParameters.FIRST_NOT_ENABLED_LAST_ENABLED:
                    this.modalDialogBtnFindFirst.Enabled = false;
                    this.modalDialogBtnFindLast.Enabled = true;
                    break;
                case SearchVisibilityParameters.FIRST_ENABLED:
                    this.modalDialogBtnFindFirst.Enabled = true;
                    break;
                case SearchVisibilityParameters.LAST_ENABLED:
                    this.modalDialogBtnFindLast.Enabled = true;
                    break;
                default:
                    MessageBox.Show("Błędny parametr wejściowy!", "Błąd!");
                    break;
            }
        }
        #endregion

        #region Sprawdzenie, czy tekst jest już zaznaczony (przed szukaniem)
        public void GetTextSelected(string selectedText)
        {
            this.modalDialogTxtBox.Text = selectedText;
        }
        #endregion 
        #region Sprawdzenie, czy tekst został znaleziony
        public bool FoundedText(bool state)
        {
            this.isTextFound = state;
            return this.isTextFound;
        }
        #endregion


       //ZDARZENIA
       //zmiana tekstu w oknie wpisywania szukanego tekstu
        private void modalDialogTxtBox_TextChanged(object sender, EventArgs e)
        {
            if(this.isTextFound)//jeżeli znaleziono poprawny tekst
            {
                if (this.baseFindText != this.modalDialogTxtBox.Text) //a tekst w textBox'ie zmienił się względem znalezionego
                {   //to wycisz przyciski (oprócz "Znajdź" i "Więcej..."
                    this.modalDialogBtnFindFirst.Enabled = false;
                    this.modalDialogBtnFindLast.Enabled = false;
                    this.modalDialogbtnFindTextNext.Enabled = false;
                    this.modalDialogbtnFindTextPrevious.Enabled = false;
                }
                else
                {   //w innym razie je uaktynij z powrotem
                    this.modalDialogBtnFindFirst.Enabled = true;
                    this.modalDialogBtnFindLast.Enabled = true;
                    this.modalDialogbtnFindTextNext.Enabled = true;
                    this.modalDialogbtnFindTextPrevious.Enabled = true;
                }
            }
            if (this.modalDialogTxtBox.Text != "")
            {
                this.modalDialogbtnChangeText.Enabled = true;
                this.modalDialogbtnChangeAllText.Enabled = true;
            }
        }
  
        #region Funkcja wymuszająca szukanie
        public void FindNow()
        {
            this.modalDialogbtnFindText.PerformClick();
        }
        #endregion


    }
}
