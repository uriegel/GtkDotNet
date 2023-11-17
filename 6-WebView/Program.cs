using System.Runtime.InteropServices;
using GtkDotNet;

using LinqTools;

return Application.Run("org.gtk.example", app =>
    Application
        .NewWindow(app)
        .SideEffect(n => {
            var affe = 
            Application.Dispatch(() =>
            GtkSettings.GetDefault()
                .GetString("gtk-theme-name")).Result;
        })
        .SideEffect(w => w.SetTitle("Hello Web View👍"))
        .SideEffect(w => w.SetDefaultSize(800, 600))
        .SideEffect(w => 
        {
            var test = Application.Dispatch(() => 
                    GtkSettings.GetDefault().GetString("gtk-theme-name")).Result;
        })
        .SideEffect(w => w.SetChild(
            WebKit
                .New()
                .SideEffect(wk => 
                    wk
                        .GetSettings()
                        .SideEffect(s => s.SetBool("enable-developer-extras", true))
                    )
                .SideEffect(wk => wk.BeginDragDrop("text/uri-list"))
                // .SideEffect(wk => Gtk.SignalConnect<TwoIntPtr>(wk, "script-dialog", (_, d) =>
                //     {
                //         var msg = WebKit.ScriptDialogGetMessage(d);
                //         var text = Marshal.PtrToStringUTF8(msg);
                //         Console.WriteLine(text);
                //         if (text == "dragStart")
                //         {
                //             Console.WriteLine("dragStart ja");
                //             //w.StartDrag(new TargetList(new TargetEntry("text/uri-list", GtkDotNet.Raw.TargetEntry.Flags.Default, 14)), GtkDotNet.Raw.DragDrop.DragActions.Copy, 1, -1, -1);
                //             w.BeginDragDrop("text/uri-list");
                //         }
                //     }))
                .SideEffect(wk => wk.LoadUri($"file://{Directory.GetCurrentDirectory()}/webroot/index.html"))
        ))
        .Show());

enum PlatformType
{
    Kde,
    Gnome,
    Windows
}

