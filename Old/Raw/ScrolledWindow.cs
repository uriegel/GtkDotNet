using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class ScrolledWindow
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_scrolled_window_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(IntPtr zero, IntPtr zero2);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_scrolled_window_set_policy", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetPolicy(IntPtr scrolledWindow, PolicyType horizontal, PolicyType vertical);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_scrolled_window_set_min_content_width", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetMinContentWidth(IntPtr scrolledWindow, int width);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_scrolled_window_set_child", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetChild(IntPtr scrolledWindow, IntPtr widget);
    }
}
