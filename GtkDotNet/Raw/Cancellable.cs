using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw;

public static class Cancellable
{
    [DllImport(Globals.LibGio, EntryPoint = "g_cancellable_new", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr New();

    [DllImport(Globals.LibGio, EntryPoint = "g_cancellable_cancel", CallingConvention = CallingConvention.Cdecl)]
    public extern static void Cancel(IntPtr handle);
}