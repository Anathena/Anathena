using SharpDX.DirectInput;
using System;
using System.Collections.Generic;

namespace Anathena.Source.Engine.InputCapture.HotKeys
{
    public class KeyboardHotKey : IHotKey
    {
        private HashSet<Key> ActivationKeys;

        public KeyboardHotKey()
        {
            ActivationKeys = new HashSet<Key>();
        }

        public void SetActivationKeys(IEnumerable<Key> ActivationKeys)
        {
            this.ActivationKeys = new HashSet<Key>(ActivationKeys);
        }

        public HashSet<Key> GetActivationKeys()
        {
            return ActivationKeys;
        }

        public override String ToString()
        {
            String HotKeyString = String.Empty;

            if (ActivationKeys == null)
                return HotKeyString;

            foreach (Key Key in ActivationKeys)
                HotKeyString += Key.ToString() + "+";

            return HotKeyString.TrimEnd('+');
        }

    } // End class

} // End namespace