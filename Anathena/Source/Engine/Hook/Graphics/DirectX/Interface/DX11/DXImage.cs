using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Diagnostics;

namespace Anathena.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    public class DXImage
    {
        private DeviceContext DeviceContext;
        private Texture2D Tex;
        private ShaderResourceView TexSRV;
        private Int32 TexWidth, TexHeight;
        private Boolean Initialized = false;

        public int Width { get { return TexWidth; } }
        public int Height { get { return TexHeight; } }

        private Device _Device;
        public Device Device { get { return _Device; } }

        public DXImage(Device Device, DeviceContext deviceContext)// : base("DXImage")
        {
            _Device = Device;
            DeviceContext = deviceContext;
            Tex = null;
            TexSRV = null;
            TexWidth = 0;
            TexHeight = 0;
        }

        public Boolean Initialize(System.Drawing.Bitmap Bitmap)
        {
            // RemoveAndDispose(ref Tex);
            // RemoveAndDispose(ref TexSRV);

            // Debug.Assert(Bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Imaging.BitmapData BitmapData;

            TexWidth = Bitmap.Width;
            TexHeight = Bitmap.Height;

            BitmapData = Bitmap.LockBits(new System.Drawing.Rectangle(0, 0, TexWidth, TexHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            try
            {
                Texture2DDescription TexDescription = new Texture2DDescription();
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
                Data.RowPitch = BitmapData.Stride;// _texWidth * 4;
                Data.SlicePitch = 0;

                Tex = new Texture2D(_Device, TexDescription, new[] { Data }); // ToDispose()
                if (Tex == null)
                    return false;

                ShaderResourceViewDescription ShaderResourceViewDescription = new ShaderResourceViewDescription();
                ShaderResourceViewDescription.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
                ShaderResourceViewDescription.Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D;
                ShaderResourceViewDescription.Texture2D.MipLevels = 1;
                ShaderResourceViewDescription.Texture2D.MostDetailedMip = 0;

                TexSRV = new ShaderResourceView(_Device, Tex, ShaderResourceViewDescription); // ToDispose()

                if (TexSRV == null)
                    return false;
            }
            finally
            {
                Bitmap.UnlockBits(BitmapData);
            }

            Initialized = true;

            return true;
        }

        public ShaderResourceView GetSRV()
        {
            Debug.Assert(Initialized);
            return TexSRV;
        }

    } // End class

} // End namespace