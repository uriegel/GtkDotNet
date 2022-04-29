using System;

namespace GtkDotNet;

[Flags]
public enum BindFlags
{
    Default = 1,
    Get = 2,
    Set = 4, 
    NoSensitivity = 8,
    GetNoChanges = 16,
    InvertBoolean = 32,
}