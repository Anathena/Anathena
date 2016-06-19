using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.WinApi;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook
{
    /// <summary>
    /// Provides extended data for the <see cref='KeyListener.KeyPress' /> event.
    /// </summary>
    public class KeyPressEventArgsExt : KeyPressEventArgs
    {
        // http://msdn.microsoft.com/en-us/library/ms644984(v=VS.85).aspx
        const UInt32 MaskKeydown = 0x40000000;  // Bit 30
        const UInt32 MaskKeyup = 0x80000000;    // Bit 31
        const UInt32 MaskScanCode = 0xff0000;   // Bit 23-16

        const Int32 FUState = 0;

        internal KeyPressEventArgsExt(Char KeyChar, Int32 Timestamp) : base(KeyChar)
        {
            IsNonChar = KeyChar == (Char)0x0;
            this.Timestamp = Timestamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='KeyPressEventArgsExt' /> class.
        /// </summary>
        /// <param name="KeyChar">
        /// Character corresponding to the key pressed. 0 char if represents a system or functional non char key.
        /// </param>
        public KeyPressEventArgsExt(Char KeyChar) : this(KeyChar, Environment.TickCount)
        {

        }

        /// <summary>
        /// True if represents a system or functional non char key.
        /// </summary>
        public Boolean IsNonChar { get; private set; }

        /// <summary>
        /// The system tick count of when the event occurred.
        /// </summary>
        public Int32 Timestamp { get; private set; }

        internal static IEnumerable<KeyPressEventArgsExt> FromRawDataApp(CallbackData Data)
        {
            IntPtr WParam = Data.WParam;
            IntPtr LParam = Data.LParam;

            UInt32 Flags = (UInt32)LParam.ToInt64();

            // Bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
            Boolean WasKeyDown = (Flags & MaskKeydown) > 0;

            // Bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
            Boolean IsKeyReleased = (Flags & MaskKeyup) > 0;

            if (!WasKeyDown && !IsKeyReleased)
                yield break;

            Int32 VirtualKeyCode = (Int32)WParam;
            Int32 ScanCode = checked((Int32)(Flags & MaskScanCode));

            Char[] Chars;

            KeyboardNativeMethods.TryGetCharFromKeyboardState(VirtualKeyCode, ScanCode, FUState, out Chars);

            if (Chars == null)
                yield break;

            foreach (Char Char in Chars)
                yield return new KeyPressEventArgsExt(Char);
        }

        internal static IEnumerable<KeyPressEventArgsExt> FromRawDataGlobal(CallbackData Data)
        {
            IntPtr WParam = Data.WParam;
            IntPtr LParam = Data.LParam;

            if ((Int32)WParam != Messages.WM_KEYDOWN)
                yield break;

            KeyboardHookStruct KeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(LParam, typeof(KeyboardHookStruct));

            Int32 VirtualKeyCode = KeyboardHookStruct.VirtualKeyCode;
            Int32 ScanCode = KeyboardHookStruct.ScanCode;
            Int32 FUState = KeyboardHookStruct.Flags;

            if (VirtualKeyCode == KeyboardNativeMethods.VK_PACKET)
            {
                Char Char = (Char)ScanCode;

                yield return new KeyPressEventArgsExt(Char, KeyboardHookStruct.Time);
            }
            else
            {
                Char[] Chars;

                KeyboardNativeMethods.TryGetCharFromKeyboardState(VirtualKeyCode, ScanCode, FUState, out Chars);

                if (Chars == null)
                    yield break;

                foreach (Char Current in Chars)
                    yield return new KeyPressEventArgsExt(Current, KeyboardHookStruct.Time);
            }
        }

    } // End class

} // End namespace