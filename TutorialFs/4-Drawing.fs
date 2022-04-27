module GtkTutorial

open GtkDotNet

type Resize =
   delegate of
      wi: nativeint *
      w : int *
      h : int *
      z : nativeint
       -> unit

type DragAction =
   delegate of
      gesture: nativeint *
      x      : double *
      y      : double *
      zero   : nativeint
       -> unit

type Pressed =
   delegate of
      gesture   : nativeint *
      pressCount: int *
      x         : double *
      y         : double *
      zero      : nativeint
       -> unit


let app = Application.New "org.gtk.example"
let onActivate () =
    let window = Application.NewWindow app
    Window.SetTitle (window, "Drawing Area👍")

    let frame = Frame.New ()
    Window.SetChild (window, frame) |> ignore

    let drawingArea = DrawingArea.New ()
    Widget.SetSizeRequest (drawingArea, 100, 100)

    Frame.SetChild (frame, drawingArea)

    let draw (area: nativeint) (cairo: nativeint) (w: int) (h: int) (nil: nativeint) =
        printfn "Mal"
        ()

    let resize (widget: nativeint) (w: int) (h: int) (zero: nativeint) =
        printfn "reiseiz"
        ()

    DrawingArea.SetDrawFunction (drawingArea, draw)
    Gtk.SignalConnectAfter<Resize>(drawingArea, "resize", resize);

    let dragBegin (gesture: nativeint) (x: double) (y: double) (widget: nativeint) = 
        printfn "drag begin"

    let dragUpdate (gesture: nativeint) (x: double) (y: double) (widget: nativeint) = 
        printfn "drag bupdate"
    
    let dragEnd (gesture: nativeint) (x: double) (y: double) (widget: nativeint) = 
        printfn "drag end"

    let pressed (gesture: nativeint) (pressCount: int) (x: double) (y: double) (zero: nativeint) =
        printfn "pressed"
        ()

    let gestureDrag = GestureDrag.New();
    GestureSingle.SetButton(gestureDrag, MouseButton.Primary);
    Widget.AddController(drawingArea, gestureDrag);
    Gtk.SignalConnect<DragAction>(gestureDrag, "drag-begin", dragBegin);
    Gtk.SignalConnect<DragAction>(gestureDrag, "drag-update", dragUpdate);
    Gtk.SignalConnect<DragAction>(gestureDrag, "drag-end", dragEnd);

    let press = GestureClick.New();
    GestureSingle.SetButton(press, MouseButton.Secondary);
    Widget.AddController(drawingArea, press);
    Gtk.SignalConnect<Pressed>(press, "pressed", pressed);

    Widget.Show window

    let closeWindow () = ()

    Gtk.SignalConnect<System.Action> (window, "destroy", closeWindow)

let status = Application.Run (app, onActivate)
GObject.Unref app

