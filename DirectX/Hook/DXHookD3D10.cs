using DirectXShell.Interface;
using SharpDX;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Device = SharpDX.Direct3D10.Device;

namespace DirectXShell.Hook
{
    enum D3D10DeviceVirtualTableEnum : Int16
    {
        // IUnknown
        QueryInterface = 0,
        AddRef = 1,
        Release = 2,

        // ID3D10Device
        VSSetConstantBuffers = 3,
        PSSetShaderResources = 4,
        PSSetShader = 5,
        PSSetSamplers = 6,
        VSSetShader = 7,
        DrawIndexed = 8,
        Draw = 9,
        PSSetConstantBuffers = 10,
        IASetInputLayout = 11,
        IASetVertexBuffers = 12,
        IASetIndexBuffer = 13,
        DrawIndexedInstanced = 14,
        DrawInstanced = 15,
        GSSetConstantBuffers = 16,
        GSSetShader = 17,
        IASetPrimitiveTopology = 18,
        VSSetShaderResources = 19,
        VSSetSamplers = 20,
        SetPredication = 21,
        GSSetShaderResources = 22,
        GSSetSamplers = 23,
        OMSetRenderTargets = 24,
        OMSetBlendState = 25,
        OMSetDepthStencilState = 26,
        SOSetTargets = 27,
        DrawAuto = 28,
        RSSetState = 29,
        RSSetViewports = 30,
        RSSetScissorRects = 31,
        CopySubresourceRegion = 32,
        CopyResource = 33,
        UpdateSubresource = 34,
        ClearRenderTargetView = 35,
        ClearDepthStencilView = 36,
        GenerateMips = 37,
        ResolveSubresource = 38,
        VSGetConstantBuffers = 39,
        PSGetShaderResources = 40,
        PSGetShader = 41,
        PSGetSamplers = 42,
        VSGetShader = 43,
        PSGetConstantBuffers = 44,
        IAGetInputLayout = 45,
        IAGetVertexBuffers = 46,
        IAGetIndexBuffer = 47,
        GSGetConstantBuffers = 48,
        GSGetShader = 49,
        IAGetPrimitiveTopology = 50,
        VSGetShaderResources = 51,
        VSGetSamplers = 52,
        GetPredication = 53,
        GSGetShaderResources = 54,
        GSGetSamplers = 55,
        OMGetRenderTargets = 56,
        OMGetBlendState = 57,
        OMGetDepthStencilState = 58,
        SOGetTargets = 59,
        RSGetState = 60,
        RSGetViewports = 61,
        RSGetScissorRects = 62,
        GetDeviceRemovedReason = 63,
        SetExceptionMode = 64,
        GetExceptionMode = 65,
        GetPrivateData = 66,
        SetPrivateData = 67,
        SetPrivateDataInterface = 68,
        ClearState = 69,
        Flush = 70,
        CreateBuffer = 71,
        CreateTexture1D = 72,
        CreateTexture2D = 73,
        CreateTexture3D = 74,
        CreateShaderResourceView = 75,
        CreateRenderTargetView = 76,
        CreateDepthStencilView = 77,
        CreateInputLayout = 78,
        CreateVertexShader = 79,
        CreateGeometryShader = 80,
        CreateGemoetryShaderWithStreamOutput = 81,
        CreatePixelShader = 82,
        CreateBlendState = 83,
        CreateDepthStencilState = 84,
        CreateRasterizerState = 85,
        CreateSamplerState = 86,
        CreateQuery = 87,
        CreatePredicate = 88,
        CreateCounter = 89,
        CheckFormatSupport = 90,
        CheckMultisampleQualityLevels = 91,
        CheckCounterInfo = 92,
        CheckCounter = 93,
        GetCreationFlags = 94,
        OpenSharedResource = 95,
        SetTextFilterSize = 96,
        GetTextFilterSize = 97,
    }

    /// <summary>
    /// Direct3D 10 Hook - this hooks the SwapChain.Present method to capture images
    /// </summary>
    internal class DXHookD3D10 : BaseDXHook
    {
        private static Int32 D3D10_DEVICE_METHOD_COUNT = Enum.GetNames(typeof(D3D10DeviceVirtualTableEnum)).Length;

        private List<IntPtr> D3D10VirtualTableAddresses;
        private List<IntPtr> DXGISwapChainVirtualTableAddresses;
        private Hook<DXGISwapChain_PresentDelegate> DXGISwapChainPresentHook;
        private Hook<DXGISwapChain_ResizeTargetDelegate> DXGISwapChainResizeTargetHook;

        public DXHookD3D10(CaptureInterface CaptureInterface) : base(CaptureInterface)
        {
            D3D10VirtualTableAddresses = null;
            DXGISwapChainVirtualTableAddresses = null;
            DXGISwapChainPresentHook = null;
            DXGISwapChainResizeTargetHook = null;

            DebugMessage("Create");
        }

        protected override String HookName { get { return "DXHookD3D10"; } }

