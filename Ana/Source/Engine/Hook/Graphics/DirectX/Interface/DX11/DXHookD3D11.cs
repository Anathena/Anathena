namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.DX11
{
    using SharpDX.Direct3D11;
    using SharpDX.DXGI;
    using SharpDX.Windows;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Direct3D 11 Hook - this hooks the SwapChain.Present to take screenshots
    /// </summary>
    internal class DXHookD3D11 : BaseDXHook
    {
        public DXHookD3D11(DirextXGraphicsInterface graphicsInterface) : base(graphicsInterface)
        {
            this.D3D11VirtualTableAddresses = null;
            this.DXGISwapChainVirtualTableAddresses = null;
            this.DXGISwapChainPresentHook = null;
            this.DXGISwapChainResizeTargetHook = null;
            this.SwapChainPointer = IntPtr.Zero;
        }

        /// <summary>
        /// The IDXGISwapChain.Present function definition
        /// </summary>
        /// <param name="swapChainPtr"></param>
        /// <param name="syncInterval"></param>
        /// <param name="presentFlags"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Int32 DXGISwapChain_PresentDelegate(IntPtr swapChainPtr, Int32 syncInterval, PresentFlags presentFlags);

        /// <summary>
        /// The IDXGISwapChain.ResizeTarget function definition
        /// </summary>
        /// <param name="swapChainPtr"></param>
        /// <param name="newTargetParameters"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Int32 DXGISwapChain_ResizeTargetDelegate(IntPtr swapChainPtr, ref ModeDescription newTargetParameters);

        private List<IntPtr> D3D11VirtualTableAddresses { get; set; }

        private List<IntPtr> DXGISwapChainVirtualTableAddresses { get; set; }

        private Hook<DXGISwapChain_PresentDelegate> DXGISwapChainPresentHook { get; set; }

        private Hook<DXGISwapChain_ResizeTargetDelegate> DXGISwapChainResizeTargetHook { get; set; }

        private SharpDX.Direct3D11.Device Device { get; set; }

        private SwapChain SwapChain { get; set; }

        private RenderForm RenderForm { get; set; }

        private Texture2D ResolvedRTShared { get; set; }

        private KeyedMutex ResolvedRTSharedKeyedMutex { get; set; }

        private ShaderResourceView ResolvedSharedSRV { get; set; }

        private ScreenAlignedQuadRenderer ScreenAlignedQuad { get; set; }

        private Texture2D FinalRT { get; set; }

        private Texture2D ResizedRT { get; set; }

        private RenderTargetView ResizedRTV { get; set; }

        private Query Query { get; set; }

        private Boolean QueryIssued { get; set; }

        private Boolean FinalRTMapped { get; set; }

        private Texture2D ResolvedRT { get; set; }

        private KeyedMutex ResolvedRTKeyedMutex { get; set; }

        private KeyedMutex ResolvedRTKeyedMutex_Dev2 { get; set; }

        private DXOverlayEngine OverlayEngine { get; set; }

        private IntPtr SwapChainPointer { get; set; }

        private ShaderResourceView ResolvedSRV { get; set; }

        public override void Hook()
        {
            if (this.D3D11VirtualTableAddresses == null)
            {
                this.D3D11VirtualTableAddresses = new List<IntPtr>();
                this.DXGISwapChainVirtualTableAddresses = new List<IntPtr>();

                // Create temporary device + swapchain and determine method addresses
                //// this.RenderForm = ToDispose(new RenderForm());
                //// SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport,
                ////    DXGI.CreateSwapChainDescription(this.RenderForm.Handle), out Device, out this.SwapChain);
                //// this.ToDispose(this.Device);
                //// this.ToDispose(this.SwapChain);

                if (this.Device != null && this.SwapChain != null)
                {
                    this.D3D11VirtualTableAddresses.AddRange(this.GetVirtualTableAddresses(this.Device.NativePointer, DirectXFlags.D3D11DeviceMethodCount));
                    this.DXGISwapChainVirtualTableAddresses.AddRange(this.GetVirtualTableAddresses(SwapChain.NativePointer, DirectXFlags.DXGISwapChainMethodCount));
                }
            }

            // We will capture the backbuffer here
            this.DXGISwapChainPresentHook = new Hook<DXGISwapChain_PresentDelegate>(
                this.DXGISwapChainVirtualTableAddresses[(Int32)DirectXFlags.DXGISwapChainVirtualTableEnum.Present],
                new DXGISwapChain_PresentDelegate(this.PresentHook),
                this);

            // We will capture target/window resizes here
            this.DXGISwapChainResizeTargetHook = new Hook<DXGISwapChain_ResizeTargetDelegate>(
                this.DXGISwapChainVirtualTableAddresses[(Int32)DirectXFlags.DXGISwapChainVirtualTableEnum.ResizeTarget],
                new DXGISwapChain_ResizeTargetDelegate(this.ResizeTargetHook),
                this);

            // The following ensures that all threads are intercepted: (must be done for each hook)
            this.DXGISwapChainPresentHook.Activate();
            this.DXGISwapChainResizeTargetHook.Activate();
            this.Hooks.Add(this.DXGISwapChainPresentHook);
            this.Hooks.Add(this.DXGISwapChainResizeTargetHook);
        }

        public override void Cleanup()
        {
            try
            {
                if (this.OverlayEngine != null)
                {
                    //// this.OverlayEngine.Dispose();
                    this.OverlayEngine = null;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Hooked to allow resizing a texture/surface that is reused. Currently not in use as we create the texture for each request
        /// to support different sizes each time (as we use DirectX to copy only the region we are after rather than the entire backbuffer)
        /// </summary>
        /// <param name="swapChainPtr"></param>
        /// <param name="newTargetParameters"></param>
        /// <returns></returns>
        private Int32 ResizeTargetHook(IntPtr swapChainPtr, ref ModeDescription newTargetParameters)
        {
            // Dispose of overlay engine (so it will be recreated with correct renderTarget view size)
            if (this.OverlayEngine != null)
            {
                //// this.OverlayEngine.Dispose();
                this.OverlayEngine = null;
            }

            return this.DXGISwapChainResizeTargetHook.Original(swapChainPtr, ref newTargetParameters);
        }

        /// <summary>
        /// Our present hook that will grab a copy of the backbuffer when requested. Note: this supports multi-sampling (anti-aliasing)
        /// </summary>
        /// <param name="swapChainPtr"></param>
        /// <param name="syncInterval"></param>
        /// <param name="presentFlags"></param>
        /// <returns>The HRESULT of the original method</returns>
        private Int32 PresentHook(IntPtr swapChainPtr, Int32 syncInterval, PresentFlags presentFlags)
        {
            this.Frame();
            SwapChain swapChain = (SwapChain)swapChainPtr;

            try
            {
                if (this.SwapChainPointer != swapChain.NativePointer || this.OverlayEngine == null)
                {
                    // Draw FPS
                    //// if (this.OverlayEngine != null)
                    //// {
                    ////    this.OverlayEngine.Dispose();
                    //// }

                    this.OverlayEngine = new DXOverlayEngine();
                    this.OverlayEngine.Initialize(swapChain);

                    this.SwapChainPointer = swapChain.NativePointer;
                }
                else if (this.OverlayEngine != null)
                {
                    // Draw Overlay(s)
                    this.OverlayEngine.Draw();
                }
            }
            catch
            {
            }

            // As always we need to call the original method, note that EasyHook will automatically skip the hook and call the original method
            // i.e. calling it here will not cause a stack overflow into this function
            return this.DXGISwapChainPresentHook.Original(swapChainPtr, syncInterval, presentFlags);
        }
    }
    //// End class
}
//// End namespace