
using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class TreeView
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_append_column", CallingConvention = CallingConvention.Cdecl)]
        public extern static int AppendColumn(IntPtr treeView, IntPtr column);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_get_columns", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetColumns(IntPtr treeView);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_remove_column", CallingConvention = CallingConvention.Cdecl)]
        public extern static int RemoveColumn(IntPtr treeView, IntPtr col);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_set_model", CallingConvention = CallingConvention.Cdecl)]
        public extern static int SetModel(IntPtr treeView, IntPtr model);
    }
}
