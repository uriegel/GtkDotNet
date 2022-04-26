using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class CairoContext
    {
        [DllImport(Globals.LibGtk, EntryPoint = "cairo_set_antialias", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetAntiAlias(IntPtr context, CairoAntialias antialias);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_set_line_join", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetLineJoin(IntPtr context, LineJoin lineJoin);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_set_line_cap", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetLineCap(IntPtr context, LineCap lineCap);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_translate", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Translate(IntPtr context, double x, double y);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_stroke_preserve", CallingConvention = CallingConvention.Cdecl)]
        public extern static void StrokePreserve(IntPtr context);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_arc_negative", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ArcNegative(IntPtr context, double x, double y, double radius, double angle1, double angle2);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_line_to", CallingConvention = CallingConvention.Cdecl)]
        public extern static void LineTo(IntPtr context, double x, double y);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_set_source_rgb", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetSourceRgb(IntPtr context, double r, double g, double b);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_fill", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Fill(IntPtr context);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_move_to", CallingConvention = CallingConvention.Cdecl)]
        public extern static void MoveTo(IntPtr context, double x, double y);

        [DllImport(Globals.LibGtk, EntryPoint = "cairo_arc", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Arc(IntPtr context, double x, double y, double radius, double angle1, double angle2);
    }
}
