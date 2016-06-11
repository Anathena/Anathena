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

extern const IID IID_IDWriteFontFace;

#include "../ComObject.h"
#include "../math/Matrix3x2.h"
#include "../direct2d/SimplifiedGeometrySink.h"

#include "Enums.h"
#include "FontMetrics.h"
#include "GlyphMetrics.h"
#include "FontFile.h"
#include "GlyphOffset.h"
#include "RenderingParameters.h"
#include "FontTable.h"

namespace SlimDX
{
	namespace DirectWrite
	{
		public ref class FontFace : public ComObject
		{
			COMOBJECT(IDWriteFontFace, FontFace);

		public:
			array<GlyphMetrics>^ GetDesignGlyphMetrics(array<short>^ indices, bool isSideways);
			array<GlyphMetrics>^ GetGdiCompatibleGlyphMetrics(float size, float pixelsPerDip, bool useGdiNatural, bool isSideways, array<short>^ indices);
			array<GlyphMetrics>^ GetGdiCompatibleGlyphMetrics(float size, float pixelsPerDip, bool useGdiNatural, bool isSideways, array<short>^ indices, Matrix3x2 transform);
			array<short>^ GetGlyphIndices(array<int>^ codePoints);
			array<FontFile^>^ GetFiles();

			FontMetrics GetGdiCompatibleMetrics(float size, float pixelsPerDip);
			FontMetrics GetGdiCompatibleMetrics(float size, float pixelsPerDip, Matrix3x2 transform);

			Result GetGlyphRunOutline(float size, array<short>^ glyphIndices, array<float>^ glyphAdvances, array<GlyphOffset>^ glyphOffsets, bool isSideways, bool isRightToLeft, SlimDX::Direct2D::SimplifiedGeometrySink^ geometrySink);
			RenderingMode GetRecommendedRenderingMode(float size, float pixelsPerDip, MeasuringMode measuringMode, RenderingParameters^ renderingParameters);

			FontTable^ GetFontTable(int tableTag);
			FontTable^ GetFontTable(char a, char b, char c, char d);
			void ReleaseFontTable(FontTable^ table);

			property FontFaceType Type
			{
				FontFaceType get();
			}

			property FontMetrics Metrics
			{
				FontMetrics get();
			}

			property FontSimulations Simulations
			{
				FontSimulations get();
			}

			property bool IsSymbolFont
			{
				bool get();
			}

			property int GlyphCount
			{
				int get();
			}

			property int Index
			{
				int get();
			}
		};
	}
}