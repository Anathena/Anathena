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
#include <ostream>

#include <dwrite.h>

#include "CommonMocks.h"
#include "IDWriteFontCollectionMock.h"
#include "IDWriteInlineObjectMock.h"
#include "SlimDXTest.h"

using namespace testing;
using namespace SlimDX::DirectWrite;
using namespace System;

class IDWriteTextFormatMock : public IDWriteTextFormat
{
public:
	MOCK_IUNKNOWN;

	// IDWriteTextFormat
	STDMETHOD(SetTextAlignment)(DWRITE_TEXT_ALIGNMENT textAlignment) { return E_NOTIMPL; }
	STDMETHOD(SetParagraphAlignment)(DWRITE_PARAGRAPH_ALIGNMENT paragraphAlignment) { return E_NOTIMPL; }
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, SetWordWrapping, HRESULT(DWRITE_WORD_WRAPPING) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, SetReadingDirection, HRESULT(DWRITE_READING_DIRECTION) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, SetFlowDirection, HRESULT(DWRITE_FLOW_DIRECTION) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, SetIncrementalTabStop, HRESULT(FLOAT) );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, SetTrimming, HRESULT(DWRITE_TRIMMING const*, IDWriteInlineObject*) );
	MOCK_METHOD3_WITH_CALLTYPE( STDMETHODCALLTYPE, SetLineSpacing, HRESULT(DWRITE_LINE_SPACING_METHOD, FLOAT, FLOAT) );
	STDMETHOD_(DWRITE_TEXT_ALIGNMENT, GetTextAlignment)() { return DWRITE_TEXT_ALIGNMENT(-1); }
	STDMETHOD_(DWRITE_PARAGRAPH_ALIGNMENT, GetParagraphAlignment)() { return DWRITE_PARAGRAPH_ALIGNMENT(-1); }
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetWordWrapping, DWRITE_WORD_WRAPPING() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetReadingDirection, DWRITE_READING_DIRECTION() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFlowDirection, DWRITE_FLOW_DIRECTION() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetIncrementalTabStop, FLOAT() );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetTrimming, HRESULT(DWRITE_TRIMMING*, IDWriteInlineObject**) );
	MOCK_METHOD3_WITH_CALLTYPE( STDMETHODCALLTYPE, GetLineSpacing, HRESULT(DWRITE_LINE_SPACING_METHOD*, FLOAT*, FLOAT*) );
	MOCK_METHOD1_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFontCollection, HRESULT(IDWriteFontCollection**) );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFontFamilyNameLength, UINT32() );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFontFamilyName, HRESULT(WCHAR*, UINT32) );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFontWeight, DWRITE_FONT_WEIGHT() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFontStyle, DWRITE_FONT_STYLE() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFontStretch, DWRITE_FONT_STRETCH() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetFontSize, FLOAT() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetLocaleNameLength, UINT32() );
	MOCK_METHOD2_WITH_CALLTYPE( STDMETHODCALLTYPE, GetLocaleName, HRESULT(WCHAR*, UINT32) );
};

ref class MockedTextFormat
{
public:
	MockedTextFormat()
		: mockFormat(new IDWriteTextFormatMock),
		format(TextFormat::FromPointer(System::IntPtr(mockFormat))),
		throwOnError(SlimDX::Configuration::ThrowOnError)
	{
	}
	~MockedTextFormat()
	{
		SlimDX::Configuration::ThrowOnError = throwOnError;
		delete format;
		format = nullptr;
		delete mockFormat;
		mockFormat = 0;
	}
	property IDWriteTextFormatMock &Mock
	{
		IDWriteTextFormatMock &get() { return *mockFormat; }
	}
	property TextFormat ^Format
	{
		TextFormat ^get() { return format; }
	}

private:
	bool const throwOnError;
	IDWriteTextFormatMock *mockFormat;
	TextFormat ^format;
};

