using System;
using System.Runtime.InteropServices;

namespace Anathena.Source.Engine.OperatingSystems.Windows.Native
{
    /// <summary>
    /// Static class referencing all P/Invoked functions used by the library.
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// Determines whether the specified process is running under WOW64
        /// </summary>
        /// <param name="ProcessHandle"></param>
        /// <param name="Wow64Process"></param>
        /// <returns>
        /// A pointer to a value that is set to TRUE if the process is running under WOW64.
        /// If the process is running under 32-bit Windows, the value is set to FALSE.
        /// If the process is a 64-bit application running under 64-bit Windows, the value is also set to FALSE.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean IsWow64Process([In] IntPtr ProcessHandle, [Out, MarshalAs(UnmanagedType.Bool)] out bool Wow64Process);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="HObject">A valid handle to an open object.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean CloseHandle(IntPtr HObject);

        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count. 
        /// When the reference count reaches zero, the module is unloaded from the address space of the calling process and the handle is no longer valid.
        /// </summary>
        /// <param name="hModule">A handle to the loaded library module. The <see cref="LoadLibrary"/> , LoadLibraryEx, GetModuleHandle, or GetModuleHandleEx function returns this handle.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean FreeLibrary(IntPtr hModule);

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="HModule">
        /// A handle to the DLL module that contains the function or variable. The LoadLibrary, LoadLibraryEx, LoadPackagedLibrary, or GetModuleHandle function returns this handle. 
        /// The GetProcAddress function does not retrieve addresses from modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag. For more information, see LoadLibraryEx.
        /// </param>
        /// <param name="ProcName">
        /// The function or variable name, or the function's ordinal value. 
        /// If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the address of the exported function or variable. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr HModule, String ProcName);

        /// <summary>
        /// Retrieves the process identifier of the specified process.
        /// </summary>
        /// <param name="HProcess">
        /// A handle to the process. The handle must have the PROCESS_QUERY_INFORMATION or PROCESS_QUERY_LIMITED_INFORMATION access right. 
        /// For more information, see Process Security and Access Rights.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a process id to the specified handle. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 GetProcessId(IntPtr HProcess);

        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="LpFileName">
        /// The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file). 
        /// The name specified is the file name of the module and is not related to the name stored in the library module itself, 
        /// as specified by the LIBRARY keyword in the module-definition (.def) file.
        /// If the string specifies a full path, the function searches only that path for the module.
        /// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module; for more information, see the Remarks.
        /// If the function cannot find the module, the function fails. When specifying a path, be sure to use backslashes (\), not forward slashes (/). 
        /// For more information about paths, see Naming a File or Directory.
        /// If the string specifies a module name without a path and the file name extension is omitted, the function appends the default library extension .dll to the module name. 
        /// To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the module. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibrary(String LpFileName);

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="DwDesiredAccess">
        /// [Flags] he access to the process object. This access right is checked against the security descriptor for the process. This parameter can be one or more of the process access rights. 
        /// If the caller has enabled the SeDebugPrivilege privilege, the requested access is granted regardless of the contents of the security descriptor.
        /// </param>
        /// <param name="BInheritHandle">If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
        /// <param name="DwProcessId">The identifier of the local process to be opened.</param>
        /// <returns>
        /// If the function succeeds, the return value is an open handle to the specified process. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccessFlags DwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] Boolean BInheritHandle, Int32 DwProcessId);

        /// <summary>
        /// Reads data from an area of memory in a specified process. The entire area to be read must be accessible or the operation fails.
        /// </summary>
        /// <param name="HProcess">A handle to the process with memory that is being read. The handle must have PROCESS_VM_READ access to the process.</param>
        /// <param name="LpBaseAddress">
        /// A pointer to the base address in the specified process from which to read. Before any data transfer occurs, 
        /// the system verifies that all data in the base address and memory of the specified size is accessible for read access, 
        /// and if it is not accessible the function fails.
        /// </param>
        /// <param name="LpBuffer">[Out] A pointer to a buffer that receives the contents from the address space of the specified process.</param>
        /// <param name="DwSize">The number of bytes to be read from the specified process.</param>
        /// <param name="LpNumberOfBytesRead">
        /// [Out] A pointer to a variable that receives the number of bytes transferred into the specified buffer. If lpNumberOfBytesRead is NULL, the parameter is ignored.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean ReadProcessMemory(IntPtr HProcess, IntPtr LpBaseAddress, [Out] Byte[] LpBuffer, Int32 DwSize, out Int32 LpNumberOfBytesRead);

