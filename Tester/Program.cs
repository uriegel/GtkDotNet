using System;
using GtkDotNet;

namespace Tester
{
    class Program
    { 
        delegate bool BoolFunc();
        
        static void Main(string[] args)
        {
            Console.WriteLine("Program started 😎😎👌");
            
            Gtk.Init();

            var window = Window.New(WindowType.TopLevel);
            Window.SetTitle(window, "Örstes Fänster 😎😎👌");
            Container.SetBorderWidth(window, 10);
            Widget.SetSizeRequest(window, 200, 100);

            Action destroyAction = () => Gtk.MainQuit();
            Gtk.SignalConnect(window, "destroy", destroyAction);
            BoolFunc deleteEventFunc = () => false; // true cancels the destroy request!
            Gtk.SignalConnect(window, "delete_event", deleteEventFunc);

            var label = Label.New("Hellö Wörld 😎👌");
            Label.SetSelectable(label, true);
            Container.Add(window, label);

            Widget.ShowAll(window);

            Gtk.Main();

            Console.WriteLine("Program finished!");
        }
    }
}
