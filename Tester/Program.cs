using System;
using System.Runtime.InteropServices;
using GtkDotNet;

namespace Tester
{
    class Program
    { 
        delegate bool BoolFunc();
        delegate bool ScripDialogFunc(IntPtr webView, IntPtr dialog);
        
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

            ScripDialogFunc scripDialogFunc = (_, dialog) => {
                var ptr = WebKit.ScriptDialogGetMessage(dialog);
                var text = Marshal.PtrToStringUTF8(ptr);
                if (text == "anfang") 
                    WebKit.RunJavascript(webView, "var affe = 'Ein Äffchen'");
                else 
                    Console.WriteLine($"---ALERT--- {text}");
                return true;
            };
            Gtk.SignalConnect(webView, "script-dialog", scripDialogFunc);
            BoolFunc contextMenuFunc = () => true; // true cancels the context menu request
            Gtk.SignalConnect(webView, "context-menu", contextMenuFunc);
            Widget.ShowAll(window);

            //WebKit.LoadUri(webView, "https://google.de");
            WebKit.LoadUri(webView, $"file://{System.IO.Directory.GetCurrentDirectory()}/webroot/index.html");

            Gtk.Main();
        }
    }
}
