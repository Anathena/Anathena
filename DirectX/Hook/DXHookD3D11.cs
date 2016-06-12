using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX;
using System.Runtime.InteropServices;
using EasyHook;
using System.Threading;
using System.IO;
using Capture.Interface;
using SharpDX.Direct3D;

namespace Capture.Hook
{
    enum D3D11DeviceVTbl : short
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
    internal class DXHookD3D11: BaseDXHook
    {
        const int D3D11_DEVICE_METHOD_COUNT = 43;

        public DXHookD3D11(CaptureInterface ssInterface)
            : base(ssInterface)
        {
        }

        List<IntPtr> _d3d11VTblAddresses = null;
        List<IntPtr> _dxgiSwapChainVTblAddresses = null;

        Hook<DXGISwapChain_PresentDelegate> DXGISwapChain_PresentHook = null;
        Hook<DXGISwapChain_ResizeTargetDelegate> DXGISwapChain_ResizeTargetHook = null;

        object _lock = new object();

        #region Internal device resources
        SharpDX.Direct3D11.Device _device;
        SwapChain _swapChain;
        SharpDX.Windows.RenderForm _renderForm;
        Texture2D _resolvedRTShared;
        SharpDX.DXGI.KeyedMutex _resolvedRTSharedKeyedMutex;
        ShaderResourceView _resolvedSharedSRV;
        Capture.Hook.DX11.ScreenAlignedQuadRenderer _saQuad;
        Texture2D _finalRT;
        Texture2D _resizedRT;
        RenderTargetView _resizedRTV;
        #endregion

        Query _query;
        bool _queryIssued;
        bool _finalRTMapped;
        ScreenshotRequest _requestCopy;

        #region Main device resources
        Texture2D _resolvedRT;
        SharpDX.DXGI.KeyedMutex _resolvedRTKeyedMutex;
        SharpDX.DXGI.KeyedMutex _resolvedRTKeyedMutex_Dev2;
        //ShaderResourceView _resolvedSRV;
        #endregion

        protected override string HookName
        {
            get
            {
                return "DXHookD3D11";
            }
        }

        public override void Hook()
        {
            this.DebugMessage("Hook: Begin");
            if (_d3d11VTblAddresses == null)
            {
                _d3d11VTblAddresses = new List<IntPtr>();
                _dxgiSwapChainVTblAddresses = new List<IntPtr>();

                #region Get Device and SwapChain method addresses
                // Create temporary device + swapchain and determine method addresses
                _renderForm = ToDispose(new SharpDX.Windows.RenderForm());
                this.DebugMessage("Hook: Before device creation");
                SharpDX.Direct3D11.Device.CreateWithSwapChain(
                    DriverType.Hardware,
                    DeviceCreationFlags.BgraSupport,
                    DXGI.CreateSwapChainDescription(_renderForm.Handle),
                    out _device,
                    out _swapChain);

                ToDispose(_device);
                ToDispose(_swapChain);

                if (_device != null && _swapChain != null)
                {
                    this.DebugMessage("Hook: Device created");
                    _d3d11VTblAddresses.AddRange(GetVTblAddresses(_device.NativePointer, D3D11_DEVICE_METHOD_COUNT));
                    _dxgiSwapChainVTblAddresses.AddRange(GetVTblAddresses(_swapChain.NativePointer, DXGI.DXGI_SWAPCHAIN_METHOD_COUNT));
                }
                else
                {
                    this.DebugMessage("Hook: Device creation failed");
                }
                #endregion
            }

            // We will capture the backbuffer here
            DXGISwapChain_PresentHook = new Hook<DXGISwapChain_PresentDelegate>(
                _dxgiSwapChainVTblAddresses[(int)DXGI.DXGISwapChainVTbl.Present],
                new DXGISwapChain_PresentDelegate(PresentHook),
                this);
            
            // We will capture target/window resizes here
            DXGISwapChain_ResizeTargetHook = new Hook<DXGISwapChain_ResizeTargetDelegate>(
                _dxgiSwapChainVTblAddresses[(int)DXGI.DXGISwapChainVTbl.ResizeTarget],
                new DXGISwapChain_ResizeTargetDelegate(ResizeTargetHook),
                this);

            /*
             * Don't forget that all hooks will start deactivated...
             * The following ensures that all threads are intercepted:
             * Note: you must do this for each hook.
             */
            DXGISwapChain_PresentHook.Activate();
            
            DXGISwapChain_ResizeTargetHook.Activate();

            Hooks.Add(DXGISwapChain_PresentHook);
            Hooks.Add(DXGISwapChain_ResizeTargetHook);
        }

