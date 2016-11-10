namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using SharpDX.Direct3D11;

    /// <summary>
    /// Manages a DirextX 11 Device
    /// </summary>
    internal class DeviceManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceManager" /> class
        /// </summary>
        /// <param name="device">DirectX11 Device</param>
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

        /// <summary>
        /// Gets or sets the DirectX 11 Device
        /// </summary>
        protected Device D3DDevice { get; set; }

        /// <summary>
        /// Gets or sets the Direct 3D context
        /// </summary>
        protected DeviceContext D3DContext { get; set; }
    }
    //// End class
}
//// End namespace