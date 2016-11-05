namespace Ana.Source.Engine.Input.HotKeys
{
    using SharpDX.DirectInput;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// A keyboard hotkey, which is activated by a given set of input
    /// </summary>
    [DataContract]
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
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

        [DataMember]
        public HashSet<Key> ActivationKeys { get; set; }

        public void SetActivationKeys(IEnumerable<Key> activationKeys)
        {
            this.ActivationKeys = new HashSet<Key>(activationKeys);
        }

        public HashSet<Key> GetActivationKeys()
        {
            return this.ActivationKeys;
        }

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