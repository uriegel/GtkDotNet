using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class ListStore : GObject
    {
        public ListStore(GType[] types) : base(Raw.ListStore.New(types.Length, types)) { }

        public void InsertWithValues(int position, int[] columns, IntPtr[] values)
            => Raw.ListStore.InsertWithValues(handle, IntPtr.Zero, position, columns, values[0], columns.Length);

    }

    [StructLayout(LayoutKind.Sequential)]
    public        struct GValueDummy
    {
        long z;
        long z2;
        long z3;
    }
}
