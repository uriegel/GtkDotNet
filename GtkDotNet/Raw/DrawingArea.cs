using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class DrawingArea
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_drawing_area_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New();
    }
}
