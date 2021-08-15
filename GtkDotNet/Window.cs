using System;

namespace GtkDotNet
{
    public class Window : Widget
    {
        public Window(GObject obj) : base(obj) {}
        public void SetTitle(string title) => Raw.Window.SetTitle(handle, title);
        public void ShowAll() => Raw.Widget.ShowAll(handle);
        public void SetDefaultSize(int width, int height) => Raw.Window.SetDefaultSize(handle, width, height);
        public void Move(int x, int y) => Raw.Window.Move(handle, x, y);
    }
}
