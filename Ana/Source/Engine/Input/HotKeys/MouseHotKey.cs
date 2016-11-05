namespace Ana.Source.Engine.Input.HotKeys
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// A mouse hotkey, which is activated by a given set of input
    /// </summary>
    [DataContract]
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    internal class MouseHotKey : IHotkey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseHotKey" /> class
        /// </summary>
        public MouseHotKey()
        {
            this.ActivationMouseButtons = new HashSet<Byte>();
        }

        [DataMember]
        public HashSet<Byte> ActivationMouseButtons { get; set; }

        public void SetActivationKeys(IEnumerable<Byte> activationMouseButtons)
        {
            this.ActivationMouseButtons = new HashSet<Byte>(activationMouseButtons);
        }

        public HashSet<Byte> GetActivationMouseButtons()
        {
            return this.ActivationMouseButtons;
        }

        public override String ToString()
        {
            return base.ToString();
        }
    }
    //// End class
}
//// End namespace