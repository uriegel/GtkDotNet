using System;

namespace GtkDotNet
{
    public class Pixbuf : GObject, IDisposable
    {
        public static Pixbuf FromResource(string path) 
            => new Pixbuf(Raw.Pixbuf.NewFromResource(path, IntPtr.Zero));
        Pixbuf(IntPtr pixbuf) : base(new GObject(pixbuf)) { }
        
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
        ~Pixbuf() 
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
