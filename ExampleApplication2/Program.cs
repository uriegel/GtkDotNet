using System;
using System.Linq;
using GtkDotNet;

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    Application.RegisterResources();
    var builder = Builder.FromResource("/org/gtk/example/window.ui");
    Builder.AddFromFile(builder, "builder.ui");
    var window = Builder.GetObject(builder, "window");
    Window.SetApplication(window, app);
    GObject.Unref( builder);
    Widget.Show(window);

    var files = System.Environment.CommandLine.Split(' ').Skip(1).Select(GFile.New);
    var file = files.First();
    var name = GFile.GetBasename(file);
};

var status = Application.Run(app, onActivate);

GObject.Unref(app);

return status;

