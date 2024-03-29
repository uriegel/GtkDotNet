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
                        EventType = (GdkEventType)evt.EventType,
                        Height = evt.Height,
                        Width = evt.Width,
                        SendEvent = evt.SendEvent != 0,
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

        public event EventHandler<NotifyEventArgs> LeaveNotify
        { 
            add 
            {
                leaveNotify += value;
                leaveNotifyFunc = (a, b, c) => 
                {
                    var dea = new NotifyEventArgs();
                    leaveNotify?.Invoke(this, dea);
                    return dea.Cancel;  
                };
                Raw.Gtk.SignalConnect<NotifyEventFunc>(handle, "leave-notify_event", leaveNotifyFunc);// true cancels the destroy request!
            }
            remove 
            {
                leaveNotify -= value;
                Raw.Gtk.SignalDisconnect<NotifyEventFunc>(handle, "leave-notify_event", leaveNotifyFunc);
                leaveNotify = null;
            }
        }
        event EventHandler<NotifyEventArgs> leaveNotify;
        NotifyEventFunc leaveNotifyFunc;

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
        public void Resize(int width, int height) => Raw.Window.Resize(handle, width, height);
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

        public void SetIconFromCSharpResource(string path) 
        {
            var resIcon = System.Reflection.Assembly
                .GetEntryAssembly()
                ?.GetManifestResourceStream(path);
            using var ms = new GtkDotNet.MemoryStream(resIcon);
            using var pixbuf = Pixbuf.FromStream(ms);
            SetIcon(pixbuf);
        } 

        public void Close() => Raw.Window.Close(handle);

        delegate bool BoolFunc();
        delegate bool ConfigureEventFunc(IntPtr widget, IntPtr evt);
        delegate bool NotifyEventFunc(IntPtr widget, IntPtr evt, IntPtr _);
    }
}