        /// <summary>
        /// Reserves or commits a region of memory within the virtual address space of a specified process. The function initializes the memory it allocates to zero, unless MEM_RESET is used.
        /// To specify the NUMA node for the physical memory, see VirtualAllocExNuma.
        /// </summary>
        /// <param name="HProcess">
        /// The handle to a process. The function allocates memory within the virtual address space of this process. 
        /// The handle must have the PROCESS_VM_OPERATION access right. For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="LpAddress">
        /// The pointer that specifies a desired starting address for the region of pages that you want to allocate. 
        /// If you are reserving memory, the function rounds this address down to the nearest multiple of the allocation granularity. 
        /// If you are committing memory that is already reserved, the function rounds this address down to the nearest page boundary. 
        /// To determine the size of a page and the allocation granularity on the host computer, use the GetSystemInfo function.
        /// </param>
        /// <param name="DwSize">
        /// The size of the region of memory to allocate, in bytes. 
        /// If lpAddress is NULL, the function rounds dwSize up to the next page boundary. 
        /// If lpAddress is not NULL, the function allocates all pages that contain one or more bytes in the range from lpAddress to lpAddress+dwSize. 
        /// This means, for example, that a 2-byte range that straddles a page boundary causes the function to allocate both pages.
        /// </param>
        /// <param name="FlAllocationType">[Flags] The type of memory allocation.</param>
        /// <param name="FlProtect">[Flags] The memory protection for the region of pages to be allocated.</param>
        /// <returns>
        /// If the function succeeds, the return value is the base address of the allocated region of pages. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr HProcess, IntPtr LpAddress, Int32 DwSize, MemoryAllocationFlags FlAllocationType, MemoryProtectionFlags FlProtect);

        /// <summary>
        /// Releases, decommits, or releases and decommits a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="HProcess">A handle to a process. The function frees memory within the virtual address space of the process. 
        /// The handle must have the PROCESS_VM_OPERATION access right. For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="LpAddress">
        /// A pointer to the starting address of the region of memory to be freed. 
        /// If the dwFreeType parameter is MEM_RELEASE, lpAddress must be the base address returned by the <see cref="VirtualAllocEx"/> function when the region is reserved.
        /// </param>
        /// <param name="DwSize">
        /// The size of the region of memory to free, in bytes. 
        /// If the dwFreeType parameter is MEM_RELEASE, dwSize must be 0 (zero). 
        /// The function frees the entire region that is reserved in the initial allocation call to <see cref="VirtualAllocEx"/>. 
        /// If dwFreeType is MEM_DECOMMIT, the function decommits all memory pages that contain one or more bytes in the range from the lpAddress parameter to (lpAddress+dwSize). 
        /// This means, for example, that a 2-byte region of memory that straddles a page boundary causes both pages to be decommitted. 
        /// If lpAddress is the base address returned by VirtualAllocEx and dwSize is 0 (zero), the function decommits the entire region that is allocated by <see cref="VirtualAllocEx"/>. 
        /// After that, the entire region is in the reserved state.
        /// </param>
        /// <param name="DwFreeType">[Flags] The type of free operation.</param>
        /// <returns>
        /// If the function succeeds, the return value is a nonzero value. 
        /// If the function fails, the return value is 0 (zero). To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean VirtualFreeEx(IntPtr HProcess, IntPtr LpAddress, Int32 DwSize, MemoryReleaseFlags DwFreeType);