class TextFormatTest : public SlimDXTest
{
};

static std::ostream &operator<<(std::ostream &stream, FlowDirection dir)
{
	return stream << dir.ToString();
}

static std::ostream &operator<<(std::ostream &stream, ReadingDirection dir)
{
	return stream << dir.ToString();
}

static std::ostream &operator<<(std::ostream &stream, WordWrapping wrap)
{
	return stream << wrap.ToString();
}

#define TEXT_FORMAT_TEST(name_) TEST_F(TextFormatTest, name_)

TEXT_FORMAT_TEST(GetFlowDirection)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetFlowDirection())
		.Times(1)
		.WillOnce(Return(DWRITE_FLOW_DIRECTION_TOP_TO_BOTTOM));
	ASSERT_EQ(FlowDirection::TopToBottom, format.Format->FlowDirection);
	AssertLastResultSucceeded();
}

TEXT_FORMAT_TEST(SetFlowDirection)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, SetFlowDirection(DWRITE_FLOW_DIRECTION_TOP_TO_BOTTOM))
		.Times(1)
		.WillOnce(Return(S_OK));
	format.Format->FlowDirection = FlowDirection::TopToBottom;
	AssertLastResultSucceeded();
}

TEXT_FORMAT_TEST(GetFontCollection)
{
	MockedTextFormat format;
	IDWriteFontCollectionMock mockCollection;
	EXPECT_CALL(format.Mock, GetFontCollection(NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(&mockCollection), Return(S_OK)));
	FontCollection ^collection = format.Format->FontCollection;
	AssertLastResultSucceeded();
	ASSERT_TRUE(collection != nullptr);
	ASSERT_EQ(&mockCollection, collection->InternalPointer);
	delete collection;
}

TEXT_FORMAT_TEST(GetFontCollectionFailureReturnsNullPtr)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetFontCollection(NotNull()))
		.Times(1)
		.WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	FontCollection ^collection = format.Format->FontCollection;
	AssertLastResultFailed();
	ASSERT_TRUE(collection == nullptr);
}

TEXT_FORMAT_TEST(GetFontFamilyName)
{
	MockedTextFormat format;
	WCHAR const fakeName[] = L"Slartibartfast";
	UINT32 const fakeNameLength = NUM_OF(fakeName);
	EXPECT_CALL(format.Mock, GetFontFamilyNameLength())
		.Times(1)
		.WillOnce(Return(fakeNameLength - 1));
	EXPECT_CALL(format.Mock, GetFontFamilyName(NotNull(), Gt(0U)))
		.Times(1)
		.WillOnce(DoAll(SetArrayArgument<0>(&fakeName[0], &fakeName[fakeNameLength]),
						Return(S_OK)));
	String ^name = format.Format->FontFamilyName;
	AssertLastResultSucceeded();
	ASSERT_TRUE(gcnew String(fakeName) == name);
}

TEXT_FORMAT_TEST(GetFontFamilyNameFailureReturnsEmptyString)
{
	MockedTextFormat format;
	WCHAR const fakeName[] = L"Slartibartfast";
	UINT32 const fakeNameLength = NUM_OF(fakeName);
	EXPECT_CALL(format.Mock, GetFontFamilyNameLength())
		.Times(1)
		.WillOnce(Return(fakeNameLength - 1));
	EXPECT_CALL(format.Mock, GetFontFamilyName(NotNull(), Gt(0U)))
		.Times(1)
		.WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	String ^name = format.Format->FontFamilyName;
	AssertLastResultFailed();
	ASSERT_TRUE(String::Empty == name);
}

TEXT_FORMAT_TEST(GetFontSize)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetFontSize())
		.Times(1)
		.WillOnce(Return(45.4f));
	ASSERT_EQ(45.4f, format.Format->FontSize);
}

