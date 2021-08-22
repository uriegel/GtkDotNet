using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class GValue
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_value_init", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr Init(ref GValueDummy dummy, GType type);

        
        [DllImport(Globals.LibGtk, EntryPoint="g_value_set_string", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SetString(IntPtr gvalue, string text);

        
        [DllImport(Globals.LibGtk, EntryPoint="g_value_get_string", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetString(IntPtr gvalue);

        [DllImport(Globals.LibGtk, EntryPoint="g_value_set_int", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SetInt(IntPtr gvalue, int value);
    }
}
