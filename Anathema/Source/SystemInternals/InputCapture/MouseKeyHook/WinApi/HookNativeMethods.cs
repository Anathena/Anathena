using System;
using System.Runtime.InteropServices;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.WinApi
{
    internal static class HookNativeMethods
    {
        /// <summary>
        /// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain.
        /// A hook procedure can call this function either before or after processing the hook information.
        /// </summary>
        /// <param name="IdHook">This parameter is ignored.</param>
        /// <param name="NCode">[in] Specifies the hook code passed to the current hook procedure.</param>
        /// <param name="WParam">[in] Specifies the wParam value passed to the current hook procedure.</param>
        /// <param name="LParam">[in] Specifies the lParam value passed to the current hook procedure.</param>
        /// <returns>This value is returned by the next hook procedure in the chain.</returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr CallNextHookEx(IntPtr IdHook, Int32 NCode, IntPtr WParam, IntPtr LParam);

        /// <summary>
        /// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
        /// You would install a hook procedure to monitor the system for certain types of events. These events
        /// are associated either with a specific thread or with all threads in the same desktop as the calling thread.
        /// </summary>
        /// <param name="IdHook">
        /// [in] Specifies the type of hook procedure to be installed. This parameter can be one of the following values.
        /// </param>
        /// <param name="LPFN">
        /// [in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a
        /// thread created by a different process, the lpfn parameter must point to a hook procedure in a dynamic-link
        /// library (DLL). Otherwise, lpfn can point to a hook procedure in the code associated with the current process.
        /// </param>
        /// <param name="HMod">
        /// [in] Handle to the DLL containing the hook procedure pointed to by the lpfn parameter.
        /// The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by
        /// the current process and if the hook procedure is within the code associated with the current process.
        /// </param>
        /// <param name="DWThreadId">
        /// [in] Specifies the identifier of the thread with which the hook procedure is to be associated.
        /// If this parameter is zero, the hook procedure is associated with all existing threads running in the
        /// same desktop as the calling thread.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the handle to the hook procedure.
        /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern HookProcedureHandle SetWindowsHookEx(Int32 IdHook, HookProcedure LPFN, IntPtr HMod, Int32 DWThreadId);

        /// <summary>
        /// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="IdHook">
        /// [in] Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to SetWindowsHookEx.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern Int32 UnhookWindowsHookEx(IntPtr IdHook);

    } // End class

} // End namespace