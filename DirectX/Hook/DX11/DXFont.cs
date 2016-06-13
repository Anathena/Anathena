// Adapted from Frank Luna's "Sprites and Text" example here: http://www.d3dcoder.net/resources.htm 
// checkout his books here: http://www.d3dcoder.net/default.htm
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Diagnostics;

namespace DirectXShell.Hook.DX11
{
    public class DXFont : IDisposable
    {
        private const Char StartChar = (Char)33;
        private const Char EndChar = (Char)127;
        private const UInt32 NumChars = EndChar - StartChar;

        private DeviceContext DeviceContext;
        private Device Device;

        private Rectangle[] CharRects = new Rectangle[NumChars];
        private ShaderResourceView FontSheetSRV;
        private Texture2D FontSheetTex;
        private Boolean Initialized;
        private Int32 TexWidth;
        private Int32 TexHeight;
        private Int32 SpaceWidth;
        private Int32 CharHeight;

        public DXFont(Device Device, DeviceContext DeviceContext)
        {
            this.Device = Device;
            this.DeviceContext = DeviceContext;
            Initialized = false;
            FontSheetTex = null;
            FontSheetSRV = null;
            TexWidth = 1024;
            TexHeight = 0;
            SpaceWidth = 0;
            CharHeight = 0;
        }

        public void Dispose()
        {
            if (FontSheetTex != null)
                FontSheetTex.Dispose();

            if (FontSheetSRV != null)
                FontSheetSRV.Dispose();

            FontSheetTex = null;
            FontSheetSRV = null;
            Device = null;
            DeviceContext = null;
        }

