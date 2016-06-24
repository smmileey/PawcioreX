using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing.Printing;

namespace PawcioreX
{
    /*                                                      WSTĘP
     * w standardowym Rich Text Box'ie (RTB) jest pewien bug. Mianowicie, po ustawieniu własności AutoWordSelection na false
     * mimo tego podczas zaznaczania tekstu, zaznaczane są całe słowa (jeśli zaznaczenie obejmuje kilka wyrazów) zamiast
     * pojedyncze znaki. Dlatego trzeba dokonać pewnej "głupiej" modyfikacji, która zastosowano poniżej */
    public class FixedRichTextBox:RichTextBox
    {
        // nadpisanie funkcji bazowej OnHandleCreated. Jest to metoda, która bezpośrednio pozwala zająć się zdarzeniem
        // HandleCreated, bez przypisywania delegacji. Takie podejście jest preferowane, przy obsłudze zdarzeń w klasach
        // dziedziczących. Samo zdarzenie HandleCreated jest wywoływane, kiedy kontrolka jest pierwszy raz wyświetlana, czyli
        // gdy jej właściwość Visible jest ustawiona na true.
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!base.AutoWordSelection)
                base.AutoWordSelection = true;
            base.AutoWordSelection = false;
        }
        //http://stackoverflow.com/questions/3678620/c-sharp-richtextbox-selection-problem#comment3873783_3678620

        //Żeby podczas zaznaczania tekstu, widać było kolor poszczególnych znaków (a nie wszystkie litery białe)
        //należy użyć ostatniej, najnowszej wersji RichTextBox'a, czyli RichEdit50W, która nie jest domyślnie używaną.
        //(nie wiedzieć czemu). W tym celu, należy zaimportować z biblioteki systemowej "kernel32.dll" metodę 
        //"LoadLibraryW", która zwraca uchwyt do biblioteki w systemie (dostępne od Windows XP SP1). W tym celu,
        //importujemy metodę, pamiętając, że mamy do czynienia z kodem niezarządzanym
        [DllImport("kernel32.dll",EntryPoint="LoadLibraryW",CharSet=CharSet.Unicode,SetLastError=true)]
        private static extern IntPtr LoadLibraryW(string s_File);

        private static IntPtr LoadLibrary(string s_File)
        {
            IntPtr h_Module = LoadLibraryW(s_File);
            if(h_Module!=IntPtr.Zero)
                return h_Module;

            int s32_Error = Marshal.GetLastWin32Error();
            throw new Win32Exception(s32_Error);
        }

        //teraz pozostaje jeszcze przesłonięcie "CreateParams", czyli informacji, którę są wykorzystywane do stworzenia
        //kontrolki. Próbujemy zatem wczytać za pomocą stworzonej metody "LoadLibrary" bibliotekę "MsftEdit.dll"
        //zawierającą interesującą nas klasę "RichEdit50W". Ewentualne błędy (stara wersja Windowsa) wyłapujemy w bloku
        //catch. Teraz nasz FixedRichTextBox obsługuje już wyróżnianie poszczególnych kolorów czcionki podczas zaznaczania
        //tekstu!
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams i_Params = base.CreateParams;
                try
                {                 
                    LoadLibrary("MsftEdit.dll");
                    i_Params.ClassName = "RichEdit50W";
                }
                catch
                {
                   //WindowsXP errors
                }
            return i_Params;
            }
        }

        //struktura opisująca wymiary całej strony RTF
        private struct STRUCT_RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        
        //struktura określająca zakres druku
        private struct STRUCT_CHARRANGE
        {
            public int cpMin;
            public int cpMax;
        }

        //struktura opisująca wymiary druku
        private struct STRUCT_FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public STRUCT_RECT rc;
            public STRUCT_RECT rcPage;
            public STRUCT_CHARRANGE chrg;
        }

        //import API Win32 do obsługi druku
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        private const int WM_USER = 0x400;
        private const int EM_FORMATRANGE = WM_USER + 57;
 
        //metoda drukująca określoną ilość znaków na stronie
        public int FormatRange (bool measureOnly, PrintPageEventArgs e, int startChar, int stopChar)
        {
            //określ zakres druku
            STRUCT_CHARRANGE cr = default(STRUCT_CHARRANGE);
            cr.cpMin = startChar;
            cr.cpMax = stopChar;

            //określ wymiary strony
            STRUCT_RECT rcPage = default(STRUCT_RECT);
            rcPage.top = HundredInchToTwips(e.PageBounds.Top);
            rcPage.bottom = HundredInchToTwips(e.PageBounds.Bottom);
            rcPage.left = HundredInchToTwips(e.PageBounds.Left);
            rcPage.right = HundredInchToTwips(e.PageBounds.Right);

            //okreś margines druku
            STRUCT_RECT rc = default(STRUCT_RECT);
            rc.top = HundredInchToTwips(e.MarginBounds.Top);
            rc.bottom = HundredInchToTwips(e.MarginBounds.Bottom);
            rc.left = HundredInchToTwips(e.MarginBounds.Left);
            rc.right = HundredInchToTwips(e.MarginBounds.Right);

            //określ kontekst drukarki
            IntPtr hdc = default(IntPtr);
            hdc = e.Graphics.GetHdc();

            //określ strukturę druku
            STRUCT_FORMATRANGE fr = default(STRUCT_FORMATRANGE);
            fr.chrg = cr;
            fr.hdc = hdc;
            fr.hdcTarget = hdc;
            fr.rc = rc;
            fr.rcPage = rcPage;

            //renderuj/mierz znaki
            int wParam = default(int);
            if (measureOnly)
            {
                wParam = 0;
            }
            else
            {
                wParam = 1;
            }

            //zarezerwuj pamięć dla FORMATRANGE i skopiuj ją na stos
            IntPtr lParam = default(IntPtr);
            lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
            Marshal.StructureToPtr(fr, lParam, false);

            //wyślij polecenie drukowania- wiadomość Win32 
            int res = 0;
            res = SendMessage(Handle, EM_FORMATRANGE, wParam, lParam);

            //zwolnij ulokowaną pamięć i zwolnij uchwyt do kontekstu drukarki
            Marshal.FreeCoTaskMem(lParam);
            e.Graphics.ReleaseHdc(hdc);

            //zwróć indeks ostatniego znaku, który zmieścił się na stronie + 1
            return res;
        }

        //w .NET Framework jednostka to 1/100 cala, Win32 API używają jednostki 1/1440 cala
        //potrzebna zatem konwersja, wartość zwracana przez .NET jest w jednostce (1/(100 * n))
        public int HundredInchToTwips(int n)
        {
            return Convert.ToInt32(n * 14.4);
        }

        //po zakończeniu drukowania, należy zwolnić cache, przekazująć lParam jako 0
        //inaczej dojdzie do wycieku pamięci
        public void FormatRangeDone()
        {
            IntPtr lParam = new IntPtr(0);
            SendMessage(Handle, EM_FORMATRANGE, 0, lParam);
        }
    }
}
