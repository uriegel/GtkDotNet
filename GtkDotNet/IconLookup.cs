using System;

namespace GtkDotNet
{
    [Flags]
    public enum IconLookup
    {
        None = 0,
        NoSvg = 1,
        ForceSvg = 2,
        UseBuildin = 4,
        GenericFallback = 8,
        ForceSize = 16,
        ForceRegular = 32,
        ForceSymbolic = 64,
    }
}