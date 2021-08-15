using System;
using System.Runtime.InteropServices;
using static GtkDotNet.Dialog;

namespace GtkDotNet.Raw
{
    public class Dialog
    {
        [DllImport(Globals.LibGtk, EntryPoint="gtk_file_chooser_dialog_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewFileChooser(string title, IntPtr parent, FileChooserAction action, 
            string firstButtonText, ResponseId firstButtonId, string secondButtonText, ResponseId secondButtonId, IntPtr zero);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_dialog_run", CallingConvention = CallingConvention.Cdecl)]
        public extern static ResponseId Run(IntPtr dialog);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_file_chooser_get_filename", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr FileChooserGetFileName(IntPtr dialog);
        
    }
}
