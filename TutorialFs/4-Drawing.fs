module GtkTutorial

open GtkDotNet

let draw (area: nativeint) (cairo: nativeint) (w: int) (h: int) (nil: nativeint) =
    printfn "Mal"
    ()


let app = Application.New "org.gtk.example"
let onActivate () =
    let window = Application.NewWindow app
    Window.SetTitle (window, "Drawing Area👍")

    let frame = Frame.New ()
    Window.SetChild (window, frame) |> ignore

    let drawingArea = DrawingArea.New ()
    Widget.SetSizeRequest (drawingArea, 100, 100)

    Frame.SetChild (frame, drawingArea)

    DrawingArea.SetDrawFunction (drawingArea, draw)

    Window.SetDefaultSize (window, 200, 200)
    Widget.Show window

    let closeWindow () = 
        //DrawingArea.SetDrawFunction (drawingArea, draw)
        ()

    Gtk.SignalConnect<System.Action> (window, "destroy", closeWindow)

let status = Application.Run (app, onActivate)
GObject.Unref app

