using System;
using System.Runtime.InteropServices;

namespace GtkDotNet
{
    public class WebView : Container
    {
        #region Events
        
        public event EventHandler<ScriptDialogEventArgs> ScriptDialog
        { 
            add 
            {
                scriptDialog += value;
                scriptDialogFunc = (w, d) => 
                {
                    var ptr = Raw.WebKit.ScriptDialogGetMessage(d);
                    var text = Marshal.PtrToStringUTF8(ptr);
                    var sdea = new ScriptDialogEventArgs()
                    {
                        Message = text
                    };
                    scriptDialog?.Invoke(this, sdea);
                    return true;  
                };
                Raw.Gtk.SignalConnect<ScriptDialogFunc>(handle, "script-dialog", scriptDialogFunc);
            }
            remove 
            {
                scriptDialog -= value;
                Raw.Gtk.SignalDisconnect<ScriptDialogFunc>(handle, "script-dialog", scriptDialogFunc);
                scriptDialogFunc = null;
            }
        }
        event EventHandler<ScriptDialogEventArgs> scriptDialog;
        ScriptDialogFunc scriptDialogFunc;

        public event EventHandler<LoadChangedEventArgs> LoadChanged
        { 
            add 
            {
                loadChanged += value;
                loadChangedFunc = (w, e, _) => 
                    loadChanged?.Invoke(this, new LoadChangedEventArgs()
                    {
                        LoadEvent = (WebKitLoadEvent)e
                    });
                Raw.Gtk.SignalConnect<WebViewLoadChangedFunc>(handle, "load-changed", loadChangedFunc);
            }
            remove 
            {
                loadChanged -= value;
                Raw.Gtk.SignalDisconnect<WebViewLoadChangedFunc>(handle, "load-changed", loadChangedFunc);
                loadChangedFunc = null;
            }
        }
        event EventHandler<LoadChangedEventArgs> loadChanged;
        WebViewLoadChangedFunc loadChangedFunc;

        #endregion

        public WebKitSettings Settings { get; }

        public Inspector Inspector  {  get  => new Inspector(Raw.WebKit.GetInspector(handle)); }

        public class WebKitSettings : GObject
        {
            public bool EnableDeveloperExtras 
            {
                get => GetBool("enable-developer-extras");
                set => SetBool("enable-developer-extras", value);
            }

            internal WebKitSettings(WebView webView) : base(new GObject(Raw.WebKit.GetSettings(webView.handle))) { }
        }

        public WebView() : base(new GObject(Raw.WebKit.New())) 
        {
            Settings = new WebKitSettings(this);
            Raw.Gtk.SignalConnect<BoolFunc>(handle, "context-menu", () => true);
        }

        public void LoadUri(string uri) => Raw.WebKit.LoadUri(handle, uri);
        public void RunJavascript(string script)
            => Raw.WebKit.RunJavascript(handle, script);
        delegate bool ScriptDialogFunc(IntPtr webView, IntPtr dialog);
        delegate void WebViewLoadChangedFunc(IntPtr widget, WebKitLoadEvent loadEvent, IntPtr data);
        delegate bool BoolFunc();
    }
}