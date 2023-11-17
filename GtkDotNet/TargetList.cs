using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

class TargetList : IDisposable
{
    public TargetList(TargetEntry entry)
        => handle = New(entry.handle, 1);
        
    [DllImport(Globals.LibGtk, EntryPoint = "gtk_target_list_new", CallingConvention = CallingConvention.Cdecl)]
    extern static IntPtr New(IntPtr targetEntries, int count);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_target_list_unref", CallingConvention = CallingConvention.Cdecl)]
    extern static IntPtr Unref(IntPtr list);

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
            Unref(handle);
            disposedValue = true;
        }
    }

    // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~TargetList()
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
