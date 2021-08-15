using System;

namespace GtkDotNet
{
    public class GtkAction
    {
        public GtkAction(string actionName, Action action, string accelerator = null)
        {
            Action = action;
            Name = actionName;
            Accelerator = accelerator;
        }
        public GtkAction(string actionName, bool initialState, BoolStateChangedDelegate stateChanged, string accelerator = null)
        {
            Name = actionName;
            Accelerator = accelerator;
            StateParameterType = null;
            State = initialState;
            StateChanged = (a, s) => 
            {
                var state = GtkDotNet.Raw.GtkAction.HandleBoolState(a, s);
                stateChanged(state);
            };
        }
        public GtkAction(string actionName, string initialState, StringStateChangedDelegate stateChanged, string accelerator = null)
        {
            Name = actionName;
            Accelerator = accelerator;
            StateParameterType = "s";
            State = initialState;
            StateChanged = (a, s) => 
            {
                var state = GtkDotNet.Raw.GtkAction.HandleStringState(a, s);
                stateChanged(state);
            };
        }

        public delegate void BoolStateChangedDelegate(bool newState);
        public delegate void StringStateChangedDelegate(string newState);

        readonly internal string Name;
        readonly internal string Accelerator;
        readonly internal Action Action;
        readonly internal string StateParameterType;
        readonly internal object State;
        readonly internal GtkDotNet.Raw.GtkAction.StateChangedDelegate StateChanged;
    }
}
