using System;

namespace GtkDotNet;

public class DragDataGetEventArgs : EventArgs
{
    public SelectionData SelectionData { get; internal set; }
}
