using System;

namespace AnathenaProxy32
{
    /// <summary>
    /// Program to handle operations that are required to be run in 32 bit mode.
    /// This is needed when Ana is running in 64 bit and editing a 32 bit application
    /// </summary>
    class Program
    {
        private static AnathenaProxy.AnathenaProxy AnathenaProxy;

        static void Main(String[] Args)
        {
            if (Args.Length < 3)
                return;

            Console.WriteLine("Initialized Anathena 32-bit helper process");
            AnathenaProxy = new AnathenaProxy.AnathenaProxy(Int32.Parse(Args[0]), Args[1], Args[2]);
        }

    } // End class

} // End namespace