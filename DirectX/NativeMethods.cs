using System;
using System.Runtime.InteropServices;

namespace DirectXShell
{
    [System.Security.SuppressUnmanagedCodeSecurity()]
    internal sealed class NativeMethods
    {
        private NativeMethods() { }

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

    } // End class

} // End namespace