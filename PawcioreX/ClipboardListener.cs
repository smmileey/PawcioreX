using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PawcioreX
{
    public class ClipboardListener
    {
        //importujemy funkcję zewnętrzną, pozwala ona umieścić dane okno w zarządzanej systemowo 
        //liście nasłuchiwania zmian zawartości schowka

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        //zewnętrzna funkcja, do usunięcia nasłuchiwania zdarzeń zmian zawartości w schowku
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        public bool StartClipboardListener(IntPtr command)
        {
            return AddClipboardFormatListener(command);
        }

        public bool RemoveClipboardListener(IntPtr command)
        {
            return RemoveClipboardFormatListener(command);
        }

        
    }
}
