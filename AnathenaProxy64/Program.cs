namespace AnathenaProxy32
{
    using System;

    /// <summary>
    /// Program to handle operations that are required to be run in the same bitness as Anathena.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Gets or sets the proxy program.
        /// </summary>
        private static AnathenaProxy.AnathenaProxy Proxy { get; set; }

        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">The args to pass to the proxy service.</param>
        public static void Main(String[] args)
        {
            if (args.Length < 3)
            {
                return;
            }

            Program.Proxy = new AnathenaProxy.AnathenaProxy(Int32.Parse(args[0]), args[1], args[2]);
        }
    }
    //// End class
}
//// End namespace