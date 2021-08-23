using System;

namespace GtkDotNet
{
    public class TreeModel : GObject
    {
        public TreeModel(GObject obj) : base(obj) { }

        public int GetInt(IntPtr iter, int pos)
        {
            int value = 0;
            Raw.TreeModel.ModelGetInt(handle, iter, pos, ref value, -1);
            return value;
        }
    }
}
