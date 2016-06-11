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
#include "IDWriteFontMock.h"
#include "IDWriteFontFaceMock.h"

using namespace testing;
using namespace System;
using namespace SlimDX;
using namespace SlimDX::DirectWrite;

ref class MockedFont
{
public:
	MockedFont()
		: mockFont(new IDWriteFontMock),
		font(SlimDX::DirectWrite::Font::FromPointer(System::IntPtr(mockFont)))
	{
	}
	~MockedFont()
	{
		delete font;
		font = nullptr;
		delete mockFont;
		mockFont = 0;
	}
	property IDWriteFontMock &Mock
	{
		IDWriteFontMock &get() { return *mockFont; }
	}
	property SlimDX::DirectWrite::Font ^Font
	{
		SlimDX::DirectWrite::Font ^get() { return font; }
	}

private:
	IDWriteFontMock *mockFont;
	SlimDX::DirectWrite::Font ^font;
};

class FontTest : public testing::Test
{
protected:
	virtual void TearDown()
	{
		ASSERT_EQ(0, ObjectTable::Objects->Count);
	}
};

#define FONT_TEST(name_) TEST_F(FontTest, name_)

FONT_TEST(CreateFontFaceErrorThrowsSlimDXException)
{
	MockedFont font;
	EXPECT_CALL(font.Mock, CreateFontFace(NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(static_cast<IDWriteFontFace *>(0)),
						Return(E_UNEXPECTED)));
	SlimDX::Configuration::ThrowOnError = true;
	FontFace ^face;
	ASSERT_MANAGED_THROW( face = font.Font->CreateFontFace(), SlimDXException );
}

FONT_TEST(CreateFontFaceOK)
{
	MockedFont font;
	IDWriteFontFaceMock mockFace;
	EXPECT_CALL(font.Mock, CreateFontFace(NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(&mockFace),
						Return(S_OK)));
	FontFace ^face = font.Font->CreateFontFace();
	ASSERT_TRUE( nullptr != face );
	ASSERT_EQ( &mockFace, face->InternalPointer );
	delete face;
}
