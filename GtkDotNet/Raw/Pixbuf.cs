using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Pixbuf
    {
        [DllImport(Globals.LibGtk, EntryPoint="gdk_pixbuf_new_from_file", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewFromFile(string fileName, IntPtr err);

        [DllImport(Globals.LibGtk, EntryPoint="gdk_pixbuf_new_from_resource", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewFromResource(string resourcePath, IntPtr err);
         
        [DllImport(Globals.LibGtk, EntryPoint="gdk_pixbuf_new_from_stream", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewFromStream(IntPtr gstream, IntPtr cancellable, IntPtr err);
    }
}
