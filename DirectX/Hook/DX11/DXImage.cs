using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D11;
using SharpDX;
using System.Diagnostics;

namespace Capture.Hook.DX11
{
    public class DXImage : Component
    {
        Device _device;
        DeviceContext _deviceContext;
        Texture2D _tex;
        ShaderResourceView _texSRV;
        int _texWidth, _texHeight;
        bool _initialised = false;

        public int Width
        {
            get
            {
                return _texWidth;
            }
        }

        public int Height
        {
            get
            {
                return _texHeight;
            }
        }
        
        public Device Device
        {
            get { return _device; }
        }

        public DXImage(Device device, DeviceContext deviceContext): base("DXImage")
        {
            _device = device;
            _deviceContext = deviceContext;
            _tex = null;
            _texSRV = null;
            _texWidth = 0;
            _texHeight = 0;
        }

        public bool Initialise(System.Drawing.Bitmap bitmap)
        {
            RemoveAndDispose(ref _tex);
            RemoveAndDispose(ref _texSRV);

            //Debug.Assert(bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Imaging.BitmapData bmData;

            _texWidth = bitmap.Width;
            _texHeight = bitmap.Height;

            bmData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, _texWidth, _texHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            try
            {
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
                data.RowPitch = bmData.Stride;// _texWidth * 4;
                data.SlicePitch = 0;

                _tex = ToDispose(new Texture2D(_device, texDesc, new[] { data }));
                if (_tex == null)
                    return false;

                ShaderResourceViewDescription srvDesc = new ShaderResourceViewDescription();
                srvDesc.Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm;
                srvDesc.Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D;
                srvDesc.Texture2D.MipLevels = 1;
                srvDesc.Texture2D.MostDetailedMip = 0;

                _texSRV = ToDispose(new ShaderResourceView(_device, _tex, srvDesc));
                if (_texSRV == null)
                    return false;
            }
            finally
            {
                bitmap.UnlockBits(bmData);
            }

            _initialised = true;

            return true;
        }

        public ShaderResourceView GetSRV()
        {
            Debug.Assert(_initialised);
            return _texSRV;
        }
    }
}
