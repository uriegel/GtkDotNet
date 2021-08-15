using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class DragDrop
    {
        [Flags]
        public enum DefaultDestination
        {
            Motion = 1,
            Highlight = 2,
            Drop = 4
        }

        [Flags]
        public enum DragActions
        {
            Default = 1,
            Copy = 2,
            Move = 4, 
            Link = 8,
            Private = 0x10,
            Ask = 0x20
        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_drag_dest_set", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetDestination(IntPtr widget, DefaultDestination defaultDestination, IntPtr targets, int targetCount, DragActions actions);
        [DllImport(Globals.LibGtk, EntryPoint="gtk_drag_dest_unset", CallingConvention = CallingConvention.Cdecl)]
        public extern static void UnSet(IntPtr widget);
        
    }
}
