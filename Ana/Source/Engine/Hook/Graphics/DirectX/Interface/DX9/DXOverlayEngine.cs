namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX9
{
    using Ana.Source.Engine.Hook.Graphics.DirectX.Interface.Elements;
    using SharpDX;
    using SharpDX.Direct3D9;
    using SharpDX.Mathematics.Interop;
    using System;
    using System.Collections.Generic;

    internal class DXOverlayEngine : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DXOverlayEngine" /> class
        /// </summary>
        /// <param name="graphicsInterface">An object providing access to control of the DirectX graphics library</param>
        public DXOverlayEngine(DirextXGraphicsInterface graphicsInterface)
        {
            this.GraphicsInterface = graphicsInterface;
            this.ImageCache = new Dictionary<Element, Texture>();
            this.FontCache = new Dictionary<String, Font>();
        }

        public Device Device { get; private set; }

        private Dictionary<Element, Texture> ImageCache { get; set; }

        private Dictionary<String, Font> FontCache { get; set; }

        private Sprite Sprite { get; set; }

        private DirextXGraphicsInterface GraphicsInterface { get; set; }

        public void Dispose()
        {
            this.Sprite?.Dispose();

            foreach (Element element in this.ImageCache.Keys)
            {
                element?.Dispose();
            }

            foreach (Font font in this.FontCache.Values)
            {
                font?.Dispose();
            }
        }

        public void Initialize(Device device)
        {
            this.Device = device;
            this.Sprite = new Sprite(device);

            foreach (TextElement textElement in this.GraphicsInterface.GetTextElements())
            {
                this.GetFontForTextElement(textElement);
            }

            foreach (ImageElement imageElement in this.GraphicsInterface.GetImageElements())
            {
                this.GetImageForImageElement(imageElement);
            }
        }

        /// <summary>
        /// Draw the overlay(s)
        /// </summary>
        public void Draw()
        {
            this.Begin();

            foreach (TextElement textElement in this.GraphicsInterface.GetTextElements())
            {
                textElement?.Frame();

                Font font = this.GetFontForTextElement(textElement);

                // TODO: Maybe offload draw into element itself, passing in whatever needed
                font?.DrawText(Sprite, textElement.Text, textElement.Location.X, textElement.Location.Y, new RawColorBGRA(textElement.Color.B, textElement.Color.G, textElement.Color.R, textElement.Color.A));
            }

            foreach (ImageElement imageElement in this.GraphicsInterface.GetImageElements())
            {
                imageElement?.Frame();

                Texture image = this.GetImageForImageElement(imageElement);
                this.Sprite?.Draw(image, new RawColorBGRA(255, 255, 255, 255), null, null, new RawVector3(imageElement.Location.X, imageElement.Location.Y, 0));
            }

            this.End();
        }

        /// <summary>
        /// In Direct3D9 it is necessary to call OnLostDevice before any call to device.Reset(...) for certain interfaces found in D3DX (e.g. ID3DXSprite, ID3DXFont, ID3DXLine) - https://msdn.microsoft.com/en-us/library/windows/desktop/bb172979(v=vs.85).aspx
        /// </summary>
        public void BeforeDeviceReset()
        {
            try
            {
                foreach (KeyValuePair<String, Font> item in this.FontCache)
                {
                    item.Value.OnLostDevice();
                }

                if (this.Sprite != null)
                {
                    this.Sprite.OnLostDevice();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources
        /// </summary>
        /// <param name="disposing">true if disposing both unmanaged and managed</param>
        protected void Dispose(Boolean disposing)
        {
            Device = null;
        }

        private Font GetFontForTextElement(TextElement element)
        {
            Font result = null;
            String fontKey = String.Format("{0}{1}{2}", element.Font.Name, element.Font.Size, element.Font.Style, true);

            if (!this.FontCache.TryGetValue(fontKey, out result))
            {
                result = new Font(
                    Device,
                    new FontDescription
                    {
                        FaceName = element.Font.Name,
                        Italic = false,
                        Quality = FontQuality.Antialiased,
                        Weight = FontWeight.Bold,
                        Height = (Int32)element.Font.SizeInPoints
                    });

                this.FontCache[fontKey] = result;
            }

            return result;
        }

        private Texture GetImageForImageElement(ImageElement element)
        {
            Texture result = null;

            if (!String.IsNullOrEmpty(element.Filename))
            {
                if (!this.ImageCache.TryGetValue(element, out result))
                {
                    result = Texture.FromFile(Device, element.Filename); // ToDispose()

                    this.ImageCache[element] = result;
                }
            }

            return result;
        }

        private void End()
        {
            this.Sprite.End();
        }

        private void Begin()
        {
            this.Sprite.Begin(SpriteFlags.AlphaBlend);
        }

        private void SafeDispose(DisposeBase disposableObject)
        {
            disposableObject?.Dispose();
        }
    }
    //// End class
}
//// End namespace