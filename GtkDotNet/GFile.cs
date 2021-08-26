using System;

namespace GtkDotNet
{
    public class GFile : GObject, IDisposable
    {
        public GFile(string path) : base(new GObject(Raw.GFile.New(path))) { }

        public void Trash()
        {
            var errorp = IntPtr.Zero;
            var deleted = Raw.GFile.FileTrash(handle, IntPtr.Zero, ref errorp);
            var error = new GError(errorp);
            if (!deleted)
                throw GErrorException.New(error);
        }

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
                Raw.GObject.Unref(handle);
                disposedValue = true;
            }
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~GFile() 
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
}
