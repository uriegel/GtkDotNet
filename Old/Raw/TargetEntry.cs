using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class TargetEntry
    {
        [Flags]
        public enum Flags
        {
            SameApp = 1,
            SameWidget = 2,
            OtherApp = 4,
            OtherWidget = 8
        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_target_entry_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr New(string identifier, Flags flags, int id);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_target_entry_free", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Free(IntPtr targetEntry);
    }
}

