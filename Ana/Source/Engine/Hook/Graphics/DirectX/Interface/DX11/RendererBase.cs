namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using SharpDX.Mathematics.Interop;
    using System;

    internal abstract class RendererBase
    {
        public RendererBase()
        {
            //// this.World = RawMatrix.Identity;
            this.Show = true;
        }

        public DeviceManager DeviceManager { get; protected set; }

        public virtual Boolean Show { get; set; }

        public RawMatrix World { get; set; }

        /// <summary>
        /// Initialize with the provided deviceManager
        /// </summary>
        /// <param name="deviceManager"></param>
        public virtual void Initialize(DeviceManager deviceManager)
        {
            this.DeviceManager = deviceManager;

            // The device is already initialized, create
            // any device resources immediately.
            if (this.DeviceManager.Direct3DDevice != null)
            {
                this.CreateDeviceDependentResources();
            }
        }

        /// <summary>
        /// Render a frame
        /// </summary>
        public void Render()
        {
            if (this.Show)
            {
                this.DoRender();
            }
        }

        public void Render(SharpDX.Direct3D11.DeviceContext context)
        {
            if (this.Show)
            {
                this.DoRender(context);
            }
        }

        /// <summary>
        /// Each descendant of RendererBase performs a frame
        /// render within the implementation of DoRender
        /// </summary>
        protected abstract void DoRender();

        /// <summary>
        /// Create any resources that depend on the device or device context
        /// </summary>
        protected virtual void CreateDeviceDependentResources()
        {
        }

        /// <summary>
        /// Create any resources that depend upon the size of the render target
        /// </summary>
        protected virtual void CreateSizeDependentResources()
        {
        }

        protected virtual void DoRender(SharpDX.Direct3D11.DeviceContext context)
        {
        }
    }
    //// End class
}
//// End namespace