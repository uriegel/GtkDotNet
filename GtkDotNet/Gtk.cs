using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ConfigureEvent
    {
        public int EventType;
        public IntPtr Window;
        public byte SendEvent;
        public int X;
        public int Y;
        public int Width;
        public int Height;            
    }

    public class Gtk
    {
        public static void Init() 
        {
            var c = 0;
            var args = IntPtr.Zero;
            init(ref c, ref args);
        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_main", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Main();

        [DllImport(Globals.LibGtk, EntryPoint="gtk_main_quit", CallingConvention = CallingConvention.Cdecl)]
        public extern static void MainQuit();

        public static void SignalConnect<TDelegate>(IntPtr widget, string name, TDelegate callback) where TDelegate : Delegate
        {
            var delegat = callback as Delegate;
            Delegates.Add(delegat);
            SignalConnect(widget, name, Marshal.GetFunctionPointerForDelegate(callback), IntPtr.Zero, IntPtr.Zero, 0);
        }
            
        [DllImport(Globals.LibGtk, EntryPoint="gtk_init", CallingConvention = CallingConvention.Cdecl)]
        private extern static void init (ref int argc, ref IntPtr argv);

        [DllImport(Globals.LibGtk, EntryPoint="g_signal_connect_data", CallingConvention = CallingConvention.Cdecl)]
        private extern static void SignalConnect(IntPtr widget, string name, IntPtr callback, IntPtr n, IntPtr n2, int n3);
    }
}


 

