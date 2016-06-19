using System;
using System.Windows.Forms;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation
{
    internal class ButtonSet
    {
        private MouseButtons MouseSet;

        public ButtonSet()
        {
            MouseSet = MouseButtons.None;
        }

        public void Add(MouseButtons Element)
        {
            MouseSet |= Element;
        }

        public void Remove(MouseButtons Element)
        {
            MouseSet &= ~Element;
        }

        public Boolean Contains(MouseButtons Element)
        {
            return (MouseSet & Element) != MouseButtons.None;
        }

    } // End class

} // End namespace