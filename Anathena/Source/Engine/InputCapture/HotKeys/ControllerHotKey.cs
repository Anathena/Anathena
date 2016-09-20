using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Ana.Source.Engine.InputCapture.HotKeys
{
    [DataContract()]
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public class ControllerHotKey : IHotKey
    {
        [DataMember()]
        public HashSet<Int32> ActivationKeys;

        public ControllerHotKey()
        {
            ActivationKeys = new HashSet<Int32>();
        }

        public void SetActivationKeys(IEnumerable<Int32> ActivationKeys)
        {
            this.ActivationKeys = new HashSet<Int32>(ActivationKeys);
        }

        public HashSet<Int32> GetActivationKeys()
        {
            return ActivationKeys;
        }

        public override String ToString()
        {
            return base.ToString();
        }

    } // End class

} // End namespace