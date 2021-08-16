namespace GtkDotNet
{
    public class WebView : Container
    {
        public class WebKitSettings : GObject
        {
            public bool EnableDeveloperExtras 
            {
                get => this["enable-developer-extras"];
                set => this["enable-developer-extras"] = value;
            }

            internal WebKitSettings(WebView webView) : base(new GObject(Raw.WebKit.GetSettings(webView.handle))) { }
        }

        public WebView() : base(new GObject(Raw.WebKit.New())) 
            => Settings = new WebKitSettings(this);

        public WebKitSettings Settings { get; }
        public void LoadUri(string uri) => Raw.WebKit.LoadUri(handle, uri);
    }
}