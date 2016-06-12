using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capture.Hook.Common
{
    public class ImageElement: Element
    {
        public virtual System.Drawing.Bitmap Bitmap { get; set; }
        
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

        public float Angle { get; set; }

        public float Scale { get; set; }

        public string Filename { get; set; }

        bool _ownsBitmap = false;

        public ImageElement(string filename):
            this(new System.Drawing.Bitmap(filename), true)
        {
            Filename = filename;
        }

        public ImageElement(System.Drawing.Bitmap bitmap, bool ownsImage = false)
        {
            Tint = System.Drawing.Color.White;
            this.Bitmap = bitmap;
            _ownsBitmap = ownsImage;
            Scale = 1.0f;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (_ownsBitmap)
                {
                    SafeDispose(this.Bitmap);
                    this.Bitmap = null;
                }
            }
        }
    }
}
