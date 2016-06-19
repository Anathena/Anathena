using System;
using System.Drawing;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface.Common
{
    public class TextElement : Element
    {
        public virtual String Text { get; set; }
        public virtual Font Font { get; set; }
        public virtual Color Color { get; set; }
        public virtual Point Location { get; set; }

        public TextElement(Font Font) : base()
        {
            this.Font = Font;
        }

    } // End class

} // End namespace