namespace Ana.Source.ActionScheduler
{
    using System;
    using Utils.Extensions;

    /// <summary>
    /// A task that repeatedly performs an action.
    /// </summary>
    internal abstract class ScheduledTask
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledTask" /> class.
        /// </summary>
        public ScheduledTask(Boolean isRepeated) : this(isRepeated, new DependencyBehavior())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledTask" /> class.
        /// </summary>
        /// <param name="dependencyBehavior">The dependencies and dependency behavior of this task.</param>
        public ScheduledTask(Boolean isRepeated, DependencyBehavior dependencyBehavior)
        {
            this.IsRepeated = isRepeated;
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
        /// Gets a value indicating whether the scheduled task has completed in terms of progress.
        /// </summary>
        public Boolean HasProgressCompleted { get; private set; }

        /// <summary>
        /// Gets or sets the progress of this task.
        /// </summary>
        private Single Progress { get; set; }

        /// <summary>
        /// Gets or sets the progress completion threshold. Progress higher this threshold will be considered complete.
        /// </summary>
        private Single ProgressCompletionThreshold { get; set; }

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

            if (this.Progress >= this.ProgressCompletionThreshold)
            {
                this.HasProgressCompleted = true;
            }
            else
            {
                this.HasProgressCompleted = false;
            }
        }

        /// <summary>
        /// Updates the progress of this task.
        /// </summary>
        /// <param name="subtotal">The current subtotal of an arbitrary progress goal.</param>
        /// <param name="total">The progress goal total.</param>
        public void UpdateProgress(Int32 subtotal, Int32 total)
        {
            this.UpdateProgress(total <= 0 ? 0f : ((subtotal / total) * ScheduledTask.MaximumProgress + ScheduledTask.MinimumProgress));
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
    }
    //// End class
}
//// End namespace