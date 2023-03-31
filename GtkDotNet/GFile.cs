using System;
using System.IO;
using System.Threading;

namespace GtkDotNet
{
    public class GFile : GObject, IDisposable
    {
        public delegate void ProgressCallback(long current, long total);

        public static void Copy(string source, string destination, FileCopyFlags flags, ProgressCallback cb)
            => Copy(source, destination, flags, false, cb);

        public static void Copy(string source, string destination, FileCopyFlags flags, bool createTargetPath, ProgressCallback cb)            
            => Copy(source, destination, flags, createTargetPath, cb, null);
        
        public static void Copy(string source, string destination, FileCopyFlags flags, bool createTargetPath, ProgressCallback cb, CancellationToken? cancellation)
        {
            using var cancellable = cancellation.HasValue ? new Cancellable(cancellation.Value) : null;
            using var sourceFile = new GFile(source);
            using var destinationFile = new GFile(destination);
            var error = IntPtr.Zero;
            Raw.GFile.FileProgressCallback rcb = cb != null ? (c, t, _) => cb(c, t) : null;
            if (!Raw.GFile.Copy(sourceFile.handle, destinationFile.handle, flags, cancellable?.handle ?? IntPtr.Zero, rcb, IntPtr.Zero, ref error))
            {
                var gerror = new GError(error);
                if (createTargetPath && gerror.Domain == 232 && gerror.Code == 1 && File.Exists(source))
                {
                    var fi = new FileInfo(destination);
                    var path = fi.Directory;
                    try 
                    {
                        path.Create();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        throw GErrorException.New(new GError(232, 14, "Access Denied"), source, destination);
                    }
                    Copy(source, destination, flags, true, cb, cancellation);
                }
                else
                    throw GErrorException.New(gerror, source, destination);
            }
        }

        public static void Move(string source, string destination, FileCopyFlags flags, ProgressCallback cb)
            => Move(source, destination, flags, false, cb);

        public static void Move(string source, string destination, FileCopyFlags flags, bool createTargetPath, ProgressCallback cb)            
            => Move(source, destination, flags, createTargetPath, cb, null);

        public static void Move(string source, string destination, FileCopyFlags flags, bool createTargetPath, ProgressCallback cb, CancellationToken? cancellation)
        {
            using var sourceFile = new GFile(source);
            using var destinationFile = new GFile(destination);
            var error = IntPtr.Zero;
            Raw.GFile.FileProgressCallback rcb = cb != null ? (c, t, _) => cb(c, t) : null;
            if (!Raw.GFile.Move(sourceFile.handle, destinationFile.handle, flags, IntPtr.Zero, rcb, IntPtr.Zero, ref error))
            {
                var gerror = new GError(error);
                if (createTargetPath && gerror.Domain == 232 && gerror.Code == 1 && File.Exists(source))
                {
                    var fi = new FileInfo(destination);
                    var path = fi.Directory;
                    try 
                    {
                        path.Create();
                    }
                    catch (AccessDeniedException)
                    {
                        throw GErrorException.New(new GError(232, 14, "Access Denied"), source, destination);
                    }
                    Move(source, destination, flags, true, cb);
                }
                else
                    throw GErrorException.New(gerror, source, destination);
            }
        }

        public static void Trash(string path)
        {
            using var file = new GFile(path);
            var error = IntPtr.Zero;
            if (!Raw.GFile.Trash(file.handle, IntPtr.Zero, ref error))
                throw GErrorException.New(new GError(error), path, null);
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
