using System;

namespace GtkDotNet
{
    public class Dialog : IDisposable
    {
        public enum FileChooserAction
        {
            Open,
            Save, 
            SelectFolder,
            CreateFolder
        }

        public enum ResponseId 
        {
            None = -1,
            Reject = -2,
            Accept = -3,
            DeleteEvent = -4,
            Ok = -5,
            Cancel = -6,
            Close = -7,
            Yes = -8,
            No = -9,
            Apply = -10,
            Help = -11            
        }

        public ResponseId Run() => GtkDotNet.Raw.Dialog.Run(handle);
        protected Dialog(IntPtr handle) => this.handle = handle;

        protected readonly IntPtr handle;

        #region IDisposable 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                GtkDotNet.Raw.Widget.Destroy(handle);
                disposedValue = true;
            }
        }

        ~Dialog()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

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
