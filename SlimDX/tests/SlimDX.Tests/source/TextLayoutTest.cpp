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

#include <dwrite.h>
#include "TextLayoutTest.h"

using namespace SlimDX::DirectWrite;

//HRESULT const E_NOT_SUFFICIENT_BUFFER = HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER);


DWRITE_TEXT_RANGE ExpectedTextRange()
{
	DWRITE_TEXT_RANGE fakeTextRange;
	fakeTextRange.length = 1;
	fakeTextRange.startPosition = 0;
	return fakeTextRange;
}

void AssertTextRangeMatchesExpected(TextRange range)
{
	DWRITE_TEXT_RANGE expected = ExpectedTextRange();
	ASSERT_EQ(expected.startPosition, range.StartPosition);
	ASSERT_EQ(expected.length, range.Length);
}
