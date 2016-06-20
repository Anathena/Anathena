using Microsoft.Win32.SafeHandles;
using System;
using System.Windows.Forms;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.WinApi
{
    internal class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private static Boolean Closing;

        static HookProcedureHandle()
        {
            Application.ApplicationExit += (Sender, E) => { Closing = true; };
        }

        public HookProcedureHandle() : base(true) { }

        protected override Boolean ReleaseHandle()
        {
            // NOTE Calling Unhook during processexit causes deley
            if (Closing)
                return true;

            return HookNativeMethods.UnhookWindowsHookEx(handle) != 0;
        }

    } // End class

} // End namespace