using Anathema.Source.SystemInternals.Graphics.DirectX.Interface.Common;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface.DX9
{
    internal class DXOverlayEngine : Component
    {
        private Boolean Initialized;
        private Boolean Initializing;

        private Dictionary<Element, Texture> ImageCache;
        private Dictionary<String, Font> FontCache;

        private Sprite Sprite;
        public Device Device { get; private set; }

        private DirextXGraphicsInterface GraphicsInterface;

        public DXOverlayEngine(DirextXGraphicsInterface GraphicsInterface)
        {
            this.GraphicsInterface = GraphicsInterface;
            Initialized = false;
            Initializing = false;

            ImageCache = new Dictionary<Element, Texture>();
            FontCache = new Dictionary<String, Font>();
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
                this.Device = Device;
                Sprite = ToDispose(new Sprite(Device));

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
            foreach (TextElement TextElement in GraphicsInterface.GetTextElements())
                GetFontForTextElement(TextElement);

            foreach (ImageElement ImageElement in GraphicsInterface.GetImageElements())
                GetImageForImageElement(ImageElement);
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

            foreach (TextElement TextElement in GraphicsInterface.GetTextElements())
            {
                TextElement?.Frame();

                Font Font = GetFontForTextElement(TextElement);
                // Maybe offload draw into element itself, passing in whatever needed
                Font?.DrawText(Sprite, TextElement.Text, TextElement.Location.X, TextElement.Location.Y, new ColorBGRA(TextElement.Color.R, TextElement.Color.G, TextElement.Color.B, TextElement.Color.A));
            }

            TextElement DamnIt = new TextElement(new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold)) { Location = new System.Drawing.Point(5, 5), Color = System.Drawing.Color.Red, Text = "Some bullshit", AntiAliased = true };
            Font Font66 = GetFontForTextElement(DamnIt);
            Font66.DrawText(Sprite, "GOD DAMN IT", 0, 0, new ColorBGRA(DamnIt.Color.R, DamnIt.Color.G, DamnIt.Color.B, DamnIt.Color.A));

            foreach (ImageElement ImageElement in GraphicsInterface.GetImageElements())
            {
                ImageElement?.Frame();

                Texture Image = GetImageForImageElement(ImageElement);
                Sprite?.Draw(Image, new ColorBGRA(Color.White.R, Color.White.G, Color.White.B, Color.White.A), null, null, new Vector3(ImageElement.Location.X, ImageElement.Location.Y, 0));
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
                Result = ToDispose(new Font(Device, new FontDescription
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
                    Result = ToDispose(Texture.FromFile(Device, Element.Filename));

                    ImageCache[Element] = Result;
                }
            }

            return Result;
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources
        /// </summary>
        /// <param name="Disposing">true if disposing both unmanaged and managed</param>
        protected override void Dispose(Boolean Disposing)
        {
            if (true)
                Device = null;
        }

        void SafeDispose(DisposeBase DisposableObject)
        {
            DisposableObject?.Dispose();
        }

    } // End class

} // End namespace