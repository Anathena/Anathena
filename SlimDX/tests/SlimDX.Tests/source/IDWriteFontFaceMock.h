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

#include "CommonMocks.h"
#include <dwrite.h>

struct IDWriteFontFaceMock : public IDWriteFontFace
{
	MOCK_IUNKNOWN;

	// IDWriteFontFace
	STDMETHOD_(DWRITE_FONT_FACE_TYPE, GetType)() { return DWRITE_FONT_FACE_TYPE(0); }
	STDMETHOD(GetFiles)( UINT32* numberOfFiles, IDWriteFontFile** fontFiles ) { return E_NOTIMPL; }
	STDMETHOD_(UINT32, GetIndex)() { return UINT32(0); }
	STDMETHOD_(DWRITE_FONT_SIMULATIONS, GetSimulations)() { return DWRITE_FONT_SIMULATIONS(0); }
	STDMETHOD_(BOOL, IsSymbolFont)() { return BOOL(0); }
	STDMETHOD_(void, GetMetrics)( DWRITE_FONT_METRICS* fontFaceMetrics ) { }
	STDMETHOD_(UINT16, GetGlyphCount)() { return UINT16(0); }
	STDMETHOD(GetDesignGlyphMetrics)( UINT16 const* glyphIndices, UINT32 glyphCount, DWRITE_GLYPH_METRICS* glyphMetrics, BOOL isSideways = FALSE ) { return E_NOTIMPL; }
	STDMETHOD(GetGlyphIndices)( UINT32 const* codePoints, UINT32 codePointCount, UINT16* glyphIndices ) { return E_NOTIMPL; }
	STDMETHOD(TryGetFontTable)( UINT32 openTypeTableTag, const void** tableData, UINT32* tableSize, void** tableContext, BOOL* exists ) { return E_NOTIMPL; }
	STDMETHOD_(void, ReleaseFontTable)( void* tableContext ) { }
	STDMETHOD(GetGlyphRunOutline)( FLOAT emSize, UINT16 const* glyphIndices, FLOAT const* glyphAdvances, DWRITE_GLYPH_OFFSET const* glyphOffsets, UINT32 glyphCount, BOOL isSideways, BOOL isRightToLeft, IDWriteGeometrySink* geometrySink ) { return E_NOTIMPL; }
	STDMETHOD(GetRecommendedRenderingMode)( FLOAT emSize, FLOAT pixelsPerDip, DWRITE_MEASURING_MODE measuringMode, IDWriteRenderingParams* renderingParams, DWRITE_RENDERING_MODE* renderingMode ) { return E_NOTIMPL; }
	STDMETHOD(GetGdiCompatibleMetrics)( FLOAT emSize, FLOAT pixelsPerDip, DWRITE_MATRIX const* transform, DWRITE_FONT_METRICS* fontFaceMetrics ) { return E_NOTIMPL; }
	STDMETHOD(GetGdiCompatibleGlyphMetrics)( FLOAT emSize, FLOAT pixelsPerDip, DWRITE_MATRIX const* transform, BOOL useGdiNatural, UINT16 const* glyphIndices, UINT32 glyphCount, DWRITE_GLYPH_METRICS* glyphMetrics, BOOL isSideways = FALSE ) { return E_NOTIMPL; }
};
