using System;
using System.IO;
using System.Runtime.InteropServices;

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
        
        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

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
            => throw new System.NotImplementedException();

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer.Length < count + offset)
                throw new IndexOutOfRangeException();
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr address = handle.AddrOfPinnedObject();
            address += offset;
            var read = (int)Raw.Gio.StreamRead(gstream, address, count, IntPtr.Zero, IntPtr.Zero);
            handle.Free();
            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
            => throw new System.NotImplementedException();
        
        public override void SetLength(long value)
            => throw new System.NotImplementedException();

        public override void Write(byte[] buffer, int offset, int count)
            => throw new System.NotImplementedException();

        #endregion
    }
}
