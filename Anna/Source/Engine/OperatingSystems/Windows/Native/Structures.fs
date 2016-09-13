namespace Anna.Source.Engine.OperatingSystems.Windows.Structures

open System
open System.Runtime.InteropServices

open Anna.Source.Engine.OperatingSystems.Windows.Enumerations

// Disable warning for <StructLayout> complaining about unverifiable IL code
#nowarn "9"

/// <summary>
/// Contains information about a range of pages in the virtual address space of a process. The VirtualQuery and <see cref="NativeMethods.VirtualQueryEx"/> functions use this structure.
/// </summary>
[<StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi)>]
type MemoryBasicInformation32 =
    /// <summary>
    /// A pointer to the base address of the region of pages.
    /// </summary>
    val mutable BaseAddress: IntPtr
    /// <summary>
    /// A pointer to the base address of a range of pages allocated by the VirtualAlloc function. The page pointed to by the BaseAddress member is contained within this allocation range.
    /// </summary>
    val mutable AllocationBase: IntPtr
    /// <summary>
    /// The memory protection option when the region was initially allocated. This member can be one of the memory protection constants or 0 if the caller does not have access.
    /// </summary>
    val mutable AllocationProtect: MemoryProtectionFlags
    /// <summary>
    /// The size of the region beginning at the base address in which all pages have identical attributes, in bytes.
    /// </summary>
    val mutable RegionSize: Int32
    /// <summary>
    /// The state of the pages in the region.
    /// </summary>
    val mutable State: MemoryStateFlags
    /// <summary>
    /// The access protection of the pages in the region. This member is one of the values listed for the AllocationProtect member.
    /// </summary>
    val mutable Protect: MemoryProtectionFlags
    /// <summary>
    /// The type of pages in the region.
    /// </summary>
    val mutable Type: MemoryTypeFlags

[<StructLayout(LayoutKind.Sequential)>]
type MemoryBasicInformation64 =
    /// <summary>
    /// A pointer to the base address of the region of pages.
    /// </summary>
    val mutable BaseAddress: IntPtr
    /// <summary>
    /// A pointer to the base address of a range of pages allocated by the VirtualAlloc function. The page pointed to by the BaseAddress member is contained within this allocation range.
    /// </summary>
    val mutable AllocationBase: IntPtr
    /// <summary>
    /// The memory protection option when the region was initially allocated. This member can be one of the memory protection constants or 0 if the caller does not have access.
    /// </summary>
    val mutable AllocationProtect: MemoryProtectionFlags
    /// <summary>
    /// Required in the 64 bit struct. Blame Windows.
    /// </summary>
    val mutable Alignment1: UInt32
    /// <summary>
    /// The size of the region beginning at the base address in which all pages have identical attributes, in bytes.
    /// </summary>
    val mutable RegionSize: Int64
    /// <summary>
    /// The state of the pages in the region.
    /// </summary>
    val mutable State: MemoryStateFlags
    /// <summary>
    /// The access protection of the pages in the region. This member is one of the values listed for the AllocationProtect member.
    /// </summary>
    val mutable Protect: MemoryProtectionFlags
    /// <summary>
    /// The type of pages in the region.
    /// </summary>
    val mutable Type: MemoryTypeFlags
    /// <summary>
    /// Required in the 64 bit struct. Blame Windows.
    /// </summary>
    val mutable Alignment2: UInt32