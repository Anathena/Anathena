/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Runtime.InteropServices;

namespace Binarysharp.MemoryManagement.Native
{
    #region FlashWindowFlags
    /// <summary>
    /// Flash window flags list.
    /// </summary>
    [Flags]
    public enum FlashWindowFlags
    {
        /// <summary>
        /// Flash both the window caption and taskbar button. This is equivalent to setting the <see cref="Caption"/> | <see cref="Tray"/> flags.
        /// </summary>
        All = 0x3,
        /// <summary>
        /// Flash the window caption.
        /// </summary>
        Caption = 0x1,
        /// <summary>
        /// Stop flashing. The system restores the window to its original state.
        /// </summary>
        Stop = 0x0,
        /// <summary>
        /// Flash continuously, until the <see cref="Stop"/> flag is set.
        /// </summary>
        Timer = 0x4,
        /// <summary>
        /// Flash continuously until the window comes to the foreground.
        /// </summary>
        TimerNoForeground = 0x0C,
        /// <summary>
        /// Flash the taskbar button.
        /// </summary>
        Tray = 0x2
    }
    #endregion

    #region InputTypes
    /// <summary>
    /// The types used in the function <see cref="NativeMethods.SendInput"/> for input events.
    /// </summary>
    public enum InputTypes
    {
        Mouse = 0,
        Keyboard = 1,
        Hardware = 2
    }
    #endregion

    #region KeyboardFlags
    /// <summary>
    /// The keyboard flags list.
    /// </summary>
    [Flags]
    public enum KeyboardFlags
    {
        /// <summary>
        /// If specified, the scan code was preceded by a prefix byte that has the value 0xE0 (224).
        /// </summary>
        ExtendedKey = 1,
        /// <summary>
        /// If specified, the key is being released. If not specified, the key is being pressed.
        /// </summary>
        KeyUp = 2,
        /// <summary>
        /// If specified, <see cref="KeyboardInput.ScanCode"/> identifies the key and <see cref="KeyboardInput.VirtualKey"/> is ignored. 
        /// </summary>
        ScanCode = 8,
        /// <summary>
        /// If specified, the system synthesizes a VK_PACKET keystroke. The <see cref="KeyboardInput.VirtualKey"/> parameter must be zero. 
        /// This flag can only be combined with the KEYEVENTF_KEYUP flag.
        /// </summary>
        Unicode = 4
    }
    #endregion

    #region Keys
    /// <summary>
    /// The key codes and modifiers list.
    /// </summary>
    [Flags]
    public enum Keys
    {
        A = 0x41,
        Add = 0x6b,
        Alt = 0x40000,
        Apps = 0x5d,
        Attn = 0xf6,
        B = 0x42,
        Back = 8,
        BrowserBack = 0xa6,
        BrowserFavorites = 0xab,
        BrowserForward = 0xa7,
        BrowserHome = 0xac,
        BrowserRefresh = 0xa8,
        BrowserSearch = 170,
        BrowserStop = 0xa9,
        C = 0x43,
        Cancel = 3,
        Capital = 20,
        CapsLock = 20,
        Clear = 12,
        Control = 0x20000,
        ControlKey = 0x11,
        Crsel = 0xf7,
        D = 0x44,
        D0 = 0x30,
        D1 = 0x31,
        D2 = 50,
        D3 = 0x33,
        D4 = 0x34,
        D5 = 0x35,
        D6 = 0x36,
        D7 = 0x37,
        D8 = 0x38,
        D9 = 0x39,
        Decimal = 110,
        Delete = 0x2e,
        Divide = 0x6f,
        Down = 40,
        E = 0x45,
        End = 0x23,
        Enter = 13,
        EraseEof = 0xf9,
        Escape = 0x1b,
        Execute = 0x2b,
        Exsel = 0xf8,
        F = 70,
        F1 = 0x70,
        F10 = 0x79,
        F11 = 0x7a,
        F12 = 0x7b,
        F13 = 0x7c,
        F14 = 0x7d,
        F15 = 0x7e,
        F16 = 0x7f,
        F17 = 0x80,
        F18 = 0x81,
        F19 = 130,
        F2 = 0x71,
        F20 = 0x83,
        F21 = 0x84,
        F22 = 0x85,
        F23 = 0x86,
        F24 = 0x87,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 120,
        FinalMode = 0x18,
        G = 0x47,
        H = 0x48,
        HanguelMode = 0x15,
        HangulMode = 0x15,
        HanjaMode = 0x19,
        Help = 0x2f,
        Home = 0x24,
        I = 0x49,
        ImeAccept = 30,
        ImeAceept = 30,
        ImeConvert = 0x1c,
        ImeModeChange = 0x1f,
        ImeNonconvert = 0x1d,
        Insert = 0x2d,
        J = 0x4a,
        JunjaMode = 0x17,
        K = 0x4b,
        KanaMode = 0x15,
        KanjiMode = 0x19,
        KeyCode = 0xffff,
        L = 0x4c,
        LaunchApplication1 = 0xb6,
        LaunchApplication2 = 0xb7,
        LaunchMail = 180,
        LButton = 1,
        LControlKey = 0xa2,
        Left = 0x25,
        LineFeed = 10,
        LMenu = 0xa4,
        LShiftKey = 160,
        LWin = 0x5b,
        M = 0x4d,
        MButton = 4,
        MediaNextTrack = 0xb0,
        MediaPlayPause = 0xb3,
        MediaPreviousTrack = 0xb1,
        MediaStop = 0xb2,
        Menu = 0x12,
        Modifiers = -65536,
        Multiply = 0x6a,
        N = 0x4e,
        Next = 0x22,
        NoName = 0xfc,
        None = 0,
        NumLock = 0x90,
        NumPad0 = 0x60,
        NumPad1 = 0x61,
        NumPad2 = 0x62,
        NumPad3 = 0x63,
        NumPad4 = 100,
        NumPad5 = 0x65,
        NumPad6 = 0x66,
        NumPad7 = 0x67,
        NumPad8 = 0x68,
        NumPad9 = 0x69,
        O = 0x4f,
        Oem1 = 0xba,
        Oem102 = 0xe2,
        Oem2 = 0xbf,
        Oem3 = 0xc0,
        Oem4 = 0xdb,
        Oem5 = 220,
        Oem6 = 0xdd,
        Oem7 = 0xde,
        Oem8 = 0xdf,
        OemBackslash = 0xe2,
        OemClear = 0xfe,
        OemCloseBrackets = 0xdd,
        Oemcomma = 0xbc,
        OemMinus = 0xbd,
        OemOpenBrackets = 0xdb,
        OemPeriod = 190,
        OemPipe = 220,
        Oemplus = 0xbb,
        OemQuestion = 0xbf,
        OemQuotes = 0xde,
        OemSemicolon = 0xba,
        Oemtilde = 0xc0,
        P = 80,
        Pa1 = 0xfd,
        Packet = 0xe7,
        PageDown = 0x22,
        PageUp = 0x21,
        Pause = 0x13,
        Play = 250,
        Print = 0x2a,
        PrintScreen = 0x2c,
        Prior = 0x21,
        ProcessKey = 0xe5,
        Q = 0x51,
        R = 0x52,
        RButton = 2,
        RControlKey = 0xa3,
        Return = 13,
        Right = 0x27,
        RMenu = 0xa5,
        RShiftKey = 0xa1,
        RWin = 0x5c,
        S = 0x53,
        Scroll = 0x91,
        Select = 0x29,
        SelectMedia = 0xb5,
        Separator = 0x6c,
        Shift = 0x10000,
        ShiftKey = 0x10,
        Sleep = 0x5f,
        Snapshot = 0x2c,
        Space = 0x20,
        Subtract = 0x6d,
        T = 0x54,
        Tab = 9,
        U = 0x55,
        Up = 0x26,
        V = 0x56,
        VolumeDown = 0xae,
        VolumeMute = 0xad,
        VolumeUp = 0xaf,
        W = 0x57,
        X = 0x58,
        XButton1 = 5,
        XButton2 = 6,
        Y = 0x59,
        Z = 90,
        Zoom = 0xfb
    }
    #endregion

    #region MemoryAllocationFlags
    /// <summary>
    /// Memory-allocation options list.
    /// </summary>
    [Flags]
    public enum MemoryAllocationFlags
    {
        /// <summary>
        /// Allocates memory charges (from the overall size of memory and the paging files on disk) for the specified reserved memory pages. 
        /// The function also guarantees that when the caller later initially accesses the memory, the contents will be zero. 
        /// Actual physical pages are not allocated unless/until the virtual addresses are actually accessed.
        /// To reserve and commit pages in one step, call <see cref="NativeMethods.VirtualAllocEx"/> with MEM_COMMIT | MEM_RESERVE.
        /// The function fails if you attempt to commit a page that has not been reserved. The resulting error code is ERROR_INVALID_ADDRESS.
        /// An attempt to commit a page that is already committed does not cause the function to fail. 
        /// This means that you can commit pages without first determining the current commitment state of each page.
        /// </summary>
        Commit = 0x00001000,
        /// <summary>
        /// Reserves a range of the process's virtual address space without allocating any actual physical storage in memory or in the paging file on disk.
        /// You commit reserved pages by calling <see cref="NativeMethods.VirtualAllocEx"/> again with MEM_COMMIT. 
        /// To reserve and commit pages in one step, call VirtualAllocEx with MEM_COMMIT | MEM_RESERVE.
        /// Other memory allocation functions, such as malloc and LocalAlloc, cannot use reserved memory until it has been released.
        /// </summary>
        Reserve = 0x00002000,
        /// <summary>
        /// Indicates that data in the memory range specified by lpAddress and dwSize is no longer of interest. 
        /// The pages should not be read from or written to the paging file.
        ///  However, the memory block will be used again later, so it should not be decommitted. This value cannot be used with any other value.
        /// Using this value does not guarantee that the range operated on with MEM_RESET will contain zeros. If you want the range to contain zeros, decommit the memory and then recommit it.
        /// When you use MEM_RESET, the VirtualAllocEx function ignores the value of fProtect. However, you must still set fProtect to a valid protection value, such as PAGE_NOACCESS.
        /// <see cref="NativeMethods.VirtualAllocEx"/> returns an error if you use MEM_RESET and the range of memory is mapped to a file. 
        /// A shared view is only acceptable if it is mapped to a paging file.
        /// </summary>
        Reset = 0x00080000,
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
        ResetUndo = 0x1000000,
        /// <summary>
        /// Allocates memory using large page support.
        /// The size and alignment must be a multiple of the large-page minimum. To obtain this value, use the GetLargePageMinimum function.
        /// </summary>
        LargePages = 0x20000000,
        /// <summary>
        /// Reserves an address range that can be used to map Address Windowing Extensions (AWE) pages.
        /// This value must be used with MEM_RESERVE and no other values.
        /// </summary>
        Physical = 0x00400000,
        /// <summary>
        /// Allocates memory at the highest possible address. This can be slower than regular allocations, especially when there are many allocations.
        /// </summary>
        TopDown = 0x00100000
    }
    #endregion

