using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Listbox
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_list_box_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New();

        [DllImport(Globals.LibGtk, EntryPoint="gtk_list_box_prepend", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Prepend(IntPtr listbox, IntPtr widget);
    }
}
