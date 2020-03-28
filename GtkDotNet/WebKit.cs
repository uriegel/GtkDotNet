using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class WebKit
    {
        [DllImport(Globals.LibWebKit, EntryPoint="webkit_web_view_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New();

        [DllImport(Globals.LibWebKit, EntryPoint="webkit_web_view_load_uri", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr LoadUri(IntPtr webView, string uri);
    }
}
