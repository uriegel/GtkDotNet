//#define Program
#if Program

using System;
using GtkDotNet;

Console.WriteLine("Hello Gtk 4");

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    var window = Application.NewWindow(app);
    Window.SetTitle(window, "Listbox 👍");
    Window.SetDefaultSize(window, 600, 300);

    var listStore = ListStore.New(GManaged<string>.Type);
    var count = 200_000;
    var ints = new IntPtr[count];
    for (var i = 0; i < count; i++)
        ints[i] = GManaged<string>.New($"Item # {i}");
    ListStore.Splice(listStore, 0, 0, ints, ints.Length);
    for (var i = 0; i < count; i++)
        GObject.Unref(ints[i]);
    
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
    var listView = ListView.New(selectionModel, modelFactory);
  
    var scrolledWindow = ScrolledWindow.New();
    ScrolledWindow.SetPolicy(scrolledWindow, GtkDotNet.PolicyType.Never, GtkDotNet.PolicyType.Automatic);
    ScrolledWindow.SetMinContentWidth(scrolledWindow, 360);
    ScrolledWindow.SetChild(scrolledWindow, listView);
    Window.SetChild(window, scrolledWindow);

    Widget.Show(window);
};

var result =  Application.Run(app, onActivate);
return result;

#endif