using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class GioSettings
    {
        [DllImport(Globals.LibGtk, EntryPoint="g_settings_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(string schemaId);
    }
}