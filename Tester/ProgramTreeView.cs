using System;
using System.Runtime.InteropServices;
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

    app.AddActions(new[] 
    {
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
        treeView = new TreeView(builder.GetObject($"{id}-folder"));
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
            Prepare();
            Fill();
            treeView.GrabFocus();
        };
    }

    public void Prepare()
    {
        using var columns = treeView.Columns;
        foreach (var col in columns.Items)
        {
            var name = col.Title;
            var colCount = treeView.RemoveColumn(col);
            Console.WriteLine($"Col: {name}, colCount: {colCount}");
        }

        var column = new TreeViewColumn();
        var cell = new CellRendererText();
        column.Title = "Name";
        column.Expand = true;
        column.Resizable = true;
        column.PackStart(cell, true);
        column.AddAttribute(cell, "text", 0);
        treeView.AppendColumn(column);

        column = new TreeViewColumn();
        cell = new CellRendererText();
        column.Title = "Datum";
        column.Expand = true;
        column.Resizable = true;
        column.PackStart(cell, true);
        column.AddAttribute(cell, "text", 1);
        treeView.AppendColumn(column);

        column = new TreeViewColumn();
        cell = new CellRendererText();
        column.Title = "Größe";
        column.Expand = true;
        column.Resizable = true;
        column.PackStart(cell, true);
        column.AddAttribute(cell, "text", 2);
        treeView.AppendColumn(column);
    }

    public void Fill()
    {
        //var model = new ListStore(new[] { GType.String, GType.Int });
        var model = new ListStore(new[] { GType.String });

        var dummy = new GValueDummy();
        var affe = Marshal.SizeOf(dummy);
        var gvalue = GtkDotNet.Raw.GValue.Init(ref dummy, GType.String);
        GtkDotNet.Raw.GValue.SetString(gvalue, "Einen wundervollen schönen guten Morgen");
        var zurück = Marshal.PtrToStringUTF8(GtkDotNet.Raw.GValue.GetString(gvalue));

        var dummy2 = new GValueDummy();
        var gvalue2 = GtkDotNet.Raw.GValue.Init(ref dummy2, GType.Int);
        GtkDotNet.Raw.GValue.SetInt(gvalue2, 45);

        //model.InsertWithValues(-1, new[] { 0, 1 }, new[] { gvalue, gvalue2 });
        model.InsertWithValues(-1, new[] { 0 }, new[] { gvalue });
        treeView.SetModel(model);
    }

    public void GrabFocus() => treeView.GrabFocus();

    TreeView treeView;
    Entry entry;
}               