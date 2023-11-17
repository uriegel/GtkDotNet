
using System;
using System.Runtime.InteropServices;
using GtkDotNet;

public static class SelectionData
{
    [DllImport(Globals.LibGtk, EntryPoint = "gtk_selection_data_set_uris", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr DataSetUris(this IntPtr data, string[] uris);
}