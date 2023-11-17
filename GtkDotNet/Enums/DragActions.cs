using System;

[Flags]
public enum DragActions
{
    Default = 1,
    Copy = 2,
    Move = 4,
    Link = 8,
    Private = 0x10,
    Ask = 0x20
}
