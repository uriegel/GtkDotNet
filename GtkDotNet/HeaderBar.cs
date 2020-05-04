using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class HeaderBar
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_header_bar_set_subtitle", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetSubtitle(IntPtr headerBar, string subtitle);
    }
}