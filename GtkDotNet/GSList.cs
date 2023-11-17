using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

[StructLayout(LayoutKind.Sequential)]
public struct GSList 
{
    public IntPtr Data;
    public IntPtr Next;
}