    #region MemoryProtectionFlags
    /// <summary>
    /// Memory-protection options list.
    /// </summary>
    [Flags]
    public enum MemoryProtectionFlags
    {
        /// <summary>
        /// Disables all access to the committed region of pages. An attempt to read from, write to, or execute the committed region results in an access violation.
        /// This value is not officially present in the Microsoft's enumeration but can occur according to the MEMORY_BASIC_INFORMATION structure documentation.
        /// </summary>
        ZeroAccess = 0x0,
        /// <summary>
        /// Enables execute access to the committed region of pages. An attempt to read from or write to the committed region results in an access violation.
        /// This flag is not supported by the CreateFileMapping function.
        /// </summary>
        Execute = 0x10,
        /// <summary>
        /// Enables execute or read-only access to the committed region of pages. An attempt to write to the committed region results in an access violation.
        /// </summary>
        ExecuteRead = 0x20,
        /// <summary>
        /// Enables execute, read-only, or read/write access to the committed region of pages.
        /// </summary>
        ExecuteReadWrite = 0x40,
        /// <summary>
        /// Enables execute, read-only, or copy-on-write access to a mapped view of a file mapping object. 
        /// An attempt to write to a committed copy-on-write page results in a private copy of the page being made for the process. 
        /// The private page is marked as PAGE_EXECUTE_READWRITE, and the change is written to the new page.
        /// This flag is not supported by the VirtualAlloc or <see cref="NativeMethods.VirtualAllocEx"/> functions. 
        /// </summary>
        ExecuteWriteCopy = 0x80,
        /// <summary>
        /// Disables all access to the committed region of pages. An attempt to read from, write to, or execute the committed region results in an access violation.
        /// This flag is not supported by the CreateFileMapping function.
        /// </summary>
        NoAccess = 0x01,
        /// <summary>
        /// Enables read-only access to the committed region of pages. An attempt to write to the committed region results in an access violation. 
        /// If Data Execution Prevention is enabled, an attempt to execute code in the committed region results in an access violation.
        /// </summary>
        ReadOnly = 0x02,
        /// <summary>
        /// Enables read-only or read/write access to the committed region of pages. 
        /// If Data Execution Prevention is enabled, attempting to execute code in the committed region results in an access violation.
        /// </summary>
        ReadWrite = 0x04,
        /// <summary>
        /// Enables read-only or copy-on-write access to a mapped view of a file mapping object. 
        /// An attempt to write to a committed copy-on-write page results in a private copy of the page being made for the process. 
        /// The private page is marked as PAGE_READWRITE, and the change is written to the new page. 
        /// If Data Execution Prevention is enabled, attempting to execute code in the committed region results in an access violation.
        /// This flag is not supported by the VirtualAlloc or <see cref="NativeMethods.VirtualAllocEx"/> functions.
        /// </summary>
        WriteCopy = 0x08,
        /// <summary>
        /// Pages in the region become guard pages. 
        /// Any attempt to access a guard page causes the system to raise a STATUS_GUARD_PAGE_VIOLATION exception and turn off the guard page status. 
        /// Guard pages thus act as a one-time access alarm. For more information, see Creating Guard Pages.
        /// When an access attempt leads the system to turn off guard page status, the underlying page protection takes over.
        /// If a guard page exception occurs during a system service, the service typically returns a failure status indicator.
        /// This value cannot be used with PAGE_NOACCESS.
        /// This flag is not supported by the CreateFileMapping function.
        /// </summary>
        Guard = 0x100,
        /// <summary>
        /// Sets all pages to be non-cachable. Applications should not use this attribute except when explicitly required for a device. 
        /// Using the interlocked functions with memory that is mapped with SEC_NOCACHE can result in an EXCEPTION_ILLEGAL_INSTRUCTION exception.
        /// The PAGE_NOCACHE flag cannot be used with the PAGE_GUARD, PAGE_NOACCESS, or PAGE_WRITECOMBINE flags.
        /// The PAGE_NOCACHE flag can be used only when allocating private memory with the VirtualAlloc, <see cref="NativeMethods.VirtualAllocEx"/>, or VirtualAllocExNuma functions. 
        /// To enable non-cached memory access for shared memory, specify the SEC_NOCACHE flag when calling the CreateFileMapping function.
        /// </summary>
        NoCache = 0x200,
        /// <summary>
        /// Sets all pages to be write-combined.
        /// Applications should not use this attribute except when explicitly required for a device. 
        /// Using the interlocked functions with memory that is mapped as write-combined can result in an EXCEPTION_ILLEGAL_INSTRUCTION exception.
        /// The PAGE_WRITECOMBINE flag cannot be specified with the PAGE_NOACCESS, PAGE_GUARD, and PAGE_NOCACHE flags.
        /// The PAGE_WRITECOMBINE flag can be used only when allocating private memory with the VirtualAlloc, <see cref="NativeMethods.VirtualAllocEx"/>, or VirtualAllocExNuma functions. 
        /// To enable write-combined memory access for shared memory, specify the SEC_WRITECOMBINE flag when calling the CreateFileMapping function.
        /// </summary>
        WriteCombine = 0x400
    }
    #endregion

    #region MemoryReleaseFlags
    /// <summary>
    /// Memory-release options list.
    /// </summary>
    [Flags]
    public enum MemoryReleaseFlags
    {
        /// <summary>
        /// Decommits the specified region of committed pages. After the operation, the pages are in the reserved state.
        /// The function does not fail if you attempt to decommit an uncommitted page. 
        /// This means that you can decommit a range of pages without first determining their current commitment state.
        /// Do not use this value with MEM_RELEASE.
        /// </summary>
        Decommit = 0x4000,
        /// <summary>
        /// Releases the specified region of pages. After the operation, the pages are in the free state.
        /// If you specify this value, dwSize must be 0 (zero), and lpAddress must point to the base address returned by the VirtualAllocEx function when the region is reserved. 
        /// The function fails if either of these conditions is not met.
        /// If any pages in the region are committed currently, the function first decommits, and then releases them.
        /// The function does not fail if you attempt to release pages that are in different states, some reserved and some committed. 
        /// This means that you can release a range of pages without first determining the current commitment state.
        /// Do not use this value with MEM_DECOMMIT.
        /// </summary>
        Release = 0x8000
    }
    #endregion

    #region MemoryStateFlags
    /// <summary>
    /// Memory-state options list.
    /// </summary>
    [Flags]
    public enum MemoryStateFlags
    {
        /// <summary>
        /// Indicates committed pages for which physical storage has been allocated, either in memory or in the paging file on disk.
        /// </summary>
        Commit = 0x1000,
        /// <summary>
        /// Indicates free pages not accessible to the calling process and available to be allocated. 
        /// For free pages, the information in the AllocationBase, AllocationProtect, Protect, and Type members is undefined.
        /// </summary>
        Free = 0x10000,
        /// <summary>
        /// Indicates reserved pages where a range of the process's virtual address space is reserved without any physical storage being allocated. 
        /// For reserved pages, the information in the Protect member is undefined.
        /// </summary>
        Reserve = 0x2000
    }
    #endregion

    #region MemoryTypeFlags
    /// <summary>
    /// Memory-type options list.
    /// </summary>
    [Flags]
    public enum MemoryTypeFlags
    {
        /// <summary>
        /// This value is not officially present in the Microsoft's enumeration but can occur after testing.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Indicates that the memory pages within the region are mapped into the view of an image section.
        /// </summary>
        Image = 0x1000000,
        /// <summary>
        /// Indicates that the memory pages within the region are mapped into the view of a section.
        /// </summary>
        Mapped = 0x40000,
        /// <summary>
        /// Indicates that the memory pages within the region are private (that is, not shared by other processes).
        /// </summary>
        Private = 0x20000
    }
    #endregion

