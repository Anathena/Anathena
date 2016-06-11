/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Drawing;

using SlimDX.Direct2D;

namespace SlimDX.SampleFramework {
    /// <summary>
    /// Provides creation and management functionality for a Direct2D rendering context.
    /// </summary>
    public class DeviceContext2D : IDisposable {
        #region Public Interface

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceContext2D"/> class.
        /// </summary>
        /// <param name="handle">The window handle to associate with the device.</param>
        /// <param name="settings">The settings used to configure the device.</param>
        public DeviceContext2D(IntPtr handle, DeviceSettings2D settings) {
            if (handle == IntPtr.Zero)
                throw new ArgumentException("Value must be a valid window handle.", "handle");
            if (settings == null)
                throw new ArgumentNullException("settings");

            this.settings = settings;

            factory = new Factory();
            RenderTarget = new WindowRenderTarget(factory, new WindowRenderTargetProperties {
                Handle = handle,
                PixelSize = new Size(settings.Width, settings.Height)
            });
        }

        /// <summary>
        /// Performs object finalization.
        /// </summary>
        ~DeviceContext2D() {
            Dispose(false);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected virtual void Dispose(bool disposeManagedResources) {
            if (disposeManagedResources) {
                RenderTarget.Dispose();
                factory.Dispose();
            }
        }

        /// <summary>
        /// Gets the underlying Direct3D render target.
        /// </summary>
        public WindowRenderTarget RenderTarget {
            get;
            private set;
        }

        #endregion
        #region Implementation Detail

        DeviceSettings2D settings;

        Factory factory;

        #endregion
    }
}
