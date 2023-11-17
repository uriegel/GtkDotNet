using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;
public static class GValue
{
    [DllImport(Globals.LibGtk, EntryPoint="g_value_init", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr Init(Nix nix, GType type);

    [DllImport(Globals.LibGtk, EntryPoint="g_value_init", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr Init(IntPtr ptr, GType type);
    
    [DllImport(Globals.LibGtk, EntryPoint="g_value_set_string", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetString(IntPtr ptr, string text);

    
    [DllImport(Globals.LibGtk, EntryPoint="g_value_get_string", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr GetString(IntPtr ptr);

    [DllImport(Globals.LibGtk, EntryPoint="g_value_set_int", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetInt(IntPtr ptr, int value);

    [DllImport(Globals.LibGtk, EntryPoint="g_value_unset", CallingConvention = CallingConvention.Cdecl)]
    public extern static void Unset(IntPtr ptr);


    [StructLayout(LayoutKind.Sequential)]
    public struct Nix
    {
        public IntPtr Data;
        public IntPtr Next;
    }
}
