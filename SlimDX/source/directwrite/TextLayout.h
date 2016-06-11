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

#include "ClusterMetrics.h"
#include "FactoryDW.h"
#include "HitTestMetrics.h"
#include "LineMetrics.h"
#include "TextFormat.h"
#include "TextRange.h"
#include "Typography.h"

extern const IID IID_IDWriteTextLayout;
struct DWRITE_TEXT_RANGE;

namespace SlimDX
{
	namespace DirectWrite
	{
		using namespace System;
		value class OverhangMetrics;
		value class TextMetrics;
		ref class InlineObject;
		interface struct ITextRenderer;
		interface struct IClientDrawingEffect;

		public ref class TextLayout : public TextFormat
		{
			COMOBJECT(IDWriteTextLayout, TextLayout);

			void Init( Factory^ factory, String^ text, TextFormat^ format, float maxWidth, float maxHeight );

		public:
			TextLayout( Factory^ factory, String^ text, TextFormat^ format );
			TextLayout( Factory^ factory, String^ text, TextFormat^ format, float maxWidth, float maxHeight );

			float DetermineMinWidth();
			Result Draw(IntPtr clientDrawingContext, ITextRenderer ^renderer, float originX, float originY);
			HitTestMetrics HitTestPoint( float pointX, float pointY, [Out] bool% isTrailingHit, [Out] bool% isInside );
			HitTestMetrics HitTestTextPosition( int textPosition, bool isTrailingHit, [Out] float% pointX, [Out] float% pointY );
			array< HitTestMetrics >^ HitTestTextRange( int textPosition, int textLength, float originX, float originY );

			array<ClusterMetrics>^ GetClusterMetrics();
			IClientDrawingEffect ^GetDrawingEffect(int currentPosition);
			IClientDrawingEffect ^GetDrawingEffect(int currentPosition, [Out] TextRange %textRange);
			SlimDX::DirectWrite::FontCollection^ GetFontCollection( int currentPosition );
			SlimDX::DirectWrite::FontCollection^ GetFontCollection( int currentPosition, [Out] TextRange% textRange );
			String^ GetFontFamilyName( int currentPosition );
			String^ GetFontFamilyName( int currentPosition, [Out] TextRange% textRange );
			float GetFontSize( int currentPosition );
			float GetFontSize( int currentPosition, [Out] TextRange% textRange );
			SlimDX::DirectWrite::FontStretch GetFontStretch( int currentPosition );
			SlimDX::DirectWrite::FontStretch GetFontStretch(int currentPosition, [Out] TextRange %textRange);
			SlimDX::DirectWrite::FontStyle GetFontStyle(int currentPosition);
			SlimDX::DirectWrite::FontStyle GetFontStyle(int currentPosition, [Out] TextRange %textRange);
			SlimDX::DirectWrite::FontWeight GetFontWeight(int currentPosition);
			SlimDX::DirectWrite::FontWeight GetFontWeight(int currentPosition, [Out] TextRange %textRange);
			InlineObject ^GetInlineObject(int currentPosition);
			InlineObject ^GetInlineObject(int currentPosition, [Out] TextRange %textRange);
			array<LineMetrics> ^GetLineMetrics();
			String ^GetLocaleName(int currentPosition);
			String ^GetLocaleName(int currentPosition, [Out] TextRange %textRange);
			bool GetStrikethrough(int currentPosition);
			bool GetStrikethrough(int currentPosition, [Out] TextRange %textRange);
			Typography ^GetTypography(int currentPosition);
			Typography ^GetTypography(int currentPosition, [Out] TextRange %textRange);
			bool GetUnderline(int currentPosition);
			bool GetUnderline(int currentPosition, [Out] TextRange %textRange);

			Result SetDrawingEffect(IClientDrawingEffect ^drawingEffect, TextRange range);
			Result SetFontCollection( SlimDX::DirectWrite::FontCollection^ collection, TextRange range );
			Result SetFontFamilyName( String^ name, TextRange range );
			Result SetFontSize( float size, TextRange range );
			Result SetFontStretch(SlimDX::DirectWrite::FontStretch stretch, TextRange range);
			Result SetFontStyle(SlimDX::DirectWrite::FontStyle style, TextRange range);
			Result SetFontWeight( SlimDX::DirectWrite::FontWeight weight, TextRange range );
			Result SetInlineObject(InlineObject ^obj, TextRange range);
			Result SetLocaleName(String ^name, TextRange range);
			Result SetStrikethrough(bool strikethrough, TextRange range);
			Result SetTypography( Typography^ typography, TextRange range );
			Result SetUnderline( bool underline, TextRange range );

			property float MaxWidth
			{
				float get();
				void set( float value );
			}

			property float MaxHeight
			{
				float get();
				void set( float value );
			}

			property TextMetrics Metrics
			{
				TextMetrics get();
			}

			property SlimDX::DirectWrite::OverhangMetrics OverhangMetrics
			{
				SlimDX::DirectWrite::OverhangMetrics get();
			}
		};
	}
}