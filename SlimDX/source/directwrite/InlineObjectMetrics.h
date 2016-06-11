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
#pragma once

namespace SlimDX
{
	namespace DirectWrite
	{
		/// <summary>
		/// Properties describing the geometric measurement of an
		/// application-defined inline object.
		/// </summary>
		public value class InlineObjectMetrics
		{
		public:
			InlineObjectMetrics(float width, float height, float baseline, bool supportsSideways)
			{
				Width = width;
				Height = height;
				Baseline = baseline;
				SupportsSideways = supportsSideways;
			}

			/// <summary>
			/// Width of the inline object.
			/// </summary>
			property float Width;

			/// <summary>
			/// Height of the inline object as measured from top to bottom.
			/// </summary>
			property float Height;

			/// <summary>
			/// Distance from the top of the object to the baseline where it is lined up with the adjacent text.
			/// If the baseline is at the bottom, baseline simply equals height.
			/// </summary>
			property float Baseline;

			/// <summary>
			/// Flag indicating whether the object is to be placed upright or alongside the text baseline
			/// for vertical text.
			/// </summary>
			property bool SupportsSideways;
		};
	}
}
