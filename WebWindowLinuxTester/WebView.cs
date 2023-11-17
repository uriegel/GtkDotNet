using GtkDotNet;
using WebWindowNetCore.Data;
using LinqTools;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace WebWindowNetCore;

enum Action
{
    DevTools = 1,
    Show,
}

record ScriptAction(Action Action, int? Width, int? Height, bool? IsMaximized);

public class WebView : WebWindowNetCore.Base.WebView
{
    public static WebViewBuilder Create() => new();

    public override int Run(string gtkId = "de.uriegel.WebViewNetCore")
        => Application.Run(gtkId, app =>
            Application
                .NewWindow(app)
                .SideEffect(_ => Application.EnableSynchronizationContext())
                .SideEffect(w => w.SetTitle(settings?.Title))
                .SideEffect(w => w.SetDefaultSize(settings!.Width, settings!.Height))
                .SideEffectIf(settings?.ResourceIcon != null,
                    w => Window.SetIconFromDotNetResource(w, settings?.ResourceIcon))
                .SideEffect(w => w.SetChild(
                    WebKit
                        .New()
                        .SideEffect(wk =>
                            wk
                                .GetSettings()
                                .SideEffectIf(settings?.DevTools == true,
                                    s => s.SetBool("enable-developer-extras", true))
                        )
                        .SideEffectIf(settings?.DefaultContextMenuEnabled != true,
                            wk => wk.SignalConnect<BoolFunc>("context-menu", () => true))
                        .SideEffect(wk => wk.LoadUri((Debugger.IsAttached && !string.IsNullOrEmpty(settings?.DebugUrl)
                                                        ? settings?.DebugUrl
                                                        : settings?.Url != null
                                                        ? settings.Url
                                                        : $"http://localhost:{settings?.HttpSettings?.Port ?? 80}{settings?.HttpSettings?.WebrootUrl}/{settings?.HttpSettings?.DefaultHtml}")
                                                            + (settings?.Query ?? settings?.GetQuery?.Invoke())))
                        .SideEffect(wk => Gtk.SignalConnect<TwoIntPtr>(wk, "script-dialog", (_, d) =>
                            {
                                var msg = WebKit.ScriptDialogGetMessage(d);
                                var text = Marshal.PtrToStringUTF8(msg);
                                Console.WriteLine(text);
                                var action = JsonSerializer.Deserialize<ScriptAction>(text ?? "", JsonDefault.Value);

                                switch (action?.Action)
                                {
                                    case Action.DevTools:
                                        WebKit.InspectorShow(WebKit.GetInspector(wk));
                                        break;
                                    case Action.Show:
                                        if (action.Width.HasValue && action.Height.HasValue)
                                            Window.SetDefaultSize(w, action.Width.Value, action.Height.Value);
                                        if (action?.IsMaximized == true)
                                            Window.Maximize(w);
                                        Widget.Show(w);
                                        break;
                                }
                            }))
                        .SideEffect(wk => Gtk.SignalConnect<TwoIntPtr>(wk, "load-changed", (_, e) =>
                            {
                                if ((WebKitLoadEvent)e == WebKitLoadEvent.WEBKIT_LOAD_COMMITTED)
                                {
                                    if (settings?.SaveBounds == true)
                                        WebKit.RunJavascript(wk,
                                            """ 
                                                const bounds = JSON.parse(localStorage.getItem('window-bounds') || '{}')
                                                const isMaximized = localStorage.getItem('isMaximized')
                                                if (bounds.width && bounds.height)
                                                    alert(JSON.stringify({action: 2, width: bounds.width, height: bounds.height, isMaximized: isMaximized == 'true'}))
                                                else
                                                    alert(JSON.stringify({action: 2}))
                                            """);
                                    if (settings?.DevTools == true)
                                        WebKit.RunJavascript(wk,
                                            """ 
                                                function webViewShowDevTools() {
                                                    alert(JSON.stringify({action: 1}))
                                                }
                                            """);
                                    if ((settings?.HttpSettings?.RequestDelegates?.Length ?? 0) > 0)
                                        wk.RunJavascript(
                                            """ 
                                                async function webViewRequest(method, input) {
                                                    const msg = {
                                                        method: 'POST',
                                                        headers: { 'Content-Type': 'application/json' },
                                                        body: JSON.stringify(input)
                                                    }

                                                    const response = await fetch(`/request/${method}`, msg) 
                                                    return await response.json() 
                                                }
                                            """);
                                    settings?.OnStarted?.Invoke();
                                }
                            }))
                        .SideEffectIf(settings?.SaveBounds == true,
                            wk => w.SideEffect(_ => w.SignalConnectAfter<CloseDelegate>("delete-event", (___, _, __) =>
                                false
                                    .SideEffectChoose(Window.IsMaximized(w) == false,
                                        _ => { WebKit.RunJavascript(wk,
                                            $$"""
                                                localStorage.setItem('window-bounds', JSON.stringify({width: {{w.GetWidth()}}, height: {{w.GetHeight()}}}))
                                                localStorage.setItem('isMaximized', false)
                                            """);},
                                    _ => { WebKit.RunJavascript(wk, $"localStorage.setItem('isMaximized', true)"); }


                                )))
                        ))
                )
                .SideEffectIf(settings?.SaveBounds != true,
                    w => w.Show())
            );

    internal WebView(WebViewBuilder builder)
        => settings = builder.Data;

    delegate bool CloseDelegate(IntPtr widget, IntPtr z2, IntPtr z3);
    delegate bool BoolFunc();
    readonly WebViewSettings? settings;
}

