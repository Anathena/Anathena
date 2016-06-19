using System;
using System.Runtime.InteropServices;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.WinApi
{
    /// <summary>
    /// The KeyboardHookStruct structure contains information about a low-level keyboard input event.
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookstructures/cwpstruct.asp
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyboardHookStruct
    {
        /// <summary>
        /// Specifies a virtual-key code. The code must be a value in the range 1 to 254.
        /// </summary>
        public int VirtualKeyCode;

        /// <summary>
        /// Specifies a hardware scan code for the key.
        /// </summary>
        public Int32 ScanCode;

        /// <summary>
        /// Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        public Int32 Flags;

        /// <summary>
        /// Specifies the Time stamp for this message.
        /// </summary>
        public Int32 Time;

        /// <summary>
        /// Specifies extra information associated with the message.
        /// </summary>
        public Int32 ExtraInfo;

    } // End class

} // End namespace