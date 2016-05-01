using Be.Windows.Forms;
using System;
using System.Windows.Forms;

namespace Anathema.Utils.MVP
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

        internal static void InvokeControlAction<T>(HexBox hexEditorBox, Action p)
        {
            throw new NotImplementedException();
        }

        internal static void InvokeControlAction(ListView constraintsListView, Action p)
        {
            throw new NotImplementedException();
        }
    } // End class

} // End namespace