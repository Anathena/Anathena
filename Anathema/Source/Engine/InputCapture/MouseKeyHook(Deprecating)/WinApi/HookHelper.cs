using Anathema.Source.Engine.InputCapture.MouseKeyHook.Implementation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi
{
    internal static class HookHelper
    {
        public static HookResult HookAppMouse(Callback Callback)
        {
            return HookApp(HookIds.WH_MOUSE, Callback);
        }

        public static HookResult HookAppKeyboard(Callback Callback)
        {
            return HookApp(HookIds.WH_KEYBOARD, Callback);
        }

        public static HookResult HookGlobalMouse(Callback Callback)
        {
            return HookGlobal(HookIds.WH_MOUSE_LL, Callback);
        }

        public static HookResult HookGlobalKeyboard(Callback Callback)
        {
            return HookGlobal(HookIds.WH_KEYBOARD_LL, Callback);
        }

        private static HookResult HookApp(Int32 HookId, Callback Callback)
        {
            HookProcedure HookProc = (Code, WParam, LParam) => HookProcedure(Code, WParam, LParam, Callback);

            HookProcedureHandle HookHandle = HookNativeMethods.SetWindowsHookEx(HookId, HookProc, IntPtr.Zero,
                ThreadNativeMethods.GetCurrentThreadId());

            if (HookHandle.IsInvalid)
                ThrowLastUnmanagedErrorAsException();

            return new HookResult(HookHandle, HookProc);
        }

        private static HookResult HookGlobal(Int32 HookId, Callback Callback)
        {
            HookProcedure HookProc = (Code, WParam, LParam) => HookProcedure(Code, WParam, LParam, Callback);

            HookProcedureHandle HookHandle = HookNativeMethods.SetWindowsHookEx(HookId, HookProc,
                Process.GetCurrentProcess().MainModule.BaseAddress, 0);

            if (HookHandle.IsInvalid)
                ThrowLastUnmanagedErrorAsException();

            return new HookResult(HookHandle, HookProc);
        }

        private static IntPtr HookProcedure(Int32 NCode, IntPtr WParam, IntPtr LParam, Callback Callback)
        {
            // Pass through
            if (NCode != 0)
                return CallNextHookEx(NCode, WParam, LParam);

            CallbackData CallbackData = new CallbackData(WParam, LParam);
            Boolean ContinueProcessing = Callback(CallbackData);

            if (!ContinueProcessing)
                return new IntPtr(-1);

            return CallNextHookEx(NCode, WParam, LParam);
        }

        private static IntPtr CallNextHookEx(Int32 NCode, IntPtr WParam, IntPtr LParam)
        {
            return HookNativeMethods.CallNextHookEx(IntPtr.Zero, NCode, WParam, LParam);
        }

        private static void ThrowLastUnmanagedErrorAsException()
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

    } // End class

} // End namespace