        public override void Hook()
        {
            DebugMessage("Hook: Begin");

            // Determine method addresses in Direct3D10.Device, and DXGI.SwapChain
            if (D3D10VirtualTableAddresses == null)
            {
                D3D10VirtualTableAddresses = new List<IntPtr>();
                DXGISwapChainVirtualTableAddresses = new List<IntPtr>();

                DebugMessage("Hook: Before device creation");

                using (Factory Factory = new Factory())
                {
                    using (Device Device = new Device(Factory.GetAdapter(0), DeviceCreationFlags.None))
                    {
                        DebugMessage("Hook: Device created");

                        D3D10VirtualTableAddresses.AddRange(GetVirtualTableAddresses(Device.NativePointer, D3D10_DEVICE_METHOD_COUNT));

                        using (Form RenderForm = new Form())
                        {
                            using (SwapChain SwapChain = new SwapChain(Factory, Device, DXGI.CreateSwapChainDescription(RenderForm.Handle)))
                            {
                                DXGISwapChainVirtualTableAddresses.AddRange(GetVirtualTableAddresses(SwapChain.NativePointer, DXGI.DXGI_SWAPCHAIN_METHOD_COUNT));
                            }
                        }
                    }
                }
            }

            // We will capture the backbuffer here
            DXGISwapChainPresentHook = new Hook<DXGISwapChain_PresentDelegate>(
                DXGISwapChainVirtualTableAddresses[(Int32)DXGI.DXGISwapChainVirtualTableEnum.Present],
                new DXGISwapChain_PresentDelegate(PresentHook), this);

            // We will capture target/window resizes here
            DXGISwapChainResizeTargetHook = new Hook<DXGISwapChain_ResizeTargetDelegate>(
                DXGISwapChainVirtualTableAddresses[(Int32)DXGI.DXGISwapChainVirtualTableEnum.ResizeTarget],
                new DXGISwapChain_ResizeTargetDelegate(ResizeTargetHook), this);

            // The following ensures that all threads are intercepted: (Must be done for each hook)
            DXGISwapChainPresentHook.Activate();
            DXGISwapChainResizeTargetHook.Activate();

            Hooks.Add(DXGISwapChainPresentHook);
            Hooks.Add(DXGISwapChainResizeTargetHook);
        }

        public override void Cleanup() { }

        /// <summary>
        /// The IDXGISwapChain.Present function definition
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate Int32 DXGISwapChain_PresentDelegate(IntPtr SwapChainPtr, Int32 SyncInterval, PresentFlags Flags);

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
            SwapChain SwapChain = (SwapChain)SwapChainPtr;

            // This version creates a new texture for each request so there is nothing to resize.
            // IF the size of the texture is known each time, we could create it once, and then possibly need to resize it here
            SwapChain.ResizeTarget(ref NewTargetParameters);

            return Result.Ok.Code;
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
            Frame();
            SwapChain SwapChain = (SwapChain)SwapChainPtr;

