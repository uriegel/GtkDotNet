using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw;

public class CellRendererText
{
    [DllImport(Globals.LibGtk, EntryPoint = "gtk_cell_renderer_text_new", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr New();
}
