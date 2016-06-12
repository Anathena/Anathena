// Adapted from Frank Luna's "Sprites and Text" example here: http://www.d3dcoder.net/resources.htm 
// checkout his books here: http://www.d3dcoder.net/default.htm
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D11;
using SharpDX;
using System.Diagnostics;

namespace Capture.Hook.DX11
{
    public class DXFont : IDisposable
    {
        Device _device;
        DeviceContext _deviceContext;

        public DXFont(Device device, DeviceContext deviceContext)
        {
            _device = device;
            _deviceContext = deviceContext;
            _initialized = false;
            _fontSheetTex = null;
            _fontSheetSRV = null;
            _texWidth = 1024;
            _texHeight = 0;
            _spaceWidth = 0;
            _charHeight = 0;
        }

        public void Dispose()
        {
            if (_fontSheetTex != null)
                _fontSheetTex.Dispose();
            if (_fontSheetSRV != null)
                _fontSheetSRV.Dispose();

            _fontSheetTex = null;
            _fontSheetSRV = null;
            _device = null;
            _deviceContext = null;
        }

        enum STYLE
        {
            STYLE_NORMAL = 0,
            STYLE_BOLD = 1,
            STYLE_ITALIC = 2,
            STYLE_BOLD_ITALIC = 3,
            STYLE_UNDERLINE = 4,
            STYLE_STRIKEOUT = 8
        };

        bool _initialized;
        const char StartChar = (char)33;
        const char EndChar = (char)127;
        const uint NumChars = EndChar - StartChar;
        ShaderResourceView _fontSheetSRV;
        Texture2D _fontSheetTex;
        int _texWidth, _texHeight;
        Rectangle[] _charRects = new Rectangle[NumChars];
        int _spaceWidth, _charHeight;

