using AnathenaProxy;
using System;

namespace AnathenaProxy64
{
    /// <summary>
    /// While technically unneeded, this mirrors the required Proxy32 service
    /// </summary>
    class Program
    {
        private static ProxyService ProxyService;

        static void Main(String[] Args)
        {
            if (Args.Length < 1)
                return;

            Console.WriteLine("Initialized Anathena 64-bit helper process with channel name: " + Args[0]);
            ProxyService = new ProxyService(Args[0]);
        }

    } // End class

} // End namespace