        public bool Initialize(string FontName, float FontSize, System.Drawing.FontStyle FontStyle, bool AntiAliased)
        {
            Debug.Assert(!Initialized);

            System.Drawing.Font Font = new System.Drawing.Font(FontName, FontSize, FontStyle, System.Drawing.GraphicsUnit.Pixel);

            System.Drawing.Text.TextRenderingHint Hint = AntiAliased ? System.Drawing.Text.TextRenderingHint.AntiAlias : System.Drawing.Text.TextRenderingHint.SystemDefault;

            int tempSize = (int)(FontSize * 2);
            using (System.Drawing.Bitmap CharBitmap = new System.Drawing.Bitmap(tempSize, tempSize, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (System.Drawing.Graphics CharGraphics = System.Drawing.Graphics.FromImage(CharBitmap))
                {
                    CharGraphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;
                    CharGraphics.TextRenderingHint = Hint;

                    MeasureChars(Font, CharGraphics);

                    using (System.Drawing.Bitmap FontSheetBitmap = new System.Drawing.Bitmap(TexWidth, TexHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                    {
                        using (System.Drawing.Graphics FontSheetGraphics = System.Drawing.Graphics.FromImage(FontSheetBitmap))
                        {
                            FontSheetGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                            FontSheetGraphics.Clear(System.Drawing.Color.FromArgb(0, System.Drawing.Color.Black));

                            BuildFontSheetBitmap(Font, CharGraphics, CharBitmap, FontSheetGraphics);

                            if (!BuildFontSheetTexture(FontSheetBitmap))
                            {
                                return false;
                            }
                        }

                        // System.Drawing.Bitmap Bitmap = new System.Drawing.Bitmap(FontSheetBitmap);
                        // Bitmap.Save(@"C:\temp\test.png");
                    }
                }
            }

            Initialized = true;

            return true;
        }

        private bool BuildFontSheetTexture(System.Drawing.Bitmap FontSheetBitmap)
        {
            Texture2DDescription TexDescription = new Texture2DDescription();
            System.Drawing.Imaging.BitmapData BitmapData;

            BitmapData = FontSheetBitmap.LockBits(new System.Drawing.Rectangle(0, 0, TexWidth, TexHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            TexDescription.Width = TexWidth;
            TexDescription.Height = TexHeight;
            TexDescription.MipLevels = 1;
            TexDescription.ArraySize = 1;
            TexDescription.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
            TexDescription.SampleDescription.Count = 1;
            TexDescription.SampleDescription.Quality = 0;
            TexDescription.Usage = ResourceUsage.Immutable;
            TexDescription.BindFlags = BindFlags.ShaderResource;
            TexDescription.CpuAccessFlags = CpuAccessFlags.None;
            TexDescription.OptionFlags = ResourceOptionFlags.None;

            DataBox Data;
            Data.DataPointer = BitmapData.Scan0;
            Data.RowPitch = TexWidth * 4;
            Data.SlicePitch = 0;

            FontSheetTex = new Texture2D(Device, TexDescription, new[] { Data });

            if (FontSheetTex == null)
                return false;

            ShaderResourceViewDescription ShaderResourceViewDescription = new ShaderResourceViewDescription();
            ShaderResourceViewDescription.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
            ShaderResourceViewDescription.Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D;
            ShaderResourceViewDescription.Texture2D.MipLevels = 1;
            ShaderResourceViewDescription.Texture2D.MostDetailedMip = 0;

            FontSheetSRV = new ShaderResourceView(Device, FontSheetTex, ShaderResourceViewDescription);

            if (FontSheetSRV == null)
                return false;

            FontSheetBitmap.UnlockBits(BitmapData);

            return true;
        }

        void MeasureChars(System.Drawing.Font Font, System.Drawing.Graphics CharGraphics)
        {
            Char[] AllChars = new Char[NumChars];

            for (Char Index = (Char)0; Index < NumChars; ++Index)
                AllChars[Index] = (Char)(StartChar + Index);

            System.Drawing.SizeF Size;
            Size = CharGraphics.MeasureString(new String(AllChars), Font, new System.Drawing.PointF(0, 0), System.Drawing.StringFormat.GenericDefault);

            CharHeight = (Int32)(Size.Height + 0.5f);

            Int32 NumRows = (Int32)(Size.Width / TexWidth) + 1;
            TexHeight = (NumRows * CharHeight) + 1;

            System.Drawing.StringFormat StringFormat = System.Drawing.StringFormat.GenericDefault;
            StringFormat.FormatFlags |= System.Drawing.StringFormatFlags.MeasureTrailingSpaces;
            Size = CharGraphics.MeasureString(" ", Font, 0, StringFormat);
            SpaceWidth = (Int32)(Size.Width + 0.5f);
        }

        void BuildFontSheetBitmap(System.Drawing.Font Font, System.Drawing.Graphics CharGraphics, System.Drawing.Bitmap CharBitmap, System.Drawing.Graphics FontSheetGraphics)
        {
            System.Drawing.Brush WhiteBrush = System.Drawing.Brushes.White;
            Int32 FontSheetX = 0;
            Int32 FontSheetY = 0;

            for (Int32 Index = 0; Index < NumChars; ++Index)
            {
                CharGraphics.Clear(System.Drawing.Color.FromArgb(0, System.Drawing.Color.Black));
                CharGraphics.DrawString(((Char)(StartChar + Index)).ToString(), Font, WhiteBrush, new System.Drawing.PointF(0.0f, 0.0f));

                Int32 MinX = GetCharMinX(CharBitmap);
                Int32 MaxX = GetCharMaxX(CharBitmap);
                Int32 CharWidth = MaxX - MinX + 1;

                if (FontSheetX + CharWidth >= TexWidth)
                {
                    FontSheetX = 0;
                    FontSheetY += (Int32)(CharHeight) + 1;
                }

                CharRects[Index] = new Rectangle(FontSheetX, FontSheetY, CharWidth, CharHeight);
                FontSheetGraphics.DrawImage(CharBitmap, FontSheetX, FontSheetY, new System.Drawing.Rectangle(MinX, 0, CharWidth, CharHeight), System.Drawing.GraphicsUnit.Pixel);
                FontSheetX += CharWidth + 1;
            }
        }

        private Int32 GetCharMaxX(System.Drawing.Bitmap CharBitmap)
        {
            Int32 Width = CharBitmap.Width;
            Int32 Height = CharBitmap.Height;

            for (Int32 X = Width - 1; X >= 0; --X)
            {
                for (Int32 Y = 0; Y < Height; ++Y)
                {
                    System.Drawing.Color Color = CharBitmap.GetPixel(X, Y);

                    if (Color.A > 0)
                        return X;
                }
            }

            return Width - 1;
        }

        private Int32 GetCharMinX(System.Drawing.Bitmap CharBitmap)
        {
            Int32 Width = CharBitmap.Width;
            Int32 Height = CharBitmap.Height;

            for (Int32 X = 0; X < Width; ++X)
            {
                for (Int32 Y = 0; Y < Height; ++Y)
                {
                    System.Drawing.Color Color;

                    Color = CharBitmap.GetPixel(X, Y);

                    if (Color.A > 0)
                        return X;
                }
            }

            return 0;
        }

        public ShaderResourceView GetFontSheetSRV()
        {
            Debug.Assert(Initialized);

            return FontSheetSRV;
        }

        public Rectangle GetCharRect(Char Char)
        {
            Debug.Assert(Initialized);

            return CharRects[Char - StartChar];
        }

        public int GetSpaceWidth()
        {
            Debug.Assert(Initialized);

            return SpaceWidth;
        }

        public int GetCharHeight()
        {
            Debug.Assert(Initialized);

            return CharHeight;
        }

    } // End class

} // End namespace