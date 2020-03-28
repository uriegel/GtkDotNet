using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class GObject
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_object_unref", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Unref(IntPtr obj);
    }
}

