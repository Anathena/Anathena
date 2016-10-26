namespace Ana.Source.Engine.Input.HotKeys
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    [DataContract]
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    internal class ControllerHotKey : IHotKey
    {
        [DataMember]
        public HashSet<Int32> ActivationKeys { get; set; }

        public ControllerHotKey()
        {
            this.ActivationKeys = new HashSet<Int32>();
        }

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