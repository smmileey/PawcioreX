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
using System.Drawing.Printing;
using System.Collections.Specialized;
using System.Media;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace PawcioreX
{
    public partial class mainForm : Form
    {
        private bool canNewFile; //pomocnicza do określenia, czy program może bezpiecznie utworzyć nowy dokument
        private PrintDocument docToPrint; //obiekt wysyłający dane do drukowania
        private Search myFindTextDialog; //okno dialogowe do wyszukiwania tekstu
        private int indexText; //miejsce, w którym szukany będzie tekst (Ctrl + F)
        private string textToFind; //tekst, który będzie szukany
        private bool isFindAlreadyOpen; //służy do sprawdzenia, czy okno szukania wyrazów jest już włączone
        private int lastCorrectIndexText; //zmienna pomocnicza przy przejsciach Następny/Poprzedni w opcji szukania
        private ListDictionary listOfSpecialTextElements; //zmienna pomocnicza przy szykaniu wartości "Min" i "Max" 
        private string baseText; //tekst odniesienia do ewentualnego zapisu
        private int DocumentNumber = 1; //zmienna pomocnicza do zapisu nazwy pliku
        private string documentTitle; //zmienna pomocnicza do zapisu nazwy pliku
        private string documentExt; //zmienna pomocnicza do zapisu nazwy pliku
        private string currentOpenDocument; //zmienna pomocnicza do zapamiętania ostatnio otwieranego dokumentu
        private string currentPathToOpenDocument; //zmienna pomocnicza do zapamiętania ścieżki do ostatnio otwieranego dokumentu
        private FontStyle currStyle; //pomocnicza określająca styl czcionki (pogrubienie, pochylenie, itp.)
        private bool isFontStyleChanged; //pomocnicza do określenia, czy zmieniony został styl czcionki (wówczas zapisujemy plik)
        private Font currFont; //pomocnicza określająca czcionkę
        private string lastSearchedText; //pomocnicza do zachowania ostatniego poprawnie szukanego tekstu
        private bool isForcedToFind; //pomocnicza, aby zablokować pewne zachowania wyszukiwania przy funkcji zamiany słów
        private FontFamily currFontFamily; //pomocnicza do oznaczenia aktualnej rodziny czcionek (do aktualizacji stylu i rozmiaru)
        private FontFamily startFontFamily; //pomocnicza do oznaczenia domyślnej rodziny czcionek (przy resetowaniu)
        private float startSize; //domyślny rozmiar czcionki
        private float baseSize; //aktualny rozmiar czcionki w danym miejscu kursora (bądź zaznaczenia)
        private float charOffsetDivider; //zmienna pomocnicza przy definiowaniu SelectionCharOffset (patrz: indeks górny/dolny)
        private int ClosingCount; //pomocnicza przy obsłudze zamykania palety kolorów, gdy kliknięty jest ts, na którym się znajduje
        private int ClosingCount1; //j.w dla palety kolorów t
        private ColorDialog colorDialog; //modalne okno dialogowe z zaawansowaną paletą kolorów
        private bool isFromDropDownClosing; //pomocnicza do określenia czy wybór rozmiaru czcionki został dokonany przez ComboBox
        private InstalledFontCollection systemFonts; //kolekcja czcionek zainstalowanych w systemie
        private object[] systemFontsNames; //tablica obiektów zawierających FontFamily.Name dla powyższej kolekcji
        private bool isFromDropDownClosing2; //pomocnicza do określenia czy wybór czcionki został dokonany przez ComboBox
        private string startFontComboBoxText; //pomocnicza, wyjściowa nazwa czcionki przy uruchomieniu aplikacji
        private string startFontSizeComboBoxText; //pomocnicza, wyjściowy rozmiar czcionki przy uruchomieniu aplikacji
        private bool isEscClicked; //identyfikacja kliknięcia Escape w ComboBox dla wyboru czcionki
        private bool isBackClicked; //identyfikacja kliknięcia backspace'a w ComboBox dla wyboru czcionki
        private string lastWrittenInFontChoice; //pomocnicza, ostatnio wpisywana do ComboBox'a nazwa czcionki
        private string lastWrittenInFontSizeChoice; //pomocnicza, ostatnio wpisywany do ComboBox'a rozmiar czcionki
        private ClipboardListener clipboardListener;
        private int firstCharOnPage;
        private int lastCharOnPage;
        private int currentPrintPage;
        private int startPage;
        private int lastPage;

        private ToolStripButton AlignmentTempButton;
        private DateTimeMod dtMod;
     
        public mainForm()
        {
            InitializeComponent();
            CenterToScreen();

            this.WindowState = FormWindowState.Normal;
            this.docToPrint = new PrintDocument();
            this.BackColor = Color.BurlyWood;
            this.KeyPreview = true;
            this.charOffsetDivider = 2.1000F;
            this.colorDialog = new ColorDialog();
            this.systemFonts = new InstalledFontCollection();
            this.systemFontsNames=new object[this.systemFonts.Families.Length];


            this.startSize = 11f; //ustawienie domyślnego rozmiaru czcionki (przy resetowaniu)
            this.startFontFamily = new FontFamily("Consolas");
            this.currFontFamily = this.startFontFamily;
            this.baseSize = this.startSize;
            float.TryParse(this.tsComboBoxFontSizeChoice.Text, out this.baseSize);
            this.currStyle = this.richTextBox.SelectionFont.Style;
            this.currFont = new Font(this.currFontFamily,this.baseSize, this.currStyle);
            this.richTextBox.SelectionFont = this.currFont;
           
            //ZDARZENIA
            this.KeyDown += new KeyEventHandler(this.OnKeyDown);
            this.Click += new EventHandler(this.mainForm_Clicked);
            this.docToPrint.PrintPage += new PrintPageEventHandler(this.docToPrintCustom);
            this.docToPrint.BeginPrint += new PrintEventHandler(this.docToPrintBeforeFirstPage);
            this.docToPrint.EndPrint += new PrintEventHandler(this.docToPrintAfterLastPage);
            this.FormClosing += new FormClosingEventHandler(this.mainForm_FormClosing);

            this.richTextBox.MouseUp += new MouseEventHandler(this.richTextBox_MouseUp);
            this.richTextBox.KeyUp += new KeyEventHandler(this.richTextBox_KeyUp);
            this.richTextBox.PreviewKeyDown += new PreviewKeyDownEventHandler(this.richTextBox_PreviewKeyDown);
            this.richTextBox.KeyDown += new KeyEventHandler(this.richTextBox_KeyDown);
            this.richTextBox.MouseMove += new MouseEventHandler(this.MouseMove_RTB);


            this.toolStripMain.Click += new EventHandler(this.toolStripMain_Click);
            this.toolStripFont.Click += new EventHandler(this.toolStripFont_Click);
            this.menuStripFile.Click += new EventHandler(this.menuStripFile_Click);
            this.paletteFontColor.Leave += new EventHandler(this.paletteFontColor_Leave);
            this.paletteBackColor.Leave += new EventHandler(this.paletteBackColor_Leave);
            this.paletteFontColor.ColorChanged += paletteFontColor_ColorChanged;
            this.paletteBackColor.ColorChanged += PaletteBackColor_ColorChanged;

            //WIZUALIZACJA
            this.tsBtnSaveFile.Image = Properties.Resources.Save_6530;
            this.tsBtnOpenFile.Image = Properties.Resources.folder_Open_32xMD;
            this.tsBtnNewFile.Image = Properties.Resources.NewFile_6276;
            this.tsBtnPrint.Image = Properties.Resources.PrintDocumentControl_697;
            this.tsUndo.Image = Properties.Resources.Arrow_UndoRevertRestore_16xLG_color;
            this.tsRedo.Image = Properties.Resources.Redo_16x;
            this.tsBtnBold.Image = Properties.Resources.format_Bold_16xMD;
            this.tsBtnItalic.Image = Properties.Resources.format_Italics_16xMD;
            this.tsBtnUnderline.Image = Properties.Resources.format_Underline_16xMD;
            this.tsBtnStrikeout.Image = Properties.Resources.strikeout_16;
            this.tsBtnSubscript.Image = Properties.Resources.subscript_16;
            this.tsBtnSuperscript.Image = Properties.Resources.superscript;
            this.tsBtnFontDecrease.Image = Properties.Resources.font_decrease;
            this.tsBtnFontIncrease.Image = Properties.Resources.font_increase;
            this.tsBtnFontColor.Image = Properties.Resources.FontColor_11146_24;
            this.tsBtnFontBackColor.Image = Properties.Resources.BackgroundColor_326_32;
            this.pn1_BtnCut.Image = Properties.Resources.Cut_6523;
            this.pn1_BtnKopiuj.Image = Properties.Resources.Copy_6524;
            this.pn1_BtnPaste.Image = Properties.Resources.Paste_6520;
            this.tsBtnLeftAllignement.Image = Properties.Resources.text_align_left;
            this.tsBtnCenterAllignment.Image = Properties.Resources.text_center_alignment;
            this.tsBtnRightAlignment.Image = Properties.Resources.text_right_alignment;
            this.BtnImage.Image = Properties.Resources.image24x24;
            this.date_time.Image = Properties.Resources.date_and_time;

            //INNE
            this.paletteFontColor.Location = new Point(243, 76);
            this.paletteBackColor.Location = new Point(274, 76);

            #region Czcionki systemowe
            //pobranie czcionek zainstalowanych w systemie
            for (int i = 0; i < this.systemFonts.Families.Length; i++)
            {
                this.systemFontsNames[i] = this.systemFonts.Families[i].Name;
            }
            //dodanie ich do ComboBox'a
            this.tsComboBoxFontChoice.Items.AddRange(this.systemFontsNames);
            #endregion

        }

        //OBSŁUGA ZDARZEŃ / METODY WEWNĘTRZNE
        #region Obsługa "drobnych zdarzeń"
        //zdarzenie "Leave" palety z kolorami (czcionka)
        private void paletteFontColor_Leave(object sender, EventArgs e)
        {
            this.paletteFontColor.Visible = false;
        }

        //zdarzenie "Leave" palety z kolorami (tło)
        private void paletteBackColor_Leave(object sender, EventArgs e)
        {
            this.paletteBackColor.Visible = false;
        }

        //zdarzenie "Click" dla menu głównego
        private void menuStripFile_Click(object sender, EventArgs e)
        {
            this.paletteFontColor.Visible = false;
            this.paletteBackColor.Visible = false;
        }

        //"Click" paska narzędziowego z obróbką czcionki
        private void toolStripFont_Click(object sender, EventArgs e)
        {
            if (this.ClosingCount != 0)
                this.ClosingCount++;
            if (this.ClosingCount == 3)
                this.paletteFontColor.Visible = false;

            if (this.ClosingCount1 != 0)
                this.ClosingCount1++;
            if (this.ClosingCount1 == 3)
                this.paletteBackColor.Visible = false;
        }

        //"Click" głównego paska narzędziowego
        private void toolStripMain_Click(object sender, EventArgs e)
        {
            this.paletteFontColor.Visible = false;
            this.paletteBackColor.Visible = false;
        }

        //"Click" głównej kontrolki
        private void mainForm_Clicked(object sender, EventArgs e)
        {
            this.paletteFontColor.Visible = false;
            this.paletteBackColor.Visible = false;
        }
        //"Enter" dla ComboBox'a z wyborem nazwy czcionki
        private void tsComboBoxFontChoice_Enter(object sender, EventArgs e)
        {
            this.startFontComboBoxText = this.tsComboBoxFontChoice.Text;
        }

        //"Enter" dla ComboBox'a z wyborem rozmiaru czcionki
        private void tsComboBoxFontSizeChoice_Enter(object sender, EventArgs e)
        {
            this.startFontSizeComboBoxText = this.tsComboBoxFontSizeChoice.Text;
        }
        //"Click" dla ComboBox'a z wyborem czcionki
        private void tsComboBoxFontChoice_Click(object sender, EventArgs e)
        {
            this.lastWrittenInFontChoice = this.tsComboBoxFontChoice.Text;
        }
        //"Click" dla ComboBox'a z wyborem rozmiaru czcionki
        private void tsComboBoxFontSizeChoice_Click(object sender, EventArgs e)
        {
            this.lastWrittenInFontSizeChoice = this.tsComboBoxFontSizeChoice.Text;
        }      

        #endregion

        #region Załadowanie aplikacji
        private void mainForm_Load(object sender, EventArgs e)
        {
            //stan schowka przy załadowaniu aplikacji
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text) || iData.GetDataPresent(DataFormats.Rtf) ||
                iData.GetDataPresent(DataFormats.Bitmap))
            {
                this.pn1_BtnPaste.Enabled = true;
                this.tsMenuEditPaste.Enabled = true;
            }
            else
            {
                this.pn1_BtnPaste.Enabled = false;
                this.tsMenuEditPaste.Enabled = false;
            }

            this.clipboardListener = new ClipboardListener();
            clipboardListener.StartClipboardListener(this.Handle); //włączamy nasłuchiwanie zmian w schowku
            this.Text = "Nowy dokument" + this.DocumentNumber + ".rtf - " + Application.ProductName;
            this.documentTitle = "Nowy dokument" + this.DocumentNumber + ".rtf";
            this.documentExt = ".rtf";
            this.baseText = this.richTextBox.Rtf;
            this.DocumentNumber++; //zwieksz numer numeracji nowych dokumentów
            this.tsMenuEditUndo.Enabled = false;
            this.tsMenuEditRedo.Enabled = false;
            this.AlignmentTempButton = this.tsBtnLeftAllignement;
            this.AlignmentTempButton.Checked = true;
        }
        #endregion
        #region Zamykanie aplikacji
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.GetSaveState()) //sprawdź czy zmienił się stan tekstu od momentu załadowania programu
            {                                               //bądź od momentu wczytania pliku
                DialogResult dr = MessageBox.Show(string.Format("Czy zapisać zmiany w pliku {0}?", this.documentTitle),
                        Application.ProductName, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    this.tsBtnSaveFile.PerformClick();
                    if (this.canNewFile)
                        e.Cancel = false; //jeśli wybrano plik, czyli zapisano, to zamknij aplikację
                    else
                        e.Cancel = true; //jeśli anulowano zapis pliku, anuluj zamykanie aplikacji
                }
                else if (dr == DialogResult.No)
                    e.Cancel = false; //zamknij bez zapisywania
                else
                    e.Cancel = true; //anuluj zamykanie aplikacji    

                this.clipboardListener.RemoveClipboardListener(this.Handle); //usuwamy nasłuchiwanie zmian w schowku
            }
        }
        #endregion

        #region Przygotowanie tekstu do druku/ drukowanie /zakończenie drukowania
        private void docToPrintBeforeFirstPage(object sender, PrintEventArgs e)
        {
            firstCharOnPage = 0;
            lastCharOnPage = this.richTextBox.TextLength;
        }

        private void docToPrintAfterLastPage(object sender, PrintEventArgs e)
        {
            this.richTextBox.FormatRangeDone();
        }

        private void docToPrintCustom(object sender, PrintPageEventArgs e)
        {
                if (e.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages)
                {
                    if (currentPrintPage == startPage && currentPrintPage <= lastPage)
                    {
                        currentPrintPage++;
                        firstCharOnPage = this.richTextBox.FormatRange(false, e, firstCharOnPage,
                            lastCharOnPage);

                        if (currentPrintPage <= lastPage && firstCharOnPage < lastCharOnPage)
                            e.HasMorePages = true;
                        else
                            e.HasMorePages = false;

                    }
                    else
                    {
                        while (this.currentPrintPage < startPage)
                        {
                            firstCharOnPage = this.richTextBox.FormatRange(true, e, firstCharOnPage,
                                lastCharOnPage);
                            this.currentPrintPage += 1;
                        }

                        if (this.currentPrintPage <= lastPage)
                        {
                            this.currentPrintPage++;
                            firstCharOnPage = this.richTextBox.FormatRange(false, e, firstCharOnPage,
                                lastCharOnPage);

                            if (currentPrintPage <= lastPage && firstCharOnPage < lastCharOnPage)
                                e.HasMorePages = true;
                            else
                                e.HasMorePages = false;
                        }
                        else
                            e.HasMorePages = false;
                    }
                }
                else
                {
                    firstCharOnPage = this.richTextBox.FormatRange(false, e, firstCharOnPage,
                            lastCharOnPage);

                if (firstCharOnPage < lastCharOnPage)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
                }
        }
        #endregion 
        #region Przycisk "Drukuj..."
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            //ustawienie właściwości okna dialogowego "Drukuj"
            PrintDialog myPrintDialog = new PrintDialog();
            myPrintDialog.AllowCurrentPage = true;
            myPrintDialog.AllowSomePages = true;
            myPrintDialog.Document = docToPrint;
            if (myPrintDialog.ShowDialog() == DialogResult.OK)
                {
                int copies = myPrintDialog.PrinterSettings.Copies;              
                    try
                    {
                        startPage = myPrintDialog.PrinterSettings.FromPage;
                        lastPage = myPrintDialog.PrinterSettings.ToPage;
                        for (int i = 0; i < copies; i++)
                        {
                            this.currentPrintPage = 1;
                            this.docToPrint.Print();
                        }
                    this.tsLabelState.Text =
                    string.Format("Drukowanie ukończone. Liczba plików: {0}", copies);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Niespodziewany błąd, drukowanie zostało przerwane.");
                    }              
            }
        }
        #endregion

        #region MainForm_OnKeyDown
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //OKNO WYSZUKIWANIA
            if (!isFindAlreadyOpen) //jeżeli okno wyszukiwania nie jest już otwarte
            {
                if (e.Control && e.KeyCode == Keys.F)
                {
                    this.isFindAlreadyOpen = true; //oznaczenie, że okno jest już otwarte
                    //zawsze tworzymy teraz nowy obiekt, po zamknięciu się niszczy, więc nie można w konstruktorze!!
                    this.myFindTextDialog = new Search();
                    this.listOfSpecialTextElements = new ListDictionary();

                    bool isFired = true; //zmienna zapobiegająca wielokrotnym "fire" zdarzeń, gdy okno jest już otwarte
                    //nasłuchujemy zdarzenia w tym miejscu, bo dopiero teraz następuje wystąpienie obiektu
                    if (isFired)
                    {
                        this.myFindTextDialog.findStarted += new Search.TextHandler(this.myFindTextDialog_FindText);
                        this.myFindTextDialog.nextClicked += new Search.ButtonClickHandler(this.myFindTextDialog_NextClicked);
                        this.myFindTextDialog.previousClicked += new Search.ButtonClickHandler(this.myFindTextDialog_PreviousClicked);
                        this.myFindTextDialog.FormClosed += new FormClosedEventHandler(this.myFindTextDialog_Closed);
                        this.myFindTextDialog.firstClicked += new Search.ButtonClickHandler(this.myFindTextDialog_firstClicked);
                        this.myFindTextDialog.lastClicked += new Search.ButtonClickHandler(this.myFindTextDialog_lastClicked);
                        this.myFindTextDialog.lastTextSaved += new Search.TextHandler(this.myFindTextDialog_lastText);
                        this.myFindTextDialog.changeClicked += new Search.DoubleTextHandler(this.myFindTextDialog_changeClicked);
                        this.myFindTextDialog.changeAllTextClicked += new Search.DoubleTextHandler(this.myFindTextDialog_changeAllTextClicked);
                    }
                    if (this.richTextBox.SelectionLength == 0)
                        this.myFindTextDialog.GetTextSelected(this.lastSearchedText);
                    else
                        this.myFindTextDialog.GetTextSelected(this.richTextBox.SelectedText);

                    this.myFindTextDialog.Location = new Point(580, 85);
                    this.myFindTextDialog.Show();
                    //blokujemy wyświetlenie przycisku "Następny..."
                    myFindTextDialog.ChangeFindNextState(0); //0 - niewidoczny
                }
            }
            else if(e.Control && e.KeyCode==Keys.F)
            {   // czyli, gdy ktoś kliknie Ctrl + F, a okno już otwarte, to tylko niech statnie się aktywne
                // (+ niech wyświetli ewentualnie zaznaczony tekst LUB wcześniej wprowadzony, gdy brak zaznaczenia)
                if (this.richTextBox.SelectionLength == 0)
                    this.myFindTextDialog.GetTextSelected(this.lastSearchedText);
                else
                {
                    this.myFindTextDialog.GetTextSelected(this.richTextBox.SelectedText);
                }
                this.myFindTextDialog.Focus();
            }

            //RESETOWANIE PASKA STANU
            this.tsLabelState.Text = "";

            //ZAMYKANIE OKNA Z WYBOREM KOLORÓW
            this.paletteFontColor.Visible = false;
            this.paletteBackColor.Visible = false;

        }


        #endregion

        #region Znalezienie tekstu (pierwszego) (Przycisk "Znajdź" - algorytm)
        private void myFindTextDialog_FindText(string textToFind)
        {
            this.myFindTextDialog.ChangeFindNextState(SearchVisibilityParameters.NEXT_NOT_VISIBLE); //na początku przycisk "Następny.." ma być niewidoczny!
            this.myFindTextDialog.ChangeFindPreviousState(SearchVisibilityParameters.PREVIOUS_NOT_VISIBLE); //przycisk "Poprzedni.." też (j.w)

            this.textToFind = textToFind;
            if (textToFind == "")
                MessageBox.Show("Nie wpisano tekstu!", "Błąd!");
            else
            {
                this.indexText = this.richTextBox.Text.IndexOf(this.textToFind);

                if (this.indexText != -1)
                {
                    //daj znać, że poprawnie znaleziono tekst
                    this.myFindTextDialog.FoundedText(true);

                    //ustaw przycisk "Następny.." na widoczny
                    myFindTextDialog.ChangeFindNextState(SearchVisibilityParameters.NEXT_ENABLED_VISIBLE); //1 - włączony całkiem (widoczny+aktywny)

                    //zapisz pierwszy wyraz w całym tekście w specjalnym typie ListDictionary
                    this.listOfSpecialTextElements["Min"] = this.indexText;
                    this.myFindTextDialog.ChangeFirstLastState(SearchVisibilityParameters.FIRST_AND_LAST_ENABLED_VISIBLE); //włącz całkiem przyciski "Pierwszy" i "Ostatni"

                    this.richTextBox.Select(this.indexText, this.textToFind.Length); //zaznacz tekst w richTextBox'ie
                    this.Focus(); //zabieg w celu natychmiastowej wizualizacji podświetlenia w połączeniu z HideSelection=true
                    this.myFindTextDialog.Focus(); //aby Focus cały czas pozostał na oknie wyszukiwania (obecnie bug!!)
                    this.lastSearchedText = textToFind;
                }
                else //gdy nie znaleziono tekstu
                {
                    this.myFindTextDialog.FoundedText(false); //daj znać, że nie znaleziono szukanej frazy
                    if(!isForcedToFind) //flaga pomocnicza blokująca komunikat przy algorytmie zamiany słów
                        MessageBox.Show("Wybrany element nie został odnaleziony!");
                }
            }
        }
        #endregion
        #region Znalezienie tekstu (drugi - ostatni) (Przycisk "Następny" - algorytm)
        private void myFindTextDialog_NextClicked()
        {
            this.indexText = this.richTextBox.Text.IndexOf(this.textToFind, this.indexText + this.textToFind.Length);
            if (this.indexText != -1) //znaleziono kolejny tekst
            {
                lastCorrectIndexText = this.indexText; //zmienna pomocnicza, aby składować ostatni indeks, który != -1
                this.richTextBox.Select(this.indexText, this.textToFind.Length);
                this.Focus();
                this.myFindTextDialog.Focus(); 
                this.myFindTextDialog.ChangeFindPreviousState(SearchVisibilityParameters.PREVIOUS_ENABLED_VISIBLE); //włącz całkiem przycisku "Poprzedni..."
                this.myFindTextDialog.ChangeFirstLastState(SearchVisibilityParameters.FIRST_ENABLED); //włącz przycisk "Pierwszy"
            }
            else //nie znaleziono więcej dopasowań
            {
               this.indexText = lastCorrectIndexText; //wykorzystujemy zmienną pomocniczą
               MessageBox.Show("Nie znaleziono późniejszego występienia szukanej frazy!","Koniec wyszukiwania!");
               this.myFindTextDialog.ChangeFindNextState(SearchVisibilityParameters.NEXT_NOT_ENABLED); // wyłącz przycisk "Następny..."
               this.myFindTextDialog.ChangeFindPreviousState(SearchVisibilityParameters.PREVIOUS_ENABLED_VISIBLE); // włącz widoczność przycisku "Poprzedni..."
               this.myFindTextDialog.ChangeFirstLastState(SearchVisibilityParameters.FIRST_ENABLED_LAST_NOT_ENABLED); //włącz przycisk "Pierwszy..." i wyłącz "Ostatni..."
            }
        }
        #endregion
        #region Znalezienie poprzedniego tekstu (Przycisk "Poprzedni" - algorytm)
        private void myFindTextDialog_PreviousClicked()
        {
            int maxIndexText = this.indexText; //maksimum ewentualnego następnego (po pierwszym udanym) szukania
            if (this.indexText != 0) //czyli gdy nie jesteśmy na pierwszym znaku
            {
                if (this.indexText > this.textToFind.Length) //bo musimy przeszukać conajmniej tyle znaków, ile ma szukana fraza!
                    this.indexText = this.richTextBox.Text.IndexOf(this.textToFind, 0, (this.indexText - 1));
                else
                    this.indexText = this.richTextBox.Text.IndexOf(this.textToFind, 0, this.textToFind.Length);
            }
            else //gdy jesteśmy na pierwszym znaku
                this.indexText = -1; // to na pewno nie ma już wcześniej szukanej frazy

            if (this.indexText == -1) //nie znaleziono wcześniejszej frazy
            {
                this.indexText = this.lastCorrectIndexText;
                MessageBox.Show("Nie znaleziono wcześniejszego wystąpienia szukanej frazy!", "Koniec wyszukiwania");
                this.myFindTextDialog.ChangeFindPreviousState(SearchVisibilityParameters.PREVOIUS_NOT_ENABLED); //wygaś przycisk "Poprzedni..."
                this.myFindTextDialog.ChangeFirstLastState(SearchVisibilityParameters.FIRST_NOT_ENABLED_LAST_ENABLED); //wygas przycisk "Pierwszy"
                this.myFindTextDialog.ChangeFindNextState(SearchVisibilityParameters.NEXT_ENABLED_VISIBLE); //włącz widoczność przycisku "Następny"
                this.Focus();
                this.myFindTextDialog.Focus();
            }
            else //znaleziono wcześniejszą frazę
            {
                do
                {
                    this.myFindTextDialog.ChangeFindNextState(SearchVisibilityParameters.NEXT_ENABLED_VISIBLE); //włącz widoczność przycisku "Następny"
                    this.myFindTextDialog.ChangeFirstLastState(SearchVisibilityParameters.LAST_ENABLED); //włącz widoczność przycisku "Ostatni"
                    this.lastCorrectIndexText = this.indexText;
                    this.indexText = this.richTextBox.Text.IndexOf(this.textToFind, (this.indexText + this.textToFind.Length), maxIndexText - (this.indexText + 1));
                } while (this.indexText != -1);
                this.indexText = this.lastCorrectIndexText;
                this.richTextBox.Select(this.indexText, this.textToFind.Length);
            }
        }
        #endregion
        #region Przycisk "Ostatni" - algorytm
        private void myFindTextDialog_lastClicked()
        {
            this.indexText = (int)this.listOfSpecialTextElements["Min"]; //na początku zakładamy, że max=min
            while (this.indexText != -1)
            {
                this.listOfSpecialTextElements["Max"] = this.indexText;
                if ((this.indexText + this.textToFind.Length) > this.richTextBox.Text.Length) //przerwij, bo to ostatnie słowo
                    break;
                else
                    this.indexText = this.richTextBox.Text.IndexOf(this.textToFind, this.indexText + this.textToFind.Length);
            }
            this.indexText = (int)this.listOfSpecialTextElements["Max"]; //ustawiamy poprawny aktualny indeks
            this.richTextBox.Select(this.indexText, this.textToFind.Length);

            this.myFindTextDialog.ChangeFindPreviousState(SearchVisibilityParameters.PREVIOUS_ENABLED_VISIBLE); //włącz widoczność przycisku "Poprzedni"
            this.myFindTextDialog.ChangeFindNextState(SearchVisibilityParameters.NEXT_NOT_ENABLED); //włącz widoczność przycisku "Następny"
            this.myFindTextDialog.ChangeFirstLastState(SearchVisibilityParameters.FIRST_ENABLED_LAST_NOT_ENABLED); //włącz widoczność "Pierwszy" i wyłącz "Ostatni"

            this.richTextBox.Focus();
            this.Focus();
        }
        #endregion
        #region Przycisk "Pierwszy" - algorytm
        private void myFindTextDialog_firstClicked()
        {
                this.indexText = (int)this.listOfSpecialTextElements["Min"];this.richTextBox.Select(this.indexText, this.textToFind.Length);
                this.myFindTextDialog.ChangeFirstLastState(SearchVisibilityParameters.FIRST_NOT_ENABLED_LAST_ENABLED); //włączamy widoczność przycisku "Ostatni" i wyłączamy "Poprzedni"
                this.myFindTextDialog.ChangeFindNextState(SearchVisibilityParameters.NEXT_ENABLED_VISIBLE); //włączamy widoczność przycisku "Następny"
                this.myFindTextDialog.ChangeFindPreviousState(SearchVisibilityParameters.PREVOIUS_NOT_ENABLED); //wyłączamy widoczność przycisku "Poprzedni"
                this.Focus();
                this.myFindTextDialog.Focus();

        }
        #endregion

        #region Zamiana pojedynczego tekstu
        private void myFindTextDialog_changeClicked(string textSearched, string textToChange)
        {
            //jeśli mamy już zaznaczony tekst i odpowiada on temu szukanemu
            if (this.richTextBox.SelectionLength != 0 && this.richTextBox.SelectedText == textSearched)
            {
                this.lastSearchedText = textSearched; //wiemy od razu, że tekst jest poprawnie znaleziony (bo zaznaczony)
                int tempStart = this.richTextBox.SelectionStart;
                //tutaj podmieniamy tylko tekst, a nie styl! więc nie dajemy "SelectedRtf"
                this.richTextBox.SelectedText = textToChange;

                //mogła się zmienić wartość "Min"(choć nie musiała), bo element został zamieniony, więc trzeba wymusić szukanie!
                this.isForcedToFind = true; //zmienna pomocnicza, anuluje wyświetlenie komunikatu po zamianie ostatniego tekstu (o tym, że nie ma już więcej wystąpień szukanej frazy)
                this.myFindTextDialog.FindNow();
                this.isForcedToFind = false;
                //dopiero teraz ustawiamy zaznaczenie
                this.richTextBox.Select(tempStart, textToChange.Length);
            }
            else //gdy nie mamy aktualnie zaznaczonego szukanego tekstu
            {
                this.myFindTextDialog.FindNow(); //wymuszamy szukanie
                if(this.myFindTextDialog.IsTextFound) //jeśli znaleziono szukaną frazę, postępuj jak powyżej
                {
                    //analogicznie j.w
                    this.lastSearchedText = textSearched;
                    int tempStart = this.richTextBox.SelectionStart;
                    this.richTextBox.SelectedText = textToChange;
                    this.isForcedToFind = true;
                    this.myFindTextDialog.FindNow();
                    this.isForcedToFind = false;
                    this.richTextBox.Select(tempStart, textToChange.Length);
                }
            }
        }
        #endregion
        #region Zamiana wszystkich wystąpień szukanego tekstu
        private void myFindTextDialog_changeAllTextClicked(string textToFind, string textToChange)
        {
            using (RichTextBox tempRichTextBox = new RichTextBox()) //sztuczka z tymczasowym RTB
            {
                tempRichTextBox.Rtf = this.richTextBox.Rtf;
                int foundTextCount = 0; //pomocnicza do policzenia znalezionych wyrazów
                int tempIndexOf = 0; //pomocniczy indeks w tymczasowym RTB
                tempIndexOf = tempRichTextBox.Text.IndexOf(textToFind);

                while(tempIndexOf != -1) //póki są kolejne frazy znajdowane
                {
                    foundTextCount++; 
                    tempRichTextBox.Select(tempIndexOf, textToFind.Length);
                    tempRichTextBox.SelectedText = textToChange;
                    tempIndexOf = tempRichTextBox.Text.IndexOf(textToFind,tempIndexOf+textToChange.Length); //uaktualnij indeks
                }
                if(foundTextCount != 0) //jeśli znaleziono choć jedną frazę
                {
                    this.richTextBox.Rtf = tempRichTextBox.Rtf; //zamień z powrotem RTF
                    MessageBox.Show(string.Format("Zakończono wyszukiwanie! Liczba zamienionych elementów: {0}", foundTextCount),
                        Application.ProductName);
                    this.lastSearchedText = textToFind; //zapamiętaj wyszukiwany tekst
                }
                else
                    MessageBox.Show(string.Format("Brak wyrazów do zamiany w szukanym tekście!", foundTextCount),
                        Application.ProductName);
            }
        }
        #endregion
        #region Zapamiętanie ostatnio szukanego tekstu
        private void myFindTextDialog_lastText(string textLastSearched)
        {
            this.lastSearchedText = textLastSearched;
        }
        #endregion       
        #region Reset eventów/blokada kolejnych okien wyszukiwania
        private void myFindTextDialog_Closed(object sender, FormClosedEventArgs e)
        {
            this.isFindAlreadyOpen = false; //powiadom, że okno wyszukiwania zostało zamknięte
        }
        #endregion

        #region Przycisk "Utwórz plik..."
        private void tsBtnNewFile_Click(object sender, EventArgs e)
        {
            if (this.GetSaveState()) //sprawdź stan tekstu od włączenia/załadowania pliku, jeśli się zmienił to:
            {
                DialogResult dr = MessageBox.Show(string.Format("Czy zapisać zmiany w pliku {0}?", this.documentTitle),
                           Application.ProductName, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    if ((this.documentTitle + this.documentExt) != this.currentOpenDocument)
                    {
                        this.tsBtnSaveFile.PerformClick(); //sztucznie włączamy okno zapisu pliku
                        if (this.canNewFile)
                        {
                            this.richTextBox.Clear();
                            this.Text = "Nowy dokument" + this.DocumentNumber + ".rtf - " + Application.ProductName;
                            this.documentTitle = "Nowy dokument" + this.DocumentNumber;
                            this.documentExt = ".rtf";
                            this.tsLabelState.Text = "Utworzono nowy dokument (" + this.Text + ")";
                            this.DocumentNumber++; //zwieksz numer numeracji nowych dokumentów
                            this.baseText = this.richTextBox.Rtf; //przypisz tekst odniesienia do ewentualnego zapisu
                            this.isFontStyleChanged = false;
                            this.ResetPageSettings();

                            //ustaw opcje dostępne dla RTF!
                            this.toolStripFont.Enabled = true;
                            this.BtnImage.Enabled = true;
                        }
                    }
                    else //gdy zapisujemy biężący otwarty dokument
                    {
                        string fileExt = this.documentExt;

                        if (fileExt == ".txt")
                        {
                            string plainText = this.richTextBox.Text;
                            StreamWriter textWriter = new StreamWriter(this.currentPathToOpenDocument);
                            textWriter.WriteLine(this.richTextBox.Text);
                            textWriter.Dispose();
                            this.richTextBox.Clear();
                            this.baseText = this.richTextBox.Text; //przypisz tekst odniesienia do ewentualnego zapisu
                            this.Text = "Nowy dokument" + this.DocumentNumber + ".txt - " + Application.ProductName;
                            this.tsLabelState.Text = "Utworzono nowy dokument (" + this.Text + ")";
                            this.documentTitle = "Nowy dokument" + this.DocumentNumber;
                            this.documentExt = ".txt";
                            this.DocumentNumber++; //zwieksz numer numeracji nowych dokumentów
                            //w tym miejscu ścieżka i nazwa ostatnio otwartego pliku się nie zmieniają!
                            this.isFontStyleChanged = false;
                            this.ResetPageSettings();

                            //wyłącz opcję dostępne tylko dla RTF!
                            this.toolStripFont.Enabled = false;
                            this.BtnImage.Enabled = false;
                        }
                        else if (fileExt == ".rtf")
                        {
                            this.richTextBox.SaveFile(this.currentPathToOpenDocument, RichTextBoxStreamType.RichText);
                            this.richTextBox.Clear();
                            this.baseText = this.richTextBox.Rtf; //przypisz tekst odniesienia do ewentualnego zapisu
                            this.Text = "Nowy dokument" + this.DocumentNumber + ".rtf - " + Application.ProductName;
                            this.tsLabelState.Text = "Utworzono nowy dokument (" + this.Text + ")";
                            this.documentTitle = "Nowy dokument" + this.DocumentNumber;
                            this.documentExt = ".rtf";
                            this.DocumentNumber++; //zwieksz numer numeracji nowych dokumentów
                            //w tym miejscu ścieżka i nazwa ostatnio otwartego pliku się nie zmieniają!
                            this.isFontStyleChanged = false;
                            this.ResetPageSettings();

                            //ustaw opcje dostępne tylko dla RTF!
                            this.toolStripFont.Enabled = true;
                            this.BtnImage.Enabled = true;
                        }
                    }
                }
                else if (dr == DialogResult.No)
                {
                    this.richTextBox.Clear();
                    this.Text = "Nowy dokument" + this.DocumentNumber + ".rtf - " + Application.ProductName;
                    this.tsLabelState.Text = "Utworzono nowy dokument (" + this.Text + ")";
                    this.documentTitle = "Nowy dokument" + this.DocumentNumber;
                    this.documentExt = ".rtf";
                    this.DocumentNumber++; //zwieksz numer numeracji nowych dokumentów
                    this.baseText = this.richTextBox.Rtf; //przypisz tekst odniesienia do ewentualnego zapisu
                    this.isFontStyleChanged = false;
                    this.ResetPageSettings();

                    //ustaw opcje dostępne tylko dla RTF!
                    this.toolStripFont.Enabled = true;
                    this.BtnImage.Enabled = true;
                }
            }
            else
            {
                this.richTextBox.Clear();
                this.Text = "Nowy dokument" + this.DocumentNumber + ".rtf - " + Application.ProductName;
                this.tsLabelState.Text = "Utworzono nowy dokument (" + this.Text + ")";
                this.documentTitle = "Nowy dokument" + this.DocumentNumber + this.documentExt;
                this.DocumentNumber++; //zwieksz numer numeracji nowych dokumentów
                this.baseText = this.richTextBox.Rtf; //przypisz tekst odniesienia do ewentualnego zapisu
                this.isFontStyleChanged = false;
                this.ResetPageSettings();

                //ustaw opcje dostępne tylko dla RTF!
                this.toolStripFont.Enabled = true;
                this.BtnImage.Enabled = true;
            }
        }
        #endregion
        #region Przycisk "Otwórz plik..."
        private void tsBtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog myOpenDialog = new OpenFileDialog();
            myOpenDialog.Filter = "RichTextFile(*.rtf)|*.rtf|TextFile(*.txt)|*.txt|AllFiles(*.*)|*.*";
            myOpenDialog.FilterIndex = 1;
            myOpenDialog.FileName = this.documentTitle;
            bool isCorrectFile=false; //lokalna zmienna pomocnicza do identyfikacji poprawności wyboru pliku
            do
            {
                if (this.GetSaveState())
                { 
                        DialogResult dr = MessageBox.Show(string.Format("Czy zapisać zmiany w pliku {0}?", this.documentTitle),
                            Application.ProductName, MessageBoxButtons.YesNoCancel);
                        if (dr == DialogResult.Yes)
                        {
                            //jeżeli aktualny dokument nie jest ostatnio otwartym, postępuj standardowo
                            if ((this.documentTitle + this.documentExt) != this.currentOpenDocument)
                            {
                                this.tsBtnSaveFile.PerformClick();
                                if (!this.canNewFile) //jeśli nie wybrano pliku, opuść całą pętlę!
                                    break;
                            }
                            else //zapisz od razu, bo aktualny dokument jest tym ostatnio otwieranym
                            {
                                //wyciągamy rozszerzenie wskazanego pliku
                                string fileExt = this.documentExt;

                                if (fileExt == ".txt")
                                {
                                    //działam w ten sposób, ponieważ metoda SaveFile() dla RichTextBox.PlainText
                                    //zapisuje tak, że przy odczycie nie ma polskich znaków
                                    string plainText = this.richTextBox.Text;
                                    StreamWriter textWriter = new StreamWriter(this.currentPathToOpenDocument);
                                    textWriter.WriteLine(this.richTextBox.Text);
                                    textWriter.Dispose();
                                    //w tym miejscu ścieżka i nazwa ostatnio otwartego pliku się nie zmieniają!
                                    this.isFontStyleChanged = false;
                                    this.BtnImage.Enabled = false;
                                }
                                else if (fileExt == ".rtf")
                                {
                                    this.richTextBox.SaveFile(this.currentPathToOpenDocument, RichTextBoxStreamType.RichText);
                                    //w tym miejscu ścieżka i nazwa ostatnio otwartego pliku się nie zmieniają!
                                    this.isFontStyleChanged = false;
                                    this.BtnImage.Enabled = true;
                            }
                                this.Text = Path.GetFileName(this.currentPathToOpenDocument) + " - " + Application.ProductName;
                                this.tsLabelState.Text = "Zapisano zmiany w pliku " + this.Text;
                            }
                        }
                        else if (dr == DialogResult.Cancel)
                        {
                            this.tsLabelState.Text = "Anulowanie otwieranie pliku ";
                            break; //wychodzi na koniec zewnętrznej pętli (czyli pętli "do")  
                        }
                    //w pozostałym przypadku nie rób nic nowego, czyli otwórz okno dialogowe
                }
                try
                {
                    if (myOpenDialog.ShowDialog() == DialogResult.OK)
                    {
                        Stream myStream;
                        if ((myStream = myOpenDialog.OpenFile()) != null)
                        {
                            string fileExt = Path.GetExtension(myOpenDialog.FileName); //pobierz rozszerzenie
                            if (fileExt == ".txt")
                            {
                                StreamReader textReader = new StreamReader(myStream);
                                string text = textReader.ReadToEnd().TrimEnd();
                                this.richTextBox.Text = text;
                                textReader.Dispose();
                                myStream.Dispose();
                                isCorrectFile = true;

                                //zapisz tekst wczytanego pliku, jako odniesienie do zapytania o zapis
                                this.baseText = this.richTextBox.Text;
                                this.documentTitle = Path.GetFileNameWithoutExtension(myOpenDialog.FileName);
                                this.documentExt = Path.GetExtension(myOpenDialog.FileName);
                                this.currentOpenDocument = Path.GetFileName(myOpenDialog.FileName); //ostatnio zapisany dokument
                                this.currentPathToOpenDocument = myOpenDialog.FileName; //ścieżka ostatnio zapisanego dokumentu 
                                this.isFontStyleChanged = false; //zresetuj styl czionki (odniesienie)
                                this.richTextBox.SelectionStart = this.richTextBox.Text.Length; //przenieś kursor na koniec tekstu
                                this.Text = Path.GetFileName(myOpenDialog.FileName) + " - " + Application.ProductName;
                                this.tsLabelState.Text = "Otwarto plik: " + this.Text;
                                this.tsRedo.Enabled = false;
                                this.tsUndo.Enabled = false;
                                this.tsMenuEditUndo.Enabled = false;
                                this.tsMenuEditRedo.Enabled = false;

                                //dezaktywuj funkcje zarezerwowane tylko dla RTF!
                                this.toolStripFont.Enabled = false;
                                this.BtnImage.Enabled = false;
                            }
                            else if (fileExt == ".rtf")
                            {
                                this.richTextBox.LoadFile(myOpenDialog.FileName);
                                myStream.Dispose();
                                isCorrectFile = true;
                                this.baseText = this.richTextBox.Rtf;
                                this.documentTitle = Path.GetFileNameWithoutExtension(myOpenDialog.FileName);
                                this.documentExt = Path.GetExtension(myOpenDialog.FileName);
                                this.currentOpenDocument = Path.GetFileName(myOpenDialog.FileName); //ostatnio zapisany dokument
                                this.currentPathToOpenDocument = myOpenDialog.FileName; //ścieżka ostatnio zapisanego dokumentu 
                                this.isFontStyleChanged = false; //zresetuj styl czionki (odniesienie)
                                this.richTextBox.SelectionStart = this.richTextBox.Text.Length; //przenieś kursor na koniec tekstu
                                this.Text = Path.GetFileName(myOpenDialog.FileName) + " - " + Application.ProductName;
                                this.tsLabelState.Text = "Otwarto plik: " + this.Text;
                                this.tsRedo.Enabled = false;
                                this.tsUndo.Enabled = false;
                                this.tsMenuEditUndo.Enabled = false;
                                this.tsMenuEditRedo.Enabled = false;

                                //aktywuj funkcje zarezerwowane tylko dla RTF!
                                this.toolStripFont.Enabled = true;
                                this.BtnImage.Enabled = true;
                            }
                            else //wybrano plik z innym rozszerzeniem
                            {
                                MessageBox.Show("Błędny plik!");
                            }
                        }
                    }
                    else
                    {
                        this.tsLabelState.Text = "Anulowano otwieranie pliku.";
                        isCorrectFile = true; //zakończ pętle, zrezygnowano z wyboru dokumentu
                    }
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("Plik jest używany przez inny proces!");
                    this.Close();
                    break;
                }
            } while (isCorrectFile == false);       
        }
        #endregion
        #region Przycisk "Zapisz plik..."
        private void tsBtnSaveFile_Click(object sender, EventArgs e)
        {
            if ((this.documentTitle + this.documentExt) != this.currentOpenDocument)
            {
                SaveFileDialog mySaveDialog = new SaveFileDialog();
                mySaveDialog.Filter = "RichTextFile(*.rtf)|*.rtf|TextFile(*.txt)|*.txt";
                mySaveDialog.FilterIndex = 1;
                mySaveDialog.RestoreDirectory = false;
                mySaveDialog.FileName = this.documentTitle;
                DialogResult dr;

                if ((dr = mySaveDialog.ShowDialog()) == DialogResult.OK)
                {
                    //wyciągamy rozszerzenie wskazanego pliku
                    string fileExt = Path.GetExtension(mySaveDialog.FileName);

                    //sprawdź próbę zapisania grafiki w pliku tekstowym
                    if (this.richTextBox.Rtf.Contains(@"\pict") && fileExt == ".txt")
                    {
                        DialogResult dr2 = MessageBox.Show(string.Format("Wykryto grafikę (typ pliku: tekstowy)."
                            + "Nie zostanie ona zapisana.\n Kontynuować mimo to?"), "Ostrzeżenie",
                            MessageBoxButtons.YesNo);
                        if (dr2 == DialogResult.No)
                        {
                            this.tsLabelState.Text = "Anulowano zapisywanie zmian w pliku.";
                            return;
                        }
                    }
                    Stream myStream;
                    if ((myStream = mySaveDialog.OpenFile()) != null)
                    {
                        if (fileExt == ".txt")
                        {
                            //działam w ten sposób, ponieważ metoda SaveFile() dla RichTextBox.PlainText
                            //zapisuje tak, że przy odczycie nie ma polskich znaków
                            string plainText = this.richTextBox.Text;
                            StreamWriter textWriter = new StreamWriter(myStream);
                            textWriter.WriteLine(this.richTextBox.Text);
                            textWriter.Dispose();
                            myStream.Dispose();
                            this.baseText = this.richTextBox.Text;
                            this.documentTitle = Path.GetFileNameWithoutExtension(mySaveDialog.FileName);
                            this.documentExt = Path.GetExtension(mySaveDialog.FileName);
                            this.currentOpenDocument = Path.GetFileName(mySaveDialog.FileName); //ostatnio zapisany dokument
                            this.currentPathToOpenDocument = mySaveDialog.FileName; //ścieżka ostatnio zapisanego dokumentu 
                            this.isFontStyleChanged = false; //zresteuj styl czcionki (odniesienie)

                            //dezaktywacja funkcji RTF
                            this.toolStripFont.Enabled = false;
                            this.BtnImage.Enabled = false;
                        }
                        else if (fileExt == ".rtf")
                        {
                            this.richTextBox.SaveFile(myStream, RichTextBoxStreamType.RichText);
                            myStream.Dispose();
                            this.baseText = this.richTextBox.Rtf;
                            this.documentTitle = Path.GetFileNameWithoutExtension(mySaveDialog.FileName);
                            this.documentExt = Path.GetExtension(mySaveDialog.FileName);
                            this.currentOpenDocument = Path.GetFileName(mySaveDialog.FileName); //ostatnio zapisany dokument
                            this.currentPathToOpenDocument = mySaveDialog.FileName; //ścieżka ostatnio zapisanego dokumentu 
                            this.isFontStyleChanged = false; //zresteuj styl czcionki (odniesienie)

                            //aktywacja funkcji RTF
                            this.toolStripFont.Enabled = true;
                            this.BtnImage.Enabled = true;
                        }
                        this.canNewFile = true; //wybrano plik, opcja NewFile może wykonać operacje
                        this.Text = Path.GetFileName(mySaveDialog.FileName) + " - " + Application.ProductName;
                        this.tsLabelState.Text = "Zapisano zmiany w pliku " + this.Text;
                        myStream.Dispose();
                    }
                }
                else if (dr == DialogResult.Cancel)
                {
                    this.tsLabelState.Text = "Anulowano zapisywanie zmian pliku "+this.Text;
                    this.canNewFile = false; //zrezygnowano z wyboru pliku, opcja NewFile nie może wykonać operacji
                }
            }
            else //automatyczny zapis
            {
                string fileExt = this.documentExt; //pobierz rozszerzenie dokumentu
                
                if (fileExt == ".txt")
                {
                    //zapobiegnij niepoprawnej operacji zapisu grafki w pliku tekstowym
                    if (this.richTextBox.Rtf.Contains(@"\pict"))
                    {
                        DialogResult dr2 = MessageBox.Show(string.Format("Wykryto grafikę (typ pliku: tekstowy)."
                            + "Nie zostanie ona zapisana.\n Kontynuować mimo to?"), "Ostrzeżenie",
                            MessageBoxButtons.YesNo);
                        if (dr2 == DialogResult.No)
                        {
                            this.tsLabelState.Text = "Anulowano zapisywanie zmian w pliku.";
                            return;
                        }
                    }

                    string plainText = this.richTextBox.Text;
                    StreamWriter textWriter = new StreamWriter(this.currentPathToOpenDocument);
                    textWriter.WriteLine(this.richTextBox.Text);
                    textWriter.Dispose();
                    //w tym miejscu ścieżka i nazwa ostatnio otwartego pliku się nie zmieniają! (dokonano tylko zapisu)
                    this.baseText = this.richTextBox.Text;
                    this.isFontStyleChanged = false;

                    //dezaktywacja funkcji RTF
                    this.toolStripFont.Enabled = false;
                    this.BtnImage.Enabled = false;
                }
                else if (fileExt == ".rtf")
                {
                    this.richTextBox.SaveFile(this.currentPathToOpenDocument, RichTextBoxStreamType.RichText);
                    //w tym miejscu ścieżka i nazwa ostatnio otwartego pliku się nie zmieniają! (dokonano tylko zapisu)
                    this.baseText = this.richTextBox.Rtf;
                    this.isFontStyleChanged = false;

                    //aktywacja funkcji RTF
                    this.toolStripFont.Enabled = true;
                    this.BtnImage.Enabled = true;
                }
                this.canNewFile = true; //wybrano plik, opcja NewFile może wykonać operacje
                this.tsLabelState.Text = "Zapisano zmiany w pliku " + this.Text;
                this.Text = Path.GetFileName(this.currentPathToOpenDocument) + " - " + Application.ProductName;
            }
        }
        #endregion

        #region Uaktualnienie stylu czcionki
        private void GetFontStyle(string state)
        {
            if(this.richTextBox.SelectionLength==0) //jeśli nie zaznaczono żadnego tekstu
            {
                this.currStyle = this.richTextBox.SelectionFont.Style; //pobierz styl czcionki w miejscu kursora

                if (this.tsBtnBold.Checked)
                    this.currStyle = this.currStyle | FontStyle.Bold; //"dodaj" styl czcionki (operator bitowy)
                else
                    this.currStyle = this.currStyle & ~FontStyle.Bold; // "usuń" styl czcionki (operator bitowy)

                if (this.tsBtnItalic.Checked)
                    this.currStyle = this.currStyle | FontStyle.Italic;
                else
                    this.currStyle = this.currStyle & ~FontStyle.Italic;

                if (this.tsBtnUnderline.Checked)
                    this.currStyle = this.currStyle | FontStyle.Underline;
                else
                    this.currStyle = this.currStyle & ~FontStyle.Underline;

                if (this.tsBtnStrikeout.Checked)
                    this.currStyle = this.currStyle | FontStyle.Strikeout;
                else
                    this.currStyle = this.currStyle & ~FontStyle.Strikeout;

                //zwróć czcionkę z uaktualnionym stylem (w miejscu kursora)
                this.richTextBox.SelectionFont = new Font(this.richTextBox.SelectionFont, this.currStyle);
                this.currFontFamily = this.richTextBox.SelectionFont.FontFamily;
            }
            else //mamy zaznaczony cały tekst
            {
                //tutaj trzeba pokombinować, bo jak zaznaczymy tekst, który ma mieszane style (np. i pogrubienie i kursywa)
                //to zawsze SelectionFont zwraca styl z cześci wspólnej zaznaczonego tekstu! A my musimy wiedzieć dokładnie,
                //jaka część zaznaczenia ma jaki styl, dlatego przejdziemy pętlą po każdym znaku zaznaczenia

                //w tym przypadku nie musimy martwić się wymuszoną przez VS zmianą rozmiaru czcionki o =-0.25pt 
                //wyjaśnienie: patrz "Uaktualnienie rozmiaru czcionki", ponieważ nie manipulujemy jej wielkością bezpośrednio,
                //tylko stylem, zatem po powrocie do stanu początkowego (bez względu na początkowy rozmiar czcionki) 
                //ta różnica utrzyma się na powyższym poziomie, co jest zupełnie w porządku (dla naszych potrzeb),
                //ogólnie, to ten proces jest baaaaardzo wkurzający ....

                //tworzymy tymczasowy RTB, w którym podmienimy style czcionki (tymczasowy RTB zapobiega migotaniom ekranu!)
                using(RichTextBox tempRichTextBox =new RichTextBox()) //po using, wywoływane jest automatycznie Dispose()
                {
                    int tempRichTextBoxStart = 0; //pomocnicza, punkt wyjścia zaznaczenia dla tymczasowego RTB
                    int richTextLength = this.richTextBox.SelectionLength; //długość bazowego zaznaczenia
                    int richTextStart = this.richTextBox.SelectionStart; //punkt wyjścia bazowego zaznaczenia
                    //kopiujemy tekst zaznaczenia z bazowego RTB do tymczasowego RTB
                    tempRichTextBox.Rtf = this.richTextBox.SelectedRtf;
                    for (int i = 0; i < this.richTextBox.SelectionLength; i++) //przechodzimy przez każdy punkt zaznaczenia
                    {
                        tempRichTextBox.Select(tempRichTextBoxStart + i, 1); //zaznaczamy każdy kolejny JEDEN znak

                        this.currStyle = tempRichTextBox.SelectionFont.Style; //i pobieramy jego styl

                        if (state == "b")//jeśli metodę wywołało kliknięcie przycisku pogrubienia
                        {
                            if (this.tsBtnBold.Checked) //to jeśli jest zaznaczony
                                this.currStyle = this.currStyle | FontStyle.Bold; //to "dodaj" styl czcionki (operator bitowy)
                            else
                                this.currStyle = this.currStyle & ~FontStyle.Bold; //lub "usuń" styl czcionki (operator bitowy)
                        }

                        if (state == "i") //j.w.
                        {
                            if(this.tsBtnItalic.Checked)
                            this.currStyle = this.currStyle | FontStyle.Italic;
                        else
                            this.currStyle = this.currStyle & ~FontStyle.Italic;
                        }

                        if (state == "u") //j.w.
                        {
                            if (this.tsBtnUnderline.Checked)
                                this.currStyle = this.currStyle | FontStyle.Underline;
                            else
                                this.currStyle = this.currStyle & ~FontStyle.Underline;
                        }

                        if(state == "s") //j.w.
                        if (this.tsBtnStrikeout.Checked)
                            this.currStyle = this.currStyle | FontStyle.Strikeout;
                        else
                            this.currStyle = this.currStyle & ~FontStyle.Strikeout;

                        //zwróć czcionkę z uaktualnionym stylem (dla pojedynczego znaku tymczasowego RTB)
                        tempRichTextBox.SelectionFont = new Font(tempRichTextBox.SelectionFont, this.currStyle);
                    }
                    //zaznacz tekst z uaktualnioną czcionką z tymczasowego RTB
                    tempRichTextBox.Select(tempRichTextBoxStart, this.richTextBox.SelectionLength);
                    //i ustaw go jako ten zaznaczony w oryginalnym RTB
                    //uwaga! tekst zostatnie OD RAZU zamieniony + znika dotychczasowe zazn.w oryg. RTB (kursor idzie na koniec!)
                    //dlatego konieczne są zmienne "richTextStart" i "richTextLength" deklarowane na początku instrukcji!
                    this.richTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;
                    this.richTextBox.Select(richTextStart, richTextLength); //zaznacz tekst w oryginalnym RTB
                }//automatycznie wywołana metoda Dispose() dla tempRichTextBox            
            }
        }
        #endregion

        #region Resetowanie ustawień strony
        private void ResetPageSettings() //funkcja przywracająca RichTextBox do stanu początkowego
        {
            this.richTextBox.ResetText();
            this.tsBtnBold.Checked = false;
            this.tsBtnItalic.Checked = false;
            this.tsBtnUnderline.Checked = false;
            this.tsBtnSubscript.Checked = false;
            this.tsBtnSuperscript.Checked = false;
            this.tsBtnFontColor.BackColor = Color.Black;
            this.tsBtnFontBackColor.BackColor = Color.White;
            this.tsComboBoxFontSizeChoice.Text = this.startSize.ToString();
            this.tsComboBoxFontChoice.Text = this.startFontFamily.Name;
            this.GetFontStyle("");
            this.tsUndo.Enabled = false;
            this.tsRedo.Enabled = false;
            this.tsMenuEditUndo.Enabled = false;
            this.tsMenuEditRedo.Enabled = false;
            if (Clipboard.GetDataObject() != null && this.documentExt==".rtf")
                this.pn1_BtnPaste.Enabled = true;
            this.AlignmentTempButton = this.tsBtnLeftAllignement;
            this.AlignmentTempButton.Checked = true;
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }
        #endregion
        #region Sprawdzanie stanu do zapisu
        private bool GetSaveState()
        {
            if (this.documentExt == ".rtf")
            {
                if (this.richTextBox.Rtf != this.baseText) //jeśli nastąpiła zmiena w tekście względem tekstu odniesienia
                    return true;
            }
            else if (this.documentExt == ".txt")
                if (this.richTextBox.Text != this.baseText) //jeśli nastąpiła zmiena w tekście względem tekstu odniesienia
                    return true;

            if (this.isFontStyleChanged) //jeśli nastąpiła choć jedna zmiana stylu czcionki
                return true;

            return false;
        }
        #endregion
        #region  Uaktualnienie rozmiaru czcionki
        private void GetBaseSize()
        {
            if (this.richTextBox.SelectionLength == 0)
            {
                this.baseSize = this.richTextBox.SelectionFont.Size;
            }
            else //gdy tekst jest zaznaczony, osobny algorytm
            {
                //gdy tekst ma się podnieść, obniżyć (czyli wciśnięty przycisk dowolnego indeksu)
                if (this.tsBtnSuperscript.Checked || this.tsBtnSubscript.Checked)
                {
                    using (RichTextBox tempRichTextBox = new RichTextBox()) //trick z tymczasowym RTB
                    {
                        //info: prawdopodobnie w VS jest pewien bug. Polega on na tym, że po zaznaczeniu tekstu
                        //rozmiar poszczególnych znaków różni się od tego, który jest wskazywany przez program bez zaznaczenia!
                        //Wynika to prawdopodobnie z opisanej przeze mnie w opisie do przycisku "Indeks górny" kwestii
                        //modyfikowania przez VS rozmiaru czcionki o +-0.25pt. Niestety zaokrąglenie to działa w pewien 
                        //bardzo dziwny sposób, np. rozmiar 11 pozostawia w tym przypadku jako 11, ale 11.25 zmienia na 11.50 (...)
                        //dlatego też poniższy sposób działania ogranicza różnice w rozmiarze czcionki przed/po zaznaczeniu
                        //maksymalnie jak tylko potrafiłem, czyli właśnie o +-0.25pt. Różnica ta nie wzrasta wraz z rozmiarem,
                        //a gołym okiem jest niemożliwa do zauważenia - tekst wygląda identycznie. Można byłoby wyeliminować
                        //ją całkiem, ale wymagało by to wykonania ostatniego działania na oryginalnym RTB, co przy dużych ilościach
                        //tekstu mogło by spowodować widoczne migotanie ekranu, spowolnienie działania programu i zaburzenia 
                        //w wykonywaniu sekwencji cofania (ctrl + Z), dlatego rozsądniejsza wydaje się być pierwsza opcja.

                        //Co więcej warto zadać sobie pytanie - kto wie, że wybierając w Microsoft Word czcionkę 11, wybieramy
                        //tak na prawdę czcionkę 11.25? :)

                        tempRichTextBox.Rtf = this.richTextBox.SelectedRtf; 
                        int tempStart = this.richTextBox.SelectionStart;
                        int tempLength = this.richTextBox.SelectionLength;
                        for (int i = 0; i < tempLength; i++)
                        {
                            tempRichTextBox.Select(i, 1); //zaznaczamy pojedynczy znak
                            if (tempRichTextBox.SelectionCharOffset == 0) //jeśli znak znajduje się na linii bazowej
                            {
                                //pobieramy styl czcionki dla zaznaczonego znaku
                                this.currFontFamily = tempRichTextBox.SelectionFont.FontFamily;
                                this.currStyle = tempRichTextBox.SelectionFont.Style;
                                //korygujemy automatyczną zmianę czcionki o =-0.25pt
                                this.GetCorrectedSize(tempRichTextBox.SelectionFont.Size, false);
                                this.baseSize = (float)Math.Round((decimal)this.baseSize, 0, MidpointRounding.AwayFromZero);
                                //ustawiamy odpowiednie przesunięcie pionowe tekstu
                                if(this.tsBtnSuperscript.Checked)
                                    tempRichTextBox.SelectionCharOffset = (int)(this.baseSize / this.charOffsetDivider);
                                else
                                    tempRichTextBox.SelectionCharOffset = -(int)(this.baseSize / this.charOffsetDivider);
                                this.currFont = new Font(this.currFontFamily, this.baseSize / (this.charOffsetDivider),
                                    this.currStyle);
                                tempRichTextBox.SelectionFont = this.currFont; //przypisujemy zmienionę czcionkę do znaku
                            } //gdy przechodzimy z indeksu dolnego bezpośrednio na indeks górny
                            else if (tempRichTextBox.SelectionCharOffset < 0 && tsBtnSuperscript.Checked)
                            {
                                //najpierw wchodzimy na poziom "0"
                                this.currFontFamily = tempRichTextBox.SelectionFont.FontFamily;
                                this.currStyle = tempRichTextBox.SelectionFont.Style;
                                //korygowanie czcionki 
                                this.GetCorrectedSize(tempRichTextBox.SelectionFont.Size, true);
                                tempRichTextBox.SelectionCharOffset = 0;
                                this.currFont = new Font(this.currFontFamily, this.baseSize,
                                   this.currStyle);
                                tempRichTextBox.SelectionFont = this.currFont;

                                //wchodzimy na poziom indeksu górnego
                                //ponowna korekta rozmiaru czionki - tekst znów był zaznaczany!
                                this.GetCorrectedSize(tempRichTextBox.SelectionFont.Size, false);
                                this.baseSize = (float)Math.Round((decimal)this.baseSize, 0, MidpointRounding.AwayFromZero);
                                tempRichTextBox.SelectionCharOffset = (int)(this.baseSize / this.charOffsetDivider);
                                this.currFont = new Font(this.currFontFamily, this.baseSize / (this.charOffsetDivider),
                                    this.currStyle);
                                tempRichTextBox.SelectionFont = this.currFont;                           
                            } //gdy przechodzimy z indeksu górnego bezpośrednio na idneks dolny
                            else if(tempRichTextBox.SelectionCharOffset > 0 && tsBtnSubscript.Checked)
                            {
                                //działanie analogiczne j.w (poziom "0" -> poziom indeksu dolnego)
                                this.currFontFamily = tempRichTextBox.SelectionFont.FontFamily;
                                this.currStyle = tempRichTextBox.SelectionFont.Style;
                                this.GetCorrectedSize(tempRichTextBox.SelectionFont.Size, true);
                                tempRichTextBox.SelectionCharOffset = 0;
                                this.currFont = new Font(this.currFontFamily, this.baseSize,
                                    this.currStyle);
                                tempRichTextBox.SelectionFont = this.currFont;

                                this.GetCorrectedSize(tempRichTextBox.SelectionFont.Size, false);
                                this.baseSize = (float)Math.Round((decimal)this.baseSize, 0, MidpointRounding.AwayFromZero);
                                tempRichTextBox.SelectionCharOffset = (int)(this.baseSize / this.charOffsetDivider);
                                this.currFont = new Font(this.currFontFamily, this.baseSize / (this.charOffsetDivider),
                                    this.currStyle);
                                tempRichTextBox.SelectionFont = this.currFont;
                            }
                        }
                        //tutaj zaznaczenie tekstu, którego nie korygujemy, bo należało by to zrobić dla oryginalnego RTB
                        //a chcemy uniknąć niepotrzebnego migotania ekranu
                        tempRichTextBox.Select(0, tempLength); 
                        this.richTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;
                        this.richTextBox.Select(tempStart, tempLength);
                    }
                }
                else //gdy tekst ma powrócić do poziomu "0" (czyli nie wciśnięty żaden przycisk indeksu)
                {
                    using (FixedRichTextBox tempRichTextBox = new FixedRichTextBox())
                    {
                        tempRichTextBox.Rtf = this.richTextBox.SelectedRtf;
                        int tempStart = this.richTextBox.SelectionStart;
                        int tempLength = this.richTextBox.SelectionLength;
                        for (int i = 0; i < tempLength; i++)
                        {
                            tempRichTextBox.Select(i, 1);
                            if (tempRichTextBox.SelectionCharOffset != 0) 
                            {
                                this.currFontFamily = tempRichTextBox.SelectionFont.FontFamily;
                                this.currStyle = tempRichTextBox.SelectionFont.Style;
                                //korekta rozmiaru czcionki
                                this.GetCorrectedSize(tempRichTextBox.SelectionFont.Size, true);
                                tempRichTextBox.SelectionCharOffset = 0;
                                this.currFont = new Font(this.currFontFamily, this.baseSize,
                                    this.currStyle);
                                tempRichTextBox.SelectionFont = this.currFont;
                            }
                        }
                        //j.w. - korekty po tym zaznaczeniu nie eliminujemy
                        tempRichTextBox.Select(0, tempLength);
                        this.richTextBox.SelectedRtf = tempRichTextBox.SelectedRtf;
                        this.richTextBox.Select(tempStart, tempLength);
                    }
                }
            }

        }
        #endregion
        #region Poprawka czcionki przy zmniejszaniu (wyjaśnienie w metodzie)
        //funkcja pomocnicza do regulacji rozmiaru czcionki
        //wyjaśnienie: generalnie, w programie włączone są pewne automatyczne przeliczenia rozmiaru czcionki, które
        //niezmiernie utrudniają pracę w VS. Chodzi o to, że określając rozmiar czcionki np. na 8pt, wcale nie mamy pewności,
        //że kompilator tak ją ustawi- wręcz przeciwnie, raczej tak się nie stanie. Wynika to z faktu, że dokonywane jest
        //przeliczenie typu: czcionka rozmiar 8pt-> jeden pt to 1/72 cala, natomiast standardowa rozdzielczość ekranu to 96 dpi,
        //zatem wyświetlamy czcionkę z wielkością 8/72*96 = 10.(6)~ 11pt-> powracamy do projektu, działanie odwrotne: 
        // 11/96*72=8.25pt! Można to zauważyć, wybierając w udostępnionym przez Microsoft oknie dialogowym wyboru czcionki:
        //wybierając np. czcionkę 11, tak na prawdę kompilator podstawia wartość 11.25. Różnice te wahają się +- 0.25 pt i nie
        //zwiększają się wraz z rozmiarem czcionki (dla 5000pt będzie to wciąż +- 0.25pt). Poniższa funkcja wykorzystuje 
        //stworzony przeze mnie prosty algorytm do korygowania tych zmian, które są w pewnym sensie nam narzucone. Parametr
        //boolowski wynika z faktu, czy potrzebujemy w danym momencie zaokrąglenia, czy mamy brać podaną w pierwszym parametrze
        //jako oryginalną. W różnych miejscach, może się przydać jedna i druga opcja, to już zależy od zamierzeń programisty.
        private float GetCorrectedSize(float actualSize,bool state)
        {
            decimal size=0;
            if(state)
                size = Math.Round((decimal)(actualSize * this.charOffsetDivider), 0, MidpointRounding.AwayFromZero);
            else
                size= (decimal)actualSize;
            decimal size2 = (Math.Round(size * 96 / 72)) * 72 / 96;
            if (size > size2)
                this.baseSize = (float)size - 0.25F;
            else if (size < size2)
                this.baseSize = (float)size + 0.25F;
            else
                this.baseSize = (float)size;
            return this.baseSize;
        }
        #endregion

        #region RTB_MouseMove
        private void MouseMove_RTB(object sender, MouseEventArgs e)
        {
            if(this.richTextBox.SelectionLength!=0)
            {
                this.pn1_BtnCut.Enabled = true;
                this.pn1_BtnKopiuj.Enabled = true;
                this.tsMenuEditCut.Enabled = true;
                this.tsMenuEditCopy.Enabled = true;
            }
            else
            {
                this.pn1_BtnCut.Enabled = false;
                this.pn1_BtnKopiuj.Enabled = false;
                this.tsMenuEditCut.Enabled = false;
                this.tsMenuEditCopy.Enabled = false;
            }
        }
        #endregion
        #region RTB_MouseUp
        private void richTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) //jeśli był to lewy przycisk myszy
            {
                #region Wykryj styl czcionki
                //pobierz styl z miejsca wskazania kursora lub zaznaczenia
                //uwaga! jeśli zaznaczenie ma więcej niż jeden styl, wybierana jest ich część wspólna !!
                //
                //wyjątek!: jak zaznaczymy cały tekst tak, że zaznaczone jest również ostatnie puste pole
                //dodawane automatycznie, to SelectionFont zwróci wartość "null"! Należy zatem się przed tym zabezpieczyć
                try
                {
                    this.currStyle = this.richTextBox.SelectionFont.Style;
                }
                catch(System.NullReferenceException)
                {
                    this.currStyle = FontStyle.Regular;
                }
                //wykryj istniejące style i uaktywnij odpowiednie przyciski 
                if (this.currStyle.ToString().IndexOf("Bold") != -1)
                    this.tsBtnBold.Checked = true;
                else
                    this.tsBtnBold.Checked = false;

                if (this.currStyle.ToString().IndexOf("Italic") != -1)
                    this.tsBtnItalic.Checked = true;
                else
                    this.tsBtnItalic.Checked = false;

                if (this.currStyle.ToString().IndexOf("Underline") != -1)
                    this.tsBtnUnderline.Checked = true;
                else
                    this.tsBtnUnderline.Checked = false;

                if (this.currStyle.ToString().IndexOf("Strikeout") != -1)
                    this.tsBtnStrikeout.Checked = true;
                else
                    this.tsBtnStrikeout.Checked = false;
                #endregion

                #region Wykryj indeks górny/dolny
                //wykryj indeks górny i dolny (0 - to linia bazowa, powyżej: indeks górny, poniżej: indeks dolny)
                if (this.richTextBox.SelectionLength == 0)
                {
                    if (this.richTextBox.SelectionCharOffset < 0)
                    {
                        this.tsBtnSubscript.Checked = true;
                        this.tsBtnSuperscript.Checked = false;
                    }
                    else if (this.richTextBox.SelectionCharOffset > 0)
                    {
                        this.tsBtnSubscript.Checked = false;
                        this.tsBtnSuperscript.Checked = true;
                    }
                    else
                    {
                        this.tsBtnSubscript.Checked = false;
                        this.tsBtnSuperscript.Checked = false;
                    }
                }
                else //gdy jest zaznaczenie 
                {
                    using (FixedRichTextBox tempRichTextBox = new FixedRichTextBox())
                    {
                        tempRichTextBox.Rtf = this.richTextBox.SelectedRtf;
                        int tempStart = this.richTextBox.SelectionStart;
                        int tempLength = this.richTextBox.SelectionLength;

                        tempRichTextBox.Select(0, 1);
                        bool startSign = tempRichTextBox.SelectionCharOffset > 0;
                        int firstValue = tempRichTextBox.SelectionCharOffset;
                        if(startSign) //jeżeli pierwszy większy od zera
                            {
                                for (int i = 1; i < this.richTextBox.SelectionLength; i++)
                                {
                                    tempRichTextBox.Select(i, 1);
                                    if (tempRichTextBox.SelectionCharOffset <= 0)
                                    {
                                        this.tsBtnSubscript.Checked = false;
                                        this.tsBtnSuperscript.Checked = false;
                                        break;
                                    }
                                    this.tsBtnSuperscript.Checked = true;
                                }
                            }
                        else if(!startSign && firstValue != 0) //jeżeli pierwszy mniejszy od zera
                        {
                            for (int i = 1; i < this.richTextBox.SelectionLength; i++)
                            {
                                this.tsBtnSubscript.Checked = true;
                                tempRichTextBox.Select(i, 1);
                                if (tempRichTextBox.SelectionCharOffset >= 0)
                                {
                                    this.tsBtnSubscript.Checked = false;
                                    this.tsBtnSuperscript.Checked = false;
                                    break;
                                }
                                this.tsBtnSuperscript.Checked = true;
                            }
                        }
                        else //jeżeli równy zero
                        {
                            this.tsBtnSubscript.Checked = false;
                            this.tsBtnSuperscript.Checked = false;
                        }
                    }
                }
                #endregion

                #region Wykryj rozmiar czcionki
                if(this.richTextBox.SelectionLength == 0)
                {
                    this.baseSize = this.richTextBox.SelectionFont.Size;
                    this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                }
                else
                {
                    using(FixedRichTextBox tempRichTextBox=new FixedRichTextBox())
                    {
                        int startLength=this.richTextBox.SelectionLength;
                        tempRichTextBox.Rtf = this.richTextBox.SelectedRtf;
                        tempRichTextBox.Select(0,1);
                        float startSize = tempRichTextBox.SelectionFont.Size;
                        for (int i = 1; i < startLength; i++ )
                        {
                            tempRichTextBox.Select(i, 1);
                            if(tempRichTextBox.SelectionFont.Size != startSize)
                            {
                                this.tsComboBoxFontSizeChoice.Text = "";
                                break;
                            }
                            this.baseSize = startSize;
                            this.tsComboBoxFontSizeChoice.Text =((int)this.baseSize).ToString();
                        }
                    }
                }
                #endregion

                #region Wykryj nazwę rodziny czcionek
                if(this.richTextBox.SelectionLength==0)
                {
                    this.tsComboBoxFontChoice.Text = this.richTextBox.SelectionFont.FontFamily.Name;
                }
                else
                {
                    using(FixedRichTextBox tempRichTextBox = new FixedRichTextBox())
                    {
                        tempRichTextBox.Rtf = this.richTextBox.SelectedRtf;
                        int tempStart = this.richTextBox.SelectionStart;
                        bool isDifferent = false;

                        tempRichTextBox.Select(0,1);
                        string firstFamilyName = tempRichTextBox.SelectionFont.FontFamily.Name;

                        for(int i=1; i < this.richTextBox.SelectionLength;i++)
                        {
                            tempRichTextBox.Select(i, 1);
                            if(firstFamilyName != tempRichTextBox.SelectionFont.FontFamily.Name)
                            {
                                isDifferent = true;
                                break;
                            }
                        }
                        if (!isDifferent)
                            this.tsComboBoxFontChoice.Text = firstFamilyName;
                        else
                            this.tsComboBoxFontChoice.Text = "";
                    }
                }
                #endregion
             
                #region Wykrywanie wyrównania tekstu
                if (this.richTextBox.SelectionLength == 0)
                {
                    this.AlignmentTempButton.Checked = false;
                    HorizontalAlignment tempAlign = this.richTextBox.SelectionAlignment;

                    if (tempAlign == HorizontalAlignment.Left)
                        this.AlignmentTempButton = this.tsBtnLeftAllignement;
                    else if (tempAlign == HorizontalAlignment.Center)
                        this.AlignmentTempButton = this.tsBtnCenterAllignment;
                    else
                        this.AlignmentTempButton = this.tsBtnRightAlignment;
                    this.AlignmentTempButton.Checked = true;
                }
                #endregion
            }
        }
        #endregion 
        #region RTB_PreviewKeyDown
        private void richTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           
            #region Aktualizacja rozmiaru czcionki
            //aktualizacja rozmiaru czcionki, gdy zaznaczony tekst obejmuje więcej niż jeden rozmiar
            //zakładamy, że pobieramy rozmiar pierwszego znaku zaznaczenia
                if (this.richTextBox.SelectionLength != 0 && this.tsComboBoxFontSizeChoice.Text == "")
                {
                    using (FixedRichTextBox tempRichTextBox = new FixedRichTextBox())
                    {
                        int startPos = this.richTextBox.SelectionStart;
                        tempRichTextBox.Rtf = this.richTextBox.SelectedRtf;
                        tempRichTextBox.Select(0, 1);
                        this.baseSize = tempRichTextBox.SelectionFont.Size;
                        this.tsComboBoxFontSizeChoice.Text = this.baseSize.ToString();
                    }
                }
            #endregion

            #region Aktualizacja wyrównania tekstu

                this.AlignmentTempButton.Checked = false;
                if (this.richTextBox.SelectionAlignment == HorizontalAlignment.Left)
                    this.AlignmentTempButton = this.tsBtnLeftAllignement;
                else if (this.richTextBox.SelectionAlignment == HorizontalAlignment.Center)
                    this.AlignmentTempButton = this.tsBtnCenterAllignment;
                else
                    this.AlignmentTempButton = this.tsBtnRightAlignment;
                this.AlignmentTempButton.Checked = true;
            #endregion

            #region Działania undo /redo
            if (e.Modifiers != (Keys.Control))
            {
                this.tsRedo.Enabled = false;
                this.tsUndo.Enabled = true;
                this.tsMenuEditUndo.Enabled = true;
                this.tsMenuEditRedo.Enabled = false;
            }
            #endregion

        }
        #endregion
        #region RTB_KeyDown
        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {           
            //nie pozwół użytkownikowi wklejać zdjęć, gdy format pliku to *.txt
           if(this.documentExt == ".txt")
            {
                if (Clipboard.ContainsImage() && e.KeyCode == Keys.V && ModifierKeys == Keys.Control)
                    Clipboard.Clear();

                //a gdy jest tekst, to zmień kolor na domyślny dla plików tekstowych (czarny)
                if(Clipboard.ContainsText() && e.KeyCode == Keys.V && ModifierKeys == Keys.Control)
                {
                    using(FixedRichTextBox tempRichTextBox = new FixedRichTextBox())
                    {
                        tempRichTextBox.Text = Clipboard.GetText();
                        tempRichTextBox.SelectAll();
                        tempRichTextBox.SelectionColor = Color.Black;                       
                        this.richTextBox.SelectedText = tempRichTextBox.SelectedText;
                        e.SuppressKeyPress = true;
                    }
                }
            }
        }
        #endregion	
        #region RTB_KeyUp
        private void richTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            #region Aktualizacja stylu czionki / wyrównania tekstu
            //KLAWISZE STRZAŁEK + CONTROL_Z + BACKSPACE
            //musimy uaktualnić styl czcionki (stan przycisków), gdy usuwamy znaki!!
            //ułatwienie: zawsze usuwamy jeden znak
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up ||
                e.KeyCode == Keys.Down || e.KeyCode == Keys.Delete || (e.Control && e.KeyCode == Keys.Z)
                || e.KeyCode == Keys.Back)
            {

                #region Styl
                this.currStyle = this.richTextBox.SelectionFont.Style;

                //wykryj istniejące style i uaktywnij odpowiednie przyciski 
                if (this.currStyle.ToString().IndexOf("Bold") != -1)
                    this.tsBtnBold.Checked = true;
                else
                    this.tsBtnBold.Checked = false;

                if (this.currStyle.ToString().IndexOf("Italic") != -1)
                    this.tsBtnItalic.Checked = true;
                else
                    this.tsBtnItalic.Checked = false;

                if (this.currStyle.ToString().IndexOf("Underline") != -1)
                    this.tsBtnUnderline.Checked = true;
                else
                    this.tsBtnUnderline.Checked = false;

                if (this.currStyle.ToString().IndexOf("Strikeout") != -1)
                    this.tsBtnStrikeout.Checked = true;
                else
                    this.tsBtnStrikeout.Checked = false;
                #endregion

                #region Indeks górny/dolny
                if (this.richTextBox.SelectionCharOffset > 0)
                    this.tsBtnSuperscript.Checked = true;
                else
                    this.tsBtnSuperscript.Checked = false;

                if (this.richTextBox.SelectionCharOffset < 0)
                    this.tsBtnSubscript.Checked = true;
                else
                    this.tsBtnSubscript.Checked = false;
                #endregion

                #region Rozmiar czcionki
                this.tsComboBoxFontSizeChoice.Text = ((int)this.richTextBox.SelectionFont.Size).ToString();
                #endregion

                #region Wyrównanie tekstu

                this.AlignmentTempButton.Checked = false;
                if (this.richTextBox.SelectionAlignment == HorizontalAlignment.Left)
                    this.AlignmentTempButton = this.tsBtnLeftAllignement;
                else if (this.richTextBox.SelectionAlignment == HorizontalAlignment.Center)
                    this.AlignmentTempButton = this.tsBtnCenterAllignment;
                else
                    this.AlignmentTempButton = this.tsBtnRightAlignment;
                this.AlignmentTempButton.Checked = true;
                #endregion

            }

            //uaktywnij Kopiuj/Wklej, gdy jest zaznaczenie
            if (this.richTextBox.SelectionLength != 0)
            {
                this.pn1_BtnCut.Enabled = true;
                this.pn1_BtnKopiuj.Enabled = true;
                this.tsMenuEditCut.Enabled = true;
                this.tsMenuEditCopy.Enabled = true;
            }
            else
            {
                this.pn1_BtnCut.Enabled = false;
                this.pn1_BtnKopiuj.Enabled = false;
                this.tsMenuEditCut.Enabled = false;
                this.tsMenuEditCopy.Enabled = false;
            }
            #endregion
           
        }
        #endregion


        #region Przesłonięcie ProcessCmdKey przetwarzania polecenia wywołanego naciśnięciem klawisza
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.tsComboBoxFontChoice.Focused)
                {
                    this.tsComboBoxFontChoice.Text = this.startFontComboBoxText;
                    this.richTextBox.Focus();
                    this.tsComboBoxFontChoice.Focus();
                    this.isEscClicked = true;
                    return true;
                }
                else if (this.tsComboBoxFontSizeChoice.Focused)
                {
                    this.tsComboBoxFontSizeChoice.Text = this.startFontSizeComboBoxText;
                    this.tsComboBoxFontSizeChoice.Focus();
                    return true;
                }
            }

            if(keyData ==Keys.Back && this.tsComboBoxFontChoice.Focused)
            {
                this.isBackClicked = true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        #region Przesłonięcie WndProc w celu wyłapania zmian zawartości schowka
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if(m.Msg == (int)ClipboardOption.WM_CLIPBOARDUPDATE)
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (iData.GetDataPresent(DataFormats.Text) || iData.GetDataPresent(DataFormats.Rtf) ||
                    iData.GetDataPresent(DataFormats.Bitmap))
                {
                    this.pn1_BtnPaste.Enabled = true;
                    this.tsMenuEditPaste.Enabled = true;
                }
                else
                {
                    this.pn1_BtnPaste.Enabled = false;
                    this.tsMenuEditPaste.Enabled = false;
                }
            }
        }
        #endregion

        #region Przycisk "Pogrubienie"
        private void tsBtnBold_Click(object sender, EventArgs e)
        {
            this.isFontStyleChanged = true; //powiadomo, że zmianie uległ styl czcionki
            if (this.richTextBox.SelectionLength == 0)
            {
                this.GetFontStyle("");
                if (!this.richTextBox.Focused)
                    this.richTextBox.Focus();                
            }
            else
            {                
                this.GetFontStyle("b"); //ważne, aby przy zaznaczonym tekście powiadomić, który styl ma zostać zmieniony
                                        //aby pozostałe style pozostały nienaruszone
                if (!this.richTextBox.Focused)
                    this.richTextBox.Focus();
            }
        }
        #endregion
        #region Przycisk "Pochylenie"
        private void tsBtnItalic_Click(object sender, EventArgs e)
        {
            this.isFontStyleChanged = true; //powiadomo, że zmianie uległ styl czcionki
            if (this.richTextBox.SelectionLength == 0)
            {
                this.GetFontStyle("");
                if (!this.richTextBox.Focused)
                    this.richTextBox.Focus();               
            }
            else
            {               
                this.GetFontStyle("i");//ważne, aby przy zaznaczonym tekście powiadomić, który styl ma zostać zmieniony
                                        //aby pozostałe style pozostały nienaruszone
                if (!this.richTextBox.Focused)
                    this.richTextBox.Focus();
            }
        }
        #endregion
        #region Przycisk "Podkreślenie"
        private void tsBtnUnderline_Click(object sender, EventArgs e)
        {
            this.isFontStyleChanged = true;//powiadomo, że zmianie uległ styl czcionki
            if (this.richTextBox.SelectionLength == 0)
            {
                this.GetFontStyle("");
                if (!this.richTextBox.Focused)
                    this.richTextBox.Focus();                
            }
            else
            {                
                this.GetFontStyle("u");//ważne, aby przy zaznaczonym tekście powiadomić, który styl ma zostać zmieniony
                                        //aby pozostałe style pozostały nienaruszone
                if (!this.richTextBox.Focused)
                    this.richTextBox.Focus();
            }
        }
        #endregion
        #region Przycisk "Przekreślenie"
        private void tsBtnStrikeout_Click(object sender, EventArgs e)
        {
            this.isFontStyleChanged = true; //powiadomo, że zmianie uległ styl czcionki
            if (this.richTextBox.SelectionLength == 0)
            {
                this.GetFontStyle("");
                if (!this.richTextBox.Focused)
                    this.richTextBox.Focus();               
            }
            else
            {
                this.GetFontStyle("s"); //ważne, aby przy zaznaczonym tekście powiadomić, który styl ma zostać zmieniony
                                        //aby pozostałe style pozostały nienaruszone
                if (!this.richTextBox.Focused)
                    this.richTextBox.Focus();
            }
        }
        #endregion

        #region Przycisk "Indeks górny"
        private void tsBtnSuperscript_Click(object sender, EventArgs e)
        {
            if (this.richTextBox.SelectionLength == 0) //gdy nie mamy zaznaczenia
            {
                if (this.tsBtnSuperscript.Checked) //jeśli przycisk indeks górny jest wciśnięty
                {
                    if (!this.tsBtnSubscript.Checked) //sprawdzam, czy aktualnie nie znajduję się na indeksie dolnym 
                        this.GetBaseSize(); //jeżeli nie, to pobieram rozmiar czcionki w miejscu kursora
                    else //jeżeli tak, to ustawiam rozmiar dynamicznie (czcionka przy indeksie dolnym mogła się zmienić!)
                        this.baseSize = -1 * (this.richTextBox.SelectionCharOffset * this.charOffsetDivider);
                    this.tsBtnSubscript.Checked = false; //niezależnie od powyższego, odznaczam przycisk indeksu dolnego 
                    //teraz ustalam pozycję kursora ponad linią bazową
                    this.richTextBox.SelectionCharOffset = (int)(this.baseSize / this.charOffsetDivider);
                    this.GetFontStyle(""); //pobieram aktualny styl czcionki w miejscu kursora
                    //i definiuje aktualną czcionkę w oparciu o powyższe informacje
                    this.currFont = new Font(this.currFontFamily, (this.baseSize / this.charOffsetDivider), this.currStyle);
                    this.richTextBox.SelectionFont = this.currFont; //przypisuje ustaloną czcionkę w miejscu kursora

                    //wyjaśnienie: rozmiar czcionki ustalam na (this.baseSize/this.charOffsetDivider) dlatego, abym potem
                    //mógł łatwo odnaleźć rozmiar czcionki oryginalnej (wykonując działanie odwrotne - mnożenie)
                    //ponieważ charOffsetDivider jest minimalnie większy od 2 (2.1), to rozmiar nowej czcionki jest minimalnie
                    //mniejszy od połowy rozmiaru czcionki oryginalnej. Kursor znajduje się teraz również mniej więcej
                    //w połowie rozmiaru czcionki oryginalnej - SelectionCharOffset (mniej więcej, ponieważ nie można dla niej 
                    //ustawić liczby rzeczywistej, lecz tylko całkowitą (niestety, ograniczenie programowe)). Dzięki temu w 
                    //programie istnieje złudzenie, że stosując indeks górny i dolny, nie dość, że nie zmieniamy położenia linii
                    //bazowej, to na dodatek tekst jest wyrównany do poziomu wysokości czcionki oryginalnej. Oczywiście istnieje
                    //pewna różnica, wynikająca z utraty informacji przy konwersji jawnej z typu całkowitego na rzeczywisty
                    //jednak jest to różnica w granicach 0-0.3pt, co jest niezauważalne, nawet przy większych czcionkach
                    if (!this.richTextBox.Focused)
                        this.richTextBox.Focus();
                }
                else //przycisk indeks górny nie jest wciśnięty
                {
                    this.GetBaseSize(); //pobieram rozmiar w miejscu kursora
                    this.GetCorrectedSize(this.baseSize, true); //dokonuje odpowiedniego przeliczenia - funkcja pomocnicza GCS
                    this.richTextBox.SelectionCharOffset = 0; //ustawiam tekst na linii bazowej
                    this.GetFontStyle("");
                    this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                    this.richTextBox.SelectionFont = this.currFont;
                    if (!this.richTextBox.Focused)
                        this.richTextBox.Focus();
                }               
            }
            else //gdy mamy zaznaczony tekst
            {
                if (this.tsBtnSuperscript.Checked) //jeśli wciśnięty jest przycisk indeks górny
                {
                    this.GetBaseSize(); //pobierz rozmiar dla każdego znaku zaznaczenia
                    this.tsBtnSubscript.Checked = false; //odznacz wciśnięcie przycisku indeks dolny
                    if (!this.richTextBox.Focused)
                        this.richTextBox.Focus();
                }
                else //jeśli nie jest wciśnięty przycisk indeks górny
                {
                    this.GetBaseSize(); //pobierz rozmiar dla każdego znaku zaznaczenia
                    if (!this.richTextBox.Focused)
                        this.richTextBox.Focus();
                }
            }
        }
        #endregion
        #region Przycisk "Indeks dolny"
        //wytłumaczenie działania - patrz "Indeks górny"
        private void tsBtnSubscript_Click(object sender, EventArgs e)
        {
            if (this.richTextBox.SelectionLength == 0)
            {
                if (this.tsBtnSubscript.Checked)
                {
                    if (!this.tsBtnSuperscript.Checked)
                        this.GetBaseSize();
                    this.tsBtnSuperscript.Checked = false;
                    this.richTextBox.SelectionCharOffset = -(int)(this.baseSize / this.charOffsetDivider);
                    this.GetFontStyle("");
                    this.currFont = new Font(currFontFamily, (this.baseSize / this.charOffsetDivider), this.currStyle);
                    this.richTextBox.SelectionFont = this.currFont;
                    if (!this.richTextBox.Focused)
                        this.richTextBox.Focus();
                }
                else
                {
                    this.GetBaseSize();
                    this.GetCorrectedSize(this.baseSize, true);
                    this.richTextBox.SelectionCharOffset = 0;
                    this.GetFontStyle("");
                    this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                    this.richTextBox.SelectionFont = this.currFont;
                    if (!this.richTextBox.Focused)
                        this.richTextBox.Focus();
                }                
            }
            else //gdy mamy zaznaczenie
            {               
                if (this.tsBtnSubscript.Checked)
                {
                    this.GetBaseSize();
                    this.tsBtnSuperscript.Checked = false;
                    if (!this.richTextBox.Focused)
                        this.richTextBox.Focus();

                }
                else
                {
                    this.GetBaseSize();
                    //this.richTextBox.SelectionCharOffset = 0;
                    if (!this.richTextBox.Focused)
                        this.richTextBox.Focus();
                }
            }
        }
        #endregion

        #region Przycisk "Zwiększ rozmiar czcionki"
        private void tsBtnFontIncrease_Click(object sender, EventArgs e)
        {
            float size = this.baseSize + 1;
            if (size <= 1638)
            {                
                this.baseSize = size;
                this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                this.richTextBox.SelectionFont = this.currFont;
                this.tsComboBoxFontSizeChoice.Text = this.baseSize.ToString();
            }
            else
            {
                MessageBox.Show("Liczba musi zawierać się miedzy 1 a 1638");
            }
        }
        #endregion
        #region Przycisk "Zmniejsz rozmiar czcionki"
        private void tsBtnFontDecrease_Click(object sender, EventArgs e)
        {
            float size = this.baseSize - 1;
            if (size >= 1)
            {                
                this.baseSize = size;
                this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                this.richTextBox.SelectionFont = this.currFont;
                this.tsComboBoxFontSizeChoice.Text = this.baseSize.ToString();
            }
            else
            {
                MessageBox.Show("Liczba musi zawierać się miedzy 1 a 1638");
            }
        }
        #endregion

        #region Otwarcie palety kolorów (czcionka)
        private void tsBtnFontColor_DropDownOpening(object sender, EventArgs e)
        {
            this.paletteFontColor.Visible = !this.paletteFontColor.Visible;
            if (this.paletteFontColor.Visible)
            {
                this.paletteFontColor.Focus();
                this.ClosingCount = 1;
            }
            else
                this.ClosingCount = 0;

        }
        #endregion
        #region Przycisk "Kolor czcionki"
        private void tsBtnFontColor_ButtonClick(object sender, EventArgs e)
        {          
            this.richTextBox.SelectionColor = this.tsBtnFontColor.BackColor;
        }
        #endregion
        #region paletteColorChanged
        private void paletteFontColor_ColorChanged()
        {
            if (this.paletteFontColor.RecentColor.R == 0 && paletteFontColor.RecentColor.G == 0 && paletteFontColor.RecentColor.B == 0)
                this.tsBtnFontColor.BackColor = Color.FromArgb(210, 0, 0, 0);
            else
                this.tsBtnFontColor.BackColor = this.paletteFontColor.RecentColor;

            this.richTextBox.SelectionColor = this.tsBtnFontColor.BackColor;
            this.richTextBox.Focus();
        }
        #endregion

        #region Otwarcie palety kolorów wyróżnienia tekstu
        private void tsBtnFontBackColor_DropDownOpening(object sender, EventArgs e)
        {
            this.paletteBackColor.Visible = !this.paletteBackColor.Visible;
            if (this.paletteBackColor.Visible)
            {
                this.paletteBackColor.Focus();
                this.ClosingCount1 = 1;
            }
            else
                this.ClosingCount1 = 0;
        }

   
        #endregion
        #region Przycisk "Wyróżnienie tekstu"
        private void tsBtnFontBackColor_ButtonClick(object sender, EventArgs e)
        {
            this.richTextBox.SelectionBackColor = SystemColors.Control;            
            this.richTextBox.SelectionBackColor = this.tsBtnFontBackColor.BackColor;
        }

        #endregion
        #region paletteBackColorChanged
        private void PaletteBackColor_ColorChanged()
        {
            if (this.paletteBackColor.RecentColor.R == 0 && paletteBackColor.RecentColor.G == 0 && paletteBackColor.RecentColor.B == 0)
                this.tsBtnFontBackColor.BackColor = Color.FromArgb(210, 0, 0, 0);
            else
                this.tsBtnFontBackColor.BackColor = this.paletteBackColor.RecentColor;

            this.richTextBox.SelectionBackColor = this.tsBtnFontBackColor.BackColor;
            this.richTextBox.Focus();
        }
        #endregion

        #region Rozmiar czcionki (ComboBox) -> DropDownClosed
        private void tsComboBoxFontChoice_DropDownClosed(object sender, EventArgs e)
        {
            float size = 0;
            string text = (string)this.tsComboBoxFontSizeChoice.SelectedItem;
            bool isOk = float.TryParse(text, out size);
            if (this.tsComboBoxFontSizeChoice.SelectedItem != null)
            {
                this.isFromDropDownClosing = true;
                if (!isOk)
                {
                    MessageBox.Show("Nieprawidłowa liczba!");
                }
                else if (size <= 0 || size > 1638)
                {
                    MessageBox.Show("Liczba musi zawierać się między 1 a 1638");
                }
                else
                {
                    this.baseSize = size;
                    this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                    this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                    if (text != this.lastWrittenInFontSizeChoice)                      
                    this.richTextBox.SelectionFont = this.currFont;
                    this.richTextBox.Focus();
                }
            }
            else
            {
                this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                this.richTextBox.Focus();
            }
        }
        #endregion
        #region Rozmiar czcionki (ComboBox) -> Leave
        private void tsComboBoxFontChoice_Leave(object sender, EventArgs e)
        {
            if (!this.isFromDropDownClosing)
            {
                float size = 0;
                string text = ((ToolStripComboBox)sender).Text;
                bool isOk = float.TryParse(text, out size);
                if (!isOk)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Nieprawidłowa liczba!");
                    this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                    this.richTextBox.Focus();
                }
                else if (size <= 0 || size > 1638)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Liczba musi zawierać się między 1 a 1638");
                    this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                    this.richTextBox.Focus();
                }
                else
                {
                    this.baseSize = size;
                    this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                    this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                    if(text!= this.lastWrittenInFontSizeChoice)                     
                    this.richTextBox.Focus();
                    this.richTextBox.SelectionFont = this.currFont;
                }
            }
            this.isFromDropDownClosing = false;
        }
        #endregion
        #region Rozmiar czcionki (ComboBox) -> Naciśnięcie Entera
        private void tsComboBoxFontChoice_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter)
                {
                    this.isFromDropDownClosing = true;
                    float size = 0;
                    string text = ((ToolStripComboBox)sender).Text;
                    bool isOk = float.TryParse(text, out size);
                    if (!isOk)
                    {
                        MessageBox.Show("Nieprawidłowa liczba!");
                        this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                        this.richTextBox.Focus();
                    }
                    else if (size <= 0 || size > 1638)
                    {
                        MessageBox.Show("Liczba musi zawierać się między 1 a 1638");
                        this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                        this.richTextBox.Focus();
                    }
                    else
                    {
                        this.tsComboBoxFontSizeChoice.Text = text;
                        this.baseSize = size;
                        this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                        this.tsComboBoxFontSizeChoice.Text = ((int)this.baseSize).ToString();
                        if (text != this.lastWrittenInFontSizeChoice)                          
                        this.richTextBox.Focus();
                        this.richTextBox.SelectionFont = this.currFont;
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
        }
        #endregion

        #region Nazwa czcionki (ComboBox) -> DropDownClosed
        private void tsComboBoxFontChoice_DropDownClosed_1(object sender, EventArgs e)
        {
            if (!this.isEscClicked)
            {
                object selectedFontFamily = this.tsComboBoxFontChoice.SelectedItem;
                if (selectedFontFamily != null)
                {
                    this.isFromDropDownClosing2 = true;
                    this.currFontFamily = new FontFamily((string)selectedFontFamily);
                    if (this.currFontFamily.Name != this.lastWrittenInFontChoice)                      
                    this.richTextBox.SelectionFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                    this.richTextBox.Focus();
                }
                else
                {
                    this.tsComboBoxFontChoice.Text = this.currFontFamily.Name;
                    this.richTextBox.Focus();
                }
            }
            this.isEscClicked = false;
        }
        #endregion
        #region Nazwa czcionki (ComboBox) -> Leave
        private void tsComboBoxFontChoice_Leave_1(object sender, EventArgs e)
        {
            if (!this.isFromDropDownClosing2)
            {
                FontFamily tempFamily = null;
                bool isFound = false;
                try
                {
                    tempFamily = new FontFamily(this.tsComboBoxFontChoice.Text);
                    for (int i = 0; i < this.systemFonts.Families.Length; i++)
                    {
                        if (tempFamily.Name == this.systemFonts.Families[i].Name)
                        {
                            isFound = true;
                            break;
                        }
                    }
                }
                catch (ArgumentException)
                {
                    isFound = false;
                    SystemSounds.Beep.Play();
                    MessageBox.Show(string.Format("Czcionka \"{0}\" nie jest dostępna w systemie!",
                        this.tsComboBoxFontChoice.Text));
                    this.tsComboBoxFontChoice.Text = this.currFontFamily.Name;
                    this.richTextBox.Focus();
                }
                if (isFound)
                {
                    if (this.lastWrittenInFontChoice != this.tsComboBoxFontChoice.Text)                      
                    this.currFontFamily = tempFamily;
                    this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                    this.richTextBox.SelectionFont = this.currFont;
                    this.richTextBox.Focus();
                }
            }
            this.isFromDropDownClosing2 = false;
            this.tsComboBoxFontChoice.Text = this.currFontFamily.Name;
        }
        #endregion
        #region Nazwa czcionki (ComboBox) -> Naciśnięcie Entera
        private void tsComboBoxFontChoice_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            
            {
                this.isFromDropDownClosing2 = true;
                FontFamily tempFamily = null;
                bool isFound = false;
                try
                {
                    tempFamily = new FontFamily(this.tsComboBoxFontChoice.Text);
                    for (int i = 0; i < this.systemFonts.Families.Length; i++)
                    {
                        if (tempFamily.Name == this.systemFonts.Families[i].Name)
                        {
                            isFound = true;
                            break;
                        }
                    }
                }
                catch (ArgumentException)
                {
                    isFound = false;
                    SystemSounds.Beep.Play();
                    MessageBox.Show(string.Format("Czcionka \"{0}\" nie jest dostępna w systemie!",
                        this.tsComboBoxFontChoice.Text));
                    this.tsComboBoxFontChoice.Text = this.currFontFamily.Name;
                    this.richTextBox.Focus();
                }
                if (isFound)
                {
                    this.currFontFamily = tempFamily;
                    this.currFont = new Font(this.currFontFamily, this.baseSize, this.currStyle);
                    this.richTextBox.SelectionFont = this.currFont;
                    if (this.lastWrittenInFontChoice != this.tsComboBoxFontChoice.Text)                       
                    this.richTextBox.Focus();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }
        #endregion
        #region Podpowiadanie nazwy czcionki (ComboBox)
        private void tsComboBoxFontChoice_TextUpdate(object sender, EventArgs e)
        {
            if (!this.isBackClicked) //jeśli nie został wciśnięty Backspace (sprawdzamy w ProcessCmdKey)
            {
                ToolStripComboBox box = sender as ToolStripComboBox;
                if (box != null)
                {
                    string oldText = box.Text;
                    string suggestedText = this.suggestedFontName(oldText); //funkcja wyszukiwania sugestii czcionki

                    box.Text = suggestedText;
                    box.Select(oldText.Length, suggestedText.Length - oldText.Length);
                }
            }
            else
            {
                this.isBackClicked = false;
            }
        }

        private string suggestedFontName(string prefix)
        {
            //wykorzystujemy zapytania lambda !
            string result = this.systemFonts.Families
                .Select(font => font.Name)
                .Where(name => name.StartsWith(prefix))
                .OrderBy(name => name.Length)
                .ThenBy(name => name)
                .FirstOrDefault();

            return string.IsNullOrEmpty(result) ? prefix : result;
        }
        #endregion
        #region Zamiana pierwszej wprowadzonej litery na dużą
        private void tsComboBoxFontChoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((this.tsComboBoxFontChoice.Text.Length == 0 ||
                this.tsComboBoxFontChoice.SelectionLength == this.tsComboBoxFontChoice.Text.Length)
                && char.IsLower(e.KeyChar))
            {
                char chr = char.ToUpper(e.KeyChar);
                e.Handled = true;
                this.tsComboBoxFontChoice.SelectedText = chr.ToString();
            }
        }
        #endregion

        #region Przycisk Undo
        private void tsUndo_Click(object sender, EventArgs e)
        {
            this.richTextBox.Undo();
            if (!this.richTextBox.CanUndo)
            {
                this.tsUndo.Enabled = false;
                this.tsMenuEditUndo.Enabled = false;
            }

            if(this.richTextBox.CanRedo)
            {
                this.tsRedo.Enabled = true;
                this.tsMenuEditRedo.Enabled = true;
            }
            //uaktualnienie czcionki
            #region Styl
            this.currStyle = this.richTextBox.SelectionFont.Style;

            //wykryj istniejące style i uaktywnij odpowiednie przyciski 
            if (this.currStyle.ToString().IndexOf("Bold") != -1)
                this.tsBtnBold.Checked = true;
            else
                this.tsBtnBold.Checked = false;

            if (this.currStyle.ToString().IndexOf("Italic") != -1)
                this.tsBtnItalic.Checked = true;
            else
                this.tsBtnItalic.Checked = false;

            if (this.currStyle.ToString().IndexOf("Underline") != -1)
                this.tsBtnUnderline.Checked = true;
            else
                this.tsBtnUnderline.Checked = false;

            if (this.currStyle.ToString().IndexOf("Strikeout") != -1)
                this.tsBtnStrikeout.Checked = true;
            else
                this.tsBtnStrikeout.Checked = false;
            #endregion

            #region Indeks górny/dolny
            if (this.richTextBox.SelectionCharOffset > 0)
                this.tsBtnSuperscript.Checked = true;
            else
                this.tsBtnSuperscript.Checked = false;

            if (this.richTextBox.SelectionCharOffset < 0)
                this.tsBtnSubscript.Checked = true;
            else
                this.tsBtnSubscript.Checked = false;
            #endregion

            #region Rozmiar czcionki
            this.tsComboBoxFontSizeChoice.Text = ((int)this.richTextBox.SelectionFont.Size).ToString();
            #endregion
        }
        #endregion
        #region Przycisk Redo / Repeat
        private void tsRedo_Click(object sender, EventArgs e)
        {

            this.richTextBox.Redo();
            if (!this.richTextBox.CanRedo)
            {
                this.tsRedo.Enabled = false;
                this.tsMenuEditRedo.Enabled = false;
            }
            if (this.richTextBox.CanUndo)
            {
                this.tsUndo.Enabled = true;
                this.tsMenuEditUndo.Enabled = true;
            }

            //uaktualnienie czcionki
            #region Styl
            this.currStyle = this.richTextBox.SelectionFont.Style;

                //wykryj istniejące style i uaktywnij odpowiednie przyciski 
                if (this.currStyle.ToString().IndexOf("Bold") != -1)
                    this.tsBtnBold.Checked = true;
                else
                    this.tsBtnBold.Checked = false;

                if (this.currStyle.ToString().IndexOf("Italic") != -1)
                    this.tsBtnItalic.Checked = true;
                else
                    this.tsBtnItalic.Checked = false;

                if (this.currStyle.ToString().IndexOf("Underline") != -1)
                    this.tsBtnUnderline.Checked = true;
                else
                    this.tsBtnUnderline.Checked = false;

                if (this.currStyle.ToString().IndexOf("Strikeout") != -1)
                    this.tsBtnStrikeout.Checked = true;
                else
                    this.tsBtnStrikeout.Checked = false;
                #endregion

                #region Indeks górny/dolny
                if (this.richTextBox.SelectionCharOffset > 0)
                    this.tsBtnSuperscript.Checked = true;
                else
                    this.tsBtnSuperscript.Checked = false;

                if (this.richTextBox.SelectionCharOffset < 0)
                    this.tsBtnSubscript.Checked = true;
                else
                    this.tsBtnSubscript.Checked = false;
                #endregion

                #region Rozmiar czcionki
                this.tsComboBoxFontSizeChoice.Text = ((int)this.richTextBox.SelectionFont.Size).ToString();
                #endregion
        }
        #endregion

        #region Przycisk "Wytnij"
        private void pn1_BtnCut_Click(object sender, EventArgs e)
        {
            if (this.documentExt == ".rtf")
            {               
                Clipboard.SetText(this.richTextBox.SelectedRtf, TextDataFormat.Rtf);
                this.richTextBox.SelectedRtf = "";
                this.richTextBox.Focus();
                this.pn1_BtnKopiuj.Enabled = false;
                this.pn1_BtnCut.Enabled = false;
                this.pn1_BtnPaste.Enabled = true;
                this.tsMenuEditCopy.Enabled = false;
                this.tsMenuEditCut.Enabled = false;
                this.tsMenuEditPaste.Enabled = true;
            }
            else
            {               
                Clipboard.SetText(this.richTextBox.SelectedText, TextDataFormat.Text);
                this.richTextBox.SelectedText = "";
                this.richTextBox.Focus();
                this.pn1_BtnKopiuj.Enabled = false;
                this.pn1_BtnCut.Enabled = false;
                this.pn1_BtnPaste.Enabled = true;
                this.tsMenuEditCopy.Enabled = false;
                this.tsMenuEditCut.Enabled = false;
                this.tsMenuEditPaste.Enabled = true;
            }
        }
        #endregion
        #region Przycisk "Kopiuj"
        private void pn1_BtnKopiuj_Click(object sender, EventArgs e)
        {
            if (this.documentExt == ".rtf")
            {             
                Clipboard.SetText(this.richTextBox.SelectedRtf, TextDataFormat.Rtf);
                this.pn1_BtnPaste.Enabled = true;
            }
            else
            {                
                Clipboard.SetText(this.richTextBox.SelectedText, TextDataFormat.Text);
                this.pn1_BtnPaste.Enabled = true;
            }
        }
        #endregion
        #region Przycisk "Wklej"
        private void pn1_BtnPaste_Click(object sender, EventArgs e)
        {
            if (this.documentExt == ".rtf")
            {
                this.richTextBox.Paste();
                this.richTextBox.Focus();
            }
            else
            {                
                this.richTextBox.SelectedText = Clipboard.GetText(TextDataFormat.Text);
                this.richTextBox.Focus();
            }
        }
        #endregion

        #region Przyciski wyrównania tekstu
        private void tsBtnLeftAllignement_Click(object sender, EventArgs e)
        {
            this.AlignmentTempButton.Checked = false;
            this.AlignmentTempButton = this.tsBtnLeftAllignement;
            this.AlignmentTempButton.Checked = true;
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void tsBtnCenterAllignment_Click(object sender, EventArgs e)
        {
            this.AlignmentTempButton.Checked = false;
            this.AlignmentTempButton = this.tsBtnCenterAllignment;
            this.AlignmentTempButton.Checked = true;
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void tsBtnRightAlignment_Click(object sender, EventArgs e)
        {
            this.AlignmentTempButton.Checked = false;
            this.AlignmentTempButton = this.tsBtnRightAlignment;
            this.AlignmentTempButton.Checked = true;
            this.richTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }
        #endregion
        #region Przycisk "Obraz"
        private void BtnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog newOpenFileDialog = new OpenFileDialog();
            string filter;
            bool isCorrectFile = false;

            filter = newOpenFileDialog.Filter = "BMP(*.BMP;*.DIB;*.RLE)|*.bmp;*.dib;*.rle|JPEG(*.JPG;*.JPEG;*.JPE;*.JFIF)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF(*.GIF)|*.gif|EMF(*.EMF)|*.emf|WMF(*.WMF)|*.wmf|TIFF(*.TIFF)|*.tiff|PNG(*.PNG)|*.png|ICO(*.ICO)|*.ico|Wszystkie pliki obrazów (*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.EMF;*.WMF;*.TIFF;*.PNG;*.ICO)|*.bmp;*.dib;*.rle;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif;*.emf;*.wmf;*.tiff;*.png;*.ico|AllFiles(*.*)|*.*";
            newOpenFileDialog.FilterIndex = 9;
            newOpenFileDialog.Title = "Wybierz obraz...";
            newOpenFileDialog.Multiselect = false;

            do
            {
                if (newOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (newOpenFileDialog.FileName != null)
                    {
                        if (filter.IndexOf(Path.GetExtension(newOpenFileDialog.FileName)) != -1)
                        {
                            //"work around" dla braku wsparcia dla obiektów OLE w RichTextFormat przez .NET (Windows Forms)
                            //nie działa dla wszystkich wersji systemów operacyjnych
                            IDataObject obj = Clipboard.GetDataObject();
                            Clipboard.Clear();

                            Clipboard.SetImage(Image.FromFile(Path.GetFullPath(newOpenFileDialog.FileName)));
                            this.richTextBox.Paste();

                            //Undo/Redo
                            this.tsUndo.Enabled = true;
                            this.tsMenuEditUndo.Enabled = true;
                            this.tsRedo.Enabled = false;
                            this.tsMenuEditRedo.Enabled = false;

                            try
                            {
                                Clipboard.Clear();
                                Clipboard.SetDataObject(obj);
                            }
                            catch(Exception)
                            {
                                this.tsLabelState.Text = "Brak obsługi obiektów OLE dla RTF. "
                                    + "Zawartość schowka została utracona (alternatywna metoda nie powiodła się- przyczyna: wersja systemu operacyjnego)";
                            }

                            isCorrectFile = true;
                            this.tsLabelState.Text = string.Format("Wklejono obraz {0}",
                            Path.GetFileName(newOpenFileDialog.FileName));
                            this.richTextBox.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Nieprawidłowy format pliku!");
                        }
                    }
                }
                else
                {
                    this.tsLabelState.Text = "Anulowano wklejanie obrazu.";
                    isCorrectFile = true;
                }
            } while (isCorrectFile == false);
        }
        #endregion
        #region Przycisk "Data/godz.
        private void date_time_Click(object sender, EventArgs e)
        {
            dtMod = new DateTimeMod();

            if (this.dtMod.ShowDialog() == DialogResult.OK)
            {
                this.richTextBox.SelectedText = dtMod.ChosenDateTimeFormat;
                //Undo
                this.tsUndo.Enabled = true;
                this.tsMenuEditUndo.Enabled = true;
                this.tsRedo.Enabled = false;
                this.tsMenuEditRedo.Enabled = false;

                this.richTextBox.Focus();
            }
        }
        #endregion

        //MENU GŁÓWNE
        #region "Plik" -> Zapisz jako...
        private void tsMenuFileSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog mySaveDialog = new SaveFileDialog();
            mySaveDialog.InitialDirectory = Directory.GetCurrentDirectory();
            mySaveDialog.Filter = "RichTextFile(*.rtf)|*.rtf|TextFile(*.txt)|*.txt";
            mySaveDialog.FilterIndex = 1;
            mySaveDialog.RestoreDirectory = true;
            mySaveDialog.FileName = this.documentTitle;
            DialogResult dr;

            if ((dr = mySaveDialog.ShowDialog()) == DialogResult.OK)
            {
                string fileExt = Path.GetExtension(mySaveDialog.FileName);

                //sprawdź próbę zapisania grafiki w pliku tekstowym
                if(this.richTextBox.Rtf.Contains(@"\pict") && fileExt==".txt")
                {
                    DialogResult dr2 = MessageBox.Show(string.Format("Wykryto grafikę (typ pliku: tekstowy)."
                        + "Nie zostanie ona zapisana.\n Kontynuować mimo to?"), "Ostrzeżenie",
                        MessageBoxButtons.YesNo);
                    if (dr2 == DialogResult.No)
                    {
                        this.tsLabelState.Text = "Anulowano zapisywanie zmian w pliku.";
                        return;
                    }
                }

                Stream myStream;
                if ((myStream = mySaveDialog.OpenFile()) != null)
                {
                    if (fileExt == ".txt")
                    {
                        //działam w ten sposób, ponieważ metoda SaveFile() dla RichTextBox.PlainText
                        //zapisuje tak, że przy odczycie nie ma polskich znaków
                        string plainText = this.richTextBox.Text;
                        StreamWriter textWriter = new StreamWriter(myStream);
                        textWriter.WriteLine(this.richTextBox.Text);
                        textWriter.Dispose();
                        myStream.Dispose();
                        this.baseText = this.richTextBox.Text;
                        this.documentTitle = Path.GetFileNameWithoutExtension(mySaveDialog.FileName);
                        this.documentExt = Path.GetExtension(mySaveDialog.FileName);
                        this.currentOpenDocument = Path.GetFileName(mySaveDialog.FileName); //ostatnio zapisany dokument
                        this.currentPathToOpenDocument = mySaveDialog.FileName; //ścieżka ostatnio zapisanego dokumentu 
                        this.isFontStyleChanged = false; //zresteuj styl czcionki (odniesienie)

                        //dezaktywacja funkcji RTF
                        this.toolStripFont.Enabled = false;
                        this.BtnImage.Enabled = false;
                    }
                    else if (fileExt == ".rtf")
                    {
                        this.richTextBox.SaveFile(myStream, RichTextBoxStreamType.RichText);
                        myStream.Dispose();
                        this.baseText = this.richTextBox.Text;
                        this.documentTitle = Path.GetFileNameWithoutExtension(mySaveDialog.FileName);
                        this.documentExt = Path.GetExtension(mySaveDialog.FileName);
                        this.currentOpenDocument = Path.GetFileName(mySaveDialog.FileName); //ostatnio zapisany dokument
                        this.currentPathToOpenDocument = mySaveDialog.FileName; //ścieżka ostatnio zapisanego dokumentu 
                        this.isFontStyleChanged = false; //zresteuj styl czcionki (odniesienie)

                        //aktywacja funkcji RTF
                        this.toolStripFont.Enabled = true;
                        this.BtnImage.Enabled = true;
                    }
                    this.canNewFile = true; //wybrano plik, opcja NewFile może wykonać operacje
                    this.Text = Path.GetFileName(mySaveDialog.FileName) + " - " + Application.ProductName;
                    this.tsLabelState.Text = "Zapisano zmiany w pliku " + this.Text;
                    myStream.Dispose();
                }
            }
            else if (dr == DialogResult.Cancel)
            {
                this.tsLabelState.Text = "Anulowano zapisywanie zmian pliku " + this.Text;
                this.canNewFile = false; //zrezygnowano z wyboru pliku, opcja NewFile nie może wykonać operacji
            }
        }
        #endregion
        #region "Plik" - reszta
        private void tsMenuFileNewRtf_Click(object sender, EventArgs e)
        {
            this.tsBtnNewFile.PerformClick();
        }

        private void otwórzPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tsBtnOpenFile.PerformClick();
        }

        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tsBtnPrint.PerformClick();
        }

        private void wyjścieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region "Edycja"
        private void tsMenuEditUndo_Click(object sender, EventArgs e)
        {
            this.tsUndo.PerformClick();
        }

        private void tsMenuEditRedo_Click(object sender, EventArgs e)
        {
            this.tsRedo.PerformClick();
        }

        private void tsMenuEditCut_Click(object sender, EventArgs e)
        {
            this.pn1_BtnCut.PerformClick();
        }

        private void tsMenuEditCopy_Click(object sender, EventArgs e)
        {
            this.pn1_BtnKopiuj.PerformClick();
        }

        private void tsMenuEditPaste_Click(object sender, EventArgs e)
        {
            this.pn1_BtnPaste.PerformClick();
        }

        private void tsMenuEditPasteImage_Click(object sender, EventArgs e)
        {
            this.BtnImage.PerformClick();
        }

        private void tsMenuEditPasteDataFormat_Click(object sender, EventArgs e)
        {
            this.date_time.PerformClick();
        }
        #endregion
        #region "Formatuj"
        private void tsMenuFormatAlignLeft_Click(object sender, EventArgs e)
        {
            this.tsBtnLeftAllignement.PerformClick();
        }

        private void tsMenuFormatAlignCenter_Click(object sender, EventArgs e)
        {
            this.tsBtnCenterAllignment.PerformClick();
        }

        private void tsMenuFormatAlignRight_Click(object sender, EventArgs e)
        {
            this.tsBtnRightAlignment.PerformClick();
        }
        #endregion
        #region "Narzędzia"
        private void tsMenuToolsFind_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^F");
        }
        #endregion
        #region "Pomoc"
        private void tsMenuHelpAbout_Click(object sender, EventArgs e)
        {
            Info about = new Info();

            about.ShowDialog();
        }

        private void tsMenuHelpDocumentation_Click(object sender, EventArgs e)
        {
            Documentation usr = new Documentation();
            usr.UseRtf = true;
            usr.RtfContent = Properties.Resources.user;
            usr.TiteLabel = "Wersja programu 1.0";
            usr.Show();
        }

        private void dlaProgramistyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Documentation prg = new Documentation();
            prg.UseRtf = true;
            prg.RtfContent = Properties.Resources.programmer;
            prg.TiteLabel = "Wersja programu 1.0";
            prg.Show();
        }
        #endregion
    }
}
