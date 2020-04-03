using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Popover
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_popover_popup", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Popup(IntPtr popover);
    }
}

