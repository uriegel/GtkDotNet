
using System;
using System.Linq;
using System.Runtime.InteropServices;
using GtkDotNet;
using LinqTools;

public static class SelectionData
{
    public static IntPtr DataSetUris(this IntPtr data, string[] uris)
    {
        var intptrs = uris.Select(n => Marshal.StringToCoTaskMemUTF8(Uri.EscapeDataString(n).Replace("%2F", "/").Replace("%3A", ":"))).ToArray();
        var result = DataSetUris(data, intptrs);
        intptrs.ForEach(n => Marshal.FreeCoTaskMem(n));
        return result;
    }

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_selection_data_set_uris", CallingConvention = CallingConvention.Cdecl)]
    extern static IntPtr DataSetUris(this IntPtr data, IntPtr[] uris);
}