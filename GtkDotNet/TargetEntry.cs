using System;

namespace GtkDotNet;

public class TargetEntry : IDisposable
{
    public TargetEntry(string identifier, GtkDotNet.Raw.TargetEntry.Flags flags, int id)
        => handle = GtkDotNet.Raw.TargetEntry.New(identifier, flags, id);

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
            Raw.TargetEntry.Free(handle);
            disposedValue = true;
        }
    }

    // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~TargetEntry()
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
