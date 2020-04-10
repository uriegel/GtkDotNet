using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ConnectDelegate(IntPtr builder, IntPtr obj, string signal, string handleName, IntPtr connectObj, int flags);

    public class Builder
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_builder_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New();

        [DllImport(Globals.LibGtk, EntryPoint="gtk_builder_add_from_file", CallingConvention = CallingConvention.Cdecl)]
        public extern static int AddFromFile(IntPtr builder, string file, IntPtr nil);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_builder_get_object", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetObject(IntPtr builder, string objectName);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_builder_connect_signals_full", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr ConnectSignals(IntPtr builder, ConnectDelegate onConnection);
    }
}