        /// <summary>
        /// Changes the protection on a region of committed pages in the virtual address space of a specified process.
        /// </summary>
        /// <param name="HProcess">
        /// A handle to the process whose memory protection is to be changed. The handle must have the PROCESS_VM_OPERATION access right. 
        /// For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="LpAddress">
        /// A pointer to the base address of the region of pages whose access protection attributes are to be changed. 
        /// All pages in the specified region must be within the same reserved region allocated when calling the VirtualAlloc or VirtualAllocEx function using MEM_RESERVE. 
        /// The pages cannot span adjacent reserved regions that were allocated by separate calls to VirtualAlloc or <see cref="VirtualAllocEx"/> using MEM_RESERVE.
        /// </param>
        /// <param name="DwSize">
        /// The size of the region whose access protection attributes are changed, in bytes. 
        /// The region of affected pages includes all pages containing one or more bytes in the range from the lpAddress parameter to (lpAddress+dwSize). 
        /// This means that a 2-byte range straddling a page boundary causes the protection attributes of both pages to be changed.
        /// </param>
        /// <param name="FlNewProtect">
        /// The memory protection option. This parameter can be one of the memory protection constants. 
        /// For mapped views, this value must be compatible with the access protection specified when the view was mapped (see MapViewOfFile, MapViewOfFileEx, and MapViewOfFileExNuma).
        /// </param>
        /// <param name="LpflOldProtect">
        /// A pointer to a variable that receives the previous access protection of the first page in the specified region of pages. 
        /// If this parameter is NULL or does not point to a valid variable, the function fails.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Boolean VirtualProtectEx(IntPtr HProcess, IntPtr LpAddress, Int32 DwSize, MemoryProtectionFlags FlNewProtect, out MemoryProtectionFlags LpflOldProtect);

        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="HProcess">
        /// A handle to the process whose memory information is queried. 
        /// The handle must have been opened with the PROCESS_QUERY_INFORMATION access right, which enables using the handle to read information from the process object. 
        /// For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="LpAddress">
        /// A pointer to the base address of the region of pages to be queried. 
        /// This value is rounded down to the next page boundary. 
        /// To determine the size of a page on the host computer, use the GetSystemInfo function. 
        /// If lpAddress specifies an address above the highest memory address accessible to the process, the function fails with ERROR_INVALID_PARAMETER.
        /// </param>
        /// <param name="LpBuffer">[Out] A pointer to a <see cref="MemoryBasicInformation32"/> structure in which information about the specified page range is returned.</param>
        /// <param name="DwLength">The size of the buffer pointed to by the lpBuffer parameter, in bytes.</param>
        /// <returns>
        /// The return value is the actual number of bytes returned in the information buffer. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 VirtualQueryEx(IntPtr HProcess, IntPtr LpAddress, out MemoryBasicInformation32 LpBuffer, Int32 DwLength);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 VirtualQueryEx(IntPtr HProcess, IntPtr LpAddress, out MemoryBasicInformation64 LpBuffer, Int32 DwLength);

        /// <summary>
        /// Writes data to an area of memory in a specified process. The entire area to be written to must be accessible or the operation fails.
        /// </summary>
        /// <param name="HProcess">A handle to the process memory to be modified. The handle must have PROCESS_VM_WRITE and PROCESS_VM_OPERATION access to the process.</param>
        /// <param name="LpBaseAddress">
        /// A pointer to the base address in the specified process to which data is written. Before data transfer occurs, the system verifies that 
        /// all data in the base address and memory of the specified size is accessible for write access, and if it is not accessible, the function fails.
        /// </param>
        /// <param name="LpBuffer">A pointer to the buffer that contains data to be written in the address space of the specified process.</param>
        /// <param name="NSize">The number of bytes to be written to the specified process.</param>
        /// <param name="LpNumberOfBytesWritten">
        /// A pointer to a variable that receives the number of bytes transferred into the specified process. 
        /// This parameter is optional. If lpNumberOfBytesWritten is NULL, the parameter is ignored.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean WriteProcessMemory(IntPtr HProcess, IntPtr LpBaseAddress, Byte[] LpBuffer, Int32 NSize, out Int32 LpNumberOfBytesWritten);
    }

} // End namespace