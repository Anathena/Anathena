namespace Ana.Source.Engine.Input.HotKeys
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// A controller hotkey, which is activated by a given set of input
    /// </summary>
    [DataContract]
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    internal class ControllerHotkey : IHotkey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerHotkey" /> class
        /// </summary>
        public ControllerHotkey()
        {
            this.ActivationKeys = new HashSet<Int32>();
        }

        [DataMember]
        public HashSet<Int32> ActivationKeys { get; set; }

        public void SetActivationKeys(IEnumerable<Int32> activationKeys)
        {
            this.ActivationKeys = new HashSet<Int32>(activationKeys);
        }

        public HashSet<Int32> GetActivationKeys()
        {
            return this.ActivationKeys;
        }

        public override String ToString()
        {
            return base.ToString();
        }
    }
    //// End class
}
//// End namespace