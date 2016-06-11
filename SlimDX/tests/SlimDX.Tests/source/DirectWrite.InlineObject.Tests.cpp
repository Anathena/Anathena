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

#include "IDWriteInlineObjectMock.h"
#include "IDWriteTextRendererMock.h"
#include "SlimDXTest.h"

using namespace testing;
using namespace SlimDX;
using namespace SlimDX::DirectWrite;
using namespace System;

class InlineObjectTest : public SlimDXTest
{
};

ref class MockedInlineObject
{
public:
	MockedInlineObject()
		: mockInlineObject(new IDWriteInlineObjectMock),
		obj(SlimDX::DirectWrite::InlineObject::FromPointer(System::IntPtr(mockInlineObject))),
		throwOnError(SlimDX::Configuration::ThrowOnError)
	{
	}
	~MockedInlineObject()
	{
		SlimDX::Configuration::ThrowOnError = throwOnError;
		delete obj;
		obj = nullptr;
		delete mockInlineObject;
		mockInlineObject = 0;
	}
	property IDWriteInlineObjectMock &Mock
	{
		IDWriteInlineObjectMock &get() { return *mockInlineObject; }
	}
	property SlimDX::DirectWrite::InlineObject ^InlineObject
	{
		SlimDX::DirectWrite::InlineObject ^get() { return obj; }
	}

private:
	bool const throwOnError;
	IDWriteInlineObjectMock *mockInlineObject;
	SlimDX::DirectWrite::InlineObject ^obj;
};

static DWRITE_INLINE_OBJECT_METRICS ExpectedInlineObjectMetrics()
{
	DWRITE_INLINE_OBJECT_METRICS expected;
	expected.width = 24;
	expected.height = 36;
	expected.baseline = 12;
	expected.supportsSideways = TRUE;
	return expected;
}

static void AssertInlineObjectMetricsMatchExpected(InlineObjectMetrics metrics)
{
	DWRITE_INLINE_OBJECT_METRICS expected = ExpectedInlineObjectMetrics();
	ASSERT_EQ(expected.width, metrics.Width);
	ASSERT_EQ(expected.height, metrics.Height);
	ASSERT_EQ(expected.baseline, metrics.Baseline);
	ASSERT_EQ(expected.supportsSideways == TRUE, metrics.SupportsSideways);
}

#define INLINE_OBJECT_TEST(name_) TEST_F(InlineObjectTest, name_)

INLINE_OBJECT_TEST(Metrics)
{
	MockedInlineObject obj;
	EXPECT_CALL(obj.Mock, GetMetrics(NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(ExpectedInlineObjectMetrics()), Return(S_OK)));
	InlineObjectMetrics metrics = obj.InlineObject->Metrics;
	AssertLastResultSucceeded();
	AssertInlineObjectMetricsMatchExpected(metrics);
}

INLINE_OBJECT_TEST(MetricsReturnsZeroOnFailure)
{
	MockedInlineObject obj;
	EXPECT_CALL(obj.Mock, GetMetrics(NotNull()))
		.Times(1)
		.WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	InlineObjectMetrics metrics = obj.InlineObject->Metrics;
	AssertLastResultFailed();
	ASSERT_EQ(0, metrics.Width);
	ASSERT_EQ(0, metrics.Height);
	ASSERT_EQ(0, metrics.Baseline);
	ASSERT_FALSE(metrics.SupportsSideways);
}

static std::ostream &operator<<(std::ostream &stream, BreakCondition bc)
{
	return stream << bc.ToString();
}

INLINE_OBJECT_TEST(GetBreakConditions)
{
	MockedInlineObject obj;
	EXPECT_CALL(obj.Mock, GetBreakConditions(NotNull(), NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(DWRITE_BREAK_CONDITION_CAN_BREAK),
						SetArgumentPointee<1>(DWRITE_BREAK_CONDITION_MUST_BREAK),
						Return(S_OK)));
	BreakCondition before, after;
	ASSERT_TRUE(obj.InlineObject->GetBreakConditions(before, after).IsSuccess);
	ASSERT_EQ(BreakCondition::CanBreak, before);
	ASSERT_EQ(BreakCondition::MustBreak, after);
}

static DWRITE_OVERHANG_METRICS ExpectedOverhangMetrics()
{
	DWRITE_OVERHANG_METRICS expected;
	expected.left = 40;
	expected.right = 123;
	expected.top = 50;
	expected.bottom = 1256;
	return expected;
}

static void AssertOverhangMetricsMatchExpected(OverhangMetrics metrics)
{
	DWRITE_OVERHANG_METRICS expected = ExpectedOverhangMetrics();
	ASSERT_EQ(expected.left, metrics.Left);
	ASSERT_EQ(expected.right, metrics.Right);
	ASSERT_EQ(expected.top, metrics.Top);
	ASSERT_EQ(expected.bottom, metrics.Bottom);
}

INLINE_OBJECT_TEST(OverhangMetrics)
{
	MockedInlineObject obj;
	EXPECT_CALL(obj.Mock, GetOverhangMetrics(NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<0>(ExpectedOverhangMetrics()), Return(S_OK)));
	OverhangMetrics metrics = obj.InlineObject->OverhangMetrics;
	AssertLastResultSucceeded();
	AssertOverhangMetricsMatchExpected(metrics);
}

INLINE_OBJECT_TEST(OverhangMetricsReturnsZeroOnFailure)
{
	MockedInlineObject obj;
	EXPECT_CALL(obj.Mock, GetOverhangMetrics(NotNull()))
		.Times(1)
		.WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	OverhangMetrics metrics = obj.InlineObject->OverhangMetrics;
	AssertLastResultFailed();
	ASSERT_EQ(0, metrics.Left);
	ASSERT_EQ(0, metrics.Right);
	ASSERT_EQ(0, metrics.Top);
	ASSERT_FALSE(metrics.Bottom);
}

INLINE_OBJECT_TEST(Draw)
{
	MockedInlineObject obj;
	IDWriteTextRendererMock mockRenderer;
	EXPECT_CALL(obj.Mock, Draw(0, &mockRenderer, 12.5f, 16.2f, TRUE, TRUE, 0))
		.Times(1)
		.WillOnce(Return(S_OK));
	TextRenderer ^renderer = TextRenderer::FromPointer(&mockRenderer);
	ASSERT_TRUE(obj.InlineObject->Draw(IntPtr(0), renderer, 12.5f, 16.2f, true, true, nullptr).IsSuccess);
	AssertLastResultSucceeded();
	delete renderer;
}
