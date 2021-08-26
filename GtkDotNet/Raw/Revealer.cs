using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Revealer
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_revealer_get_reveal_child", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool GetRevealChild(IntPtr handle);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_revealer_set_reveal_child", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetRevealChild(IntPtr handle, bool value);
    }
}
