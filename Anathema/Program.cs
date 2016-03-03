using Anathema.GUI;
using ScintillaNET;
using System;
using System.Windows.Forms;

namespace Anathema
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Required to make Scintilla play nicely with DockSuite
            Scintilla.SetDestroyHandleBehavior(true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GUIMain GUIMain = new GUIMain();
            if (GUIMain != null && !GUIMain.IsDisposed)
                Application.Run();
        }

    } // End class

} // End namespace