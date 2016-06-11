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

#include "DirectWriteException.h"

#include "BitmapRenderTargetDW.h"
#include "Font.h"
#include "FontFace.h"
#include "GdiInterop.h"

const IID IID_IDWriteGdiInterop = __uuidof(IDWriteGdiInterop);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	Result GdiInterop::ToLogFont(FontFace^ fontFace, Object^ logFont)
	{
		pin_ptr<Object^> pinnedFont = &logFont;

		HRESULT hr = InternalPointer->ConvertFontFaceToLOGFONT(fontFace->InternalPointer, reinterpret_cast<LOGFONTW*>(pinnedFont));
		return RECORD_DW(hr);
	}

	Result GdiInterop::ToLogFont(Font^ font, Object^ logFont)
	{
		pin_ptr<Object^> pinnedFont = &logFont;
		BOOL systemFont;

		HRESULT hr = InternalPointer->ConvertFontToLOGFONT(font->InternalPointer, reinterpret_cast<LOGFONTW*>(pinnedFont), &systemFont);
		return RECORD_DW(hr);
	}

	FontFace^ GdiInterop::FromHdc(IntPtr hdc)
	{
		IDWriteFontFace *face;

		HRESULT hr = InternalPointer->CreateFontFaceFromHdc(reinterpret_cast<HDC>(hdc.ToPointer()), &face);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return FontFace::FromPointer(face);
	}

	Font^ GdiInterop::FromLogFont(Object^ logFont)
	{
		IDWriteFont *font;
		pin_ptr<Object^> pinnedFont = &logFont;

		HRESULT hr = InternalPointer->CreateFontFromLOGFONT(reinterpret_cast<LOGFONTW*>(pinnedFont), &font);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return Font::FromPointer(font);
	}

	BitmapRenderTarget ^GdiInterop::CreateBitmapRenderTarget(System::IntPtr hdc, int width, int height)
	{
		IDWriteBitmapRenderTarget *target = 0;
		HRESULT hr = InternalPointer->CreateBitmapRenderTarget(static_cast<HDC>(hdc.ToPointer()), width, height, &target);
		if (RECORD_DW(hr).IsFailure)
		{
			return nullptr;
		}
		return BitmapRenderTarget::FromPointer(target);
	}
}
}
