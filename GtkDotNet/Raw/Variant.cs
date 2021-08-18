using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public static class Variant
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_variant_new_boolean", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewBool(bool value);
        
        [DllImport(Globals.LibGtk, EntryPoint="g_variant_new_string", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewString(string value);    

        [DllImport(Globals.LibGtk, EntryPoint="g_variant_get_boolean", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetBool(IntPtr value);
        
        [DllImport(Globals.LibGtk, EntryPoint="g_variant_get_string", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetString(IntPtr value, IntPtr size);

        [DllImport(Globals.LibGtk, EntryPoint="g_variant_unref", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr Unref(IntPtr variant);
    }
}
