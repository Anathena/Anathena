namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.Elements
{
    using System;
    using System.Drawing;

    internal class TextElement : Element
    {
        public TextElement(Font font) : base()
        {
            this.Font = font;
        }

        public virtual String Text { get; set; }

        public virtual Font Font { get; set; }

        public virtual Color Color { get; set; }

        public virtual Point Location { get; set; }
    }
    //// End class
}
//// End namespace