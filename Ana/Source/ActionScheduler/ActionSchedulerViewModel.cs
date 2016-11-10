namespace Ana.Source.ActionScheduler
{
    using System.Collections.Generic;

    /// <summary>
    /// Class to schedule repeated tasks that are executed
    /// (This is not implemented yet, nor was it ever. This is just an idea. Think how Eclipse bottle necks certain actions
    /// or whatever and forces them to execute in sequence to avoid overlap of conflicting actions)
    /// </summary>
    internal class ActionSchedulerViewModel
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="ActionSchedulerViewModel" /> class from being created
        /// </summary>
        private ActionSchedulerViewModel()
        {
        }

        /// <summary>
        /// Gets or sets actions being scheduled
        /// </summary>
        private Queue<IAction> Actions { get; set; }
    }
    //// End class
}
//// End namespace