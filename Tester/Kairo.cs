// #define Kairo
#if Kairo

using System;
using GtkDotNet;

var app = new Application("de.uriegel.test");
app.Run(() =>
{
    var window = new Window();
    window.SetTitle("Kairo");

    var kairo = new DrawingArea((kairo, context) => 
    {
        var w = kairo.GetAllocatedWidth();
        var h = kairo.GetAllocatedHeight();
        context.SetAntiAlias(GtkDotNet.CairoAntialias.Best);
        context.SetLineJoin(GtkDotNet.LineJoin.Miter);
        context.SetLineCap(GtkDotNet.LineCap.Round);
        context.Translate(w / 2.0, h / 2.0);
        context.StrokePreserve();
        context.ArcNegative(0, 0, (w < h ? w : h) / 2.0, -Math.PI / 2.0, -Math.PI / 2.0 + 0.1 * Math.PI);
        context.LineTo(0, 0);
        context.SetSourceRgb(0.7, 0.7, 0.7);
        context.Fill();
        
        context.MoveTo(0, 0);
        context.Arc(0, 0, (w < h ? w : h) / 2.0, -Math.PI / 2.0, -Math.PI / 2.0 + 0.1 * Math.PI);
        context.SetSourceRgb(0.3, 0.3, 0.3);
        context.Fill();
    });
    window.Add(kairo);
    app.AddWindow(window);
    window.ShowAll();
});

delegate void DrawFunc(IntPtr widget, IntPtr context, IntPtr data);

#endif