    #region MouseFlags
    /// <summary>
    /// The mouse flags list.
    /// </summary>
    [Flags]
    public enum MouseFlags
    {
        /// <summary>
        /// The DeltaX and DeltaY members contain normalized absolute coordinates. If the flag is not set, DeltaX and DeltaY contain relative data 
        /// (the change in position since the last reported position). This flag can be set, or not set, regardless of what kind of mouse or other
        /// pointing device, if any, is connected to the system.
        /// </summary>
        Absolute = 0x8000,
        /// <summary>
        /// The wheel was moved horizontally, if the mouse has a wheel. The amount of movement is specified in MouseData.
        /// Windows XP/2000:  This value is not supported.
        /// </summary>
        HWheel = 0x1000,
        /// <summary>
        /// Movement occurred.
        /// </summary>
        Move = 1,
        /// <summary>
        /// The WM_MOUSEMOVE messages will not be coalesced. The default behavior is to coalesce WM_MOUSEMOVE messages.
        /// Windows XP/2000:  This value is not supported.
        /// </summary>
        MoveNoCoalesce = 0x2000,
        /// <summary>
        /// The left button was pressed.
        /// </summary>
        LeftDown = 2,
        /// <summary>
        /// The left button was released.
        /// </summary>
        LeftUp = 4,
        /// <summary>
        /// The right button was pressed.
        /// </summary>
        RightDown = 8,
        /// <summary>
        /// The right button was released.
        /// </summary>
        RightUp = 0x10,
        /// <summary>
        /// The middle button was pressed.
        /// </summary>
        MiddleDown = 0x20,
        /// <summary>
        /// The middle button was released.
        /// </summary>
        MiddleUp = 0x40,
        /// <summary>
        /// Maps coordinates to the entire desktop. Must be used with <see cref="Absolute"/>.
        /// </summary>
        VirtualDesk = 0x4000,
        /// <summary>
        /// The wheel was moved, if the mouse has a wheel. The amount of movement is specified in MouseData. 
        /// </summary>
        Wheel = 0x800,
        /// <summary>
        /// An X button was pressed.
        /// </summary>
        XDown = 0x80,
        /// <summary>
        /// An X button was released.
        /// </summary>
        XUp = 0x100
    }
    #endregion

    #region PebStructure
    /// <summary>
    /// The structure of the Process Environment Block.
    /// </summary>
    /// <remarks>
    /// Tested on Windows 7 x64, 2013-03-10
    /// Source: http://blog.rewolf.pl/blog/?p=573#.UTyBo1fJL6p
    /// </remarks>
    public enum PebStructure
    {
        InheritedAddressSpace = 0x0,
        ReadImageFileExecOptions = 0x1,
        /// <summary>
        /// Gets if the process is being debugged.
        /// </summary>
        BeingDebugged = 0x2,
        SpareBool = 0x3,
        Mutant = 0x4,
        ImageBaseAddress = 0x8,
        Ldr = 0xC,
        ProcessParameters = 0x10,
        SubSystemData = 0x14,
        ProcessHeap = 0x18,
        FastPebLock = 0x1C,
        FastPebLockRoutine = 0x20,
        FastPebUnlockRoutine = 0x24,
        EnvironmentUpdateCount = 0x28,
        KernelCallbackTable = 0x2C,
        SystemReserved = 0x30,
        AtlThunkSListPtr32 = 0x34,
        FreeList = 0x38,
        TlsExpansionCounter = 0x3C,
        TlsBitmap = 0x40,
        /// <summary>
        /// Length: 8 bytes.
        /// </summary>
        TlsBitmapBits = 0x44,
        ReadOnlySharedMemoryBase = 0x4C,
        ReadOnlySharedMemoryHeap = 0x50,
        ReadOnlyStaticServerData = 0x54,
        AnsiCodePageData = 0x58,
        OemCodePageData = 0x5C,
        UnicodeCaseTableData = 0x60,
        NumberOfProcessors = 0x64,
        /// <summary>
        /// Length: 8 bytes.
        /// </summary>
        NtGlobalFlag = 0x68,
        /// <summary>
        /// Length: 8 bytes (LARGE_INTEGER type).
        /// </summary>
        CriticalSectionTimeout = 0x70,
        HeapSegmentReserve = 0x78,
        HeapSegmentCommit = 0x7C,
        HeapDeCommitTotalFreeThreshold = 0x80,
        HeapDeCommitFreeBlockThreshold = 0x84,
        NumberOfHeaps = 0x88,
        MaximumNumberOfHeaps = 0x8C,
        ProcessHeaps = 0x90,
        GdiSharedHandleTable = 0x94,
        ProcessStarterHelper = 0x98,
        GdiDcAttributeList = 0x9C,
        LoaderLock = 0xA0,
        OsMajorVersion = 0xA4,
        OsMinorVersion = 0xA8,
        /// <summary>
        /// Length: 2 bytes.
        /// </summary>
        OsBuildNumber = 0xAC,
        /// <summary>
        /// Length: 2 bytes.
        /// </summary>
        OsCsdVersion = 0xAE,
        OsPlatformId = 0xB0,
        ImageSubsystem = 0xB4,
        ImageSubsystemMajorVersion = 0xB8,
        ImageSubsystemMinorVersion = 0xBC,
        ImageProcessAffinityMask = 0xC0,
        /// <summary>
        /// Length: 0x88 bytes (0x22 * sizeof(IntPtr)).
        /// </summary>
        GdiHandleBuffer = 0xC4,
        PostProcessInitRoutine = 0x14C,
        TlsExpansionBitmap = 0x150,
        /// <summary>
        /// Length: 0x80 bytes (0x20 * sizeof(IntPtr))
        /// </summary>
        TlsExpansionBitmapBits = 0x154,
        SessionId = 0x1D4,
        /// <summary>
        /// Length: 8 bytes (LARGE_INTEGER type).
        /// </summary>
        AppCompatFlags = 0x1D8,
        /// <summary>
        /// Length: 8 bytes (LARGE_INTEGER type).
        /// </summary>
        AppCompatFlagsUser = 0x1E0,
        ShimData = 0x1E8,
        AppCompatInfo = 0x1EC,
        /// <summary>
        /// Length: 8 bytes (UNICODE_STRING type).
        /// </summary>
        CsdVersion = 0x1F0,
        ActivationContextData = 0x1F8,
        ProcessAssemblyStorageMap = 0x1FC,
        SystemDefaultActivationContextData = 0x200,
        SystemAssemblyStorageMap = 0x204,
        MinimumStackCommit = 0x208
    }
    #endregion

    #region ProcessAccessFlags
    /// <summary>
    /// Process access rights list.
    /// </summary>
    [Flags]
    public enum ProcessAccessFlags
    {
        /// <summary>
        /// All possible access rights for a process object.
        /// </summary>
        AllAccess = 0x001F0FFF,
        /// <summary>
        /// Required to create a process.
        /// </summary>
        CreateProcess = 0x0080,
        /// <summary>
        /// Required to create a thread.
        /// </summary>
        CreateThread = 0x0002,
        /// <summary>
        /// Required to duplicate a handle using DuplicateHandle.
        /// </summary>
        DupHandle = 0x0040,
        /// <summary>
        /// Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).
        /// </summary>
        QueryInformation = 0x0400,
        /// <summary>
        /// Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName). 
        /// A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION.
        /// </summary>
        QueryLimitedInformation = 0x1000,
        /// <summary>
        /// Required to set certain information about a process, such as its priority class (see SetPriorityClass).
        /// </summary>
        SetInformation = 0x0200,
        /// <summary>
        /// Required to set memory limits using SetProcessWorkingSetSize.
        /// </summary>
        SetQuota = 0x0100,
        /// <summary>
        /// Required to suspend or resume a process.
        /// </summary>
        SuspendResume = 0x0800,
        /// <summary>
        /// Required to terminate a process using TerminateProcess.
        /// </summary>
        Terminate = 0x0001,
        /// <summary>
        /// Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory).
        /// </summary>
        VmOperation = 0x0008,
        /// <summary>
        /// Required to read memory in a process using <see cref="NativeMethods.ReadProcessMemory"/>.
        /// </summary>
        VmRead = 0x0010,
        /// <summary>
        /// Required to write to memory in a process using WriteProcessMemory.
        /// </summary>
        VmWrite = 0x0020,
        /// <summary>
        /// Required to wait for the process to terminate using the wait functions.
        /// </summary>
        Synchronize = 0x00100000
    }
    #endregion

    #region ProcessInformationClass
    /// <summary>
    /// The type of process information to be retrieved.
    /// </summary>
    public enum ProcessInformationClass
    {
        /// <summary>
        /// Retrieves a pointer to a PEB structure that can be used to determine whether the specified process is being debugged, 
        /// and a unique value used by the system to identify the specified process. 
        /// </summary>
        ProcessBasicInformation = 0x0,
        /// <summary>
        /// Retrieves a DWORD_PTR value that is the port number of the debugger for the process. 
        /// A nonzero value indicates that the process is being run under the control of a ring 3 debugger.
        /// </summary>
        ProcessDebugPort = 0x7,
        /// <summary>
        /// Determines whether the process is running in the WOW64 environment (WOW64 is the x86 emulator that allows Win32-based applications to run on 64-bit Windows).
        /// </summary>
        ProcessWow64Information = 0x1A,
        /// <summary>
        /// Retrieves a UNICODE_STRING value containing the name of the image file for the process.
        /// </summary>
        ProcessImageFileName = 0x1B
    }
    #endregion

    #region SystemMetrics
    /// <summary>
    /// The system metrics list used in the function <see cref="NativeMethods.GetSystemMetrics"/>.
    /// </summary>
    public enum SystemMetrics
    {
        CxScreen = 0,
        CyScreen = 1
    }
    #endregion

