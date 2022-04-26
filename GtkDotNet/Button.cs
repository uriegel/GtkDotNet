using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Button
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_button_new_with_label", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewWithLabel(string label);


        [DllImport(Globals.LibGtk, EntryPoint="gtk_button_set_label", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetLabel(IntPtr button, string label);
    }
}
