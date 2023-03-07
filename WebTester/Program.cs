using GtkDotNet;

var app = new Application("de.uriegel.test");
var ret = app.Run(() =>
{
    app.EnableSynchronizationContext();

    var window = new Window();
    var webView = new WebView();
    window.Add(webView);
    //webView.LoadUri($"http://localhost:3000/?theme=adwaita&platform=linux");
    webView.LoadUri($"file://{Directory.GetCurrentDirectory()}/../webroot/index.html");
    webView.Settings.EnableDeveloperExtras = true;
    
    webView.ScriptDialog += (s, e) => 
    {
        if (e.Message == "anfang")
            webView?.Inspector.Show();  
        else if (e.Message == "dragStart")
        {
            Console.WriteLine("dragStart");
            window.StartDrag(new TargetList(new TargetEntry("text/uri-list", GtkDotNet.Raw.TargetEntry.Flags.Default, 14)), GtkDotNet.Raw.DragDrop.DragActions.Copy, 1, -1, -1);
        }
        else if (e.Message == "dragEnd")
            Console.WriteLine("dragEnd");
    };

    window.DragDataGet += (s, e) =>
    {
        Console.WriteLine("dragabfrage");
    };

    app.AddWindow(window);
        window.SetTitle("Web View 😎😎👌");
        window.SetSizeRequest(800, 800);        
        window.ShowAll();
});


// TODO Drag and drop
// TODO Theme detection 
// TODO WebView dark background or light background