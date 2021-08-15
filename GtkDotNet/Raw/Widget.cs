using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Widget
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_show", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Show(IntPtr widget);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_hide", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Hide(IntPtr widget);
        
        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_show_all", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ShowAll(IntPtr widget);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_set_size_request", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetSizeRequest(IntPtr widget, int width, int height);
        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_destroy", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Destroy(IntPtr widget);
    }
}
