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

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_get_position", CallingConvention = CallingConvention.Cdecl)]
        public extern static void GetPosition(IntPtr window, out int x, out int y);
        
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_close", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Close(IntPtr window);
        
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_title", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetTitle(IntPtr window, string title);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_get_size", CallingConvention = CallingConvention.Cdecl)]
        public extern static void GetSize(IntPtr window, out int width, out int height);        

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_maximize", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Maximize(IntPtr window);        

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_is_maximized", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool IsMaximized(IntPtr window);        
        
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_icon", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SetIcon(IntPtr window, IntPtr pixbuf);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_child", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SetChild(IntPtr window, IntPtr child);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_application", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetApplication(IntPtr window, IntPtr application);
    }
}
 

