namespace Ana.View.Controls
{
    using System.Windows.Forms;
    using System.Windows.Forms.Integration;

    internal static class WinformsHostingHelper
    {
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