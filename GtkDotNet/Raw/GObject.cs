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
        
        public static void SetBool(IntPtr obj, string name, bool value)
            => SetBool(obj, name, value, IntPtr.Zero);
        public static bool GetBool(IntPtr obj, string name)
        {
            GetBool(obj, name, out var value, IntPtr.Zero);
            return value;
        }

        [DllImport(Globals.LibGtk, EntryPoint="g_object_set", CallingConvention = CallingConvention.Cdecl)]
        extern static void SetBool(IntPtr obj, string name, bool value, IntPtr end);
        [DllImport(Globals.LibGtk, EntryPoint="g_object_get", CallingConvention = CallingConvention.Cdecl)]
        extern static bool GetBool(IntPtr obj, string name, out bool value, IntPtr end);
    }
}

