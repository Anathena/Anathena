using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Anathema
{
    /// <summary>
    /// TODO: FUNCTIONALITY SHOULD BE MOVED TO MEMORYSHARP
    /// </summary>
    class OSInterface
    {
        public OSInterface()
        {

        }

        public static bool IsProcess64Bit(IntPtr ProcessHandle)
        {
            // First do the simple check if seeing if the OS is 32 bit, in which case the process wont be 64 bit
            if (!IsOS64Bit())
                return false;

            // OS is 64 bit. Must determine if target is 32 bit or 64 bit.
            bool Result;
            IsWow64Process(ProcessHandle, out Result);
            return Result;
        }

        /// <summary>
        /// Determines if the OS is 32 bit or 64 bit windows
        /// </summary>
        /// <returns></returns>
        public static bool IsOS64Bit()
        {
            return Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines if Anathema is running as 32 bit or 64 bit
        /// </summary>
        /// <returns></returns>
        public static bool IsAnthema64Bit()
        {
            return Environment.Is64BitProcess;
        }

        #region P/Invokes
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr ProcessHandle, [Out, MarshalAs(UnmanagedType.Bool)] out bool Wow64Process);
        #endregion
    }
}