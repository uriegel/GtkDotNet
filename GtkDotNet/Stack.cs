using System;
using System.Runtime.InteropServices;

namespace GtkDotNet;

public class Stack
{
    [DllImport(Globals.LibGtk, EntryPoint="gtk_stack_add_titled", CallingConvention = CallingConvention.Cdecl)]
    public extern static void AddTitled(IntPtr stack, IntPtr widget, string name, string title);
}

