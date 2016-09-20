using System.Windows.Forms;

namespace Ana.Source.Utils
{
    /// <summary>
    /// Helper class to request input from the user via a GUI based editor (ie script editor, offset editor, etc)
    /// </summary>
    public class InputRequest
    {
        /// <summary>
        /// Delegate function that returns the dialog result from a GUI based editor
        /// </summary>
        /// <returns></returns>
        public delegate DialogResult InputRequestDelegate();

    } // End class

} // End namespace