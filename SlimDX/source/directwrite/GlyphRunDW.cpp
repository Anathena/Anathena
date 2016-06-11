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
#include "stdafx.h"

#include "../stack_array.h"

#include "GlyphRunDW.h"

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	GlyphRun::GlyphRun(const DWRITE_GLYPH_RUN &run)
	{
		run.fontFace->AddRef();
		FontFace = DirectWrite::FontFace::FromPointer(run.fontFace);
		FontSize = run.fontEmSize;
		GlyphCount = run.glyphCount;
		IsSideways = run.isSideways != 0;
		BidiLevel = run.bidiLevel;

		GlyphIndices = gcnew array<short>(GlyphCount);
		GlyphAdvances = gcnew array<float>(GlyphCount);
		GlyphOffsets = gcnew array<GlyphOffset>(GlyphCount);

		for (int i = 0; i < GlyphCount; i++)
		{
			GlyphIndices[i] = run.glyphIndices[i];
			GlyphAdvances[i] = run.glyphAdvances[i];
			GlyphOffsets[i].AdvanceOffset = run.glyphOffsets[i].advanceOffset;
			GlyphOffsets[i].AscenderOffset = run.glyphOffsets[i].ascenderOffset;
		}
	}

	DWRITE_GLYPH_RUN GlyphRun::ToUnmanaged(stack_array<UINT16> &indices, stack_array<FLOAT> &advances, stack_array<DWRITE_GLYPH_OFFSET> &offsets)
	{
		DWRITE_GLYPH_RUN result;

		result.fontFace = FontFace->InternalPointer;
		result.fontEmSize = FontSize;
		result.glyphCount = GlyphCount;
		result.isSideways = IsSideways;
		result.bidiLevel = BidiLevel;
		result.glyphIndices = NULL;
		result.glyphAdvances = NULL;
		result.glyphOffsets = NULL;

		if (GlyphIndices != nullptr && GlyphIndices->Length > 0)
		{
			pin_ptr<short> pinnedIndices = &GlyphIndices[0];
			memcpy(&indices[0], pinnedIndices, sizeof(short) * GlyphIndices->Length);
			result.glyphIndices = &indices[0];
		}

		if (GlyphAdvances != nullptr && GlyphAdvances->Length > 0)
		{
			pin_ptr<float> pinnedAdvances = &GlyphAdvances[0];
			memcpy(&advances[0], pinnedAdvances, sizeof(float) * GlyphAdvances->Length);
			result.glyphAdvances = &advances[0];
		}

		if (GlyphOffsets != nullptr && GlyphOffsets->Length > 0)
		{
			pin_ptr<GlyphOffset> pinnedOffsets = &GlyphOffsets[0];
			memcpy(&offsets[0], pinnedOffsets, sizeof(DWRITE_GLYPH_OFFSET) * GlyphOffsets->Length);
			result.glyphOffsets = &offsets[0];
		}

		return result;
	}
}
}
