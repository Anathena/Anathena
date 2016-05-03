using Anathema.Source.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema.Utils
{
    abstract class RepeatedTask
    {
        private CancellationTokenSource CancelRequest;  // Tells the task to finish
        private Task Task;                              // Event that constantly checks the target process for changes

        protected Boolean CancelFlag;   // Flag that may be triggered in the update cycle to end the task
        protected Int32 AbortTime;      // Time to wait (in ms) before giving up when ending scan
        protected Int32 WaitTime;       // Time to wait (in ms) for a cancel request between each scan
        private Object AccessLock;

        private Boolean FinishedFlag;

        public RepeatedTask()
        {
            AccessLock = new Object();
            AbortTime = 3000;   // Set a default abort time
            WaitTime = 400;     // Set a default wait time
        }

        public virtual void Begin()
        {
            CancelFlag = false;
            FinishedFlag = false;

            CancelRequest = new CancellationTokenSource();
            Task = Task.Run(async () =>
            {
                while (true)
                {
                    UpdateController();

                    // Await with cancellation
                    await Task.Delay(WaitTime, CancelRequest.Token);
                }
            }, CancelRequest.Token);
        }

        private void UpdateController()
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (FinishedFlag)
                    return;

                if (CancelFlag)
                {
                    FinishedFlag = true;
                    Action Action = End;
                    Action.BeginInvoke(X => Action.EndInvoke(X), null);
                    return;
                }

                Update();
            }
        }

        protected abstract void Update();

        public virtual void End()
        {
            // Wait for the task to finish
            CancelRequest?.Cancel();
            try { Task?.Wait(AbortTime); }
            catch (AggregateException) { }
        }

    } // End class

} // End namespace