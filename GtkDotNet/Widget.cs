using System;

namespace GtkDotNet
{
    public class Widget : GObject
    {
        public Widget(GObject obj) : base(obj) {}

        public void SetSizeRequest(int width, int height) => Raw.Widget.SetSizeRequest(handle, width, height);
    }
}
