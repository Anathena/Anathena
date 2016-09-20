using Ana.GUI;
using ScintillaNET;
using SharpCli;
using System;
using System.IO;
using System.Windows.Forms;

namespace Ana
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

            // Get SharpCli to patch all SharpDX DLLs to implement generated functions 
            InteropApp InteropApp = new InteropApp(Path.GetDirectoryName(Application.ExecutablePath));
            InteropApp.PatchAll();

            GUIMain GUIMain = new GUIMain();
            if (GUIMain != null && !GUIMain.IsDisposed)
                Application.Run();
        }

    } // End class

} // End namespace