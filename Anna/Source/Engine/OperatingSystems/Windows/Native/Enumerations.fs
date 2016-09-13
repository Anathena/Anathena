namespace Anna.Source.Engine.OperatingSystems.Windows.Enumerations

open System
open System.Runtime.InteropServices

/// <summary>
/// Memory-allocation options list.
/// </summary>
[<Flags>]
type MemoryAllocationFlags =
    /// <summary>
    /// Allocates memory charges (from the overall size of memory and the paging files on disk) for the specified reserved memory pages. 
    /// The function also guarantees that when the caller later initially accesses the memory, the contents will be zero. 
    /// Actual physical pages are not allocated unless/until the virtual addresses are actually accessed.
    /// To reserve and commit pages in one step, call <see cref="NativeMethods.VirtualAllocEx"/> with MEM_COMMIT | MEM_RESERVE.
    /// The function fails if you attempt to commit a page that has not been reserved. The resulting error code is ERROR_INVALID_ADDRESS.
    /// An attempt to commit a page that is already committed does not cause the function to fail. 
    /// This means that you can commit pages without first determining the current commitment state of each page.
    /// </summary>
    | Commit = 0x00001000 
    /// <summary>
    /// Reserves a range of the process's virtual address space without allocating any actual physical storage in memory or in the paging file on disk.
    /// You commit reserved pages by calling <see cref="NativeMethods.VirtualAllocEx"/> again with MEM_COMMIT. 
    /// To reserve and commit pages in one step, call VirtualAllocEx with MEM_COMMIT | MEM_RESERVE.
    /// Other memory allocation functions, such as malloc and LocalAlloc, cannot use reserved memory until it has been released.
    /// </summary>
    | Reserve = 0x00002000
    /// <summary>
    /// Indicates that data in the memory range specified by lpAddress and dwSize is no longer of interest. 
    /// The pages should not be read from or written to the paging file.
    ///  However, the memory block will be used again later, so it should not be decommitted. This value cannot be used with any other value.
    /// Using this value does not guarantee that the range operated on with MEM_RESET will contain zeros. If you want the range to contain zeros, decommit the memory and then recommit it.
    /// When you use MEM_RESET, the VirtualAllocEx function ignores the value of fProtect. However, you must still set fProtect to a valid protection value, such as PAGE_NOACCESS.
    /// <see cref="NativeMethods.VirtualAllocEx"/> returns an error if you use MEM_RESET and the range of memory is mapped to a file. 
    /// A shared view is only acceptable if it is mapped to a paging file.
    /// </summary>
    | Reset = 0x00080000
    /// <summary>
    /// MEM_RESET_UNDO should only be called on an address range to which MEM_RESET was successfully applied earlier. 
    /// It indicates that the data in the specified memory range specified by lpAddress and dwSize is of interest to the caller and attempts to reverse the effects of MEM_RESET. 
    /// If the function succeeds, that means all data in the specified address range is intact. 
    /// If the function fails, at least some of the data in the address range has been replaced with zeroes.
    /// This value cannot be used with any other value. 
    /// If MEM_RESET_UNDO is called on an address range which was not MEM_RESET earlier, the behavior is undefined. 
    /// When you specify MEM_RESET, the <see cref="NativeMethods.VirtualAllocEx"/> function ignores the value of flProtect. 
    /// However, you must still set flProtect to a valid protection value, such as PAGE_NOACCESS.
    /// </summary>
    | ResetUndo = 0x1000000
    /// <summary>
    /// Allocates memory using large page support.
    /// The size and alignment must be a multiple of the large-page minimum. To obtain this value, use the GetLargePageMinimum function.
    /// </summary>
    | LargePages = 0x20000000
    /// <summary>
    /// Reserves an address range that can be used to map Address Windowing Extensions (AWE) pages.
    /// This value must be used with MEM_RESERVE and no other values.
    /// </summary>
    | Physical = 0x00400000
    /// <summary>
    /// Allocates memory at the highest possible address. This can be slower than regular allocations, especially when there are many allocations.
    /// </summary>
    | TopDown = 0x00100000

