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

#include "IDWriteInlineObjectMock.h"
#include "IDWriteTextLayoutMock.h"
#include "IDWriteTextRendererMock.h"
#include "TextLayoutTest.h"

using namespace testing;
using namespace System;
using namespace SlimDX;
using namespace SlimDX::DirectWrite;

ref class MockedTextLayout
{
public:
	MockedTextLayout()
		: mockLayout(new IDWriteTextLayoutMock),
		layout(TextLayout::FromPointer(System::IntPtr(mockLayout))),
		throwOnError(SlimDX::Configuration::ThrowOnError)
	{
	}
	~MockedTextLayout()
	{
		SlimDX::Configuration::ThrowOnError = throwOnError;
		delete layout;
		layout = nullptr;
		delete mockLayout;
		mockLayout = 0;
	}
	property IDWriteTextLayoutMock &Mock
	{
		IDWriteTextLayoutMock &get() { return *mockLayout; }
	}
	property TextLayout ^Layout
	{
		TextLayout ^get() { return layout; }
	}

private:
	bool const throwOnError;
	IDWriteTextLayoutMock *mockLayout;
	TextLayout ^layout;
};

TEXT_LAYOUT_TEST(DetermineMinWidth)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, DetermineMinWidth(NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(14.5f), Return(S_OK)));
	float minWidth = -1.0f;
	ASSERT_EQ(14.5f, layout.Layout->DetermineMinWidth());
}

static DWRITE_HIT_TEST_METRICS ExpectedHitTestMetrics()
{
	DWRITE_HIT_TEST_METRICS expectedMetrics;
	expectedMetrics.textPosition = 0;
	expectedMetrics.length = 1;
	expectedMetrics.left = 0.15f;
	expectedMetrics.top = 0.245f;
	expectedMetrics.width = 45.8f;
	expectedMetrics.height = 998.5f;
	expectedMetrics.bidiLevel = 23;
	expectedMetrics.isText = TRUE;
	expectedMetrics.isTrimmed = TRUE;
	return expectedMetrics;
}

static void AssertHitTestMetricsMatchExpected(HitTestMetrics% actual)
{
	DWRITE_HIT_TEST_METRICS expected = ExpectedHitTestMetrics();
	ASSERT_EQ(expected.textPosition, actual.TextPosition);
	ASSERT_EQ(expected.length, actual.Length);
	ASSERT_EQ(expected.left, actual.Left);
	ASSERT_EQ(expected.top, actual.Top);
	ASSERT_EQ(expected.width, actual.Width);
	ASSERT_EQ(expected.height, actual.Height);
	ASSERT_EQ(expected.bidiLevel, actual.BidiLevel);
	ASSERT_EQ(expected.isText == TRUE, actual.IsText);
	ASSERT_EQ(expected.isTrimmed == TRUE, actual.IsTrimmed);
}

TEXT_LAYOUT_TEST(HitTestPoint)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, HitTestPoint(10.0f, 12.0f, NotNull(), NotNull(), NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<2>(false),
						SetArgumentPointee<3>(false),
						SetArgumentPointee<4>(ExpectedHitTestMetrics()),
						Return(S_OK)));
	bool isTrailingHit = true;
	bool isInside = true;
	HitTestMetrics metrics = layout.Layout->HitTestPoint(10.0f, 12.0f, isTrailingHit, isInside);
	AssertLastResultSucceeded();
	ASSERT_FALSE(isTrailingHit);
	ASSERT_FALSE(isInside);
	AssertHitTestMetricsMatchExpected(metrics);
}

TEXT_LAYOUT_TEST(HitTestTextPosition)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, HitTestTextPosition(12U, FALSE, NotNull(), NotNull(), NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<2>(12.5f),
						SetArgumentPointee<3>(13.5f),
						SetArgumentPointee<4>(ExpectedHitTestMetrics()),
						Return(S_OK)));
	float x, y;
	HitTestMetrics metrics = layout.Layout->HitTestTextPosition(12U, false, x, y);
	AssertLastResultSucceeded();
	ASSERT_EQ(12.5f, x);
	ASSERT_EQ(13.5f, y);
	AssertHitTestMetricsMatchExpected(metrics);
}

