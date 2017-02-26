namespace Ana.Source.Engine.Input.HotKeys
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A mouse hotkey, which is activated by a given set of input
    /// </summary>
    [DataContract]
    internal class MouseHotKey : IHotkey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseHotKey" /> class
        /// </summary>
        public MouseHotKey()
        {
            this.ActivationMouseButtons = new HashSet<Byte>();
        }

        /// <summary>
        /// Gets or sets the set of inputs corresponding to this hotkey
        /// </summary>
        [DataMember]
        public HashSet<Byte> ActivationMouseButtons { get; set; }

        /// <summary>
        /// Determines if the current set of activation hotkeys are empty.
        /// </summary>
        /// <returns>True if there are hotkeys, otherwise false.</returns>
        public Boolean HasHotkey()
        {
            return this.ActivationMouseButtons == null ? false : this.ActivationMouseButtons.Count > 0;
        }

        /// <summary>
        /// Clones the hotkey.
        /// </summary>
        /// <returns>A clone of the hotkey.</returns>
        public IHotkey Clone()
        {
            MouseHotKey hotkey = new MouseHotKey();
            hotkey.ActivationMouseButtons = new HashSet<Byte>(this.ActivationMouseButtons);
            return hotkey;
        }

        /// <summary>
        /// Gets the string representation of the hotkey inputs
        /// </summary>
        /// <returns>The string representatio of hotkey inputs</returns>
        public override String ToString()
        {
            return base.ToString();
        }
    }
    //// End class
}
//// End namespace