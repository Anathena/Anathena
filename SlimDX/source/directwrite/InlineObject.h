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

extern const IID IID_IDWriteInlineObject;

#include "../ComObject.h"
#include "Enums.h"
#include "InlineObjectMetrics.h"
#include "OverhangMetrics.h"

namespace SlimDX
{
	namespace DirectWrite
	{
		interface struct ITextRenderer;
		interface struct IClientDrawingEffect;

		public ref class InlineObject : public ComObject
		{
			COMOBJECT(IDWriteInlineObject, InlineObject);

		public:
			Result Draw(System::IntPtr clientDrawingContext, ITextRenderer ^renderer,
				float originX, float originY, bool isSideways, bool isRightToLeft,
				IClientDrawingEffect ^clientDrawingEffect);
			Result GetBreakConditions([Out] BreakCondition %before, [Out] BreakCondition %after);
			property InlineObjectMetrics Metrics
			{
				InlineObjectMetrics get();
			}
			property SlimDX::DirectWrite::OverhangMetrics OverhangMetrics
			{
				SlimDX::DirectWrite::OverhangMetrics get();
			}
		};
	}
}
