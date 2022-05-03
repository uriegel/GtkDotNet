﻿using GtkDotNet;

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    Application.RegisterResources();
    var cssProvider = CssProvider.New();
    CssProvider.LoadFromResource(cssProvider, "/org/gtk/example/style.css");
    StyleContext.AddProviderForDisplay(Display.GetDefault(), cssProvider, StyleProviderPriority.Application);

    var builder = Builder.FromResource("/org/gtk/example/window.ui");
    var window = Builder.GetObject(builder, "window");
    var columnView = Builder.GetObject(builder, "column-view");
    var button = Builder.GetObject(builder, "button");
    Window.SetApplication(window, app);
    GObject.Unref( builder);
    Widget.Show(window);

    var listStore = new ListStore<GObjectRef>();

    async void GetFileItems(string path)
    {
        var file = GFile.New(path);
        var items = (await GFile.EnumerateChildrenAsync(file, "*", FileQueryInfoFlags.None, 100))
            .Select(n => 
            {
                GObject.AddWeakRef(n, (_, _) => Console.WriteLine("Finilisiere"));
                return new GObjectRef(n);
            });
        listStore.Splice(items);
    }

    GetFileItems("/home/uwe");

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
        var item = listStore.GetListItem(listItem);
        var val = GFileInfo.GetDisplayName(item.Value);
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
        var item = listStore.GetListItem(listItem);
        var val = GFileInfo.GetIcon(item.Value);
        var child = ListItem.GetChild(listItem);
        Image.SetFromGIcon(child, val);
    });

    var actions = new GtkAction[] 
    {
        new GtkAction("change-model", () => 
        {
            listStore.RemoveAll();
            //GetFileItems("/media/uwe/Home/Bilder/Fotos/2017/Abu Dabbab/");
        }, "<Ctrl>C")
    };
    Application.AddActions(app, actions);

    var selectionModel = SingleSelection.New(listStore);
    ColumnView.SetModel(columnView, selectionModel);

    var column = ColumnViewColumn.New("Icon", modelIconFactory);
    ColumnView.AppendColumn(columnView, column);
    ColumnView.SetReorderable(columnView, true);
    column = ColumnViewColumn.New("Name", modelFactory);
    ColumnViewColumn.SetResizable(column, true);
    ColumnViewColumn.SetExpand(column, true);
    ColumnView.AppendColumn(columnView, column);
};

var status = Application.Run(app, onActivate);

GObject.Unref(app);

GC.Collect();
GC.Collect();
Thread.Sleep(1000);

return status;

class FileItem
{
    public string Name {get; init;} = "";
    public IntPtr Icon {get; init;}
}

class Test
{
    public string Text { get;}

    public Test(string text) => Text = text;
    ~Test()
    {
        Console.WriteLine("Finalisiert");
    }
}