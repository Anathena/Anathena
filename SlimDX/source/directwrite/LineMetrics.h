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
		/// Contains information about a formatted line of text.
		/// </summary>
		public value class LineMetrics
		{
		public:
			LineMetrics( int length, int trailingWhitespaceLength, int newlineLength, float height, float baseline, bool isTrimmed )
			{
				Length = length;
				TrailingWhitespaceLength = trailingWhitespaceLength;
				NewlineLength = newlineLength;
				Height = height;
				Baseline = baseline;
				IsTrimmed = isTrimmed;
			}

			/// <summary>
			/// The number of text positions in the text line. This includes any trailing whitespace and newline characters.
			/// </summary>
			property int Length;

			/// <summary>
			/// The number of whitespace positions at the end of the text line. Newline sequences are considered whitespace.
			/// </summary>
			property int TrailingWhitespaceLength;

			/// <summary>
			/// The number of characters in the newline sequence at the end of the text line. If the count is zero, then the text line was either wrapped or it is the end of the text.
			/// </summary>
			property int NewlineLength;

			/// <summary>
			/// The height of the text line.
			/// </summary>
			property float Height;

			/// <summary>
			/// The distance from the top of the text line to its baseline.
			/// </summary>
			property float Baseline;

			/// <summary>
			/// The line is trimmed.
			/// </summary>
			property bool IsTrimmed;
		};
	}
}