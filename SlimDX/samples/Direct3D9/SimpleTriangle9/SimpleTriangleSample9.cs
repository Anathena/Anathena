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

using System.Drawing;
using System.Runtime.InteropServices;

using SlimDX;
using SlimDX.Direct3D9;

using SlimDX.SampleFramework;

namespace SimpleTriangle9 {
    /// <summary>
    /// Demonstrates how to render a simple colored triangle with Direct3D9.
    /// </summary>
    class SimpleTriangle9Sample : Sample {
        protected override void OnInitialize() {
            var settings = new DeviceSettings9 {
                AdapterOrdinal = 0,
                CreationFlags = CreateFlags.HardwareVertexProcessing,
                Width = WindowWidth,
                Height = WindowHeight
            };

            InitializeDevice( settings );
        }

        protected override void OnResourceLoad() {
            vertexBuffer = new VertexBuffer(
                Context9.Device,
                3 * Marshal.SizeOf( typeof( ColoredVertex ) ),
                Usage.WriteOnly,
                VertexFormat.None,
                Pool.Managed
            );

            var stream = vertexBuffer.Lock( 0, 0, LockFlags.None );
            stream.WriteRange( new[] {
				new ColoredVertex( new Vector3(0.0f, 0.5f, 0.5f), Color.Red.ToArgb() ),
				new ColoredVertex( new Vector3(0.5f, -0.5f, 0.5f), Color.Blue.ToArgb() ),
				new ColoredVertex( new Vector3(-0.5f, -0.5f, 0.5f), Color.Green.ToArgb() ),
			} );

            vertexBuffer.Unlock();

            // Since this sample does not use any lights, disable lighting (otherwise the
            // triangle will appear flat black).
            Context9.Device.SetRenderState( RenderState.Lighting, false );

        	vertexDecl = new VertexDeclaration(Context9.Device, new[] {
				new VertexElement(0, 0, DeclarationType.Float3, DeclarationMethod.Default, DeclarationUsage.Position, 0), 
				new VertexElement(0, 12, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0), 
				VertexElement.VertexDeclarationEnd
        	});
        }

        protected override void OnResourceUnload() {
            vertexBuffer.Dispose();    
			vertexDecl.Dispose();
        }

        protected override void OnRenderBegin() {
            Context9.Device.Clear( ClearFlags.Target | ClearFlags.ZBuffer, new Color4( 0.3f, 0.3f, 0.3f ), 1.0f, 0 );
            Context9.Device.BeginScene();
        }

        protected override void OnRender() {
            Context9.Device.SetStreamSource( 0, vertexBuffer, 0, Marshal.SizeOf( typeof( ColoredVertex ) ) );
        	Context9.Device.VertexDeclaration = vertexDecl;
            Context9.Device.DrawPrimitives( PrimitiveType.TriangleList, 0, 1 );
        }

        protected override void OnRenderEnd() {
            Context9.Device.EndScene();
            Context9.Device.Present();
        }

        #region Implementation Detail

        VertexBuffer vertexBuffer;
    	VertexDeclaration vertexDecl;
        #endregion
    }
}
