using SharpDX.DXGI;
using System;

namespace Anathena.Source.Engine.Hook.Graphics.DirectX.Interface
{
    internal static class DXGI
    {
        public static SwapChainDescription CreateSwapChainDescription(IntPtr WindowHandle)
        {
            return new SwapChainDescription
            {
                BufferCount = 1,
                Flags = SwapChainFlags.None,
                IsWindowed = true,
                ModeDescription = new ModeDescription(100, 100, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                OutputHandle = WindowHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
        }

    } // End class

} // End namespace