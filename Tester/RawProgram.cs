﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using GtkDotNet.Raw;

namespace Tester
{
    class Program
    { 
        delegate bool BoolFunc();
        delegate bool ScriptDialogFunc(IntPtr webView, IntPtr dialog);
        delegate bool ConfigureEventFunc(IntPtr widget, IntPtr evt);
        delegate void DragMotionFunc(IntPtr widget, IntPtr context, int x, int y);
        delegate void DragDataReceivedFunc(IntPtr widget, IntPtr context, int x, int y, IntPtr data);

        static IntPtr window;
        static IntPtr headerBar;

        static int Main2()
        {
            var app = Application.New("de.uriegel.test");
            if (Environment.CurrentDirectory.Contains("netcoreapp"))
                Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, "../../../");

            Application.AddActions(app, new [] { 
                new GtkAction("destroy", () => Application.Quit(app), "<Ctrl>Q"), 
                new GtkAction("menuopen", () => {
                    var dialog = Dialog.NewFileChooser("Datei öffnen", window, GtkDotNet.Dialog.FileChooserAction.Open,
                    "_Abbrechen", GtkDotNet.Dialog.ResponseId.Cancel, "_Öffnen", GtkDotNet.Dialog.ResponseId.Ok, IntPtr.Zero);
                    var res = Dialog.Run(dialog);
                    if (res == GtkDotNet.Dialog.ResponseId.Ok) {
                        var ptr = Dialog.FileChooserGetFileName(dialog);
                        string file = Marshal.PtrToStringUTF8(ptr);
                        Console.WriteLine(file);
                        GObject.Free(ptr);

                    }
                    Widget.Destroy(dialog);
                }),
                new GtkAction("test", () => Console.WriteLine("Ein Test"), "F6"), 
                new GtkAction("test2", () => Console.WriteLine("Ein Test 2")),
                new GtkAction("test3", () => HeaderBar.SetSubtitle(headerBar, "Das ist der neue Subtitle"), "F5"),
                new GtkAction("showhidden", true, 
                    (a, s) => 
                    {
                        var state = GtkAction.HandleBoolState(a, s);
                        Console.WriteLine(state);
                    }, "<Ctrl>H"),
                new GtkAction("theme", "yaru", 
                    (a, s) => 
                    {
                        var state = GtkAction.HandleStringState(a, s);
                        Console.WriteLine(state);
                    })
            });

            var ret =  Application.Run(app, () => {
                var type = Gtk.GuessContentType("/home/uwe/Dokumente/hypovereinsbank.pdf");
                var type1 = Gtk.GuessContentType("x.fs");
                var type2 = Gtk.GuessContentType("x.cs");
                type = Gtk.GuessContentType("x.pdf");
                var icon = Icon.Get(type);
                var theme = Theme.GetDefault();
                var names = Icon.GetNames(icon);
                // GTK_ICON_LOOKUP_FORCE_SVG
                var iconInfo = Theme.ChooseIcon(theme, names, 48, GtkDotNet.IconLookup.ForceSvg);
                var filename = IconInfo.GetFileName(iconInfo);
                var text = Marshal.PtrToStringUTF8(filename);
                GObject.Unref(icon);
                
                var builder = Builder.New();
                var res = Builder.AddFromFile(builder, "glade", IntPtr.Zero);
                window = Builder.GetObject(builder, "window");
                headerBar = Builder.GetObject(builder, "headerbar");
                Builder.ConnectSignals(builder, (IntPtr builder, IntPtr obj, string signal, string handleName, IntPtr connectObj, int flags) =>
                {
                    switch (handleName) 
                    {
                        case "app.delete":
                            Gtk.SignalConnectObject<BoolFunc>(obj, signal, () => {
                                return false;
                            }, connectObj);
                        break;                       
                    }
                });
                GObject.Unref(builder);

                //var gioSettings = GioSettings.New("de.uriegel.commander");

                Application.AddWindow(app, window);

                Window.SetTitle(window, "Web View 😎😎👌");            
                Window.SetDefaultSize(window, 300, 300);
                Widget.SetSizeRequest(window, 200, 100);
                Window.Move(window,2900, 456);

                var webView = WebKit.New();
                var settings = WebKit.GetSettings(webView);
                GObject.SetBool(settings, "enable-developer-extras", true);
                Container.Add(window, webView);

                var target = TargetEntry.New("text/plain", TargetEntry.Flags.OtherApp, 0);
                DragDrop.UnSet(webView);
                DragDrop.SetDestination(window, DragDrop.DefaultDestination.Drop | DragDrop.DefaultDestination.Highlight| DragDrop.DefaultDestination.Motion, 
                        target, 1, DragDrop.DragActions.Move);
                TargetEntry.Free(target);
                Gtk.SignalConnect<DragDataReceivedFunc>(window, "drag-data-received", 
                    (w, context, x, y, data) => 
                    {
                        var text = SelectionData.GetText(data);
                        Console.WriteLine(text);
                    }
                );

                Gtk.SignalConnect<DragMotionFunc>(window, "drag-motion", 
                    (w, context, x, y) => 
                    {
                        Console.WriteLine("motion");
                    }
                );
                
                Gtk.SignalConnect<BoolFunc>(window, "delete_event", () => false);// true cancels the destroy request!
                Gtk.SignalConnect<ConfigureEventFunc>(window, "configure_event", (w, e) => {
                    var evt = Marshal.PtrToStructure<ConfigureEvent>(e);
                    Console.WriteLine("Configure " + evt.Width.ToString() + " " + evt.Height.ToString());

                    Window.GetSize(window, out var ww, out var hh);
                    Console.WriteLine("Configure- " + ww.ToString() + " " + hh.ToString());

                    return false;
                });

                ScriptDialogFunc scripDialogFunc = (_, dialog) => {
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

                //WebKit.LoadUri(webView, "https://google.de");
                //WebKit.LoadUri(webView, "http://localhost:3000/");
                WebKit.LoadUri(webView, $"file://{System.IO.Directory.GetCurrentDirectory()}/../webroot/index.html");
            });

            Console.WriteLine("Das wars");
            return ret;
        }
    }
}

