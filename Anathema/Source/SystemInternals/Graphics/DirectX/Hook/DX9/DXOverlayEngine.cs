using Anathema.Source.SystemInternals.Graphics.DirectX.Hook.Common;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Hook.DX9
{
    internal class DXOverlayEngine : Component
    {
        public List<IOverlay> Overlays { get; set; }

        private Boolean Initialized;
        private Boolean Initializing;

        private Dictionary<Element, Texture> ImageCache;
        private Dictionary<String, Font> FontCache;

        private Sprite Sprite;

        private Device _Device;
        public Device Device { get { return _Device; } }

        public DXOverlayEngine()
        {
            Initialized = false;
            Initializing = false;

            ImageCache = new Dictionary<Element, Texture>();
            FontCache = new Dictionary<String, Font>();

            Overlays = new List<IOverlay>();
        }

        private void EnsureInitiliazed()
        {
            Debug.Assert(Initialized);
        }

        public Boolean Initialize(Device Device)
        {
            Debug.Assert(!Initialized);

            if (Initializing)
                return false;

            Initializing = true;

            try
            {
                _Device = Device;
                Sprite = ToDispose(new Sprite(this.Device));

                // Initialize any resources required for overlay elements
                IntializeElementResources();
                Initialized = true;

                return true;
            }
            finally
            {
                Initializing = false;
            }
        }

        private void IntializeElementResources()
        {
            foreach (IOverlay Overlay in Overlays)
            {
                foreach (IOverlayElement Element in Overlay.Elements)
                {
                    TextElement TextElement = Element as TextElement;
                    ImageElement ImageElement = Element as ImageElement;

                    if (TextElement != null)
                    {
                        GetFontForTextElement(TextElement);
                    }
                    else if (ImageElement != null)
                    {
                        GetImageForImageElement(ImageElement);
                    }
                }
            }
        }

        private void Begin()
        {
            Sprite.Begin(SpriteFlags.AlphaBlend);
        }

        /// <summary>
        /// Draw the overlay(s)
        /// </summary>
        public void Draw()
        {
            EnsureInitiliazed();

            Begin();

            foreach (IOverlay Overlay in Overlays)
            {
                foreach (IOverlayElement Element in Overlay.Elements)
                {
                    if (Element.Hidden)
                        continue;

                    TextElement TextElement = Element as TextElement;
                    ImageElement ImageElement = Element as ImageElement;

                    if (TextElement != null)
                    {
                        Font Font = GetFontForTextElement(TextElement);
                        if (Font != null && !String.IsNullOrEmpty(TextElement.Text))
                            Font.DrawText(Sprite, TextElement.Text, TextElement.Location.X, TextElement.Location.Y, new ColorBGRA(TextElement.Color.R, TextElement.Color.G, TextElement.Color.B, TextElement.Color.A));
                    }
                    else if (ImageElement != null)
                    {
                        Texture Image = GetImageForImageElement(ImageElement);
                        if (Image != null)
                            Sprite.Draw(Image, new ColorBGRA(ImageElement.Tint.R, ImageElement.Tint.G, ImageElement.Tint.B, ImageElement.Tint.A), null, null, new Vector3(ImageElement.Location.X, ImageElement.Location.Y, 0));
                    }
                }
            }

            End();
        }

        private void End()
        {
            Sprite.End();
        }

        /// <summary>
        /// In Direct3D9 it is necessary to call OnLostDevice before any call to device.Reset(...) for certain interfaces found in D3DX (e.g. ID3DXSprite, ID3DXFont, ID3DXLine) - https://msdn.microsoft.com/en-us/library/windows/desktop/bb172979(v=vs.85).aspx
        /// </summary>
        public void BeforeDeviceReset()
        {
            try
            {
                foreach (KeyValuePair<String, Font> Item in FontCache)
                    Item.Value.OnLostDevice();

                if (Sprite != null)
                    Sprite.OnLostDevice();
            }
            catch { }
        }

        Font GetFontForTextElement(TextElement Element)
        {
            Font Result = null;
            String FontKey = String.Format("{0}{1}{2}", Element.Font.Name, Element.Font.Size, Element.Font.Style, Element.AntiAliased);

            if (!FontCache.TryGetValue(FontKey, out Result))
            {
                Result = ToDispose(new Font(_Device, new FontDescription
                {
                    FaceName = Element.Font.Name,
                    Italic = (Element.Font.Style & System.Drawing.FontStyle.Italic) == System.Drawing.FontStyle.Italic,
                    Quality = (Element.AntiAliased ? FontQuality.Antialiased : FontQuality.Default),
                    Weight = ((Element.Font.Style & System.Drawing.FontStyle.Bold) == System.Drawing.FontStyle.Bold) ? FontWeight.Bold : FontWeight.Normal,
                    Height = (Int32)Element.Font.SizeInPoints
                }));

                FontCache[FontKey] = Result;
            }
            return Result;
        }

        Texture GetImageForImageElement(ImageElement Element)
        {
            Texture Result = null;

            if (!String.IsNullOrEmpty(Element.Filename))
            {
                if (!ImageCache.TryGetValue(Element, out Result))
                {
                    Result = ToDispose(Texture.FromFile(_Device, Element.Filename));

                    ImageCache[Element] = Result;
                }
            }

            return Result;
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources
        /// </summary>
        /// <param name="disposing">true if disposing both unmanaged and managed</param>
        protected override void Dispose(Boolean disposing)
        {
            if (true)
                _Device = null;
        }

        void SafeDispose(DisposeBase DisposableObject)
        {
            DisposableObject?.Dispose();
        }

    } // End class

} // End namespace