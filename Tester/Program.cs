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

            Action testAction = () => Console.WriteLine("Ein Test");
            Action test2Action = () => Console.WriteLine("Ein Test 2");
            Action test3Action = () => Console.WriteLine("Ein Test 3");
            Application.AddActions(app, new [] { new GtkAction("quit", testAction, "<Ctrl>Q"), new GtkAction("test", test2Action),new GtkAction("test3", test3Action, "F5")});

            return Application.Run(app, () => {
                var builder = Builder.New();
                var res = Builder.AddFromFile(builder, "Tester/glade", IntPtr.Zero);
                var window = Builder.GetObject(builder, "window");
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

                Action destroyAction = () => Application.Quit(app);
                Gtk.SignalConnect(window, "destroy", destroyAction);
                BoolFunc deleteEventFunc = () => false; // true cancels the destroy request!
                Gtk.SignalConnect(window, "delete_event", deleteEventFunc);
                ConfigureEventFunc configureEventFunc = (w, e) => {
                    var evt = Marshal.PtrToStructure<ConfigureEvent>(e);
                    Console.WriteLine("Configure " + evt.Width.ToString() + " " + evt.Height.ToString());
                    return false;
                }; // true cancels the destroy request!
                Gtk.SignalConnect(window, "configure_event", configureEventFunc);

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
                BoolFunc contextMenuFunc = () => true; // true cancels the context menu request
                Gtk.SignalConnect(webView, "context-menu", contextMenuFunc);
                Widget.ShowAll(window);

                var button = Builder.GetObject(builder, "menudrücker");
                var popupMenu = Builder.GetObject(builder, "menu");

                Action clickedAction = () => Popover.Popup(popupMenu);
                Gtk.SignalConnect(button, "clicked", clickedAction);

                var menu2 = Builder.GetObject(builder, "abschuss");

                Widget.Hide(menu2);

                Action clickedAction2 = () => Console.WriteLine("Aktion 2");
                Gtk.SignalConnect(menu2, "clicked", clickedAction2);

                //WebKit.LoadUri(webView, "https://google.de");
                WebKit.LoadUri(webView, $"file://{System.IO.Directory.GetCurrentDirectory()}/webroot/index.html");
            });
        }
    }
}

