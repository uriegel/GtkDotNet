using System;
using System.Threading;

namespace GtkDotNet
{
    class GtkSynchronizationContext : SynchronizationContext
    {
        public GtkSynchronizationContext(Application app) => this.app = app;
        public override void OperationCompleted() => base.OperationCompleted();
        public override void OperationStarted() => base.OperationStarted();
        public override void Post(SendOrPostCallback d, object state) 
            => app.BeginInvoke(100, () => d(state));
         
        public override void Send(SendOrPostCallback d, object state) => Post(d, state);
        public override int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout) 
            => base.Wait(waitHandles, waitAll, millisecondsTimeout);

        readonly Application app;
    }
}