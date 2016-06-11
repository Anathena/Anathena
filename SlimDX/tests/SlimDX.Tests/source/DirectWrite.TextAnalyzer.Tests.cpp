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
using namespace SlimDX;
using namespace SlimDX::DirectWrite;

class IDWriteTextAnalyzerMock : public IDWriteTextAnalyzer
{
public:
	MOCK_IUNKNOWN;

	MOCK_METHOD4_WITH_CALLTYPE(STDMETHODCALLTYPE, AnalyzeScript, HRESULT(IDWriteTextAnalysisSource*, UINT32, UINT32, IDWriteTextAnalysisSink*));
	MOCK_METHOD4_WITH_CALLTYPE(STDMETHODCALLTYPE, AnalyzeBidi, HRESULT(IDWriteTextAnalysisSource*, UINT32, UINT32, IDWriteTextAnalysisSink*));
	STDMETHOD(AnalyzeNumberSubstitution)(IDWriteTextAnalysisSource* analysisSource, UINT32 textPosition, UINT32 textLength, IDWriteTextAnalysisSink* analysisSink) { return E_NOTIMPL; }
	STDMETHOD(AnalyzeLineBreakpoints)(IDWriteTextAnalysisSource* analysisSource, UINT32 textPosition, UINT32 textLength, IDWriteTextAnalysisSink* analysisSink) { return E_NOTIMPL; }
	STDMETHOD(GetGlyphs)(WCHAR const* textString, UINT32 textLength, IDWriteFontFace* fontFace, BOOL isSideways, BOOL isRightToLeft,
		DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis, WCHAR const* localeName, IDWriteNumberSubstitution* numberSubstitution,
		DWRITE_TYPOGRAPHIC_FEATURES const** features, UINT32 const* featureRangeLengths, UINT32 featureRanges,
		UINT32 maxGlyphCount, UINT16* clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES* textProps, UINT16* glyphIndices,
		DWRITE_SHAPING_GLYPH_PROPERTIES* glyphProps, UINT32* actualGlyphCount)
	{ return E_NOTIMPL; }
	STDMETHOD(GetGlyphPlacements)(WCHAR const* textString, UINT16 const* clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES* textProps, UINT32 textLength,
		UINT16 const* glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES const* glyphProps, UINT32 glyphCount, IDWriteFontFace * fontFace,
		FLOAT fontEmSize, BOOL isSideways, BOOL isRightToLeft, DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis, WCHAR const* localeName,
		DWRITE_TYPOGRAPHIC_FEATURES const** features, UINT32 const* featureRangeLengths, UINT32 featureRanges, FLOAT* glyphAdvances, DWRITE_GLYPH_OFFSET* glyphOffsets)
	{ return E_NOTIMPL; }
	STDMETHOD(GetGdiCompatibleGlyphPlacements)(WCHAR const* textString, UINT16 const* clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
		UINT32 textLength, UINT16 const* glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES const* glyphProps, UINT32 glyphCount, IDWriteFontFace * fontFace,
		FLOAT fontEmSize, FLOAT pixelsPerDip, DWRITE_MATRIX const* transform, BOOL useGdiNatural, BOOL isSideways, BOOL isRightToLeft,
		DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis, WCHAR const* localeName, DWRITE_TYPOGRAPHIC_FEATURES const** features,
		UINT32 const* featureRangeLengths, UINT32 featureRanges, FLOAT* glyphAdvances, DWRITE_GLYPH_OFFSET* glyphOffsets)
	{ return E_NOTIMPL; }
};

ref class MockedTextAnalyzer
{
public:
	MockedTextAnalyzer()
		: mockAnalyzer(new IDWriteTextAnalyzerMock),
		analyzer(TextAnalyzer::FromPointer(System::IntPtr(mockAnalyzer))),
		throwOnError(SlimDX::Configuration::ThrowOnError)
	{
	}
	~MockedTextAnalyzer()
	{
		SlimDX::Configuration::ThrowOnError = throwOnError;
		delete analyzer;
		analyzer = nullptr;
		delete mockAnalyzer;
		mockAnalyzer = 0;
	}
	property IDWriteTextAnalyzerMock &Mock
	{
		IDWriteTextAnalyzerMock &get() { return *mockAnalyzer; }
	}
	property TextAnalyzer ^Analyzer
	{
		TextAnalyzer ^get() { return analyzer; }
	}

private:
	bool const throwOnError;
	IDWriteTextAnalyzerMock *mockAnalyzer;
	TextAnalyzer ^analyzer;
};

