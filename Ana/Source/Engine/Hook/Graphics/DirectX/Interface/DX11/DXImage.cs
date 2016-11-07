namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using SharpDX;
    using SharpDX.Direct3D11;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;

    internal class DXImage
    {
        private Device device;

        public DXImage(Device device, DeviceContext deviceContext)
        {
            this.Initialized = false;
            this.Device = device;
            this.DeviceContext = deviceContext;
            this.Tex = null;
            this.TexSRV = null;
            this.TexWidth = 0;
            this.TexHeight = 0;
        }

        public Int32 Width
        {
            get
            {
                return this.TexWidth;
            }
        }

        public Int32 Height
        {
            get
            {
                return this.TexHeight;
            }
        }

        public Device Device
        {
            get
            {
                return this.device;
            }

            private set
            {
                this.device = value;
            }
        }

        private DeviceContext DeviceContext { get; set; }

        private Texture2D Tex { get; set; }

        private ShaderResourceView TexSRV { get; set; }

        private Int32 TexWidth { get; set; }

        private Int32 TexHeight { get; set; }

        private Boolean Initialized { get; set; }

        public Boolean Initialize(Bitmap bitmap)
        {
            //// RemoveAndDispose(ref Tex);
            //// RemoveAndDispose(ref TexSRV);

            //// Debug.Assert(bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData bitmapData;

            this.TexWidth = bitmap.Width;
            this.TexHeight = bitmap.Height;

            bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, this.TexWidth, this.TexHeight), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                Texture2DDescription texDescription = new Texture2DDescription();
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
                data.RowPitch = bitmapData.Stride; //// texWidth * 4;
                data.SlicePitch = 0;

                this.Tex = new Texture2D(this.Device, texDescription, new[] { data });

                if (this.Tex == null)
                {
                    return false;
                }

                ShaderResourceViewDescription shaderResourceViewDescription = new ShaderResourceViewDescription();
                shaderResourceViewDescription.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
                shaderResourceViewDescription.Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D;
                shaderResourceViewDescription.Texture2D.MipLevels = 1;
                shaderResourceViewDescription.Texture2D.MostDetailedMip = 0;

                this.TexSRV = new ShaderResourceView(this.Device, this.Tex, shaderResourceViewDescription);

                if (this.TexSRV == null)
                {
                    return false;
                }
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            this.Initialized = true;

            return true;
        }

        public ShaderResourceView GetSRV()
        {
            Debug.Assert(this.Initialized, "Ensure initialized");
            return this.TexSRV;
        }
    }
    //// End class
}
//// End namespace