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

#include "Strikethrough.h"

using namespace System;

namespace SlimDX
{
namespace DirectWrite
{
	Strikethrough::Strikethrough(const DWRITE_STRIKETHROUGH &strikethrough)
	{
		Width = strikethrough.width;
		Thickness = strikethrough.thickness;
		Offset = strikethrough.offset;
		ReadingDirection = static_cast<DirectWrite::ReadingDirection>(strikethrough.readingDirection);
		FlowDirection = static_cast<DirectWrite::FlowDirection>(strikethrough.flowDirection);
		LocaleName = gcnew String(strikethrough.localeName);
		MeasuringMode = static_cast<DirectWrite::MeasuringMode>(strikethrough.measuringMode);
	}
}
}
