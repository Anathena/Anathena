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
#include <cassert>
#include <vector>

#include "DirectWriteException.h"
#include "IClientDrawingEffect.h"
#include "InlineObject.h"
#include "OverhangMetrics.h"
#include "TextLayout.h"
#include "TextMetrics.h"
#include "ITextRenderer.h"

const IID IID_IDWriteTextLayout = __uuidof(IDWriteTextLayout);

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	TextLayout::TextLayout( Factory^ factory, System::String^ text, TextFormat^ format )
	{
		Init( factory, text, format, 0, 0 );
	}

	TextLayout::TextLayout( Factory^ factory, System::String^ text, TextFormat^ format, float maxWidth, float maxHeight )
	{
		Init( factory, text, format, maxWidth, maxHeight );
	}

	void TextLayout::Init( Factory^ factory, System::String^ text, TextFormat^ format, float maxWidth, float maxHeight )
	{
		IDWriteTextLayout *layout = NULL;
		pin_ptr<const wchar_t> pinnedText = PtrToStringChars( text );

		HRESULT hr = factory->InternalPointer->CreateTextLayout( pinnedText, text->Length, format->InternalPointer, maxWidth, maxHeight, &layout );
		if( RECORD_DW( hr ).IsFailure )
			throw gcnew DirectWriteException( Result::Last );

		Construct( layout );
	}

	float TextLayout::DetermineMinWidth()
	{
		float minWidth = -1.0f;
		RECORD_DW( InternalPointer->DetermineMinWidth(&minWidth) );
		return minWidth;
	}

	Result TextLayout::Draw(IntPtr clientDrawingContext, ITextRenderer ^renderer, float originX, float originY)
	{
		ITextRendererShim *shim = ITextRendererShim::CreateInstance(renderer);

		HRESULT hr = InternalPointer->Draw(static_cast<void *>(clientDrawingContext), shim, originX, originY);
		shim->Release();

		return RECORD_DW(hr);
	}

	HitTestMetrics TextLayout::HitTestPoint( float pointX, float pointY, [Out] bool% isTrailingHit, [Out] bool% isInside )
	{
		DWRITE_HIT_TEST_METRICS htm;
		BOOL trailingHit;
		BOOL inside;

		HRESULT hr = InternalPointer->HitTestPoint( pointX, pointY, &trailingHit, &inside, &htm );
		if( RECORD_DW( hr ).IsFailure )
		{
			return HitTestMetrics();
		}

		isTrailingHit = trailingHit == TRUE;
		isInside = inside == TRUE;
		return HitTestMetrics( htm.textPosition, htm.length, htm.left, htm.top, htm.width, htm.height,
			htm.bidiLevel, htm.isText == TRUE, htm.isTrimmed == TRUE );
	}

	HitTestMetrics TextLayout::HitTestTextPosition( int textPosition, bool isTrailingHit, [Out] float% pointX, [Out] float% pointY )
	{
		DWRITE_HIT_TEST_METRICS htm;
		FLOAT x;
		FLOAT y;

		HRESULT hr = InternalPointer->HitTestTextPosition( textPosition, isTrailingHit, &x, &y, &htm );
		if( RECORD_DW( hr ).IsFailure )
		{
			return HitTestMetrics();
		}

		pointX = x;
		pointY = y;
		return HitTestMetrics( htm.textPosition, htm.length, htm.left, htm.top, htm.width, htm.height,
			htm.bidiLevel, htm.isText == TRUE, htm.isTrimmed == TRUE );
	}

	array<HitTestMetrics>^ TextLayout::HitTestTextRange( int textPosition, int textLength, float originX, float originY )
	{
		// Find out how many hit-test metrics to buffer for
		UINT32 count = 0;
		{
			HRESULT const hr = InternalPointer->HitTestTextRange(textPosition, textLength,
				originX, originY, 0, 0, &count);
			hr, assert(FAILED(hr) && (HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER) == hr));
		}

		std::vector<DWRITE_HIT_TEST_METRICS> metrics(count);
		if (RECORD_DW(InternalPointer->HitTestTextRange(
				textPosition, textLength, originX, originY, 
				&metrics[0], count, &count)).IsFailure)
		{
			return nullptr;
		}

		array<HitTestMetrics>^ result = gcnew array<HitTestMetrics>(count);
		for (UINT32 i = 0; i < count; ++i )
		{
			result[i] = HitTestMetrics( metrics[i].textPosition, metrics[i].length,
				metrics[i].left, metrics[i].top, metrics[i].width, metrics[i].height,
				metrics[i].bidiLevel, metrics[i].isText == TRUE, metrics[i].isTrimmed == TRUE);
		}
		return result;
	}

	array<ClusterMetrics>^ TextLayout::GetClusterMetrics()
	{
		UINT32 count = 0;
		{
			HRESULT const hr = InternalPointer->GetClusterMetrics(0, 0, &count);
			hr, assert(FAILED(hr) && (HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER) == hr));
		}

		std::vector<DWRITE_CLUSTER_METRICS> metrics;
		metrics.resize(count);
		if (RECORD_DW(InternalPointer->GetClusterMetrics(&metrics[0], count, &count)).IsFailure)
		{
			return nullptr;
		}
		array<SlimDX::DirectWrite::ClusterMetrics>^ result = gcnew array<SlimDX::DirectWrite::ClusterMetrics>(count);
		for (UINT32 i = 0; i < count; i++)
		{
			result[i] = ClusterMetrics( metrics[i].width, metrics[i].length,
				metrics[i].canWrapLineAfter != 0,
				metrics[i].isWhitespace != 0,
				metrics[i].isNewline != 0,
				metrics[i].isSoftHyphen != 0,
				metrics[i].isRightToLeft != 0 );
		}
		return result;
	}

	static IClientDrawingEffect ^ClientDrawingEffectFromUnknown(IUnknown *)
	{
		// TOOD: Return the IClientDrawingEffect associated with this IUnknown
		return nullptr;
	}

	static TextRange TextRangeFromNative(DWRITE_TEXT_RANGE const &range)
	{
		TextRange result;
		result.StartPosition = range.startPosition;
		result.Length = range.length;
		return result;
	}

	static IClientDrawingEffect ^GetDrawingEffectInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *textRange)
	{
		IUnknown *nativeDrawingEffect;
		if (RECORD_DW(layout->GetDrawingEffect(currentPosition, &nativeDrawingEffect, textRange)).IsFailure)
		{
			return nullptr;
		}
		return ClientDrawingEffectFromUnknown(nativeDrawingEffect);
	}

	IClientDrawingEffect ^TextLayout::GetDrawingEffect(int currentPosition)
	{
		return GetDrawingEffectInternal(InternalPointer, currentPosition, 0);
	}

	IClientDrawingEffect ^TextLayout::GetDrawingEffect(int currentPosition,	[Out] TextRange %textRange)
	{
		DWRITE_TEXT_RANGE range;
		IClientDrawingEffect ^result = GetDrawingEffectInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}
		return result;
	}

	static FontCollection ^GetFontCollectionInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *textRange)
	{
		IDWriteFontCollection* fc;
		if( RECORD_DW(layout->GetFontCollection(currentPosition, &fc, textRange)).IsFailure)
		{
			return nullptr;
		}

		return SlimDX::DirectWrite::FontCollection::FromPointer(fc);
	}

	FontCollection^ TextLayout::GetFontCollection ( int currentPosition )
	{
		return GetFontCollectionInternal(InternalPointer, currentPosition, 0);
	}

	SlimDX::DirectWrite::FontCollection^ TextLayout::GetFontCollection ( int currentPosition, [Out] TextRange% textRange )
	{
		DWRITE_TEXT_RANGE tr;
		SlimDX::DirectWrite::FontCollection ^collection = GetFontCollectionInternal(InternalPointer, currentPosition, &tr);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(tr);
		}

		return collection;
	}

	static String ^GetFontFamilyNameInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *textRange)
	{
		UINT32 length = 0U;
		if (RECORD_DW(layout->GetFontFamilyNameLength(currentPosition, &length, textRange)).IsFailure)
		{
			return String::Empty;
		}

		std::vector<WCHAR> name(length + 1);
		if (RECORD_DW(layout->GetFontFamilyName(currentPosition, &name[0], static_cast<UINT32>(name.size()), textRange)).IsFailure)
		{
			return String::Empty;
		}

		return gcnew String(&name[0]);
	}

	String^ TextLayout::GetFontFamilyName( int currentPosition )
	{
		return GetFontFamilyNameInternal(InternalPointer, currentPosition, 0);
	}

	String^ TextLayout::GetFontFamilyName( int currentPosition, [Out] TextRange% textRange )
	{
		DWRITE_TEXT_RANGE range;
		String ^familyName = GetFontFamilyNameInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}

		return familyName;
	}

	static float GetFontSizeInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		float result = -1.0f;
		RECORD_DW(layout->GetFontSize(currentPosition, &result, range));
		return result;
	}

	float TextLayout::GetFontSize( int currentPosition )
	{
		return GetFontSizeInternal(InternalPointer, currentPosition, 0);
	}

	float TextLayout::GetFontSize(int currentPosition, [Out] TextRange% textRange)
	{
		DWRITE_TEXT_RANGE range;
		float const result = GetFontSizeInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}
		return result;
	}

	static FontStretch GetFontStretchInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		DWRITE_FONT_STRETCH stretch;
		if (RECORD_DW(layout->GetFontStretch(currentPosition, &stretch, range)).IsFailure)
		{
			stretch = DWRITE_FONT_STRETCH_UNDEFINED;
		}
		return static_cast<FontStretch>(stretch);
	}

	SlimDX::DirectWrite::FontStretch TextLayout::GetFontStretch(int currentPosition)
	{
		return GetFontStretchInternal(InternalPointer, currentPosition, 0);
	}

	SlimDX::DirectWrite::FontStretch TextLayout::GetFontStretch(int currentPosition, [Out] TextRange %textRange)
	{
		DWRITE_TEXT_RANGE range;
		SlimDX::DirectWrite::FontStretch result = GetFontStretchInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}
		return result;
	}

	static SlimDX::DirectWrite::FontStyle GetFontStyleInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		DWRITE_FONT_STYLE style;
		if (RECORD_DW(layout->GetFontStyle(currentPosition, &style, range)).IsFailure)
		{
			style = DWRITE_FONT_STYLE(-1);
		}
		return static_cast<FontStyle>(style);
	}

	SlimDX::DirectWrite::FontStyle TextLayout::GetFontStyle(int currentPosition)
	{
		return GetFontStyleInternal(InternalPointer, currentPosition, 0);
	}

	SlimDX::DirectWrite::FontStyle TextLayout::GetFontStyle(int currentPosition, [Out] TextRange %textRange)
	{
		DWRITE_TEXT_RANGE range;
		SlimDX::DirectWrite::FontStyle style = GetFontStyleInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}
		return style;
	}

	static FontWeight GetFontWeightInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		DWRITE_FONT_WEIGHT weight;
		if (RECORD_DW(layout->GetFontWeight(currentPosition, &weight, range)).IsFailure)
		{
			weight = DWRITE_FONT_WEIGHT(-1);
		}
		return static_cast<FontWeight>(weight);
	}

	SlimDX::DirectWrite::FontWeight TextLayout::GetFontWeight(int currentPosition)
	{
		return GetFontWeightInternal(InternalPointer, currentPosition, 0);
	}

	SlimDX::DirectWrite::FontWeight TextLayout::GetFontWeight(int currentPosition, [Out] TextRange %textRange)
	{
		DWRITE_TEXT_RANGE range;
		SlimDX::DirectWrite::FontWeight result = GetFontWeightInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}
		return result;
	}

	static InlineObject ^GetInlineObjectInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		IDWriteInlineObject *obj;
		if (RECORD_DW(layout->GetInlineObject(currentPosition, &obj, range)).IsFailure)
		{
			return nullptr;
		}
		return InlineObject::FromPointer(obj);
	}

	InlineObject ^TextLayout::GetInlineObject(int currentPosition)
	{
		return GetInlineObjectInternal(InternalPointer, currentPosition, 0);
	}

	InlineObject ^TextLayout::GetInlineObject(int currentPosition, [Out] TextRange %textRange)
	{
		DWRITE_TEXT_RANGE range;
		InlineObject ^result = GetInlineObjectInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}
		return result;
	}

	array<LineMetrics> ^TextLayout::GetLineMetrics()
	{
		UINT32 count = 0;
		{
			HRESULT const hr = InternalPointer->GetLineMetrics(0, 0, &count);
			hr, assert(FAILED(hr) && (HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER) == hr));
		}

		std::vector<DWRITE_LINE_METRICS> metrics;
		metrics.resize(count);
		if (RECORD_DW(InternalPointer->GetLineMetrics(&metrics[0], count, &count)).IsFailure)
		{
			return nullptr;
		}
		array<SlimDX::DirectWrite::LineMetrics>^ result = gcnew array<SlimDX::DirectWrite::LineMetrics>(count);
		for (UINT32 i = 0; i < count; i++)
		{
			result[i] = LineMetrics(metrics[i].length, metrics[i].trailingWhitespaceLength, metrics[i].newlineLength,
				metrics[i].height, metrics[i].baseline, metrics[i].isTrimmed != 0);
		}
		return result;
	}

	static String ^GetLocaleNameInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		UINT32 length = 0U;
		if (RECORD_DW(layout->GetLocaleNameLength(currentPosition, &length, 0)).IsFailure)
		{
			return String::Empty;
		}

		std::vector<WCHAR> name(length + 1);
		if (RECORD_DW(layout->GetLocaleName(currentPosition, &name[0], static_cast<UINT32>(name.size()), range)).IsFailure)
		{
			return String::Empty;
		}

		return gcnew String(&name[0]);
	}

	String ^TextLayout::GetLocaleName(int currentPosition)
	{
		return GetLocaleNameInternal(InternalPointer, currentPosition, 0);
	}

	String ^TextLayout::GetLocaleName(int currentPosition, [Out] TextRange %range)
	{
		DWRITE_TEXT_RANGE textRange;
		String ^result = GetLocaleNameInternal(InternalPointer, currentPosition, &textRange);
		if (Result::Last.IsSuccess)
		{
			range = TextRangeFromNative(textRange);
		}
		return result;
	}

	static bool GetStrikethroughInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		BOOL strikethrough;
		if (RECORD_DW(layout->GetStrikethrough(currentPosition, &strikethrough, range)).IsFailure)
		{
			strikethrough = FALSE;
		}
		return strikethrough == TRUE;
	}

	bool TextLayout::GetStrikethrough(int currentPosition)
	{
		return GetStrikethroughInternal(InternalPointer, currentPosition, 0);
	}

	bool TextLayout::GetStrikethrough(int currentPosition, [Out] TextRange %textRange)
	{
		DWRITE_TEXT_RANGE range = { 0 };
		bool const result = GetStrikethroughInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}
		return result;
	}

	static Typography ^GetTypographyInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		IDWriteTypography *typography = 0;
		if (RECORD_DW(layout->GetTypography(currentPosition, &typography, range)).IsFailure)
		{
			return nullptr;
		}

		return Typography::FromPointer(typography);
	}

	Typography ^TextLayout::GetTypography(int currentPosition)
	{
		return GetTypographyInternal(InternalPointer, currentPosition, 0);
	}

	Typography ^TextLayout::GetTypography(int currentPosition, [Out] TextRange %textRange)
	{
		DWRITE_TEXT_RANGE range;
		Typography ^result = GetTypographyInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}

		return result;
	}

	static bool GetUnderlineInternal(IDWriteTextLayout *layout, int currentPosition, DWRITE_TEXT_RANGE *range)
	{
		BOOL result;
		if (RECORD_DW(layout->GetUnderline(currentPosition, &result, range)).IsFailure)
		{
			result = FALSE;
		}
		return result == TRUE;
	}

	bool TextLayout::GetUnderline(int currentPosition)
	{
		return GetUnderlineInternal(InternalPointer, currentPosition, 0);
	}

	bool TextLayout::GetUnderline(int currentPosition, [Out] TextRange %textRange)
	{
		DWRITE_TEXT_RANGE range;
		bool const result = GetUnderlineInternal(InternalPointer, currentPosition, &range);
		if (Result::Last.IsSuccess)
		{
			textRange = TextRangeFromNative(range);
		}
		return result;
	}

	static DWRITE_TEXT_RANGE TextRangeFromManaged(TextRange range)
	{
		DWRITE_TEXT_RANGE tr;
		tr.startPosition = range.StartPosition;
		tr.length = range.Length;
		return tr;
	}

	Result TextLayout::SetDrawingEffect(IClientDrawingEffect ^drawingEffect, TextRange range)
	{
		return RECORD_DW(InternalPointer->
			SetDrawingEffect(drawingEffect == nullptr ? 0 : reinterpret_cast<IUnknown*>(drawingEffect->ComPointer.ToPointer()),
				TextRangeFromManaged(range)));
	}

	Result TextLayout::SetFontCollection( SlimDX::DirectWrite::FontCollection^ collection, TextRange range )
	{
		return RECORD_DW(InternalPointer->SetFontCollection( collection->InternalPointer, TextRangeFromManaged(range)));
	}

	Result TextLayout::SetFontFamilyName(String ^name, TextRange range)
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(name);
		return RECORD_DW(InternalPointer->SetFontFamilyName(pinnedName, TextRangeFromManaged(range)));
	}

	Result TextLayout::SetFontSize( float size, TextRange range )
	{
		return RECORD_DW(InternalPointer->SetFontSize( size, TextRangeFromManaged(range)));
	}

	Result TextLayout::SetFontStretch(SlimDX::DirectWrite::FontStretch stretch, TextRange range)
	{
		return RECORD_DW(InternalPointer->SetFontStretch(static_cast<DWRITE_FONT_STRETCH>(stretch), TextRangeFromManaged(range)));
	}

	Result TextLayout::SetFontStyle(SlimDX::DirectWrite::FontStyle style, TextRange range)
	{
		return RECORD_DW(InternalPointer->SetFontStyle(static_cast<DWRITE_FONT_STYLE>(style), TextRangeFromManaged(range)));
	}

	Result TextLayout::SetFontWeight( SlimDX::DirectWrite::FontWeight weight, TextRange range )
	{
		return RECORD_DW(InternalPointer->SetFontWeight(static_cast<DWRITE_FONT_WEIGHT>(weight), TextRangeFromManaged(range)));
	}

	Result TextLayout::SetInlineObject(InlineObject ^obj, TextRange range)
	{
		return RECORD_DW(InternalPointer->SetInlineObject(obj->InternalPointer, TextRangeFromManaged(range)));
	}

	Result TextLayout::SetLocaleName(String ^name, TextRange range)
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars(name);
		return RECORD_DW(InternalPointer->SetLocaleName(pinnedName, TextRangeFromManaged(range)));
	}

	Result TextLayout::SetStrikethrough(bool strikethrough, TextRange range)
	{
		return RECORD_DW(InternalPointer->SetStrikethrough(strikethrough, TextRangeFromManaged(range)));
	}

	Result TextLayout::SetTypography( Typography^ typography, TextRange range )
	{
		return RECORD_DW(InternalPointer->SetTypography(typography->InternalPointer, TextRangeFromManaged(range)));
	}

	Result TextLayout::SetUnderline( bool underline, TextRange range )
	{
		return RECORD_DW(InternalPointer->SetUnderline( underline, TextRangeFromManaged(range)));
	}

	float TextLayout::MaxWidth::get()
	{
		return InternalPointer->GetMaxWidth();
	}

	void TextLayout::MaxWidth::set( float value )
	{
		RECORD_DW( InternalPointer->SetMaxWidth( value ) );
	}

	float TextLayout::MaxHeight::get()
	{
		return InternalPointer->GetMaxHeight();
	}

	void TextLayout::MaxHeight::set( float value )
	{
		RECORD_DW( InternalPointer->SetMaxHeight( value ) );
	}

	TextMetrics TextLayout::Metrics::get()
	{
		DWRITE_TEXT_METRICS metrics;
		if (RECORD_DW(InternalPointer->GetMetrics(&metrics)).IsFailure)
		{
			DWRITE_TEXT_METRICS const zero = { 0 };
			metrics = zero;
		}
		return TextMetrics(metrics.left, metrics.top,
			metrics.width, metrics.widthIncludingTrailingWhitespace, metrics.height,
			metrics.layoutWidth, metrics.layoutHeight,
			metrics.maxBidiReorderingDepth, metrics.lineCount);
	}

	OverhangMetrics TextLayout::OverhangMetrics::get()
	{
		DWRITE_OVERHANG_METRICS metrics;
		if (RECORD_DW(InternalPointer->GetOverhangMetrics(&metrics)).IsFailure)
		{
			DWRITE_OVERHANG_METRICS zero = { 0 };
			metrics = zero;
		}
		return SlimDX::DirectWrite::OverhangMetrics(metrics.left, metrics.top, metrics.right, metrics.bottom);
	}
}
}