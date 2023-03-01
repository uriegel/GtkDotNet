using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw;

public class TargetList
{
    [DllImport(Globals.LibGtk, EntryPoint = "gtk_target_list_new", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr New(IntPtr targetEntries, int count);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_target_list_unref", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr Unref(IntPtr list);
}

