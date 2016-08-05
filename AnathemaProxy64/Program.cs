using AnathemaProxy;
using System;

namespace AnathemaProxy64
{
    /// <summary>
    /// While technically unneeded, this mirrors the required Proxy32 service
    /// </summary>
    class Program
    {
        private static ProxyService ProxyService;

        static void Main(String[] Args)
        {
            ProxyService = new ProxyService();
        }

    } // End class

} // End namespace