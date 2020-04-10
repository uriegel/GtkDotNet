using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public abstract class GtkActionBase
    {
        internal string Name;
        internal string Accelerator;
    }

    public class GtkAction : GtkActionBase
    {
        public GtkAction(string actionName, Action action, string accelerator = null)
        {
            Action = action;
            Name = actionName;
            Accelerator = accelerator;
        }

        internal Action Action;
    }

    public class GtkBoolStateAction : GtkActionBase
    {
        public delegate void ChangeBoolStateFunc(IntPtr state);

        public GtkBoolStateAction(string actionName, bool initialState, ChangeBoolStateFunc changeState, string accelerator = null)
        {
            Name = actionName;
            Accelerator = accelerator;
            StateParameterType = "b";
            State = initialState;
            ChangeState = changeState;
        }

        internal string StateParameterType;
        internal bool State;
        internal ChangeBoolStateFunc ChangeState;
    }


    [StructLayout(LayoutKind.Sequential)]
    struct AppEntry
    {
        public AppEntry(string actionName, Delegate action)
        {
            ActionName = actionName;
            Action = Marshal.GetFunctionPointerForDelegate(action);
            ParameterType = null;
            State = null;
            ChangeState = IntPtr.Zero;
            Nil1 = IntPtr.Zero;
            Nil2 = IntPtr.Zero;
            Nil3 = IntPtr.Zero;
        }
        public AppEntry(string actionName, Delegate changeState, string parameterType, string initialState)
        {
            ActionName = actionName;
            Action = IntPtr.Zero;
            ParameterType = parameterType;
            State = initialState;
            ChangeState = Marshal.GetFunctionPointerForDelegate(changeState);
            Nil1 = IntPtr.Zero;
            Nil2 = IntPtr.Zero;
            Nil3 = IntPtr.Zero;
        }
        string ActionName;
        internal IntPtr Action;
        string ParameterType;
        string State;
        internal IntPtr ChangeState;
        IntPtr Nil1;
        IntPtr Nil2;
        IntPtr Nil3;
    }

    public class Application
    {
        public static IntPtr New(string id) => _New(id, 0);

        delegate void StateChangedDelegate(IntPtr action, IntPtr state);

        static void StateChanged(IntPtr action, IntPtr state)
        {
            Console.WriteLine("Bin gedrückt");
            //ActionSetState(action, NewBool(0));
            ActionSetState(action, state);
        }
        
        public static int Run(IntPtr app, Action onActivate) 
        {
            Gtk.SignalConnect(app, "activate", onActivate);
            return _Run(app, 0, IntPtr.Zero);
        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_application_add_window", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddWindow(IntPtr app, IntPtr window);

        [DllImport(Globals.LibGtk, EntryPoint="g_application_quit", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Quit(IntPtr app);

        public static void AddActions(IntPtr app, IEnumerable<GtkActionBase> actions)
        {
            var gtkActions = actions.OfType<GtkAction>();
            foreach (var action in gtkActions)
                Delegates.Add(action.Action);
            var appEntries = gtkActions.Select(n => new AppEntry(n.Name, n.Action)).ToArray();  
            Application.AddEntriesToActionMap(app, appEntries, appEntries.Length, IntPtr.Zero);

            var boolStateActions = actions.OfType<GtkBoolStateAction>();
            foreach (var action in boolStateActions)
            {
                Delegates.Add(action.ChangeState);
                var simpleAction = NewStatefulAction(action.Name, IntPtr.Zero, NewBool(-1));
                Gtk.SignalConnect<StateChangedDelegate>(simpleAction, "change-state", StateChanged);
                AddAction(app, simpleAction);
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

        [DllImport(Globals.LibGtk, EntryPoint="g_action_map_add_action_entries", CallingConvention = CallingConvention.Cdecl)]
        extern static void AddEntriesToActionMap(IntPtr app, [In, MarshalAs(UnmanagedType.LPArray)] AppEntry[] appEntries, int count, IntPtr nil);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_application_set_accels_for_action", CallingConvention = CallingConvention.Cdecl)]
        extern static void SetAccelsForAction(IntPtr app, string action, [In] string[] accels);

        [DllImport(Globals.LibGtk, EntryPoint="g_simple_action_new_stateful", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr NewStatefulAction(string action, IntPtr p, IntPtr state);

        [DllImport(Globals.LibGtk, EntryPoint="g_variant_new_boolean", CallingConvention = CallingConvention.Cdecl)]
        extern static IntPtr NewBool(int value);

        [DllImport(Globals.LibGtk, EntryPoint="g_action_map_add_action", CallingConvention = CallingConvention.Cdecl)]
        extern static void AddAction(IntPtr app, IntPtr action);
        
        [DllImport(Globals.LibGtk, EntryPoint="g_simple_action_set_enabled", CallingConvention = CallingConvention.Cdecl)]
        extern static void EnableAction(IntPtr action, int enabled);

        [DllImport(Globals.LibGtk, EntryPoint="g_simple_action_set_state", CallingConvention = CallingConvention.Cdecl)]
        extern static void ActionSetState(IntPtr action, IntPtr state);
    }
}
