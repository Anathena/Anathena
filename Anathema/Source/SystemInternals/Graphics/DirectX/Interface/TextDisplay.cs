using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface
{
    public class TextDisplay
    {
        public Boolean Visible { get; set; }
        public String Text { get; set; }

        public TextDisplay()
        {
            Visible = true;
        }

        /// <summary>
        /// Must be called each frame
        /// </summary>
        public void Frame()
        {

        }

    } // End class

} // End namespace