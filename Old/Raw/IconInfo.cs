using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class IconInfo
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_icon_info_get_filename", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetFileName(IntPtr iconInfo);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_icon_info_free", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Free(IntPtr iconInfo);
    }
}