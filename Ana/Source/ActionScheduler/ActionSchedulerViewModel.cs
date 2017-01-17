namespace Ana.Source.ActionScheduler
{
    using Output;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Utils.Tasks;

    /// <summary>
    /// Class to schedule tasks that are executed.
    /// </summary>
    internal class ActionSchedulerViewModel
    {
        /// <summary>
        /// The interval between scheduler calls, in milliseconds.
        /// </summary>
        private const Int32 SchedulerInterval = 16;

        /// <summary>
        /// Singleton instance of the <see cref="ActionSchedulerViewModel" /> class.
        /// </summary>
        private static Lazy<ActionSchedulerViewModel> actionSchedulerViewModelInstance = new Lazy<ActionSchedulerViewModel>(
            () => { return new ActionSchedulerViewModel(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="ActionSchedulerViewModel" /> class from being created.
        /// </summary>
        private ActionSchedulerViewModel()
        {
            this.AccessLock = new Object();
            this.Actions = new LinkedList<ScheduledTaskWrapper>();

            this.Update();
        }

        /// <summary>
        /// Gets or sets a lock for access to scheduled tasks.
        /// </summary>
        private Object AccessLock { get; set; }

        /// <summary>
        /// Gets or sets actions being scheduled.
        /// </summary>
        private LinkedList<ScheduledTaskWrapper> Actions { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ActionSchedulerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static ActionSchedulerViewModel GetInstance()
        {
            return ActionSchedulerViewModel.actionSchedulerViewModelInstance.Value;
        }

        /// <summary>
        /// Schedules a given task.
        /// </summary>
        /// <param name="scheduledTask">The task to be scheduled.</param>
        /// <param name="startAction">The start callback function.</param>
        /// <param name="updateAction">The update callback function.</param>
        /// <param name="endAction">The end callback function.</param>
        public void ScheduleAction(ScheduledTask scheduledTask, Action startAction, Action updateAction, Action endAction)
        {
            lock (this.AccessLock)
            {
                this.Actions.AddLast(new ScheduledTaskWrapper(scheduledTask, startAction, updateAction, endAction));
            }
        }

        /// <summary>
        /// Cancels the given scheduled task.
        /// </summary>
        /// <param name="scheduledTask">The scheduled task to cancel.</param>
        public void CancelAction(ScheduledTask scheduledTask)
        {
            lock (this.AccessLock)
            {
                ScheduledTaskWrapper scheduledTaskWrapper = this.Actions.Select(x => x).Where(x => x.ScheduledTask == scheduledTask).FirstOrDefault();

                if (scheduledTaskWrapper == null)
                {
                    return;
                }

                this.Actions.Remove(scheduledTaskWrapper);

                Task.Run(() => scheduledTaskWrapper.EndAction());
            }
        }

        /// <summary>
        /// The scheduler update loop. Cycles through tasks, updating them.
        /// </summary>
        private void Update()
        {
            Task.Run(
                async () =>
            {
                do
                {
                    await Task.Delay(ActionSchedulerViewModel.SchedulerInterval);

                    lock (this.AccessLock)
                    {
                        ScheduledTaskWrapper scheduledTask = this.Actions.FirstOrDefault();

                        if (scheduledTask == null)
                        {
                            continue;
                        }

                        this.Actions.Remove(scheduledTask);

                        if (scheduledTask.CanStart)
                        {
                            // Start the task
                            scheduledTask.InitializeStart();
                            Task.Run(() => scheduledTask.StartAction());
                        }
                        else if (scheduledTask.CanUpdate)
                        {
                            if (!scheduledTask.IsBusy)
                            {
                                // Update the task
                                scheduledTask.InitializeUpdate();
                                Task.Run(() => scheduledTask.UpdateAction());
                            }
                        }
                        else if (scheduledTask.CanEnd)
                        {
                            // End the task
                            Task.Run(() => scheduledTask.EndAction());

                            // End early as to prevent the task from getting requeued
                            continue;
                        }

                        // Add the task to the end of the queue
                        this.Actions.AddLast(scheduledTask);
                    }
                }
                while (true);
            });
        }

        /// <summary>
        /// Manages a scheduled task and the state information associated with the task.
        /// </summary>
        private class ScheduledTaskWrapper
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ScheduledTaskWrapper" /> class.
            /// </summary>
            /// <param name="scheduledTask">The task to be scheduled.</param>
            /// <param name="startAction">The start callback function.</param>
            /// <param name="updateAction">The update callback function.</param>
            /// <param name="endAction">The end callback function.</param>
            public ScheduledTaskWrapper(ScheduledTask scheduledTask, Action startAction, Action updateAction, Action endAction)
            {
                this.ScheduledTask = scheduledTask;
                this.StartAction = startAction;
                this.InternalUpdateAction = updateAction;
                this.UpdateAction = this.UpdateWrapper;
                this.EndAction = endAction;

                this.HasStarted = false;
                this.AccessLock = new Object();
            }

            /// <summary>
            /// Gets the scheduled task that this class wraps.
            /// </summary>
            public ScheduledTask ScheduledTask { get; private set; }

            /// <summary>
            /// Gets the start callback function.
            /// </summary>
            public Action StartAction { get; private set; }

            /// <summary>
            /// Gets the update callback function. This will be a wrapper function.
            /// </summary>
            public Action UpdateAction { get; private set; }

            /// <summary>
            /// Gets the end callback function.
            /// </summary>
            public Action EndAction { get; private set; }

            /// <summary>
            /// Gets a value indicating whether the start callback function can be called.
            /// </summary>
            public Boolean CanStart
            {
                get
                {
                    return !this.HasStarted;
                }
            }

            /// <summary>
            /// Gets a value indicating whether the update callback function can be called.
            /// </summary>
            public Boolean CanUpdate
            {
                get
                {
                    return !this.HasUpdated || this.ScheduledTask.IsRepeated;
                }
            }

            /// <summary>
            /// Gets a value indicating whether the end callback function can be called.
            /// </summary>
            public Boolean CanEnd
            {
                get
                {
                    return !this.IsBusy || this.ScheduledTask.IsRepeated;
                }
            }

            /// <summary>
            /// Gets a value indicating whether this task is busy.
            /// </summary>
            public Boolean IsBusy { get; private set; }

            /// <summary>
            /// Gets or sets a value indicating whether this task has started.
            /// </summary>
            private Boolean HasStarted { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this task has been updated.
            /// </summary>
            private Boolean HasUpdated { get; set; }

            /// <summary>
            /// Gets or sets the actual update callback function.
            /// </summary>
            private Action InternalUpdateAction { get; set; }

            /// <summary>
            /// Gets or sets a lock for access to state information.
            /// </summary>
            private Object AccessLock { get; set; }

            /// <summary>
            /// Initializes start state variables. Must be called before calling the start callback.
            /// </summary>
            public void InitializeStart()
            {
                lock (this.AccessLock)
                {
                    this.HasStarted = true;
                }
            }

            /// <summary>
            /// Initializes update state variables. Must be called before calling the update callback.
            /// </summary>
            public void InitializeUpdate()
            {
                lock (this.AccessLock)
                {
                    this.HasUpdated = true;
                    this.IsBusy = true;
                }
            }

            /// <summary>
            /// A wrapper function for the update callback. This will call the update function and update required state information.
            /// </summary>
            private void UpdateWrapper()
            {
                lock (this.AccessLock)
                {
                    if (!this.IsBusy)
                    {
                        String error = "Error in task scheduler. Attempting to update before flagging action as busy.";
                        OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Fatal, error);
                        throw new Exception(error);
                    }

                    this.InternalUpdateAction();

                    Thread.Sleep(this.ScheduledTask.UpdateInterval);

                    this.IsBusy = false;
                }
            }
        }
    }
    //// End class
}
//// End namespace