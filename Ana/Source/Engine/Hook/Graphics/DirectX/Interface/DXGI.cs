namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface
{
    using SharpDX.DXGI;
    using System;

    internal static class DXGI
    {
        public static SwapChainDescription CreateSwapChainDescription(IntPtr windowHandle)
        {
            return new SwapChainDescription
            {
                BufferCount = 1,
                Flags = SwapChainFlags.None,
                IsWindowed = true,
                ModeDescription = new ModeDescription(100, 100, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                OutputHandle = windowHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
        }
    }
    //// End class
}
//// End namespace