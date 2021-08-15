using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Window
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(WindowType windowType);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_default_size", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetDefaultSize(IntPtr window, int width, int height);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_move", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Move(IntPtr window, int x, int y);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_title", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetTitle(IntPtr window, string title);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_get_size", CallingConvention = CallingConvention.Cdecl)]
        public extern static void GetSize(IntPtr window, out int width, out int height);        
    }
}
 

