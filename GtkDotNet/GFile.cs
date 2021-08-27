using System;

namespace GtkDotNet
{
    public class GFile : GObject, IDisposable
    {
        public delegate void ProgressCallback(long current, long total);
        
        public static void Copy(string source, string destination, FileCopyFlags flags, ProgressCallback cb)
        {
            using var sourceFile = new GFile(source);
            using var destinationFile = new GFile(destination);
            var errorp = IntPtr.Zero;
            Raw.GFile.FileProgressCallback rcb = cb != null ? (c, t, _) => cb(c, t) : null;
            Raw.GFile.Copy(sourceFile.handle, destinationFile.handle, flags, IntPtr.Zero, rcb, IntPtr.Zero, ref errorp);
            rcb = null;
            // TODO Error Handling
        }

        public static void Trash(string path)
        {
            using var file = new GFile(path);
            var errorp = IntPtr.Zero;
            var deleted = Raw.GFile.Trash(file.handle, IntPtr.Zero, ref errorp);
            var error = new GError(errorp);
            if (!deleted)
                throw GErrorException.New(error);
            // TODO Error Handling                
        }

        public GFile(string path) : base(new GObject(Raw.GFile.New(path))) { }

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
