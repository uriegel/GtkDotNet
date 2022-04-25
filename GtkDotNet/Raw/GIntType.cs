using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw;

public class GIntType 
{
    public static long Type { get; }
    public static int GetValue(IntPtr intType) 
        => Marshal.ReadInt32(intType, 28); 

    public static void SetValue(IntPtr intType, int value) 
        => Marshal.WriteInt32(intType, 28, value); 
    
    public static IntPtr New() => GObject.New(Type, IntPtr.Zero);

    static GIntType()
    {
        var info = new GTypeInfo();
        info.class_size = (ushort)Marshal.SizeOf<IntTypeClass>();// 136
        info.instance_size = (ushort)Marshal.SizeOf<IntType>(); // 32
        Type = RegisterStatic(80, "GtkDotNetInt", ref info, 0);
    }

    [StructLayout(LayoutKind.Sequential)]
    struct IntType 
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=28)] 
        byte[] parent; 
        public int value;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct IntTypeClass 
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=136)] 
        public byte[] parentClass; 
    }
    
    [DllImport(Globals.LibGtk, EntryPoint="g_type_register_static", CallingConvention = CallingConvention.Cdecl)]
    extern static long RegisterStatic(int type, string typeName, ref GTypeInfo info, int flags);
}
 