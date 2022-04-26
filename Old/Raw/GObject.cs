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

        [DllImport(Globals.LibGtk, EntryPoint="g_object_weak_ref", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddWeakRef(IntPtr obj, FinalizerDelegate finalizer, IntPtr zero);
                
        public static void SetBool(IntPtr obj, string name, bool value)
            => SetBool(obj, name, value, IntPtr.Zero);
        public static bool GetBool(IntPtr obj, string name)
        {
            GetBool(obj, name, out var value, IntPtr.Zero);
            return value;
        }

        public static void SetInt(IntPtr obj, string name, int value)
            => SetInt(obj, name, value, IntPtr.Zero);
        public static int GetInt(IntPtr obj, string name)
        {
            GetInt(obj, name, out var value, IntPtr.Zero);
            return value;
        }

        [DllImport(Globals.LibGtk, EntryPoint="g_object_set", CallingConvention = CallingConvention.Cdecl)]
        extern static void SetBool(IntPtr obj, string name, bool value, IntPtr end);
        [DllImport(Globals.LibGtk, EntryPoint="g_object_get", CallingConvention = CallingConvention.Cdecl)]
        extern static bool GetBool(IntPtr obj, string name, out bool value, IntPtr end);

        [DllImport(Globals.LibGtk, EntryPoint="g_object_set", CallingConvention = CallingConvention.Cdecl)]
        extern static void SetInt(IntPtr obj, string name, int value, IntPtr end);

        [DllImport(Globals.LibGtk, EntryPoint="g_object_get", CallingConvention = CallingConvention.Cdecl)]
        extern static bool GetInt(IntPtr obj, string name, out int value, IntPtr end);

        [DllImport(Globals.LibGtk, EntryPoint="g_object_set", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetString(IntPtr obj, string name, string value, IntPtr end);

        [DllImport(Globals.LibGtk, EntryPoint="g_object_new", CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr New(long type, IntPtr zero);

        public delegate void FinalizerDelegate(IntPtr zero, IntPtr obj);
    }
}

