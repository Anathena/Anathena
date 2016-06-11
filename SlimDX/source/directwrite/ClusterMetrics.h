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
		/// Contains information about a glyph cluster.
		/// </summary>
		public value class ClusterMetrics
		{
		public:
			ClusterMetrics( float width, short length, bool canWrapLineAfter,
				bool isWhitespace, bool isNewline, bool isSoftHyphen, bool isRightToLeft )
			{
				Width = width;
				Length = length;
				CanWrapLineAfter = canWrapLineAfter;
				IsWhitespace = isWhitespace;
				IsNewline = isNewline;
				IsSoftHyphen = isSoftHyphen;
				IsRightToLeft = isRightToLeft;
			}

			/// <summary>
			/// The total advance width of all glyphs in the cluster.
			/// </summary>
			property float Width;

			/// <summary>
			/// The number of text positions in the cluster.
			/// </summary>
			property short Length;

			/// <summary>
			/// Indicates whether a line can be broken right after the cluster.
			/// </summary>
			property bool CanWrapLineAfter;

			/// <summary>
			/// Indicates whether the cluster corresponds to a whitespace character.
			/// </summary>
			property bool IsWhitespace;

			/// <summary>
			/// Indicates whether the cluster corresponds to a newline character.
			/// </summary>
			property bool IsNewline;

			/// <summary>
			/// Indicates whether the cluster corresponds to a soft hyphen character.
			/// </summary>
			property bool IsSoftHyphen;

			/// <summary>
			/// Indicates whether the cluster is read from right to left.
			/// </summary>
			property bool IsRightToLeft;
		};
	}
}
