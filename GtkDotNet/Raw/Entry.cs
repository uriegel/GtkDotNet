using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Entry
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_entry_set_text", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetText(IntPtr headerBar, string text);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_entry_get_text", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetText(IntPtr headerBar);
    }
}