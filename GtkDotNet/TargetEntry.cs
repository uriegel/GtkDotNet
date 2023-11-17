using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

class TargetEntry : IDisposable
{
    [Flags]
    public enum Flags
    {
        Default = 0,
        SameApp = 1,
        SameWidget = 2,
        OtherApp = 4,
        OtherWidget = 8
    }

    public TargetEntry(string identifier, Flags flags, int id)
        => handle = New(identifier, flags, id);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_target_entry_new", CallingConvention = CallingConvention.Cdecl)]
    extern static IntPtr New(string identifier, Flags flags, int id);

    [DllImport(Globals.LibGtk, EntryPoint = "gtk_target_entry_free", CallingConvention = CallingConvention.Cdecl)]
    extern static void Free(IntPtr targetEntry);

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
            Free(handle);
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
