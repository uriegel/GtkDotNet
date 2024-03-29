using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
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
            var id = SignalConnect(widget, name, Marshal.GetFunctionPointerForDelegate(callback), IntPtr.Zero, IntPtr.Zero, 0);
            Delegates.Add(id, delegat);
        }

        public static void SignalConnectObject<TDelegate>(IntPtr widget, string name, TDelegate callback, IntPtr obj) where TDelegate : Delegate
        {
            var delegat = callback as Delegate;
            var id = SignalConnect(widget, name, Marshal.GetFunctionPointerForDelegate(callback), obj, 0);
            Delegates.Add(id, delegat);
        }

        public static void SignalDisconnect<TDelegate>(IntPtr widget, string name, TDelegate callback) where TDelegate : Delegate
        {
            var delegat = callback as Delegate;
            var id = Delegates.Remove(delegat);
            SignalDisconnect(widget, id);
        }            

        public static string GuessContentType(string filename)
        {
            var ptr = Gtk.GuessContentType(filename, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            var text = Marshal.PtrToStringUTF8(ptr);
            GObject.Free(ptr);
            return text;
        }

        [DllImport(Globals.LibGtk, EntryPoint="g_idle_add_full", CallingConvention = CallingConvention.Cdecl)]
        public extern static void IdleAddFull(int priority, IntPtr func, IntPtr nil, IntPtr nil2);

        [DllImport(Globals.LibGtk, EntryPoint="g_content_type_guess", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr GuessContentType(string filename, IntPtr nil1,  IntPtr nil2, IntPtr nil3);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_init", CallingConvention = CallingConvention.Cdecl)]
        extern static void init (ref int argc, ref IntPtr argv);

        [DllImport(Globals.LibGtk, EntryPoint="g_signal_connect_data", CallingConvention = CallingConvention.Cdecl)]
        extern static long SignalConnect(IntPtr widget, string name, IntPtr callback, IntPtr n, IntPtr n2, int n3);
        [DllImport(Globals.LibGtk, EntryPoint="g_signal_connect_object", CallingConvention = CallingConvention.Cdecl)]
        extern static long SignalConnect(IntPtr widget, string name, IntPtr callback, IntPtr obj, int n3);
        
        [DllImport(Globals.LibGtk, EntryPoint="g_signal_handler_disconnect", CallingConvention = CallingConvention.Cdecl)]
        extern static void SignalDisconnect(IntPtr widget, long id);
    }
}


 

