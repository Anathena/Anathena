namespace Ana.Source.Utils
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A task that repeatedly performs an action
    /// </summary>
    internal abstract class RepeatedTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatedTask" /> class
        /// </summary>
        public RepeatedTask()
        {
            this.AccessLock = new Object();
            this.AbortTime = 3000;       // Set a default abort time
            this.UpdateInterval = 400;   // Set a default update interval
        }

        /// <summary>
        /// Gets or sets a value indicating whether the task should be canceled
        /// </summary>
        protected Boolean CancelFlag { get; set; }

        /// <summary>
        /// Gets or sets the time to wait (in ms) before giving up when ending scan
        /// </summary>
        protected Int32 AbortTime { get; set; }

        /// <summary>
        /// Gets or sets the time to wait (in ms) before next update (and time to wait for cancelation)
        /// </summary>
        protected Int32 UpdateInterval { get; set; }

        /// <summary>
        /// Gets or sets the multithreading lock for shared resources
        /// </summary>
        private Object AccessLock { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the repeated task has started
        /// </summary>
        private Boolean StartedFlag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the repeated task has finished
        /// </summary>
        private Boolean FinishedFlag { get; set; }

        /// <summary>
        /// Gets or sets an object that informs the running task that it should cancel and exit
        /// </summary>
        private CancellationTokenSource CancelRequest { get; set; }

        /// <summary>
        /// Gets or sets the task being repeated
        /// </summary>
        private Task Task { get; set; }

        /// <summary>
        /// Starts the repeated task
        /// </summary>
        public virtual void Begin()
        {
            lock (this.AccessLock)
            {
                if (this.StartedFlag)
                {
                    return;
                }

                this.StartedFlag = true;
                this.CancelFlag = false;
                this.FinishedFlag = false;
                this.CancelRequest = new CancellationTokenSource();
            }

            this.Task = Task.Run(
            async () =>
            {
                while (true)
                {
                    this.UpdateController();

                    // Await with cancellation
                    await Task.Delay(this.UpdateInterval, this.CancelRequest.Token);
                }
            },
            this.CancelRequest.Token);
        }

        /// <summary>
        /// Cancels the running task
        /// </summary>
        public void End()
        {
            Task.Run(() =>
            {
                // Wait for the task to finish
                try
                {
                    this.CancelRequest?.Cancel();
                    this.Task?.Wait(AbortTime);
                }
                catch (Exception)
                {
                }

                this.OnEnd();
            });
        }

        /// <summary>
        /// Performs the update logic for the running task
        /// </summary>
        protected abstract void OnUpdate();

        /// <summary>
        /// Called when the repeated task completes
        /// </summary>
        protected virtual void OnEnd()
        {
            lock (this.AccessLock)
            {
                this.StartedFlag = false;
            }
        }

        /// <summary>
        /// Controls the repeated task, calling <see cref="OnUpdate"/> at each interval
        /// </summary>
        private void UpdateController()
        {
            lock (this.AccessLock)
            {
                if (this.FinishedFlag)
                {
                    return;
                }

                if (this.CancelFlag)
                {
                    this.FinishedFlag = true;

                    Action action = this.OnEnd;
                    action.BeginInvoke(x => action.EndInvoke(x), null);
                    return;
                }

                this.OnUpdate();
            }
        }
    }
    //// End class
}
//// End namespace