using System;
using System.Threading;

namespace GtkDotNet;

public class Cancellable : GObject, IDisposable
{
    public Cancellable() : base(Raw.Cancellable.New()) {}

    public Cancellable(CancellationToken cancellationToken) : base(Raw.Cancellable.New()) 
        => cancellationToken.Register(Cancel);
    
    public void Cancel() => Raw.Cancellable.Cancel(handle);

    #region IDisposable

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            //if (disposing)
                // Verwalteten Zustand (verwaltete Objekte) bereinigen
           

            // Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
            // Große Felder auf NULL setzen
            
            Raw.GObject.Unref(handle);
            disposedValue = true;
        }
    }

    //  Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
    ~Cancellable()
        // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
        => Dispose(disposing: false);

    public void Dispose()
    {
        // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    bool disposedValue;
    
    #endregion
}