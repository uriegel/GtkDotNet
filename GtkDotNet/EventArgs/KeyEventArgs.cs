using System;

namespace GtkDotNet;

public class KeyEventArgs : EventArgs
{
    public EventKey Key { get; }
    public bool Cancel { get; set; }

    public KeyEventArgs(EventKey key) => Key = key;
}
