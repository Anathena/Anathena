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
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

using SlimDX;

namespace SlimDX.SampleFramework {
    /// <summary>
    /// Provides baseline functionality for rendering a user interface.
    /// </summary>
    public abstract class UserInterfaceRenderer : IDisposable {
        #region Public Interface

        /// <summary>
        /// Performs object finalization.
        /// </summary>
        ~UserInterfaceRenderer() {
            Dispose(false);
        }

        /// <summary>
        /// Renders the specified user interface.
        /// </summary>
        /// <param name="userInterface">The user interface to render.</param>
        public void Render(UserInterface userInterface) {
            if (userInterface == null)
                throw new ArgumentNullException("userInterface");

            int y = 0;
            foreach (Element element in userInterface.Container) {
                IElementVisual visual = new DefaultVisual();
                Vector2 size = visual.Measure(this, element);
                visual.Render(this, element, 0, y, (int)size.X, (int)size.Y);
                y += (int)size.Y;
            }

            Flush();
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected virtual void Dispose(bool disposeManagedResources) {
        }

        /// <summary>
        /// In a derived class, implements logic to flush all pending rendering commands.
        /// </summary>
        protected abstract void Flush();

        /// <summary>
        /// Computes the metrics for a string if it were to be rendered with this renderer.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <returns>The size metrics for the string.</returns>
        internal abstract Vector2 MeasureString(string text);

        /// <summary>
        /// Renders a string.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <param name="x">The X coordinate of the upper left corner of the text.</param>
        /// <param name="y">The Y coordinate of the upper left corner of the text.</param>
        /// <param name="color">The color of the text.</param>
        internal abstract void RenderString(string text, int x, int y, Color4 color);

        /// <summary>
        /// Renders a line.
        /// </summary>
        /// <param name="x0">The X coordinate of the first point.</param>
        /// <param name="y0">The Y coordinate of the first point.</param>
        /// <param name="color0">The color of the first point.</param>
        /// <param name="x1">The X coordinate of the second point.</param>
        /// <param name="y1">The Y coordinate of the second point.</param>
        /// <param name="color1">The color of the second point.</param>
        internal abstract void RenderLine(int x0, int y0, Color4 color0, int x1, int y1, Color4 color1);

        /// <summary>
        /// Renders a rectangle.
        /// </summary>
        /// <param name="x">The X coordinate of the upper left corner of the rectangle.</param>
        /// <param name="y">The Y coordinate of the upper left corner of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="color">The color of the rectangle.</param>
        internal void RenderRectangle(int x, int y, int width, int height, Color4 color) {
            RenderLine(x, y, color, x + width, y, color);
            RenderLine(x + width, y, color, x + width, y + height, color);
            RenderLine(x + width, y + height, color, x, y + height, color);
            RenderLine(x, y + height, color, x, y, color);
        }

        #endregion
    }
}
