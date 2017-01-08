namespace Ana.Source.CustomControls
{
    using System.Windows.Forms;
    using System.Windows.Forms.Integration;

    /// <summary>
    /// Helper class for embedded windows forms controls in WPF.
    /// </summary>
    internal static class WinformsHostingHelper
    {
        /// <summary>
        /// Creates a windows form hosting object for a winforms control.
        /// </summary>
        /// <param name="control">The control to host.</param>
        /// <returns>The windows forms hosting object.</returns>
        public static WindowsFormsHost CreateHostedControl(Control control)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            host.Child = control;
            return host;
        }
    }
    //// End class
}
//// End namespace