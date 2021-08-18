using System;
using System.IO;

namespace GtkDotNet
{
    public class ResourceStream : Stream
    {
        public ResourceStream(string path)
        {
            this.path = path;
            gstream = Raw.Gio.ResourcesOpenStream(path, 0, IntPtr.Zero);
        }

        protected override void Dispose(bool disposing)
        {
            if (gstream != IntPtr.Zero)
                Raw.GObject.Unref(gstream);
            gstream = IntPtr.Zero;
        }

        readonly string path;
        IntPtr gstream;

        #region Stream
        public override bool CanRead => throw new System.NotImplementedException();

        public override bool CanSeek => throw new System.NotImplementedException();

        public override bool CanWrite => throw new System.NotImplementedException();

        public override long Length 
        {
            get 
            {
                Raw.Gio.ResourcesGetInfo(path, 0, out var size, out var flags, IntPtr.Zero);
                return size;
            }
        }
        
        public override long Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void Flush()
        {
            throw new System.NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new System.NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
