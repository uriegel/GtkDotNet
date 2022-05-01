using System;
using GtkDotNet;

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    Application.RegisterResources();
    var builder = Builder.FromResource("/org/gtk/example/window.ui");
    Builder.AddFromFile(builder, "builder.ui");
    var window = Builder.GetObject(builder, "window");
    var listView = Builder.GetObject(builder, "list-view");
    Window.SetApplication(window, app);
    GObject.Unref( builder);
    Widget.Show(window);

    var listStore = ListStore.New(GManaged<string>.Type);

    var items = Enumerable.Range(0, 200_000).Select(n => $"Item # {n}");
    ListStore.Splice(listStore, items);

    var item = ListStore.GetObject(listStore, 1);
    var val = GManaged<string>.GetValue(item);
    GObject.Unref(item);

    var modelFactory = SignalListItemFactory.New();
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelFactory, "setup", (_, listItem, _) => 
    {
        var label = Label.New("");
        ListItem.SetChild(listItem, label);
    });
    Gtk.SignalConnect<SignalListItemFactory.Delegate>(modelFactory, "bind", (_, listItem, _) => 
    {
        var item = ListItem.GetItem(listItem);
        var val = GManaged<string>.GetValue(item);
        var child = ListItem.GetChild(listItem);
        Label.SetLabel(child, $"Eintrag # {val}");
    });

    var selectionModel = SingleSelection.New(listStore);
    ListView.SetModel(listView, selectionModel);
    ListView.SetFactory(listView, modelFactory);
};

var status = Application.Run(app, onActivate);

GObject.Unref(app);

return status;

