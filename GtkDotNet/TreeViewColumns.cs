using System;
using System.Collections.Generic;

namespace GtkDotNet
{
    public class TreeViewColumns : IDisposable
    {
        public IEnumerable<TreeViewColumn> Items
        {
            get 
            {
                var length = Raw.GList.GetLength(columnList);
                for (var i = 0; i < length; i++)
                {
                    var data = Raw.GList.GetData(columnList, i);
                    yield return new TreeViewColumn(data);
                }
            }
        }

        internal TreeViewColumns(IntPtr list) => columnList = list;

        IntPtr columnList = IntPtr.Zero;

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
                Raw.GList.Free(columnList);
                disposedValue = true;
            }
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~TreeViewColumns()
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
