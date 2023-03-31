using GtkDotNet;

var app = new Application("de.uriegel.test");
var ret = app.Run(() =>
{
    app.EnableSynchronizationContext();


    var cancellation = new CancellationTokenSource();
    Task.Factory.StartNew(() =>
    {

        GFile.Copy("/media/uwe/Home/Videos/Tatort - Dunkle Wege.mp4", "/home/uwe/film.mp4", GtkDotNet.FileCopyFlags.Overwrite, true,
            (c, t) => Console.WriteLine($"Copying {c}/{t}"), cancellation.Token);
    });

    Task.Factory.StartNew(async () =>
    {
        await Task.Delay(1000);
        cancellation.Cancel();
    });

    var window = new Window();
    var webView = new WebView();
    window.Add(webView);

    window.SetIconFromCSharpResource("icon.png");
    //webView.LoadUri($"http://localhost:3000/?theme=adwaita&platform=linux");
    webView.LoadUri($"file://{Directory.GetCurrentDirectory()}/../webroot/index.html");
    webView.Settings.EnableDeveloperExtras = true;

    webView.ScriptDialog += (s, e) => 
    {
        Console.WriteLine(e.Message);
        if (e.Message == "devtools")
            webView?.Inspector.Show();  
        else if (e.Message == "dragStart")
        {
            Console.WriteLine("dragStart");
            window.StartDrag(new TargetList(new TargetEntry("text/uri-list", GtkDotNet.Raw.TargetEntry.Flags.Default, 14)), GtkDotNet.Raw.DragDrop.DragActions.Copy, 1, -1, -1);
        }
        else if (e.Message == "dragEnd")
            Console.WriteLine("dragEnd");
    };

    webView.LoadChanged += (s, e) =>
    {
        Console.WriteLine($"Load changed: {e.LoadEvent}");
        if (e.LoadEvent == WebKitLoadEvent.WEBKIT_LOAD_COMMITTED)
            webView.RunJavascript(
            """ 
                const button = document.getElementById('button')
                const devTools = document.getElementById('devTools')
                button.onclick = () => alert(`Das is es`)
                devTools.onclick = () => alert(`devtools`)
            """);
    };

    window.DragDataGet += (s, e) =>
    {
        Console.WriteLine("dragabfrage");
        e.SelectionData.SetUris(new[] { "file:///home/uwe/test/222.jpg" });
    };

    app.AddWindow(window);
        window.SetTitle("Web View 😎😎👌");
        window.SetSizeRequest(800, 800);        
        window.ShowAll();
});


// TODO Drag and drop
// TODO Theme detection 
// TODO WebView dark background or light background