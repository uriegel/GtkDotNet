module GtkTutorial

open GtkDotNet

let clicked () =
    printfn "Clicked button"

let app = Application.New "org.gtk.example"
let onActivate () =
    let builder = Builder.New ()
    Builder.AddFromFile (builder, "builder.ui")
    let window = Builder.GetObject (builder, "window")    
    Window.SetApplication (window, app)

    let button1 = Builder.GetObject (builder, "button1")
    Gtk.SignalConnect<System.Action> (button1, "clicked", clicked)
    let button2 = Builder.GetObject (builder, "button2")
    Gtk.SignalConnect<System.Action> (button2, "clicked", clicked)
    let button3 = Builder.GetObject (builder, "quit")
    Gtk.SignalConnect<System.Action> (button3, "clicked", fun () -> (Window.Close window))

    Widget.Show window
    GObject.Unref builder

let status = Application.Run (app, onActivate)
GObject.Unref app

