namespace Ana.Source.Utils
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class RepeatedTask
    {
        private CancellationTokenSource cancelRequest;  // Tells the task to finish
        private Task task;                              // Event that constantly checks the target process for changes

        protected Boolean cancelFlag;   // Flag that may be triggered in the update cycle to end the task
        protected Int32 abortTime;      // Time to wait (in ms) before giving up when ending scan
        protected Int32 updateInterval; // Time to wait (in ms) before next update (and time to wait for cancelation)
        private Object accessLock;

        private Boolean startedFlag;
        private Boolean finishedFlag;

        public RepeatedTask()
        {
            accessLock = new Object();
            abortTime = 3000;       // Set a default abort time
            updateInterval = 400;   // Set a default update interval
        }

        public virtual void Begin()
        {
            using (TimedLock.Lock(accessLock))
            {
                if (startedFlag)
                {
                    return;
                }

                startedFlag = true;
            }

            cancelFlag = false;
            finishedFlag = false;

            cancelRequest = new CancellationTokenSource();
            task = Task.Run(async () =>
            {
                while (true)
                {
                    UpdateController();

                    // Await with cancellation
                    await Task.Delay(updateInterval, cancelRequest.Token);
                }
            }, cancelRequest.Token);
        }

        private void UpdateController()
        {
            using (TimedLock.Lock(accessLock))
            {
                if (finishedFlag)
                {
                    return;
                }

                if (cancelFlag)
                {
                    finishedFlag = true;

                    Action action = End;
                    action.BeginInvoke(x => action.EndInvoke(x), null);
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
                    cancelRequest?.Cancel();
                    task?.Wait(abortTime);
                }
                catch (Exception)
                {
                }

                End();
            });
        }

        protected virtual void End()
        {
            startedFlag = false;
        }
    }
    //// End class
}
//// End namespace