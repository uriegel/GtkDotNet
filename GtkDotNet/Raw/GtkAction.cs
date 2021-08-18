using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class GtkAction
    {
        public static bool HandleBoolState(IntPtr action, IntPtr state)
        {
            ActionSetState(action, state);
            return Variant.GetBool(state) != 0;
        }
        public static string HandleStringState(IntPtr action, IntPtr state)
        {
            ActionSetState(action, state);
            var strptr = Variant.GetString(state, IntPtr.Zero);
            return Marshal.PtrToStringAuto(strptr);
        }

        public delegate void StateChangedDelegate(IntPtr action, IntPtr state);

        public GtkAction(string actionName, Action action, string accelerator = null)
        {
            Action = action;
            Name = actionName;
            Accelerator = accelerator;
        }
        public GtkAction(string actionName, bool initialState, StateChangedDelegate stateChanged, string accelerator = null)
        {
            Name = actionName;
            Accelerator = accelerator;
            StateParameterType = null;
            State = initialState;
            StateChanged = stateChanged;
        }
        public GtkAction(string actionName, string initialState, StateChangedDelegate stateChanged, string accelerator = null)
        {
            Name = actionName;
            Accelerator = accelerator;
            StateParameterType = "s";
            State = initialState;
            StateChanged = stateChanged;
        }

        internal string Name;
        internal string Accelerator;
        internal Action Action;
        internal string StateParameterType;
        internal object State;
        internal StateChangedDelegate StateChanged;

        [DllImport(Globals.LibGtk, EntryPoint="g_simple_action_set_state", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ActionSetState(IntPtr action, IntPtr state);
    }
}