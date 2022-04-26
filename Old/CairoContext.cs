using System;

namespace GtkDotNet
{
    public class CairoContext
    {
        public void SetAntiAlias(CairoAntialias antialias)
            => Raw.CairoContext.SetAntiAlias(context, antialias);
        public void SetLineJoin(LineJoin lineJoin)
            => Raw.CairoContext.SetLineJoin(context, lineJoin);
        public void SetLineCap(LineCap lineCap)
            => Raw.CairoContext.SetLineCap(context, lineCap);
        public void Translate(double x, double y)
            => Raw.CairoContext.Translate(context, x, y); 
        public void StrokePreserve() => Raw.CairoContext.StrokePreserve(context);
        public void ArcNegative(double x, double y, double radius, double angle1, double angle2)
            => Raw.CairoContext.ArcNegative(context, x, y, radius, angle1, angle2);
        public void LineTo(double x, double y) => Raw.CairoContext.LineTo(context, x, y);
        public void SetSourceRgb(double r, double g, double b)
            => Raw.CairoContext.SetSourceRgb(context, r, g, b);
        public void Fill() => Raw.CairoContext.Fill(context);
        public void MoveTo(double x, double y) => Raw.CairoContext.MoveTo(context, x, y);
        public void Arc(double x, double y, double radius, double angle1, double angle2)
            => Raw.CairoContext.Arc(context, x, y, radius, angle1, angle2);

        internal CairoContext(IntPtr context) => this.context = context;

        readonly IntPtr context;
    }
}
