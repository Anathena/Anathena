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

struct IDWriteFontMock : public IDWriteFont
{
	MOCK_IUNKNOWN;

	// IDWriteFont
	STDMETHOD(GetFontFamily)(IDWriteFontFamily**) { return E_NOTIMPL; }
	STDMETHOD_(DWRITE_FONT_WEIGHT, GetWeight)() { return DWRITE_FONT_WEIGHT(0); }
	STDMETHOD_(DWRITE_FONT_STRETCH, GetStretch)() { return DWRITE_FONT_STRETCH(0); }
	STDMETHOD_(DWRITE_FONT_STYLE, GetStyle)() { return DWRITE_FONT_STYLE(0); }
	STDMETHOD_(BOOL, IsSymbolFont)() { return BOOL(0); }
	STDMETHOD(GetFaceNames)(IDWriteLocalizedStrings**) { return E_NOTIMPL; }
	STDMETHOD(GetInformationalStrings)(DWRITE_INFORMATIONAL_STRING_ID, IDWriteLocalizedStrings**, BOOL*) { return E_NOTIMPL; }
	STDMETHOD_(DWRITE_FONT_SIMULATIONS, GetSimulations)() { return DWRITE_FONT_SIMULATIONS(0); }
	STDMETHOD_(void, GetMetrics)(DWRITE_FONT_METRICS*) { }
	STDMETHOD(HasCharacter)(UINT32, BOOL*) { return E_NOTIMPL; }
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, CreateFontFace, HRESULT( IDWriteFontFace** ) );
};
