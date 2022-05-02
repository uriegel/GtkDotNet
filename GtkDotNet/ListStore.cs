using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GtkDotNet;

public class ListStore
{
    [DllImport(Globals.LibGtk, EntryPoint="g_list_store_new", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr New(long type);

    [DllImport(Globals.LibGtk, EntryPoint="g_list_store_new", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr New(GType type);

    [DllImport(Globals.LibGtk, EntryPoint="g_list_model_get_object", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr GetObject(IntPtr store, int position);

    public static void Splice<T>(IntPtr listStore, IEnumerable<T> additions)
    {
        var items = additions.Select(n => GManaged<T>.New(n)).ToArray();
        Splice(listStore, 0, 0, items, items.Length);
        for (var i = 0; i < items.Length; i++)
            GObject.Unref(items[i]);    
    }

    [DllImport(Globals.LibGtk, EntryPoint="g_list_store_remove_all", CallingConvention = CallingConvention.Cdecl)]
    public extern static void RemoveAll(IntPtr store);

    [DllImport(Globals.LibGtk, EntryPoint="g_list_store_splice", CallingConvention = CallingConvention.Cdecl)]
    extern static void Splice(IntPtr store, int position, int countRemovals, 
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)][In] IntPtr[] additions, int countAdditions);
}

