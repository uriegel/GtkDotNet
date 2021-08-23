using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class ListStore : GObject
    {
        public ListStore(GType[] types) : base(Raw.ListStore.New(types.Length, types))
            => this.types = types;


        public void Append(params object[] items)
        {
            int index = 0;
            var dummies = new Raw.GValue.GValueDummy[items.Length];
            var columns = Enumerable.Range(0, items.Length).ToArray();
            foreach (var item in items)
            {
                var type = types[index];
                dummies[index] = new Raw.GValue.GValueDummy();
                switch (type)
                {
                    case GType.Int:
                        GtkDotNet.Raw.GValue.Init(ref dummies[index], GType.Int);
                        GtkDotNet.Raw.GValue.SetInt(ref dummies[index], (int)items[index]);
                        break;
                    case GType.String:
                        GtkDotNet.Raw.GValue.Init(ref dummies[index], GType.String);
                        GtkDotNet.Raw.GValue.SetString(ref dummies[index], (string)items[index]);
                        break;
                }
                index++;
            }
            var intPtr = Marshal.AllocHGlobal(24 * items.Length);
            index = 0;
            foreach (var dummy in dummies)
            {
                Marshal.Copy(new[] { dummy.Part1, dummy.Part2, dummy.Part3 }, 0, intPtr + (index * 3 * 8), 3);
                index++;
            }
            Raw.ListStore.InsertWithValues(handle, IntPtr.Zero, -1, columns, intPtr, columns.Length);
            Marshal.FreeHGlobal(intPtr);
        }

        GType[] types;
    }
}
