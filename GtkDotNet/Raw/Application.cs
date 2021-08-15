using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
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
            var gtkActions = actions.OfType<GtkAction>();
            foreach (var action in gtkActions)
            {
                if (action.Action != null)
                {
                    Delegates.Add(action.Action);
                    var simpleAction = NewAction(action.Name, null);
                    Gtk.SignalConnect<Action>(simpleAction, "activate", action.Action);
                    AddAction(app, simpleAction);                    
                }
                else 
                {
                    Delegates.Add(action.StateChanged);
                    var state = action.StateParameterType == "s" 
                        ? NewString(action.State as string)
                        : NewBool((bool)action.State == true ? -1 : 0);
                    var simpleAction = NewStatefulAction(action.Name, action.StateParameterType, state);
                    Gtk.SignalConnect<GtkAction.StateChangedDelegate>(simpleAction, "change-state", action.StateChanged);
                    AddAction(app, simpleAction);
                }
            }

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

        [DllImport(Globals.LibGtk, EntryPoint="gtk_application_set_accels_for_action", CallingConvention = CallingConvention.Cdecl)]
        extern static void SetAccelsForAction(IntPtr app, string action, [In] string[] accels);

        [DllImport(Globals.LibGtk, EntryPoint="g_simple_action_new", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr NewAction(string action, string p);

        [DllImport(Globals.LibGtk, EntryPoint="g_simple_action_new_stateful", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr NewStatefulAction(string action, string p, IntPtr state);

        [DllImport(Globals.LibGtk, EntryPoint="g_variant_new_boolean", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr NewBool(int value);

        [DllImport(Globals.LibGtk, EntryPoint="g_variant_new_int32", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr NewInt(int value);

        [DllImport(Globals.LibGtk, EntryPoint="g_variant_new_string", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr NewString(string value);

        [DllImport(Globals.LibGtk, EntryPoint="g_action_map_add_action", CallingConvention = CallingConvention.Cdecl)]
        extern static void AddAction(IntPtr app, IntPtr action);
        
        [DllImport(Globals.LibGtk, EntryPoint="g_simple_action_set_enabled", CallingConvention = CallingConvention.Cdecl)]
        extern static void EnableAction(IntPtr action, int enabled);
    }
}
