using System;
using System.Collections;

namespace GtkDotNet
{
    public class GObject 
    {
        internal GObject(IntPtr handle) => this.handle = handle;
        public GObject(GObject obj) => handle = obj.handle;

        public int GetInt(string key) => Raw.GObject.GetInt(handle, key);
        public void SetInt(string key, int value) => Raw.GObject.SetInt(handle, key, value);

        public bool GetBool(string key) => Raw.GObject.GetBool(handle, key);
        public void SetBool(string key, bool value) => Raw.GObject.SetBool(handle, key, value);

        public void SetString(string key, string value) => Raw.GObject.SetString(handle, key, value, IntPtr.Zero);

        internal readonly IntPtr handle;
    }
}
