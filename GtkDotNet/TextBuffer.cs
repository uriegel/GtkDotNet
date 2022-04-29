using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

[StructLayout(LayoutKind.Sequential)]
public struct GtkTextIter 
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=80)] 
    byte[] phantom; 
}

public class TextBuffer
{
    [DllImport(Globals.LibGtk, EntryPoint="gtk_text_buffer_set_text", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetText(IntPtr buffer, IntPtr content, int size);

    [DllImport(Globals.LibGtk, EntryPoint="gtk_text_buffer_create_tag", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr CreateTag(IntPtr buffer, string name = null, string firstProppertyName = null);

    [DllImport(Globals.LibGtk, EntryPoint="gtk_text_buffer_apply_tag", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr ApplyTag(IntPtr buffer, IntPtr tag, ref GtkTextIter startIter, ref GtkTextIter endIter);

    [DllImport(Globals.LibGtk, EntryPoint="gtk_text_buffer_get_start_iter", CallingConvention = CallingConvention.Cdecl)]
    public extern static void GetStartIter(IntPtr buffer, out GtkTextIter startIter);

    [DllImport(Globals.LibGtk, EntryPoint="gtk_text_buffer_get_end_iter", CallingConvention = CallingConvention.Cdecl)]
    public extern static void GetEndIter(IntPtr buffer, out GtkTextIter endIter);
}
