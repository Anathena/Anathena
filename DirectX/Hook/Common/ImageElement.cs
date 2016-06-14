using System;

namespace DirectXHook.Hook.Common
{
    public class ImageElement : Element
    {
        /// <summary>
        /// This value is multiplied with the source color (e.g. White will result in same color as source image)
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="System.Drawing.Color.White"/>.
        /// </remarks>
        public virtual System.Drawing.Color Tint { get; set; }

        /// <summary>
        /// The location of where to render this image element
        /// </summary>
        public virtual System.Drawing.Point Location { get; set; }

        public virtual System.Drawing.Bitmap Bitmap { get; set; }
        public Single Angle { get; set; }
        public Single Scale { get; set; }
        public String Filename { get; set; }
        private Boolean OwnsBitmap = false;

        public ImageElement(String Filename) : this(new System.Drawing.Bitmap(Filename), true)
        {
            this.Filename = Filename;
        }

        public ImageElement(System.Drawing.Bitmap Bitmap, Boolean OwnsImage = false)
        {
            Tint = System.Drawing.Color.White;
            this.Bitmap = Bitmap;
            OwnsBitmap = OwnsImage;
            Scale = 1.0f;
        }

        protected override void Dispose(Boolean Disposing)
        {
            base.Dispose(Disposing);

            if (Disposing)
            {
                if (OwnsBitmap)
                {
                    SafeDispose(this.Bitmap);
                    this.Bitmap = null;
                }
            }
        }

    } // End class

} // End namespace