using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class ColumnViewColumn
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_column_view_column_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(string title, IntPtr listItemFactory);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_column_view_column_set_resizable", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SetResizable(IntPtr column, bool resizable);
    }
}