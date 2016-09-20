using SharpDX.Mathematics.Interop;
using System;

namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    public abstract class RendererBase
    {
        public DeviceManager DeviceManager { get; protected set; }
        public virtual Boolean Show { get; set; }
        public RawMatrix World;

        public RendererBase()
        {
            // World = RawMatrix.Identity;
            Show = true;
        }

        /// <summary>
        /// Initialize with the provided deviceManager
        /// </summary>
        /// <param name="deviceManager"></param>
        public virtual void Initialize(DeviceManager DeviceManager)
        {
            this.DeviceManager = DeviceManager;

            // The device is already initialized, create
            // any device resources immediately.
            if (this.DeviceManager.Direct3DDevice != null)
            {
                CreateDeviceDependentResources();
            }
        }

        /// <summary>
        /// Create any resources that depend on the device or device context
        /// </summary>
        protected virtual void CreateDeviceDependentResources() { }

        /// <summary>
        /// Create any resources that depend upon the size of the render target
        /// </summary>
        protected virtual void CreateSizeDependentResources() { }

        /// <summary>
        /// Render a frame
        /// </summary>
        public void Render()
        {
            if (Show)
                DoRender();
        }

        /// <summary>
        /// Each descendant of RendererBase performs a frame
        /// render within the implementation of DoRender
        /// </summary>
        protected abstract void DoRender();

        public void Render(SharpDX.Direct3D11.DeviceContext Context)
        {
            if (Show)
                DoRender(Context);
        }

        protected virtual void DoRender(SharpDX.Direct3D11.DeviceContext Context) { }

    } // End class

} // End namespace