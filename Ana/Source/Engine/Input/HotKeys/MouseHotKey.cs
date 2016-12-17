﻿namespace Ana.Source.Engine.Input.HotKeys
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