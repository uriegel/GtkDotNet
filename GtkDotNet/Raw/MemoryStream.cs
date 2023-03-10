using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw;

public class MemoryStream
{
    [DllImport(Globals.LibGtk, EntryPoint = "g_memory_input_stream_new_from_data", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr NewInputStreamFromData(IntPtr data, int length, NotifyFree free);

    public delegate void NotifyFree(IntPtr request);
}

