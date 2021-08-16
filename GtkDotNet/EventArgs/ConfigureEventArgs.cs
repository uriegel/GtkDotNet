using System;

namespace GtkDotNet
{
    public class ConfigureEventArgs : EventArgs
    {
        public int EventType { get; init; }
        public byte SendEvent { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }     
    }
}