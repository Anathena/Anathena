/*
* Copyright (c) 2007-2009 SlimDX Group
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
#pragma once

namespace SlimDX
{
	namespace DirectWrite
	{
		/// <summary>
		/// Indicates how much any visible DIPs (device independent pixels)
		/// overshoot each side of the layout or inline objects.
		///
		/// Positive overhangs indicate that the visible area extends outside
		/// the layout box or inline object, while negative values mean there
		/// is whitespace inside. The returned values are unaffected by
		/// rendering transforms or pixel snapping. Additionally, they may not
		/// exactly match the final target's pixel bounds after applying grid 
		/// fitting and hinting.
		/// </summary>
		public value class OverhangMetrics
		{
		public:
			OverhangMetrics( float left, float top, float right, float bottom )
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			/// <summary>
			/// The distance from the left-most visible DIP to its left
			/// alignment edge.
			/// </summary>
			property float Left;

			/// <summary>
			/// The distance from the top-most visible DIP to its top
			/// alignment edge.
			/// </summary>
			property float Top;

			/// <summary>
			/// The distance from the right-most visible DIP to its right
			/// alignment edge.
			/// </summary>
			property float Right;

			/// <summary>
			/// The distance from the bottom-most visible DIP to its lower
			/// alignment edge.
			/// </summary>
			property float Bottom;
		};
	}
}