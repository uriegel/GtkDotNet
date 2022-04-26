using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class Entry : Widget
    {
        public string Text
        {
            get => Marshal.PtrToStringUTF8(Raw.Entry.GetText(handle));
            set => Raw.Entry.SetText(handle, value);
        }
        
        public Entry(GObject obj) : base(obj) {}
    }
}
