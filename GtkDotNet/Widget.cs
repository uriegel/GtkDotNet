using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Widget
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_widget_show", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Show(IntPtr widget);
    }
}
 
