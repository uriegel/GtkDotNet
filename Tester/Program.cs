using System;
using System.IO;
using GtkDotNet;

var app = new Application("de.uriegel.test");
app.Run(() =>
{
    using var builder = new Builder();
    builder.AddFromFile("glade");
    var window = new Window(builder.GetObject("window"));
    var headerBar = new HeaderBar(builder.GetObject("headerbar"));

    app.AddActions(new[] {
        new GtkAction("destroy", () => app.Quit(), "<Ctrl>Q"), 
        new GtkAction("menuopen", () => {
            using var dialog = new FileChooserDialog("Datei öffnen", window, Dialog.FileChooserAction.Open,
                "_Abbrechen", Dialog.ResponseId.Cancel, "_Öffnen", Dialog.ResponseId.Ok);
            var res = dialog.Run();
            if (res == Dialog.ResponseId.Ok) 
                Console.WriteLine(dialog.FileName);
        }),
        new GtkAction("test", () => Console.WriteLine("Ein Test"), "F6"),
        new GtkAction("test2", () => Console.WriteLine("Ein Test 2")),
        new GtkAction("test3", () => headerBar.SetSubtitle("Das ist der neue Subtitle"), "F5"),
        new GtkAction("showhidden", true, state =>  Console.WriteLine(state), "<Ctrl>H"),
        new GtkDotNet.GtkAction("theme", "yaru", state => Console.WriteLine(state))
    });

    var webView = new WebView();
    window.Add(webView);
    webView.LoadUri($"file://{Directory.GetCurrentDirectory()}/../webroot/index.html");
    var ede = webView.Settings.EnableDeveloperExtras;
    webView.Settings.EnableDeveloperExtras = false;
    ede = webView.Settings.EnableDeveloperExtras;
    webView.Settings.EnableDeveloperExtras = true;
    ede = webView.Settings.EnableDeveloperExtras;

    EventHandler<DeleteEventArgs> deleteEvent = (s, de) => de.Cancel = true;
    window.Delete += deleteEvent;
    window.Delete -= deleteEvent;

    app.AddWindow(window);
    window.SetTitle("Web View 😎😎👌");
    window.SetDefaultSize(300, 300);
    window.SetSizeRequest(200, 100);
    window.Move(2900, 456);
    window.ShowAll();
});

