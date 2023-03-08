namespace GtkDotNet;

public enum GdkEventType
{
    Nothing = -1,
    
    /// <summary>
    /// the window manager has requested that the toplevel window be hidden or destroyed, usually when the user clicks on a special icon in the title bar.
    /// </summary>
    Delete = 0,
    
    /// <summary>
    /// the window has been destroyed.
    /// </summary>
    DESTROY,

    /// <summary>
    /// all or part of the window has become visible and needs to be redrawn.
    /// </summary>
    EXPOSE,

    /// <summary>
    /// the pointer (usually a mouse) has moved.
    /// </summary>
    MOTION_NOTIFY,    

    /// <summary>
    /// a mouse button has been pressed.
    /// </summary>
    BUTTON_PRESS,

    /// <summary>
    /// alias for GDK_2BUTTON_PRESS, added in 3.6.
    /// </summary>
    DOUBLE_BUTTON_PRESS,

    /// <summary>
    /// alias for GDK_3BUTTON_PRESS, added in 3.6.
    /// </summary>
    TRIPLE_BUTTON_PRESS,

    /// <summary>
    /// a mouse button has been released.
    /// </summary>
    BUTTON_RELEASE,

    /// <summary>
    /// a key has been pressed.
    /// </summary>
    KEY_PRESS,

    /// <summary>
    /// a key has been released.
    /// </summary>
    KEY_RELEASE,

    /// <summary>
    /// the pointer has entered the window.
    /// </summary>
    ENTER_NOTIFY,

    /// <summary>
    /// the pointer has left the window.
    /// </summary>
    LEAVE_NOTIFY,

    /// <summary>
    /// the keyboard focus has entered or left the window.
    /// </summary>
    FOCUS_CHANGE,

    /// <summary>
    /// the size, position or stacking order of the window has changed. Note that GTK+ discards these events for GDK_WINDOW_CHILD windows.
    /// </summary>
    CONFIGURE,

    /// <summary>
    /// the window has been mapped.
    /// </summary>
    MAP,

    /// <summary>
    /// the window has been unmapped.
    /// </summary>
    UNMAP,
}

/*



PROPERTY_NOTIFY
16
a property on the window has been changed or deleted.

SELECTION_CLEAR
17
the application has lost ownership of a selection.

SELECTION_REQUEST
18
another application has requested a selection.

SELECTION_NOTIFY
19
a selection has been received.

PROXIMITY_IN
20
an input device has moved into contact with a sensing surface (e.g. a touchscreen or graphics tablet).

PROXIMITY_OUT
21
an input device has moved out of contact with a sensing surface.

DRAG_ENTER
22
the mouse has entered the window while a drag is in progress.

DRAG_LEAVE
23
the mouse has left the window while a drag is in progress.

DRAG_MOTION
24
the mouse has moved in the window while a drag is in progress.

DRAG_STATUS
25
the status of the drag operation initiated by the window has changed.

DROP_START
26
a drop operation onto the window has started.

DROP_FINISHED
27
the drop operation initiated by the window has completed.

CLIENT_EVENT
28
a message has been received from another application.

VISIBILITY_NOTIFY
29
the window visibility status has changed.

SCROLL
31
the scroll wheel was turned

WINDOW_STATE
32
the state of a window has changed. See GdkWindowState for the possible window states

SETTING
33
a setting has been modified.

OWNER_CHANGE
34
the owner of a selection has changed. This event type was added in 2.6

GRAB_BROKEN
35
a pointer or keyboard grab was broken. This event type was added in 2.8.

DAMAGE
36
the content of the window has been changed. This event type was added in 2.14.

TOUCH_BEGIN
37
A new touch event sequence has just started. This event type was added in 3.4.

TOUCH_UPDATE
38
A touch event sequence has been updated. This event type was added in 3.4.

TOUCH_END
39
A touch event sequence has finished. This event type was added in 3.4.

TOUCH_CANCEL
40
A touch event sequence has been canceled. This event type was added in 3.4.

TOUCHPAD_SWIPE
41
A touchpad swipe gesture event, the current state is determined by its phase field. This event type was added in 3.18.

TOUCHPAD_PINCH
42
A touchpad pinch gesture event, the current state is determined by its phase field. This event type was added in 3.18.

PAD_BUTTON_PRESS
43
A tablet pad button press event. This event type was added in 3.22.

PAD_BUTTON_RELEASE
44
A tablet pad button release event. This event type was added in 3.22.

PAD_RING
45
A tablet pad axis event from a "ring". This event type was added in 3.22.

PAD_STRIP
46
A tablet pad axis event from a "strip". This event type was added in 3.22.

PAD_GROUP_MODE
47
A tablet pad group mode change. This event type was added in 3.22.

EVENT_LAST
48
marks the end of the GdkEventType enumeration. Added in 2.18

*/