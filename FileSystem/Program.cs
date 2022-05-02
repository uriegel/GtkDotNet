﻿using GtkDotNet;

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    Application.RegisterResources();
    var cssProvider = CssProvider.New();
    CssProvider.LoadFromResource(cssProvider, "/org/gtk/example/style.css");
    StyleContext.AddProviderForDisplay(Display.GetDefault(), cssProvider, StyleProviderPriority.Fallback);

    var builder = Builder.FromResource("/org/gtk/example/window.ui");
    var window = Builder.GetObject(builder, "window");
    var columnView = Builder.GetObject(builder, "column-view");
    Window.SetApplication(window, app);
    GObject.Unref( builder);
    Widget.Show(window);

    var listStore = ListStore.New(GManaged<IntPtr>.Type);

    async void GetFileItems()
    {
        var file = GFile.New("/media/uwe/Home/Bilder/Fotos/2017/Abu Dabbab/");
        var items = await GFile.EnumerateChildrenAsync(file, "*", FileQueryInfoFlags.None, 100);
        ListStore.Splice(listStore, items);
    }
    
    GetFileItems();

    var modelFactory = SignalListItemFactory.New();


    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelFactory, "setup", (_, listItem, _) => 
    {
        var listItemBuilder = Builder.FromResource("/org/gtk/example/listitem.ui");
        var label = Builder.GetObject(listItemBuilder, "listitem");
        ListItem.SetChild(listItem, label);
        GObject.Unref(listItemBuilder);
    });
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelFactory, "bind", (_, listItem, _) => 
    {
        var item = ListItem.GetItem(listItem);
        var val = GFileInfo.GetDisplayName(GManaged<IntPtr>.GetValue(item));
        var child = ListItem.GetChild(listItem);
        Label.SetLabel(child, val);
    });
    var modelIconFactory = SignalListItemFactory.New();
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelIconFactory, "setup", (_, listItem, _) => 
    {
        var image = Image.New();
        ListItem.SetChild(listItem, image);
    });
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelIconFactory, "bind", (_, listItem, _) => 
    {
        var item = ListItem.GetItem(listItem);
        var val = GFileInfo.GetIcon(GManaged<IntPtr>.GetValue(item));
        var child = ListItem.GetChild(listItem);
        Image.SetFromGIcon(child, val);
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