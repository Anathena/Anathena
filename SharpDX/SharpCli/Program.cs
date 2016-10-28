namespace SharpCli
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Class to patch generated SharpDx method stubs, and replace them with the actual required method.
    /// This is necessary to use almost all of the features that this project requires.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">Arguments passed to the program</param>
        public static void Main(String[] args)
        {
            // Get SharpCli to patch all SharpDX DLLs to implement generated functions 
            InteropApp interopApp = new InteropApp(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            interopApp.PatchAll();
        }
    }
    //// End class
}
//// End namespace