TEXT_LAYOUT_TEST(HitTestTextRange)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, HitTestTextRange(12U, 4U, 10.0f, 12.0f, 0, 0, NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<6>(1U), Return(E_NOT_SUFFICIENT_BUFFER)));
	EXPECT_CALL(layout.Mock, HitTestTextRange(12U, 4U, 10.0f, 12.0f, NotNull(), 1U, NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<6>(1U),
						SetArgumentPointee<4>(ExpectedHitTestMetrics()),
						Return(S_OK)));
	array<HitTestMetrics> ^metrics = layout.Layout->HitTestTextRange(12U, 4U, 10.0f, 12.0f);
	AssertLastResultSucceeded();
	ASSERT_EQ(1, metrics->Length);
	AssertHitTestMetricsMatchExpected(metrics[0]);
}

static bool operator==(DWRITE_TEXT_RANGE const &lhs, DWRITE_TEXT_RANGE const &rhs)
{
	return lhs.startPosition == rhs.startPosition
		&& lhs.length == rhs.length;
}

static TextRange ExpectedManagedTextRange()
{
	DWRITE_TEXT_RANGE expected = ExpectedTextRange();
	TextRange managed;
	managed.StartPosition = expected.startPosition;
	managed.Length = expected.length;
	return managed;
}

TEXT_LAYOUT_TEST(SetFontFamilyName)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, SetFontFamilyName(NotNull(), ExpectedTextRange()))
		.Times(1)
		.WillOnce(Return(S_OK));
	
	ASSERT_TRUE(layout.Layout->SetFontFamilyName(gcnew String("Slartibartfast!"), ExpectedManagedTextRange()).IsSuccess);
}

TEXT_LAYOUT_TEST(SetFontStyle)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, SetFontStyle(DWRITE_FONT_STYLE_ITALIC, ExpectedTextRange()))
		.Times(1)
		.WillOnce(Return(S_OK));
	ASSERT_TRUE(layout.Layout->SetFontStyle(FontStyle::Italic, ExpectedManagedTextRange()).IsSuccess);
}

TEXT_LAYOUT_TEST(SetFontStretch)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, SetFontStretch(DWRITE_FONT_STRETCH_ULTRA_EXPANDED, ExpectedTextRange()))
		.Times(1)
		.WillOnce(Return(S_OK));
	ASSERT_TRUE(layout.Layout->SetFontStretch(FontStretch::UltraExpanded, ExpectedManagedTextRange()).IsSuccess);
}

TEXT_LAYOUT_TEST(SetLocaleName)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, SetLocaleName(NotNull(), ExpectedTextRange()))
		.Times(1)
		.WillOnce(Return(S_OK));
	ASSERT_TRUE(layout.Layout->SetLocaleName(gcnew String("Slartibartfast"), ExpectedManagedTextRange()).IsSuccess);
}

TEXT_LAYOUT_TEST(SetStrikethrough)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, SetStrikethrough(TRUE, ExpectedTextRange()))
		.Times(1)
		.WillOnce(Return(S_OK));
	ASSERT_TRUE(layout.Layout->SetStrikethrough(true, ExpectedManagedTextRange()).IsSuccess);
}

TEXT_LAYOUT_TEST(SetInlineObject)
{
	MockedTextLayout layout;
	IDWriteInlineObjectMock mockInlineObject;
	EXPECT_CALL(layout.Mock, SetInlineObject(&mockInlineObject, ExpectedTextRange()))
		.Times(1)
		.WillOnce(Return(S_OK));
	InlineObject ^obj = InlineObject::FromPointer(&mockInlineObject);
	ASSERT_TRUE(layout.Layout->SetInlineObject(obj, ExpectedManagedTextRange()).IsSuccess);
	delete obj;
}

TEXT_LAYOUT_TEST(SetDrawingEffect)
{
	MockedTextLayout layout;
	EXPECT_CALL(layout.Mock, SetDrawingEffect(0, ExpectedTextRange()))
		.Times(1)
		.WillOnce(Return(S_OK));
	ASSERT_TRUE(layout.Layout->SetDrawingEffect(nullptr, ExpectedManagedTextRange()).IsSuccess);
}

TEXT_LAYOUT_TEST(Draw)
{
	MockedTextLayout layout;
	IDWriteTextRendererMock mockRenderer;
	EXPECT_CALL(layout.Mock, Draw(0, &mockRenderer, 12.5f, 33.0f))
		.Times(1)
		.WillOnce(Return(S_OK));
	TextRenderer ^renderer = TextRenderer::FromPointer(&mockRenderer);
	ASSERT_TRUE(layout.Layout->Draw(IntPtr(0), renderer, 12.5f, 33.0f).IsSuccess);
	delete renderer;
}
