using Capture.Hook.Common;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Capture.Hook.DX9
{
    internal class DXOverlayEngine : Component
    {
        public List<IOverlay> Overlays { get; set; }

        bool _initialised = false;
        bool _initialising = false;

        Device _device;
        Sprite _sprite;
        Dictionary<string, Font> _fontCache = new Dictionary<string, Font>();
        Dictionary<Element, Texture> _imageCache = new Dictionary<Element, Texture>();

        public Device Device { get { return _device; } }

        public DXOverlayEngine()
        {
            Overlays = new List<IOverlay>();
        }

        private void EnsureInitiliased()
        {
            Debug.Assert(_initialised);
        }

        public bool Initialise(Device device)
        {
            Debug.Assert(!_initialised);
            if (_initialising)
                return false;

            _initialising = true;

            try
            {

                _device = device;

                _sprite = ToDispose(new Sprite(_device));

                // Initialise any resources required for overlay elements
                IntialiseElementResources();

                _initialised = true;
                return true;
            }
            finally
            {
                _initialising = false;
            }
        }

        private void IntialiseElementResources()
        {
            foreach (var overlay in Overlays)
            {
                foreach (var element in overlay.Elements)
                {
                    var textElement = element as TextElement;
                    var imageElement = element as ImageElement;

                    if (textElement != null)
                    {
                        GetFontForTextElement(textElement);
                    }
                    else if (imageElement != null)
                    {
                        GetImageForImageElement(imageElement);
                    }
                }
            }
        }

        private void Begin()
        {
            _sprite.Begin(SpriteFlags.AlphaBlend);
        }

        /// <summary>
        /// Draw the overlay(s)
        /// </summary>
        public void Draw()
        {
            EnsureInitiliased();

            Begin();

            foreach (var overlay in Overlays)
            {
                foreach (var element in overlay.Elements)
                {
                    if (element.Hidden)
                        continue;

                    var textElement = element as TextElement;
                    var imageElement = element as ImageElement;

                    if (textElement != null)
                    {
                        Font font = GetFontForTextElement(textElement);
                        if (font != null && !String.IsNullOrEmpty(textElement.Text))
                            font.DrawText(_sprite, textElement.Text, textElement.Location.X, textElement.Location.Y, new SharpDX.ColorBGRA(textElement.Color.R, textElement.Color.G, textElement.Color.B, textElement.Color.A));
                    }
                    else if (imageElement != null)
                    {
                        Texture image = GetImageForImageElement(imageElement);
                        if (image != null)
                            _sprite.Draw(image, new SharpDX.ColorBGRA(imageElement.Tint.R, imageElement.Tint.G, imageElement.Tint.B, imageElement.Tint.A), null, null, new Vector3(imageElement.Location.X, imageElement.Location.Y, 0));
                    }
                }
            }

            End();
        }

        private void End()
        {
            _sprite.End();
        }

        /// <summary>
        /// In Direct3D9 it is necessary to call OnLostDevice before any call to device.Reset(...) for certain interfaces found in D3DX (e.g. ID3DXSprite, ID3DXFont, ID3DXLine) - https://msdn.microsoft.com/en-us/library/windows/desktop/bb172979(v=vs.85).aspx
        /// </summary>
        public void BeforeDeviceReset()
        {
            try
            {
                foreach (var item in _fontCache)
                    item.Value.OnLostDevice();
                
                if (_sprite != null)
                    _sprite.OnLostDevice();
            }
            catch { }
        }

        Font GetFontForTextElement(TextElement element)
        {
            Font result = null;

            string fontKey = String.Format("{0}{1}{2}", element.Font.Name, element.Font.Size, element.Font.Style, element.AntiAliased);

            if (!_fontCache.TryGetValue(fontKey, out result))
            {
                result = ToDispose(new Font(_device, new FontDescription { 
                    FaceName = element.Font.Name,
                    Italic = (element.Font.Style & System.Drawing.FontStyle.Italic) == System.Drawing.FontStyle.Italic,
                    Quality = (element.AntiAliased ? FontQuality.Antialiased : FontQuality.Default),
                    Weight = ((element.Font.Style & System.Drawing.FontStyle.Bold) == System.Drawing.FontStyle.Bold) ? FontWeight.Bold : FontWeight.Normal,
                    Height = (int)element.Font.SizeInPoints
                }));
                _fontCache[fontKey] = result;
            }
            return result;
        }

        Texture GetImageForImageElement(ImageElement element)
        {
            Texture result = null;

            if (!String.IsNullOrEmpty(element.Filename))
            {
                if (!_imageCache.TryGetValue(element, out result))
                {
                    result = ToDispose(SharpDX.Direct3D9.Texture.FromFile(_device, element.Filename));

                    _imageCache[element] = result;
                }
            }
            return result;
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources
        /// </summary>
        /// <param name="disposing">true if disposing both unmanaged and managed</param>
        protected override void Dispose(bool disposing)
        {
            if (true)
            {
                _device = null;
            }
        }

        void SafeDispose(DisposeBase disposableObj)
        {
            if (disposableObj != null)
                disposableObj.Dispose();
        }

    }
}
