namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using SharpDX;
    using SharpDX.Direct3D11;
    using SharpDX.Mathematics.Interop;
    using System;
    using System.Diagnostics;
    using System.Drawing.Imaging;

    internal class DXFont : IDisposable
    {
        private const Char StartChar = (Char)33;

        private const Char EndChar = (Char)127;

        private const UInt32 NumChars = DXFont.EndChar - DXFont.StartChar;

        public DXFont(Device device, DeviceContext deviceContext)
        {
            this.CharRects = new RawRectangle[DXFont.NumChars];
            this.Device = device;
            this.DeviceContext = deviceContext;
            this.Initialized = false;
            this.FontSheetTex = null;
            this.FontSheetSRV = null;
            this.TexWidth = 1024;
            this.TexHeight = 0;
            this.SpaceWidth = 0;
            this.CharHeight = 0;
        }

        private DeviceContext DeviceContext { get; set; }

        private Device Device { get; set; }

        private RawRectangle[] CharRects { get; set; }

        private ShaderResourceView FontSheetSRV { get; set; }

        private Texture2D FontSheetTex { get; set; }

        private Boolean Initialized { get; set; }

        private Int32 TexWidth { get; set; }

        private Int32 TexHeight { get; set; }

        private Int32 SpaceWidth { get; set; }

        private Int32 CharHeight { get; set; }

        public void Dispose()
        {
            if (this.FontSheetTex != null)
            {
                this.FontSheetTex.Dispose();
            }

            if (this.FontSheetSRV != null)
            {
                this.FontSheetSRV.Dispose();
            }

            this.FontSheetTex = null;
            this.FontSheetSRV = null;
            this.Device = null;
            this.DeviceContext = null;
        }

        public Boolean Initialize(String fontName, Single fontSize, System.Drawing.FontStyle fontStyle, Boolean antiAliased)
        {
            Debug.Assert(!this.Initialized, "Ensure not initialized");

            System.Drawing.Font font = new System.Drawing.Font(fontName, fontSize, fontStyle, System.Drawing.GraphicsUnit.Pixel);

            System.Drawing.Text.TextRenderingHint hint = antiAliased ? System.Drawing.Text.TextRenderingHint.AntiAlias : System.Drawing.Text.TextRenderingHint.SystemDefault;

            Int32 tempSize = (Int32)(fontSize * 2.0f);
            using (System.Drawing.Bitmap charBitmap = new System.Drawing.Bitmap(tempSize, tempSize, PixelFormat.Format32bppArgb))
            {
                using (System.Drawing.Graphics charGraphics = System.Drawing.Graphics.FromImage(charBitmap))
                {
                    charGraphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;
                    charGraphics.TextRenderingHint = hint;

                    this.MeasureChars(font, charGraphics);

                    using (System.Drawing.Bitmap fontSheetBitmap = new System.Drawing.Bitmap(this.TexWidth, this.TexHeight, PixelFormat.Format32bppArgb))
                    {
                        using (System.Drawing.Graphics fontSheetGraphics = System.Drawing.Graphics.FromImage(fontSheetBitmap))
                        {
                            fontSheetGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                            fontSheetGraphics.Clear(System.Drawing.Color.FromArgb(0, System.Drawing.Color.Black));

                            this.BuildFontSheetBitmap(font, charGraphics, charBitmap, fontSheetGraphics);

                            if (!this.BuildFontSheetTexture(fontSheetBitmap))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            this.Initialized = true;
            return true;
        }

        public ShaderResourceView GetFontSheetSRV()
        {
            Debug.Assert(this.Initialized, "Ensure initialized");

            return this.FontSheetSRV;
        }

        public RawRectangle GetCharRect(Char character)
        {
            Debug.Assert(this.Initialized, "Ensure initialized");

            return this.CharRects[character - StartChar];
        }

        public Int32 GetSpaceWidth()
        {
            Debug.Assert(this.Initialized, "Ensure initialized");

            return this.SpaceWidth;
        }

        public Int32 GetCharHeight()
        {
            Debug.Assert(this.Initialized, "Ensure initialized");

            return this.CharHeight;
        }

        private Boolean BuildFontSheetTexture(System.Drawing.Bitmap fontSheetBitmap)
        {
            Texture2DDescription texDescription = new Texture2DDescription();
            BitmapData bitmapData;

            bitmapData = fontSheetBitmap.LockBits(new System.Drawing.Rectangle(0, 0, this.TexWidth, this.TexHeight), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            texDescription.Width = this.TexWidth;
            texDescription.Height = this.TexHeight;
            texDescription.MipLevels = 1;
            texDescription.ArraySize = 1;
            texDescription.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
            texDescription.SampleDescription.Count = 1;
            texDescription.SampleDescription.Quality = 0;
            texDescription.Usage = ResourceUsage.Immutable;
            texDescription.BindFlags = BindFlags.ShaderResource;
            texDescription.CpuAccessFlags = CpuAccessFlags.None;
            texDescription.OptionFlags = ResourceOptionFlags.None;

            DataBox data;
            data.DataPointer = bitmapData.Scan0;
            data.RowPitch = this.TexWidth * 4;
            data.SlicePitch = 0;

            this.FontSheetTex = new Texture2D(this.Device, texDescription, new[] { data });

            if (this.FontSheetTex == null)
            {
                return false;
            }

            ShaderResourceViewDescription shaderResourceViewDescription = new ShaderResourceViewDescription();
            shaderResourceViewDescription.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
            shaderResourceViewDescription.Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D;
            shaderResourceViewDescription.Texture2D.MipLevels = 1;
            shaderResourceViewDescription.Texture2D.MostDetailedMip = 0;

            this.FontSheetSRV = new ShaderResourceView(this.Device, this.FontSheetTex, shaderResourceViewDescription);

            if (this.FontSheetSRV == null)
            {
                return false;
            }

            fontSheetBitmap.UnlockBits(bitmapData);

            return true;
        }

        private void MeasureChars(System.Drawing.Font font, System.Drawing.Graphics charGraphics)
        {
            Char[] allChars = new Char[NumChars];

            for (Char index = (Char)0; index < NumChars; index++)
            {
                allChars[index] = (Char)(StartChar + index);
            }

            System.Drawing.SizeF size;
            size = charGraphics.MeasureString(new String(allChars), font, new System.Drawing.PointF(0, 0), System.Drawing.StringFormat.GenericDefault);

            this.CharHeight = (Int32)(size.Height + 0.5f);

            Int32 numRows = (Int32)(size.Width / this.TexWidth) + 1;
            this.TexHeight = (numRows * this.CharHeight) + 1;

            System.Drawing.StringFormat stringFormat = System.Drawing.StringFormat.GenericDefault;
            stringFormat.FormatFlags |= System.Drawing.StringFormatFlags.MeasureTrailingSpaces;
            size = charGraphics.MeasureString(" ", font, 0, stringFormat);
            this.SpaceWidth = (Int32)(size.Width + 0.5f);
        }

        private void BuildFontSheetBitmap(System.Drawing.Font font, System.Drawing.Graphics charGraphics, System.Drawing.Bitmap charBitmap, System.Drawing.Graphics fontSheetGraphics)
        {
            Int32 fontSheetX = 0;
            Int32 fontSheetY = 0;

            for (Int32 index = 0; index < DXFont.NumChars; ++index)
            {
                charGraphics.Clear(System.Drawing.Color.FromArgb(0, System.Drawing.Color.Black));
                charGraphics.DrawString(((Char)(StartChar + index)).ToString(), font, System.Drawing.Brushes.White, new System.Drawing.PointF(0.0f, 0.0f));

                Int32 minX = this.GetCharMinX(charBitmap);
                Int32 maxX = this.GetCharMaxX(charBitmap);
                Int32 charWidth = maxX - minX + 1;

                if (fontSheetX + charWidth >= this.TexWidth)
                {
                    fontSheetX = 0;
                    fontSheetY += this.CharHeight + 1;
                }

                this.CharRects[index] = new RawRectangle(fontSheetX, fontSheetY, charWidth, this.CharHeight);
                fontSheetGraphics.DrawImage(charBitmap, fontSheetX, fontSheetY, new System.Drawing.Rectangle(minX, 0, charWidth, this.CharHeight), System.Drawing.GraphicsUnit.Pixel);
                fontSheetX += charWidth + 1;
            }
        }

        private Int32 GetCharMaxX(System.Drawing.Bitmap charBitmap)
        {
            Int32 width = charBitmap.Width;
            Int32 height = charBitmap.Height;

            for (Int32 x = width - 1; x >= 0; x++)
            {
                for (Int32 y = 0; y < height; y++)
                {
                    System.Drawing.Color color = charBitmap.GetPixel(x, y);

                    if (color.A > 0)
                    {
                        return x;
                    }
                }
            }

            return width - 1;
        }

        private Int32 GetCharMinX(System.Drawing.Bitmap charBitmap)
        {
            Int32 width = charBitmap.Width;
            Int32 height = charBitmap.Height;

            for (Int32 x = 0; x < width; x++)
            {
                for (Int32 y = 0; y < height; y++)
                {
                    System.Drawing.Color color;

                    color = charBitmap.GetPixel(x, y);

                    if (color.A > 0)
                    {
                        return x;
                    }
                }
            }

            return 0;
        }
    }
    //// End class
}
//// End namespace