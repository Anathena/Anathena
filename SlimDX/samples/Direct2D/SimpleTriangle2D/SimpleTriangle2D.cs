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

using SlimDX;
using SlimDX.Direct2D;

using SlimDX.SampleFramework;

namespace SimpleTriangle2D {
    /// <summary>
    /// Demonstrates how to render a simple colored triangle with Direct2D.
    /// </summary>
    class SimpleTriangle2DSample : Sample {
        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected override void Dispose(bool disposeManagedResources) {
            if (disposeManagedResources) {
                brush.Dispose();
                geometry.Dispose();
            }

            base.Dispose(disposeManagedResources);
        }

        /// <summary>
        /// In a derived class, implements logic to initialize the sample.
        /// </summary>
        protected override void OnInitialize() {
            DeviceSettings2D settings = new DeviceSettings2D {
                Width = WindowWidth,
                Height = WindowHeight
            };

            InitializeDevice(settings);
            geometry = new PathGeometry(Context2D.RenderTarget.Factory);
            using (GeometrySink sink = geometry.Open()) {
                PointF p0 = new PointF(0.50f * WindowWidth, 0.25f * WindowHeight);
                PointF p1 = new PointF(0.75f * WindowWidth, 0.75f * WindowHeight);
                PointF p2 = new PointF(0.25f * WindowWidth, 0.75f * WindowHeight);

                sink.BeginFigure(p0, FigureBegin.Filled);
                sink.AddLine(p1);
                sink.AddLine(p2);
                sink.EndFigure(FigureEnd.Closed);

                // Note that Close() and Dispose() are not equivalent like they are for
                // some other IDisposable() objects.
                sink.Close();
            }

            brush = new SolidColorBrush(Context2D.RenderTarget, new Color4(0.93f, 0.40f, 0.08f));
        }

        /// <summary>
        /// In a derived class, implements logic that should occur before all
        /// other rendering.
        /// </summary>
        protected override void OnRenderBegin() {
            Context2D.RenderTarget.BeginDraw();
            Context2D.RenderTarget.Transform = Matrix3x2.Identity;
            Context2D.RenderTarget.Clear(new Color4(0.3f, 0.3f, 0.3f));
        }

        /// <summary>
        /// In a derived class, implements logic to render the sample.
        /// </summary>
        protected override void OnRender() {
            Context2D.RenderTarget.FillGeometry(geometry, brush);
        }

        /// <summary>
        /// In a derived class, implements logic that should occur after all
        /// other rendering.
        /// </summary>
        protected override void OnRenderEnd() {
            Context2D.RenderTarget.EndDraw();
        }

        #region Implementation Detail

        PathGeometry geometry;
        SolidColorBrush brush;

        #endregion
    }
}