    #region TebStructure
    /// <summary>
    /// The structure of the Thread Environment Block.
    /// </summary>
    /// <remarks>Tested on Windows 7 x64, 2013-03-10.</remarks>
    public enum TebStructure
    {
        /// <summary>
        /// Current Structured Exception Handling (SEH) frame.
        /// </summary>
        CurrentSehFrame = 0x0,
        /// <summary>
        /// The top of stack.
        /// </summary>
        TopOfStack = 0x4,
        /// <summary>
        /// The current bottom of stack.
        /// </summary>
        BottomOfStack = 0x8,
        /// <summary>
        /// The TEB sub system.
        /// </summary>
        SubSystemTeb = 0xC,
        /// <summary>
        /// The fiber data.
        /// </summary>
        FiberData = 0x10,
        /// <summary>
        /// The arbitrary data slot.
        /// </summary>
        ArbitraryDataSlot = 0x14,
        /// <summary>
        /// The linear address of Thread Environment Block (TEB).
        /// </summary>
        Teb = 0x18,
        /// <summary>
        /// The environment pointer.
        /// </summary>
        EnvironmentPointer = 0x1C,
        /// <summary>
        /// The process Id.
        /// </summary>
        ProcessId = 0x20,
        /// <summary>
        /// The current thread Id.
        /// </summary>
        ThreadId = 0x24,
        /// <summary>
        /// The active RPC handle.
        /// </summary>
        RpcHandle = 0x28,
        /// <summary>
        /// The linear address of the thread-local storage (TLS) array.
        /// </summary>
        Tls = 0x2C,
        /// <summary>
        /// The linear address of Process Environment Block (PEB).
        /// </summary>
        Peb = 0x30,
        /// <summary>
        /// The last error number.
        /// </summary>
        LastErrorNumber = 0x34,
        /// <summary>
        /// The count of owned critical sections.
        /// </summary>
        CriticalSectionsCount = 0x38,
        /// <summary>
        /// The address of CSR Client Thread.
        /// </summary>
        CsrClientThread = 0x3C,
        /// <summary>
        /// Win32 Thread Information.
        /// </summary>
        Win32ThreadInfo = 0x40,
        /// <summary>
        /// Win32 client information (NT), user32 private data (Wine), 0x60 = LastError (Win95), 0x74 = LastError (WinME). (length: 124 bytes)
        /// </summary>
        Win32ClientInfo = 0x44,
        /// <summary>
        /// Reserved for Wow64. Contains a pointer to FastSysCall in Wow64.
        /// </summary>
        WoW64Reserved = 0xC0,
        /// <summary>
        /// The current locale.
        /// </summary>
        CurrentLocale = 0xC4,
        /// <summary>
        /// The FP Software Status Register.
        /// </summary>
        FpSoftwareStatusRegister = 0xC8,
        /// <summary>
        /// Reserved for OS (NT), kernel32 private data (Wine). (length: 216 bytes)
        /// herein: FS:[0x124] 4 NT Pointer to KTHREAD (ETHREAD) structure.
        /// </summary>
        SystemReserved1 = 0xCC,
        /// <summary>
        /// The exception code.
        /// </summary>
        ExceptionCode = 0x1A4,
        /// <summary>
        /// The activation context stack. (length: 18 bytes)
        /// </summary>
        ActivationContextStack = 0x1A8,
        /// <summary>
        /// The spare bytes (NT), ntdll private data (Wine). (length: 24 bytes)
        /// </summary>
        SpareBytes = 0x1BC,
        /// <summary>
        /// Reserved for OS (NT), ntdll private data (Wine). (length: 40 bytes)
        /// </summary>
        SystemReserved2 = 0x1D4,
        /// <summary>
        /// The GDI TEB Batch (OS), vm86 private data (Wine). (length: 1248 bytes)
        /// </summary>
        GdiTebBatch = 0x1FC,
        /// <summary>
        /// The GDI Region.
        /// </summary>
        GdiRegion = 0x6DC,
        /// <summary>
        /// The GDI Pen.
        /// </summary>
        GdiPen = 0x6E0,
        /// <summary>
        /// The GDI Brush.
        /// </summary>
        GdiBrush = 0x6E4,
        /// <summary>
        /// The real process Id.
        /// </summary>
        RealProcessId = 0x6E8,
        /// <summary>
        /// The real thread Id.
        /// </summary>
        RealThreadId = 0x6EC,
        /// <summary>
        /// The GDI cached process handle.
        /// </summary>
        GdiCachedProcessHandle = 0x6F0,
        /// <summary>
        /// The GDI client process Id (PID).
        /// </summary>
        GdiClientProcessId = 0x6F4,
        /// <summary>
        /// The GDI client thread Id (TID).
        /// </summary>
        GdiClientThreadId = 0x6F8,
        /// <summary>
        /// The GDI thread locale information.
        /// </summary>
        GdiThreadLocalInfo = 0x6FC,
        /// <summary>
        /// Reserved for user application. (length: 20 bytes)
        /// </summary>
        UserReserved1 = 0x700,
        /// <summary>
        /// Reserved for GL. (length: 1248 bytes)
        /// </summary>
        GlReserved1 = 0x714,
        /// <summary>
        /// The last value status value.
        /// </summary>
        LastStatusValue = 0xBF4,
        /// <summary>
        /// The static UNICODE_STRING buffer. (length: 532 bytes)
        /// </summary>
        StaticUnicodeString = 0xBF8,
        /// <summary>
        /// The pointer to deallocation stack.
        /// </summary>
        DeallocationStack = 0xE0C,
        /// <summary>
        /// The TLS slots, 4 byte per slot. (length: 256 bytes)
        /// </summary>
        TlsSlots = 0xE10,
        /// <summary>
        /// The TLS links (LIST_ENTRY structure). (length 8 bytes)
        /// </summary>
        TlsLinks = 0xF10,
        /// <summary>
        /// Virtual DOS Machine.
        /// </summary>
        Vdm = 0xF18,
        /// <summary>
        /// Reserved for RPC.
        /// </summary>
        RpcReserved = 0xF1C,
        /// <summary>
        /// The thread error mode (RtlSetThreadErrorMode).
        /// </summary>
        ThreadErrorMode = 0xF28
    }
    #endregion

    #region ThreadAccessFlags

    /// <summary>
    /// Thread access rights list.
    /// </summary>
    [Flags]
    public enum ThreadAccessFlags
    {
        /// <summary>
        /// Enables the use of the thread handle in any of the wait functions.
        /// </summary>
        Synchronize = 0x00100000,
        /// <summary>
        /// All possible access rights for a thread object.
        /// </summary>
        AllAccess = 0x001F0FFF,
        /// <summary>
        /// Required for a server thread that impersonates a client.
        /// </summary>
        DirectImpersonation = 0x0200,
        /// <summary>
        /// Required to read the context of a thread using <see cref="NativeMethods.GetThreadContext"/>.
        /// </summary>
        GetContext = 0x0008,
        /// <summary>
        /// Required to use a thread's security information directly without calling it by using a communication mechanism that provides impersonation services.
        /// </summary>
        Impersonate = 0x0100,
        /// <summary>
        /// Required to read certain information from the thread object, such as the exit code (see GetExitCodeThread).
        /// </summary>
        QueryInformation = 0x0040,
        /// <summary>
        /// Required to read certain information from the thread objects (see <see cref="NativeMethods.GetThreadContext"/>). 
        /// A handle that has the THREAD_QUERY_INFORMATION access right is automatically granted THREAD_QUERY_LIMITED_INFORMATION.
        /// </summary>
        QueryLimitedInformation = 0x0800,
        /// <summary>
        /// Required to write the context of a thread using <see cref="NativeMethods.SetThreadContext"/>.
        /// </summary>
        SetContext = 0x0010,
        /// <summary>
        /// Required to set certain information in the thread object.
        /// </summary>
        SetInformation = 0x0020,
        /// <summary>
        /// Required to set certain information in the thread object. A handle that has the THREAD_SET_INFORMATION access right is automatically granted THREAD_SET_LIMITED_INFORMATION.
        /// </summary>
        SetLimitedInformation = 0x0400,
        /// <summary>
        /// Required to set the impersonation token for a thread using SetThreadToken.
        /// </summary>
        SetThreadToken = 0x0080,
        /// <summary>
        /// Required to suspend or resume a thread (see <see cref="NativeMethods.SuspendThread"/> and <see cref="NativeMethods.ResumeThread"/>).
        /// </summary>
        SuspendResume = 0x0002,
        /// <summary>
        /// Required to terminate a thread using <see cref="NativeMethods.TerminateThread"/>.
        /// </summary>
        Terminate = 0x0001
    }
    #endregion

    #region ThreadContextFlags
    /// <summary>
    /// Determines which registers are returned or set when using <see cref="NativeMethods.GetThreadContext"/> or <see cref="NativeMethods.SetThreadContext"/>.
    /// </summary>
    [Flags]
    public enum ThreadContextFlags
    {
        /// <summary>
        /// The Intel 80386 microprocessor, also known as the i386.
        /// </summary>
        Intel386 = 0x10000,
        /// <summary>
        /// The Intel 80486 microprocessor, also known as the i486.
        /// </summary>
        Intel486 = 0x10000,
        /// <summary>
        /// SS:SP, CS:IP, FLAGS, BP
        /// </summary>
        Control = Intel386 | 0x01,
        /// <summary>
        /// AX, BX, CX, DX, SI, DI
        /// </summary>
        Integer = Intel386 | 0x02,
        /// <summary>
        /// DS, ES, FS, GS
        /// </summary>
        Segments = Intel386 | 0x04,
        /// <summary>
        /// 387 state
        /// </summary>
        FloatingPoint = Intel386 | 0x08,
        /// <summary>
        /// DB 0-3,6,7
        /// </summary>
        DebugRegisters = Intel386 | 0x10,
        /// <summary>
        /// CPU specific extensions
        /// </summary>
        ExtendedRegisters = Intel386 | 0x20,
        /// <summary>
        /// All flags excepted FloatingPoint, DebugRegisters and ExtendedRegisters. 
        /// </summary>
        Full = Control | Integer | Segments,
        /// <summary>
        /// All flags.
        /// </summary>
        All = Control | Integer | Segments | FloatingPoint | DebugRegisters | ExtendedRegisters
    }
    #endregion

    #region ThreadCreationFlags
    /// <summary>
    /// Thread creation options list.
    /// </summary>
    [Flags]
    public enum ThreadCreationFlags
    {
        /// <summary>
        /// The thread runs immediately after creation.
        /// </summary>
        Run = 0x0,
        /// <summary>
        /// The thread is created in a suspended state, and does not run until the <see cref="NativeMethods.ResumeThread"/> function is called.
        /// </summary>
        Suspended = 0x04,
        /// <summary>
        /// The dwStackSize parameter specifies the initial reserve size of the stack. If this flag is not specified, dwStackSize specifies the commit size.
        /// </summary>
        StackSizeParamIsAReservation = 0x10000
    }
    #endregion

