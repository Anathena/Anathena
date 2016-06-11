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

extern const IID IID_IDWriteBitmapRenderTarget;

#include "../ComObject.h"
#include "../math/Color4.h"
#include "../math/Matrix3x2.h"

#include "Enums.h"
#include "GlyphRunDW.h"
#include "RenderingParameters.h"
#include "GdiInterop.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace DirectWrite
	{
		public ref class BitmapRenderTarget : public ComObject
		{
			COMOBJECT(IDWriteBitmapRenderTarget, BitmapRenderTarget);

		public:
			BitmapRenderTarget(GdiInterop^ interop, System::IntPtr hdc, int width, int height);

			Result DrawGlyphRun(float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun^ glyphRun, RenderingParameters^ renderingParameters, Color4 color);
			Result DrawGlyphRun(float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun^ glyphRun, RenderingParameters^ renderingParameters, Color4 color, [Out] System::Drawing::Rectangle% blackBoxBounds);
			Result Resize(int width, int height);

			property Matrix3x2 Transform
			{
				Matrix3x2 get();
				void set(Matrix3x2 value);
			}

			property float PixelsPerDip
			{
				float get();
				void set(float value);
			}

			property System::Drawing::Size Size
			{
				System::Drawing::Size get();
			}

			property System::IntPtr MemoryDC
			{
				System::IntPtr get();
			}
		};
	}
}
