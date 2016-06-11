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
		/// Contains the metrics associated with text after layout. All coordinates are in device independent pixels (DIPs).
		/// </summary>
		public value class TextMetrics
		{
		public:
			TextMetrics ( float left, float top, float width, float widthIncludingTrailingWhitespace,
				float height, float layoutWidth, float layoutHeight, int maximumBidiReorderingDepth, int lineCount )
			{
				Left = left;
				Top = top;
				Width = width;
				WidthIncludingTrailingWhitespace = widthIncludingTrailingWhitespace;
				Height = height;
				LayoutWidth = layoutWidth;
				LayoutHeight = layoutHeight;
				MaximumBidiReorderingDepth = maximumBidiReorderingDepth;
				LineCount = lineCount;
			}

			/// <summary>
			/// A value that indicates the left-most point of formatted text
			/// relative to the layout box, while excluding any glyph overhang.
			/// </summary>
			property float Left;

			/// <summary>
			/// A value that indicates the top-most point of formatted text
			/// relative to the layout box, while excluding any glyph overhang.
			/// </summary>
			property float Top;

			/// <summary>
			/// A value that indicates the width of the formatted text, while
			/// ignoring trailing whitespace at the end of each line.
			/// </summary>
			property float Width;

			/// <summary>
			/// The width of the formatted text, taking into account the
			/// trailing whitespace at the end of each line.
			/// </summary>
			property float WidthIncludingTrailingWhitespace;

			/// <summary>
			/// The height of the formatted text. The height of an empty string
			/// is set to the same value as that of the default font.
			/// </summary>
			property float Height;

			/// <summary>
			/// The initial width given to the layout. It can be either larger
			/// or smaller than the text content width, depending on whether
			/// the text was wrapped.
			/// </summary>
			property float LayoutWidth;

			/// <summary>
			/// Initial height given to the layout. Depending on the length of
			/// the text, it may be larger or smaller than the text content
			/// height.
			/// </summary>
			property float LayoutHeight;

			/// <summary>
			/// The maximum reordering count of any line of text, used to
			/// calculate the most number of hit-testing boxes needed. If the
			/// layout has no bidirectional text, or no text at all, the
			/// minimum level is 1.
			/// </summary>
			property int MaximumBidiReorderingDepth;

			/// <summary>
			/// Total number of lines.
			/// </summary>
			property int LineCount;
		};
	}
}