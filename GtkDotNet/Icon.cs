using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Icon
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_content_type_get_icon", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr Get(string contentType);

        [DllImport(Globals.LibGtk, EntryPoint="g_themed_icon_get_names", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetNames(IntPtr icon);
    }
}