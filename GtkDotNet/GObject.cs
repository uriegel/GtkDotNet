using System;

namespace GtkDotNet
{
    public class GObject 
    {
        internal GObject(IntPtr handle) => this.handle = handle;
        public GObject(GObject obj) => handle = obj.handle;
        internal readonly IntPtr handle;
    }
}
