using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class SelectionData
    {
        public static string GetText(IntPtr data)
        {
            var ptr =  GetTextPtr(data);
            var result = Marshal.PtrToStringUTF8(ptr);
            GObject.Free(ptr);
            return result;
        }

        public static string GetData(IntPtr data)
        {
            var ptr =  GetDataPtr(data);
            var result = Marshal.PtrToStringUTF8(ptr);
            //GObject.Free(ptr);
            return result;
        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_selection_data_get_text", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr GetTextPtr(IntPtr data);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_selection_data_get_data", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr GetDataPtr(IntPtr data);
        [DllImport(Globals.LibGtk, EntryPoint="gtk_selection_data_set_uris", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr DataSetUris(IntPtr data, string[] uris);
    }
}
