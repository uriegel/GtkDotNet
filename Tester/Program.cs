//#define Program
#if Program

using System;
using GtkDotNet.Raw;

Console.WriteLine("Hello Gtk 4");

var app = Application.New("org.gtk.example");
Action onActivate = () => 
{
    var window = Application.NewWindow(app);
    Window.SetTitle(window, "Hellöchen Gtk👍");
    Window.SetDefaultSize(window, 200, 300);

    var box = Box.New(GtkDotNet.Orientation.Vertical, 5);    
    Widget.SetHAlign(box, GtkDotNet.Align.Center);
    Widget.SetVAlign(box, GtkDotNet.Align.Center);
    Window.SetChild(window, box);

    var button = Button.NewWithLabel("Hello World");
    Gtk.SignalConnect(button, "clicked", () => Console.WriteLine("Hello Wörld"));    

    var button2 = Button.NewWithLabel("Hello Gtk");
    
    Box.Append(box, button);
    Box.Append(box, button2);
    
    Widget.Show(window);
};


var status = Application.Run(app, onActivate);

GObject.Unref(app);

return status;

#endif