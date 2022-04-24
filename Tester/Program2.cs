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

    var grid = Grid.New();    
    Window.SetChild(window, grid);

    var button = Button.NewWithLabel("Button 1");
    Gtk.SignalConnect(button, "clicked", () => Console.WriteLine("Hello Wörld"));    
    Grid.Attach(grid, button, 0, 0, 1, 1);

    var button2 = Button.NewWithLabel("Button 2");
    Gtk.SignalConnect(button2, "clicked", () => Console.WriteLine("Hello Wörld #2"));    
    Grid.Attach(grid, button2, 1, 0, 1, 1);

    var button3 = Button.NewWithLabel("Quit");
    Gtk.SignalConnect(button3, "clicked", () => Window.Close(window));    
    Grid.Attach(grid, button3, 0, 1, 2, 1);
    
    Widget.Show(window);
};


var status = Application.Run(app, onActivate);

GObject.Unref(app);

return status;

#endif