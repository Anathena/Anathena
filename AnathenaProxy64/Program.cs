using System;

namespace AnathenaProxy64
{
    /// <summary>
    /// While technically unneeded, this mirrors the required Proxy32 service
    /// </summary>
    class Program
    {
        private static AnathenaProxy.AnathenaProxy AnathenaProxy;

        static void Main(String[] Args)
        {
            if (Args.Length < 3)
                return;

            Console.WriteLine("Initialized Anathena 64-bit helper process");
            AnathenaProxy = new AnathenaProxy.AnathenaProxy(Int32.Parse(Args[0]), Args[1], Args[2]);
        }

    } // End class

} // End namespace