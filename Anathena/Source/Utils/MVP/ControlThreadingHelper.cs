using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ana.Source.Utils.MVP
{
    /// <summary>
    /// Class that allows threads outside of a windows form (Presenter in an MVP) to update controls in the windows form
    /// </summary>
    public static class ControlThreadingHelper
    {
        /// <summary>
        /// Allow for any thread to update a windows form control by passing in the control and an action to perform on the control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Control"></param>
        /// <param name="Action"></param>
        public static void InvokeControlAction<T>(T Control, Action Action) where T : Control
        {
            if (Control.InvokeRequired)
                Control.Invoke(new Action<T, Action>(InvokeControlAction), new Object[] { Control, Action });
            else
                Action();
        }

        internal class NestedInvoke<T>
        {
            public delegate void Delegate(IEnumerable<T> Controls, Action Action);
        }

        /// <summary>
        /// (TEST) Multiple controls that are involved in the same action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Controls"></param>
        /// <param name="Action"></param>
        public static void InvokeMultiControlAction<T>(IEnumerable<T> Controls, Action Action) where T : Control
        {
            while (Controls.Count() > 0 && !Controls.First().InvokeRequired)
                Controls = Controls.Skip(1);

            if (Controls.Count() <= 0)
            {
                Action();
                return;
            }

            if (Controls.First().InvokeRequired)
            {
                if (Controls.Count() > 1)
                    Controls.First().Invoke(new NestedInvoke<T>.Delegate(InvokeMultiControlAction), new Object[] { Controls.Skip(1), Action });
                else
                    Controls.First().Invoke(new Action<T, Action>(InvokeControlAction), new Object[] { Controls.First(), Action });
            }
            else
                Action();
        }

    } // End class

} // End namespace