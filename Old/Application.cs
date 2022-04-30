using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace GtkDotNet
{
    public class Application
    {
        public void RegisterStylesFromResource(string path)
        {
            var cssProvider = Raw.CssProvider.New();
            Raw.CssProvider.LoadFromResource(cssProvider, path);
            var display = Raw.Display.GetDefault();
            var screen = Raw.Display.GetDefaultScreen(display);
            Raw.StyleContext.AddProviderForScreen(screen, cssProvider, GtkDotNet.StyleProviderPriority.Application);
        }

        readonly IntPtr app;
    }   
}
