using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Window
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(WindowType windowType);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_title", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetTitle(IntPtr window, string title);
    }
}
 

