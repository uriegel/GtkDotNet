using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class ListStore
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_list_store_newv", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(int columns, GType[] types);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_list_store_insert_with_valuesv", CallingConvention = CallingConvention.Cdecl)]
        public extern static void InsertWithValues(IntPtr listStore, IntPtr zero, int position, int[] columns, IntPtr values, int count);
        
    }
}

