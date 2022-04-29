using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

public class TextBuffer
{
    [DllImport(Globals.LibGtk, EntryPoint="gtk_text_buffer_set_text", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetText(IntPtr buffer, IntPtr content, int size);
}
