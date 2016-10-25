namespace Ana.Source.Engine.InputCapture.HotKeys
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;

    [DataContract]
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    internal class MouseHotKey : IHotKey
    {
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