        public bool Initialize(string FontName, float FontSize, System.Drawing.FontStyle FontStyle, bool AntiAliased)
        {
            Debug.Assert(!_initialized);
            System.Drawing.Font font = new System.Drawing.Font(FontName, FontSize, FontStyle, System.Drawing.GraphicsUnit.Pixel);

            System.Drawing.Text.TextRenderingHint hint = AntiAliased ? System.Drawing.Text.TextRenderingHint.AntiAlias : System.Drawing.Text.TextRenderingHint.SystemDefault;

            int tempSize = (int)(FontSize * 2);
            using (System.Drawing.Bitmap charBitmap = new System.Drawing.Bitmap(tempSize, tempSize, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (System.Drawing.Graphics charGraphics = System.Drawing.Graphics.FromImage(charBitmap))
                {
                    charGraphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;
                    charGraphics.TextRenderingHint = hint;

                    MeasureChars(font, charGraphics);

                    using (var fontSheetBitmap = new System.Drawing.Bitmap(_texWidth, _texHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                    {
                        using (var fontSheetGraphics = System.Drawing.Graphics.FromImage(fontSheetBitmap))
                        {
                            fontSheetGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                            fontSheetGraphics.Clear(System.Drawing.Color.FromArgb(0, System.Drawing.Color.Black));

                            BuildFontSheetBitmap(font, charGraphics, charBitmap, fontSheetGraphics);

                            if (!BuildFontSheetTexture(fontSheetBitmap))
                            {
                                return false;
                            }
                        }
                        //System.Drawing.Bitmap bm = new System.Drawing.Bitmap(fontSheetBitmap);
                        //bm.Save(@"C:\temp\test.png");
                    }
                }
            }

            _initialized = true;

            return true;
        }

        private bool BuildFontSheetTexture(System.Drawing.Bitmap fontSheetBitmap)
        {
            System.Drawing.Imaging.BitmapData bmData;

            bmData = fontSheetBitmap.LockBits(new System.Drawing.Rectangle(0, 0, _texWidth, _texHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Texture2DDescription texDesc = new Texture2DDescription();
            texDesc.Width = _texWidth;
            texDesc.Height = _texHeight;
            texDesc.MipLevels = 1;
            texDesc.ArraySize = 1;
            texDesc.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
            texDesc.SampleDescription.Count = 1;
            texDesc.SampleDescription.Quality = 0;
            texDesc.Usage = ResourceUsage.Immutable;
            texDesc.BindFlags = BindFlags.ShaderResource;
            texDesc.CpuAccessFlags = CpuAccessFlags.None;
            texDesc.OptionFlags = ResourceOptionFlags.None;


            SharpDX.DataBox data;
            data.DataPointer = bmData.Scan0;
            data.RowPitch = _texWidth * 4;
            data.SlicePitch = 0;

            _fontSheetTex = new Texture2D(_device, texDesc, new[] { data });
            if (_fontSheetTex == null)
                return false;

            ShaderResourceViewDescription srvDesc = new ShaderResourceViewDescription();
            srvDesc.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
            srvDesc.Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D;
            srvDesc.Texture2D.MipLevels = 1;
            srvDesc.Texture2D.MostDetailedMip = 0;

            _fontSheetSRV = new ShaderResourceView(_device, _fontSheetTex, srvDesc);
            if (_fontSheetSRV == null)
                return false;

            fontSheetBitmap.UnlockBits(bmData);

            return true;
        }

        void MeasureChars(System.Drawing.Font font, System.Drawing.Graphics charGraphics)
        {
            char[] allChars = new char[NumChars];

            for (char i = (char)0; i < NumChars; ++i)
                allChars[i] = (char)(StartChar + i);

            System.Drawing.SizeF size;
            size = charGraphics.MeasureString(new String(allChars), font, new System.Drawing.PointF(0, 0), System.Drawing.StringFormat.GenericDefault);

            _charHeight = (int)(size.Height + 0.5f);

            int numRows = (int)(size.Width / _texWidth) + 1;
            _texHeight = (numRows * _charHeight) + 1;

            System.Drawing.StringFormat sf = System.Drawing.StringFormat.GenericDefault;
            sf.FormatFlags |= System.Drawing.StringFormatFlags.MeasureTrailingSpaces;
            size = charGraphics.MeasureString(" ", font, 0, sf);
            _spaceWidth = (int)(size.Width + 0.5f);
        }

        void BuildFontSheetBitmap(System.Drawing.Font font, System.Drawing.Graphics charGraphics, System.Drawing.Bitmap charBitmap, System.Drawing.Graphics fontSheetGraphics)
        {
            System.Drawing.Brush whiteBrush = System.Drawing.Brushes.White;
            int fontSheetX = 0;
            int fontSheetY = 0;


            for (int i = 0; i < NumChars; ++i)
            {
                charGraphics.Clear(System.Drawing.Color.FromArgb(0, System.Drawing.Color.Black));
                charGraphics.DrawString(((char)(StartChar + i)).ToString(), font, whiteBrush, new System.Drawing.PointF(0.0f, 0.0f));

                int minX = GetCharMinX(charBitmap);
                int maxX = GetCharMaxX(charBitmap);
                int charWidth = maxX - minX + 1;

                if (fontSheetX + charWidth >= _texWidth)
                {
                    fontSheetX = 0;
                    fontSheetY += (int)(_charHeight) + 1;
                }

                _charRects[i] = new Rectangle(fontSheetX, fontSheetY, charWidth, _charHeight);

                fontSheetGraphics.DrawImage(charBitmap, fontSheetX, fontSheetY, new System.Drawing.Rectangle(minX, 0, charWidth, _charHeight), System.Drawing.GraphicsUnit.Pixel);

                fontSheetX += charWidth + 1;
            }
        }

        private int GetCharMaxX(System.Drawing.Bitmap charBitmap)
        {
            int width = charBitmap.Width;
            int height = charBitmap.Height;

            for (int x = width - 1; x >= 0; --x)
            {
                for (int y = 0; y < height; ++y)
                {
                    System.Drawing.Color color;

                    color = charBitmap.GetPixel(x, y);
                    if (color.A > 0)
                        return x;
                }
            }

            return width - 1;
        }

        private int GetCharMinX(System.Drawing.Bitmap charBitmap)
        {
            int width = charBitmap.Width;
            int height = charBitmap.Height;

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    System.Drawing.Color color;

                    color = charBitmap.GetPixel(x, y);
                    if (color.A > 0)
                        return x;
                }
            }

            return 0;
        }

        public ShaderResourceView GetFontSheetSRV()
        {
            Debug.Assert(_initialized);

            return _fontSheetSRV;
        }

        public Rectangle GetCharRect(char c)
        {
            Debug.Assert(_initialized);

            return _charRects[c - StartChar];
        }

        public int GetSpaceWidth()
        {
            Debug.Assert(_initialized);

            return _spaceWidth;
        }

        public int GetCharHeight()
        {
            Debug.Assert(_initialized);

            return _charHeight;
        }

    }
}


