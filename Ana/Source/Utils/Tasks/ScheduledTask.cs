namespace Ana.Source.Utils.Tasks
{
    using ActionScheduler;
    using System;

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
            this.DependencyBehavior = dependencyBehavior;
        }

        /// <summary>
        /// Gets or sets the dependency behavior of this task.
        /// </summary>
        private DependencyBehavior DependencyBehavior { get; set; }

        /// <summary>
        /// Gets or sets the time to wait (in ms) before next update (and time to wait for cancelation).
        /// </summary>
        public Int32 UpdateInterval { get; set; }

        /// <summary>
        /// Gets a value indicating whether the scheduled task is repeated.
        /// </summary>
        public Boolean IsRepeated { get; private set; }

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