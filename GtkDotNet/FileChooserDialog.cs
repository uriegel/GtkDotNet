using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class FileChooserDialog : Dialog
    {
        public string FileName 
        { 
            get
            {
                var ptr = GtkDotNet.Raw.Dialog.FileChooserGetFileName(handle);
                string file = Marshal.PtrToStringUTF8(ptr);
                GtkDotNet.Raw.GObject.Free(ptr);
                return file;
            }
        }
        public FileChooserDialog(string title, Window parent, FileChooserAction action, 
            string firstButtonText, ResponseId firstButtonId, string secondButtonText, ResponseId secondButtonId)
                : base(Raw.Dialog.NewFileChooser(title, parent.handle, action, 
                    firstButtonText, firstButtonId, secondButtonText, secondButtonId, IntPtr.Zero))
                    {} 

    }
}
