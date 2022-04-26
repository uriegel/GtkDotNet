using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class ListView
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_list_view_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(IntPtr selectionModel, IntPtr itemFactory);
    }
}
