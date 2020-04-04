using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public struct GtkAction
    {
        public GtkAction(string actionName, Action action, string accelerator = null)
        {
            Action = action;
            Name = actionName;
            Accelerator = accelerator;
        }

        internal Action Action;
        internal string Name;
        internal string Accelerator;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct AppEntry
    {
        public AppEntry(string actionName, Delegate action)
        {
            ActionName = actionName;
            Action = Marshal.GetFunctionPointerForDelegate(action);
            Nil1 = IntPtr.Zero;
            Nil2 = IntPtr.Zero;
            Nil3 = IntPtr.Zero;
            Nil4 = IntPtr.Zero;
            Nil5 = IntPtr.Zero;
            Nil6 = IntPtr.Zero;
        }
        string ActionName;
        IntPtr Action;
        IntPtr Nil1;
        IntPtr Nil2;
        IntPtr Nil3;
        IntPtr Nil4;
        IntPtr Nil5;
        IntPtr Nil6;
    }

    public class Application
    {
        public static IntPtr New(string id) => _New(id, 0);
        
        public static int Run(IntPtr app, Action onActivate) 
        {
            Gtk.SignalConnect(app, "activate", onActivate);
            return _Run(app, 0, IntPtr.Zero);
        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_application_add_window", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddWindow(IntPtr app, IntPtr window);

        [DllImport(Globals.LibGtk, EntryPoint="g_application_quit", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Quit(IntPtr app);

        public static void AddActions(IntPtr app, IEnumerable<GtkAction> actions)
        {

            var appEntries = actions.Select(n => new AppEntry(n.Name, n.Action)).ToArray();  
            Application.AddEntriesToActionMap(app, appEntries, appEntries.Length, IntPtr.Zero);

            var accelEntries = 
                actions
                .Where(n => n.Accelerator != null)
                .Select(n => new { Name = "app." + n.Name, n.Accelerator});  
            foreach (var accelEntry in accelEntries)
                Application.SetAccelsForAction(app, accelEntry.Name, new [] { accelEntry.Accelerator, null});

        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_application_new", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr _New(string id, int flags);

        [DllImport(Globals.LibGtk, EntryPoint="g_application_run", CallingConvention = CallingConvention.Cdecl)]
        extern static int _Run(IntPtr app, int c, IntPtr a);

        [DllImport(Globals.LibGtk, EntryPoint="g_action_map_add_action_entries", CallingConvention = CallingConvention.Cdecl)]
        extern static void AddEntriesToActionMap(IntPtr app, [In, MarshalAs(UnmanagedType.LPArray)] AppEntry[] appEntries, int count, IntPtr nil);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_application_set_accels_for_action", CallingConvention = CallingConvention.Cdecl)]
        extern static void SetAccelsForAction(IntPtr app, string action, [In] string[] accels);
    }
}
