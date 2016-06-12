
namespace Capture.Hook.DX11
{
    // Copyright (c) 2013 Justin Stenning
    // Adapted from original code by Alexandre Mutel
    // 
    //----------------------------------------------------------------------------
    // Copyright (c) 2010-2013 SharpDX - Alexandre Mutel
    // 
    // Permission is hereby granted, free of charge, to any person obtaining a copy
    // of this software and associated documentation files (the "Software"), to deal
    // in the Software without restriction, including without limitation the rights
    // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    // copies of the Software, and to permit persons to whom the Software is
    // furnished to do so, subject to the following conditions:
    // 
    // The above copyright notice and this permission notice shall be included in
    // all copies or substantial portions of the Software.
    // 
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    // THE SOFTWARE.
    using SharpDX;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class RendererBase : Component
    {
        public DeviceManager DeviceManager { get; protected set; }
        public virtual bool Show { get; set; }
        public Matrix World;

        public RendererBase()
        {
            World = Matrix.Identity;
            Show = true;
        }

        /// <summary>
        /// Initialize with the provided deviceManager
        /// </summary>
        /// <param name="deviceManager"></param>
        public virtual void Initialize(DeviceManager dm)
        {
            this.DeviceManager = dm;

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
        protected virtual void CreateDeviceDependentResources()
        {
        }

        /// <summary>
        /// Create any resources that depend upon the size of the render target
        /// </summary>
        protected virtual void CreateSizeDependentResources()
        {
        }

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

        public void Render(SharpDX.Direct3D11.DeviceContext context)
        {
            if (Show)
                DoRender(context);
        }

        protected virtual void DoRender(SharpDX.Direct3D11.DeviceContext context)
        {

        }
    }
}
