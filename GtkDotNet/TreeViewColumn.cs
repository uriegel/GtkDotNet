using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class TreeViewColumn : GObject
    {
        public string Title 
        {
            get => Marshal.PtrToStringUTF8(Raw.TreeViewColumn.GetTitle(handle));
            set => Raw.TreeViewColumn.SetTitle(handle, value);
        }

        public bool Resizable 
        {
            get => Raw.TreeViewColumn.GetResizable(handle);
            set => Raw.TreeViewColumn.SetResizable(handle, value);
        }

        public bool Expand
        {
            get => Raw.TreeViewColumn.GetExpand(handle);
            set => Raw.TreeViewColumn.SetExpand(handle, value);
        }

        public TreeViewColumn() : base(Raw.TreeViewColumn.New()) { }
        internal TreeViewColumn(IntPtr cols) : base(cols) { }

        public void PackStart(CellRenderer cell, bool expand)
            => Raw.TreeViewColumn.PackStart(handle, cell.handle, expand);

        public void AddAttribute(CellRenderer cell, string attribute, int column)
            => Raw.TreeViewColumn.AddAttribute(handle, cell.handle, attribute, column);        
    }
}


