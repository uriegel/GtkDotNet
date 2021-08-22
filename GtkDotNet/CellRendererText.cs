namespace GtkDotNet
{
    public class CellRendererText : CellRenderer 
    {
        public CellRendererText() : base(new GObject(Raw.CellRendererText.New())) {}
    }
}
