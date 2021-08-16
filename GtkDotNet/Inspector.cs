using System;

namespace GtkDotNet
{
    public class Inspector : GObject
    {
        internal Inspector(IntPtr handle) : base(handle) { }

        public void Show() => Raw.WebKit.InspectorShow(handle);
    }
}
