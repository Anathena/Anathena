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

class IDWriteTextRendererMock : public IDWriteTextRenderer
{
	MOCK_IUNKNOWN;

	// IDWritePixelSnapping
	STDMETHOD(IsPixelSnappingDisabled)(void* clientDrawingContext, BOOL* isDisabled) { return E_NOTIMPL; }
	STDMETHOD(GetCurrentTransform)(void* clientDrawingContext, DWRITE_MATRIX* transform) { return E_NOTIMPL; }
	STDMETHOD(GetPixelsPerDip)(void* clientDrawingContext, FLOAT* pixelsPerDip) { return E_NOTIMPL; }

	// IDWriteTextRenderer
	STDMETHOD(DrawGlyphRun)(void* clientDrawingContext, FLOAT baselineOriginX, FLOAT baselineOriginY, DWRITE_MEASURING_MODE measuringMode, DWRITE_GLYPH_RUN const* glyphRun, DWRITE_GLYPH_RUN_DESCRIPTION const* glyphRunDescription, IUnknown* clientDrawingEffect) { return E_NOTIMPL; }
	STDMETHOD(DrawUnderline)(void* clientDrawingContext, FLOAT baselineOriginX, FLOAT baselineOriginY, DWRITE_UNDERLINE const* underline, IUnknown* clientDrawingEffect) { return E_NOTIMPL; }
	STDMETHOD(DrawStrikethrough)(void* clientDrawingContext, FLOAT baselineOriginX, FLOAT baselineOriginY, DWRITE_STRIKETHROUGH const* strikethrough, IUnknown* clientDrawingEffect) { return E_NOTIMPL; }
	STDMETHOD(DrawInlineObject)(void* clientDrawingContext, FLOAT originX, FLOAT originY, IDWriteInlineObject* inlineObject, BOOL isSideways, BOOL isRightToLeft, IUnknown* clientDrawingEffect) { return E_NOTIMPL; }
};

