using System;
using System.Runtime.InteropServices;
using GtkDotNet;

namespace Tester
{
    class Program
    { 
        delegate bool BoolFunc();
        delegate bool ScripDialogFunc(IntPtr webView, IntPtr dialog);
        delegate bool ConfigureEventFunc(IntPtr widget, IntPtr evt);
       
        static int Main(string[] args)
        {
            var app = Application.New("de.uriegel.test");
            IntPtr menu = IntPtr.Zero;

            Application.AddActions(app, new [] { 
                new GtkAction("destroy", () => Application.Quit(app), "<Ctrl>Q"), 
                new GtkAction("menuopen", () => Popover.Popup(menu)),
                new GtkAction("test", () => Console.WriteLine("Ein Test"), "F6"), 
                new GtkAction("test2", () => Console.WriteLine("Ein Test 2")),
                new GtkAction("test3", () => Console.WriteLine("Ein Test 3"), "F5")
            });

            return Application.Run(app, () => {
                var builder = Builder.New();
                var res = Builder.AddFromFile(builder, "Tester/glade", IntPtr.Zero);
                var window = Builder.GetObject(builder, "window");
                menu = Builder.GetObject(builder, "menu");
                GObject.Unref(builder);
                Application.AddWindow(app, window);

                Window.SetTitle(window, "Web View 😎😎👌");            
                Window.SetDefaultSize(window, 400, 600);
                Widget.SetSizeRequest(window, 200, 100);
                Window.Move(window,2900, 456);

                // sudo apt install libwebkit2gtk-4.0-dev
                var webView = WebKit.New();
                var settings = WebKit.GetSettings(webView);
                GObject.SetBool(settings, "enable-developer-extras", true);
                Container.Add(window, webView);

                Gtk.SignalConnect<BoolFunc>(window, "delete_event", () => false);// true cancels the destroy request!
                Gtk.SignalConnect<ConfigureEventFunc>(window, "configure_event", (w, e) => {
                    var evt = Marshal.PtrToStructure<ConfigureEvent>(e);
                    Console.WriteLine("Configure " + evt.Width.ToString() + " " + evt.Height.ToString());
                    return false;
                });

                ScripDialogFunc scripDialogFunc = (_, dialog) => {
                    var ptr = WebKit.ScriptDialogGetMessage(dialog);
                    var text = Marshal.PtrToStringUTF8(ptr);
                    switch (text) 
                    {
                        case "anfang":
                            WebKit.RunJavascript(webView, "var affe = 'Ein Äffchen'");
                            break;
                        case "devTools":
                            var inspector = WebKit.GetInspector(webView);
                            WebKit.InspectorShow(inspector);
                            break;
                        default:
                            Console.WriteLine($"---ALERT--- {text}");
                            break;
                    }
                    return true;
                };
                Gtk.SignalConnect(webView, "script-dialog", scripDialogFunc);
                Gtk.SignalConnect<BoolFunc>(webView, "context-menu", () => true);
                Widget.ShowAll(window);

                // var menu2 = Builder.GetObject(builder, "abschuss");
                // Widget.Hide(menu2);

                //WebKit.LoadUri(webView, "https://google.de");
                WebKit.LoadUri(webView, $"file://{System.IO.Directory.GetCurrentDirectory()}/webroot/index.html");
            });
        }
    }
}

