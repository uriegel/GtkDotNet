using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Gtk
    {
        public static void Init() 
        {
            var c = 0;
            var args = IntPtr.Zero;
            init(ref c, ref args);
        }

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_main", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Main();

        [DllImport(Globals.LibGtk, EntryPoint = "gtk_init", CallingConvention = CallingConvention.Cdecl)]
        private extern static void init (ref int argc, ref IntPtr argv);
    }
}


 

