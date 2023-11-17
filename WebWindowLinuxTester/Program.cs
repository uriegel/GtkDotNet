using LinqTools;
using AspNetExtensions;
using WebWindowNetCore;
using GtkDotNet;

var sseEventSource = WebView.CreateEventSource<Event>();
StartEvents(sseEventSource.Send);

WebView
    .Create()   
    .InitialBounds(600, 800)
    .ResourceIcon("icon")
    .Title("Commander")
    .QueryString(() => $"?theme={Application.Dispatch(() => GtkSettings.GetDefault().GetString("gtk-theme-name")).Result}")
    .SaveBounds()
    .DefaultContextMenuEnabled()
    .OnStarted(() => Console.WriteLine("Now started"))
    //.DebugUrl("http://localhost:3000")
    .Url($"file://{Directory.GetCurrentDirectory()}/../6-WebView/webroot/index.html")
    .ConfigureHttp(http => http
        .ResourceWebroot("webroot", "/web")
        .UseSse("sse/test", sseEventSource)
        .MapGet("video", context => 
            context
                .SideEffect(c => Console.WriteLine("Range request"))
            .StreamRangeFile("/home/uwe/Videos/Buster Keaton - Sherlock Jr..mp4"))        
        .Build())
#if DEBUG            
    .DebuggingEnabled()
#endif            
    .Build()
    .Run("de.uriegel.Commander");    

void StartEvents(Action<Event> onChanged)   
{
    var counter = 0;
    new Thread(_ =>
        {
            while (true)
            {
                Thread.Sleep(5000);
                onChanged(new($"Ein Event {counter++}"));
           }
        })
        {
            IsBackground = true
        }.Start();   
}

record Event(string Content);

// TODO https://webkitgtk.org/reference/webkit2gtk/2.28.2/WebKitURISchemeRequest.html
// TODO Windows Version: https://learn.microsoft.com/en-us/dotnet/api/microsoft.web.webview2.core.corewebview2.webresourcerequested?view=webview2-dotnet-1.0.1587.40
// TODO Favicon in Webroot, Property obsolet
