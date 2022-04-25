using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Label
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_label_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(string text);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_label_set_label", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetLabel(IntPtr label, string text);
        
        [DllImport(Globals.LibGtk, EntryPoint="gtk_label_set_selectable", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetSelectable(IntPtr label, bool selectable);
    }
}