    #region TranslationTypes
    /// <summary>
    /// The translation types used in the function <see cref="NativeMethods.MapVirtualKey"/> for the keys mapping.
    /// </summary>
    public enum TranslationTypes
    {
        /// <summary>
        /// uCode is a virtual-key code and is translated into a scan code. 
        /// If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned. 
        /// If there is no translation, the function returns 0.
        /// </summary>
        VirtualKeyToScanCode = 0,
        /// <summary>
        /// uCode is a scan code and is translated into a virtual-key code that does not distinguish between left- and right-hand keys. 
        /// If there is no translation, the function returns 0.
        /// </summary>
        ScanCodeToVirtualKey = 1,
        /// <summary>
        /// uCode is a virtual-key code and is translated into an unshifted character value in the low-order word of the return value. 
        /// Dead keys (diacritics) are indicated by setting the top bit of the return value. 
        /// If there is no translation, the function returns 0.
        /// </summary>
        VirtualKeyToChar = 2,
        /// <summary>
        /// uCode is a scan code and is translated into a virtual-key code that distinguishes between left- and right-hand keys. 
        /// If there is no translation, the function returns 0.
        /// </summary>
        ScanCodeToVirtualKeyEx = 3
    }
    #endregion

    #region WaitReturnValues
    /// <summary>
    /// The return values for the function <see cref="NativeMethods.WaitForSingleObject"/>.
    /// </summary>
    public enum WaitValues : uint
    {
        /// <summary>
        /// The specified object is a mutex object that was not released by the thread that owned the mutex object before the owning thread terminated.
        /// Ownership of the mutex object is granted to the calling thread and the mutex state is set to nonsignaled.
        /// If the mutex was protecting persistent state information, you should check it for consistency.
        /// </summary>
        Abandoned = 0x80,
        /// <summary>
        /// The state of the specified object is signaled. Similar to WAIT_OBJECT_0.
        /// </summary>
        Signaled = 0x0,
        /// <summary>
        /// The time-out interval elapsed, and the object's state is nonsignaled.
        /// </summary>
        Timeout = 0x102,
        /// <summary>
        /// The function has failed. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </summary>
        Failed = 0xFFFFFFFF
    }
    #endregion

