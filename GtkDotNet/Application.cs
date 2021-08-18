using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

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

        public void RegisterResources()
        {
            var assembly = Assembly.GetEntryAssembly();
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.resources.gresource");
            var memIntPtr = Marshal.AllocHGlobal((int)stream.Length);
            unsafe 
            {
                var memBytePtr = (byte*)memIntPtr.ToPointer();
                var writeStream = new UnmanagedMemoryStream(memBytePtr, stream.Length, stream.Length, FileAccess.Write);
                stream.CopyTo(writeStream);
            }
            var gbytes = Raw.GBytes.New(memIntPtr, stream.Length);
            Marshal.FreeHGlobal(memIntPtr);
            var res = Raw.Gio.NewResourceFromData(gbytes, IntPtr.Zero);
            Raw.GBytes.Unref(gbytes);
            Raw.Gio.RegisterResources(res); 
        }

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
