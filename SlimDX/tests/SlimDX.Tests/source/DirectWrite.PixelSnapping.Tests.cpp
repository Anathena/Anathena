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

#include "CommonMocks.h"
#include "SlimDXTest.h"

using namespace testing;
using namespace SlimDX;
using namespace SlimDX::Direct2D;
using namespace SlimDX::DirectWrite;
using namespace System;

class IDWritePixelSnappingMock : public IDWritePixelSnapping
{
public:
	MOCK_IUNKNOWN;

	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, IsPixelSnappingDisabled, HRESULT(void*, BOOL*) );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetCurrentTransform, HRESULT(void*, DWRITE_MATRIX*) );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetPixelsPerDip, HRESULT(void*, FLOAT*) );
};

ref class MockedPixelSnapping
{
public:
	MockedPixelSnapping()
		: mockPixelSnapping(new IDWritePixelSnappingMock),
		obj(SlimDX::DirectWrite::PixelSnapping::FromPointer(System::IntPtr(mockPixelSnapping))),
		throwOnError(SlimDX::Configuration::ThrowOnError)
	{
	}
	~MockedPixelSnapping()
	{
		SlimDX::Configuration::ThrowOnError = throwOnError;
		delete obj;
		obj = nullptr;
		delete mockPixelSnapping;
		mockPixelSnapping = 0;
	}
	property IDWritePixelSnappingMock &Mock
	{
		IDWritePixelSnappingMock &get() { return *mockPixelSnapping; }
	}
	property SlimDX::DirectWrite::PixelSnapping ^PixelSnapping
	{
		SlimDX::DirectWrite::PixelSnapping ^get() { return obj; }
	}

private:
	bool const throwOnError;
	IDWritePixelSnappingMock *mockPixelSnapping;
	SlimDX::DirectWrite::PixelSnapping ^obj;
};

class PixelSnappingTest : public SlimDXTest
{
};

#define SNAPPING_TEST(name_) TEST_F(PixelSnappingTest, name_)

SNAPPING_TEST(IsPixelSnappingDisabled)
{
	MockedPixelSnapping snapping;
	EXPECT_CALL(snapping.Mock, IsPixelSnappingDisabled(0, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<1>(TRUE), Return(S_OK)));
	ASSERT_TRUE(snapping.PixelSnapping->IsPixelSnappingDisabled(IntPtr(0)));
	AssertLastResultSucceeded();
}

static DWRITE_MATRIX ExpectedMatrix()
{
	DWRITE_MATRIX expected;
	expected.m11 = 1.0f;
	expected.m12 = 0.0f;
	expected.m21 = 0.0f;
	expected.m22 = 1.0f;
	expected.dx = 12.5f;
	expected.dy = 25.5f;
	return expected;
}

static void AssertMatrixMatchesExpected(Matrix3x2 matrix)
{
	DWRITE_MATRIX expected = ExpectedMatrix();
	ASSERT_EQ(expected.m11, matrix.M11);
	ASSERT_EQ(expected.m12, matrix.M12);
	ASSERT_EQ(expected.m21, matrix.M21);
	ASSERT_EQ(expected.m22, matrix.M22);
	ASSERT_EQ(expected.dx, matrix.M31);
	ASSERT_EQ(expected.dy, matrix.M32);
}

SNAPPING_TEST(GetCurrentTransform)
{
	MockedPixelSnapping snapping;
	EXPECT_CALL(snapping.Mock, GetCurrentTransform(0, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<1>(ExpectedMatrix()), Return(S_OK)));
	Matrix3x2 transform = snapping.PixelSnapping->GetCurrentTransform(IntPtr(0));
	AssertLastResultSucceeded();
	AssertMatrixMatchesExpected(transform);
}

SNAPPING_TEST(GetPixelsPerDip)
{
	MockedPixelSnapping snapping;
	EXPECT_CALL(snapping.Mock, GetPixelsPerDip(0, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<1>(12.5f), Return(S_OK)));
	ASSERT_EQ(12.5f, snapping.PixelSnapping->GetPixelsPerDip(IntPtr(0)));
	AssertLastResultSucceeded();
}
