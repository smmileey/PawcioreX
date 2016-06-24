using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawcioreX
{
    //komunikaty nasłuchiwania zdarzeń zmian zawartości schowka
    public enum ClipboardOption
    {
        WM_CLIPBOARDUPDATE = 0x031D
    }

    //parametry widoczności przycisków w niemodalnym oknie wyszukiwania fraz
    public enum SearchVisibilityParameters
    {
        NEXT_NOT_VISIBLE,
        NEXT_ENABLED_VISIBLE,
        NEXT_NOT_ENABLED,
        PREVIOUS_NOT_VISIBLE,
        PREVIOUS_ENABLED_VISIBLE,
        PREVOIUS_NOT_ENABLED,
        FIRST_AND_LAST_NOT_VISIBLE,
        FIRST_AND_LAST_ENABLED_VISIBLE,
        FIRST_AND_LAST_NOT_ENABLED,
        FIRST_ENABLED_LAST_NOT_ENABLED,
        FIRST_NOT_ENABLED_LAST_ENABLED,
        FIRST_ENABLED,
        LAST_ENABLED
    }

}
