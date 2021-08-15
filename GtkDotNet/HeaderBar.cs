using System;

namespace GtkDotNet
{
    public class HeaderBar : Widget
    {
        public HeaderBar(GObject obj) : base(obj) {}

        public void SetSubtitle(string subtitle) => Raw.HeaderBar.SetSubtitle(handle, subtitle);
    }
}
