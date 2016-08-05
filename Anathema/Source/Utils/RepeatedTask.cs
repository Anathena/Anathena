using System;
using System.Threading;
using System.Threading.Tasks;

namespace Anathema.Source.Utils
{
    public abstract class RepeatedTask
    {
        private CancellationTokenSource CancelRequest;  // Tells the task to finish
        private Task Task;                              // Event that constantly checks the target process for changes

        protected Boolean CancelFlag;   // Flag that may be triggered in the update cycle to end the task
        protected Int32 AbortTime;      // Time to wait (in ms) before giving up when ending scan
        protected Int32 UpdateInterval; // Time to wait (in ms) before next update (and time to wait for cancelation)
        private Object AccessLock;

        private Boolean StartedFlag;
        private Boolean FinishedFlag;

        public RepeatedTask()
        {
            AccessLock = new Object();
            AbortTime = 3000;       // Set a default abort time
            UpdateInterval = 400;   // Set a default update interval
        }

        public virtual void Begin()
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (StartedFlag)
                    return;

                StartedFlag = true;
            }

            CancelFlag = false;
            FinishedFlag = false;

            CancelRequest = new CancellationTokenSource();
            Task = Task.Run(async () =>
            {
                while (true)
                {
                    UpdateController();

                    // Await with cancellation
                    await Task.Delay(UpdateInterval, CancelRequest.Token);
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

        public void TriggerEnd()
        {
            Task.Run(() =>
            {
                // Wait for the task to finish
                try
                {
                    CancelRequest?.Cancel();
                    Task?.Wait(AbortTime);
                }
                catch (Exception) { }


                End();
            });
        }

        protected virtual void End()
        {
            StartedFlag = false;
        }

    } // End class

} // End namespace