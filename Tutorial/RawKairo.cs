#define RAWKairo
#if RAWKairo

using System;
using GtkDotNet;

var app = Application.New("de.uriegel.test");
Application.Run(app, () =>
{
    var window = Window.New(GtkDotNet.WindowType.TopLevel);
    Window.SetTitle(window, "Kairo");

    var kairo = DrawingArea.New();
    DrawingArea.SetDrawFunction(kairo, (a, context, w, h, data) =>
    {
        CairoContext.SetAntiAlias(context, GtkDotNet.CairoAntialias.Best);
        CairoContext.SetLineJoin(context, GtkDotNet.LineJoin.Miter);
        CairoContext.SetLineCap(context, GtkDotNet.LineCap.Round);
        CairoContext.Translate(context, w / 2.0, h / 2.0);
        CairoContext.StrokePreserve(context);
        CairoContext.ArcNegative(context, 0, 0, (w < h ? w : h) / 2.0, -Math.PI / 2.0, -Math.PI / 2.0 + 0.1 * Math.PI);
        CairoContext.LineTo(context, 0, 0);
        CairoContext.SetSourceRgb(context, 0.7, 0.7, 0.7);
        CairoContext.Fill(context);
        
        CairoContext.MoveTo(context, 0, 0);
        CairoContext.Arc(context, 0, 0, (w < h ? w : h) / 2.0, -Math.PI / 2.0, -Math.PI / 2.0 + 0.1 * Math.PI);
        CairoContext.SetSourceRgb(context, 0.3, 0.3, 0.3);
        CairoContext.Fill(context);
    });

    Window.SetChild(window, kairo);
    Application.AddWindow(app, window);
    Widget.Show(window);
});

delegate void DrawFunc(IntPtr widget, IntPtr context, IntPtr data);

#endif