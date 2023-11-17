using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

public static class DragDrop
{
    [Flags]
    public enum DefaultDestination
    {
        Motion = 1,
        Highlight = 2,
        Drop = 4
    }

    [Flags]
    public enum DragActions
    {
        Default = 1,
        Copy = 2,
        Move = 4,
        Link = 8,
        Private = 0x10,
        Ask = 0x20
    }

    [Flags]
    public enum ModifierType
    {
        ShiftMask = 1,
        LockMask = 2,
        ControlMask = 4,
        Mod1Mask = 8,
        Mod2Mask = 0x10,
        Mod3Mask = 0x20,
        Mod4Mask = 0x40,
        Mod5Mask = 0x80,
        Button1Mask = 0x100,
        Button2Mask = 0x200,
        Button3Mask = 0x400,
        Button4Mask = 0x800,
        Button5Mask = 0x1000,

    }

    public static void BeginDragDrop(this IntPtr widget, string identifier)
    {
        var ds = NewDragSource();
        ds.SignalConnect<DragPrepareDelegate>("prepare", (s, x, y, w) =>
        {
            GSList list = new();

            GValue.Nix nix = new();
            list.Data = GValue.Init(nix, GType.String);
            GValue.SetString(list.Data, "Affenkopf");
            var mist = GValue.GetString(list.Data);
            var misti = Marshal.PtrToStringUni(mist);

//            GDK_TYPE_FILE_LIST
            return IntPtr.Zero;
        });
        ds.SignalConnect<DragBeginDelegate>("drag-begin", (s, x, y) =>
        {

        });
        widget.AddController(ds);
    }
    
    [DllImport(Globals.LibGtk, EntryPoint = "gtk_drag_source_new", CallingConvention = CallingConvention.Cdecl)]
    extern static IntPtr NewDragSource();

    [DllImport(Globals.LibGtk, EntryPoint = "gdk_content_provider_new_typed", CallingConvention = CallingConvention.Cdecl)]
    extern static IntPtr NewTypeContentProvider();

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_widget_add_controller", CallingConvention = CallingConvention.Cdecl)]
    extern static IntPtr AddController(IntPtr widget, IntPtr controller);
       
    delegate IntPtr DragPrepareDelegate(IntPtr source, double x, double y, IntPtr widget);
    delegate void DragBeginDelegate(IntPtr source, IntPtr s, IntPtr o);
}