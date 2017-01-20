namespace Ana.Source.ActionScheduler
{
    using System;
    using System.ComponentModel;
    using Utils.Extensions;

    /// <summary>
    /// A task that repeatedly performs an action.
    /// </summary>
    internal abstract class ScheduledTask : INotifyPropertyChanged
    {
        /// <summary>
        /// The default update loop time.
        /// </summary>
        private const Int32 DefaultUpdateTime = 400;

        /// <summary>
        /// The minimum progress.
        /// </summary>
        private const Single MinimumProgress = 0f;

        /// <summary>
        /// The maximum progress.
        /// </summary>
        private const Single MaximumProgress = 100f;

        /// <summary>
        /// The default progress completion threshold.
        /// </summary>
        private const Single DefaultProgressCompletionThreshold = 100f;

        private Single progress;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledTask" /> class.
        /// </summary>
        /// <param name="taskName">The dependencies and dependency behavior of this task.</param>
        /// <param name="isRepeated">Whether or not this task is repeated.</param>
        /// <param name="trackProgress">Whether or not progress is tracked for this task.</param>
        public ScheduledTask(String taskName, Boolean isRepeated, Boolean trackProgress) : this(taskName, isRepeated, trackProgress, new DependencyBehavior())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledTask" /> class.
        /// </summary>
        /// <param name="taskName">The dependencies and dependency behavior of this task.</param>
        /// <param name="isRepeated">Whether or not this task is repeated.</param>
        /// <param name="trackProgress">Whether or not progress is tracked for this task.</param>
        /// <param name="dependencyBehavior">The dependencies and dependency behavior of this task.</param>
        public ScheduledTask(String taskName, Boolean isRepeated, Boolean trackProgress, DependencyBehavior dependencyBehavior)
        {
            this.TaskName = taskName;
            this.IsRepeated = isRepeated;
            this.IsTaskComplete = !trackProgress;
            this.DependencyBehavior = dependencyBehavior == null ? new DependencyBehavior() : dependencyBehavior;

            this.ProgressCompletionThreshold = ScheduledTask.DefaultProgressCompletionThreshold;
        }

        /// <summary>
        /// Gets or sets the dependency behavior of this task.
        /// </summary>
        public DependencyBehavior DependencyBehavior { get; set; }

        /// <summary>
        /// Gets or sets the time to wait (in ms) before next update (and time to wait for cancelation).
        /// </summary>
        public Int32 UpdateInterval { get; set; }

        /// <summary>
        /// Gets a value indicating whether the scheduled task is repeated.
        /// </summary>
        public Boolean IsRepeated { get; private set; }

        /// <summary>
        /// Gets or sets the name of this task.
        /// </summary>
        public String TaskName { get; set; }

        /// <summary>
        /// Gets a value indicating whether the scheduled task has completed. This is not in terms of progress, but instead indicates the task is entirely done.
        /// </summary>
        public Boolean IsTaskComplete { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the scheduled task has completed in terms of progress, although not necessarily finalized.
        /// </summary>
        public Boolean IsProgressComplete
        {
            get
            {
                return this.Progress >= this.ProgressCompletionThreshold;
            }
        }

        /// <summary>
        /// Gets or sets the progress of this task.
        /// </summary>
        public Single Progress
        {
            get
            {
                return this.progress;
            }

            private set
            {
                this.progress = value;
                this.NotifyPropertyChanged(nameof(this.Progress));
            }
        }

        /// <summary>
        /// Gets or sets the progress completion threshold. Progress higher this threshold will be considered complete.
        /// </summary>
        protected Single ProgressCompletionThreshold { get; set; }

        /// <summary>
        /// Starts the repeated task.
        /// </summary>
        public void Begin()
        {
            ActionSchedulerViewModel.GetInstance().ScheduleAction(this, this.OnBegin, this.OnUpdate, this.OnEnd);
        }

        /// <summary>
        /// Cancels the running task.
        /// </summary>
        public void Cancel()
        {
            ActionSchedulerViewModel.GetInstance().CancelAction(this);
        }

        /// <summary>
        /// Updates the progress of this task.
        /// </summary>
        /// <param name="progress">The new progress.</param>
        public void UpdateProgress(Single progress)
        {
            this.Progress = progress.Clamp(ScheduledTask.MinimumProgress, ScheduledTask.MaximumProgress);
        }

        /// <summary>
        /// Updates the progress of this task.
        /// </summary>
        /// <param name="subtotal">The current subtotal of an arbitrary progress goal.</param>
        /// <param name="total">The progress goal total.</param>
        public void UpdateProgress(Int32 subtotal, Int32 total)
        {
            this.UpdateProgress(total <= 0 ? 0f : (((Single)subtotal / (Single)total) * ScheduledTask.MaximumProgress + ScheduledTask.MinimumProgress));
        }

        /// <summary>
        /// Called when the scheduled task starts.
        /// </summary>
        protected virtual void OnBegin()
        {
        }

        /// <summary>
        /// Called when the scheduled task is updated
        /// </summary>
        protected virtual void OnUpdate()
        {
        }

        /// <summary>
        /// Called when the repeated task completes.
        /// </summary>
        protected virtual void OnEnd()
        {
        }

        /// <summary>
        /// Indicates that a given property in this project item has changed.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void NotifyPropertyChanged(String propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //// End class
}
//// End namespace