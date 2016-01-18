using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema
{
    abstract class RepeatedTask
    {
        private CancellationTokenSource CancelRequest;  // Tells the task to finish
        private Task Task;                              // Event that constantly checks the target process for changes

        protected Boolean CancelFlag;   // Flag that may be triggered in the update cycle to end the task
        protected Boolean Handled;      // Flag to determine if cancel flag has been handled
        protected Int32 AbortTime;      // Time to wait (in ms) before giving up when ending scan
        protected Int32 WaitTime;       // Time to wait (in ms) for a cancel request between each scan

        public RepeatedTask()
        {
            AbortTime = 3000;   // Set a default abort time
            WaitTime = 400;     // Set a default wait time
        }

        public virtual void Begin()
        {
            CancelFlag = false;
            Handled = false;

            CancelRequest = new CancellationTokenSource();
            Task = Task.Run(async () =>
            {
                while (true)
                {
                    if (!Handled)
                    {
                        if (CancelFlag)
                        {
                            Handled = true;
                            Action Action = End;
                            Action.BeginInvoke(x => Action.EndInvoke(x), null);
                        }
                        else
                        {
                            Update();
                        }
                    }
                    // Await with cancellation
                    await Task.Delay(WaitTime, CancelRequest.Token);
                }
            }, CancelRequest.Token);

        }

        protected abstract void Update();

        public virtual void End()
        {
            // Wait for the filter to finish
            CancelRequest.Cancel();
            try { Task.Wait(AbortTime); }
            catch (AggregateException) { }
        }
    }
}
