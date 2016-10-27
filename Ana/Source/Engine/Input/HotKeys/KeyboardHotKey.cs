namespace Ana.Source.Engine.Input.HotKeys
{
    using SharpDX.DirectInput;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    [DataContract]
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    internal class KeyboardHotkey : IHotkey
    {
        public KeyboardHotkey()
        {
            this.ActivationKeys = new HashSet<Key>();
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