TEXT_FORMAT_TEST(GetFontStretch)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetFontStretch())
		.Times(1)
		.WillOnce(Return(DWRITE_FONT_STRETCH_ULTRA_EXPANDED));
	ASSERT_EQ(FontStretch::UltraExpanded, format.Format->FontStretch);
}

TEXT_FORMAT_TEST(GetFontStyle)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetFontStyle())
		.Times(1)
		.WillOnce(Return(DWRITE_FONT_STYLE_ITALIC));
	ASSERT_EQ(FontStyle::Italic, format.Format->FontStyle);
}

TEXT_FORMAT_TEST(GetFontWeight)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetFontWeight())
		.Times(1)
		.WillOnce(Return(DWRITE_FONT_WEIGHT_ULTRA_BOLD));
	ASSERT_EQ(FontWeight::UltraBold, format.Format->FontWeight);
}

TEXT_FORMAT_TEST(GetIncrementalTabStop)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetIncrementalTabStop())
		.Times(1)
		.WillOnce(Return(45.5f));
	ASSERT_EQ(45.5f, format.Format->IncrementalTabStop);
}

TEXT_FORMAT_TEST(SetIncrementalTabStop)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, SetIncrementalTabStop(45.5f))
		.Times(1)
		.WillOnce(Return(S_OK));
	format.Format->IncrementalTabStop = 45.5f;
	AssertLastResultSucceeded();
}

static std::ostream &operator<<(std::ostream &stream, LineSpacingMethod method)
{
	return stream << method.ToString();
}

TEXT_FORMAT_TEST(GetLineSpacing)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetLineSpacing(NotNull(), NotNull(), NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(DWRITE_LINE_SPACING_METHOD_UNIFORM),
						SetArgumentPointee<1>(12.5f),
						SetArgumentPointee<2>(14.8f),
						Return(S_OK)));
	LineSpacingMethod method;
	float lineSpacing, baseline;
	ASSERT_TRUE(format.Format->GetLineSpacing(method, lineSpacing, baseline).IsSuccess);
	AssertLastResultSucceeded();
	ASSERT_EQ(LineSpacingMethod::Uniform, method);
	ASSERT_EQ(12.5f, lineSpacing);
	ASSERT_EQ(14.8f, baseline);
}

TEXT_FORMAT_TEST(SetLineSpacing)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, SetLineSpacing(DWRITE_LINE_SPACING_METHOD_UNIFORM, 12.5f, 14.8f))
		.Times(1)
		.WillOnce(Return(S_OK));
	ASSERT_TRUE(format.Format->SetLineSpacing(LineSpacingMethod::Uniform, 12.5f, 14.8f).IsSuccess);
	AssertLastResultSucceeded();
}

TEXT_FORMAT_TEST(LocaleName)
{
	MockedTextFormat format;
	WCHAR const fakeName[] = L"Slartibartfast";
	UINT32 const nameLength = NUM_OF(fakeName);
	EXPECT_CALL(format.Mock, GetLocaleNameLength())
		.Times(1)
		.WillOnce(Return(nameLength - 1));
	EXPECT_CALL(format.Mock, GetLocaleName(NotNull(), Gt(0U)))
		.Times(1)
		.WillOnce(DoAll(SetArrayArgument<0>(&fakeName[0], &fakeName[nameLength]),
						Return(S_OK)));
	String ^name = format.Format->LocaleName;
	AssertLastResultSucceeded();
	ASSERT_TRUE(gcnew String(fakeName) == name);
}

TEXT_FORMAT_TEST(LocaleNameFailureReturnsEmptyString)
{
	MockedTextFormat format;
	WCHAR const fakeName[] = L"Slartibartfast";
	UINT32 const nameLength = NUM_OF(fakeName);
	EXPECT_CALL(format.Mock, GetLocaleNameLength())
		.Times(1)
		.WillOnce(Return(nameLength - 1));
	EXPECT_CALL(format.Mock, GetLocaleName(NotNull(), Gt(0U)))
		.Times(1)
		.WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	String ^name = format.Format->LocaleName;
	AssertLastResultFailed();
	ASSERT_TRUE(String::Empty == name);
}

