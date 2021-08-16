using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Resources
    {
        public Resources(string nameSpace)
        {
            var assi = Assembly.GetEntryAssembly();
            var stream = assi.GetManifestResourceStream($"{nameSpace}.resources.gresource");
            var memIntPtr = Marshal.AllocHGlobal((int)stream.Length);
            unsafe 
            {
                var memBytePtr = (byte*)memIntPtr.ToPointer();
                var writeStream = new UnmanagedMemoryStream(memBytePtr, stream.Length, stream.Length, FileAccess.Write);
                stream.CopyTo(writeStream);
            }
            var gbytes = Raw.GBytes.New(memIntPtr, stream.Length);
            Marshal.FreeHGlobal(memIntPtr);
            var res = Raw.Gio.NewResourceFromData(gbytes, IntPtr.Zero);
            Raw.GBytes.Unref(gbytes);
            Raw.Gio.RegisterResources(res); 
        }
    }
}
