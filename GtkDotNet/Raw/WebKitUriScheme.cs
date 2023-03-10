using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw;

public class WebKitUriScheme
{
    [DllImport(Globals.LibWebKit, EntryPoint = "webkit_uri_scheme_request_get_scheme", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr GetScheme(IntPtr request);
    
    [DllImport(Globals.LibWebKit, EntryPoint = "webkit_uri_scheme_request_get_uri", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr GetUri(IntPtr request);
    
    [DllImport(Globals.LibWebKit, EntryPoint = "webkit_uri_scheme_request_get_path", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr GetPath(IntPtr request);
    
    [DllImport(Globals.LibWebKit, EntryPoint = "webkit_uri_scheme_request_get_web_view", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr GetWebView(IntPtr request);
    
    [DllImport(Globals.LibWebKit, EntryPoint = "webkit_uri_scheme_request_finish", CallingConvention = CallingConvention.Cdecl)]
    public extern static void Finish(IntPtr request, IntPtr stream, long length, string contentType = null);

    [DllImport(Globals.LibWebKit, EntryPoint = "webkit_uri_scheme_request_finish_error", CallingConvention = CallingConvention.Cdecl)]
    public extern static void FinishError(IntPtr request, IntPtr error);
}
