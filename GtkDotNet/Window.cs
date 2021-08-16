using System;

namespace GtkDotNet
{
    public class Window : Container
    {
        public event EventHandler<DeleteEventArgs> Delete
        { 
            add 
            {
                delete += value;
                deleteFunc = () => 
                {
                    var dea = new DeleteEventArgs();
                    delete?.Invoke(this, dea);
                    return dea.Cancel;  
                };
                Raw.Gtk.SignalConnect<BoolFunc>(handle, "delete_event", deleteFunc);// true cancels the destroy request!
            }
            remove 
            {
                delete -= value;
                Raw.Gtk.SignalDisconnect<BoolFunc>(handle, "delete_event", deleteFunc);
                deleteFunc = null;
            }
        }
        event EventHandler<DeleteEventArgs> delete;
        BoolFunc deleteFunc;

        public Window(GObject obj) : base(obj) {}
        public void SetTitle(string title) => Raw.Window.SetTitle(handle, title);
        public void ShowAll() => Raw.Widget.ShowAll(handle);
        public void SetDefaultSize(int width, int height) => Raw.Window.SetDefaultSize(handle, width, height);
        public void Move(int x, int y) => Raw.Window.Move(handle, x, y);

        delegate bool BoolFunc();
    }
}
