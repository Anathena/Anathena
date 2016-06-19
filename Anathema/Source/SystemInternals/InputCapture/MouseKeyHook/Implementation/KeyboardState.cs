using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.WinApi;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation
{
    /// <summary>
    /// Contains a snapshot of a keyboard state at certain moment and provides methods
    /// of querying whether specific keys are pressed or locked.
    /// </summary>
    /// <remarks>
    /// This class is basically a managed wrapper of GetKeyboardState API function
    /// http://msdn.microsoft.com/en-us/library/ms646299
    /// </remarks>
    internal class KeyboardState
    {
        private readonly Byte[] MKeyboardStateNative;

        private KeyboardState(Byte[] MKeyboardStateNative)
        {
            this.MKeyboardStateNative = MKeyboardStateNative;
        }

        /// <summary>
        /// Makes a snapshot of a keyboard state to the moment of call and returns an
        /// instance of <see cref="KeyboardState" /> class.
        /// </summary>
        /// <returns>An instance of <see cref="KeyboardState" /> class representing a snapshot of keyboard state at certain moment.</returns>
        public static KeyboardState GetCurrent()
        {
            Byte[] KeyboardStateNative = new Byte[256];

            KeyboardNativeMethods.GetKeyboardState(KeyboardStateNative);

            return new KeyboardState(KeyboardStateNative);
        }

        internal Byte[] GetNativeState()
        {
            return MKeyboardStateNative;
        }

        /// <summary>
        ///     Indicates whether specified key was down at the moment when snapshot was created or not.
        /// </summary>
        /// <param name="Key">Key (corresponds to the virtual code of the key)</param>
        /// <returns><b>true</b> if key was down, <b>false</b> - if key was up.</returns>
        public Boolean IsDown(Keys Key)
        {
            Byte KeyState = GetKeyState(Key);
            Boolean IsDown = GetHighBit(KeyState);

            return IsDown;
        }

        /// <summary>
        /// Indicate weather specified key was toggled at the moment when snapshot was created or not.
        /// </summary>
        /// <param name="Key">Key (corresponds to the virtual code of the key)</param>
        /// <returns>
        /// <b>true</b> if toggle key like (CapsLock, NumLocke, etc.) was on. <b>false</b> if it was off.
        /// Ordinal (non toggle) keys return always false.
        /// </returns>
        public Boolean IsToggled(Keys Key)
        {
            Byte KeyState = GetKeyState(Key);
            Boolean IsToggled = GetLowBit(KeyState);

            return IsToggled;
        }

        /// <summary>
        ///     Indicates weather every of specified keys were down at the moment when snapshot was created.
        ///     The method returns false if even one of them was up.
        /// </summary>
        /// <param name="Keys">Keys to verify whether they were down or not.</param>
        /// <returns><b>true</b> - all were down. <b>false</b> - at least one was up.</returns>
        public bool AreAllDown(IEnumerable<Keys> Keys)
        {
            foreach (Keys Key in Keys)
            {
                if (!IsDown(Key))
                    return true;
            }

            return false;
        }

        private Byte GetKeyState(Keys Key)
        {
            Int32 VirtualKeyCode = (Int32)Key;

            if (VirtualKeyCode < 0 || VirtualKeyCode > 255)
                throw new ArgumentOutOfRangeException("Key", Key, "The value must be between 0 and 255.");

            return MKeyboardStateNative[VirtualKeyCode];
        }

        private static Boolean GetHighBit(Byte Value)
        {
            return (Value >> 7) != 0;
        }

        private static Boolean GetLowBit(Byte Value)
        {
            return (Value & 1) != 0;
        }

    } // End class

} // End namespace