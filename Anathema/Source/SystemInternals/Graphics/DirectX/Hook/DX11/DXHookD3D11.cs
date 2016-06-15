using Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook.Common;
using Anathema.Source.SystemInternals.Graphics.DirectXHook.Interface;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook.DX11
{
    enum D3D11DeviceVirtualTableEnum : Int16
    {
        // IUnknown
        QueryInterface = 0,
        AddRef = 1,
        Release = 2,

        // ID3D11Device
        CreateBuffer = 3,
        CreateTexture1D = 4,
        CreateTexture2D = 5,
        CreateTexture3D = 6,
        CreateShaderResourceView = 7,
        CreateUnorderedAccessView = 8,
        CreateRenderTargetView = 9,
        CreateDepthStencilView = 10,
        CreateInputLayout = 11,
        CreateVertexShader = 12,
        CreateGeometryShader = 13,
        CreateGeometryShaderWithStreamOutput = 14,
        CreatePixelShader = 15,
        CreateHullShader = 16,
        CreateDomainShader = 17,
        CreateComputeShader = 18,
        CreateClassLinkage = 19,
        CreateBlendState = 20,
        CreateDepthStencilState = 21,
        CreateRasterizerState = 22,
        CreateSamplerState = 23,
        CreateQuery = 24,
        CreatePredicate = 25,
        CreateCounter = 26,
        CreateDeferredContext = 27,
        OpenSharedResource = 28,
        CheckFormatSupport = 29,
        CheckMultisampleQualityLevels = 30,
        CheckCounterInfo = 31,
        CheckCounter = 32,
        CheckFeatureSupport = 33,
        GetPrivateData = 34,
        SetPrivateData = 35,
        SetPrivateDataInterface = 36,
        GetFeatureLevel = 37,
        GetCreationFlags = 38,
        GetDeviceRemovedReason = 39,
        GetImmediateContext = 40,
        SetExceptionMode = 41,
        GetExceptionMode = 42,
    }

    /// <summary>
    /// Direct3D 11 Hook - this hooks the SwapChain.Present to take screenshots
    /// </summary>
    internal class DXHookD3D11 : BaseDXHook
    {
        private static Int32 D3D11_DEVICE_METHOD_COUNT = Enum.GetNames(typeof(D3D11DeviceVirtualTableEnum)).Length;

        public DXHookD3D11(ClientInterface CaptureInterface) : base(CaptureInterface) { }

        private List<IntPtr> D3D11VirtualTableAddresses = null;
        private List<IntPtr> DXGISwapChainVirtualTableAddresses = null;

        private Hook<DXGISwapChain_PresentDelegate> DXGISwapChainPresentHook = null;
        private Hook<DXGISwapChain_ResizeTargetDelegate> DXGISwapChainResizeTargetHook = null;

        private Object AccessLock = new Object();

        #region Internal Device Resources

        private SharpDX.Direct3D11.Device Device;
        private SwapChain SwapChain;
        private RenderForm RenderForm;
        private Texture2D ResolvedRTShared;
        private KeyedMutex ResolvedRTSharedKeyedMutex;
        private ShaderResourceView ResolvedSharedSRV;
        private ScreenAlignedQuadRenderer ScreenAlignedQuad;
        private Texture2D FinalRT;
        private Texture2D ResizedRT;
        private RenderTargetView ResizedRTV;

        #endregion

        private Query Query;
        private Boolean QueryIssued;
        private Boolean FinalRTMapped;

        #region Main device resources

        private Texture2D ResolvedRT;
        private KeyedMutex ResolvedRTKeyedMutex;
        private KeyedMutex ResolvedRTKeyedMutex_Dev2;
        // private ShaderResourceView ResolvedSRV;

        #endregion

        protected override String HookName { get { return "DXHookD3D11"; } }

        public override void Hook()
        {
            DebugMessage("Hook: Begin");

            if (D3D11VirtualTableAddresses == null)
            {
                D3D11VirtualTableAddresses = new List<IntPtr>();
                DXGISwapChainVirtualTableAddresses = new List<IntPtr>();

                #region Get Device and SwapChain method addresses

                // Create temporary device + swapchain and determine method addresses
                RenderForm = ToDispose(new RenderForm());
                DebugMessage("Hook: Before device creation");
                SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport,
                    DXGI.CreateSwapChainDescription(RenderForm.Handle), out Device, out SwapChain);

                ToDispose(Device);
                ToDispose(SwapChain);

                if (Device != null && SwapChain != null)
                {
                    DebugMessage("Hook: Device created");
                    D3D11VirtualTableAddresses.AddRange(GetVirtualTableAddresses(Device.NativePointer, D3D11_DEVICE_METHOD_COUNT));
                    DXGISwapChainVirtualTableAddresses.AddRange(GetVirtualTableAddresses(SwapChain.NativePointer, DXGI.DXGI_SWAPCHAIN_METHOD_COUNT));
                }
                else
                {
                    DebugMessage("Hook: Device creation failed");
                }

                #endregion
            }

            // We will capture the backbuffer here
            DXGISwapChainPresentHook = new Hook<DXGISwapChain_PresentDelegate>(
                DXGISwapChainVirtualTableAddresses[(Int32)DXGI.DXGISwapChainVirtualTableEnum.Present],
                new DXGISwapChain_PresentDelegate(PresentHook), this);

            // We will capture target/window resizes here
            DXGISwapChainResizeTargetHook = new Hook<DXGISwapChain_ResizeTargetDelegate>(
                DXGISwapChainVirtualTableAddresses[(Int32)DXGI.DXGISwapChainVirtualTableEnum.ResizeTarget],
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
                    OverlayEngine.Dispose();
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
                OverlayEngine.Dispose();
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
                    if (OverlayEngine != null)
                        OverlayEngine.Dispose();

                    OverlayEngine = new DXOverlayEngine();
                    OverlayEngine.Overlays.Add(new Overlay
                    {
                        Elements =
                            {
                                // new Common.TextElement(new System.Drawing.Font("Times New Roman", 22)) { Text = "Test", Location = new System.Drawing.Point(200, 200), Color = System.Drawing.Color.Yellow, AntiAliased = false},
                                new Common.FramesPerSecond(new System.Drawing.Font("Arial", 16)) { Location = new System.Drawing.Point(5,5), Color = System.Drawing.Color.Red, AntiAliased = true },
                                // new Common.ImageElement(@"C:\Temp\test.bmp") { Location = new System.Drawing.Point(20, 20) }
                            }
                    });
                    OverlayEngine.Initialize(SwapChain);

                    SwapChainPointer = SwapChain.NativePointer;
                }

                // Draw Overlay(s)
                else if (OverlayEngine != null)
                {
                    foreach (IOverlay Overlay in OverlayEngine.Overlays)
                        Overlay.Frame();

                    OverlayEngine.Draw();
                }
            }
            catch (Exception Ex)
            {
                // If there is an error we do not want to crash the hooked application, so swallow the exception
                this.DebugMessage("PresentHook: Exeception: " + Ex.GetType().FullName + ": " + Ex.ToString());
                // return unchecked((Int32)0x8000FFFF); //E_UNEXPECTED
            }

            // As always we need to call the original method, note that EasyHook will automatically skip the hook and call the original method
            // i.e. calling it here will not cause a stack overflow into this function
            return DXGISwapChainPresentHook.Original(SwapChainPtr, SyncInterval, Flags);
        }

        DXOverlayEngine OverlayEngine;

        IntPtr SwapChainPointer = IntPtr.Zero;

    } // End class

} // End namespace