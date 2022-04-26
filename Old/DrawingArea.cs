using System;

namespace GtkDotNet
{
    public class DrawingArea : Widget
    {
        public delegate void DrawFunc(DrawingArea drawingArea, CairoContext context);
        public DrawingArea(DrawFunc drawFunc) : this(drawFunc, Raw.DrawingArea.New()) { }

        public DrawingArea(DrawFunc drawFunc, IntPtr handle) : base(new GObject(handle))
            => InitDrawFunc(drawFunc);

        public void QueueDraw() => Raw.Widget.QueueDraw(handle);

        protected void InitDrawFunc(DrawFunc drawFunc)
        {
            if (drawFunc != null)
            {
                this.drawFunc = drawFunc;
                Raw.Gtk.SignalConnect<RawDrawFunc>(handle, "draw", (_, context, __)
                    => drawFunc(this, new CairoContext(context)));
            }
        }

        DrawFunc drawFunc;

        delegate void RawDrawFunc(IntPtr widget, IntPtr context, IntPtr data);
    }
}
