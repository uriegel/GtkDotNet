using System;

namespace GtkDotNet;

public class ScriptDialogEventArgs : EventArgs
{
    public string Message { get; init; }
}
