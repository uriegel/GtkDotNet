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

            var builder = Builder.New();
            var res = Builder.AddFromFile(builder, "glade", IntPtr.Zero);

            var window = Builder.GetObject(builder, "window");
            Action destroyAction = () => Gtk.MainQuit();
            Gtk.SignalConnect(window, "destroy", destroyAction);
            BoolFunc deleteEventFunc = () => false; // true cancels the destroy request!
            Gtk.SignalConnect(window, "delete_event", deleteEventFunc);

            var button = Builder.GetObject(builder, "button");
            Action clickedAction = () => Button.SetLabel(button, "Thank You!");
            Gtk.SignalConnect(button, "clicked", clickedAction);

            GObject.Unref(builder);

            Widget.Show(window);

            Gtk.Main();

            Console.WriteLine("Program finished!");
        }
    }
}
