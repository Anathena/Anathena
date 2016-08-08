using System;
using System.Collections.Generic;

namespace Anathena.Source.Engine.InputCapture.HotKeys
{
    public class MouseHotKey : IHotKey
    {
        private HashSet<Byte> ActivationMouseButtons;

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