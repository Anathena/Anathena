/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
using System;
using System.Drawing;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.Windows;
using SlimDX.D3DCompiler;
using Device = SlimDX.Direct3D11.Device;

namespace MiniTri
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var form = new RenderForm("SlimDX - Conway's game of life Direct3D 11 Sample");
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(form.ClientSize.Width, form.ClientSize.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device device;
            SwapChain swapChain;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.Debug, desc, out device, out swapChain);

            device.Factory.SetWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            Texture2D backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            var renderView = new RenderTargetView(device, backBuffer);
            var bytecode = ShaderBytecode.CompileFromFile("Render.fx", "fx_5_0", ShaderFlags.None, EffectFlags.None);
            var effect = new Effect(device, bytecode);
            var technique = effect.GetTechniqueByIndex(0);
            var pass = technique.GetPassByIndex(0);
            String errors;
            var computeByteCode = ShaderBytecode.CompileFromFile("compute.fx", "CS", "cs_5_0", ShaderFlags.None, EffectFlags.None, null, null, out errors);
            var compute = new ComputeShader(device, computeByteCode);

            // shader variable handles
            var conwayResourceH = effect.GetVariableByName("tex").AsResource();
            var resolutionInvH = effect.GetVariableByName("resolutionInv").AsVector();
            resolutionInvH.Set(new Vector2(1.0f / form.ClientSize.Width, 1.0f / form.ClientSize.Height));
            EffectVectorVariable lightPosSSH = effect.GetVariableByName("lightPosSS").AsVector();

            // create texture, fill it with random data
            Texture2DDescription textureDesc = new Texture2DDescription()
            {
                Width = form.ClientSize.Width,
                Height = form.ClientSize.Height,
                MipLevels = 1,
                ArraySize = 1,
                CpuAccessFlags = CpuAccessFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                OptionFlags = ResourceOptionFlags.None,
                BindFlags = BindFlags.UnorderedAccess | BindFlags.ShaderResource,
                Format = Format.R32_Float
            };

            var random = new Random();
            var data = new float[form.ClientSize.Width * form.ClientSize.Height];
            for (int i = 0; i < form.ClientSize.Width; ++i)
            {
                for (int j = 0; j < form.ClientSize.Height; ++j)
                    data[i * form.ClientSize.Height + j] = (float)random.Next(2);
            }

            DataStream ds = new DataStream(data, true, false);
            DataRectangle dataRect = new DataRectangle(4 * form.ClientSize.Width, ds);

            Texture2D conwayTex = new Texture2D(device, textureDesc, dataRect);

            // Create SRV and UAV over the same texture
            UnorderedAccessView conwayUAV = new UnorderedAccessView(device, conwayTex);
            ShaderResourceView conwaySRV = new ShaderResourceView(device, conwayTex);

            // On the more typical setup where you switch shaders, 
            // you will have to set the texture after every
            conwayResourceH.SetResource(conwaySRV);

            device.ImmediateContext.OutputMerger.SetTargets(renderView);
            device.ImmediateContext.Rasterizer.SetViewports(new Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height, 0.0f, 1.0f));
            device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            Vector2 lightPosSS;
            float angle = 0;

            MessagePump.Run(form, () =>
            {
                // this does the light rotation
                angle += 0.002f;
                lightPosSS = new Vector2((float)Math.Sin(angle) * 0.5f + 0.5f, (float)Math.Cos(angle) * 0.5f + 0.5f);
                lightPosSSH.Set(lightPosSS);

                device.ImmediateContext.ComputeShader.Set(compute);
                device.ImmediateContext.ComputeShader.SetUnorderedAccessView(conwayUAV, 0);
                device.ImmediateContext.Dispatch(form.ClientSize.Width / 16 + 1, form.ClientSize.Height / 16 + 1, 1);

                // After running the CS you have to unset UAV from the shader, so you can use it as SRV
                device.ImmediateContext.ComputeShader.SetUnorderedAccessView(null, 0);

                device.ImmediateContext.ClearRenderTargetView(renderView, Color.Black);

                for (int i = 0; i < technique.Description.PassCount; ++i)
                {
                    pass.Apply(device.ImmediateContext);
                    // No vertices are send as they are created in the vertex shader on the fly.
                    device.ImmediateContext.Draw(4, 0);
                }

                swapChain.Present(0, PresentFlags.None);
            });

            computeByteCode.Dispose();
            conwayUAV.Dispose();
            conwaySRV.Dispose();
            conwayTex.Dispose();
            ds.Dispose();
            bytecode.Dispose();
            effect.Dispose();
            renderView.Dispose();
            backBuffer.Dispose();
            device.Dispose();
            swapChain.Dispose();
        }
    }
}