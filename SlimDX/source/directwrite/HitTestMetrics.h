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
		/// Describes the region obtained by a hit test.
		/// </summary>
		public value class HitTestMetrics
		{
		public:
			HitTestMetrics( int textPosition, int length, float left, float top,
				float width, float height, int bidiLevel, bool isText, bool isTrimmed )
			{
				TextPosition = textPosition;
				Length = length;
				Left = left;
				Top = top;
				Width = width;
				Height = height;
				BidiLevel = bidiLevel;
				IsText = isText;
				IsTrimmed = isTrimmed;
			}

			/// <summary>
			/// The first text position within the hit region. 
			/// </summary>
			property int TextPosition;

			/// <summary>
			/// The number of text positions within the hit region. 
			/// </summary>
			property int Length;

			/// <summary>
			/// The x-coordinate of the upper-left corner of the hit region.
			/// </summary>
			property float Left;

			/// <summary>
			/// The y-coordinate of the upper-left corner of the hit region.
			/// </summary>
			property float Top;

			/// <summary>
			/// The width of the hit region.
			/// </summary>
			property float Width;

			/// <summary>
			/// The height of the hit region.
			/// </summary>
			property float Height;

			/// <summary>
			/// The BIDI level of the text positions within the hit region.
			/// </summary>
			property int BidiLevel;

			/// <summary>
			/// true if the hit region contains text; otherwise, false.
			/// </summary>
			property bool IsText;

			/// <summary>
			/// true if the region is trimmed; otherwise, false.
			/// </summary>
			property bool IsTrimmed;
		};
	}
}