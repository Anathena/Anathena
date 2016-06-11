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
#include <vector>

#include "DirectWriteException.h"
#include "InlineObject.h"
#include "TextFormat.h"

const IID IID_IDWriteTextFormat = __uuidof(IDWriteTextFormat);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	TextFormat::TextFormat( Factory^ factory, String^ familyName, SlimDX::DirectWrite::FontWeight weight, SlimDX::DirectWrite::FontStyle style, SlimDX::DirectWrite::FontStretch stretch, 
		float fontSize, String^ localeName, SlimDX::DirectWrite::FontCollection^ fontCollection )
	{
		Init( factory, familyName, weight, style, stretch, fontSize, localeName, fontCollection );
	}

	TextFormat::TextFormat( Factory^ factory, String^ familyName, SlimDX::DirectWrite::FontWeight weight, SlimDX::DirectWrite::FontStyle style, SlimDX::DirectWrite::FontStretch stretch, 
		float fontSize, String^ localeName )
	{
		Init( factory, familyName, weight, style, stretch, fontSize, localeName, nullptr );
	}

	void TextFormat::Init( Factory^ factory, String^ familyName, SlimDX::DirectWrite::FontWeight weight, SlimDX::DirectWrite::FontStyle style, SlimDX::DirectWrite::FontStretch stretch, 
		float fontSize, String^ localeName, SlimDX::DirectWrite::FontCollection^ fontCollection )
	{
		IDWriteTextFormat *textFormat = NULL;
		IDWriteFontCollection *fc = fontCollection == nullptr ? NULL : fontCollection->InternalPointer;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( familyName );
		pin_ptr<const wchar_t> pinnedLocale = PtrToStringChars( localeName );

		HRESULT hr = factory->InternalPointer->CreateTextFormat( pinnedName, fc, static_cast<DWRITE_FONT_WEIGHT>( weight ), static_cast<DWRITE_FONT_STYLE>( style ),
			static_cast<DWRITE_FONT_STRETCH>( stretch ), fontSize, pinnedLocale, &textFormat );

		if( RECORD_DW( hr ).IsFailure )
			throw gcnew DirectWriteException( Result::Last );

		Construct( textFormat );
	}

	SlimDX::DirectWrite::FlowDirection TextFormat::FlowDirection::get()
	{
		return static_cast<SlimDX::DirectWrite::FlowDirection>(InternalPointer->GetFlowDirection());
	}

	void TextFormat::FlowDirection::set(SlimDX::DirectWrite::FlowDirection value)
	{
		InternalPointer->SetFlowDirection(static_cast<DWRITE_FLOW_DIRECTION>(value));
	}

	SlimDX::DirectWrite::FontCollection ^TextFormat::FontCollection::get()
	{
		IDWriteFontCollection *collection;
		if (RECORD_DW(InternalPointer->GetFontCollection(&collection)).IsFailure)
		{
			return nullptr;
		}

		return SlimDX::DirectWrite::FontCollection::FromPointer(collection);
	}

	String ^TextFormat::FontFamilyName::get()
	{
		UINT32 count = InternalPointer->GetFontFamilyNameLength();
		std::vector<WCHAR> name;
		name.resize(count + 1);
		if (RECORD_DW(InternalPointer->GetFontFamilyName(&name[0], static_cast<UINT32>(name.size()))).IsFailure)
		{
			return String::Empty;
		}
		return gcnew String(&name[0]);
	}

	float TextFormat::FontSize::get()
	{
		return InternalPointer->GetFontSize();
	}

	SlimDX::DirectWrite::FontStretch TextFormat::FontStretch::get()
	{
		return static_cast<SlimDX::DirectWrite::FontStretch>(InternalPointer->GetFontStretch());
	}

	SlimDX::DirectWrite::FontStyle TextFormat::FontStyle::get()
	{
		return static_cast<SlimDX::DirectWrite::FontStyle>(InternalPointer->GetFontStyle());
	}

	SlimDX::DirectWrite::FontWeight TextFormat::FontWeight::get()
	{
		return static_cast<SlimDX::DirectWrite::FontWeight>(InternalPointer->GetFontWeight());
	}

	float TextFormat::IncrementalTabStop::get()
	{
		return InternalPointer->GetIncrementalTabStop();
	}

	void TextFormat::IncrementalTabStop::set(float value)
	{
		RECORD_DW(InternalPointer->SetIncrementalTabStop(value));
	}

	Result TextFormat::GetLineSpacing([Out] LineSpacingMethod %method, [Out] float %lineSpacing, [Out] float %baseline)
	{
		DWRITE_LINE_SPACING_METHOD nativeMethod;
		float nativeLineSpacing, nativeBaseline;
		Result result = RECORD_DW(InternalPointer->GetLineSpacing(&nativeMethod, &nativeLineSpacing, &nativeBaseline));
		method = static_cast<LineSpacingMethod>(nativeMethod);
		lineSpacing = nativeLineSpacing;
		baseline = nativeBaseline;
		return result;
	}

	Result TextFormat::SetLineSpacing(LineSpacingMethod method, float lineSpacing, float baseline)
	{
		return RECORD_DW(InternalPointer->SetLineSpacing(static_cast<DWRITE_LINE_SPACING_METHOD>(method), lineSpacing, baseline));
	}

	String ^TextFormat::LocaleName::get()
	{
		UINT32 count = InternalPointer->GetLocaleNameLength();
		std::vector<WCHAR> name;
		name.resize(count + 1);
		if (RECORD_DW(InternalPointer->GetLocaleName(&name[0], static_cast<UINT32>(name.size()))).IsFailure)
		{
			return String::Empty;
		}
		return gcnew String(&name[0]);
	}

	SlimDX::DirectWrite::ParagraphAlignment TextFormat::ParagraphAlignment::get()
	{
		return static_cast<SlimDX::DirectWrite::ParagraphAlignment>( InternalPointer->GetParagraphAlignment() );
	}

	void TextFormat::ParagraphAlignment::set( SlimDX::DirectWrite::ParagraphAlignment value )
	{
		InternalPointer->SetParagraphAlignment( static_cast<DWRITE_PARAGRAPH_ALIGNMENT>( value ) );
	}

	SlimDX::DirectWrite::ReadingDirection TextFormat::ReadingDirection::get()
	{
		return static_cast<SlimDX::DirectWrite::ReadingDirection>(InternalPointer->GetReadingDirection());
	}

	void TextFormat::ReadingDirection::set(SlimDX::DirectWrite::ReadingDirection value)
	{
		RECORD_DW(InternalPointer->SetReadingDirection(static_cast<DWRITE_READING_DIRECTION>(value)));
	}

	SlimDX::DirectWrite::TextAlignment TextFormat::TextAlignment::get()
	{
		return static_cast<SlimDX::DirectWrite::TextAlignment>( InternalPointer->GetTextAlignment() );
	}

	void TextFormat::TextAlignment::set( SlimDX::DirectWrite::TextAlignment value )
	{
		InternalPointer->SetTextAlignment( static_cast<DWRITE_TEXT_ALIGNMENT>( value ) );
	}

	static Trimming GetTrimmingInternal(IDWriteTextFormat *format, IDWriteInlineObject **obj)
	{
		DWRITE_TRIMMING trimming;
		if (RECORD_DW(format->GetTrimming(&trimming, obj)).IsFailure)
		{
			DWRITE_TRIMMING zero = { DWRITE_TRIMMING_GRANULARITY(0) };
			trimming = zero;
		}
		return Trimming(static_cast<TrimmingGranularity>(trimming.granularity),
			trimming.delimiter, trimming.delimiterCount);
	}

	Trimming TextFormat::GetTrimming()
	{
		return GetTrimmingInternal(InternalPointer, 0);
	}
	Trimming TextFormat::GetTrimming([Out] InlineObject ^%trimmingSign)
	{
		IDWriteInlineObject *obj = 0;
		Trimming result = GetTrimmingInternal(InternalPointer, &obj);
		if (obj)
		{
			trimmingSign = InlineObject::FromPointer(obj);
		}
		return result;
	}

	SlimDX::DirectWrite::WordWrapping TextFormat::WordWrapping::get()
	{
		return static_cast<SlimDX::DirectWrite::WordWrapping>(InternalPointer->GetWordWrapping());
	}

	void TextFormat::WordWrapping::set(SlimDX::DirectWrite::WordWrapping value)
	{
		RECORD_DW(InternalPointer->SetWordWrapping(static_cast<DWRITE_WORD_WRAPPING>(value)));
	}
}
}