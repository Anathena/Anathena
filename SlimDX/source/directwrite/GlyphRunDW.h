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

#include "FontFace.h"
#include "GlyphOffset.h"

template<typename T>
class stack_array;

namespace SlimDX
{
	namespace DirectWrite
	{
		public ref class GlyphRun
		{
		internal:
			DWRITE_GLYPH_RUN ToUnmanaged(stack_array<UINT16> &indices, stack_array<FLOAT> &advances, stack_array<DWRITE_GLYPH_OFFSET> &offsets);
			GlyphRun(const DWRITE_GLYPH_RUN &run);

		public:
			GlyphRun() { }

			property FontFace^ FontFace;
			property float FontSize;
			property int GlyphCount;
			property array<short>^ GlyphIndices;
			property array<float>^ GlyphAdvances;
			property array<GlyphOffset>^ GlyphOffsets;
			property bool IsSideways;
			property int BidiLevel;
		};
	}
}