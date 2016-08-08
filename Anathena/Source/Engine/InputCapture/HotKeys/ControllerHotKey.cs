using System;
using System.Collections.Generic;

namespace Anathena.Source.Engine.InputCapture.HotKeys
{
    public class ControllerHotKey : IHotKey
    {
        private HashSet<Int32> ActivationKeys;

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