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

        public void EnableSynchronizationContext()
            => SynchronizationContext.SetSynchronizationContext(new GtkSynchronizationContext(this));

        /// <summary>
        /// Run the specified Action in the main GTK thread
        /// </summary>
        /// <param name="priority">Between 100 (high), 200 (idle) and 300 (low)</param>
        /// <param name="action">Action which runs in main thread</param>
        public void BeginInvoke(int priority, Action action)
        {
            IdleFunctionDelegate mainFunction = _ =>
            {
                action();
                mainFunction = null;
                action = null;
                return false;
            };
            var delegat = mainFunction as Delegate;
            var funcPtr = Marshal.GetFunctionPointerForDelegate(delegat);
            Raw.Gtk.IdleAddFull(priority, funcPtr, IntPtr.Zero, IntPtr.Zero);
        }

        delegate bool IdleFunctionDelegate(IntPtr zero);

        readonly IntPtr app;
    }   
}
