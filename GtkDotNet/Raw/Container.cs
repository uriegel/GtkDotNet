using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Container
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_container_set_border_width", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetBorderWidth(IntPtr container, int width);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_container_add", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Add(IntPtr container, IntPtr widget);
    }
}
