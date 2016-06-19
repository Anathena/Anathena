using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.WinApi;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook
{
    /// <summary>
    /// Provides extended argument data for the <see cref='KeyListener.KeyDown' /> or
    /// <see cref='KeyListener.KeyUp' /> event.
    /// </summary>
    public class KeyEventArgsExt : KeyEventArgs
    {
        const UInt32 MaskKeydown = 0x40000000;  // Bit 30
        const UInt32 MaskKeyup = 0x80000000;    // Bit 31

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgsExt" /> class.
        /// </summary>
        /// <param name="KeyData"></param>
        public KeyEventArgsExt(Keys KeyData) : base(KeyData) { }

        internal KeyEventArgsExt(Keys KeyData, Int32 Timestamp, Boolean IsKeyDown, Boolean IsKeyUp) : this(KeyData)
        {
            this.Timestamp = Timestamp;
            this.IsKeyDown = IsKeyDown;
            this.IsKeyUp = IsKeyUp;
        }

        /// <summary>
        /// The system tick count of when the event occurred.
        /// </summary>
        public int Timestamp { get; private set; }

        /// <summary>
        ///  True if event signals key down..
        /// </summary>
        public Boolean IsKeyDown { get; private set; }

        /// <summary>
        /// True if event signals key up.
        /// </summary>
        public Boolean IsKeyUp { get; private set; }

        internal static KeyEventArgsExt FromRawDataApp(CallbackData Data)
        {
            IntPtr WParam = Data.WParam;
            IntPtr LParam = Data.LParam;

            Int32 Timestamp = Environment.TickCount;
            UInt32 Flags = (UInt32)LParam.ToInt64();

            // Bit 30 Specifies the previous key state. The value is 1 if the key is down before the message is sent; it is 0 if the key is up.
            Boolean WasKeyDown = (Flags & MaskKeydown) > 0;

            // Bit 31 Specifies the transition state. The value is 0 if the key is being pressed and 1 if it is being released.
            Boolean IsKeyReleased = (Flags & MaskKeyup) > 0;

            Keys KeyData = AppendModifierStates((Keys)WParam);

            Boolean IsKeyDown = !WasKeyDown && !IsKeyReleased;
            Boolean IsKeyUp = WasKeyDown && IsKeyReleased;

            return new KeyEventArgsExt(KeyData, Timestamp, IsKeyDown, IsKeyUp);
        }

        internal static KeyEventArgsExt FromRawDataGlobal(CallbackData data)
        {
            IntPtr WParam = data.WParam;
            IntPtr LParam = data.LParam;
            KeyboardHookStruct KeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(LParam, typeof(KeyboardHookStruct));

            Keys KeyData = AppendModifierStates((Keys)KeyboardHookStruct.VirtualKeyCode);

            Int32 KeyCode = WParam.ToInt32();

            Boolean IsKeyDown = (KeyCode == Messages.WM_KEYDOWN || KeyCode == Messages.WM_SYSKEYDOWN);
            Boolean IsKeyUp = (KeyCode == Messages.WM_KEYUP || KeyCode == Messages.WM_SYSKEYUP);

            return new KeyEventArgsExt(KeyData, KeyboardHookStruct.Time, IsKeyDown, IsKeyUp);
        }

        // # It is not possible to distinguish Keys.LControlKey and Keys.RControlKey when they are modifiers
        // Check for Keys.Control instead
        // Same for Shift and Alt(Menu)
        // See more at http://www.tech-archive.net/Archive/DotNet/microsoft.public.dotnet.framework.windowsforms/2008-04/msg00127.html #

        // A shortcut to make life easier
        private static Boolean CheckModifier(Int32 VKey)
        {
            return (KeyboardNativeMethods.GetKeyState(VKey) & 0x8000) > 0;
        }

        private static Keys AppendModifierStates(Keys KeyData)
        {
            Boolean Control = CheckModifier(KeyboardNativeMethods.VK_CONTROL);
            Boolean Shift = CheckModifier(KeyboardNativeMethods.VK_SHIFT);
            Boolean Alt = CheckModifier(KeyboardNativeMethods.VK_MENU);

            // Windows keys
            // # combine LWin and RWin key with other keys will potentially corrupt the data
            // notable F5 | Keys.LWin == F12, see https://globalmousekeyhook.codeplex.com/workitem/1188
            // and the KeyEventArgs.KeyData don't recognize combined data either

            // Function (Fn) key
            // # CANNOT determine state due to conversion inside keyboard
            // See http://en.wikipedia.org/wiki/Fn_key#Technical_details #

            return KeyData | (Control ? Keys.Control : Keys.None) | (Shift ? Keys.Shift : Keys.None) | (Alt ? Keys.Alt : Keys.None);
        }

    } // End class

} // End namespace