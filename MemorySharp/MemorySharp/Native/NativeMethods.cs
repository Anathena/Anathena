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
using System.Text;

namespace Binarysharp.MemoryManagement.Native
{
    /// <summary>
    /// Static class referencing all P/Invoked functions used by the library.
    /// </summary>
    public static class NativeMethods
    {
        #region CloseHandle
        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A valid handle to an open object.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
        #endregion

        #region CreateRemoteThread
        /// <summary>
        /// Creates a thread that runs in the virtual address space of another process.
        /// Use the CreateRemoteThreadEx function to create a thread that runs in the virtual address space of another processor and optionally specify extended attributes.
        /// </summary>
        /// <param name="hProcess">
        /// A handle to the process in which the thread is to be created. The handle must have the PROCESS_CREATE_THREAD, PROCESS_QUERY_INFORMATION, 
        /// PROCESS_VM_OPERATION, PROCESS_VM_WRITE, and PROCESS_VM_READ access rights, and may fail without these rights on certain platforms. 
        /// For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="lpThreadAttributes">
        /// A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new thread and determines whether child processes can inherit the returned handle. 
        /// If lpThreadAttributes is NULL, the thread gets a default security descriptor and the handle cannot be inherited. 
        /// The access control lists (ACL) in the default security descriptor for a thread come from the primary token of the creator.
        /// </param>
        /// <param name="dwStackSize">
        /// The initial size of the stack, in bytes. The system rounds this value to the nearest page. If this parameter is 0 (zero), the new thread uses the default size for the executable. 
        /// For more information, see Thread Stack Size.</param>
        /// <param name="lpStartAddress">
        /// A pointer to the application-defined function of type LPTHREAD_START_ROUTINE to be executed by the thread and represents the starting address of the thread in the remote process. 
        /// The function must exist in the remote process. For more information, see ThreadProc.
        /// </param>
        /// <param name="lpParameter">A pointer to a variable to be passed to the thread function.</param>
        /// <param name="dwCreationFlags">The flags that control the creation of the thread.</param>
        /// <param name="lpThreadId">A pointer to a variable that receives the thread identifier. If this parameter is NULL, the thread identifier is not returned.</param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the new thread. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeMemoryHandle CreateRemoteThread(SafeMemoryHandle hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress,
            IntPtr lpParameter, ThreadCreationFlags dwCreationFlags, out int lpThreadId);
        #endregion

        #region FreeLibrary
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
        public static extern bool FreeLibrary(IntPtr hModule);
        #endregion

        #region GetClassName
        /// <summary>
        /// Retrieves the name of the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="lpClassName">The class name string.</param>
        /// <param name="nMaxCount">
        /// The length of the lpClassName buffer, in characters. 
        /// The buffer must be large enough to include the terminating null character; otherwise, the class name string is truncated to nMaxCount-1 characters.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the number of characters copied to the buffer, not including the terminating null character.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>. 
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        #endregion

