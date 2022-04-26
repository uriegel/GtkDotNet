namespace GtkDotNet
{
    public class Revealer : Widget
    {
        public bool IsRevealed 
        {
            get => Raw.Revealer.GetRevealChild(handle);
            set => Raw.Revealer.SetRevealChild(handle, value);
        }
        public Revealer(GObject obj) : base(obj) {}
    }
}
