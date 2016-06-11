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

#include "../ComObject.h"

#include "FactoryDW.h"
#include "Enums.h"
#include "FontCollection.h"
#include "Trimming.h"

extern const IID IID_IDWriteTextFormat;

namespace SlimDX
{
	namespace DirectWrite
	{
		using namespace System;
		ref class InlineObject;

		public ref class TextFormat : public ComObject
		{
			COMOBJECT(IDWriteTextFormat, TextFormat);
			
			void Init( Factory^ factory, System::String^ familyName, FontWeight weight, FontStyle style, FontStretch stretch, float fontSize, System::String^ localeName, FontCollection^ fontCollection );

		private protected:
			TextFormat() { }

		public:
			TextFormat( Factory^ factory, System::String^ familyName, FontWeight weight, FontStyle style, FontStretch stretch, float fontSize, System::String^ localeName );
			TextFormat( Factory^ factory, System::String^ familyName, FontWeight weight, FontStyle style, FontStretch stretch, float fontSize, System::String^ localeName, FontCollection^ fontCollection );

			property FlowDirection FlowDirection
			{
				SlimDX::DirectWrite::FlowDirection get();
				void set(SlimDX::DirectWrite::FlowDirection value);
			}

			property FontCollection ^FontCollection
			{
				SlimDX::DirectWrite::FontCollection ^get();
			}

			property String ^FontFamilyName
			{
				String ^get();
			}

			property float FontSize
			{
				float get();
			}

			property SlimDX::DirectWrite::FontStretch FontStretch
			{
				SlimDX::DirectWrite::FontStretch get();
			}

			property SlimDX::DirectWrite::FontStyle FontStyle
			{
				SlimDX::DirectWrite::FontStyle get();
			}

			property SlimDX::DirectWrite::FontWeight FontWeight
			{
				SlimDX::DirectWrite::FontWeight get();
			}

			property float IncrementalTabStop
			{
				float get();
				void set(float value);
			}

			Result GetLineSpacing([Out] LineSpacingMethod %method, [Out] float %lineSpacing, [Out] float %baseline);
			Result SetLineSpacing(LineSpacingMethod method, float lineSpacing, float baseline);

			property String ^LocaleName
			{
				String ^get();
			}

			property ParagraphAlignment ParagraphAlignment
			{
				SlimDX::DirectWrite::ParagraphAlignment get();
				void set( SlimDX::DirectWrite::ParagraphAlignment value );
			}

			property SlimDX::DirectWrite::ReadingDirection ReadingDirection
			{
				SlimDX::DirectWrite::ReadingDirection get();
				void set(SlimDX::DirectWrite::ReadingDirection value);
			}

			property TextAlignment TextAlignment
			{
				SlimDX::DirectWrite::TextAlignment get();
				void set( SlimDX::DirectWrite::TextAlignment value );
			}

			Trimming GetTrimming();
			Trimming GetTrimming([Out] InlineObject ^%trimmingSign);

			property SlimDX::DirectWrite::WordWrapping WordWrapping
			{
				SlimDX::DirectWrite::WordWrapping get();
				void set(SlimDX::DirectWrite::WordWrapping value);
			}
		};
	}
}
