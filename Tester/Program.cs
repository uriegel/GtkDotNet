using System;
using GtkDotNet;

namespace Tester
{
    class Program
    { 
        static void Main(string[] args)
        {
            Console.WriteLine("Program started 😎😎👌");
            
            Gtk.Init();

            var window = Window.New(WindowType.TopLevel);
            Window.SetTitle(window, "Örstes Fänster 😎😎👌");
            Widget.Show(window);

            Gtk.Main();

            Console.WriteLine("Program finished!");
        }
    }
}
