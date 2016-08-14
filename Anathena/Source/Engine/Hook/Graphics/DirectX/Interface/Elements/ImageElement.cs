using System;
using System.Drawing;

namespace Anathena.Source.Engine.Hook.Graphics.DirectX.Interface.Elements
{
    public class ImageElement : Element
    {
        /// <summary>
        /// The location of where to render this image element
        /// </summary>
        public virtual Point Location { get; set; }
        public virtual Bitmap Bitmap { get; set; }
        public Single Angle { get; set; }
        public Single Scale { get; set; }
        public String Filename { get; set; }

        private Boolean OwnsBitmap = false;

        public ImageElement(String Filename) : this(new Bitmap(Filename), true)
        {
            this.Filename = Filename;
        }

        public ImageElement(Bitmap Bitmap, Boolean OwnsImage = false) : base()
        {
            this.Bitmap = Bitmap;
            OwnsBitmap = OwnsImage;
            Scale = 1.0f;
        }

        protected override void Dispose(Boolean Disposing)
        {
            base.Dispose(Disposing);

            if (!Disposing || !OwnsBitmap)
                return;

            SafeDispose(this.Bitmap);
            this.Bitmap = null;
        }

    } // End class

} // End namespace