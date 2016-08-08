using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathena.Source.Engine.InputCapture.HotKeys
{
    [DataContract()]
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public class MouseHotKey : IHotKey
    {
        [DataMember()]
        public HashSet<Byte> ActivationMouseButtons;

        public MouseHotKey()
        {
            ActivationMouseButtons = new HashSet<Byte>();
        }

        public void SetActivationKeys(IEnumerable<Byte> ActivationMouseButtons)
        {
            this.ActivationMouseButtons = new HashSet<Byte>(ActivationMouseButtons);
        }

        public HashSet<Byte> GetActivationMouseButtons()
        {
            return ActivationMouseButtons;
        }

        public override String ToString()
        {
            return base.ToString();
        }

    } // End class

} // End namespace