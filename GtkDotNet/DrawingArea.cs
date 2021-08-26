using System;

namespace GtkDotNet
{
    public class DrawingArea : Widget
    {
        public delegate void DrawFunc(DrawingArea drawingArea, CairoContext context);
        public DrawingArea(DrawFunc drawFunc) : base(new GObject(Raw.DrawingArea.New()))
        {
            this.drawFunc = drawFunc;
            Raw.Gtk.SignalConnect<RawDrawFunc>(handle, "draw", (_, context, __) 
                => drawFunc(this, new CairoContext(context)));
        }

        readonly DrawFunc drawFunc;

        delegate void RawDrawFunc(IntPtr widget, IntPtr context, IntPtr data);
    }
}
