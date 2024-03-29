using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace GtkDotNet
{
    public class Application
    {
        public Application(string id)
            => app = Raw.Application.New(id);       

        public int Run(Action onActivate)
            => Raw.Application.Run(app, onActivate);

        public int Run(Action<Application> onActivate)
            => Raw.Application.Run(app, () => onActivate(this));

        public void AddActions(IEnumerable<GtkAction> actions)
            => Raw.Application.AddActions(app, actions);

        public void Quit() => Raw.Application.Quit(app);

        public void AddWindow(Window window) => Raw.Application.AddWindow(app, window.handle);

        public bool RegisterResources()
        {
            var assembly = Assembly.GetEntryAssembly();
            var resources = assembly.GetManifestResourceNames();
            var legacyName = $"{assembly.GetName().Name}.resources.gresource";
            var actualName = "app.gresource";
            var resourceName = resources.Contains(legacyName)
                ? legacyName
                : resources.Contains(actualName)
                ? actualName
                : null;
            if (resourceName == null)
                return false;
            var stream = assembly.GetManifestResourceStream(resourceName);
            var memIntPtr = Marshal.AllocHGlobal((int)stream.Length);
            unsafe 
            {
                var memBytePtr = (byte*)memIntPtr.ToPointer();
                var writeStream = new UnmanagedMemoryStream(memBytePtr, stream.Length, stream.Length, FileAccess.Write);
                stream.CopyTo(writeStream);
            }
            var gbytes = Raw.GBytes.New(memIntPtr, stream.Length);
            Marshal.FreeHGlobal(memIntPtr);
            var res = Raw.Resource.NewFromData(gbytes, IntPtr.Zero);
            Raw.GBytes.Unref(gbytes);
            Raw.Resource.Register(res); 
            Raw.Resource.Unref(res); 
            return true;
        }

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
        /// Run the specified Action in the main GTK thread, normal or high priority 
        /// </summary>
        /// <param name="action">Action which runs in main thread</param>
        /// <param name="highPriority"></param>
        public void BeginInvoke(Action action, bool highPriority = false)
            => BeginInvoke(highPriority ? 100 : 200, action);

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

        public Task Dispatch(Action action, bool highPriority = false)
            => Dispatch(action, highPriority ? 100 : 200);

        public Task Dispatch(Action action, int priority)
        {
            var tcs = new TaskCompletionSource();
            BeginInvoke(priority, () =>
            {
                try
                {
                    action();
                    tcs.TrySetResult();
                }
                catch (Exception e)
                {
                    tcs.TrySetException(e);
                }
            });
            return tcs.Task;
        }

        public Task<T> Dispatch<T>(Func<T> action, bool highPriority = false)
            => Dispatch(action, highPriority ? 100 : 200);

        public Task<T> Dispatch<T>(Func<T> action, int priority)
        {
            var tcs = new TaskCompletionSource<T>();
            BeginInvoke(priority, () => 
            {
                try
                {
                    tcs.TrySetResult(action());
                }
                catch (Exception e)
                {
                    tcs.TrySetException(e);
                }
            });
            return tcs.Task;
        }

        delegate bool IdleFunctionDelegate(IntPtr zero);

        readonly IntPtr app;
    }   
}
