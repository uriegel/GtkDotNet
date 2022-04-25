namespace GtkDotNet
{
    public class TreeView : Container
    {
        public TreeView(GObject obj) : base(obj) { }

        public int AppendColumn(TreeViewColumn column) => Raw.TreeView.AppendColumn(handle, column.handle);

        public TreeViewColumns Columns { get => new TreeViewColumns(Raw.TreeView.GetColumns(handle)); }

        public int RemoveColumn(TreeViewColumn column) => Raw.TreeView.RemoveColumn(handle, column.handle);

        public int SetModel(GtkListStore model) => Raw.TreeView.SetModel(handle, model.handle);
    }
}
