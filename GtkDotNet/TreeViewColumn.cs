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

        public void SetCellCallback(CellRenderer cell)
        {
            cellDataFunc = CellDataCallback;
            var funcptr = Marshal.GetFunctionPointerForDelegate(cellDataFunc);
            Raw.TreeViewColumn.SetCellDataFunc(handle, cell.handle, funcptr, IntPtr.Zero, IntPtr.Zero);        
        }

        void CellDataCallback(IntPtr treeViewColumn, IntPtr cellRenderer, IntPtr treeModelPtr, IntPtr iter, IntPtr data)
        {
            var treeModel = new TreeModel(new GObject(treeModelPtr));
            var value = treeModel.GetInt(iter, 1);
            // TODO: Type, Col position
            var cell = new CellRenderer(new GObject(cellRenderer));
            cell.SetString("text", $"{value} - Das ist ja märchenhaft!");
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CellDataDelegate(IntPtr treeViewColumn, IntPtr cellRenderer, IntPtr treeModel, IntPtr iter, IntPtr data);

        CellDataDelegate cellDataFunc;
    }
}


