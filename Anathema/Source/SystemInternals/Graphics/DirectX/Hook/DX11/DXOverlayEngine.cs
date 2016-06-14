﻿using DirectXHook.Hook.Common;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DirectXHook.Hook.DX11
{
    internal class DXOverlayEngine : Component
    {
        public List<IOverlay> Overlays { get; set; }
        public Boolean DeferredContext { get { return DeviceContext.TypeInfo == DeviceContextType.Deferred; } }

        private Boolean Initialized = false;
        private Boolean Initializing = false;

        private Device Device;
        private DeviceContext DeviceContext;
        private Texture2D RenderTarget;
        private RenderTargetView RenderTargetView;
        private DXSprite SpriteEngine;
        private Dictionary<String, DXFont> FontCache;
        private Dictionary<Element, DXImage> ImageCache;

        public DXOverlayEngine()
        {
            FontCache = new Dictionary<string, DXFont>();
            ImageCache = new Dictionary<Element, DXImage>();

            Overlays = new List<IOverlay>();
        }

        private void EnsureInitiliased()
        {
            Debug.Assert(Initialized);
        }

        public Boolean Initialize(SharpDX.DXGI.SwapChain swapChain)
        {
            return Initialize(swapChain.GetDevice<Device>(), swapChain.GetBackBuffer<Texture2D>(0));
        }

        public Boolean Initialize(Device Device, Texture2D RenderTarget)
        {
            Debug.Assert(!Initialized);

            if (Initializing)
                return false;

            Initializing = true;

            try
            {

                this.Device = Device;
                this.RenderTarget = RenderTarget;

                try
                {
                    DeviceContext = ToDispose(new DeviceContext(this.Device));
                }
                catch (SharpDXException)
                {
                    DeviceContext = this.Device.ImmediateContext;
                }

                RenderTargetView = ToDispose(new RenderTargetView(this.Device, this.RenderTarget));

                // if (DeferredContext)
                // {
                //      ViewportF[] ViewportF = { new ViewportF(0, 0, RenderTarget.Description.Width, RenderTarget.Description.Height, 0, 1) };
                //      DeviceContext.Rasterizer.SetViewports(ViewportF);
                //      DeviceContext.OutputMerger.SetTargets(RenderTargetView);
                // }

                SpriteEngine = new DXSprite(this.Device, DeviceContext);
                if (!SpriteEngine.Initialize())
                    return false;

                // Initialise any resources required for overlay elements
                IntializeElementResources();
                Initialized = true;

                return true;
            }
            finally
            {
                Initializing = false;
            }
        }

        private void IntializeElementResources()
        {
            foreach (IOverlay Overlay in Overlays)
            {
                foreach (IOverlayElement Element in Overlay.Elements)
                {
                    TextElement TextElement = Element as TextElement;
                    ImageElement ImageElement = Element as ImageElement;

                    if (TextElement != null)
                    {
                        GetFontForTextElement(TextElement);
                    }
                    else if (ImageElement != null)
                    {
                        GetImageForImageElement(ImageElement);
                    }
                }
            }
        }

        private void Begin()
        {
            // if (!DeferredContext)
            // {
            ViewportF[] ViewportF = { new ViewportF(0, 0, RenderTarget.Description.Width, RenderTarget.Description.Height, 0, 1) };
            DeviceContext.Rasterizer.SetViewports(ViewportF);
            DeviceContext.OutputMerger.SetTargets(RenderTargetView);
            // }
        }

        /// <summary>
        /// Draw the overlay(s)
        /// </summary>
        public void Draw()
        {
            EnsureInitiliased();

            Begin();

            foreach (IOverlay Overlay in Overlays)
            {
                foreach (IOverlayElement Element in Overlay.Elements)
                {
                    if (Element.Hidden)
                        continue;

                    TextElement TextElement = Element as TextElement;
                    ImageElement ImageElement = Element as ImageElement;

                    if (TextElement != null)
                    {
                        DXFont Font = GetFontForTextElement(TextElement);

                        if (Font != null && !String.IsNullOrEmpty(TextElement.Text))
                            SpriteEngine.DrawString(TextElement.Location.X, TextElement.Location.Y, TextElement.Text, TextElement.Color, Font);
                    }
                    else if (ImageElement != null)
                    {
                        DXImage Image = GetImageForImageElement(ImageElement);

                        if (Image != null)
                            SpriteEngine.DrawImage(ImageElement.Location.X, ImageElement.Location.Y, ImageElement.Scale, ImageElement.Angle, ImageElement.Tint, Image);
                    }
                }
            }

            End();
        }

        private void End()
        {
            if (DeferredContext)
            {
                var commandList = DeviceContext.FinishCommandList(true);
                Device.ImmediateContext.ExecuteCommandList(commandList, true);
                commandList.Dispose();
            }
        }

        private DXFont GetFontForTextElement(TextElement Element)
        {
            DXFont Result = null;

            String FontKey = String.Format("{0}{1}{2}", Element.Font.Name, Element.Font.Size, Element.Font.Style, Element.AntiAliased);

            if (!FontCache.TryGetValue(FontKey, out Result))
            {
                Result = ToDispose(new DXFont(Device, DeviceContext));
                Result.Initialize(Element.Font.Name, Element.Font.Size, Element.Font.Style, Element.AntiAliased);
                FontCache[FontKey] = Result;
            }

            return Result;
        }

        private DXImage GetImageForImageElement(ImageElement Element)
        {
            DXImage Result = null;

            if (!ImageCache.TryGetValue(Element, out Result))
            {
                Result = ToDispose(new DXImage(Device, DeviceContext));
                Result.Initialise(Element.Bitmap);
                ImageCache[Element] = Result;
            }

            return Result;
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources
        /// </summary>
        /// <param name="disposing">true if disposing both unmanaged and managed</param>
        protected override void Dispose(Boolean disposing)
        {
            if (true)
                Device = null;
        }

        void SafeDispose(DisposeBase DisposableObject)
        {
            if (DisposableObject != null)
                DisposableObject.Dispose();
        }

    } // End class

} // End namespace