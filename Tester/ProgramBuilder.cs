//#define ProgramBuilder
#if ProgramBuilder

using System;
using GtkDotNet.Raw;

Console.WriteLine("Hello Gtk 4");

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    var builder = Builder.New();
    Builder.AddFromFile(builder, "builder.ui", IntPtr.Zero);

    var window = Builder.GetObject(builder, "window");
    Window.SetApplication(window, app);
    
    Widget.Show(window);
    GObject.Unref(builder);
};


var status = Application.Run(app, onActivate);

GObject.Unref(app);

return status;

#endif