            try
            {
                #region Screenshot Request

                if (Request != null)
                {
                    try
                    {
                        DebugMessage("PresentHook: Request Start");

                        DateTime StartTime = DateTime.Now;

                        using (Texture2D Texture = Texture2D.FromSwapChain<SharpDX.Direct3D10.Texture2D>(SwapChain, 0))
                        {
                            #region Determine region to capture

                            System.Drawing.Rectangle RegionToCapture = new System.Drawing.Rectangle(0, 0, Texture.Description.Width, Texture.Description.Height);

                            if (base.Request.Region.Width > 0)
                            {
                                RegionToCapture = this.Request.Region;
                            }

                            #endregion

                            Texture2D TheTexture = Texture;

                            // If texture is multisampled, then we can use ResolveSubresource to copy it into a non-multisampled texture
                            Texture2D TextureResolved = null;
                            if (Texture.Description.SampleDescription.Count > 1)
                            {
                                DebugMessage("PresentHook: resolving multi-sampled texture");

                                // texture is multi-sampled, lets resolve it down to single sample
                                TextureResolved = new Texture2D(Texture.Device, new Texture2DDescription()
                                {
                                    CpuAccessFlags = CpuAccessFlags.None,
                                    Format = Texture.Description.Format,
                                    Height = Texture.Description.Height,
                                    Usage = ResourceUsage.Default,
                                    Width = Texture.Description.Width,
                                    ArraySize = 1,
                                    SampleDescription = new SampleDescription(1, 0), // Ensure single sample
                                    BindFlags = BindFlags.None,
                                    MipLevels = 1,
                                    OptionFlags = Texture.Description.OptionFlags
                                });

                                // Resolve into TextureResolved
                                Texture.Device.ResolveSubresource(Texture, 0, TextureResolved, 0, Texture.Description.Format);

                                // Make "TheTexture" be the resolved texture
                                TheTexture = TextureResolved;
                            }

                            // Create destination texture
                            Texture2D textureDest = new Texture2D(Texture.Device, new Texture2DDescription()
                            {
                                CpuAccessFlags = CpuAccessFlags.None,// CpuAccessFlags.Write | CpuAccessFlags.Read,
                                Format = Format.R8G8B8A8_UNorm, // Supports BMP/PNG
                                Height = RegionToCapture.Height,
                                Usage = ResourceUsage.Default, // ResourceUsage.Staging,
                                Width = RegionToCapture.Width,
                                ArraySize = 1, // Texture.Description.ArraySize,
                                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0), // Texture.Description.SampleDescription,
                                BindFlags = BindFlags.None,
                                MipLevels = 1, // Texture.Description.MipLevels,
                                OptionFlags = Texture.Description.OptionFlags
                            });

                            // Copy the subresource region, we are dealing with a flat 2D texture with no MipMapping, so 0 is the subresource index
                            TheTexture.Device.CopySubresourceRegion(TheTexture, 0, new ResourceRegion()
                            {
                                Top = RegionToCapture.Top,
                                Bottom = RegionToCapture.Bottom,
                                Left = RegionToCapture.Left,
                                Right = RegionToCapture.Right,
                                Front = 0,
                                Back = 1 // Must be 1 or only black will be copied
                            }, textureDest, 0, 0, 0, 0);

                            // Note: it would be possible to capture multiple frames and process them in a background thread

                            // Copy to memory and send back to host process on a background thread so that we do not cause any delay in the rendering pipeline
                            ScreenshotRequest Request = this.Request.Clone(); // this.Request gets set to null, so copy the Request for use in the thread
                            ThreadPool.QueueUserWorkItem(delegate
                            {
                                //FileStream fs = new FileStream(@"c:\temp\temp.bmp", FileMode.Create);
                                //Texture2D.ToStream(testSubResourceCopy, ImageFileFormat.Bmp, fs);

                                DateTime StartCopyToSystemMemory = DateTime.Now;
                                using (MemoryStream Stream = new MemoryStream())
                                {
                                    Texture2D.ToStream(textureDest, ImageFileFormat.Bmp, Stream);
                                    Stream.Position = 0;

                                    DebugMessage("PresentHook: Copy to System Memory time: " + (DateTime.Now - StartCopyToSystemMemory).ToString());

                                    DateTime StartSendResponse = DateTime.Now;
                                    ProcessCapture(Stream, Request);

                                    DebugMessage("PresentHook: Send response time: " + (DateTime.Now - StartSendResponse).ToString());
                                }

                                // Free the textureDest as we no longer need it.
                                textureDest.Dispose();
                                textureDest = null;

                                DebugMessage("PresentHook: Full Capture time: " + (DateTime.Now - StartTime).ToString());
                            });

                            // Make sure we free up the resolved texture if it was created
                            if (TextureResolved != null)
                            {
                                TextureResolved.Dispose();
                                TextureResolved = null;
                            }
                        }

                        DebugMessage("PresentHook: Copy BackBuffer time: " + (DateTime.Now - StartTime).ToString());
                        DebugMessage("PresentHook: Request End");
                    }
                    finally
                    {
                        // Prevent the request from being processed a second time
                        Request = null;
                    }

                }
                #endregion

                #region Example: Draw overlay (after screenshot so we don't capture overlay as well)

                if (Config.ShowOverlay)
                {
                    using (Texture2D Texture = Texture2D.FromSwapChain<SharpDX.Direct3D10.Texture2D>(SwapChain, 0))
                    {
                        if (FPS.GetFPS() >= 1)
                        {
                            FontDescription FontDescription = new FontDescription()
                            {
                                Height = 16,
                                FaceName = "Arial",
                                Italic = false,
                                Width = 0,
                                MipLevels = 1,
                                CharacterSet = FontCharacterSet.Default,
                                OutputPrecision = FontPrecision.Default,
                                Quality = FontQuality.Antialiased,
                                PitchAndFamily = FontPitchAndFamily.Default | FontPitchAndFamily.DontCare,
                                Weight = FontWeight.Bold
                            };

                            // TODO: Font should not be created every frame!
                            using (Font Font = new Font(Texture.Device, FontDescription))
                            {
                                DrawText(Font, new Vector2(5, 5), String.Format("{0:N0} fps", FPS.GetFPS()), new Color4(Color.Red.ToColor3()));

                                if (TextDisplay != null && TextDisplay.Display)
                                {
                                    DrawText(Font, new Vector2(5, 25), TextDisplay.Text, new Color4(Color.Red.ToColor3(), (Math.Abs(1.0f - TextDisplay.Remaining))));
                                }
                            }
                        }

                    }
                }

                #endregion
            }
            catch (Exception Ex)
            {
                // If there is an error we do not want to crash the hooked application, so swallow the exception
                DebugMessage("PresentHook: Exeception: " + Ex.GetType().FullName + ": " + Ex.Message);
            }

            // As always we need to call the original method, note that EasyHook has already repatched the original method
            // so calling it here will not cause an endless recursion to this function
            SwapChain.Present(SyncInterval, Flags);
            return Result.Ok.Code;
        }

        private void DrawText(Font Font, Vector2 Position, String Text, Color4 Color)
        {
            Font.DrawText(null, Text, new Rectangle((Int32)Position.X, (Int32)Position.Y, 0, 0), FontDrawFlags.NoClip, Color);
        }

    } // End class

} // End namespace