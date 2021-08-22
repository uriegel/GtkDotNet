using System;
using GtkDotNet;

var app = new Application("de.uriegel.test");
app.Run(() =>
{
    app.RegisterResources();
    using var builder = Builder.FromResource("/de/uriegel/test/listview.glade");

    var window = new Window(builder.GetObject("window"));
    var viewer = new Widget(builder.GetObject("viewer"));
    var leftFolder = new Folder(builder, "left");
    var rightFolder = new Folder(builder, "right");

    leftFolder.ShiftFocus += (s,_e) => rightFolder.GrabFocus();
    rightFolder.ShiftFocus += (s,_e) => leftFolder.GrabFocus();

    leftFolder.GrabFocus();
    viewer.Visible = false;

    app.AddActions(new[] {
        new GtkAction("destroy", window.Close, "<Ctrl>Q"), 
        new GtkAction("viewer", false, v => viewer.Visible = v, "F3"),
    });        

    using var iconInfo = IconInfo.Choose(".pdf", 64, IconLookup.ForceSvg);
    var iconFile = iconInfo.GetFileName();

    app.AddWindow(window);
    window.Visible = true;
});

class Folder
{
    public event EventHandler ShiftFocus;
    public Folder(Builder builder, string id)
    {
        treeView = new Widget(builder.GetObject($"{id}-folder"));
        treeView.KeyPress += (w, e) => 
        {
            if (e.Key.HardwareKeycode == 23 && (e.Key.State & ModifierType.Shift) != ModifierType.Shift) // tab
            {
                ShiftFocus?.Invoke(this, EventArgs.Empty);
                e.Cancel = true; 
            }
        };

        entry = new Entry(builder.GetObject($"{id}-entry"));
        entry.Activate += (s, e) =>
        {
            Console.WriteLine($"Activated: {entry.Text}");
            treeView.GrabFocus();
        };
    }

    public void GrabFocus() => treeView.GrabFocus();

    Widget treeView;
    Entry entry;
}               