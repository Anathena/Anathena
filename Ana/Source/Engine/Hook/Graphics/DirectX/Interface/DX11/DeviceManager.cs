namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using SharpDX.Direct3D11;

    internal class DeviceManager
    {
        public DeviceManager(Device device)
        {
            this.D3DDevice = device;
            this.D3DContext = device.ImmediateContext;
        }

        /// <summary>
        /// Gets the Direct3D11 device.
        /// </summary>
        public Device Direct3DDevice
        {
            get
            {
                return this.D3DDevice;
            }
        }

        /// <summary>
        /// Gets the Direct3D11 immediate context.
        /// </summary>
        public DeviceContext Direct3DContext
        {
            get
            {
                return this.D3DContext;
            }
        }

        protected Device D3DDevice { get; set; }

        protected DeviceContext D3DContext { get; set; }
    }
    //// End class
}
//// End namespace