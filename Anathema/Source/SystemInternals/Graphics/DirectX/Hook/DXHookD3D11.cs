using DirectXHook.Hook.Common;
using DirectXHook.Hook.DX11;
using DirectXHook.Interface;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace DirectXHook.Hook
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
        private ScreenshotRequest RequestCopy;

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

        void EnsureResources(SharpDX.Direct3D11.Device Device, Texture2DDescription Description, Rectangle CaptureRegion, ScreenshotRequest Request)
        {
            if (this.Device != null && Request.Resize != null && (ResizedRT == null || (ResizedRT.Device.NativePointer != this.Device.NativePointer || ResizedRT.Description.Width != Request.Resize.Value.Width || ResizedRT.Description.Height != Request.Resize.Value.Height)))
            {
                // Create/Recreate resources for resizing
                RemoveAndDispose(ref ResizedRT);
                RemoveAndDispose(ref ResizedRTV);
                RemoveAndDispose(ref ScreenAlignedQuad);

                ResizedRT = ToDispose(new Texture2D(this.Device, new Texture2DDescription()
                {
                    Format = Format.R8G8B8A8_UNorm, // Supports BMP/PNG/etc
                    Height = Request.Resize.Value.Height,
                    Width = Request.Resize.Value.Width,
                    ArraySize = 1,
                    SampleDescription = new SampleDescription(1, 0),
                    BindFlags = BindFlags.RenderTarget,
                    MipLevels = 1,
                    Usage = ResourceUsage.Default,
                    OptionFlags = ResourceOptionFlags.None
                }));

                ResizedRTV = ToDispose(new RenderTargetView(this.Device, ResizedRT));

                ScreenAlignedQuad = ToDispose(new ScreenAlignedQuadRenderer());
                ScreenAlignedQuad.Initialize(new DeviceManager(this.Device));
            }

            // Check if _resolvedRT or _finalRT require creation
            if (FinalRT != null && FinalRT.Device.NativePointer == this.Device.NativePointer &&
                FinalRT.Description.Height == CaptureRegion.Height && FinalRT.Description.Width == CaptureRegion.Width &&
                ResolvedRT != null && ResolvedRT.Description.Height == Description.Height && ResolvedRT.Description.Width == Description.Width &&
                ResolvedRT.Device.NativePointer == Device.NativePointer && ResolvedRT.Description.Format == Description.Format)
            {
                return;
            }

            RemoveAndDispose(ref Query);
            RemoveAndDispose(ref ResolvedRT);
            RemoveAndDispose(ref ResolvedSharedSRV);
            RemoveAndDispose(ref FinalRT);
            RemoveAndDispose(ref ResolvedRTShared);

            Query = new Query(this.Device, new QueryDescription()
            {
                Flags = QueryFlags.None,
                Type = QueryType.Event
            });
            QueryIssued = false;

            ResolvedRT = ToDispose(new Texture2D(Device, new Texture2DDescription()
            {
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Description.Format, // For multisampled backbuffer, this must be same format
                Height = Description.Height,
                Usage = ResourceUsage.Default,
                Width = Description.Width,
                ArraySize = 1,
                SampleDescription = new SampleDescription(1, 0), // Ensure single sample
                BindFlags = BindFlags.ShaderResource,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.SharedKeyedmutex
            }));

            // Retrieve reference to the keyed mutex
            ResolvedRTKeyedMutex = ToDispose(ResolvedRT.QueryInterfaceOrNull<KeyedMutex>());

            using (SharpDX.DXGI.Resource Resource = ResolvedRT.QueryInterface<SharpDX.DXGI.Resource>())
            {
                ResolvedRTShared = ToDispose(this.Device.OpenSharedResource<Texture2D>(Resource.SharedHandle));
                ResolvedRTKeyedMutex_Dev2 = ToDispose(ResolvedRTShared.QueryInterfaceOrNull<KeyedMutex>());
            }

            // SRV for use if resizing
            ResolvedSharedSRV = ToDispose(new ShaderResourceView(this.Device, ResolvedRTShared));

            FinalRT = ToDispose(new Texture2D(this.Device, new Texture2DDescription()
            {
                CpuAccessFlags = CpuAccessFlags.Read,
                Format = Description.Format,
                Height = CaptureRegion.Height,
                Usage = ResourceUsage.Staging,
                Width = CaptureRegion.Width,
                ArraySize = 1,
                SampleDescription = new SampleDescription(1, 0),
                BindFlags = BindFlags.None,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None
            }));
            FinalRTMapped = false;
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
                #region Screenshot Request

                if (Request != null)
                {
                    DebugMessage("PresentHook: Request Start");
                    DateTime StartTime = DateTime.Now;

                    using (Texture2D CurrentRT = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0))
                    {
                        #region Determine region to capture

                        Rectangle CaptureRegion = new Rectangle(0, 0, CurrentRT.Description.Width, CurrentRT.Description.Height);

                        if (Request.Region.Width > 0)
                        {
                            CaptureRegion = new Rectangle(Request.Region.Left, Request.Region.Top, Request.Region.Right, Request.Region.Bottom);
                        }
                        else if (Request.Resize.HasValue)
                        {
                            CaptureRegion = new Rectangle(0, 0, Request.Resize.Value.Width, Request.Resize.Value.Height);
                        }

                        #endregion

                        // Create / Recreate resources as necessary
                        EnsureResources(CurrentRT.Device, CurrentRT.Description, CaptureRegion, Request);

                        Texture2D SourceTexture = null;

                        // If texture is multisampled, then we can use ResolveSubresource to copy it into a non-multisampled texture
                        if (CurrentRT.Description.SampleDescription.Count > 1 || Request.Resize.HasValue)
                        {
                            if (Request.Resize.HasValue)
                                DebugMessage("PresentHook: resizing texture");
                            else
                                DebugMessage("PresentHook: resolving multi-sampled texture");

                            // Resolve into _resolvedRT
                            if (ResolvedRTKeyedMutex != null)
                                ResolvedRTKeyedMutex.Acquire(0, Int32.MaxValue);

                            CurrentRT.Device.ImmediateContext.ResolveSubresource(CurrentRT, 0, ResolvedRT, 0, ResolvedRT.Description.Format);

                            if (ResolvedRTKeyedMutex != null)
                                ResolvedRTKeyedMutex.Release(1);

                            if (Request.Resize.HasValue)
                            {
                                lock (AccessLock)
                                {
                                    if (ResolvedRTKeyedMutex_Dev2 != null)
                                        ResolvedRTKeyedMutex_Dev2.Acquire(1, Int32.MaxValue);

                                    ScreenAlignedQuad.ShaderResource = ResolvedSharedSRV;
                                    ScreenAlignedQuad.RenderTargetView = ResizedRTV;
                                    ScreenAlignedQuad.RenderTarget = ResizedRT;
                                    ScreenAlignedQuad.Render();

                                    if (ResolvedRTKeyedMutex_Dev2 != null)
                                        ResolvedRTKeyedMutex_Dev2.Release(0);
                                }

                                // set sourceTexture to the resized RT
                                SourceTexture = ResizedRT;
                            }
                            else
                            {
                                // Make sourceTexture be the resolved texture
                                SourceTexture = ResolvedRTShared;
                            }
                        }
                        else
                        {
                            // Copy the resource into the shared texture
                            if (ResolvedRTKeyedMutex != null)
                                ResolvedRTKeyedMutex.Acquire(0, Int32.MaxValue);

                            CurrentRT.Device.ImmediateContext.CopySubresourceRegion(CurrentRT, 0, null, ResolvedRT, 0);

                            if (ResolvedRTKeyedMutex != null)
                                ResolvedRTKeyedMutex.Release(1);

                            SourceTexture = ResolvedRTShared;
                        }

                        // Copy to memory and send back to host process on a background thread so that we do not cause any delay in the rendering pipeline
                        RequestCopy = Request.Clone(); // this.Request gets set to null, so copy the Request for use in the thread

                        // Prevent the request from being processed a second time
                        Request = null;

                        Boolean AcquireLock = SourceTexture == ResolvedRTShared;

                        ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
                        {
                            // Acquire lock on second device
                            if (AcquireLock && ResolvedRTKeyedMutex_Dev2 != null)
                                ResolvedRTKeyedMutex_Dev2.Acquire(1, Int32.MaxValue);

                            lock (AccessLock)
                            {
                                // Copy the subresource region, we are dealing with a flat 2D texture with no MipMapping, so 0 is the subresource index
                                SourceTexture.Device.ImmediateContext.CopySubresourceRegion(SourceTexture, 0, new ResourceRegion()
                                {
                                    Top = CaptureRegion.Top,
                                    Bottom = CaptureRegion.Bottom,
                                    Left = CaptureRegion.Left,
                                    Right = CaptureRegion.Right,
                                    Front = 0,
                                    Back = 1 // Must be 1 or only black will be copied
                                }, FinalRT, 0, 0, 0, 0);

                                // Release lock upon shared surface on second device
                                if (AcquireLock && ResolvedRTKeyedMutex_Dev2 != null)
                                    ResolvedRTKeyedMutex_Dev2.Release(0);

                                FinalRT.Device.ImmediateContext.End(Query);
                                QueryIssued = true;

                                while (!FinalRT.Device.ImmediateContext.GetData(Query).ReadBoolean())
                                {
                                    // Spin (usually no spin takes place)
                                }

                                DateTime StartCopyToSystemMemory = DateTime.Now;
                                try
                                {
                                    DataBox DataBox = default(DataBox);
                                    if (RequestCopy.Format == ImageFormatEnum.PixelData)
                                    {
                                        DataBox = FinalRT.Device.ImmediateContext.MapSubresource(FinalRT, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.DoNotWait);
                                        FinalRTMapped = true;
                                    }
                                    QueryIssued = false;

                                    try
                                    {
                                        using (MemoryStream Stream = new MemoryStream())
                                        {
                                            switch (RequestCopy.Format)
                                            {
                                                case ImageFormatEnum.Bitmap:
                                                    Texture2D.ToStream(FinalRT.Device.ImmediateContext, FinalRT, ImageFileFormat.Bmp, Stream);
                                                    break;
                                                case ImageFormatEnum.Jpeg:
                                                    Texture2D.ToStream(FinalRT.Device.ImmediateContext, FinalRT, ImageFileFormat.Jpg, Stream);
                                                    break;
                                                case ImageFormatEnum.Png:
                                                    Texture2D.ToStream(FinalRT.Device.ImmediateContext, FinalRT, ImageFileFormat.Png, Stream);
                                                    break;
                                                case ImageFormatEnum.PixelData:
                                                    if (DataBox.DataPointer != IntPtr.Zero)
                                                    {
                                                        ProcessCapture(FinalRT.Description.Width, FinalRT.Description.Height, DataBox.RowPitch, System.Drawing.Imaging.PixelFormat.Format32bppArgb, DataBox.DataPointer, RequestCopy);
                                                    }
                                                    return;
                                            }
                                            Stream.Position = 0;
                                            ProcessCapture(Stream, RequestCopy);
                                        }
                                    }
                                    finally
                                    {
                                        DebugMessage("PresentHook: Copy to System Memory time: " + (DateTime.Now - StartCopyToSystemMemory).ToString());
                                    }

                                    if (FinalRTMapped)
                                    {
                                        lock (AccessLock)
                                        {
                                            FinalRT.Device.ImmediateContext.UnmapSubresource(FinalRT, 0);
                                            FinalRTMapped = false;
                                        }
                                    }
                                }
                                catch (SharpDXException Ex)
                                {
                                    // Catch DXGI_ERROR_WAS_STILL_DRAWING and ignore - the data isn't available yet
                                }
                            }
                        }));


                        // Note: it would be possible to capture multiple frames and process them in a background thread
                    }
                    DebugMessage("PresentHook: Copy BackBuffer time: " + (DateTime.Now - StartTime).ToString());
                    DebugMessage("PresentHook: Request End");

                }
                #endregion

                #region Draw overlay (after screenshot so we don't capture overlay as well)

                if (Config.ShowOverlay)
                {
                    // Initialize Overlay Engine
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
                #endregion
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