        #region GetForegroundWindow
        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working). 
        /// The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads. 
        /// </summary>
        /// <returns>The return value is a handle to the foreground window. The foreground window can be NULL in certain circumstances, such as when a window is losing activation.</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        #endregion

        #region GetExitCodeThread
        /// <summary>
        /// Retrieves the termination status of the specified thread.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread. 
        /// The handle must have the <see cref="ThreadAccessFlags.QueryInformation"/> or <see cref="ThreadAccessFlags.QueryLimitedInformation"/> access right. 
        /// For more information, see Thread Security and Access Rights.
        /// </param>
        /// <param name="lpExitCode">A pointer to a variable to receive the thread termination status. For more information, see Remarks.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetExitCodeThread(SafeMemoryHandle hThread, out IntPtr lpExitCode);
        #endregion

        #region GetProcAddress
        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="hModule">
        /// A handle to the DLL module that contains the function or variable. The LoadLibrary, LoadLibraryEx, LoadPackagedLibrary, or GetModuleHandle function returns this handle. 
        /// The GetProcAddress function does not retrieve addresses from modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag. For more information, see LoadLibraryEx.
        /// </param>
        /// <param name="procName">
        /// The function or variable name, or the function's ordinal value. 
        /// If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the address of the exported function or variable. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        #endregion

        #region GetProcessId
        /// <summary>
        /// Retrieves the process identifier of the specified process.
        /// </summary>
        /// <param name="hProcess">
        /// A handle to the process. The handle must have the PROCESS_QUERY_INFORMATION or PROCESS_QUERY_LIMITED_INFORMATION access right. 
        /// For more information, see Process Security and Access Rights.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a process id to the specified handle. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetProcessId(SafeMemoryHandle hProcess);
        #endregion

        #region GetSystemMetrics
        /// <summary>
        /// Retrieves the specified system metric or system configuration setting.
        /// Note that all dimensions retrieved by <see cref="GetSystemMetrics"/> are in pixels.
        /// </summary>
        /// <param name="metric">The system metric or configuration setting to be retrieved.</param>
        /// <returns>
        /// If the function succeeds, the return value is the requested system metric or configuration setting.
        /// If the function fails, the return value is 0. <see cref="Marshal.GetLastWin32Error"/> does not provide extended error information. 
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetSystemMetrics(SystemMetrics metric);
        #endregion

        #region GetThreadContext
        /// <summary>
        /// Retrieves the context of the specified thread. A 64-bit application can retrieve the context of a WOW64 thread using the Wow64GetThreadContext function.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread whose context is to be retrieved. The handle must have <see cref="ThreadAccessFlags.GetContext"/> access to the thread. 
        /// For more information, see Thread Security and Access Rights.
        /// WOW64: The handle must also have <see cref="ThreadAccessFlags.QueryInformation"/> access.
        /// </param>
        /// <param name="lpContext">
        /// [Ref] A pointer to a <see cref="ThreadContext"/> structure that receives the appropriate context of the specified thread. 
        /// The value of the ContextFlags member of this structure specifies which portions of a thread's context are retrieved. 
        /// The <see cref="ThreadContext"/> structure is highly processor specific.
        /// Refer to the WinNT.h header file for processor-specific definitions of this structures and any alignment requirements.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetThreadContext(SafeMemoryHandle hThread, ref ThreadContext lpContext);

        #endregion

        #region GetThreadSelectorEntry
        /// <summary>
        /// Retrieves a descriptor table entry for the specified selector and thread.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread containing the specified selector.
        /// The handle must have <see cref="ThreadAccessFlags.QueryInformation"/> access.
        /// </param>
        /// <param name="dwSelector">The global or local selector value to look up in the thread's descriptor tables.</param>
        /// <param name="lpSelectorEntry">
        /// A pointer to an <see cref="LdtEntry"/> structure that receives a copy of the descriptor table entry 
        /// if the specified selector has an entry in the specified thread's descriptor table. 
        /// This information can be used to convert a segment-relative address to a linear virtual address.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// In that case, the structure pointed to by the lpSelectorEntry parameter receives a copy of the specified descriptor table entry.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetThreadSelectorEntry(SafeMemoryHandle hThread, uint dwSelector, out LdtEntry lpSelectorEntry);
        #endregion

        #region GetThreadId
        /// <summary>
        /// Retrieves the thread identifier of the specified thread.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread. The handle must have the THREAD_QUERY_INFORMATION or THREAD_QUERY_LIMITED_INFORMATION access right. 
        /// For more information about access rights, see Thread Security and Access Rights.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a thread id to the specified handle.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetThreadId(SafeMemoryHandle hThread);
        #endregion

        #region GetWindowPlacement
        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpwndpl">A pointer to the <see cref="WindowPlacement"/> structure that receives the show state and position information. 
        /// Before calling <see cref="GetWindowPlacement"/>, set the <see cref="WindowPlacement.Length"/> member.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>. 
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement lpwndpl);
        #endregion

        #region GetWindowText
        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is a control, the text of the control is copied.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control containing the text.</param>
        /// <param name="lpString">The buffer that will receive the text. If the string is as long or longer than the buffer, the string is truncated and terminated with a null character.</param>
        /// <param name="nMaxCount">The maximum number of characters to copy to the buffer, including the null character. If the text exceeds this limit, it is truncated.</param>
        /// <returns>
        /// If the function succeeds, the return value is the length, in characters, of the copied string, not including the terminating null character. 
        /// If the window has no title bar or text, if the title bar is empty, or if the window or control handle is invalid, the return value is zero. 
        /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>. 
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        #endregion

        #region GetWindowTextLength
        /// <summary>
        /// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar). 
        /// If the specified window is a control, the function retrieves the length of the text within the control.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control.</param>
        /// <returns>
        /// If the function succeeds, the return value is the length, in characters, of the text. 
        /// Under certain conditions, this value may actually be greater than the length of the text.
        /// If the window has no text, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>. 
        /// </returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);
        #endregion

        #region GetWindowThreadProcessId
        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpdwProcessId">
        /// [Out] A pointer to a variable that receives the process identifier.
        /// If this parameter is not <c>NULL</c>, <see cref="GetWindowThreadProcessId"/> copies the identifier of the process to the variable; otherwise, it does not.
        /// </param>
        /// <returns>The return value is the identifier of the thread that created the window.</returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        #endregion

        #region EnumChildWindows
        /// <summary>
        /// Enumerates the child windows that belong to the specified parent window by passing the handle to each child window, in turn, to an application-defined callback function.
        /// EnumChildWindows continues until the last child window is enumerated or the callback function returns <c>False</c>.
        /// </summary>
        /// <param name="hwndParent">
        /// A handle to the parent window whose child windows are to be enumerated.
        /// If this parameter is <see cref="IntPtr.Zero"/>, this function is equivalent to EnumWindows.
        /// </param>
        /// <param name="lpEnumFunc">
        /// A pointer to an application-defined callback function.
        /// For more information, see <see cref="EnumWindowsProc"/>.
        /// </param>
        /// <param name="lParam">An application-defined value to be passed to the callback function.</param>
        /// <returns>The return value is not used.</returns>
        /// <remarks>If a child window has created child windows of its own, <see cref="EnumChildWindows"/> enumerates those windows as well.</remarks>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);
        #endregion

        #region FlashWindow
        /// <summary>
        /// Flashes the specified window one time. It does not change the active state of the window.
        /// To flash the window a specified number of times, use the <see cref="FlashWindowEx"/> function.
        /// </summary>
        /// <param name="hwnd">A handle to the window to be flashed. The window can be either open or minimized.</param>
        /// <param name="bInvert">
        /// If this parameter is <c>True</c>, the window is flashed from one state to the other. 
        /// If it is <c>False</c>, the window is returned to its original state (either active or inactive).
        /// When an application is minimized and this parameter is <c>True</c>, the taskbar window button flashes active/inactive. 
        /// If it is <c>False</c>, the taskbar window button flashes inactive, meaning that it does not change colors. 
        /// It flashes, as if it were being redrawn, but it does not provide the visual invert clue to the user.
        /// </param>
        /// <returns>
        /// The return value specifies the window's state before the call to the <see cref="FlashWindow"/> function. 
        /// If the window caption was drawn as active before the call, the return value is nonzero. Otherwise, the return value is zero.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
        #endregion

        #region FlashWindowEx
        /// <summary>
        /// Flashes the specified window. It does not change the active state of the window.
        /// </summary>
        /// <param name="pwfi">A pointer to a <see cref="FlashInfo"/> structure.</param>
        /// <returns>
        /// The return value specifies the window's state before the call to the <see cref="FlashWindowEx"/> function. 
        /// If the window caption was drawn as active before the call, the return value is nonzero. Otherwise, the return value is zero.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FlashInfo pwfi);
        #endregion

        #region LoadLibrary
        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="lpFileName">
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
        public static extern IntPtr LoadLibrary(string lpFileName);
        #endregion

        #region MapVirtualKey
        /// <summary>
        /// Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a virtual-key code.
        /// To specify a handle to the keyboard layout to use for translating the specified code, use the MapVirtualKeyEx function.
        /// </summary>
        /// <param name="key">
        /// The virtual key code or scan code for a key. How this value is interpreted depends on the value of the uMapType parameter.
        /// </param>
        /// <param name="translation">
        /// The translation to be performed. The value of this parameter depends on the value of the uCode parameter.
        /// </param>
        /// <returns>
        /// The return value is either a scan code, a virtual-key code, or a character value, depending on the value of uCode and uMapType. 
        /// If there is no translation, the return value is zero.
        /// </returns>
        [DllImport("user32")]
        public static extern uint MapVirtualKey(uint key, TranslationTypes translation);
        #endregion

        #region NtQueryInformationProcess
        /// <summary>
        /// Retrieves information about the specified process.
        /// </summary>
        /// <param name="processHandle">A handle to the process for which information is to be retrieved.</param>
        /// <param name="infoclass">The type of process information to be retrieved.</param>
        /// <param name="processinfo">A pointer to a buffer supplied by the calling application into which the function writes the requested information.</param>
        /// <param name="length">The size of the buffer pointed to by the ProcessInformation parameter, in bytes.</param>
        /// <param name="bytesread">
        /// [Optional] A pointer to a variable in which the function returns the size of the requested information.
        /// If the function was successful, this is the size of the information written to the buffer pointed to by the ProcessInformation parameter,
        /// but if the buffer was too small, this is the minimum size of buffer needed to receive the information successfully.
        /// </param>
        /// <returns>Returns an NTSTATUS success or error code. (STATUS_SUCCESS = 0x0).</returns>
        [DllImport("ntdll.dll")]
        public static extern int NtQueryInformationProcess(SafeMemoryHandle processHandle, ProcessInformationClass infoclass, 
            ref ProcessBasicInformation processinfo, int length, IntPtr bytesread);
        #endregion

        #region NtQueryInformationThread
        /// <summary>
        /// Retrieves information about the specified thread.
        /// </summary>
        /// <param name="hwnd">A handle to the thread about which information is being requested.</param>
        /// <param name="infoclass">
        /// Usually equals to 0 to dump all the structure correctly.
        /// 
        /// If this parameter is the ThreadIsIoPending value of the THREADINFOCLASS enumeration, the function determines whether the thread has any I/O operations pending.
        /// If this parameter is the ThreadQuerySetWin32StartAddress value of the THREADINFOCLASS enumeration, the function returns the start address of the thread. 
        /// Note that on versions of Windows prior to Windows Vista, the returned start address is only reliable before the thread starts running.
        /// </param>
        /// <param name="threadinfo">
        /// A pointer to a buffer in which the function writes the requested information. 
        /// If ThreadIsIoPending is specified for the ThreadInformationClass parameter, this buffer must be large enough to hold a ULONG value, 
        /// which indicates whether the specified thread has I/O requests pending. 
        /// If this value is equal to zero, then there are no I/O operations pending; otherwise, if the value is nonzero, then the thread does have I/O operations pending.
        /// 
        /// If ThreadQuerySetWin32StartAddress is specified for the ThreadInformationClass parameter, 
        /// this buffer must be large enough to hold a PVOID value, which is the start address of the thread.
        /// </param>
        /// <param name="length">The size of the buffer pointed to by the ThreadInformation parameter, in bytes.</param>
        /// <param name="bytesread">
        /// [Optional] A pointer to a variable in which the function returns the size of the requested information. 
        /// If the function was successful, this is the size of the information written to the buffer pointed to by the ThreadInformation parameter, 
        /// but if the buffer was too small, this is the minimum size of buffer required to receive the information successfully.
        /// </param>
        /// <returns>Returns an NTSTATUS success or error code. (STATUS_SUCCESS = 0x0).</returns>
        [DllImport("ntdll.dll")]
        public static extern uint NtQueryInformationThread(SafeMemoryHandle hwnd, uint infoclass, ref ThreadBasicInformation threadinfo, int length, IntPtr bytesread); 
        #endregion

        #region OpenProcess
        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="dwDesiredAccess">
        /// [Flags] he access to the process object. This access right is checked against the security descriptor for the process. This parameter can be one or more of the process access rights. 
        /// If the caller has enabled the SeDebugPrivilege privilege, the requested access is granted regardless of the contents of the security descriptor.
        /// </param>
        /// <param name="bInheritHandle">If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
        /// <param name="dwProcessId">The identifier of the local process to be opened.</param>
        /// <returns>
        /// If the function succeeds, the return value is an open handle to the specified process. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeMemoryHandle OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);
        #endregion

        #region OpenThread
        /// <summary>
        /// Opens an existing thread object.
        /// </summary>
        /// <param name="dwDesiredAccess">
        /// The access to the thread object. This access right is checked against the security descriptor for the thread. This parameter can be one or more of the thread access rights. 
        /// If the caller has enabled the SeDebugPrivilege privilege, the requested access is granted regardless of the contents of the security descriptor.
        /// </param>
        /// <param name="bInheritHandle">If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
        /// <param name="dwThreadId">The identifier of the thread to be opened.</param>
        /// <returns>
        /// If the function succeeds, the return value is an open handle to the specified thread. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeMemoryHandle OpenThread(ThreadAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwThreadId);
        #endregion

        #region PostMessage
        /// <summary>
        /// Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.
        /// To post a message in the message queue associated with a thread, use the PostThreadMessage function.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure is to receive the message. The following values have special meanings.</param>
        /// <param name="msg">The message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. 
        /// To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>. <see cref="Marshal.GetLastWin32Error"/> returns ERROR_NOT_ENOUGH_QUOTA when the limit is hit. 
        /// </returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, UIntPtr wParam, UIntPtr lParam);
        #endregion

        #region ReadProcessMemory
        /// <summary>
        /// Reads data from an area of memory in a specified process. The entire area to be read must be accessible or the operation fails.
        /// </summary>
        /// <param name="hProcess">A handle to the process with memory that is being read. The handle must have PROCESS_VM_READ access to the process.</param>
        /// <param name="lpBaseAddress">
        /// A pointer to the base address in the specified process from which to read. Before any data transfer occurs, 
        /// the system verifies that all data in the base address and memory of the specified size is accessible for read access, 
        /// and if it is not accessible the function fails.
        /// </param>
        /// <param name="lpBuffer">[Out] A pointer to a buffer that receives the contents from the address space of the specified process.</param>
        /// <param name="dwSize">The number of bytes to be read from the specified process.</param>
        /// <param name="lpNumberOfBytesRead">
        /// [Out] A pointer to a variable that receives the number of bytes transferred into the specified buffer. If lpNumberOfBytesRead is NULL, the parameter is ignored.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory(SafeMemoryHandle hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);
        #endregion

        #region ResumeThread
        /// <summary>
        /// Decrements a thread's suspend count. When the suspend count is decremented to zero, the execution of the thread is resumed.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread to be restarted. 
        /// This handle must have the <see cref="ThreadAccessFlags.SuspendResume"/> access right. For more information, see Thread Security and Access Rights.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the thread's previous suspend count. 
        /// If the function fails, the return value is (DWORD) -1. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint ResumeThread(SafeMemoryHandle hThread);
        #endregion

        #region SendInput
        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="nInputs">The number of structures in the pInputs array.</param>
        /// <param name="pInputs">
        /// An array of <see cref="Input"/> structures. Each structure represents an event to be inserted into the keyboard or mouse input stream.
        /// </param>
        /// <param name="cbSize">
        /// The size, in bytes, of an <see cref="Input"/> structure. If <see cref="cbSize"/> is not the size of an <see cref="Input"/> structure, the function fails.
        /// </param>
        /// <returns>
        /// The function returns the number of events that it successfully inserted into the keyboard or mouse input stream.
        /// If the function returns zero, the input was already blocked by another thread. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// This function fails when it is blocked by UIPI.
        /// Note that neither <see cref="Marshal.GetLastWin32Error"/> nor the return value will indicate the failure was caused by UIPI blocking.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public extern static int SendInput(int nInputs, Input[] pInputs, int cbSize);
        #endregion

        #region SendMessage
        /// <summary>
        /// Sends the specified message to a window or windows.
        /// The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message.</param>
        /// <param name="msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);
        #endregion

        #region SetForegroundWindow
        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window. 
        /// Keyboard input is directed to the window, and various visual cues are changed for the user. 
        /// The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads. 
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <returns>
        /// If the window was brought to the foreground, the return value is nonzero.
        /// If the window was not brought to the foreground, the return value is zero.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region SetThreadContext
        /// <summary>
        /// Sets the context for the specified thread. A 64-bit application can set the context of a WOW64 thread using the Wow64SetThreadContext function.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread whose context is to be set. The handle must have the <see cref="ThreadAccessFlags.SetContext"/> access right to the thread. 
        /// For more information, see Thread Security and Access Rights.</param>
        /// <param name="lpContext">
        /// A pointer to a <see cref="ThreadContext"/> structure that contains the context to be set in the specified thread. 
        /// The value of the ContextFlags member of this structure specifies which portions of a thread's context to set. 
        /// Some values in the <see cref="ThreadContext"/> structure that cannot be specified are silently set to the correct value. 
        /// This includes bits in the CPU status register that specify the privileged processor mode, global enabling bits in the debugging register, 
        /// and other states that must be controlled by the operating system.
        /// </param>
        /// <returns>
        /// If the context was set, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetThreadContext(SafeMemoryHandle hThread, [MarshalAs(UnmanagedType.Struct)] ref ThreadContext lpContext);

        #endregion

        #region SetWindowPlacement
        /// <summary>
        /// Sets the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpwndpl">A pointer to the <see cref="WindowPlacement"/> structure that specifies the new show state and window positions.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>. 
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WindowPlacement lpwndpl);
        #endregion

        #region SetWindowText
        /// <summary>
        /// Changes the text of the specified window's title bar (if it has one). If the specified window is a control, the text of the control is changed.
        /// </summary>
        /// <param name="hwnd">A handle to the window or control whose text is to be changed.</param>
        /// <param name="lpString">The new title or control text.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>. 
        /// </returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, string lpString);
        #endregion

        #region ShowWindow
        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nCmdShow">
        /// Controls how the window is to be shown. 
        /// This parameter is ignored the first time an application calls ShowWindow, if the program that launched the application provides a STARTUPINFO structure. 
        /// Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter. 
        /// In subsequent calls, this parameter can be one of the following values.
        /// </param>
        /// <returns>
        /// If the window was previously visible, the return value is nonzero. 
        /// If the window was previously hidden, the return value is zero. 
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, WindowStates nCmdShow);
        #endregion

        #region SuspendThread
        /// <summary>
        /// Suspends the specified thread. A 64-bit application can suspend a WOW64 thread using the Wow64SuspendThread function.
        /// </summary>
        /// <param name="hThread">A handle to the thread that is to be suspended. 
        /// The handle must have the THREAD_SUSPEND_RESUME access right. For more information, see Thread Security and Access Rights.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the thread's previous suspend count; otherwise, it is (DWORD) -1. 
        /// To get extended error information, use <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint SuspendThread(SafeMemoryHandle hThread);
        #endregion

        #region TerminateThread
        /// <summary>
        /// Terminates a thread.
        /// </summary>
        /// <param name="hThread">
        /// A handle to the thread to be terminated. 
        /// The handle must have the <see cref="ThreadAccessFlags.Terminate"/> access right. For more information, see Thread Security and Access Rights.
        /// </param>
        /// <param name="dwExitCode">The exit code for the thread. Use the GetExitCodeThread function to retrieve a thread's exit value.</param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool TerminateThread(SafeMemoryHandle hThread, int dwExitCode);
        #endregion

        #region VirtualAllocEx
        /// <summary>
        /// Reserves or commits a region of memory within the virtual address space of a specified process. The function initializes the memory it allocates to zero, unless MEM_RESET is used.
        /// To specify the NUMA node for the physical memory, see VirtualAllocExNuma.
        /// </summary>
        /// <param name="hProcess">
        /// The handle to a process. The function allocates memory within the virtual address space of this process. 
        /// The handle must have the PROCESS_VM_OPERATION access right. For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="lpAddress">
        /// The pointer that specifies a desired starting address for the region of pages that you want to allocate. 
        /// If you are reserving memory, the function rounds this address down to the nearest multiple of the allocation granularity. 
        /// If you are committing memory that is already reserved, the function rounds this address down to the nearest page boundary. 
        /// To determine the size of a page and the allocation granularity on the host computer, use the GetSystemInfo function.
        /// </param>
        /// <param name="dwSize">
        /// The size of the region of memory to allocate, in bytes. 
        /// If lpAddress is NULL, the function rounds dwSize up to the next page boundary. 
        /// If lpAddress is not NULL, the function allocates all pages that contain one or more bytes in the range from lpAddress to lpAddress+dwSize. 
        /// This means, for example, that a 2-byte range that straddles a page boundary causes the function to allocate both pages.
        /// </param>
        /// <param name="flAllocationType">[Flags] The type of memory allocation.</param>
        /// <param name="flProtect">[Flags] The memory protection for the region of pages to be allocated.</param>
        /// <returns>
        /// If the function succeeds, the return value is the base address of the allocated region of pages. 
        /// If the function fails, the return value is NULL. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(SafeMemoryHandle hProcess, IntPtr lpAddress, int dwSize, MemoryAllocationFlags flAllocationType, MemoryProtectionFlags flProtect);
        #endregion

        #region VirtualFreeEx
        /// <summary>
        /// Releases, decommits, or releases and decommits a region of memory within the virtual address space of a specified process.
        /// </summary>
        /// <param name="hProcess">A handle to a process. The function frees memory within the virtual address space of the process. 
        /// The handle must have the PROCESS_VM_OPERATION access right. For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="lpAddress">
        /// A pointer to the starting address of the region of memory to be freed. 
        /// If the dwFreeType parameter is MEM_RELEASE, lpAddress must be the base address returned by the <see cref="VirtualAllocEx"/> function when the region is reserved.
        /// </param>
        /// <param name="dwSize">
        /// The size of the region of memory to free, in bytes. 
        /// If the dwFreeType parameter is MEM_RELEASE, dwSize must be 0 (zero). 
        /// The function frees the entire region that is reserved in the initial allocation call to <see cref="VirtualAllocEx"/>. 
        /// If dwFreeType is MEM_DECOMMIT, the function decommits all memory pages that contain one or more bytes in the range from the lpAddress parameter to (lpAddress+dwSize). 
        /// This means, for example, that a 2-byte region of memory that straddles a page boundary causes both pages to be decommitted. 
        /// If lpAddress is the base address returned by VirtualAllocEx and dwSize is 0 (zero), the function decommits the entire region that is allocated by <see cref="VirtualAllocEx"/>. 
        /// After that, the entire region is in the reserved state.
        /// </param>
        /// <param name="dwFreeType">[Flags] The type of free operation.</param>
        /// <returns>
        /// If the function succeeds, the return value is a nonzero value. 
        /// If the function fails, the return value is 0 (zero). To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool VirtualFreeEx(SafeMemoryHandle hProcess, IntPtr lpAddress, int dwSize, MemoryReleaseFlags dwFreeType);
        #endregion

        #region VirtualProtectEx
        /// <summary>
        /// Changes the protection on a region of committed pages in the virtual address space of a specified process.
        /// </summary>
        /// <param name="hProcess">
        /// A handle to the process whose memory protection is to be changed. The handle must have the PROCESS_VM_OPERATION access right. 
        /// For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="lpAddress">
        /// A pointer to the base address of the region of pages whose access protection attributes are to be changed. 
        /// All pages in the specified region must be within the same reserved region allocated when calling the VirtualAlloc or VirtualAllocEx function using MEM_RESERVE. 
        /// The pages cannot span adjacent reserved regions that were allocated by separate calls to VirtualAlloc or <see cref="VirtualAllocEx"/> using MEM_RESERVE.
        /// </param>
        /// <param name="dwSize">
        /// The size of the region whose access protection attributes are changed, in bytes. 
        /// The region of affected pages includes all pages containing one or more bytes in the range from the lpAddress parameter to (lpAddress+dwSize). 
        /// This means that a 2-byte range straddling a page boundary causes the protection attributes of both pages to be changed.
        /// </param>
        /// <param name="flNewProtect">
        /// The memory protection option. This parameter can be one of the memory protection constants. 
        /// For mapped views, this value must be compatible with the access protection specified when the view was mapped (see MapViewOfFile, MapViewOfFileEx, and MapViewOfFileExNuma).
        /// </param>
        /// <param name="lpflOldProtect">
        /// A pointer to a variable that receives the previous access protection of the first page in the specified region of pages. 
        /// If this parameter is NULL or does not point to a valid variable, the function fails.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(SafeMemoryHandle hProcess, IntPtr lpAddress, int dwSize, MemoryProtectionFlags flNewProtect, out MemoryProtectionFlags lpflOldProtect);
        #endregion

        #region VirtualQueryEx
        /// <summary>
        /// Retrieves information about a range of pages within the virtual address space of a specified process.
        /// </summary>
        /// <param name="hProcess">
        /// A handle to the process whose memory information is queried. 
        /// The handle must have been opened with the PROCESS_QUERY_INFORMATION access right, which enables using the handle to read information from the process object. 
        /// For more information, see Process Security and Access Rights.
        /// </param>
        /// <param name="lpAddress">
        /// A pointer to the base address of the region of pages to be queried. 
        /// This value is rounded down to the next page boundary. 
        /// To determine the size of a page on the host computer, use the GetSystemInfo function. 
        /// If lpAddress specifies an address above the highest memory address accessible to the process, the function fails with ERROR_INVALID_PARAMETER.
        /// </param>
        /// <param name="lpBuffer">[Out] A pointer to a <see cref="MemoryBasicInformation"/> structure in which information about the specified page range is returned.</param>
        /// <param name="dwLength">The size of the buffer pointed to by the lpBuffer parameter, in bytes.</param>
        /// <returns>
        /// The return value is the actual number of bytes returned in the information buffer. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int VirtualQueryEx(SafeMemoryHandle hProcess, IntPtr lpAddress, out MemoryBasicInformation lpBuffer, int dwLength);
        #endregion

        #region WaitForSingleObject
        /// <summary>
        /// Waits until the specified object is in the signaled state or the time-out interval elapses.
        /// To enter an alertable wait state, use the WaitForSingleObjectEx function. To wait for multiple objects, use the WaitForMultipleObjects.
        /// </summary>
        /// <param name="hHandle">
        /// A handle to the object. For a list of the object types whose handles can be specified, see the following Remarks section.
        /// If this handle is closed while the wait is still pending, the function's behavior is undefined.
        /// The handle must have the SYNCHRONIZE access right. For more information, see Standard Access Rights.
        /// </param>
        /// <param name="dwMilliseconds">
        /// The time-out interval, in milliseconds. If a nonzero value is specified, the function waits until the object is signaled or the interval elapses. 
        /// If dwMilliseconds is zero, the function does not enter a wait state if the object is not signaled; it always returns immediately. 
        /// If dwMilliseconds is INFINITE, the function will return only when the object is signaled.
        /// </param>
        /// <returns>If the function succeeds, the return value indicates the event that caused the function to return.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern WaitValues WaitForSingleObject(SafeMemoryHandle hHandle, uint dwMilliseconds);
        #endregion

        #region WriteProcessMemory
        /// <summary>
        /// Writes data to an area of memory in a specified process. The entire area to be written to must be accessible or the operation fails.
        /// </summary>
        /// <param name="hProcess">A handle to the process memory to be modified. The handle must have PROCESS_VM_WRITE and PROCESS_VM_OPERATION access to the process.</param>
        /// <param name="lpBaseAddress">
        /// A pointer to the base address in the specified process to which data is written. Before data transfer occurs, the system verifies that 
        /// all data in the base address and memory of the specified size is accessible for write access, and if it is not accessible, the function fails.
        /// </param>
        /// <param name="lpBuffer">A pointer to the buffer that contains data to be written in the address space of the specified process.</param>
        /// <param name="nSize">The number of bytes to be written to the specified process.</param>
        /// <param name="lpNumberOfBytesWritten">
        /// A pointer to a variable that receives the number of bytes transferred into the specified process. 
        /// This parameter is optional. If lpNumberOfBytesWritten is NULL, the parameter is ignored.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. 
        /// If the function fails, the return value is zero. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.
        /// </returns>
        [DllImport("kernel32.dll",SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WriteProcessMemory(SafeMemoryHandle hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);
        #endregion
    }

    #region Delegate EnumWindowsProc
    /// <summary>
    /// An application-defined callback function used with the EnumWindows or <c>EnumDesktopWindows</c> function. It receives top-level window handles. 
    /// The <c>WNDENUMPROC</c> type defines a pointer to this callback function. <see cref="EnumWindowsProc"/> is a placeholder for the application-defined function name. 
    /// </summary>
    /// <param name="hWnd">A handle to a top-level window.</param>
    /// <param name="lParam">The application-defined value given in EnumWindows or EnumDesktopWindows.</param>
    /// <returns>To continue enumeration, the callback function must return <c>True</c>; to stop enumeration, it must return <c>False</c>.</returns>
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    #endregion
}
