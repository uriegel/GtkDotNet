using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Window : Container
    {
        #region Events
        
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

        public event EventHandler<ConfigureEventArgs> Configure
        { 
            add 
            {
                configure += value;
                configureFunc = (w, e) => 
                {
                    var evt = Marshal.PtrToStructure<Raw.ConfigureEvent>(e);
                    var cea = new ConfigureEventArgs()
                    {
                        EventType = evt.EventType,
                        Height = evt.Height,
                        Width = evt.Width,
                        SendEvent = evt.SendEvent,
                        X = evt.X,
                        Y = evt.Y

                    };
                    configure?.Invoke(this, cea);
                    return false;
                };
                Raw.Gtk.SignalConnect<ConfigureEventFunc>(handle, "configure_event", configureFunc);
            }
            remove 
            {
                configure -= value;
                Raw.Gtk.SignalDisconnect<ConfigureEventFunc>(handle, "configure_event", configureFunc);
                configureFunc = null;
            }
        }
        event EventHandler<ConfigureEventArgs> configure;
        ConfigureEventFunc configureFunc;

        #endregion

        public (int, int) Size 
        { 
            get  
            {
                Raw.Window.GetSize(handle, out var w, out var h);
                return (w, h);
            }
        }       

        public Window() : base(new GObject(Raw.Window.New(WindowType.TopLevel))) {}
        public Window(GObject obj) : base(obj) {}
        public void SetTitle(string title) => Raw.Window.SetTitle(handle, title);
        public void ShowAll() => Raw.Widget.ShowAll(handle);
        public void SetDefaultSize(int width, int height) => Raw.Window.SetDefaultSize(handle, width, height);
        public void Maximize() => Raw.Window.Maximize(handle);
        public bool IsMaximized() => Raw.Window.IsMaximized(handle);
        public void Move(int x, int y) => Raw.Window.Move(handle, x, y);
        public (int, int) GetPosition() 
        {
            Raw.Window.GetPosition(handle, out var x, out var y);
            return (x, y);
        } 

        public void SetIcon(Pixbuf icon) => Raw.Window.SetIcon(handle, icon.handle);

        public void SetIconFromResource(string path) 
        {
            using var pixbuf = Pixbuf.FromResource(path);
            SetIcon(pixbuf);
        } 

        public void Close() => Raw.Window.Close(handle);

        delegate bool BoolFunc();
        delegate bool ConfigureEventFunc(IntPtr widget, IntPtr evt);
    }
}
