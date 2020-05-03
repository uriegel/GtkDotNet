using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class WebKitContext
    {
        [DllImport(Globals.LibWebKit, EntryPoint="webkit_web_context_get_default", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetDefault();

        [DllImport(Globals.LibWebKit, EntryPoint="webkit_web_context_clear_cache", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ClearCache(IntPtr context);
    }
}

