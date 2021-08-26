namespace GtkDotNet
{
    public class Revealer : Container
    {
        public bool IsRevealed 
        {
            get => Raw.Revealer.GetRevealChild(handle);
            set => Raw.Revealer.SetRevealChild(handle, value);
        }
        public Revealer(GObject obj) : base(obj) {}
    }
}
