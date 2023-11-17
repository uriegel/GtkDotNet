using System.Data.SqlTypes;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using GtkDotNet;

using LinqTools;

return Application.Run("org.gtk.example", app =>
        Application
            .NewWindow(app)
                .SideEffect(w => w.SetTitle("Hello Web View👍"))
                .SideEffect(w => w.SetDefaultSize(800, 600))
                .SideEffect(w => w.SetChild(
                    WebKit
                        .New()
                        .SideEffect(wk => 
                            wk
                                .GetSettings()
                                .SideEffect(s => s.SetBool("enable-developer-extras", true))
                            )
                        .SideEffect(wk => Gtk.SignalConnect<TwoIntPtr>(wk, "script-dialog", (_, d) =>
                            {
                                var msg = WebKit.ScriptDialogGetMessage(d);
                                var text = Marshal.PtrToStringUTF8(msg);
                                Console.WriteLine(text);
                                if (text == "dragStart")
                                {
                                    Console.WriteLine("dragStart ja");
                                    w.StartDrag(DragActions.Copy | DragActions.Move);
                                }
                            }))
                        .SideEffect(wk => wk.LoadUri($"file://{Directory.GetCurrentDirectory()}/webroot/index.html"))
                ))
                .SideEffect(w => w.SignalConnect<DragDataGetEventFunc>("drag-data-get", (w, c, selectionData, info, time, _) => 
                    selectionData.DataSetUris(new[] { "file:///home/uwe/test"})))
                .Show()
    );

delegate void DragDataGetEventFunc(IntPtr widget, IntPtr context, IntPtr selectionData, int info, int time, IntPtr _);