using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Widget : GObject
    {
        #region Events
        
        public event EventHandler<KeyEventArgs> KeyPress
        { 
            add 
            {
                keyPress += value;
                keyPressFunc = (w, e) => 
                {
                    var evt = Marshal.PtrToStructure<EventKey>(e);
                    var kea = new KeyEventArgs(evt);
                    keyPress?.Invoke(this, kea);
                    return kea.Cancel;  
                };
                Raw.Gtk.SignalConnect<KeyPressEventFunc>(handle, "key_press_event", keyPressFunc);
            }
            remove 
            {
                keyPress -= value;
                Raw.Gtk.SignalDisconnect<KeyPressEventFunc>(handle, "key_press_event", keyPressFunc);
                keyPressFunc = null;
            }
        }
        event EventHandler<KeyEventArgs> keyPress;
        KeyPressEventFunc keyPressFunc;

        public event EventHandler Activate
        { 
            add 
            {
                activate += value;
                activateFunc = () => activate?.Invoke(this, EventArgs.Empty);
                Raw.Gtk.SignalConnect<Action>(handle, "activate", activateFunc);
            }
            remove 
            {
                activate -= value;
                Raw.Gtk.SignalDisconnect<Action>(handle, "activate", activateFunc);
                activateFunc = null;
            }
        }
        event EventHandler activate;
        Action activateFunc;

        public event EventHandler<DragDataGetEventArgs> DragDataGet
        { 
            add 
            {
                dragDataGet += value;
                dragDataGetFunc = (widget, context, selectionData, info, time, _) 
                    => dragDataGet?.Invoke(this, new DragDataGetEventArgs(){ SelectionData = new SelectionData(selectionData) });
                Raw.Gtk.SignalConnect<DragDataGetEventFunc>(handle, "drag-data-get", dragDataGetFunc);
            }
            remove 
            {
                dragDataGet -= value;
                Raw.Gtk.SignalDisconnect<DragDataGetEventFunc>(handle, "drag-data-get", dragDataGetFunc);
                dragDataGetFunc = null;
            }
        }
        event EventHandler<DragDataGetEventArgs> dragDataGet;
        DragDataGetEventFunc dragDataGetFunc;

        #endregion
        
        public bool Visible 
        {
            get => Raw.Widget.GetVisible(handle); 
            set => Raw.Widget.SetVisible(handle, value);  
        }

        public Widget(GObject obj) : base(obj) {}

        public void SetSizeRequest(int width, int height) => Raw.Widget.SetSizeRequest(handle, width, height);

        public void GrabFocus() => Raw.Widget.GrabFocus(handle);

        public int GetAllocatedWidth() => Raw.Widget.GetAllocatedWidth(handle);

        public int GetAllocatedHeight() => Raw.Widget.GetAllocatedHeight(handle);

        public void StartDrag(TargetList targetList, Raw.DragDrop.DragActions dragActions, int button, int x, int y) 
            => Raw.DragDrop.Begin(handle, targetList.handle, dragActions, button, IntPtr.Zero, x, y);

        delegate bool KeyPressEventFunc(IntPtr widget, IntPtr evt);
        delegate void DragDataGetEventFunc(IntPtr widget, IntPtr context, IntPtr selectionData, int info, int time, IntPtr _);
    }
}
