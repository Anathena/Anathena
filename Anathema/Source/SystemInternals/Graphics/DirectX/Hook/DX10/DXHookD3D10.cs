using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using SharpDX;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Device = SharpDX.Direct3D10.Device;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Hook.DX10
{
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

        public DXHookD3D10(ClientInterface CaptureInterface) : base(CaptureInterface)
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
                // Draw FPS
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