using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Anathema
{
    /// <summary>
    /// Interfaces with the operating system to query memory
    /// </summary>
    class OSInterface
    {
        protected Process TargetProcess;
        protected Settings Settings;

        public OSInterface()
        {
            Settings = new Settings();
        }
        
        public virtual void SetTargetProcess(Process TargetProcess)
        {
            this.TargetProcess = TargetProcess;
        }

        public bool IsProcess64Bit()
        {
            return IsProcess64Bit(TargetProcess.Handle);
        }

        public static bool IsProcess64Bit(IntPtr ProcessHandle)
        {
            // First do the simple check if seeing if the OS is 32 bit. If this is the case, the target
            // Process cannot be 64 bit clearly.
            if (!IsAnthema64Bit())
            {
                return false;
            }

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

        protected unsafe void QueryMemoryPages(IntPtr lpAddress, out MEMORY_BASIC_INFORMATION64 lpBuffer, uint dwLength)
        {
            // If the target is 64 bit we can use the VirtualQueryEX API call normally
            if (IsProcess64Bit())
            {
                VirtualQueryEx(TargetProcess.Handle, lpAddress, out lpBuffer, dwLength);
            }
            else
            {
                // 32 bit requires more work, first call the 32 bit version of VirtualQueryEx
                MEMORY_BASIC_INFORMATION32 lpBuffer32;
                VirtualQueryEx(TargetProcess.Handle.ToInt32(), lpAddress.ToInt32(), out lpBuffer32, dwLength);

                // Convert the results to the format of the 64 bit struct. This makes work later easier,
                // since now we only have to deal with the 64 bit struct format instead of both formats
                lpBuffer.AllocationBase = lpBuffer32.AllocationBase;
                lpBuffer.AllocationProtect = lpBuffer32.AllocationProtect;
                lpBuffer.BaseAddress = lpBuffer32.BaseAddress;
                lpBuffer.lType = lpBuffer32.Type;
                lpBuffer.Protect = lpBuffer32.Protect;
                lpBuffer.RegionSize = lpBuffer32.RegionSize;
                lpBuffer.State = lpBuffer32.State;
                lpBuffer.__alignment1 = 0;
                lpBuffer.__alignment2 = 0;
            }
        }

        #region Memory Page Flag Constants (You can find these on the internet or windows API docs)

        [Flags]
        public enum PROCESS_ACCESS_TYPE
        {
            PROCESS_TERMINATE = (0x0001),
            PROCESS_CREATE_THREAD = (0x0002),
            PROCESS_SET_SESSIONID = (0x0004),
            PROCESS_VM_OPERATION = (0x0008),
            PROCESS_VM_READ = (0x0010),
            PROCESS_VM_WRITE = (0x0020),
            PROCESS_DUP_HANDLE = (0x0040),
            PROCESS_CREATE_PROCESS = (0x0080),
            PROCESS_SET_QUOTA = (0x0100),
            PROCESS_SET_INFORMATION = (0x0200),
            PROCESS_QUERY_INFORMATION = (0x0400)
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION64
        {
            public UInt64 BaseAddress;
            public UInt64 AllocationBase;
            public UInt32 AllocationProtect;
            public UInt32 __alignment1;
            public UInt64 RegionSize;
            public UInt32 State;
            public UInt32 Protect;
            public UInt32 lType;
            public UInt32 __alignment2;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION32
        {
            public UInt64 BaseAddress;
            public UInt64 AllocationBase;
            public UInt32 AllocationProtect;
            public UInt64 RegionSize;
            public UInt32 State;
            public UInt32 Protect;
            public UInt32 Type;
        };

        [Flags]
        public enum MEMORY_STATE : int
        {
            COMMIT = 0x0001000,
            RESERVE = 0x0002000,
            FREE = 0x0010000,
            RESET_UNDO = 0x1000000
        }

        [Flags]
        public enum MEMORY_TYPE : int
        {
            PRIVATE = 0x0020000,
            MAPPED = 0x0040000,
            IMAGE = 0x1000000
        }

        [Flags]
        public enum MEMORY_PROTECTION : uint
        {
            NO_ACCESS = 0x00000001,
            READ_ONLY = 0x00000002,
            READ_WRITE = 0x00000004,
            WRITE_COPY = 0x00000008,
            EXECUTE = 0x00000010,
            EXECUTE_READ = 0x00000020,
            EXECUTE_READ_WRITE = 0x00000040,
            EXECUTE_WRITE_COPY = 0x00000080,
            GUARD = 0x00000100,
            NO_CACHE = 0x00000200,
            WRITE_COMBINE = 0x00000400,
            TARGETS_INVALID = 0x40000000,
            TARGETS_NO_UPDATE = 0x40000000,
        }
        #endregion

        #region P/Invokes
        // function declarations are found in the MSDN and in <winbase.h> 

        //		HANDLE OpenProcess(
        //			DWORD dwDesiredAccess,  // access flag
        //			BOOL bInheritHandle,    // handle inheritance option
        //			DWORD dwProcessId       // process identifier
        //			);
        [DllImport("kernel32.dll")]
        protected static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);

        //		BOOL CloseHandle(
        //			HANDLE hObject   // handle to object
        //			);
        [DllImport("kernel32.dll")]
        protected static extern Int32 CloseHandle(IntPtr hObject);

        //		BOOL ReadProcessMemory(
        //			HANDLE hProcess,              // handle to the process
        //			LPCVOID lpBaseAddress,        // base of memory area
        //			LPVOID lpBuffer,              // data buffer
        //			SIZE_T nSize,                 // number of bytes to read
        //			SIZE_T * lpNumberOfBytesRead  // number of bytes read
        //			);
        [DllImport("kernel32.dll")]
        protected static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);

        //		BOOL WriteProcessMemory(
        //			HANDLE hProcess,                // handle to process
        //			LPVOID lpBaseAddress,           // base of memory area
        //			LPCVOID lpBuffer,               // data buffer
        //			SIZE_T nSize,                   // count of bytes to write
        //			SIZE_T * lpNumberOfBytesWritten // count of bytes written
        //			);
        [DllImport("kernel32.dll")]
        protected static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION64 lpBuffer, uint dwLength);
        [DllImport("kernel32.dll")]
        private static extern int VirtualQueryEx(Int32 hProcess, Int32 lpAddress, out MEMORY_BASIC_INFORMATION32 lpBuffer, uint dwLength);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr ProcessHandle, [Out, MarshalAs(UnmanagedType.Bool)] out bool Wow64Process);

        #endregion
    }
}