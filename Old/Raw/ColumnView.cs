using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class ColumnView
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_column_view_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(IntPtr selectionModel);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_column_view_append_column", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr AppendColumn(IntPtr columnView, IntPtr columnViewColumn);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_column_view_set_reorderable", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SetReorderable(IntPtr columnView, bool reorderable);
    }
}
