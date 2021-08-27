using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class StyleContext
    {
        [DllImport(Globals.LibGtk, EntryPoint = "gtk_style_context_add_provider_for_screen", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddProviderForScreen(IntPtr screen, IntPtr provider, StyleProviderPriority priority);
    }
}
