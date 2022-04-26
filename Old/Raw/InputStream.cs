using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class InputStream
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_input_stream_read", CallingConvention = CallingConvention.Cdecl)]
        public extern static long Read(IntPtr stream, IntPtr buffer, long count, IntPtr zero, IntPtr zero2);
    }
}