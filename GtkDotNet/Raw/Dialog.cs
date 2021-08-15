using System;
using System.Runtime.InteropServices;

namespace GtkDotNet.Raw
{
    public class Dialog
    {
        public enum FileChooserAction
        {
            Open,
            Save, 
            SelectFolder,
            CreateFolder
        }

        public enum ResponseId 
        {
            None = -1,
            Reject = -2,
            Accept = -3,
            DeleteEvent = -4,
            Ok = -5,
            Cancel = -6,
            Close = -7,
            Yes = -8,
            No = -9,
            Apply = -10,
            Help = -11            
        }

        [DllImport(Globals.LibGtk, EntryPoint="gtk_file_chooser_dialog_new", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NewFileChooser(string title, IntPtr parent, FileChooserAction action, 
            string firstButtonText, ResponseId firstButtonId, string secondButtonText, ResponseId secondButtonId, IntPtr zero);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_dialog_run", CallingConvention = CallingConvention.Cdecl)]
        public extern static ResponseId Run(IntPtr dialog);

        [DllImport(Globals.LibGtk, EntryPoint="gtk_file_chooser_get_filename", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr FileChooserGetFileName(IntPtr dialog);
        
    }
}
