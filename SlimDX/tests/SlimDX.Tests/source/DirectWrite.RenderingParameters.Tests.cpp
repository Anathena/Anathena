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
#include "SlimDXTest.h"

using namespace testing;
using namespace SlimDX::DirectWrite;

class IDWriteRenderingParamsMock : public IDWriteRenderingParams
{
public:
	MOCK_IUNKNOWN;

	// IDWriteRenderingParams
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetGamma, FLOAT() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetEnhancedContrast, FLOAT() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetClearTypeLevel, FLOAT() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetPixelGeometry, DWRITE_PIXEL_GEOMETRY() );
	MOCK_METHOD0_WITH_CALLTYPE( STDMETHODCALLTYPE, GetRenderingMode, DWRITE_RENDERING_MODE() );
};

ref class MockedRenderingParameters
{
public:
	MockedRenderingParameters()
		: mockParameters(new IDWriteRenderingParamsMock),
		parameters(RenderingParameters::FromPointer(System::IntPtr(mockParameters))),
		throwOnError(SlimDX::Configuration::ThrowOnError)
	{
	}
	~MockedRenderingParameters()
	{
		SlimDX::Configuration::ThrowOnError = throwOnError;
		delete parameters;
		parameters = nullptr;
		delete mockParameters;
		mockParameters = 0;
	}
	property IDWriteRenderingParamsMock &Mock
	{
		IDWriteRenderingParamsMock &get() { return *mockParameters; }
	}
	property RenderingParameters ^Parameters
	{
		RenderingParameters ^get() { return parameters; }
	}

private:
	bool const throwOnError;
	IDWriteRenderingParamsMock *mockParameters;
	RenderingParameters ^parameters;
};

class RenderingParametersTest : public SlimDXTest
{
};

#define PARAMS_TEST(name_) TEST_F(RenderingParametersTest, name_)

static std::ostream &operator<<(std::ostream &stream, PixelGeometry val)
{
	return stream << val.ToString();
}

static std::ostream &operator<<(std::ostream &stream, RenderingMode val)
{
	return stream << val.ToString();
}

PARAMS_TEST(Gamma)
{
	MockedRenderingParameters params;
	EXPECT_CALL(params.Mock, GetGamma())
		.Times(1).WillOnce(Return(2.22f));
	ASSERT_EQ(2.22f, params.Parameters->Gamma);
}

PARAMS_TEST(EnhancedContrast)
{
	MockedRenderingParameters params;
	EXPECT_CALL(params.Mock, GetEnhancedContrast())
		.Times(1).WillOnce(Return(2.22f));
	ASSERT_EQ(2.22f, params.Parameters->EnhancedContrast);
}

PARAMS_TEST(ClearTypeLevel)
{
	MockedRenderingParameters params;
	EXPECT_CALL(params.Mock, GetClearTypeLevel())
		.Times(1).WillOnce(Return(2.22f));
	ASSERT_EQ(2.22f, params.Parameters->ClearTypeLevel);
}

PARAMS_TEST(PixelGeometry)
{
	MockedRenderingParameters params;
	EXPECT_CALL(params.Mock, GetPixelGeometry())
		.Times(1).WillOnce(Return(DWRITE_PIXEL_GEOMETRY_FLAT));
	ASSERT_EQ(PixelGeometry::Flat, params.Parameters->PixelGeometry);
}

PARAMS_TEST(RenderingMode)
{
	MockedRenderingParameters params;
	EXPECT_CALL(params.Mock, GetRenderingMode())
		.Times(1).WillOnce(Return(DWRITE_RENDERING_MODE_CLEARTYPE_NATURAL));
	ASSERT_EQ(RenderingMode::ClearTypeNatural, params.Parameters->RenderingMode);
}
