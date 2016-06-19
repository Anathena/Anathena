using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface.DX11
{
    /// <summary>
    /// Direct3D 11 Hook - this hooks the SwapChain.Present to take screenshots
    /// </summary>
    internal class DXHookD3D11 : BaseDXHook
    {

        private List<IntPtr> D3D11VirtualTableAddresses = null;
        private List<IntPtr> DXGISwapChainVirtualTableAddresses = null;

        private Hook<DXGISwapChain_PresentDelegate> DXGISwapChainPresentHook = null;
        private Hook<DXGISwapChain_ResizeTargetDelegate> DXGISwapChainResizeTargetHook = null;

        private Object AccessLock = new Object();

        private SharpDX.Direct3D11.Device Device;
        private SwapChain SwapChain;
        // private RenderForm RenderForm;
        private Texture2D ResolvedRTShared;
        private KeyedMutex ResolvedRTSharedKeyedMutex;
        private ShaderResourceView ResolvedSharedSRV;
        private ScreenAlignedQuadRenderer ScreenAlignedQuad;
        private Texture2D FinalRT;
        private Texture2D ResizedRT;
        private RenderTargetView ResizedRTV;

        private Query Query;
        private Boolean QueryIssued;
        private Boolean FinalRTMapped;

        private Texture2D ResolvedRT;
        private KeyedMutex ResolvedRTKeyedMutex;
        private KeyedMutex ResolvedRTKeyedMutex_Dev2;
        // private ShaderResourceView ResolvedSRV;
        public DXHookD3D11(DirextXGraphicsInterface GraphicsInterface) : base(GraphicsInterface) { }

        protected override String HookName { get { return "DXHookD3D11"; } }

        public override void Hook()
        {
            if (D3D11VirtualTableAddresses == null)
            {
                D3D11VirtualTableAddresses = new List<IntPtr>();
                DXGISwapChainVirtualTableAddresses = new List<IntPtr>();

                // Create temporary device + swapchain and determine method addresses
                /*RenderForm = ToDispose(new RenderForm());
                SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport,
                    DXGI.CreateSwapChainDescription(RenderForm.Handle), out Device, out SwapChain);

                ToDispose(Device);
                ToDispose(SwapChain);*/

                if (Device != null && SwapChain != null)
                {
                    D3D11VirtualTableAddresses.AddRange(GetVirtualTableAddresses(Device.NativePointer, DirectXFlags.D3D11_DeviceMethodCount));
                    DXGISwapChainVirtualTableAddresses.AddRange(GetVirtualTableAddresses(SwapChain.NativePointer, DirectXFlags.DXGISwapChainMethodCount));
                }
            }

            // We will capture the backbuffer here
            DXGISwapChainPresentHook = new Hook<DXGISwapChain_PresentDelegate>(
                DXGISwapChainVirtualTableAddresses[(Int32)DirectXFlags.DXGISwapChainVirtualTableEnum.Present],
                new DXGISwapChain_PresentDelegate(PresentHook), this);

            // We will capture target/window resizes here
            DXGISwapChainResizeTargetHook = new Hook<DXGISwapChain_ResizeTargetDelegate>(
                DXGISwapChainVirtualTableAddresses[(Int32)DirectXFlags.DXGISwapChainVirtualTableEnum.ResizeTarget],
                new DXGISwapChain_ResizeTargetDelegate(ResizeTargetHook), this);

            // The following ensures that all threads are intercepted: (must be done for each hook)
            DXGISwapChainPresentHook.Activate();

            DXGISwapChainResizeTargetHook.Activate();

            Hooks.Add(DXGISwapChainPresentHook);
            Hooks.Add(DXGISwapChainResizeTargetHook);
        }

        public override void Cleanup()
        {
            try
            {
                if (OverlayEngine != null)
                {
                    // OverlayEngine.Dispose();
                    OverlayEngine = null;
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// The IDXGISwapChain.Present function definition
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate Int32 DXGISwapChain_PresentDelegate(IntPtr SwapChainPtr, Int32 SyncInterval, /* Int32 */ PresentFlags Flags);

        /// <summary>
        /// The IDXGISwapChain.ResizeTarget function definition
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate Int32 DXGISwapChain_ResizeTargetDelegate(IntPtr SwapChainPtr, ref ModeDescription NewTargetParameters);

        /// <summary>
        /// Hooked to allow resizing a texture/surface that is reused. Currently not in use as we create the texture for each request
        /// to support different sizes each time (as we use DirectX to copy only the region we are after rather than the entire backbuffer)
        /// </summary>
        /// <param name="SwapChainPtr"></param>
        /// <param name="NewTargetParameters"></param>
        /// <returns></returns>
        Int32 ResizeTargetHook(IntPtr SwapChainPtr, ref ModeDescription NewTargetParameters)
        {
            // Dispose of overlay engine (so it will be recreated with correct renderTarget view size)
            if (OverlayEngine != null)
            {
                // OverlayEngine.Dispose();
                OverlayEngine = null;
            }

            return DXGISwapChainResizeTargetHook.Original(SwapChainPtr, ref NewTargetParameters);
        }

        /// <summary>
        /// Our present hook that will grab a copy of the backbuffer when requested. Note: this supports multi-sampling (anti-aliasing)
        /// </summary>
        /// <param name="SwapChainPtr"></param>
        /// <param name="SyncInterval"></param>
        /// <param name="Flags"></param>
        /// <returns>The HRESULT of the original method</returns>
        Int32 PresentHook(IntPtr SwapChainPtr, Int32 SyncInterval, PresentFlags Flags)
        {
            this.Frame();
            SwapChain SwapChain = (SwapChain)SwapChainPtr;

            try
            {
                // Draw FPS
                if (SwapChainPointer != SwapChain.NativePointer || OverlayEngine == null)
                {
                    // if (OverlayEngine != null)
                    // OverlayEngine.Dispose();

                    OverlayEngine = new DXOverlayEngine();
                    OverlayEngine.Initialize(SwapChain);

                    SwapChainPointer = SwapChain.NativePointer;
                }

                // Draw Overlay(s)
                else if (OverlayEngine != null)
                {
                    OverlayEngine.Draw();
                }
            }
            catch
            {

            }

            // As always we need to call the original method, note that EasyHook will automatically skip the hook and call the original method
            // i.e. calling it here will not cause a stack overflow into this function
            return DXGISwapChainPresentHook.Original(SwapChainPtr, SyncInterval, Flags);
        }

        DXOverlayEngine OverlayEngine;

        IntPtr SwapChainPointer = IntPtr.Zero;

    } // End class

} // End namespace