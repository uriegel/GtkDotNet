using System;
using System.Collections;

namespace GtkDotNet
{
    public class GObject 
    {
        internal GObject(IntPtr handle) => this.handle = handle;
        public GObject(GObject obj) => handle = obj.handle;

        public bool this[string key] 
        { 
            set => Raw.GObject.SetBool(handle, key, value);
            get => Raw.GObject.GetBool(handle, key);
        }
        internal readonly IntPtr handle;
    }
}
