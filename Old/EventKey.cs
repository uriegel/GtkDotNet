using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    [Flags]
    public enum ModifierType
    {
        Shift = 1,
        Lock = 2,
        Control = 4,
        Alt = 8,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EventKey
    {
        public int type;  // is it a key-press or key release event
        /// the window receiving the event
        IntPtr Window; 
        /// whether the event was sent explicitly. It's TRUE in that case
        public byte SendEvent;  
        /// time of event in ms
        public int Time;    
        /// a bit mask representing state of (Ctrl/Alt/Shift)
        public ModifierType State;    
        /// the key that was pressed 
        public uint KeyVal;   
        /// length of string
        int Length;    
        /// the string that may result from this kepress
        public IntPtr String;   
        public ushort HardwareKeycode;
        public byte KeyboardGroup;
        /// : 1;  whether the key is mapped as modifier
        public int IsModifier;
    }
}