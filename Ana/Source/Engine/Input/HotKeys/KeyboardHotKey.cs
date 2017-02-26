namespace Ana.Source.Engine.Input.HotKeys
{
    using SharpDX.DirectInput;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A keyboard hotkey, which is activated by a given set of input
    /// </summary>
    [DataContract]
    internal class KeyboardHotkey : IHotkey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardHotkey" /> class
        /// </summary>
        /// <param name="activationKeys">Initial activation keys</param>
        public KeyboardHotkey(params Key[] activationKeys)
        {
            this.ActivationKeys = new HashSet<Key>(activationKeys);
        }

        /// <summary>
        /// Gets or sets the set of inputs corresponding to this hotkey
        /// </summary>
        [DataMember]
        public HashSet<Key> ActivationKeys { get; set; }

        /// <summary>
        /// Determines if the current set of activation hotkeys are empty.
        /// </summary>
        /// <returns>True if there are hotkeys, otherwise false.</returns>
        public Boolean HasHotkey()
        {
            return this.ActivationKeys == null ? false : this.ActivationKeys.Count > 0;
        }

        /// <summary>
        /// Gets the string representation of the hotkey inputs
        /// </summary>
        /// <returns>The string representatio of hotkey inputs</returns>
        public override String ToString()
        {
            String hotKeyString = String.Empty;

            if (this.ActivationKeys == null)
            {
                return hotKeyString;
            }

            foreach (Key key in this.ActivationKeys)
            {
                hotKeyString += key.ToString() + "+";
            }

            return hotKeyString.TrimEnd('+');
        }
    }
    //// End class
}
//// End namespace