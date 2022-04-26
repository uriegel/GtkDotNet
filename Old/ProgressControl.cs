using System;

namespace GtkDotNet
{
    public class ProgressControl : DrawingArea
    {
        public double Progress
        {
            get => _Progress;
            set
            {
                if (value < 0) value = 0;
                else if (value > 1) value = 1;
                _Progress = value;
                QueueDraw();
            }
        }
        double _Progress;
        
        public ProgressControl(GObject obj) : base(null, obj.handle) => InitDrawFunc(Draw);

        void Draw(DrawingArea d, CairoContext context)
        {
            var w = GetAllocatedWidth();
            var h = GetAllocatedHeight();
            context.SetAntiAlias(GtkDotNet.CairoAntialias.Best);
            context.SetLineJoin(GtkDotNet.LineJoin.Miter);
            context.SetLineCap(GtkDotNet.LineCap.Round);
            context.Translate(w / 2.0, h / 2.0);
            context.StrokePreserve();

            var angle = -Math.PI / 2.0 + Progress * 2.0 * Math.PI;
            context.ArcNegative(0, 0, (w < h ? w : h) / 2.0, -Math.PI / 2.0, angle);
            context.LineTo(0, 0);
            context.SetSourceRgb(0.7, 0.7, 0.7);
            context.Fill();
            
            context.MoveTo(0, 0);
            context.Arc(0, 0, (w < h ? w : h) / 2.0, -Math.PI / 2.0, angle);
            context.SetSourceRgb(0.3, 0.3, 0.3);
            context.Fill();
        }
    }
}