    #region WindowsMessages
    /// <summary>
    /// Windows Messages list.
    /// </summary>
    public enum WindowsMessages : uint
    {
        /// <summary>
        /// The WM_NULL message performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
        /// </summary>
        Null = 0x0000,
        /// <summary>
        /// The WM_CREATE message is sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created, but before the window becomes visible.
        /// </summary>
        Create = 0x0001,
        /// <summary>
        /// The WM_DESTROY message is sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen.
        /// This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message, it can be assumed that all child windows still exist.
        /// /// </summary>
        Destroy = 0x0002,
        /// <summary>
        /// The WM_MOVE message is sent after a window has been moved.
        /// </summary>
        Move = 0x0003,
        /// <summary>
        /// The WM_SIZE message is sent to a window after its size has changed.
        /// </summary>
        Size = 0x0005,
        /// <summary>
        /// The WM_ACTIVATE message is sent to both the window being activated and the window being deactivated. If the windows use the same input queue, the message is sent synchronously, first to the window procedure of the top-level window being deactivated, then to the window procedure of the top-level window being activated. If the windows use different input queues, the message is sent asynchronously, so the window is activated immediately.
        /// </summary>
        Activate = 0x0006,
        /// <summary>
        /// The WM_SETFOCUS message is sent to a window after it has gained the keyboard focus.
        /// </summary>
        SetFocus = 0x0007,
        /// <summary>
        /// The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus.
        /// </summary>
        KillFocus = 0x0008,
        /// <summary>
        /// The WM_ENABLE message is sent when an application changes the enabled state of a window. It is sent to the window whose enabled state is changing. This message is sent before the EnableWindow function returns, but after the enabled state (WS_DISABLED style bit) of the window has changed.
        /// </summary>
        Enable = 0x000A,
        /// <summary>
        /// An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn.
        /// </summary>
        SetRedraw = 0x000B,
        /// <summary>
        /// An application sends a WM_SETTEXT message to set the text of a window.
        /// </summary>
        SetText = 0x000C,
        /// <summary>
        /// An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller.
        /// </summary>
        GetText = 0x000D,
        /// <summary>
        /// An application sends a WM_GETTEXTLENGTH message to determine the length, in characters, of the text associated with a window.
        /// </summary>
        GetTextLength = 0x000E,
        /// <summary>
        /// The WM_PAINT message is sent when the system or another application makes a request to paint a portion of an application's window. The message is sent when the UpdateWindow or RedrawWindow function is called, or by the DispatchMessage function when the application obtains a WM_PAINT message by using the GetMessage or PeekMessage function.
        /// </summary>
        Paint = 0x000F,
        /// <summary>
        /// The WM_CLOSE message is sent as a signal that a window or an application should terminate.
        /// </summary>
        Close = 0x0010,
        /// <summary>
        /// The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero, the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero.
        /// After processing this message, the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.
        /// </summary>
        QueryEndSession = 0x0011,
        /// <summary>
        /// The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.
        /// </summary>
        QueryOpen = 0x0013,
        /// <summary>
        /// The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.
        /// </summary>
        EndSession = 0x0016,
        /// <summary>
        /// The WM_QUIT message indicates a request to terminate an application and is generated when the application calls the PostQuitMessage function. It causes the GetMessage function to return zero.
        /// </summary>
        Quit = 0x0012,
        /// <summary>
        /// The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized). The message is sent to prepare an invalidated portion of a window for painting.
        /// </summary>
        EraseBkgnd = 0x0014,
        /// <summary>
        /// This message is sent to all top-level windows when a change is made to a system color setting.
        /// </summary>
        SysColorChange = 0x0015,
        /// <summary>
        /// The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown.
        /// </summary>
        ShowWindow = 0x0018,
        /// <summary>
        /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
        /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
        /// </summary>
        WinInitChange = 0x001A,
        /// <summary>
        /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
        /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
        /// </summary>
        SettingChange = WinInitChange,
        /// <summary>
        /// The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings.
        /// </summary>
        DevModeChange = 0x001B,
        /// <summary>
        /// The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated. The message is sent to the application whose window is being activated and to the application whose window is being deactivated.
        /// </summary>
        ActivateApp = 0x001C,
        /// <summary>
        /// An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources.
        /// </summary>
        FontChange = 0x001D,
        /// <summary>
        /// A message that is sent whenever there is a change in the system time.
        /// </summary>
        TimeChange = 0x001E,
        /// <summary>
        /// The WM_CANCELMODE message is sent to cancel certain modes, such as mouse capture. For example, the system sends this message to the active window when a dialog box or message box is displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example, the EnableWindow function sends this message when disabling the specified window.
        /// </summary>
        CancelMode = 0x001F,
        /// <summary>
        /// The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured.
        /// </summary>
        SetCursor = 0x0020,
        /// <summary>
        /// The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.
        /// </summary>
        MouseActivate = 0x0021,
        /// <summary>
        /// The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.
        /// </summary>
        ChildActivate = 0x0022,
        /// <summary>
        /// The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure.
        /// </summary>
        QueueSync = 0x0023,
        /// <summary>
        /// The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change. An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size.
        /// </summary>
        GetMinmaxInfo = 0x0024,
        /// <summary>
        /// Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows, except in unusual circumstances explained in the Remarks.
        /// </summary>
        PaintIcon = 0x0026,
        /// <summary>
        /// Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.
        /// </summary>
        IconeRaseBkgnd = 0x0027,
        /// <summary>
        /// The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box.
        /// </summary>
        NextDlgCtl = 0x0028,
        /// <summary>
        /// The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue.
        /// </summary>
        SpoolerStatus = 0x002A,
        /// <summary>
        /// The WM_DRAWITEM message is sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the button, combo box, list box, or menu has changed.
        /// </summary>
        DrawItem = 0x002B,
        /// <summary>
        /// The WM_MEASUREITEM message is sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.
        /// </summary>
        MeasureItem = 0x002C,
        /// <summary>
        /// Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.
        /// </summary>
        DeleteItem = 0x002D,
        /// <summary>
        /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message.
        /// </summary>
        VkeyToItem = 0x002E,
        /// <summary>
        /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message.
        /// </summary>
        CharToItem = 0x002F,
        /// <summary>
        /// An application sends a WM_SETFONT message to specify the font that a control is to use when drawing text.
        /// </summary>
        SetFont = 0x0030,
        /// <summary>
        /// An application sends a WM_GETFONT message to a control to retrieve the font with which the control is currently drawing its text.
        /// </summary>
        GetFont = 0x0031,
        /// <summary>
        /// An application sends a WM_SETHOTKEY message to a window to associate a hot key with the window. When the user presses the hot key, the system activates the window.
        /// </summary>
        SetHotKey = 0x0032,
        /// <summary>
        /// An application sends a WM_GETHOTKEY message to determine the hot key associated with a window.
        /// </summary>
        GetHotKey = 0x0033,
        /// <summary>
        /// The WM_QUERYDRAGICON message is sent to a minimized (iconic) window. The window is about to be dragged by the user but does not have an icon defined for its class. An application can return a handle to an icon or cursor. The system displays this cursor or icon while the user drags the icon.
        /// </summary>
        QueryDragIcon = 0x0037,
        /// <summary>
        /// The system sends the WM_COMPAREITEM message to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box. Whenever the application adds a new item, the system sends this message to the owner of a combo box or list box created with the CBS_SORT or LBS_SORT style.
        /// </summary>
        CompareItem = 0x0039,
        /// <summary>
        /// Active Accessibility sends the WM_GETOBJECT message to obtain information about an accessible object contained in a server application.
        /// Applications never send this message directly. It is sent only by Active Accessibility in response to calls to AccessibleObjectFromPoint, AccessibleObjectFromEvent, or AccessibleObjectFromWindow. However, server applications handle this message.
        /// </summary>
        GetObject = 0x003D,
        /// <summary>
        /// The WM_COMPACTING message is sent to all top-level windows when the system detects more than 12.5 percent of system time over a 30- to 60-second interval is being spent compacting memory. This indicates that system memory is low.
        /// </summary>
        Compacting = 0x0041,
        /// <summary>
        /// WM_COMMNOTIFY is Obsolete for Win32-Based Applications
        /// </summary>
        [Obsolete]
        CommNotify = 0x0044,
        /// <summary>
        /// The WM_WINDOWPOSCHANGING message is sent to a window whose size, position, or place in the Z order is about to change as a result of a call to the SetWindowPos function or another window-management function.
        /// </summary>
        WindowPosChanging = 0x0046,
        /// <summary>
        /// The WM_WINDOWPOSCHANGED message is sent to a window whose size, position, or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
        /// </summary>
        WindowPosChanged = 0x0047,
        /// <summary>
        /// Notifies applications that the system, typically a battery-powered personal computer, is about to enter a suspended mode.
        /// Use: POWERBROADCAST
        /// </summary>
        [Obsolete]
        Power = 0x0048,
        /// <summary>
        /// An application sends the WM_COPYDATA message to pass data to another application.
        /// </summary>
        CopyData = 0x004A,
        /// <summary>
        /// The WM_CANCELJOURNAL message is posted to an application when a user cancels the application's journaling activities. The message is posted with a NULL window handle.
        /// </summary>
        CancelJournal = 0x004B,
        /// <summary>
        /// Sent by a common control to its parent window when an event has occurred or the control requires some information.
        /// </summary>
        Notify = 0x004E,
        /// <summary>
        /// The WM_INPUTLANGCHANGEREQUEST message is posted to the window with the focus when the user chooses a new input language, either with the hotkey (specified in the Keyboard control panel application) or from the indicator on the system taskbar. An application can accept the change by passing the message to the DefWindowProc function or reject the change (and prevent it from taking place) by returning immediately.
        /// </summary>
        InputLangChangeRequest = 0x0050,
        /// <summary>
        /// The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function, which passes the message to all first-level child windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows, and so on.
        /// </summary>
        InputLangChange = 0x0051,
        /// <summary>
        /// Sent to an application that has initiated a training card with Microsoft Windows Help. The message informs the application when the user clicks an authorable button. An application initiates a training card by specifying the HELP_TCARD command in a call to the WinHelp function.
        /// </summary>
        Tcard = 0x0052,
        /// <summary>
        /// Indicates that the user pressed the F1 key. If a menu is active when F1 is pressed, WM_HELP is sent to the window associated with the menu; otherwise, WM_HELP is sent to the window that has the keyboard focus. If no window has the keyboard focus, WM_HELP is sent to the currently active window.
        /// </summary>
        Help = 0x0053,
        /// <summary>
        /// The WM_USERCHANGED message is sent to all windows after the user has logged on or off. When the user logs on or off, the system updates the user-specific settings. The system sends this message immediately after updating the settings.
        /// </summary>
        UserChanged = 0x0054,
        /// <summary>
        /// Determines if a window accepts ANSI or Unicode structures in the WM_NOTIFY notification message. WM_NOTIFYFORMAT messages are sent from a common control to its parent window and from the parent window to the common control.
        /// </summary>
        NotifyFormat = 0x0055,
        /// <summary>
        /// The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.
        /// </summary>
        ContextMenu = 0x007B,
        /// <summary>
        /// The WM_STYLECHANGING message is sent to a window when the SetWindowLong function is about to change one or more of the window's styles.
        /// </summary>
        StyleChanging = 0x007C,
        /// <summary>
        /// The WM_STYLECHANGED message is sent to a window after the SetWindowLong function has changed one or more of the window's styles
        /// </summary>
        StyleChanged = 0x007D,
        /// <summary>
        /// The WM_DISPLAYCHANGE message is sent to all windows when the display resolution has changed.
        /// </summary>
        DisplayChange = 0x007E,
        /// <summary>
        /// The WM_GETICON message is sent to a window to retrieve a handle to the large or small icon associated with a window. The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption.
        /// </summary>
        GetIcon = 0x007F,
        /// <summary>
        /// An application sends the WM_SETICON message to associate a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption.
        /// </summary>
        SetIcon = 0x0080,
        /// <summary>
        /// The WM_NCCREATE message is sent prior to the WM_CREATE message when a window is first created.
        /// </summary>
        NcCreate = 0x0081,
        /// <summary>
        /// The WM_NCDESTROY message informs a window that its nonclient area is being destroyed. The DestroyWindow function sends the WM_NCDESTROY message to the window following the WM_DESTROY message. WM_DESTROY is used to free the allocated memory object associated with the window.
        /// The WM_NCDESTROY message is sent after the child windows have been destroyed. In contrast, WM_DESTROY is sent before the child windows are destroyed.
        /// </summary>
        NcDestroy = 0x0082,
        /// <summary>
        /// The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated. By processing this message, an application can control the content of the window's client area when the size or position of the window changes.
        /// </summary>
        NcCalcSize = 0x0083,
        /// <summary>
        /// The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released. If the mouse is not captured, the message is sent to the window beneath the cursor. Otherwise, the message is sent to the window that has captured the mouse.
        /// </summary>
        NcHitTest = 0x0084,
        /// <summary>
        /// The WM_NCPAINT message is sent to a window when its frame must be painted.
        /// </summary>
        NcPaint = 0x0085,
        /// <summary>
        /// The WM_NCACTIVATE message is sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.
        /// </summary>
        NcActivate = 0x0086,
        /// <summary>
        /// The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.
        /// </summary>
        GetDlgCode = 0x0087,
        /// <summary>
        /// The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.
        /// </summary>
        SyncPaint = 0x0088,
        /// <summary>
        /// The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcMouseMove = 0x00A0,
        /// <summary>
        /// The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcLButtonDown = 0x00A1,
        /// <summary>
        /// The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcLButtonUp = 0x00A2,
        /// <summary>
        /// The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcLButtonDblClk = 0x00A3,
        /// <summary>
        /// The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcRButtonDown = 0x00A4,
        /// <summary>
        /// The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcRButtonUp = 0x00A5,
        /// <summary>
        /// The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcRButtonDblClk = 0x00A6,
        /// <summary>
        /// The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcMButtonDown = 0x00A7,
        /// <summary>
        /// The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcMButtonUp = 0x00A8,
        /// <summary>
        /// The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcMButtonDblClk = 0x00A9,
        /// <summary>
        /// The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcXButtonDown = 0x00AB,
        /// <summary>
        /// The WM_NCXBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcXButtonUp = 0x00AC,
        /// <summary>
        /// The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        /// </summary>
        NcXButtonDblClk = 0x00AD,
        /// <summary>
        /// The WM_INPUT_DEVICE_CHANGE message is sent to the window that registered to receive raw input. A window receives this message through its WindowProc function.
        /// </summary>
        InputDeviceChange = 0x00FE,
        /// <summary>
        /// The WM_INPUT message is sent to the window that is getting raw input.
        /// </summary>
        Input = 0x00FF,
        /// <summary>
        /// This message filters for keyboard messages.
        /// </summary>
        KeyFirst = 0x0100,
        /// <summary>
        /// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
        /// </summary>
        KeyDown = 0x0100,
        /// <summary>
        /// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.
        /// </summary>
        KeyUp = 0x0101,
        /// <summary>
        /// The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed.
        /// </summary>
        Char = 0x0102,
        /// <summary>
        /// The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character, such as the umlaut (double-dot), that is combined with another character to form a composite character. For example, the umlaut-O character (Ö) is generated by typing the dead key for the umlaut character, and then typing the O key.
        /// </summary>
        DeadChar = 0x0103,
        /// <summary>
        /// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
        /// </summary>
        SysKeyDown = 0x0104,
        /// <summary>
        /// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
        /// </summary>
        SysKeyUp = 0x0105,
        /// <summary>
        /// The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is, a character key that is pressed while the ALT key is down.
        /// </summary>
        SysChar = 0x0106,
        /// <summary>
        /// The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is, a dead key that is pressed while holding down the ALT key.
        /// </summary>
        SysDeadChar = 0x0107,
        /// <summary>
        /// The WM_UNICHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_UNICHAR message contains the character code of the key that was pressed.
        /// The WM_UNICHAR message is equivalent to WM_CHAR, but it uses Unicode Transformation Format (UTF)-32, whereas WM_CHAR uses UTF-16. It is designed to send or post Unicode characters to ANSI windows and it can can handle Unicode Supplementary Plane characters.
        /// </summary>
        UniChar = 0x0109,
        /// <summary>
        /// This message filters for keyboard messages.
        /// </summary>
        KeyLast = 0x0109,
        /// <summary>
        /// Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function.
        /// </summary>
        ImeStartComposition = 0x010D,
        /// <summary>
        /// Sent to an application when the IME ends composition. A window receives this message through its WindowProc function.
        /// </summary>
        ImeEndComposition = 0x010E,
        /// <summary>
        /// Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function.
        /// </summary>
        ImeComposition = 0x010F,
        /// <summary>
        /// Signifies the last keyboard-related input msg (when including IME composition).
        /// </summary>
        ImeKeyLast = 0x010F,
        /// <summary>
        /// The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is displayed. Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box.
        /// </summary>
        InitDialog = 0x0110,
        /// <summary>
        /// The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated.
        /// </summary>
        Command = 0x0111,
        /// <summary>
        /// A window receives this message when the user chooses a command from the Window menu, clicks the maximize button, minimize button, restore button, close button, or moves the form. You can stop the form from moving by filtering this out.
        /// </summary>
        SysCommand = 0x0112,
        /// <summary>
        /// The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function.
        /// </summary>
        Timer = 0x0113,
        /// <summary>
        /// The WM_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control.
        /// </summary>
        HScroll = 0x0114,
        /// <summary>
        /// The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control.
        /// </summary>
        VScroll = 0x0115,
        /// <summary>
        /// The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is displayed.
        /// </summary>
        InitMenu = 0x0116,
        /// <summary>
        /// The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is displayed, without changing the entire menu.
        /// </summary>
        InitMenuPopup = 0x0117,
        /// <summary>
        /// The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item.
        /// </summary>
        MenuSelect = 0x011F,
        /// <summary>
        /// The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu.
        /// </summary>
        MenuChar = 0x0120,
        /// <summary>
        /// The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages.
        /// </summary>
        EnterIdle = 0x0121,
        /// <summary>
        /// The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item.
        /// </summary>
        MenuRButtonUp = 0x0122,
        /// <summary>
        /// The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item.
        /// </summary>
        MenuDrag = 0x0123,
        /// <summary>
        /// The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item.
        /// </summary>
        MenuGetObject = 0x0124,
        /// <summary>
        /// The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed.
        /// </summary>
        UnInitMenuPopup = 0x0125,
        /// <summary>
        /// The WM_MENUCOMMAND message is sent when the user makes a selection from a menu.
        /// </summary>
        MenuCommand = 0x0126,
        /// <summary>
        /// An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.
        /// </summary>
        ChangeUiState = 0x0127,
        /// <summary>
        /// An application sends the WM_UPDATEUISTATE message to change the user interface (UI) state for the specified window and all its child windows.
        /// </summary>
        UpdateUiState = 0x0128,
        /// <summary>
        /// An application sends the WM_QUERYUISTATE message to retrieve the user interface (UI) state for a window.
        /// </summary>
        QueryUiState = 0x0129,
        /// <summary>
        /// The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message, the owner window can set the text and background colors of the message box by using the given display device context handle.
        /// </summary>
        CtlColorMsgBox = 0x0132,
        /// <summary>
        /// An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the edit control.
        /// </summary>
        CtlColorEdit = 0x0133,
        /// <summary>
        /// Sent to the parent window of a list box before the system draws the list box. By responding to this message, the parent window can set the text and background colors of the list box by using the specified display device context handle.
        /// </summary>
        CtlColorListBox = 0x0134,
        /// <summary>
        /// The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However, only owner-drawn buttons respond to the parent window processing this message.
        /// </summary>
        CtlColorBtn = 0x0135,
        /// <summary>
        /// The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message, the dialog box can set its text and background colors using the specified display device context handle.
        /// </summary>
        CtlColorDlg = 0x0136,
        /// <summary>
        /// The WM_CTLCOLORSCROLLBAR message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message, the parent window can use the display context handle to set the background color of the scroll bar control.
        /// </summary>
        CtlColorScrollbar = 0x0137,
        /// <summary>
        /// A static control, or an edit control that is read-only or disabled, sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the static control.
        /// </summary>
        CtlColorStatic = 0x0138,
        /// <summary>
        /// Use WM_MOUSEFIRST to specify the first mouse message. Use the PeekMessage() Function.
        /// </summary>
        MouseFirst = 0x0200,
        /// <summary>
        /// The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        MouseMove = 0x0200,
        /// <summary>
        /// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        LButtonDown = 0x0201,
        /// <summary>
        /// The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        LButtonUp = 0x0202,
        /// <summary>
        /// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        LButtonDblClk = 0x0203,
        /// <summary>
        /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        RButtonDown = 0x0204,
        /// <summary>
        /// The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        RButtonUp = 0x0205,
        /// <summary>
        /// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        RButtonDblClk = 0x0206,
        /// <summary>
        /// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        MButtonDown = 0x0207,
        /// <summary>
        /// The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        MButtonUp = 0x0208,
        /// <summary>
        /// The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        MButtonDblClk = 0x0209,
        /// <summary>
        /// The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
        /// </summary>
        MouseWheel = 0x020A,
        /// <summary>
        /// The WM_XBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        XButtonDown = 0x020B,
        /// <summary>
        /// The WM_XBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        XButtonUp = 0x020C,
        /// <summary>
        /// The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        XButtonDblClk = 0x020D,
        /// <summary>
        /// The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
        /// </summary>
        MouseHWheel = 0x020E,
        /// <summary>
        /// Use WM_MOUSELAST to specify the last mouse message. Used with PeekMessage() Function.
        /// </summary>
        MouseLast = 0x020E,
        /// <summary>
        /// The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed, or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created, the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns. When the child window is being destroyed, the system sends the message before any processing to destroy the window takes place.
        /// </summary>
        ParentNotify = 0x0210,
        /// <summary>
        /// The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered.
        /// </summary>
        EnterMenuLoop = 0x0211,
        /// <summary>
        /// The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited.
        /// </summary>
        ExitMenuLoop = 0x0212,
        /// <summary>
        /// The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu.
        /// </summary>
        NextMenu = 0x0213,
        /// <summary>
        /// The WM_SIZING message is sent to a window that the user is resizing. By processing this message, an application can monitor the size and position of the drag rectangle and, if needed, change its size or position.
        /// </summary>
        Sizing = 0x0214,
        /// <summary>
        /// The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.
        /// </summary>
        CaptureChanged = 0x0215,
        /// <summary>
        /// The WM_MOVING message is sent to a window that the user is moving. By processing this message, an application can monitor the position of the drag rectangle and, if needed, change its position.
        /// </summary>
        Moving = 0x0216,
        /// <summary>
        /// Notifies applications that a power-management event has occurred.
        /// </summary>
        PowerBroadcast = 0x0218,
        /// <summary>
        /// Notifies an application of a change to the hardware configuration of a device or the computer.
        /// </summary>
        DeviceChange = 0x0219,
        /// <summary>
        /// An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window.
        /// </summary>
        MdiCreate = 0x0220,
        /// <summary>
        /// An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window.
        /// </summary>
        MdiDestroy = 0x0221,
        /// <summary>
        /// An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window.
        /// </summary>
        MdiActivate = 0x0222,
        /// <summary>
        /// An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size.
        /// </summary>
        MdiRestore = 0x0223,
        /// <summary>
        /// An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window.
        /// </summary>
        MdiNext = 0x0224,
        /// <summary>
        /// An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar, and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window.
        /// </summary>
        MdiMaximize = 0x0225,
        /// <summary>
        /// An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format.
        /// </summary>
        MdiTile = 0x0226,
        /// <summary>
        /// An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format.
        /// </summary>
        MdiCascade = 0x0227,
        /// <summary>
        /// An application sends the WM_MDIICONARRANGE message to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized.
        /// </summary>
        MdiIconArrange = 0x0228,
        /// <summary>
        /// An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window.
        /// </summary>
        MdiGetActive = 0x0229,
        /// <summary>
        /// An application sends the WM_MDISETMENU message to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window, to replace the window menu of the frame window, or both.
        /// </summary>
        MdiSetMenu = 0x0230,
        /// <summary>
        /// The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
        /// The system sends the WM_ENTERSIZEMOVE message regardless of whether the dragging of full windows is enabled.
        /// </summary>
        EnterSizeMove = 0x0231,
        /// <summary>
        /// The WM_EXITSIZEMOVE message is sent one time to a window, after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
        /// </summary>
        ExitSizeMove = 0x0232,
        /// <summary>
        /// Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.
        /// </summary>
        DropFiles = 0x0233,
        /// <summary>
        /// An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window.
        /// </summary>
        MdiRefreshMenu = 0x0234,
        /// <summary>
        /// Sent to an application when a window is activated. A window receives this message through its WindowProc function.
        /// </summary>
        ImeSetContext = 0x0281,
        /// <summary>
        /// Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function.
        /// </summary>
        ImeNotify = 0x0282,
        /// <summary>
        /// Sent by an application to direct the IME window to carry out the requested command. The application uses this message to control the IME window that it has created. To send this message, the application calls the SendMessage function with the following parameters.
        /// </summary>
        ImeControl = 0x0283,
        /// <summary>
        /// Sent to an application when the IME window finds no space to extend the area for the composition window. A window receives this message through its WindowProc function.
        /// </summary>
        ImeCompositionFull = 0x0284,
        /// <summary>
        /// Sent to an application when the operating system is about to change the current IME. A window receives this message through its WindowProc function.
        /// </summary>
        ImeSelect = 0x0285,
        /// <summary>
        /// Sent to an application when the IME gets a character of the conversion result. A window receives this message through its WindowProc function.
        /// </summary>
        ImeChar = 0x0286,
        /// <summary>
        /// Sent to an application to provide commands and request information. A window receives this message through its WindowProc function.
        /// </summary>
        ImeRequest = 0x0288,
        /// <summary>
        /// Sent to an application by the IME to notify the application of a key press and to keep message order. A window receives this message through its WindowProc function.
        /// </summary>
        ImeKeyDown = 0x0290,
        /// <summary>
        /// Sent to an application by the IME to notify the application of a key release and to keep message order. A window receives this message through its WindowProc function.
        /// </summary>
        ImeKeyUp = 0x0291,
        /// <summary>
        /// The WM_MOUSEHOVER message is posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to TrackMouseEvent.
        /// </summary>
        MouseHover = 0x02A1,
        /// <summary>
        /// The WM_MOUSELEAVE message is posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
        /// </summary>
        MouseLeave = 0x02A3,
        /// <summary>
        /// The WM_NCMOUSEHOVER message is posted to a window when the cursor hovers over the nonclient area of the window for the period of time specified in a prior call to TrackMouseEvent.
        /// </summary>
        NcMouseHover = 0x02A0,
        /// <summary>
        /// The WM_NCMOUSELEAVE message is posted to a window when the cursor leaves the nonclient area of the window specified in a prior call to TrackMouseEvent.
        /// </summary>
        NcMouseLeave = 0x02A2,
        /// <summary>
        /// The WM_WTSSESSION_CHANGE message notifies applications of changes in session state.
        /// </summary>
        WtsSessionChange = 0x02B1,
        TabletFirst = 0x02c0,
        TabletLast = 0x02df,
        /// <summary>
        /// An application sends a WM_CUT message to an edit control or combo box to delete (cut) the current selection, if any, in the edit control and copy the deleted text to the clipboard in CF_TEXT format.
        /// </summary>
        Cut = 0x0300,
        /// <summary>
        /// An application sends the WM_COPY message to an edit control or combo box to copy the current selection to the clipboard in CF_TEXT format.
        /// </summary>
        Copy = 0x0301,
        /// <summary>
        /// An application sends a WM_PASTE message to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position. Data is inserted only if the clipboard contains data in CF_TEXT format.
        /// </summary>
        Paste = 0x0302,
        /// <summary>
        /// An application sends a WM_CLEAR message to an edit control or combo box to delete (clear) the current selection, if any, from the edit control.
        /// </summary>
        Clear = 0x0303,
        /// <summary>
        /// An application sends a WM_UNDO message to an edit control to undo the last operation. When this message is sent to an edit control, the previously deleted text is restored or the previously added text is deleted.
        /// </summary>
        Undo = 0x0304,
        /// <summary>
        /// The WM_RENDERFORMAT message is sent to the clipboard owner if it has delayed rendering a specific clipboard format and if an application has requested data in that format. The clipboard owner must render data in the specified format and place it on the clipboard by calling the SetClipboardData function.
        /// </summary>
        RenderFormat = 0x0305,
        /// <summary>
        /// The WM_RENDERALLFORMATS message is sent to the clipboard owner before it is destroyed, if the clipboard owner has delayed rendering one or more clipboard formats. For the content of the clipboard to remain available to other applications, the clipboard owner must render data in all the formats it is capable of generating, and place the data on the clipboard by calling the SetClipboardData function.
        /// </summary>
        RenderAllFormats = 0x0306,
        /// <summary>
        /// The WM_DESTROYCLIPBOARD message is sent to the clipboard owner when a call to the EmptyClipboard function empties the clipboard.
        /// </summary>
        DestroyClipboard = 0x0307,
        /// <summary>
        /// The WM_DRAWCLIPBOARD message is sent to the first window in the clipboard viewer chain when the content of the clipboard changes. This enables a clipboard viewer window to display the new content of the clipboard.
        /// </summary>
        DrawClipboard = 0x0308,
        /// <summary>
        /// The WM_PAINTCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area needs repainting.
        /// </summary>
        PaintClipboard = 0x0309,
        /// <summary>
        /// The WM_VSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's vertical scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
        /// </summary>
        VScrollClipboard = 0x030A,
        /// <summary>
        /// The WM_SIZECLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area has changed size.
        /// </summary>
        SizeClipboard = 0x030B,
        /// <summary>
        /// The WM_ASKCBFORMATNAME message is sent to the clipboard owner by a clipboard viewer window to request the name of a CF_OWNERDISPLAY clipboard format.
        /// </summary>
        AskCbFormatName = 0x030C,
        /// <summary>
        /// The WM_CHANGECBCHAIN message is sent to the first window in the clipboard viewer chain when a window is being removed from the chain.
        /// </summary>
        ChangeCbChain = 0x030D,
        /// <summary>
        /// The WM_HSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window. This occurs when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's horizontal scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
        /// </summary>
        HScrollClipboard = 0x030E,
        /// <summary>
        /// This message informs a window that it is about to receive the keyboard focus, giving the window the opportunity to realize its logical palette when it receives the focus.
        /// </summary>
        QueryNewPalette = 0x030F,
        /// <summary>
        /// The WM_PALETTEISCHANGING message informs applications that an application is going to realize its logical palette.
        /// </summary>
        PaletteIsChanging = 0x0310,
        /// <summary>
        /// This message is sent by the OS to all top-level and overlapped windows after the window with the keyboard focus realizes its logical palette.
        /// This message enables windows that do not have the keyboard focus to realize their logical palettes and update their client areas.
        /// </summary>
        PaletteChanged = 0x0311,
        /// <summary>
        /// The WM_HOTKEY message is posted when the user presses a hot key registered by the RegisterHotKey function. The message is placed at the top of the message queue associated with the thread that registered the hot key.
        /// </summary>
        HotKey = 0x0312,
        /// <summary>
        /// The WM_PRINT message is sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context.
        /// </summary>
        Print = 0x0317,
        /// <summary>
        /// The WM_PRINTCLIENT message is sent to a window to request that it draw its client area in the specified device context, most commonly in a printer device context.
        /// </summary>
        PrintClient = 0x0318,
        /// <summary>
        /// The WM_APPCOMMAND message notifies a window that the user generated an application command event, for example, by clicking an application command button using the mouse or typing an application command key on the keyboard.
        /// </summary>
        AppCommand = 0x0319,
        /// <summary>
        /// The WM_THEMECHANGED message is broadcast to every window following a theme change event. Examples of theme change events are the activation of a theme, the deactivation of a theme, or a transition from one theme to another.
        /// </summary>
        ThemeChanged = 0x031A,
        /// <summary>
        /// Sent when the contents of the clipboard have changed.
        /// </summary>
        ClipboardUpdate = 0x031D,
        /// <summary>
        /// The system will send a window the WM_DWMCOMPOSITIONCHANGED message to indicate that the availability of desktop composition has changed.
        /// </summary>
        DwmCompositionChanged = 0x031E,
        /// <summary>
        /// WM_DWMNCRENDERINGCHANGED is called when the non-client area rendering status of a window has changed. Only windows that have set the flag DWM_BLURBEHIND.fTransitionOnMaximized to true will get this message.
        /// </summary>
        DwmNCrenderingChanged = 0x031F,
        /// <summary>
        /// Sent to all top-level windows when the colorization color has changed.
        /// </summary>
        DwmColorizationColorChanged = 0x0320,
        /// <summary>
        /// WM_DWMWINDOWMAXIMIZEDCHANGE will let you know when a DWM composed window is maximized. You also have to register for this message as well. You'd have other windowd go opaque when this message is sent.
        /// </summary>
        DwmWindowMaximizedChange = 0x0321,
        /// <summary>
        /// Sent to request extended title bar information. A window receives this message through its WindowProc function.
        /// </summary>
        GetTitleBarInfoEx = 0x033F,
        /// <summary>
        /// Specifies the first hand-held msg.
        /// </summary>
        HandheldFirst = 0x0358,
        /// <summary>
        /// Specifies the last hand-held msg.
        /// </summary>
        HandheldLast = 0x035F,
        /// <summary>
        /// Specifies the first afx msg.
        /// </summary>
        AfxFirst = 0x0360,
        /// <summary>
        /// Specifies the last afx msg.
        /// </summary>
        AfxLast = 0x037F,
        /// <summary>
        /// Specifies the first penwin msg 
        /// </summary>
        PenwinFirst = 0x0380,
        /// <summary>
        /// Specifies the last penwin msg 
        /// </summary>
        PenwinLast = 0x038F,
        /// <summary>
        /// The WM_APP constant is used by applications to help define private messages, usually of the form WM_APP+X, where X is an integer value.
        /// </summary>
        App = 0x8000,
        /// <summary>
        /// The WM_USER constant is used by applications to help define private messages for use by private window classes, usually of the form WM_USER+X, where X is an integer value.
        /// </summary>
        User = 0x0400,
        /// <summary>
        /// An application sends the WM_CPL_LAUNCH message to Windows Control Panel to request that a Control Panel application be started.
        /// </summary>
        CplLaunch = User + 0x1000,
        /// <summary>
        /// The WM_CPL_LAUNCHED message is sent when a Control Panel application, started by the WM_CPL_LAUNCH message, has closed. The WM_CPL_LAUNCHED message is sent to the window identified by the wParam parameter of the WM_CPL_LAUNCH message that started the application.
        /// </summary>
        CplLaunched = User + 0x1001,
        /// <summary>
        /// WM_SYSTIMER is a well-known yet still undocumented message. Windows uses WM_SYSTIMER for internal actions like scrolling.
        /// </summary>
        SysTimer = 0x118

    }
    #endregion

