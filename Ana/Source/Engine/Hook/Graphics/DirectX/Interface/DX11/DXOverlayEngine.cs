namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using Elements;
    using SharpDX;
    using SharpDX.Direct3D11;
    using SharpDX.Mathematics.Interop;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DXOverlayEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DXOverlayEngine" /> class
        /// </summary>
        public DXOverlayEngine()
        {
            this.Initialized = false;
            this.Initializing = false;
            this.FontCache = new Dictionary<string, DXFont>();
            this.ImageCache = new Dictionary<Element, DXImage>();
        }

        public Boolean DeferredContext
        {
            get
            {
                return this.DeviceContext.TypeInfo == DeviceContextType.Deferred;
            }
        }

        private Boolean Initialized { get; set; }

        private Boolean Initializing { get; set; }

        private Device Device { get; set; }

        private DeviceContext DeviceContext { get; set; }

        private Texture2D RenderTarget { get; set; }

        private RenderTargetView RenderTargetView { get; set; }

        private DXSprite SpriteEngine { get; set; }

        private Dictionary<String, DXFont> FontCache { get; set; }

        private Dictionary<Element, DXImage> ImageCache { get; set; }

        public Boolean Initialize(SharpDX.DXGI.SwapChain swapChain)
        {
            return this.Initialize(swapChain.GetDevice<Device>(), swapChain.GetBackBuffer<Texture2D>(0));
        }

        public Boolean Initialize(Device device, Texture2D renderTarget)
        {
            Debug.Assert(!this.Initialized, "Ensure not already initialized");

            if (this.Initializing)
            {
                return false;
            }

            this.Initializing = true;

            try
            {
                this.Device = device;
                this.RenderTarget = renderTarget;

                try
                {
                    this.DeviceContext = new DeviceContext(this.Device); // ToDispose()
                }
                catch (SharpDXException)
                {
                    this.DeviceContext = this.Device.ImmediateContext;
                }

                this.RenderTargetView = new RenderTargetView(this.Device, this.RenderTarget); // ToDispose()

                //// if (this.DeferredContext)
                //// {
                ////      ViewportF[] viewportF = { new ViewportF(0, 0, RenderTarget.Description.Width, RenderTarget.Description.Height, 0, 1) };
                ////      this.DeviceContext.Rasterizer.SetViewports(ViewportF);
                ////      this.DeviceContext.OutputMerger.SetTargets(RenderTargetView);
                //// }

                this.SpriteEngine = new DXSprite(this.Device, DeviceContext);
                if (!this.SpriteEngine.Initialize())
                {
                    return false;
                }

                // Initialize any resources required for overlay elements
                this.IntializeElementResources();
                this.Initialized = true;

                return true;
            }
            finally
            {
                this.Initializing = false;
            }
        }

        /// <summary>
        /// Draw the overlay(s)
        /// </summary>
        public void Draw()
        {
            this.EnsureInitiliazed();

            this.Begin();

            dynamic element = null;

            TextElement textElement = element as TextElement;
            ImageElement imageElement = element as ImageElement;

            if (textElement != null)
            {
                DXFont font = this.GetFontForTextElement(textElement);

                if (font != null && !String.IsNullOrEmpty(textElement.Text))
                {
                    this.SpriteEngine.DrawString(textElement.Location.X, textElement.Location.Y, textElement.Text, textElement.Color, font);
                }
            }
            else if (imageElement != null)
            {
                DXImage image = this.GetImageForImageElement(imageElement);

                if (image != null)
                {
                    this.SpriteEngine.DrawImage(imageElement.Location.X, imageElement.Location.Y, imageElement.Scale, imageElement.Angle, System.Drawing.Color.White, image);
                }
            }

            this.End();
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources
        /// </summary>
        /// <param name="disposing">true if disposing both unmanaged and managed</param>
        protected void Dispose(Boolean disposing)
        {
            if (true)
            {
                this.Device = null;
            }
        }

        private void IntializeElementResources()
        {
            dynamic element = null;

            TextElement textElement = element as TextElement;
            ImageElement imageElement = element as ImageElement;

            if (textElement != null)
            {
                this.GetFontForTextElement(textElement);
            }
            else if (imageElement != null)
            {
                this.GetImageForImageElement(imageElement);
            }
        }

        private void EnsureInitiliazed()
        {
            Debug.Assert(this.Initialized, "Ensure initialized");
        }

        private void Begin()
        {
            //// if (!this.DeferredContext)
            //// {
            RawViewportF[] viewportF = { new RawViewportF() }; //// (0, 0, RenderTarget.Description.Width, RenderTarget.Description.Height, 0, 1)
            this.DeviceContext.Rasterizer.SetViewports(viewportF);
            this.DeviceContext.OutputMerger.SetTargets(RenderTargetView);
            //// }
        }

        private void End()
        {
            if (!this.DeferredContext)
            {
                return;
            }

            CommandList commandList = DeviceContext.FinishCommandList(true);
            this.Device.ImmediateContext.ExecuteCommandList(commandList, true);
            commandList.Dispose();
        }

        private DXFont GetFontForTextElement(TextElement element)
        {
            DXFont result = null;

            String fontKey = String.Format("{0}{1}{2}", element.Font.Name, element.Font.Size, element.Font.Style, true);

            if (!this.FontCache.TryGetValue(fontKey, out result))
            {
                result = new DXFont(this.Device, this.DeviceContext);
                result.Initialize(element.Font.Name, element.Font.Size, element.Font.Style, true);
                this.FontCache[fontKey] = result;
            }

            return result;
        }

        private DXImage GetImageForImageElement(ImageElement element)
        {
            DXImage result = null;

            if (!this.ImageCache.TryGetValue(element, out result))
            {
                result = new DXImage(this.Device, this.DeviceContext);
                result.Initialize(element.Bitmap);
                this.ImageCache[element] = result;
            }

            return result;
        }

        private void SafeDispose(DisposeBase disposableObject)
        {
            if (disposableObject != null)
            {
                disposableObject.Dispose();
            }
        }
    }
    //// End class
}
//// End namespace