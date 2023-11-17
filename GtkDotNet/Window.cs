using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GtkDotNet;

public static class Window
{
    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_new", CallingConvention = CallingConvention.Cdecl)]
    public extern static IntPtr New(WindowType windowType);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_default_size", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetDefaultSize(this IntPtr window, int width, int height);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_move", CallingConvention = CallingConvention.Cdecl)]
    public extern static void Move(this IntPtr window, int x, int y);

    public static int GetWidth(this IntPtr window)
        => window.GetSize().Item1;

    public static int GetHeight(this IntPtr window)
        => window.GetSize().Item2;

    public static (int, int) GetPosition(this IntPtr window)
    {
        GetPosition(window, out var x, out var y);
        return (x, y);
    }

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_close", CallingConvention = CallingConvention.Cdecl)]
    public extern static void Close(this IntPtr window);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_modal", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetModal(this IntPtr window, bool set);
        
    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_title", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetTitle(this IntPtr window, string title);

    public static (int, int) GetSize(this IntPtr window)
    {
        GetSize(window, out var w, out var h);
        return (w, h);
    }        

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_maximize", CallingConvention = CallingConvention.Cdecl)]
    public extern static void Maximize(this IntPtr window);        

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_is_maximized", CallingConvention = CallingConvention.Cdecl)]
    public extern static bool IsMaximized(this IntPtr window);        
    
    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_transient_for", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetTransientFor(this IntPtr window, IntPtr parent);  

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_container_add", CallingConvention = CallingConvention.Cdecl)]
    public extern static bool SetChild(this IntPtr window, IntPtr child);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_application", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetApplication(this IntPtr window, IntPtr application);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_icon_name", CallingConvention = CallingConvention.Cdecl)]
    public extern static void SetIconName(this IntPtr window, string name);  

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_set_icon", CallingConvention = CallingConvention.Cdecl)]
    extern static bool SetIcon(this IntPtr window, IntPtr pixbuf);


    /// <summary>
    /// Sets the window icon. It uses an icon contained as DotNet resource
    /// </summary>
    /// <param name="window"></param>
    /// <param name="resourceIconPath">DotNet resource path of the icon</param>
    public static void SetIconFromDotNetResource(IntPtr window, string resourceIconPath)
    {
        var resIcon = Assembly
                        .GetEntryAssembly()
                        ?.GetManifestResourceStream(resourceIconPath);
        using var ms = new IconMemoryStream(resIcon);
        using var pixbuf = IconPixbuf.FromStream(ms);
        window.SetIcon(pixbuf.handle);
    }

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_get_size", CallingConvention = CallingConvention.Cdecl)]
    extern static void GetSize(IntPtr window, out int width, out int height);  

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_window_get_position", CallingConvention = CallingConvention.Cdecl)]
    extern static void GetPosition(IntPtr window, out int x, out int y);
}

class IconMemoryStream : IDisposable
{
    public IconMemoryStream(Stream inputStream)
    {
        var bytes = GObject.Malloc((int)inputStream.Length);
        unsafe
        {
            var nativeSpan = new Span<byte>(bytes.ToPointer(), (int)inputStream.Length);
            inputStream.Read(nativeSpan);
        }
        handle = NewInputStreamFromData(bytes, (int)inputStream.Length, 
            data => GObject.Free(data));
    }

    [DllImport(Globals.LibGtk, EntryPoint = "g_memory_input_stream_new_from_data", CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr NewInputStreamFromData(IntPtr data, int length, NotifyFree free);

    delegate void NotifyFree(IntPtr request);

    internal IntPtr handle { get; private set; }

    #region IDisposable

    bool disposedValue;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // Verwalteten Zustand (verwaltete Objekte) bereinigen
            }

            // Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
            // Große Felder auf NULL setzen
            GObject.Unref(handle);
            handle = IntPtr.Zero;
            disposedValue = true;
        }
    }

    // Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
    ~IconMemoryStream()
    {
        // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}

class IconPixbuf: IDisposable
{
    public static IconPixbuf FromStream(IconMemoryStream inutStream) 
        => new IconPixbuf(NewFromStream(inutStream.handle, IntPtr.Zero, IntPtr.Zero));

    IconPixbuf(IntPtr pixbuf) => handle = pixbuf;

    [DllImport(Globals.LibGtk, EntryPoint="gdk_pixbuf_new_from_stream", CallingConvention = CallingConvention.Cdecl)]
    extern static IntPtr NewFromStream(IntPtr gstream, IntPtr cancellable, IntPtr err);

    internal IntPtr handle;

    #region IDisposable

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // dispose managed state (managed objects)
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            GObject.Unref(handle);
            disposedValue = true;
        }
    }

    // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~IconPixbuf() 
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        => Dispose(disposing: false);

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    bool disposedValue;

    #endregion
}
