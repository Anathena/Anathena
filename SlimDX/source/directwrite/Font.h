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

extern const IID IID_IDWriteFont;

#include "../ComObject.h"
#include "Enums.h"
#include "LocalizedStrings.h"
#include "FontFamily.h"
#include "FontMetrics.h"

namespace SlimDX
{
	namespace DirectWrite
	{
		public ref class Font : public ComObject
		{
			COMOBJECT(IDWriteFont, Font);

		public:
			FontFace^ CreateFontFace();
			LocalizedStrings^ GetInformationalStrings(InformationalStringId stringId);
			bool HasCharacter(int characterCode);

			property LocalizedStrings^ FaceNames
			{
				LocalizedStrings^ get();
			}

			property SlimDX::DirectWrite::FontFamily^ FontFamily
			{
				SlimDX::DirectWrite::FontFamily^ get();
			}

			property FontMetrics Metrics
			{
				FontMetrics get();
			}

			property FontSimulations Simulations
			{
				FontSimulations get();
			}

			property FontStretch Stretch
			{
				FontStretch get();
			}

			property FontStyle Style
			{
				FontStyle get();
			}

			property FontWeight Weight
			{
				FontWeight get();
			}

			property bool IsSymbolFont
			{
				bool get();
			}
		};
	}
}