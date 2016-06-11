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

#include "Font.h"
#include "FontFace.h"

const IID IID_IDWriteFont = __uuidof(IDWriteFont);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	FontFace^ Font::CreateFontFace()
	{
		IDWriteFontFace *face;
		HRESULT const hr = InternalPointer->CreateFontFace(&face);
		if (RECORD_DW(hr).IsFailure)
		{
			return nullptr;
		}

		return FontFace::FromPointer(face);
	}

	LocalizedStrings^ Font::GetInformationalStrings(InformationalStringId stringId)
	{
		IDWriteLocalizedStrings *names;
		BOOL exists;

		HRESULT hr = InternalPointer->GetInformationalStrings(static_cast<DWRITE_INFORMATIONAL_STRING_ID>(stringId), &names, &exists);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return LocalizedStrings::FromPointer(names);
	}

	bool Font::HasCharacter(int characterCode)
	{
		BOOL exists = FALSE;

		HRESULT hr = InternalPointer->HasCharacter(characterCode, &exists);
		RECORD_DW(hr);

		return exists > 0;
	}

	LocalizedStrings^ Font::FaceNames::get()
	{
		IDWriteLocalizedStrings *names;

		HRESULT hr = InternalPointer->GetFaceNames(&names);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return LocalizedStrings::FromPointer(names);
	}

	SlimDX::DirectWrite::FontFamily^ Font::FontFamily::get()
	{
		IDWriteFontFamily *names;

		HRESULT hr = InternalPointer->GetFontFamily(&names);
		if (RECORD_DW(hr).IsFailure)
			return nullptr;

		return SlimDX::DirectWrite::FontFamily::FromPointer(names);
	}

	FontMetrics Font::Metrics::get()
	{
		FontMetrics result;

		InternalPointer->GetMetrics(reinterpret_cast<DWRITE_FONT_METRICS*>(&result));

		return result;
	}

	FontSimulations Font::Simulations::get()
	{
		return static_cast<FontSimulations>(InternalPointer->GetSimulations());
	}

	FontStretch Font::Stretch::get()
	{
		return static_cast<FontStretch>(InternalPointer->GetStretch());
	}

	FontStyle Font::Style::get()
	{
		return static_cast<FontStyle>(InternalPointer->GetStyle());
	}

	FontWeight Font::Weight::get()
	{
		return static_cast<FontWeight>(InternalPointer->GetWeight());
	}

	bool Font::IsSymbolFont::get()
	{
		return InternalPointer->IsSymbolFont() > 0;
	}
}
}