using SharpDX.Direct3D11;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Hook.DX11
{
    public class DeviceManager : SharpDX.Component
    {
        // Direct3D Objects
        protected Device D3DDevice;
        protected DeviceContext D3DContext;

        /// <summary>
        /// Gets the Direct3D11 device.
        /// </summary>
        public Device Direct3DDevice { get { return D3DDevice; } }

        /// <summary>
        /// Gets the Direct3D11 immediate context.
        /// </summary>
        public DeviceContext Direct3DContext { get { return D3DContext; } }

        public DeviceManager(Device Device)
        {
            D3DDevice = Device;
            D3DContext = Device.ImmediateContext;
        }

    } // End class

} // End namespace