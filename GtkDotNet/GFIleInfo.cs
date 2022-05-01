using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

public class GFileInfo
{
    [DllImport(Globals.LibGtk, EntryPoint = "g_file_info_get_display_name", CallingConvention = CallingConvention.Cdecl)]
    public extern static string GetDisplayName(IntPtr info);

    [DllImport(Globals.LibGtk, EntryPoint = "g_file_info_get_icon", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr GetIcon(IntPtr info);
}
