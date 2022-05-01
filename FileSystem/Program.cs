using GtkDotNet;

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    Application.RegisterResources();
    var builder = Builder.FromResource("/org/gtk/example/window.ui");
    Builder.AddFromFile(builder, "builder.ui");
    var window = Builder.GetObject(builder, "window");
    var columnView = Builder.GetObject(builder, "column-view");
    Window.SetApplication(window, app);
    GObject.Unref( builder);
    Widget.Show(window);

    var listStore = ListStore.New(GManaged<FileItem>.Type);

    async void GetFileItems()
    {
        var file = GFile.New("/home/uwe/Dokumente");
        var items = (await GFile.EnumerateChildrenAsync(file, "*", FileQueryInfoFlags.None, 100))
            .Select(n => new FileItem { Name = GFileInfo.GetDisplayName(n), Icon = GFileInfo.GetIcon(n)});
        ListStore.Splice(listStore, items);
    }
    
    GetFileItems();

    var modelFactory = SignalListItemFactory.New();
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelFactory, "setup", (_, listItem, _) => 
    {
        var label = Label.New("");
        ListItem.SetChild(listItem, label);
    });
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelFactory, "bind", (_, listItem, _) => 
    {
        var item = ListItem.GetItem(listItem);
        var val = GManaged<FileItem>.GetValue(item);
        var child = ListItem.GetChild(listItem);
        Label.SetLabel(child, val.Name);
    });

    var modelIconFactory = SignalListItemFactory.New();
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelIconFactory, "setup", (_, listItem, _) => 
    {
        var label = Label.New("");
        ListItem.SetChild(listItem, label);
    });
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelIconFactory, "bind", (_, listItem, _) => 
    {
        var item = ListItem.GetItem(listItem);
        var val = GManaged<FileItem>.GetValue(item);
        var child = ListItem.GetChild(listItem);
        Label.SetLabel(child, $"{val.Icon}");
    });

    var selectionModel = SingleSelection.New(listStore);
    ColumnView.SetModel(columnView, selectionModel);

    var column = ColumnViewColumn.New("Name", modelFactory);
    ColumnViewColumn.SetResizable(column, true);
    ColumnView.AppendColumn(columnView, column);
    column = ColumnViewColumn.New("Icon", modelIconFactory);
    ColumnView.AppendColumn(columnView, column);
    ColumnViewColumn.SetResizable(column, true);
    ColumnView.SetReorderable(columnView, true);
};

var status = Application.Run(app, onActivate);

GObject.Unref(app);

return status;

class FileItem
{
    public string Name {get; init;} = "";
    public IntPtr Icon {get; init;}
}