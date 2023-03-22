using System;
using System.IO;

namespace GtkDotNet;

public class MemoryStream : IDisposable
{
    public MemoryStream(Stream inputStream)
    {
        var bytes = Raw.GObject.Malloc((int)inputStream.Length);
        unsafe
        {
            var nativeSpan = new Span<byte>(bytes.ToPointer(), (int)inputStream.Length);
            inputStream.Read(nativeSpan);
        }
        handle = Raw.MemoryStream.NewInputStreamFromData(bytes, (int)inputStream.Length, 
            data => Raw.GObject.Free(data));
    }

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
            Raw.GObject.Unref(handle);
            handle = IntPtr.Zero;
            disposedValue = true;
        }
    }

    // Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
    ~MemoryStream()
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