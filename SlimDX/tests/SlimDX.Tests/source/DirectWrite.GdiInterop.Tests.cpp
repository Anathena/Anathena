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

#include "Asserts.h"
#include "SlimDXTest.h"
#include "IDWriteGdiInteropMock.h"
#include "IDWriteBitmapRenderTargetMock.h"

using namespace testing;
using namespace System;
using namespace SlimDX;
using namespace SlimDX::DirectWrite;

ref class MockedGdiInterop
{
public:
	MockedGdiInterop()
		: mockGdiInterop(new IDWriteGdiInteropMock),
		font(SlimDX::DirectWrite::GdiInterop::FromPointer(System::IntPtr(mockGdiInterop)))
	{
	}
	~MockedGdiInterop()
	{
		delete font;
		font = nullptr;
		delete mockGdiInterop;
		mockGdiInterop = 0;
	}
	property IDWriteGdiInteropMock &Mock
	{
		IDWriteGdiInteropMock &get() { return *mockGdiInterop; }
	}
	property SlimDX::DirectWrite::GdiInterop ^GdiInterop
	{
		SlimDX::DirectWrite::GdiInterop ^get() { return font; }
	}

private:
	IDWriteGdiInteropMock *mockGdiInterop;
	SlimDX::DirectWrite::GdiInterop ^font;
};

class GdiInteropTest : public SlimDXTest
{
protected:
	virtual void TearDown()
	{
		ASSERT_EQ(0, ObjectTable::Objects->Count);
	}
};

#define GDIINTEROP_TEST(name_) TEST_F(GdiInteropTest, name_)

GDIINTEROP_TEST(CreateBitmapRenderTarget)
{
	IDWriteBitmapRenderTargetMock mockRenderTarget;
	MockedGdiInterop gdi;
	EXPECT_CALL(gdi.Mock, CreateBitmapRenderTarget(NotNull(), 640, 480, NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<3>(&mockRenderTarget), Return(S_OK)));
	HDC dc = reinterpret_cast<HDC>(0x10);
	BitmapRenderTarget ^target = gdi.GdiInterop->CreateBitmapRenderTarget(System::IntPtr(dc), 640, 480);
	AssertLastResultSucceeded();
	ASSERT_TRUE(target != nullptr);
	delete target;
}
