using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class GValue
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct GValueDummy
        {
            public long Part1;
            public long Part2;
            public long Part3;
        }

        [DllImport(Globals.LibGtk, EntryPoint="g_value_init", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr Init(ref GValueDummy dummy, GType type);

        
        [DllImport(Globals.LibGtk, EntryPoint="g_value_set_string", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetString(ref GValueDummy dummy, string text);

        
        [DllImport(Globals.LibGtk, EntryPoint="g_value_get_string", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetString(ref GValueDummy dummy);

        [DllImport(Globals.LibGtk, EntryPoint="g_value_set_int", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetInt(ref GValueDummy dummy, int value);
    }
}
