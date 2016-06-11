#include "stdafx.h"
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

#include "SlimDXTest.h"

using namespace System;
using namespace System::Runtime::InteropServices;

void SlimDXTest::TearDown()
{
	ASSERT_EQ(0, SlimDX::ObjectTable::Objects->Count);
}

void SlimDXTest::AssertLastResultSucceeded()
{
	ASSERT_TRUE(SlimDX::Result::Last.IsSuccess);
}

void SlimDXTest::AssertLastResultFailed()
{
	ASSERT_TRUE(SlimDX::Result::Last.IsFailure);
}

std::ostream &operator<<(std::ostream &stream, String ^str)
{
	LPSTR text = reinterpret_cast<LPSTR>(Marshal::StringToHGlobalAnsi(str).ToPointer());
	stream << text;
	Marshal::FreeHGlobal(IntPtr(static_cast<void *>(text)));
	return stream;
}

std::ostream &operator<<(std::ostream &stream, SlimDX::DirectWrite::FontStretch stretch)
{
	return stream << stretch.ToString();
}

std::ostream &operator<<(std::ostream &stream, SlimDX::DirectWrite::FontStyle style)
{
	return stream << style.ToString();
}

std::ostream &operator<<(std::ostream &stream, SlimDX::DirectWrite::FontWeight weight)
{
	return stream << weight.ToString();
}
