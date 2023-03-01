using GtkDotNet;

var app = new Application("de.uriegel.test");
var ret = app.Run(() =>
{
    app.EnableSynchronizationContext();

    var window = new Window();
    var webView = new WebView();
    window.Add(webView);
    webView.LoadUri($"http://localhost:3000/?theme=adwaita&platform=linux");
    //webView.LoadUri($"file://{Directory.GetCurrentDirectory()}/../webroot/index.html");

    bool started = false;

    webView.ScriptDialog += (s, e) =>
    {
        // switch (e.Message)
        // {
        //     case "drag":
        Console.WriteLine(e.Message);
        //         break;
        // }
    };

    window.LeaveNotify += (s, e) =>
    {
        Console.WriteLine("Gelieft");
        if (!started)
        {
            window.StartDrag(new TargetList(new TargetEntry("text/url-list", 0, 0)), GtkDotNet.Raw.DragDrop.DragActions.Copy, 1, -1, -1);
            started = true;
        }
    };

    app.AddWindow(window);
        window.SetTitle("Web View 😎😎👌");
        window.SetSizeRequest(800, 800);        
        window.ShowAll();
});


// TODO Drag and drop
// TODO Theme detection 
// TODO WebView dark background or light background