/// <summary>
/// Memory-protection options list.
/// </summary>
[<Flags>]
type MemoryProtectionFlags =
    /// <summary>
    /// Disables all access to the committed region of pages. An attempt to read from, write to, or execute the committed region results in an access violation.
    /// This value is not officially present in the Microsoft's enumeration but can occur according to the MEMORY_BASIC_INFORMATION structure documentation.
    /// </summary>
    | ZeroAccess = 0x0
    /// <summary>
    /// Enables execute access to the committed region of pages. An attempt to read from or write to the committed region results in an access violation.
    /// This flag is not supported by the CreateFileMapping function.
    /// </summary>
    | Execute = 0x10
    /// <summary>
    /// Enables execute or read-only access to the committed region of pages. An attempt to write to the committed region results in an access violation.
    /// </summary>
    | ExecuteRead = 0x20
    /// <summary>
    /// Enables execute, read-only, or read/write access to the committed region of pages.
    /// </summary>
    | ExecuteReadWrite = 0x40
    /// <summary>
    /// Enables execute, read-only, or copy-on-write access to a mapped view of a file mapping object. 
    /// An attempt to write to a committed copy-on-write page results in a private copy of the page being made for the process. 
    /// The private page is marked as PAGE_EXECUTE_READWRITE, and the change is written to the new page.
    /// This flag is not supported by the VirtualAlloc or <see cref="NativeMethods.VirtualAllocEx"/> functions. 
    /// </summary>
    | ExecuteWriteCopy = 0x80
    /// <summary>
    /// Disables all access to the committed region of pages. An attempt to read from, write to, or execute the committed region results in an access violation.
    /// This flag is not supported by the CreateFileMapping function.
    /// </summary>
    | NoAccess = 0x01
    /// <summary>
    /// Enables read-only access to the committed region of pages. An attempt to write to the committed region results in an access violation. 
    /// If Data Execution Prevention is enabled, an attempt to execute code in the committed region results in an access violation.
    /// </summary>
    | ReadOnly = 0x02
    /// <summary>
    /// Enables read-only or read/write access to the committed region of pages. 
    /// If Data Execution Prevention is enabled, attempting to execute code in the committed region results in an access violation.
    /// </summary>
    | ReadWrite = 0x04
    /// <summary>
    /// Enables read-only or copy-on-write access to a mapped view of a file mapping object. 
    /// An attempt to write to a committed copy-on-write page results in a private copy of the page being made for the process. 
    /// The private page is marked as PAGE_READWRITE, and the change is written to the new page. 
    /// If Data Execution Prevention is enabled, attempting to execute code in the committed region results in an access violation.
    /// This flag is not supported by the VirtualAlloc or <see cref="NativeMethods.VirtualAllocEx"/> functions.
    /// </summary>
    | WriteCopy = 0x08
    /// <summary>
    /// Pages in the region become guard pages. 
    /// Any attempt to access a guard page causes the system to raise a STATUS_GUARD_PAGE_VIOLATION exception and turn off the guard page status. 
    /// Guard pages thus act as a one-time access alarm. For more information, see Creating Guard Pages.
    /// When an access attempt leads the system to turn off guard page status, the underlying page protection takes over.
    /// If a guard page exception occurs during a system service, the service typically returns a failure status indicator.
    /// This value cannot be used with PAGE_NOACCESS.
    /// This flag is not supported by the CreateFileMapping function.
    /// </summary>
    | Guard = 0x100
    /// <summary>
    /// Sets all pages to be non-cachable. Applications should not use this attribute except when explicitly required for a device. 
    /// Using the interlocked functions with memory that is mapped with SEC_NOCACHE can result in an EXCEPTION_ILLEGAL_INSTRUCTION exception.
    /// The PAGE_NOCACHE flag cannot be used with the PAGE_GUARD, PAGE_NOACCESS, or PAGE_WRITECOMBINE flags.
    /// The PAGE_NOCACHE flag can be used only when allocating private memory with the VirtualAlloc, <see cref="NativeMethods.VirtualAllocEx"/>, or VirtualAllocExNuma functions. 
    /// To enable non-cached memory access for shared memory, specify the SEC_NOCACHE flag when calling the CreateFileMapping function.
    /// </summary>
    | NoCache = 0x200
    /// <summary>
    /// Sets all pages to be write-combined.
    /// Applications should not use this attribute except when explicitly required for a device. 
    /// Using the interlocked functions with memory that is mapped as write-combined can result in an EXCEPTION_ILLEGAL_INSTRUCTION exception.
    /// The PAGE_WRITECOMBINE flag cannot be specified with the PAGE_NOACCESS, PAGE_GUARD, and PAGE_NOCACHE flags.
    /// The PAGE_WRITECOMBINE flag can be used only when allocating private memory with the VirtualAlloc, <see cref="NativeMethods.VirtualAllocEx"/>, or VirtualAllocExNuma functions. 
    /// To enable write-combined memory access for shared memory, specify the SEC_WRITECOMBINE flag when calling the CreateFileMapping function.
    /// </summary>
    | WriteCombine = 0x400

