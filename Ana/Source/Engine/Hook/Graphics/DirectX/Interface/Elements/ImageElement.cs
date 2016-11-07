namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.Elements
{
    using System;
    using System.Drawing;

    internal class ImageElement : Element
    {
        private Boolean ownsBitmap;

        public ImageElement(String filename) : this(new Bitmap(filename), true)
        {
            this.ownsBitmap = false;
            this.Filename = filename;
        }

        public ImageElement(Bitmap bitmap, Boolean ownsBitmap = false) : base()
        {
            this.Bitmap = bitmap;
            this.ownsBitmap = ownsBitmap;
            this.Scale = 1.0f;
        }

        /// <summary>
        /// Gets or sets the location of where to render this image element
        /// </summary>
        public virtual Point Location { get; set; }

        public virtual Bitmap Bitmap { get; set; }

        public Single Angle { get; set; }

        public Single Scale { get; set; }

        public String Filename { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);

            if (!disposing || !this.ownsBitmap)
            {
                return;
            }

            this.SafeDispose(this.Bitmap);
            this.Bitmap = null;
        }
    }
    //// End class
}
//// End namespace