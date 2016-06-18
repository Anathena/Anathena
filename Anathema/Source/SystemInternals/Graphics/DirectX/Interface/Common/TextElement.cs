using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface.Common
{
    public class TextElement : Element
    {
        public virtual string Text { get; set; }
        public virtual System.Drawing.Font Font { get; set; }
        public virtual System.Drawing.Color Color { get; set; }
        public virtual System.Drawing.Point Location { get; set; }
        public virtual Boolean AntiAliased { get; set; }

        public TextElement(System.Drawing.Font Font)
        {
            this.Font = Font;
        }

    } // End class

} // End namespace