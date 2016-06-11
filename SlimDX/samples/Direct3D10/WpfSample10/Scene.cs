using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.Direct3D10_1;
using SlimDX.DXGI;
using SlimDX.Windows;
using Buffer = SlimDX.Direct3D10.Buffer;
using Device = SlimDX.Direct3D10_1.Device1;
using SlimDX.D3DCompiler;

namespace WpfSample10
{
    class Scene : IDisposable
    {
        Device D3DDevice;
        DataStream SampleStream;
        InputLayout SampleLayout;
        Buffer SampleVertices;
        RenderTargetView SampleRenderView;
        DepthStencilView SampleDepthView;
        Effect SampleEffect;
        Texture2D DepthTexture;
        int WindowWidth;
        int WindowHeight;

        public Texture2D SharedTexture
        {
            get;
            set;
        }

        public Scene()
        {
            WindowWidth = 100;
            WindowHeight = 100;
            InitD3D();
        }

        public void Dispose()
        {
            DestroyD3D();
        }

        public void Render(int arg)
        {
            D3DDevice.OutputMerger.SetTargets(SampleDepthView, SampleRenderView);
            D3DDevice.Rasterizer.SetViewports(new Viewport(0, 0, WindowWidth, WindowHeight, 0.0f, 1.0f));

            D3DDevice.ClearDepthStencilView(SampleDepthView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
            float c = ((float)(arg % 1000)) / 999.0f;
            D3DDevice.ClearRenderTargetView(SampleRenderView, new SlimDX.Color4(1.0f, c, c, c));

            D3DDevice.InputAssembler.SetInputLayout(SampleLayout);
            D3DDevice.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            D3DDevice.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(SampleVertices, 32, 0));

            EffectTechnique technique = SampleEffect.GetTechniqueByIndex(0);
            EffectPass pass = technique.GetPassByIndex(0);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply();
                D3DDevice.Draw(3, 0);
            }

            D3DDevice.Flush();
        }

        void InitD3D()
        {
            D3DDevice = new Device(DriverType.Hardware, DeviceCreationFlags.Debug | DeviceCreationFlags.BgraSupport, FeatureLevel.Level_10_0);

            Texture2DDescription colordesc = new Texture2DDescription();
            colordesc.BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource;
            colordesc.Format = Format.B8G8R8A8_UNorm;
            colordesc.Width = WindowWidth;
            colordesc.Height = WindowHeight;
            colordesc.MipLevels = 1;
            colordesc.SampleDescription = new SampleDescription(1, 0);
            colordesc.Usage = ResourceUsage.Default;
            colordesc.OptionFlags = ResourceOptionFlags.Shared;
            colordesc.CpuAccessFlags = CpuAccessFlags.None;
            colordesc.ArraySize = 1;

            Texture2DDescription depthdesc = new Texture2DDescription();
            depthdesc.BindFlags = BindFlags.DepthStencil;
            depthdesc.Format = Format.D32_Float_S8X24_UInt;
            depthdesc.Width = WindowWidth;
            depthdesc.Height = WindowHeight;
            depthdesc.MipLevels = 1;
            depthdesc.SampleDescription = new SampleDescription(1, 0);
            depthdesc.Usage = ResourceUsage.Default;
            depthdesc.OptionFlags = ResourceOptionFlags.None;
            depthdesc.CpuAccessFlags = CpuAccessFlags.None;
            depthdesc.ArraySize = 1;

            SharedTexture = new Texture2D(D3DDevice, colordesc);
            DepthTexture = new Texture2D(D3DDevice, depthdesc);
            SampleRenderView = new RenderTargetView(D3DDevice, SharedTexture);
            SampleDepthView = new DepthStencilView(D3DDevice, DepthTexture);
            SampleEffect = Effect.FromFile(D3DDevice, "MiniTri.fx", "fx_4_0");
            EffectTechnique technique = SampleEffect.GetTechniqueByIndex(0); ;
            EffectPass pass = technique.GetPassByIndex(0);
            SampleLayout = new InputLayout(D3DDevice, pass.Description.Signature, new[] {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0) 
            });

            SampleStream = new DataStream(3 * 32, true, true);
            SampleStream.WriteRange(new[] {
                new Vector4(0.0f, 0.5f, 0.5f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                new Vector4(0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                new Vector4(-0.5f, -0.5f, 0.5f, 1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f)
            });
            SampleStream.Position = 0;

            SampleVertices = new Buffer(D3DDevice, SampleStream, new BufferDescription()
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = 3 * 32,
                Usage = ResourceUsage.Default
            });

            D3DDevice.Flush();
        }

        void DestroyD3D()
        {
            if (SampleVertices != null)
            {
                SampleVertices.Dispose();
                SampleVertices = null;
            }

            if (SampleLayout != null)
            {
                SampleLayout.Dispose();
                SampleLayout = null;
            }

            if (SampleEffect != null)
            {
                SampleEffect.Dispose();
                SampleEffect = null;
            }

            if (SampleRenderView != null)
            {
                SampleRenderView.Dispose();
                SampleRenderView = null;
            }

            if (SampleDepthView != null)
            {
                SampleDepthView.Dispose();
                SampleDepthView = null;
            }

            if (SampleStream != null)
            {
                SampleStream.Dispose();
                SampleStream = null;
            }

            if (SampleLayout != null)
            {
                SampleLayout.Dispose();
                SampleLayout = null;
            }

            if (SharedTexture != null)
            {
                SharedTexture.Dispose();
                SharedTexture = null;
            }

            if (DepthTexture != null)
            {
                DepthTexture.Dispose();
                DepthTexture = null;
            }

            if (D3DDevice != null)
            {
                D3DDevice.Dispose();
                D3DDevice = null;
            }
        }
    }
}