TEXT_FORMAT_TEST(GetReadingDirection)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetReadingDirection())
		.Times(1)
		.WillOnce(Return(DWRITE_READING_DIRECTION_RIGHT_TO_LEFT));
	ASSERT_EQ(ReadingDirection::RightToLeft, format.Format->ReadingDirection);
}

TEXT_FORMAT_TEST(SetReadingDirection)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, SetReadingDirection(DWRITE_READING_DIRECTION_RIGHT_TO_LEFT))
		.Times(1)
		.WillOnce(Return(S_OK));
	format.Format->ReadingDirection = ReadingDirection::RightToLeft;
	AssertLastResultSucceeded();
}

static DWRITE_TRIMMING ExpectedTrimming()
{
	DWRITE_TRIMMING expected;
	expected.granularity = DWRITE_TRIMMING_GRANULARITY_WORD;
	expected.delimiter = int('+');
	expected.delimiterCount = 14;
	return expected;
}

static void AssertTrimmingMatchesExpected(Trimming trimming)
{
	DWRITE_TRIMMING expected = ExpectedTrimming();
	ASSERT_EQ(expected.granularity, static_cast<DWRITE_TRIMMING_GRANULARITY>(trimming.Granularity));
	ASSERT_EQ(expected.delimiter, trimming.Delimiter);
	ASSERT_EQ(expected.delimiterCount, trimming.DelimiterCount);
}

#define TEXT_FORMAT_TEST(name_) TEST_F(TextFormatTest, name_)

TEXT_FORMAT_TEST(GetTrimmingNoSign)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetTrimming(NotNull(), 0))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(ExpectedTrimming()),
						Return(S_OK)));
	Trimming trimming = format.Format->GetTrimming();
	AssertLastResultSucceeded();
	AssertTrimmingMatchesExpected(trimming);
}

TEXT_FORMAT_TEST(GetTrimmingFailureReturnsZero)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetTrimming(NotNull(), 0))
		.Times(1)
		.WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	Trimming trimming = format.Format->GetTrimming();
	AssertLastResultFailed();
	ASSERT_EQ(DWRITE_TRIMMING_GRANULARITY(0), static_cast<DWRITE_TRIMMING_GRANULARITY>(trimming.Granularity));
	ASSERT_EQ(0, trimming.Delimiter);
	ASSERT_EQ(0, trimming.DelimiterCount);
}

TEXT_FORMAT_TEST(GetTrimmingWithSign)
{
	MockedTextFormat format;
	IDWriteInlineObjectMock mockSign;
	EXPECT_CALL(format.Mock, GetTrimming(NotNull(), NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(ExpectedTrimming()),
						SetArgumentPointee<1>(&mockSign),
						Return(S_OK)));
	InlineObject ^sign = nullptr;
	Trimming trimming = format.Format->GetTrimming(sign);
	AssertLastResultSucceeded();
	ASSERT_TRUE(sign != nullptr);
	ASSERT_EQ(&mockSign, sign->InternalPointer);
	AssertTrimmingMatchesExpected(trimming);
	delete sign;
}

TEXT_FORMAT_TEST(GetWordWrapping)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, GetWordWrapping())
		.Times(1)
		.WillOnce(Return(DWRITE_WORD_WRAPPING_WRAP));
	ASSERT_EQ(WordWrapping::Wrap, format.Format->WordWrapping);
}

TEXT_FORMAT_TEST(SetWordWrapping)
{
	MockedTextFormat format;
	EXPECT_CALL(format.Mock, SetWordWrapping(DWRITE_WORD_WRAPPING_WRAP))
		.Times(1)
		.WillOnce(Return(S_OK));
	format.Format->WordWrapping = WordWrapping::Wrap;
	AssertLastResultSucceeded();
}
