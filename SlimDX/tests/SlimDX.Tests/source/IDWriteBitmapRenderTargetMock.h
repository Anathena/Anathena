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

struct IDWriteBitmapRenderTargetMock : public IDWriteBitmapRenderTarget
{
	MOCK_IUNKNOWN;

    STDMETHOD(DrawGlyphRun)(
        FLOAT baselineOriginX,
        FLOAT baselineOriginY,
        DWRITE_MEASURING_MODE measuringMode,
        __in DWRITE_GLYPH_RUN const* glyphRun,
        IDWriteRenderingParams* renderingParams,
        COLORREF textColor,
        __out_opt RECT* blackBoxRect = NULL
        ) { return E_NOTIMPL; }
    STDMETHOD_(HDC, GetMemoryDC)() { return static_cast<HDC>(0); }
    STDMETHOD_(FLOAT, GetPixelsPerDip)() { return -1.0f; }
    STDMETHOD(SetPixelsPerDip)(
        FLOAT pixelsPerDip
        ) { return E_NOTIMPL; }
    STDMETHOD(GetCurrentTransform)(
        __out DWRITE_MATRIX* transform
        ) { return E_NOTIMPL; }
    STDMETHOD(SetCurrentTransform)(
        __in_opt DWRITE_MATRIX const* transform
        ) { return E_NOTIMPL; }
    STDMETHOD(GetSize)(
        __out SIZE* size
        ) { return E_NOTIMPL; }
    STDMETHOD(Resize)(
        UINT32 width,
        UINT32 height
        ) { return E_NOTIMPL; }
};
