using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class ListStore
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_list_store_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(long type);

        [DllImport(Globals.LibGtk, EntryPoint="g_list_store_splice", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Splice(IntPtr store, int position, int countRemovals, 
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)][In] IntPtr[] additions, int countAdditions);

        [DllImport(Globals.LibGtk, EntryPoint="g_list_model_get_object", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetObject(IntPtr store, int position);
    }
}
