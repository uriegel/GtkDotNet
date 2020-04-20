using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Theme
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_icon_theme_get_default", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetDefault();

        [DllImport(Globals.LibGtk, EntryPoint="gtk_icon_theme_choose_icon", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr ChooseIcon(IntPtr theme, IntPtr iconNames, int size, IconInfo.Flags flags);
    }
}