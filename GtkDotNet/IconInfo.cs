using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class IconInfo : IDisposable
    {
        public static IconInfo Choose(string filename, int size, IconLookup flags) 
        {
            var type = Raw.Gtk.GuessContentType(filename);
            var icon = Raw.Icon.Get(type);
            var names = Raw.Icon.GetNames(icon);
            var handle = Raw.Theme.ChooseIcon(theme, names, size, flags);
            Raw.GObject.Unref(icon);
            return new IconInfo(handle);
        }

        public string GetFileName()
        {
            var filename = Raw.IconInfo.GetFileName(handle);
            return Marshal.PtrToStringUTF8(filename);
        }

        IconInfo(IntPtr handle) => this.handle = handle;

        static IconInfo() => theme = Raw.Theme.GetDefault();

        static readonly IntPtr theme;
        readonly IntPtr handle;

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
                Raw.IconInfo.Free(handle);
                disposedValue = true;
            }
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~IconInfo()
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
