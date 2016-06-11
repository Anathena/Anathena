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

#include "DirectWriteException.h"
#include "FontFace.h"

const IID IID_IDWriteFontFace = __uuidof(IDWriteFontFace);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	array<GlyphMetrics>^ FontFace::GetDesignGlyphMetrics(array<short>^ indices, bool isSideways)
	{
		array<GlyphMetrics>^ metrics = gcnew array<GlyphMetrics>(indices->Length);
		pin_ptr<GlyphMetrics> pinnedMetrics = &metrics[0];
		pin_ptr<short> pinnedIndices = &indices[0];

		HRESULT hr = InternalPointer->GetDesignGlyphMetrics(reinterpret_cast<UINT16*>(pinnedIndices), indices->Length, reinterpret_cast<DWRITE_GLYPH_METRICS*>(pinnedMetrics), isSideways);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return metrics;
	}

	FontMetrics FontFace::GetGdiCompatibleMetrics(float size, float pixelsPerDip)
	{
		return GetGdiCompatibleMetrics(size, pixelsPerDip, Matrix3x2::Identity);
	}

	FontMetrics FontFace::GetGdiCompatibleMetrics(float size, float pixelsPerDip, Matrix3x2 transform)
	{
		FontMetrics metrics;

		HRESULT hr = InternalPointer->GetGdiCompatibleMetrics(size, pixelsPerDip, reinterpret_cast<DWRITE_MATRIX*>(&transform), reinterpret_cast<DWRITE_FONT_METRICS*>(&metrics));
		RECORD_DW(hr);

		return metrics;
	}

	array<GlyphMetrics>^ FontFace::GetGdiCompatibleGlyphMetrics(float size, float pixelsPerDip, bool useGdiNatural, bool isSideways, array<short>^ indices)
	{
		return GetGdiCompatibleGlyphMetrics(size, pixelsPerDip, useGdiNatural, isSideways, indices, Matrix3x2::Identity);
	}

	array<GlyphMetrics>^ FontFace::GetGdiCompatibleGlyphMetrics(float size, float pixelsPerDip, bool useGdiNatural, bool isSideways, array<short>^ indices, Matrix3x2 transform)
	{
		array<GlyphMetrics>^ metrics = gcnew array<GlyphMetrics>(indices->Length);
		pin_ptr<GlyphMetrics> pinnedMetrics = &metrics[0];
		pin_ptr<short> pinnedIndices = &indices[0];

		HRESULT hr = InternalPointer->GetGdiCompatibleGlyphMetrics(size, pixelsPerDip, reinterpret_cast<DWRITE_MATRIX*>(&transform), useGdiNatural,
			reinterpret_cast<UINT16*>(pinnedIndices), indices->Length, reinterpret_cast<DWRITE_GLYPH_METRICS*>(pinnedMetrics), isSideways);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return metrics;
	}

	array<short>^ FontFace::GetGlyphIndices(array<int>^ codePoints)
	{
		array<short>^ indices = gcnew array<short>(codePoints->Length);
		pin_ptr<short> pinnedIndices = &indices[0];
		pin_ptr<int> pinnedPoints = &codePoints[0];

		HRESULT hr = InternalPointer->GetGlyphIndices(reinterpret_cast<UINT32*>(pinnedPoints), codePoints->Length, reinterpret_cast<UINT16*>(pinnedIndices));
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return indices;
	}

	array<FontFile^>^ FontFace::GetFiles()
	{
		UINT32 count = 0;

		HRESULT hr = InternalPointer->GetFiles(&count, NULL);
		if (RECORD_DW(hr).IsFailure || count == 0)
			return nullptr;

		stack_array<IDWriteFontFile*> files = stackalloc(IDWriteFontFile*, count);
		hr = InternalPointer->GetFiles(&count, &files[0]);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		array<FontFile^>^ results = gcnew array<FontFile^>(count);
		for (UINT32 i = 0; i < count; i++)
			results[i] = FontFile::FromPointer(files[i]);

		return results;
	}

	Result FontFace::GetGlyphRunOutline(float size, array<short>^ indices, array<float>^ advances, array<GlyphOffset>^ offsets, bool isSideways, bool isRightToLeft, SlimDX::Direct2D::SimplifiedGeometrySink^ geometrySink)
	{
		UINT16* indicesPtr = NULL;
		FLOAT* advancesPtr = NULL;
		DWRITE_GLYPH_OFFSET* offsetsPtr = NULL;
		pin_ptr<short> pinnedIndices;
		pin_ptr<float> pinnedAdvances;
		pin_ptr<GlyphOffset> pinnedOffsets;

		if (indices != nullptr)
		{
			pinnedIndices = &indices[0];
			indicesPtr = reinterpret_cast<UINT16*>(pinnedIndices);
		}

		if (advances != nullptr)
		{
			pinnedAdvances = &advances[0];
			advancesPtr = reinterpret_cast<FLOAT*>(pinnedAdvances);
		}

		if (offsets != nullptr)
		{
			pinnedOffsets = &offsets[0];
			offsetsPtr = reinterpret_cast<DWRITE_GLYPH_OFFSET*>(pinnedOffsets);
		}

		HRESULT hr = InternalPointer->GetGlyphRunOutline(size, indicesPtr, advancesPtr, offsetsPtr, indices->Length, isSideways, isRightToLeft, geometrySink->InternalPointer);
		return RECORD_DW(hr);
	}

	RenderingMode FontFace::GetRecommendedRenderingMode(float size, float pixelsPerDip, MeasuringMode measuringMode, RenderingParameters^ renderingParameters)
	{
		DWRITE_RENDERING_MODE mode;

		HRESULT hr = InternalPointer->GetRecommendedRenderingMode(size, pixelsPerDip, static_cast<DWRITE_MEASURING_MODE>(measuringMode), renderingParameters->InternalPointer, &mode);
		RECORD_DW(hr);

		return static_cast<RenderingMode>(mode);
	}

	FontTable^ FontFace::GetFontTable(char a, char b, char c, char d)
	{
		return GetFontTable(DWRITE_MAKE_OPENTYPE_TAG(a, b, c, d));
	}

	FontTable^ FontFace::GetFontTable(int tableTag)
	{
		const void *data;
		UINT32 size;
		void *context;
		BOOL exists;

		HRESULT hr = InternalPointer->TryGetFontTable(tableTag, &data, &size, &context, &exists);
		if (RECORD_DW(hr).IsFailure || !exists)
			return nullptr;

		return gcnew FontTable(data, size, context);
	}

	void FontFace::ReleaseFontTable(FontTable^ table)
	{
		InternalPointer->ReleaseFontTable(table->Context.ToPointer());
	}

	FontFaceType FontFace::Type::get()
	{
		return static_cast<FontFaceType>(InternalPointer->GetType());
	}

	FontMetrics FontFace::Metrics::get()
	{
		FontMetrics result;

		InternalPointer->GetMetrics(reinterpret_cast<DWRITE_FONT_METRICS*>(&result));

		return result;
	}

	FontSimulations FontFace::Simulations::get()
	{
		return static_cast<FontSimulations>(InternalPointer->GetSimulations());
	}

	bool FontFace::IsSymbolFont::get()
	{
		return InternalPointer->IsSymbolFont() > 0;
	}

	int FontFace::GlyphCount::get()
	{
		return InternalPointer->GetGlyphCount();
	}

	int FontFace::Index::get()
	{
		return InternalPointer->GetIndex();
	}
}
}