    #region WindowStates
    /// <summary>
    /// Window states list.
    /// </summary>
    public enum WindowStates
    {
        /// <summary>Hides the window and activates another window.</summary>
        Hide = 0,
        /// <summary>
        /// Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. 
        /// An application should specify this flag when displaying the window for the first time.
        /// </summary>
        ShowNormal = 1,
        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        ShowMinimized = 2,
        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>
        ShowMaximized = 3,
        /// <summary>
        /// Maximizes the specified window.
        /// </summary>
        Maximize = 3,
        /// <summary>
        /// Displays a window in its most recent size and position.
        /// This value is similar to <see cref="ShowNormal"/>, except the window is not actived.
        /// </summary>
        ShowNormalNoActivate = 4,
        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        Show = 5,
        /// <summary>
        /// Minimizes the specified window and activates the next top-level window in the Z order.
        /// </summary>
        Minimize = 6,
        /// <summary>
        /// Displays the window as a minimized window. This value is similar to <see cref="ShowMinimized"/>, except the window is not activated.
        /// </summary>
        ShowMinNoActive = 7,
        /// <summary>
        /// Displays the window in its current size and position. This value is similar to <see cref="Show"/>, except the window is not activated.
        /// </summary>
        ShowNoActivate = 8,
        /// <summary>
        /// Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. 
        /// An application should specify this flag when restoring a minimized window.
        /// </summary>
        Restore = 9,
        /// <summary>
        /// Sets the show state based on the <see cref="WindowStates"/> value specified in the STARTUPINFO structure passed to the CreateProcess 
        /// function by the program that started the application.
        /// </summary>
        ShowDefault = 10,
        /// <summary>
        /// Windows 2000/XP: Minimizes a window, even if the thread that owns the window is hung. 
        /// This flag should only be used when minimizing windows from a different thread.
        /// </summary>
        ForceMinimized = 11
    }
    #endregion
}
