namespace Ana.Source.CustomControls
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Class that allows threads outside of a windows form (Presenter in an MVP) to update controls in the windows form.
    /// </summary>
    public static class ControlThreadingHelper
    {
        /// <summary>
        /// Allow for any thread to update a windows form control by passing in the control and an action to perform on the control.
        /// </summary>
        /// <typeparam name="T">The control type.</typeparam>
        /// <param name="control">The control.</param>
        /// <param name="action">The action to perform.</param>
        public static void InvokeControlAction<T>(T control, Action action) where T : Control
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action<T, Action>(InvokeControlAction), new Object[] { control, action });
            }
            else
            {
                action();
            }
        }
    }
    //// End class
}
//// End namespace