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
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using SlimDX.SampleFramework;
using Resource = SlimDX.Direct3D10.Resource;

namespace SimpleModel10 {
    internal class SimpleModel10Sample : Sample {
        protected override void OnInitialize() {
            var settings10 = new DeviceSettings10 {
                AdapterOrdinal = 0,
                CreationFlags = DeviceCreationFlags.None,
                Width = WindowWidth,
                Height = WindowHeight
            };

            InitializeDevice( settings10 );

        }

        protected override void OnResourceLoad() {
            CreatePrimaryRenderTarget();
            CreateDepthBuffer();

            var dssd = new DepthStencilStateDescription {
                IsDepthEnabled = true,
                IsStencilEnabled = false,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Less
            };

            var solidParentOp = new BlendStateDescription();
            solidParentOp.SetBlendEnable( 0, false );
            solidParentOp.SetWriteMask( 0, ColorWriteMaskFlags.All );

            var transParentOp = new BlendStateDescription {
                AlphaBlendOperation = BlendOperation.Add,
                BlendOperation = BlendOperation.Add,
                DestinationAlphaBlend = BlendOption.Zero,
                DestinationBlend = BlendOption.One,
                IsAlphaToCoverageEnabled = false,
                SourceAlphaBlend = BlendOption.Zero,
                SourceBlend = BlendOption.One,
            };

            transParentOp.SetBlendEnable( 0, true );
            transParentOp.SetWriteMask( 0, ColorWriteMaskFlags.All );

            transBlendState = BlendState.FromDescription( Context10.Device, transParentOp );
            solidBlendState = BlendState.FromDescription( Context10.Device, solidParentOp );

            depthStencilState = DepthStencilState.FromDescription( Context10.Device, dssd );

            jupiterMesh = new SimpleModel( Context10.Device, "SimpleModel10.fx", "jupiter.SMD", "jupiter.jpg" );

            view = Matrix.LookAtLH( new Vector3( 0, 160, 0 ), new Vector3( 0, -128.0f, 0 ), -Vector3.UnitZ );
            jupiterMesh.Effect.GetVariableByName( "view" ).AsMatrix().SetMatrix( view );

            proj = Matrix.PerspectiveFovLH( 45.0f, WindowWidth / (float)WindowHeight, 1.0f, 1000.0f );
            jupiterMesh.Effect.GetVariableByName( "proj" ).AsMatrix().SetMatrix( proj );
        }

        protected override void OnResourceUnload() {
            Context10.Device.ClearState();
            renderTargetView.Dispose();
            depthStencilView.Dispose();
            
            jupiterMesh.Dispose();
            depthStencilState.Dispose();
            solidBlendState.Dispose();
            transBlendState.Dispose();
        }

        protected override void OnRenderBegin() {
            Context10.Device.OutputMerger.DepthStencilState = depthStencilState;
            Context10.Device.OutputMerger.SetTargets( depthStencilView, renderTargetView );

            Context10.Device.Rasterizer.SetViewports( new Viewport( 0, 0, WindowWidth, WindowHeight, 0.0f, 1.0f ) );
            Context10.Device.ClearRenderTargetView( renderTargetView, new Color4( 0.0f, 0.0f, 0.0f ) );
            Context10.Device.ClearDepthStencilView( depthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0 );
        }

        protected override void OnRender() {
            rotation += (float)( Math.PI / 4.0f * FrameDelta );

            if( rotation > 2 * Math.PI )
                rotation = 0;

            /// First we'll setup the primary planet that we want to render. This is the big jupiter planet.
            /// We're not doing anything overly fancy here, just setting it up to rotate on its axis at a fixed rate.
            Matrix rotationMatrix;
            Matrix.RotationZ( rotation, out rotationMatrix );
            var world = Matrix.Identity;
            Matrix.Multiply( ref world, ref rotationMatrix, out world );

            jupiterMesh.Effect.GetVariableByName( "transparency" ).AsScalar().Set( 0.0f );
            jupiterMesh.Effect.GetVariableByName( "world" ).AsMatrix().SetMatrix( world );
            Context10.Device.OutputMerger.BlendState = solidBlendState;
            jupiterMesh.Draw();

            /// At this point we want to render a small moon that will orbit the planet (and rotate as well). 
            /// Again, we're not doing anything overly fancy, however we will set the transparency blend state and then
            /// set the transparency of the pixels (a fixed amount).
            Matrix.Translation( 0, 72, 0, out world );
            Matrix tempMatrix;
            Matrix.Scaling( .125f, .125f, .125f, out tempMatrix );
            Matrix.Multiply( ref tempMatrix, ref world, out world );
            Matrix.Multiply( ref rotationMatrix, 1.25f, out rotationMatrix );
            Matrix.Multiply( ref rotationMatrix, ref world, out world );
            Matrix orbitMatrix;
            Matrix.RotationZ( -rotation * 2f, out orbitMatrix );
            Matrix.Multiply( ref world, ref orbitMatrix, out world );

            jupiterMesh.Effect.GetVariableByName( "world" ).AsMatrix().SetMatrix( world );
            jupiterMesh.Effect.GetVariableByName( "transparency" ).AsScalar().Set( .50f );
            Context10.Device.OutputMerger.BlendState = transBlendState;
            jupiterMesh.Draw();
        }

        protected override void OnRenderEnd() {
            Context10.SwapChain.Present( 0, PresentFlags.None );
        }

        private void CreatePrimaryRenderTarget() {
            using( var swapChainBuffer = Resource.FromSwapChain<Texture2D>( Context10.SwapChain, 0 ) ) {
                renderTargetView = new RenderTargetView( Context10.Device, swapChainBuffer );
            }
        }

        private void CreateDepthBuffer() {
            var depthBufferDesc = new Texture2DDescription {
                ArraySize = 1,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.D32_Float,
                Height = WindowHeight,
                Width = WindowWidth,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription( 1, 0 ),
                Usage = ResourceUsage.Default
            };

            using( var depthBuffer = new Texture2D( Context10.Device, depthBufferDesc ) ) {
                depthStencilView = new DepthStencilView( Context10.Device, depthBuffer );
            }
        }

        #region Implementation Detail

        ///
        /// Disposable resources
        ///
        private DepthStencilState depthStencilState;
        private DepthStencilView depthStencilView;
        private SimpleModel jupiterMesh;
        private BlendState solidBlendState;
        private BlendState transBlendState;
        private RenderTargetView renderTargetView;
 
        /// 
        /// Non-Disposable resources
        ///
        private Matrix proj;
        private float rotation;
        private Matrix view;

        #endregion
    }
}