        public override void Cleanup()
        {
            try
            {
                if (_overlayEngine != null)
                {
                    _overlayEngine.Dispose();
                    _overlayEngine = null;
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
        delegate int DXGISwapChain_PresentDelegate(IntPtr swapChainPtr, int syncInterval, /* int */ SharpDX.DXGI.PresentFlags flags);

        /// <summary>
        /// The IDXGISwapChain.ResizeTarget function definition
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int DXGISwapChain_ResizeTargetDelegate(IntPtr swapChainPtr, ref ModeDescription newTargetParameters);

        /// <summary>
        /// Hooked to allow resizing a texture/surface that is reused. Currently not in use as we create the texture for each request
        /// to support different sizes each time (as we use DirectX to copy only the region we are after rather than the entire backbuffer)
        /// </summary>
        /// <param name="swapChainPtr"></param>
        /// <param name="newTargetParameters"></param>
        /// <returns></returns>
        int ResizeTargetHook(IntPtr swapChainPtr, ref ModeDescription newTargetParameters)
        {
            // Dispose of overlay engine (so it will be recreated with correct renderTarget view size)
            if (_overlayEngine != null)
            {
                _overlayEngine.Dispose();
                _overlayEngine = null;
            }

            return DXGISwapChain_ResizeTargetHook.Original(swapChainPtr, ref newTargetParameters);
        }

        void EnsureResources(SharpDX.Direct3D11.Device device, Texture2DDescription description, Rectangle captureRegion, ScreenshotRequest request)
        {
            if (_device != null && request.Resize != null && (_resizedRT == null || (_resizedRT.Device.NativePointer != _device.NativePointer || _resizedRT.Description.Width != request.Resize.Value.Width || _resizedRT.Description.Height != request.Resize.Value.Height)))
            {
                // Create/Recreate resources for resizing
                RemoveAndDispose(ref _resizedRT);
                RemoveAndDispose(ref _resizedRTV);
                RemoveAndDispose(ref _saQuad);

                _resizedRT = ToDispose(new Texture2D(_device, new Texture2DDescription() {
                    Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm, // Supports BMP/PNG/etc
                    Height = request.Resize.Value.Height,
                    Width = request.Resize.Value.Width,
                    ArraySize = 1,
                    SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                    BindFlags = BindFlags.RenderTarget,
                    MipLevels = 1,
                    Usage = ResourceUsage.Default,
                    OptionFlags = ResourceOptionFlags.None
                }));

                _resizedRTV = ToDispose(new RenderTargetView(_device, _resizedRT));

                _saQuad = ToDispose(new DX11.ScreenAlignedQuadRenderer());
                _saQuad.Initialize(new DX11.DeviceManager(_device));
            }

            // Check if _resolvedRT or _finalRT require creation
            if (_finalRT != null && _finalRT.Device.NativePointer == _device.NativePointer &&
                _finalRT.Description.Height == captureRegion.Height && _finalRT.Description.Width == captureRegion.Width &&
                _resolvedRT != null && _resolvedRT.Description.Height == description.Height && _resolvedRT.Description.Width == description.Width &&
                _resolvedRT.Device.NativePointer == device.NativePointer && _resolvedRT.Description.Format == description.Format
                )
            {
                return;
            }

            RemoveAndDispose(ref _query);
            RemoveAndDispose(ref _resolvedRT);
            RemoveAndDispose(ref _resolvedSharedSRV);
            RemoveAndDispose(ref _finalRT);
            RemoveAndDispose(ref _resolvedRTShared);

            _query = new Query(_device, new QueryDescription()
            {
                Flags = QueryFlags.None,
                Type = QueryType.Event
            });
            _queryIssued = false;

            _resolvedRT = ToDispose(new Texture2D(device, new Texture2DDescription() {
                CpuAccessFlags = CpuAccessFlags.None,
                Format = description.Format, // for multisampled backbuffer, this must be same format
                Height = description.Height,
                Usage = ResourceUsage.Default,
                Width = description.Width,
                ArraySize = 1,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0), // Ensure single sample
                BindFlags = BindFlags.ShaderResource,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.SharedKeyedmutex
            }));

            // Retrieve reference to the keyed mutex
            _resolvedRTKeyedMutex = ToDispose(_resolvedRT.QueryInterfaceOrNull<SharpDX.DXGI.KeyedMutex>());

            using (var resource = _resolvedRT.QueryInterface<SharpDX.DXGI.Resource>())
            {
                _resolvedRTShared = ToDispose(_device.OpenSharedResource<Texture2D>(resource.SharedHandle));
                _resolvedRTKeyedMutex_Dev2 = ToDispose(_resolvedRTShared.QueryInterfaceOrNull<SharpDX.DXGI.KeyedMutex>());
            }

            // SRV for use if resizing
            _resolvedSharedSRV = ToDispose(new ShaderResourceView(_device, _resolvedRTShared));

            _finalRT = ToDispose(new Texture2D(_device, new Texture2DDescription()
            {
                CpuAccessFlags = CpuAccessFlags.Read,
                Format = description.Format,
                Height = captureRegion.Height,
                Usage = ResourceUsage.Staging,
                Width = captureRegion.Width,
                ArraySize = 1,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                BindFlags = BindFlags.None,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None
            }));
            _finalRTMapped = false;
        }

        /// <summary>
        /// Our present hook that will grab a copy of the backbuffer when requested. Note: this supports multi-sampling (anti-aliasing)
        /// </summary>
        /// <param name="swapChainPtr"></param>
        /// <param name="syncInterval"></param>
        /// <param name="flags"></param>
        /// <returns>The HRESULT of the original method</returns>
        int PresentHook(IntPtr swapChainPtr, int syncInterval, SharpDX.DXGI.PresentFlags flags)
        {
            this.Frame();
            SwapChain swapChain = (SharpDX.DXGI.SwapChain)swapChainPtr;
            try
            {
                #region Screenshot Request
                if (this.Request != null)
                {
                    this.DebugMessage("PresentHook: Request Start");
                    DateTime startTime = DateTime.Now;
                    using (Texture2D currentRT = Texture2D.FromSwapChain<Texture2D>(swapChain, 0))
                    {
                        #region Determine region to capture
                        Rectangle captureRegion = new Rectangle(0, 0, currentRT.Description.Width, currentRT.Description.Height);

                        if (this.Request.RegionToCapture.Width > 0)
                        {
                            captureRegion = new Rectangle(this.Request.RegionToCapture.Left, this.Request.RegionToCapture.Top, this.Request.RegionToCapture.Right, this.Request.RegionToCapture.Bottom);
                        }
                        else if (this.Request.Resize.HasValue)
                        {
                            captureRegion = new Rectangle(0, 0, this.Request.Resize.Value.Width, this.Request.Resize.Value.Height);
                        }
                        #endregion

                        // Create / Recreate resources as necessary
                        EnsureResources(currentRT.Device, currentRT.Description, captureRegion, Request);

                        Texture2D sourceTexture = null;

                        // If texture is multisampled, then we can use ResolveSubresource to copy it into a non-multisampled texture
                        if (currentRT.Description.SampleDescription.Count > 1 || Request.Resize.HasValue)
                        {
                            if (Request.Resize.HasValue)
                                this.DebugMessage("PresentHook: resizing texture");
                            else
                                this.DebugMessage("PresentHook: resolving multi-sampled texture");

                            // Resolve into _resolvedRT
                            if (_resolvedRTKeyedMutex != null)
                                _resolvedRTKeyedMutex.Acquire(0, int.MaxValue);
                            currentRT.Device.ImmediateContext.ResolveSubresource(currentRT, 0, _resolvedRT, 0, _resolvedRT.Description.Format);
                            if (_resolvedRTKeyedMutex != null)
                                _resolvedRTKeyedMutex.Release(1);

                            if (Request.Resize.HasValue)
                            {
                                lock(_lock)
                                {
                                    if (_resolvedRTKeyedMutex_Dev2 != null)
                                        _resolvedRTKeyedMutex_Dev2.Acquire(1, int.MaxValue);
                                    _saQuad.ShaderResource = _resolvedSharedSRV;
                                    _saQuad.RenderTargetView = _resizedRTV;
                                    _saQuad.RenderTarget = _resizedRT;
                                    _saQuad.Render();
                                    if (_resolvedRTKeyedMutex_Dev2 != null)
                                        _resolvedRTKeyedMutex_Dev2.Release(0);
                                }

                                // set sourceTexture to the resized RT
                                sourceTexture = _resizedRT;
                            }
                            else
                            {
                                // Make sourceTexture be the resolved texture
                                sourceTexture = _resolvedRTShared;
                            }
                        }
                        else
                        {
                            // Copy the resource into the shared texture
                            if (_resolvedRTKeyedMutex != null) _resolvedRTKeyedMutex.Acquire(0, int.MaxValue);
                            currentRT.Device.ImmediateContext.CopySubresourceRegion(currentRT, 0, null, _resolvedRT, 0);
                            if (_resolvedRTKeyedMutex != null) _resolvedRTKeyedMutex.Release(1);
                            sourceTexture = _resolvedRTShared;
                        }

                        // Copy to memory and send back to host process on a background thread so that we do not cause any delay in the rendering pipeline
                        _requestCopy = this.Request.Clone(); // this.Request gets set to null, so copy the Request for use in the thread

                        // Prevent the request from being processed a second time
                        this.Request = null;

                        bool acquireLock = sourceTexture == _resolvedRTShared;

                        ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
                        {
                            // Acquire lock on second device
                            if (acquireLock && _resolvedRTKeyedMutex_Dev2 != null)
                                _resolvedRTKeyedMutex_Dev2.Acquire(1, int.MaxValue);

                            lock (_lock)
                            {
                                // Copy the subresource region, we are dealing with a flat 2D texture with no MipMapping, so 0 is the subresource index
                                sourceTexture.Device.ImmediateContext.CopySubresourceRegion(sourceTexture, 0, new ResourceRegion()
                                {
                                    Top = captureRegion.Top,
                                    Bottom = captureRegion.Bottom,
                                    Left = captureRegion.Left,
                                    Right = captureRegion.Right,
                                    Front = 0,
                                    Back = 1 // Must be 1 or only black will be copied
                                }, _finalRT, 0, 0, 0, 0);

                                // Release lock upon shared surface on second device
                                if (acquireLock && _resolvedRTKeyedMutex_Dev2 != null)
                                    _resolvedRTKeyedMutex_Dev2.Release(0);

                                _finalRT.Device.ImmediateContext.End(_query);
                                _queryIssued = true;
                                while (!_finalRT.Device.ImmediateContext.GetData(_query).ReadBoolean())
                                {
                                    // Spin (usually no spin takes place)
                                }

                                DateTime startCopyToSystemMemory = DateTime.Now;
                                try
                                {
                                    DataBox db = default(DataBox);
                                    if (_requestCopy.Format == ImageFormat.PixelData)
                                    {
                                        db = _finalRT.Device.ImmediateContext.MapSubresource(_finalRT, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.DoNotWait);
                                        _finalRTMapped = true;
                                    }
                                    _queryIssued = false;

                                    try
                                    {
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            switch (_requestCopy.Format)
                                            {
                                                case ImageFormat.Bitmap:
                                                    Texture2D.ToStream(_finalRT.Device.ImmediateContext, _finalRT, ImageFileFormat.Bmp, ms);
                                                    break;
                                                case ImageFormat.Jpeg:
                                                    Texture2D.ToStream(_finalRT.Device.ImmediateContext, _finalRT, ImageFileFormat.Jpg, ms);
                                                    break;
                                                case ImageFormat.Png:
                                                    Texture2D.ToStream(_finalRT.Device.ImmediateContext, _finalRT, ImageFileFormat.Png, ms);
                                                    break;
                                                case ImageFormat.PixelData:
                                                    if (db.DataPointer != IntPtr.Zero)
                                                    {
                                                        ProcessCapture(_finalRT.Description.Width, _finalRT.Description.Height, db.RowPitch, System.Drawing.Imaging.PixelFormat.Format32bppArgb, db.DataPointer, _requestCopy);
                                                    }
                                                    return;
                                            }
                                            ms.Position = 0;
                                            ProcessCapture(ms, _requestCopy);
                                        }
                                    }
                                    finally
                                    {
                                        this.DebugMessage("PresentHook: Copy to System Memory time: " + (DateTime.Now - startCopyToSystemMemory).ToString());
                                    }

                                    if (_finalRTMapped)
                                    {
                                        lock (_lock)
                                        {
                                            _finalRT.Device.ImmediateContext.UnmapSubresource(_finalRT, 0);
                                            _finalRTMapped = false;
                                        }
                                    }
                                }
                                catch (SharpDX.SharpDXException exc)
                                {
                                    // Catch DXGI_ERROR_WAS_STILL_DRAWING and ignore - the data isn't available yet
                                }
                            }
                        }));
                        

                        // Note: it would be possible to capture multiple frames and process them in a background thread
                    }
                    this.DebugMessage("PresentHook: Copy BackBuffer time: " + (DateTime.Now - startTime).ToString());
                    this.DebugMessage("PresentHook: Request End");
                }
                #endregion

                #region Draw overlay (after screenshot so we don't capture overlay as well)
                if (this.Config.ShowOverlay)
                {
                    // Initialise Overlay Engine
                    if (_swapChainPointer != swapChain.NativePointer || _overlayEngine == null)
                    {
                        if (_overlayEngine != null)
                            _overlayEngine.Dispose();

                        _overlayEngine = new DX11.DXOverlayEngine();
                        _overlayEngine.Overlays.Add(new Capture.Hook.Common.Overlay
                        {
                            Elements =
                            {
                                //new Capture.Hook.Common.TextElement(new System.Drawing.Font("Times New Roman", 22)) { Text = "Test", Location = new System.Drawing.Point(200, 200), Color = System.Drawing.Color.Yellow, AntiAliased = false},
                                new Capture.Hook.Common.FramesPerSecond(new System.Drawing.Font("Arial", 16)) { Location = new System.Drawing.Point(5,5), Color = System.Drawing.Color.Red, AntiAliased = true },
                                //new Capture.Hook.Common.ImageElement(@"C:\Temp\test.bmp") { Location = new System.Drawing.Point(20, 20) }
                            }
                        });
                        _overlayEngine.Initialise(swapChain);

                        _swapChainPointer = swapChain.NativePointer;
                    }
                    // Draw Overlay(s)
                    else if (_overlayEngine != null)
                    {
                        foreach (var overlay in _overlayEngine.Overlays)
                            overlay.Frame();
                        _overlayEngine.Draw();
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                // If there is an error we do not want to crash the hooked application, so swallow the exception
                this.DebugMessage("PresentHook: Exeception: " + e.GetType().FullName + ": " + e.ToString());
                //return unchecked((int)0x8000FFFF); //E_UNEXPECTED
            }

            // As always we need to call the original method, note that EasyHook will automatically skip the hook and call the original method
            // i.e. calling it here will not cause a stack overflow into this function
            return DXGISwapChain_PresentHook.Original(swapChainPtr, syncInterval, flags);
        }

        Capture.Hook.DX11.DXOverlayEngine _overlayEngine;

        IntPtr _swapChainPointer = IntPtr.Zero;
        
    }
}
