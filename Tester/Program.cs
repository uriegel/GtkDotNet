using System;
using GtkDotNet;

namespace Tester
{
    class Program
    { 
        delegate bool BoolFunc();
        
        static void Main(string[] args)
        {
            Gtk.Init();

            var window = Window.New(WindowType.TopLevel);
            Window.SetTitle(window, "Web View 😎😎👌");            
            Window.SetDefaultSize(window, 800, 600);
            Widget.SetSizeRequest(window, 200, 100);

            // sudo apt install libwebkit2gtk-4.0-dev
            var webView = WebKit.New();
            Container.Add(window, webView);

            Action destroyAction = () => Gtk.MainQuit();
            Gtk.SignalConnect(window, "destroy", destroyAction);
            BoolFunc deleteEventFunc = () => false; // true cancels the destroy request!
            Gtk.SignalConnect(window, "delete_event", deleteEventFunc);
            
            Widget.ShowAll(window);

            WebKit.LoadUri(webView, "https://google.de");

            Gtk.Main();
        }
    }
}
