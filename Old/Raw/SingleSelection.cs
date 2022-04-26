using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw;

public class SingleSelection
{
    [DllImport(Globals.LibGtk, EntryPoint="gtk_single_selection_new", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr New(IntPtr listModel);
}

