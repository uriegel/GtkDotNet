using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class WebKitContext
    {
        [DllImport(Globals.LibWebKit, EntryPoint="webkit_web_context_get_default", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetDefault();

        [DllImport(Globals.LibWebKit, EntryPoint="webkit_web_context_clear_cache", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ClearCache(IntPtr context);

        [DllImport(Globals.LibWebKit, EntryPoint="webkit_web_context_register_uri_scheme", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr RegisterUriScheme(IntPtr context, string scheme, SchemeRequest request, IntPtr nil, IntPtr nil1);
    }

    public delegate void SchemeRequest(IntPtr request);
}

