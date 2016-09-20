using System;
using System.Drawing;

namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.Elements
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