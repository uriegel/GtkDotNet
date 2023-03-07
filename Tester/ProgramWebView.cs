//#define WEBVIEW
#if WEBVIEW

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GtkDotNet;

var app = new Application("de.uriegel.test");
var ret =  app.Run(() => 
{
    //using var builder = new Builder();
    //builder.FromFile("glade");

    // try 
    // {
    //     GFile.Trash("/home/uwe/test/ablage12");
    //     GFile.Trash("/home/uwe/test/web.png");
    //     GFile.Trash("/etc");
    // }
    // catch (GErrorException fe)
    // {
    //     Console.Error.WriteLine($"Could not delete: {fe.Code}, {fe.Message}");
    // }

    app.RegisterResources();
    app.EnableSynchronizationContext();
    using var builder = Builder.FromResource("/de/uriegel/test/main_window.glade");

    var window = new Window(builder.GetObject("window"));
    var headerBar = new HeaderBar(builder.GetObject("headerbar"));
    var revealer = new Revealer(builder.GetObject("ProgressRevealer"));
    var progress = new ProgressControl(builder.GetObject("ProgressArea"));

    var showHiddenAction = new GtkAction("showhidden", true, state => Console.WriteLine(state), "<Ctrl>H");
    var themeAction = new GtkDotNet.GtkAction("theme", "yaru", state => Console.WriteLine(state));

    using var iconInfo = IconInfo.Choose(".pdf", 64, IconLookup.ForceSvg);
    var iconFile = iconInfo.GetFileName();

    app.AddActions(new[] {
        new GtkAction("destroy", () => window.Close(), "<Ctrl>Q"),
        new GtkAction("menuopen", () => {
            using var dialog = new FileChooserDialog("Datei öffnen", window, Dialog.FileChooserAction.Open,
                "_Abbrechen", Dialog.ResponseId.Cancel, "_Öffnen", Dialog.ResponseId.Ok);
            var res = dialog.Run();
            if (res == Dialog.ResponseId.Ok)
                Console.WriteLine(dialog.FileName);
        }),
        new GtkAction("test", () =>
        {
            Console.WriteLine("Ein Test");
            new Thread(() => app.BeginInvoke(100, () =>
            {
                headerBar.SetSubtitle("Das ist aus einem anderen Thread gesetzt");
                showHiddenAction.SetBoolState(true);
                themeAction.SetStringState("yarudark");
            })).Start();

        }, "F6"),
        new GtkAction("test2", async () =>
        {
            Console.WriteLine($"ThreadID start: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"is revealed: {revealer.IsRevealed}");
            progress.Progress = 0;
            revealer.IsRevealed = true;
            try {
                await Task.Factory.StartNew(
                    () => GFile.Copy("/home/uwe/Videos/Tatort - Hundstage.mp4", "/etc/hunde", FileCopyFlags.None,
                        (c, t) => progress.Progress = (double)((decimal)c/(decimal)t))
                );
            }
            catch (AccessDeniedException e)
            {
                Console.WriteLine($"Access Denied: {e}");
            }

            try {
                await Task.Factory.StartNew(
                    () => GFile.Copy("/home/uwe/Videos/Tatort - Hundstage.mp4", "/home/uwe/Videos/Tatort - Hundstage2.mp4", FileCopyFlags.None,
                        (c, t) => progress.Progress = (double)((decimal)c/(decimal)t))
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not copy: {e}");
            }

            try {
                await Task.Factory.StartNew(
                    () => GFile.Copy("/home/uwe/Videos/Tatort - Hundstage.mp4", "/home/uwe/Videos/Tatort - Hundstage2.mp4", FileCopyFlags.None,
                        (c, t) => progress.Progress = (double)((decimal)c/(decimal)t))
                );
            }
            catch (TargetExistingException)
            {
                await Task.Factory.StartNew(
                    () => GFile.Copy("/home/uwe/Videos/Tatort - Hundstage.mp4", "/home/uwe/Videos/Tatort - Hundstage2.mp4", FileCopyFlags.Overwrite,
                        (c, t) => progress.Progress = (double)((decimal)c/(decimal)t))
                );
            }

            try {
                await Task.Factory.StartNew(
                    () => GFile.Copy("/home/uwe/Videos/Tatort - Hundstage nicht da.mp4", "/home/uwe/Videos/film.mp4", FileCopyFlags.None,
                        (c, t) => progress.Progress = (double)((decimal)c/(decimal)t))
                );
            }
            catch (SourceNotFoundException e)
            {
                Console.WriteLine($"Source not found: {e}");
            }

            try {
                GFile.Trash("/home/uwe/Videos/Abfälle/Haufen");
            } catch {}
            try {
                await Task.Factory.StartNew(
                    () => GFile.Copy("/home/uwe/Videos/Tatort - Hundstage.mp4", "/home/uwe/Videos/Abfälle/Haufen/film.mp4", FileCopyFlags.None, 
                        (c, t) => progress.Progress = (double)((decimal)c/(decimal)t))
                );
            }
            catch (TargetNotFoundException e)
            {
                Console.WriteLine($"Target not found: {e}");
            }

            await Task.Factory.StartNew(
                    () => GFile.Copy("/home/uwe/Videos/Tatort - Hundstage.mp4", "/home/uwe/Videos/Abfälle/Haufen/film.mp4", FileCopyFlags.None, true,
                    (c, t) => progress.Progress = (double)((decimal)c/(decimal)t))
            );

            try {
                await Task.Factory.StartNew(
                    () => GFile.Copy("/home/uwe/Videos/Tatort - Hundstage.mp4", "/etc/Videos/Abfälle/Haufen/film.mp4", FileCopyFlags.None, true,
                        (c, t) => progress.Progress = (double)((decimal)c/(decimal)t))
                );
            }
            catch (AccessDeniedException e)
            {
                Console.WriteLine($"Zugriff verweigert: {e}");
            }

            await Task.Delay(1000);
            Console.WriteLine($"ThreadID end: {Thread.CurrentThread.ManagedThreadId}");
            revealer.IsRevealed = false;
        }),
        new GtkAction("test3", () => headerBar.SetSubtitle("Das ist der neue Subtitle"), "F5"),
        showHiddenAction,
        themeAction
    });

    var webView = new WebView();
    window.Add(webView);
    webView.LoadUri($"file://{Directory.GetCurrentDirectory()}/../webroot/index.html");
    //webView.LoadUri($"http://localhost:3000");
    //webView.LoadUri("file:///home/uwe/Dokumente/Functional_Programming_in_CSharp_Second_Edi.pdf");
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

    //var settings = new Settings("de.uriegel.test");

    EventHandler<DeleteEventArgs> deleteEvent = (s, de) =>
    {
        var (w, h) = (s as Window).Size;
        // settings.SetInt("window-width", w);
        // settings.SetInt("window-height", h);
        // var (x, y) = (s as Window).GetPosition();
        // settings.SetInt("window-position-x", x);
        // settings.SetInt("window-position-y", y);
        // settings.SetBool("is-maximized", window.IsMaximized());
        //de.Cancel = true;
    };
    window.Delete += deleteEvent;
    //window.Delete -= deleteEvent;
    // window.Configure += (s, e) => 
    // {
    //     Console.WriteLine($"Configure {e.Width} {e.Height}");
    //     var (w, h) = (s as Window).Size;
    //     settings.SetInt("window-width", w);
    //     settings.SetInt("window-height", h);
    //     settings.SetBool("is-maximized", window.IsMaximized());
    //     Console.WriteLine($"Configure- {w} {h}");
    // };    

    app.AddWindow(window);
    window.SetTitle("Web View 😎😎👌");

    using var resourceStream = new ResourceStream("/de/uriegel/test/web/index.html");
    var size = resourceStream.Length;

    var buffer = new byte[40000];
    var prefix = "Hello Wörld";
    int offset = Encoding.UTF8.GetBytes(prefix, 0, prefix.Length, buffer, 0);

    var read = resourceStream.Read(buffer, offset, buffer.Length - offset);
    var text = Encoding.UTF8.GetString(buffer, 0, read + offset);

    // var w = settings.GetInt("window-width");
    // var h = settings.GetInt("window-height");
    // var x = settings.GetInt("window-position-x");
    // var y = settings.GetInt("window-position-y");
    // window.SetDefaultSize(w, h);
    // if (settings.GetBool("is-maximized"))
    //     window.Maximize();
    window.SetSizeRequest(800, 200);        
    // if (x != -1 && y != -1)
        window.Move(3200, 400);    
    window.ShowAll();
});

#endif