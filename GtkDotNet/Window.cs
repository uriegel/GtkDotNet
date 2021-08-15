using System;

namespace GtkDotNet
{
    public class Window : Widget
    {
        public Window(GObject obj) : base(obj) {}
        public void SetTitle(string title) => Raw.Window.SetTitle(handle, title);
        public void ShowAll() => Raw.Widget.ShowAll(handle);
    }
}
