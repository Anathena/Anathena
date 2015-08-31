/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using Binarysharp.MemoryManagement.Native;

namespace Binarysharp.MemoryManagement.Windows.Keyboard
{
    /// <summary>
    /// Class defining a virtual keyboard using the API Message.
    /// </summary>
    public class MessageKeyboard : BaseKeyboard
    {
        #region Constructor
        public MessageKeyboard(RemoteWindow window) : base(window)
        {
        }
        #endregion

        #region Overridden Methods
        #region Press
        /// <summary>
        /// Presses the specified virtual key to the window.
        /// </summary>
        /// <param name="key">The virtual key to press.</param>
        public override void Press(Keys key)
        {
            Window.PostMessage(WindowsMessages.KeyDown, new UIntPtr((uint)key), MakeKeyParameter(key, false));
        }
        #endregion
        #region Release
        /// <summary>
        /// Releases the specified virtual key to the window.
        /// </summary>
        /// <param name="key">The virtual key to release.</param>
        public override void Release(Keys key)
        {
            // Call the base function
            base.Release(key);
            Window.PostMessage(WindowsMessages.KeyUp, new UIntPtr((uint)key), MakeKeyParameter(key, true));
        }
        #endregion
        #region Write
        /// <summary>
        /// Writes the specified character to the window.
        /// </summary>
        /// <param name="character">The character to write.</param>
        public override void Write(char character)
        {
            Window.PostMessage(WindowsMessages.Char, new UIntPtr(character), UIntPtr.Zero);
        }
        #endregion
        #endregion

        #region MakeKeyParameter (private)
        /// <summary>
        /// Makes the lParam for a key depending on several settings.
        /// </summary>
        /// <param name="key">
        /// [16-23 bits] The virtual key.
        /// </param>
        /// <param name="keyUp">
        /// [31 bit] The transition state.
        /// The value is always 0 for a <see cref="WindowsMessages.KeyDown"/> message.
        /// The value is always 1 for a <see cref="WindowsMessages.KeyUp"/> message.
        /// </param>
        /// <param name="fRepeat">
        /// [30 bit] The previous key state.
        /// The value is 1 if the key is down before the message is sent, or it is zero if the key is up.
        /// The value is always 1 for a <see cref="WindowsMessages.KeyUp"/> message.
        /// </param>
        /// <param name="cRepeat">
        /// [0-15 bits] The repeat count for the current message. 
        /// The value is the number of times the keystroke is autorepeated as a result of the user holding down the key.
        /// If the keystroke is held long enough, multiple messages are sent. However, the repeat count is not cumulative.
        /// The repeat count is always 1 for a <see cref="WindowsMessages.KeyUp"/> message.
        /// </param>
        /// <param name="altDown">
        /// [29 bit] The context code.
        /// The value is always 0 for a <see cref="WindowsMessages.KeyDown"/> message.
        /// The value is always 0 for a <see cref="WindowsMessages.KeyUp"/> message.</param>
        /// <param name="fExtended">
        /// [24 bit] Indicates whether the key is an extended key, such as the right-hand ALT and CTRL keys that appear on 
        /// an enhanced 101- or 102-key keyboard. The value is 1 if it is an extended key; otherwise, it is 0.
        /// </param>
        /// <returns>The return value is the lParam when posting or sending a message regarding key press.</returns>
        /// <remarks>
        /// KeyDown resources: http://msdn.microsoft.com/en-us/library/windows/desktop/ms646280%28v=vs.85%29.aspx
        /// KeyUp resources:  http://msdn.microsoft.com/en-us/library/windows/desktop/ms646281%28v=vs.85%29.aspx
        /// </remarks>
        private UIntPtr MakeKeyParameter(Keys key, bool keyUp, bool fRepeat, uint cRepeat, bool altDown, bool fExtended)
        {
            // Create the result and assign it with the repeat count
            var result = cRepeat;
            // Add the scan code with a left shift operation
            result |= WindowCore.MapVirtualKey(key, TranslationTypes.VirtualKeyToScanCode) << 16;
            // Does we need to set the extended flag ?
            if (fExtended)
                result |= 0x1000000;
            // Does we need to set the alt flag ?
            if (altDown)
                result |= 0x20000000;
            // Does we need to set the repeat flag ?
            if (fRepeat)
                result |= 0x40000000;
            // Does we need to set the keyUp flag ?
            if (keyUp)
                result |= 0x80000000;

            return new UIntPtr(result);
        }
        /// <summary>
        /// Makes the lParam for a key depending on several settings.
        /// </summary>
        /// <param name="key">The virtual key.</param>
        /// <param name="keyUp">
        /// The transition state.
        /// The value is always 0 for a <see cref="WindowsMessages.KeyDown"/> message.
        /// The value is always 1 for a <see cref="WindowsMessages.KeyUp"/> message.
        /// </param>
        /// <returns>The return value is the lParam when posting or sending a message regarding key press.</returns>
        private UIntPtr MakeKeyParameter(Keys key, bool keyUp)
        {
            return MakeKeyParameter(key, keyUp, keyUp, 1, false, false);
        }
        #endregion
    }
}