/// <summary>
/// Memory-release options list.
/// </summary>
[<Flags>]
type MemoryReleaseFlags =
    /// <summary>
    /// Decommits the specified region of committed pages. After the operation, the pages are in the reserved state.
    /// The function does not fail if you attempt to decommit an uncommitted page. 
    /// This means that you can decommit a range of pages without first determining their current commitment state.
    /// Do not use this value with MEM_RELEASE.
    /// </summary>
    | Decommit = 0x4000
    /// <summary>
    /// Releases the specified region of pages. After the operation, the pages are in the free state.
    /// If you specify this value, dwSize must be 0 (zero), and lpAddress must point to the base address returned by the VirtualAllocEx function when the region is reserved. 
    /// The function fails if either of these conditions is not met.
    /// If any pages in the region are committed currently, the function first decommits, and then releases them.
    /// The function does not fail if you attempt to release pages that are in different states, some reserved and some committed. 
    /// This means that you can release a range of pages without first determining the current commitment state.
    /// Do not use this value with MEM_DECOMMIT.
    /// </summary>
    | Release = 0x8000

/// <summary>
/// Memory-state options list.
/// </summary>
[<Flags>]
type MemoryStateFlags =
    /// <summary>
    /// Indicates committed pages for which physical storage has been allocated, either in memory or in the paging file on disk.
    /// </summary>
    | Commit = 0x1000
    /// <summary>
    /// Indicates free pages not accessible to the calling process and available to be allocated. 
    /// For free pages, the information in the AllocationBase, AllocationProtect, Protect, and Type members is undefined.
    /// </summary>
    | Free = 0x10000
    /// <summary>
    /// Indicates reserved pages where a range of the process's virtual address space is reserved without any physical storage being allocated. 
    /// For reserved pages, the information in the Protect member is undefined.
    /// </summary>
    | Reserve = 0x2000

/// <summary>
/// Memory-type options list.
/// </summary>
[<Flags>]
type MemoryTypeFlags =
    /// <summary>
    /// Non-official flag that is present when the page does not fall into another category
    /// </summary>
    | None = 0x0
    /// <summary>
    /// Indicates that the memory pages within the region are mapped into the view of an image section.
    /// </summary>
    | Image = 0x1000000
    /// <summary>
    /// Indicates that the memory pages within the region are mapped into the view of a section.
    /// </summary>
    | Mapped = 0x40000
    /// <summary>
    /// Indicates that the memory pages within the region are private (that is, not shared by other processes).
    /// </summary>
    | Private = 0x20000

/// <summary>
/// Process access rights list.
/// </summary>
[<Flags>]
type ProcessAccessFlags =
    /// <summary>
    /// All possible access rights for a process object.
    /// </summary>
    | AllAccess = 0x001F0FFF
    /// <summary>
    /// Required to create a process.
    /// </summary>
    | CreateProcess = 0x0080
    /// <summary>
    /// Required to create a thread.
    /// </summary>
    | CreateThread = 0x0002
    /// <summary>
    /// Required to duplicate a handle using DuplicateHandle.
    /// </summary>
    | DupHandle = 0x0040
    /// <summary>
    /// Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).
    /// </summary>
    | QueryInformation = 0x0400
    /// <summary>
    /// Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName). 
    /// A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION.
    /// </summary>
    | QueryLimitedInformation = 0x1000
    /// <summary>
    /// Required to set certain information about a process, such as its priority class (see SetPriorityClass).
    /// </summary>
    | SetInformation = 0x0200
    /// <summary>
    /// Required to set memory limits using SetProcessWorkingSetSize.
    /// </summary>
    | SetQuota = 0x0100
    /// <summary>
    /// Required to suspend or resume a process.
    /// </summary>
    | SuspendResume = 0x0800
    /// <summary>
    /// Required to terminate a process using TerminateProcess.
    /// </summary>
    | Terminate = 0x0001
    /// <summary>
    /// Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory).
    /// </summary>
    | VmOperation = 0x0008
    /// <summary>
    /// Required to read memory in a process using <see cref="NativeMethods.ReadProcessMemory"/>.
    /// </summary>
    | VmRead = 0x0010
    /// <summary>
    /// Required to write to memory in a process using WriteProcessMemory.
    /// </summary>
    | VmWrite = 0x0020
    /// <summary>
    /// Required to wait for the process to terminate using the wait functions.
    /// </summary>
    | Synchronize = 0x00100000
