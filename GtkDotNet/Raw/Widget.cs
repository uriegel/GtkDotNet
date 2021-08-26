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

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_set_visible", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetVisible(IntPtr widget, bool visible);
        
        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_get_visible", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool GetVisible(IntPtr widget);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_show_all", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ShowAll(IntPtr widget);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_set_size_request", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetSizeRequest(IntPtr widget, int width, int height);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_destroy", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Destroy(IntPtr widget);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_grab_focus", CallingConvention = CallingConvention.Cdecl)]
        public extern static void GrabFocus(IntPtr widget);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_get_allocated_width", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetAllocatedWidth(IntPtr widget);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_get_allocated_height", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetAllocatedHeight(IntPtr widget);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_queue_draw", CallingConvention = CallingConvention.Cdecl)]
        public extern static int QueueDraw(IntPtr widget);
    }
}
