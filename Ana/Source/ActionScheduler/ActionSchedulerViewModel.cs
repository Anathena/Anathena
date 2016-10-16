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
        private ActionSchedulerViewModel()
        {
        }

        private List<IAction> Actions { get; set; }

        private void Update()
        {
        }
    }
    //// End class
}
//// End namespace