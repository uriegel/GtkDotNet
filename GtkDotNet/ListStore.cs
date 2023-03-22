using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GtkDotNet;

public class ListStore : GObject
{
    public ListStore(GType[] types) : base(Raw.ListStore.New(types.Length, types))
        => this.types = types;


    public void Append(params object[] items)
    {
        int index = 0;
        var intPtr = Marshal.AllocHGlobal(24 * items.Length);
        Marshal.Copy(new byte[24 * items.Length], 0, intPtr, 24 * items.Length);

        var columns = Enumerable.Range(0, items.Length).ToArray();
        foreach (var item in items)
        {
            var type = types[index];
            var nextPtr = intPtr + 24 * index;
            switch (type)
            {
                case GType.Int:
                    GtkDotNet.Raw.GValue.Init(nextPtr, GType.Int);
                    GtkDotNet.Raw.GValue.SetInt(nextPtr, (int)items[index]);
                    break;
                case GType.String:
                    GtkDotNet.Raw.GValue.Init(nextPtr, GType.String);
                    GtkDotNet.Raw.GValue.SetString(nextPtr, (string)items[index]);
                    break;
            }
            index++;
        }
        Raw.ListStore.InsertWithValues(handle, IntPtr.Zero, -1, columns, intPtr, columns.Length);
        for (var i = 0; i < items.Length; i++)
            GtkDotNet.Raw.GValue.Unset(intPtr + 24 * i);

        Marshal.FreeHGlobal(intPtr);
    }

    GType[] types;
}

