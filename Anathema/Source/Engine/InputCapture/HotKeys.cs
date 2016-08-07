using SharpDX.DirectInput;
using System.Collections.Generic;

namespace Anathema.Source.Engine.InputCapture
{
    /// <summary>
    /// Defines a keyboard, controller, or mouse binding
    /// </summary>
    public class HotKeys
    {
        private HashSet<Key> ActivationKeys;

        public HotKeys()
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

    } // End class

} // End namespace