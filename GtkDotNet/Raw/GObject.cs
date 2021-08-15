using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class GObject
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_object_unref", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Unref(IntPtr obj);

        [DllImport(Globals.LibGtk, EntryPoint="g_free", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Free(IntPtr obj);
        
        public static void SetBool(IntPtr settings, string name, bool value)
            => SetBool(settings, name, value, IntPtr.Zero);


        [DllImport(Globals.LibGtk, EntryPoint="g_object_set", CallingConvention = CallingConvention.Cdecl)]
        extern static void SetBool(IntPtr settings, string name, bool value, IntPtr end);
    }
}

