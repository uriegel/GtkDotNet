using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class IconInfo
    {
        public enum Flags
        {
            None = 0,
            ForceSvg = 2
        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_icon_info_get_filename", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetFileName(IntPtr iconInfo);
    }
}