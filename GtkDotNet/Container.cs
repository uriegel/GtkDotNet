namespace GtkDotNet
{
    public class Container : Widget 
    {
        public Container(GObject obj) : base(obj) {}

        public void Add(Widget widget)
            => Raw.Container.Add(handle, widget.handle);
    }
}
