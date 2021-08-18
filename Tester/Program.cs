using System;
using System.IO;
using System.Reflection;
using GtkDotNet;

var app = new Application("de.uriegel.test");
app.Run(() =>
{
    //using var builder = new Builder();
    //builder.FromFile("glade");

    app.RegisterResources();
    using var builder = Builder.FromResource("/de/uriegel/test/main_window.glade");

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

    webView.ScriptDialog += (s, e) => 
    {
        switch (e.Message) 
        {
            case "anfang":
                webView.RunJavascript("var affe = 'Ein Äffchen 😎👌'");
                break;
            case "devTools":
                webView.Inspector.Show();
                break;
            default:
                Console.WriteLine($"---ALERT--- {e.Message}");
                break;
        }
    };

    var settings = new Settings("de.uriegel.test");

    EventHandler<DeleteEventArgs> deleteEvent = (s, de) => de.Cancel = true;
    window.Delete += deleteEvent;
    window.Delete -= deleteEvent;
    window.Configure += (s, e) => 
    {
        Console.WriteLine($"Configure {e.Width} {e.Height}");
        var (w, h) = (s as Window).Size;
        settings.SetInt("window-width", w);
        settings.SetInt("window-height", h);
        settings.SetBool("is-maximized", window.IsMaximized());
        Console.WriteLine($"Configure- {w} {h}");
    };    

    app.AddWindow(window);
    window.SetTitle("Web Viewuuu 😎😎👌");

    using var resourceStream = new ResourceStream("/de/uriegel/test/web/index.html");
    var size = resourceStream.Length;

    var w = settings.GetInt("window-width");
    var h = settings.GetInt("window-height");
    window.SetDefaultSize(w, h);
    if (settings.GetBool("is-maximized"))
        window.Maximize();
    //window.SetSizeRequest(200, 100);
    window.Move(2900, 456);
    window.ShowAll();
});

