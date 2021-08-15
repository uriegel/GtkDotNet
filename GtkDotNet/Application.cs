using System;
using System.Collections.Generic;

namespace GtkDotNet
{
    public class Application
    {
        public Application(string id)
            => app = Raw.Application.New(id);

        public int Run(Action onActivate)
            => Raw.Application.Run(app, onActivate);

        public void AddActions(IEnumerable<GtkAction> actions)
            => Raw.Application.AddActions(app, actions);

        public void Quit() => Raw.Application.Quit(app);

        public void AddWindow(Window window) => Raw.Application.AddWindow(app, window.handle);
        readonly IntPtr app;
    }   
}
