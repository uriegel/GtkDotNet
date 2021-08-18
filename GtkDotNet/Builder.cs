using System;

namespace GtkDotNet
{
    public class Builder : IDisposable
    {
        public static Builder FromResource(string pathToGlade)
            => new Builder(Raw.Builder.FromResource(pathToGlade));

        public Builder() => builder = Raw.Builder.New();

        Builder(IntPtr builder) => this.builder = builder;

        public void FromFile(string gladeFile)
            => Raw.Builder.AddFromFile(builder, gladeFile, IntPtr.Zero);

        public GObject GetObject(string objectName)
            => new GObject(Raw.Builder.GetObject(builder, objectName));

        readonly IntPtr builder;

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
                Raw.GObject.Unref(builder);
                disposedValue = true;
            }
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~Builder()
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
