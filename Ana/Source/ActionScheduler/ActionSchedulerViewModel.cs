namespace Ana.Source.ActionScheduler
{
    using Docking;
    using Output;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Utils.DataStructures;

    /// <summary>
    /// Class to schedule tasks that are executed.
    /// </summary>
    internal class ActionSchedulerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(ActionSchedulerViewModel);

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
        private ActionSchedulerViewModel() : base("Action Scheduler")
        {
            this.ContentId = ActionSchedulerViewModel.ToolContentId;
            this.AccessLock = new Object();
            this.Actions = new LinkedList<ScheduledTaskManager>();

            this.Update();
        }

        /// <summary>
        /// Gets the tasks that are actively running.
        /// </summary>
        public ObservableCollection<ScheduledTask> ActiveTasks
        {
            get
            {
                return new ObservableCollection<ScheduledTask>(this.Actions.Select(x => x.ScheduledTask).Where(x => !x.IsTaskComplete));
            }
        }

        /// <summary>
        /// Gets or sets a lock for access to scheduled tasks.
        /// </summary>
        private Object AccessLock { get; set; }

        /// <summary>
        /// Gets or sets actions being scheduled.
        /// </summary>
        private LinkedList<ScheduledTaskManager> Actions { get; set; }

        /// <summary>
        /// Gets or sets the next action being scheduled.
        /// </summary>
        private LinkedListNode<ScheduledTaskManager> NextAction { get; set; }

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
                // Do not schedule actions of the same type
                if (this.Actions.Select(x => x.ScheduledTask.GetType()).Any(x => x == scheduledTask.GetType()))
                {
                    OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Warn, "Action not scheduled. This action is already queued.");
                    return;
                }

                this.Actions.AddLast(new ScheduledTaskManager(scheduledTask, startAction, updateAction, endAction));
                this.RaisePropertyChanged(nameof(this.ActiveTasks));
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
                ScheduledTaskManager scheduledTaskManager = this.Actions.Select(x => x).Where(x => x.ScheduledTask == scheduledTask).FirstOrDefault();

                if (scheduledTaskManager == null)
                {
                    return;
                }

                scheduledTaskManager.Cancel();
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
                        // Check for a change in the active tasks
                        if (!this.Actions.Select(x => x.ScheduledTask).SequenceEqual(this.ActiveTasks))
                        {
                            this.RaisePropertyChanged(nameof(this.ActiveTasks));
                        }

                        // Cycle to the next task
                        this.NextAction = this.NextAction?.NextOrFirst() ?? this.Actions.First;

                        if (NextAction == null)
                        {
                            continue;
                        }

                        ScheduledTaskManager nextTask = this.NextAction.Value;

                        if (nextTask.CanStart)
                        {
                            // Check if dependencies are complete for this task to start
                            if (nextTask.ScheduledTask.DependencyBehavior.IsDependencyRequiredForStart
                                && !this.DependenciesResolved(nextTask.ScheduledTask))
                            {
                                continue;
                            }

                            // Start the task
                            nextTask.InitializeStart();
                            Task.Run(() => nextTask.StartAction());
                        }
                        else if (nextTask.CanUpdate)
                        {
                            // Check if dependencies are complete for this task to update
                            if (nextTask.ScheduledTask.DependencyBehavior.IsDependencyRequiredForUpdate
                                && !this.DependenciesResolved(nextTask.ScheduledTask))
                            {
                                continue;
                            }

                            // Update the task
                            nextTask.InitializeUpdate();
                            Task.Run(() => nextTask.UpdateAction());
                        }
                        else if (nextTask.CanEnd)
                        {
                            // End the task
                            Task.Run(() => nextTask.EndAction());

                            // Permanently remove this task
                            this.Actions.Remove(nextTask);
                            this.RaisePropertyChanged(nameof(this.ActiveTasks));
                        }
                    }
                }
                while (true);
            });
        }

        /// <summary>
        /// Determines if the depencies are resolved for a given scheduled task.
        /// </summary>
        /// <param name="scheduledTask">The scheduled task.</param>
        /// <returns>True if the dependencies are resolved, otherwise false.</returns>
        private Boolean DependenciesResolved(ScheduledTask scheduledTask)
        {
            IEnumerable<Type> completedDependencies = this.Actions.Select(x => x.ScheduledTask)
                .Where(x => x.IsTaskComplete)
                .Select(x => x.GetType());

            return scheduledTask.DependencyBehavior.AreDependenciesResolved(completedDependencies);
        }

        /// <summary>
        /// Manages a scheduled task and the state information associated with the task.
        /// </summary>
        private class ScheduledTaskManager
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ScheduledTaskManager" /> class.
            /// </summary>
            /// <param name="scheduledTask">The task to be scheduled.</param>
            /// <param name="startAction">The start callback function.</param>
            /// <param name="updateAction">The update callback function.</param>
            /// <param name="endAction">The end callback function.</param>
            public ScheduledTaskManager(ScheduledTask scheduledTask, Action startAction, Action updateAction, Action endAction)
            {
                this.ScheduledTask = scheduledTask;
                this.InternalStartAction = startAction;
                this.StartAction = this.Start;
                this.InternalUpdateAction = updateAction;
                this.UpdateAction = this.Update;
                this.EndAction = endAction;

                this.IsCanceled = false;
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
                    return !this.IsCanceled && !this.HasStarted;
                }
            }

            /// <summary>
            /// Gets a value indicating whether the update callback function can be called.
            /// </summary>
            public Boolean CanUpdate
            {
                get
                {
                    return !this.IsCanceled && !this.IsBusy && (!this.HasUpdated || this.ScheduledTask.IsRepeated);
                }
            }

            /// <summary>
            /// Gets a value indicating whether the end callback function can be called.
            /// </summary>
            public Boolean CanEnd
            {
                get
                {
                    return !this.IsBusy && (this.HasUpdated || this.IsCanceled);
                }
            }

            /// <summary>
            /// Gets a value indicating whether this task is busy.
            /// </summary>
            public Boolean IsBusy { get; private set; }

            /// <summary>
            /// Gets a value indicating whether this task has been canceled.
            /// </summary>
            public Boolean IsCanceled { get; private set; }

            /// <summary>
            /// Gets or sets a value indicating whether this task has started.
            /// </summary>
            private Boolean HasStarted { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this task has been updated.
            /// </summary>
            private Boolean HasUpdated { get; set; }

            /// <summary>
            /// Gets or sets the actual start callback function.
            /// </summary>
            private Action InternalStartAction { get; set; }

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
                    this.IsBusy = true;
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
            /// Canels this task.
            /// </summary>
            public void Cancel()
            {
                this.IsCanceled = true;
            }

            /// <summary>
            /// A wrapper function for the start callback. This will call the start function and update required state information.
            /// </summary>
            private void Start()
            {
                lock (this.AccessLock)
                {
                    if (!this.IsBusy)
                    {
                        String error = "Error in task scheduler. Attempting to start before flagging action as busy.";
                        OutputViewModel.GetInstance().Log(OutputViewModel.LogLevel.Fatal, error);
                        throw new Exception(error);
                    }

                    this.InternalStartAction();
                    this.IsBusy = false;
                }
            }

            /// <summary>
            /// A wrapper function for the update callback. This will call the update function and update required state information.
            /// </summary>
            private void Update()
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