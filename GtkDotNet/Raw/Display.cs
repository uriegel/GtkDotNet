using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Display
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gdk_display_get_default", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetDefault();

        [DllImport(Globals.LibGtk, EntryPoint = "gdk_display_get_default_screen", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetDefaultScreen(IntPtr display);
    }
}
