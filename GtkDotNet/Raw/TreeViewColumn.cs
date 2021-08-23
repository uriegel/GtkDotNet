using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class TreeViewColumn
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_column_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New();
        
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_column_set_title", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetTitle(IntPtr handle, string title);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_column_get_title", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetTitle(IntPtr handle);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_column_set_resizable", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetResizable(IntPtr handle, bool resizable);
        
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_column_get_resizable", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool GetResizable(IntPtr handle);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_column_set_expand", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetExpand(IntPtr handle, bool expand);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_column_get_expand", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool GetExpand(IntPtr handle);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_cell_layout_pack_start", CallingConvention = CallingConvention.Cdecl)]
        public extern static void PackStart(IntPtr handle, IntPtr cell, bool expand);

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_cell_layout_add_attribute", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddAttribute(IntPtr handle, IntPtr cell, string attribute, int column);


        [DllImport(Globals.LibGtk, EntryPoint = "gtk_tree_view_column_set_cell_data_func", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetCellDataFunc(IntPtr handle, IntPtr cell, IntPtr callback, IntPtr zero, IntPtr zero2);
}
}