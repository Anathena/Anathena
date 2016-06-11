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

using SlimDX;

namespace SlimDX.SampleFramework {
    /// <summary>
    /// Defines the interface required to specify an element's visual representation.
    /// </summary>
    public interface IElementVisual {
        #region Public Interface

        /// <summary>
        /// Measures the element, returning the size (in pixels) it would occupy if
        /// rendered with the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="element">The element.</param>
        /// <returns>The size of the element (in pixels).</returns>
        Vector2 Measure(UserInterfaceRenderer renderer, Element element);

        /// <summary>
        /// Renders the element using the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="element">The element.</param>
        /// <param name="x">The X coordinate (in pixels) of the upper left corner of the region the element should be rendered to.</param>
        /// <param name="y">The Y coordinate (in pixels) of the upper left corner of the region the element should be rendered to.</param>
        /// <param name="width">The width (in pixels) of the region the element should be rendered to.</param>
        /// <param name="height">The height (in pixels) of the region the element should be rendered to.</param>
        void Render(UserInterfaceRenderer renderer, Element element, int x, int y, int width, int height);

        #endregion
    }
}
