using System;

namespace GtkDotNet;

public class LoadChangedEventArgs : EventArgs
{
    public WebKitLoadEvent LoadEvent { get; init; }
}