class TextAnalyzerTest : public SlimDXTest
{
};

class IDWriteTextAnalysisSourceFake : public IDWriteTextAnalysisSource
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD(GetTextAtPosition)(UINT32 textPosition, WCHAR const** textString, UINT32* textLength) { return E_NOTIMPL; }
	STDMETHOD(GetTextBeforePosition)(UINT32 textPosition, WCHAR const** textString, UINT32* textLength) { return E_NOTIMPL; }
	STDMETHOD_(DWRITE_READING_DIRECTION, GetParagraphReadingDirection)() { return DWRITE_READING_DIRECTION(-1); }
	STDMETHOD(GetLocaleName)(UINT32 textPosition, UINT32* textLength, WCHAR const** localeName) { return E_NOTIMPL; }
	STDMETHOD(GetNumberSubstitution)(UINT32 textPosition, UINT32* textLength, IDWriteNumberSubstitution** numberSubstitution) { return E_NOTIMPL; }
};

class IDWriteTextAnalysisSinkFake : public IDWriteTextAnalysisSink
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD(SetScriptAnalysis)(UINT32 textPosition, UINT32 textLength, DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis) { return E_NOTIMPL; }
	STDMETHOD(SetLineBreakpoints)(UINT32 textPosition, UINT32 textLength, DWRITE_LINE_BREAKPOINT const* lineBreakpoints) { return E_NOTIMPL; }
	STDMETHOD(SetBidiLevel)(UINT32 textPosition, UINT32 textLength, UINT8 explicitLevel, UINT8 resolvedLevel) { return E_NOTIMPL; }
	STDMETHOD(SetNumberSubstitution)(UINT32 textPosition, UINT32 textLength, IDWriteNumberSubstitution* numberSubstitution) { return E_NOTIMPL; }
};

#define ANALYZER_TEST(name_) TEST_F(TextAnalyzerTest, name_)

ANALYZER_TEST(AnalyzeScript)
{
	MockedTextAnalyzer analyzer;
	IDWriteTextAnalysisSourceFake fakeSource;
	IDWriteTextAnalysisSinkFake fakeSink;
	EXPECT_CALL(analyzer.Mock, AnalyzeScript(&fakeSource, 0U, 10U, &fakeSink))
		.Times(1)
		.WillOnce(Return(S_OK));
	TextAnalysisSource ^source = TextAnalysisSource::FromPointer(&fakeSource);
	TextAnalysisSink ^sink = TextAnalysisSink::FromPointer(&fakeSink);
	Result result = analyzer.Analyzer->AnalyzeScript(source, 0, 10, sink);
	AssertLastResultSucceeded();
	ASSERT_TRUE(result.IsSuccess);
	delete sink;
	delete source;
}

ANALYZER_TEST(AnalyzeBidi)
{
	MockedTextAnalyzer analyzer;
	IDWriteTextAnalysisSourceFake fakeSource;
	IDWriteTextAnalysisSinkFake fakeSink;
	EXPECT_CALL(analyzer.Mock, AnalyzeBidi(&fakeSource, 0U, 10U, &fakeSink))
		.Times(1)
		.WillOnce(Return(S_OK));
	TextAnalysisSource ^source = TextAnalysisSource::FromPointer(&fakeSource);
	TextAnalysisSink ^sink = TextAnalysisSink::FromPointer(&fakeSink);
	Result result = analyzer.Analyzer->AnalyzeBidi(source, 0, 10, sink);
	AssertLastResultSucceeded();
	ASSERT_TRUE(result.IsSuccess);
	delete sink;
	delete source;
}
