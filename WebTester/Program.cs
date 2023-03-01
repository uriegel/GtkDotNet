using GtkDotNet;

var app = new Application("de.uriegel.test");
var ret = app.Run(() =>
{
    var window = new Window();
    var webView = new WebView();
    window.Add(webView);
    webView.LoadUri($"http://localhost:3000/?theme=adwaita&platform=linux");
    app.AddWindow(window);
    window.SetTitle("Web View 😎😎👌");
    window.SetSizeRequest(800, 800);        
    window.ShowAll();
});

// TODO Drag and drop
// TODO Theme detection 
// TODO WebView dark background or light background