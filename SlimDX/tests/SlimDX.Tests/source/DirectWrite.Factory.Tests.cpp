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

#include "IDWriteFontCollectionMock.h"
#include "IDWriteTextLayoutMock.h"
#include "SlimDXTest.h"

using namespace testing;
using namespace SlimDX;
using namespace SlimDX::Direct2D;
using namespace SlimDX::DirectWrite;
using namespace System;

class IDWriteFactoryMock : public IDWriteFactory
{
public:
	MOCK_IUNKNOWN;

	MOCK_METHOD2_WITH_CALLTYPE(STDMETHODCALLTYPE, GetSystemFontCollection, HRESULT(IDWriteFontCollection**, BOOL) );
	MOCK_METHOD4_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateCustomFontCollection, HRESULT(IDWriteFontCollectionLoader*, void const*, UINT32, IDWriteFontCollection**));
	MOCK_METHOD1_WITH_CALLTYPE(STDMETHODCALLTYPE, RegisterFontCollectionLoader, HRESULT(IDWriteFontCollectionLoader*));
	MOCK_METHOD1_WITH_CALLTYPE(STDMETHODCALLTYPE, UnregisterFontCollectionLoader, HRESULT(IDWriteFontCollectionLoader*));
	MOCK_METHOD3_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateFontFileReference, HRESULT(WCHAR const*, FILETIME const*, IDWriteFontFile**));
	MOCK_METHOD4_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateCustomFontFileReference, HRESULT(void const*, UINT32, IDWriteFontFileLoader*, IDWriteFontFile**));
	MOCK_METHOD6_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateFontFace, HRESULT(DWRITE_FONT_FACE_TYPE, UINT32, IDWriteFontFile* const*, UINT32, DWRITE_FONT_SIMULATIONS, IDWriteFontFace**));
	MOCK_METHOD1_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateRenderingParams, HRESULT(IDWriteRenderingParams**));
	MOCK_METHOD2_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateMonitorRenderingParams, HRESULT(HMONITOR, IDWriteRenderingParams**));
	MOCK_METHOD6_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateCustomRenderingParams, HRESULT(FLOAT, FLOAT, FLOAT, DWRITE_PIXEL_GEOMETRY, DWRITE_RENDERING_MODE, IDWriteRenderingParams**));
	MOCK_METHOD1_WITH_CALLTYPE(STDMETHODCALLTYPE, RegisterFontFileLoader, HRESULT(IDWriteFontFileLoader*));
	STDMETHOD(UnregisterFontFileLoader)(IDWriteFontFileLoader* fontFileLoader) { return E_NOTIMPL; } 
	MOCK_METHOD8_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateTextFormat, HRESULT(WCHAR const*, IDWriteFontCollection*, DWRITE_FONT_WEIGHT, DWRITE_FONT_STYLE, DWRITE_FONT_STRETCH, FLOAT, WCHAR const*, IDWriteTextFormat**));
	MOCK_METHOD1_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateTypography, HRESULT(IDWriteTypography**));
	MOCK_METHOD1_WITH_CALLTYPE(STDMETHODCALLTYPE, GetGdiInterop, HRESULT(IDWriteGdiInterop**));
	MOCK_METHOD6_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateTextLayout, HRESULT(WCHAR const*, UINT32, IDWriteTextFormat*, FLOAT, FLOAT, IDWriteTextLayout**));
	MOCK_METHOD9_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateGdiCompatibleTextLayout, HRESULT(WCHAR const*, UINT32, IDWriteTextFormat*, FLOAT, FLOAT, FLOAT, DWRITE_MATRIX const*, BOOL, IDWriteTextLayout**) );
	MOCK_METHOD2_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateEllipsisTrimmingSign, HRESULT(IDWriteTextFormat*, IDWriteInlineObject**));
	MOCK_METHOD1_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateTextAnalyzer, HRESULT(IDWriteTextAnalyzer**));
	MOCK_METHOD4_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateNumberSubstitution, HRESULT(DWRITE_NUMBER_SUBSTITUTION_METHOD, WCHAR const*, BOOL, IDWriteNumberSubstitution**));
	MOCK_METHOD8_WITH_CALLTYPE(STDMETHODCALLTYPE, CreateGlyphRunAnalysis, HRESULT(DWRITE_GLYPH_RUN const*, FLOAT, DWRITE_MATRIX const*, DWRITE_RENDERING_MODE, DWRITE_MEASURING_MODE, FLOAT, FLOAT, IDWriteGlyphRunAnalysis**));
};

class IDWriteFontFileFake : public IDWriteFontFile
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD(GetReferenceKey)(void const** fontFileReferenceKey,UINT32* fontFileReferenceKeySize) { return E_NOTIMPL; }
	STDMETHOD(GetLoader)(IDWriteFontFileLoader** fontFileLoader) { return E_NOTIMPL; }
	STDMETHOD(Analyze)(BOOL* isSupportedFontType, DWRITE_FONT_FILE_TYPE* fontFileType, DWRITE_FONT_FACE_TYPE* fontFaceType, UINT32* numberOfFaces) { return E_NOTIMPL; }
};

class IDWriteFontFaceFake : public IDWriteFontFace
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD_(DWRITE_FONT_FACE_TYPE, GetType)() { return DWRITE_FONT_FACE_TYPE(-1); }
	STDMETHOD(GetFiles)(UINT32* numberOfFiles, IDWriteFontFile** fontFiles) { return E_NOTIMPL; }
	STDMETHOD_(UINT32, GetIndex)() { return UINT32(~0U); }
	STDMETHOD_(DWRITE_FONT_SIMULATIONS, GetSimulations)() { return DWRITE_FONT_SIMULATIONS(-1); }
	STDMETHOD_(BOOL, IsSymbolFont)() { return BOOL(-1); }
	STDMETHOD_(void, GetMetrics)(DWRITE_FONT_METRICS* fontFaceMetrics) { }
	STDMETHOD_(UINT16, GetGlyphCount)() { return UINT16(~0U); }
	STDMETHOD(GetDesignGlyphMetrics)(UINT16 const* glyphIndices, UINT32 glyphCount, DWRITE_GLYPH_METRICS* glyphMetrics, BOOL isSideways = FALSE) { return E_NOTIMPL; }
	STDMETHOD(GetGlyphIndices)(UINT32 const* codePoints, UINT32 codePointCount, UINT16* glyphIndices) { return E_NOTIMPL; }
	STDMETHOD(TryGetFontTable)(UINT32 openTypeTableTag, const void** tableData, UINT32* tableSize, void** tableContext, BOOL* exists) { return E_NOTIMPL; }
	STDMETHOD_(void, ReleaseFontTable)(void* tableContext) { }
	STDMETHOD(GetGlyphRunOutline)(FLOAT emSize, UINT16 const* glyphIndices, FLOAT const* glyphAdvances, DWRITE_GLYPH_OFFSET const* glyphOffsets, UINT32 glyphCount, BOOL isSideways, BOOL isRightToLeft, IDWriteGeometrySink* geometrySink) { return E_NOTIMPL; }
	STDMETHOD(GetRecommendedRenderingMode)(FLOAT emSize, FLOAT pixelsPerDip, DWRITE_MEASURING_MODE measuringMode, IDWriteRenderingParams* renderingParams, DWRITE_RENDERING_MODE* renderingMode) { return E_NOTIMPL; }
	STDMETHOD(GetGdiCompatibleMetrics)(FLOAT emSize, FLOAT pixelsPerDip, DWRITE_MATRIX const* transform, DWRITE_FONT_METRICS* fontFaceMetrics) { return E_NOTIMPL; }
	STDMETHOD(GetGdiCompatibleGlyphMetrics)(FLOAT emSize, FLOAT pixelsPerDip, DWRITE_MATRIX const* transform, BOOL useGdiNatural, UINT16 const* glyphIndices, UINT32 glyphCount, DWRITE_GLYPH_METRICS* glyphMetrics, BOOL isSideways = FALSE) { return E_NOTIMPL; }
};

class IDWriteFontFileLoaderFake : public IDWriteFontFileLoader
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD(CreateStreamFromKey)(void const* fontFileReferenceKey, UINT32 fontFileReferenceKeySize, IDWriteFontFileStream** fontFileStream) { return E_NOTIMPL; }
};

class IDWriteGdiInteropFake : public IDWriteGdiInterop
{
public:
	MOCK_IUNKNOWN;

	// IDWriteGdiInterop
	STDMETHOD(CreateFontFromLOGFONT)(LOGFONTW const* logFont, IDWriteFont** font) { return E_NOTIMPL; }
	STDMETHOD(ConvertFontToLOGFONT)(IDWriteFont* font, LOGFONTW* logFont, BOOL* isSystemFont) { return E_NOTIMPL; }
	STDMETHOD(ConvertFontFaceToLOGFONT)(IDWriteFontFace* font, LOGFONTW* logFont) { return E_NOTIMPL; }
	STDMETHOD(CreateFontFaceFromHdc)(HDC hdc, IDWriteFontFace** fontFace) { return E_NOTIMPL; }
	STDMETHOD(CreateBitmapRenderTarget)(HDC hdc, UINT32 width, UINT32 height, IDWriteBitmapRenderTarget** renderTarget) { return E_NOTIMPL; }
};

class IDWriteGlyphRunAnalysisFake : public IDWriteGlyphRunAnalysis
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD(GetAlphaTextureBounds)(DWRITE_TEXTURE_TYPE textureType, RECT* textureBounds) { return E_NOTIMPL; }
	STDMETHOD(CreateAlphaTexture)(DWRITE_TEXTURE_TYPE textureType, RECT const* textureBounds, BYTE* alphaValues, UINT32 bufferSize) { return E_NOTIMPL; }
	STDMETHOD(GetAlphaBlendParams)(IDWriteRenderingParams* renderingParams, FLOAT* blendGamma, FLOAT* blendEnhancedContrast, FLOAT* blendClearTypeLevel) { return E_NOTIMPL; }
};

class IDWriteInlineObjectFake : public IDWriteInlineObject
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD(Draw)(void* clientDrawingContext, IDWriteTextRenderer* renderer, FLOAT originX, FLOAT originY, BOOL isSideways, BOOL isRightToLeft, IUnknown* clientDrawingEffect) { return E_NOTIMPL; }
	STDMETHOD(GetMetrics)(DWRITE_INLINE_OBJECT_METRICS* metrics) { return E_NOTIMPL; }
	STDMETHOD(GetOverhangMetrics)(DWRITE_OVERHANG_METRICS* overhangs) { return E_NOTIMPL; }
	STDMETHOD(GetBreakConditions)(DWRITE_BREAK_CONDITION* breakConditionBefore,DWRITE_BREAK_CONDITION* breakConditionAfter) { return E_NOTIMPL; }
};

class IDWriteNumberSubstitutionFake : public IDWriteNumberSubstitution
{
public:
	MOCK_IUNKNOWN;
};

class IDWriteRenderingParamsFake : public IDWriteRenderingParams
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD_(FLOAT, GetGamma)() { return -1.0f; }
	STDMETHOD_(FLOAT, GetEnhancedContrast)() { return -1.0f; }
	STDMETHOD_(FLOAT, GetClearTypeLevel)() { return -1.0f; }
	STDMETHOD_(DWRITE_PIXEL_GEOMETRY, GetPixelGeometry)() { return DWRITE_PIXEL_GEOMETRY(-1); }
	STDMETHOD_(DWRITE_RENDERING_MODE, GetRenderingMode)() { return DWRITE_RENDERING_MODE(-1); }
};

class IDWriteTextAnalyzerFake : public IDWriteTextAnalyzer
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD(AnalyzeScript)(IDWriteTextAnalysisSource* analysisSource, UINT32 textPosition, UINT32 textLength, IDWriteTextAnalysisSink* analysisSink) { return E_NOTIMPL; }
	STDMETHOD(AnalyzeBidi)(IDWriteTextAnalysisSource* analysisSource, UINT32 textPosition, UINT32 textLength, IDWriteTextAnalysisSink* analysisSink) { return E_NOTIMPL; }
	STDMETHOD(AnalyzeNumberSubstitution)(IDWriteTextAnalysisSource* analysisSource, UINT32 textPosition, UINT32 textLength, IDWriteTextAnalysisSink* analysisSink) { return E_NOTIMPL; }
	STDMETHOD(AnalyzeLineBreakpoints)(IDWriteTextAnalysisSource* analysisSource, UINT32 textPosition, UINT32 textLength, IDWriteTextAnalysisSink* analysisSink) { return E_NOTIMPL; }
	STDMETHOD(GetGlyphs)(WCHAR const* textString, UINT32 textLength, IDWriteFontFace* fontFace, BOOL isSideways, BOOL isRightToLeft,
		DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis, WCHAR const* localeName, IDWriteNumberSubstitution* numberSubstitution,
		DWRITE_TYPOGRAPHIC_FEATURES const** features, UINT32 const* featureRangeLengths, UINT32 featureRanges, UINT32 maxGlyphCount,
		UINT16* clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES* textProps, UINT16* glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES* glyphProps,
		UINT32* actualGlyphCount) { return E_NOTIMPL; }
	STDMETHOD(GetGlyphPlacements)(WCHAR const* textString, UINT16 const* clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES* textProps, UINT32 textLength,
		UINT16 const* glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES const* glyphProps, UINT32 glyphCount, IDWriteFontFace * fontFace, FLOAT fontEmSize,
		BOOL isSideways, BOOL isRightToLeft, DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis, WCHAR const* localeName, DWRITE_TYPOGRAPHIC_FEATURES const** features,
		UINT32 const* featureRangeLengths, UINT32 featureRanges, FLOAT* glyphAdvances, DWRITE_GLYPH_OFFSET* glyphOffsets) { return E_NOTIMPL; }
	STDMETHOD(GetGdiCompatibleGlyphPlacements)(WCHAR const* textString, UINT16 const* clusterMap, DWRITE_SHAPING_TEXT_PROPERTIES* textProps, UINT32 textLength,
		UINT16 const* glyphIndices, DWRITE_SHAPING_GLYPH_PROPERTIES const* glyphProps, UINT32 glyphCount, IDWriteFontFace * fontFace, FLOAT fontEmSize, FLOAT pixelsPerDip,
		DWRITE_MATRIX const* transform, BOOL useGdiNatural, BOOL isSideways, BOOL isRightToLeft, DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis, WCHAR const* localeName,
		DWRITE_TYPOGRAPHIC_FEATURES const** features, UINT32 const* featureRangeLengths, UINT32 featureRanges, FLOAT* glyphAdvances, DWRITE_GLYPH_OFFSET* glyphOffsets) { return E_NOTIMPL; }
};

class IDWriteTextFormatFake : public IDWriteTextFormat
{
public:
	MOCK_IUNKNOWN;

	STDMETHOD(SetTextAlignment)(DWRITE_TEXT_ALIGNMENT textAlignment) { return E_NOTIMPL; }
	STDMETHOD(SetParagraphAlignment)(DWRITE_PARAGRAPH_ALIGNMENT paragraphAlignment) { return E_NOTIMPL; }
	STDMETHOD(SetWordWrapping)(DWRITE_WORD_WRAPPING wordWrapping) { return E_NOTIMPL; }
	STDMETHOD(SetReadingDirection)(DWRITE_READING_DIRECTION readingDirection) { return E_NOTIMPL; }
	STDMETHOD(SetFlowDirection)(DWRITE_FLOW_DIRECTION flowDirection) { return E_NOTIMPL; }
	STDMETHOD(SetIncrementalTabStop)(FLOAT incrementalTabStop) { return E_NOTIMPL; }
	STDMETHOD(SetTrimming)(__in DWRITE_TRIMMING const* trimmingOptions,IDWriteInlineObject* trimmingSign) { return E_NOTIMPL; }
	STDMETHOD(SetLineSpacing)(DWRITE_LINE_SPACING_METHOD lineSpacingMethod,FLOAT lineSpacing,FLOAT baseline) { return E_NOTIMPL; }
	STDMETHOD_(DWRITE_TEXT_ALIGNMENT, GetTextAlignment)() { return DWRITE_TEXT_ALIGNMENT(-1); }
	STDMETHOD_(DWRITE_PARAGRAPH_ALIGNMENT, GetParagraphAlignment)() { return DWRITE_PARAGRAPH_ALIGNMENT(-1); }
	STDMETHOD_(DWRITE_WORD_WRAPPING, GetWordWrapping)() { return DWRITE_WORD_WRAPPING(-1); }
	STDMETHOD_(DWRITE_READING_DIRECTION, GetReadingDirection)() { return DWRITE_READING_DIRECTION(-1); }
	STDMETHOD_(DWRITE_FLOW_DIRECTION, GetFlowDirection)() { return DWRITE_FLOW_DIRECTION(-1); }
	STDMETHOD_(FLOAT, GetIncrementalTabStop)() { return -1.0f; }
	STDMETHOD(GetTrimming)(__out DWRITE_TRIMMING* trimmingOptions,__out IDWriteInlineObject** trimmingSign) { return E_NOTIMPL; }
	STDMETHOD(GetLineSpacing)(__out DWRITE_LINE_SPACING_METHOD* lineSpacingMethod,__out FLOAT* lineSpacing,__out FLOAT* baseline) { return E_NOTIMPL; }
	STDMETHOD(GetFontCollection)(__out IDWriteFontCollection** fontCollection) { return E_NOTIMPL; }
	STDMETHOD_(UINT32, GetFontFamilyNameLength)() { return UINT32(~0U); }
	STDMETHOD(GetFontFamilyName)(__out_ecount_z(nameSize) WCHAR* fontFamilyName,UINT32 nameSize) { return E_NOTIMPL; }
	STDMETHOD_(DWRITE_FONT_WEIGHT, GetFontWeight)() { return DWRITE_FONT_WEIGHT(-1); }
	STDMETHOD_(DWRITE_FONT_STYLE, GetFontStyle)() { return DWRITE_FONT_STYLE(-1); }
	STDMETHOD_(DWRITE_FONT_STRETCH, GetFontStretch)() { return DWRITE_FONT_STRETCH(-1); }
	STDMETHOD_(FLOAT, GetFontSize)() { return -1.0f; }
	STDMETHOD_(UINT32, GetLocaleNameLength)() { return UINT32(~0U); }
	STDMETHOD(GetLocaleName)(__out_ecount_z(nameSize) WCHAR* localeName,UINT32 nameSize) { return E_NOTIMPL; }
};

class IDWriteTypographyFake : public IDWriteTypography
{
public:
	MOCK_IUNKNOWN;

	// IDWriteTypography
	STDMETHOD(AddFontFeature)(DWRITE_FONT_FEATURE fontFeature) { return E_NOTIMPL; }
	STDMETHOD_(UINT32, GetFontFeatureCount)() { return E_NOTIMPL; }
	STDMETHOD(GetFontFeature)(UINT32 fontFeatureIndex, DWRITE_FONT_FEATURE* fontFeature) { return E_NOTIMPL; }
};

ref class MockedFactory
{
public:
	MockedFactory()
		: mockFactory(new IDWriteFactoryMock),
		font(SlimDX::DirectWrite::Factory::FromPointer(System::IntPtr(mockFactory)))
	{
	}
	~MockedFactory()
	{
		delete font;
		font = nullptr;
		delete mockFactory;
		mockFactory = 0;
	}
	property IDWriteFactoryMock &Mock
	{
		IDWriteFactoryMock &get() { return *mockFactory; }
	}
	property SlimDX::DirectWrite::Factory ^Factory
	{
		SlimDX::DirectWrite::Factory ^get() { return font; }
	}

private:
	IDWriteFactoryMock *mockFactory;
	SlimDX::DirectWrite::Factory ^font;
};

class FactoryTest : public SlimDXTest
{
};

#define FACTORY_TEST(name_) TEST_F(FactoryTest, name_)

FACTORY_TEST(GetSystemFontCollection)
{
	MockedFactory factory;
	IDWriteFontCollectionMock mockCollection;
	EXPECT_CALL(factory.Mock, GetSystemFontCollection(NotNull(), FALSE))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<0>(&mockCollection), Return(S_OK)));
	FontCollection ^collection = factory.Factory->GetSystemFontCollection(false);
	AssertLastResultSucceeded();
	ASSERT_TRUE(collection != nullptr);
	ASSERT_EQ(&mockCollection, collection->InternalPointer);
	delete collection;
}

FACTORY_TEST(GetSystemFontCollectionFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock, GetSystemFontCollection(NotNull(), FALSE))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	FontCollection ^collection = factory.Factory->GetSystemFontCollection(false);
	AssertLastResultFailed();
	ASSERT_TRUE(collection == nullptr);
}

FACTORY_TEST(CreateGdiCompatibleTextLayout)
{
	MockedFactory factory;
	IDWriteTextLayoutMock mockLayout;
	IDWriteTextFormatFake fakeFormat;
	EXPECT_CALL(factory.Mock, CreateGdiCompatibleTextLayout(NotNull(), Gt(0U), &fakeFormat,
			12.5f, 44.4f, 340.0f, 0, TRUE, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<8>(&mockLayout), Return(S_OK)));
	TextFormat ^textFormat = TextFormat::FromPointer(&fakeFormat);
	TextLayout ^layout = factory.Factory->CreateGdiCompatibleTextLayout(
		gcnew String("Slartibartfast"), textFormat, 12.5, 44.4f, 340.0f, true);
	AssertLastResultSucceeded();
	ASSERT_TRUE(layout != nullptr);
	ASSERT_EQ(&mockLayout, layout->InternalPointer);
	delete textFormat;
	delete layout;
}

FACTORY_TEST(CreateGdiCompatibleTextLayoutFailureReturnsNullPtr)
{
	MockedFactory factory;
	IDWriteTextFormatFake fakeFormat;
	EXPECT_CALL(factory.Mock, CreateGdiCompatibleTextLayout(NotNull(), Gt(0U), &fakeFormat,
		12.5f, 44.4f, 340.0f, 0, TRUE, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	TextFormat ^textFormat = TextFormat::FromPointer(&fakeFormat);
	TextLayout ^layout = factory.Factory->CreateGdiCompatibleTextLayout(
		gcnew String("Slartibartfast"), textFormat, 12.5, 44.4f, 340.0f, true);
	AssertLastResultFailed();
	ASSERT_TRUE(layout == nullptr);
	delete textFormat;
}

FACTORY_TEST(CreateEllipsisTrimmingSign)
{
	MockedFactory factory;
	IDWriteTextFormatFake fakeFormat;
	IDWriteInlineObjectFake fakeInlineObject;
	EXPECT_CALL(factory.Mock, CreateEllipsisTrimmingSign(&fakeFormat, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<1>(&fakeInlineObject), Return(S_OK)));
	TextFormat ^format = TextFormat::FromPointer(&fakeFormat);
	InlineObject ^sign = factory.Factory->CreateEllipsisTrimmingSign(format);
	AssertLastResultSucceeded();
	ASSERT_TRUE(nullptr != sign);
	ASSERT_EQ(&fakeInlineObject, sign->InternalPointer);
	delete sign;
	delete format;
}

FACTORY_TEST(CreateEllipsisTrimmingSignFailureReturnsNullPtr)
{
	MockedFactory factory;
	IDWriteTextFormatFake fakeFormat;
	EXPECT_CALL(factory.Mock, CreateEllipsisTrimmingSign(&fakeFormat, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	TextFormat ^format = TextFormat::FromPointer(&fakeFormat);
	InlineObject ^sign = factory.Factory->CreateEllipsisTrimmingSign(format);
	AssertLastResultFailed();
	ASSERT_TRUE(nullptr == sign);
	delete format;
}

FACTORY_TEST(CreateFontFace)
{
	MockedFactory factory;
	IDWriteFontFileFake fakeFile;
	IDWriteFontFaceFake fakeFace;
	EXPECT_CALL(factory.Mock, CreateFontFace(DWRITE_FONT_FACE_TYPE_TYPE1,
			1, NotNull(), 0, DWRITE_FONT_SIMULATIONS_OBLIQUE, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<5>(&fakeFace), Return(S_OK)));
	FontFile ^fontFile = FontFile::FromPointer(&fakeFile);
	array<FontFile ^> ^fontFiles = gcnew array<FontFile ^>(1);
	fontFiles[0] = fontFile;
	FontFace ^fontFace = factory.Factory->CreateFontFace(FontFaceType::Type1, fontFiles, 0, FontSimulations::Oblique);
	AssertLastResultSucceeded();
	ASSERT_TRUE(nullptr != fontFace);
	ASSERT_EQ(&fakeFace, fontFace->InternalPointer);
	delete fontFace;
	delete fontFiles;
	delete fontFile;
}

FACTORY_TEST(CreateFontFaceFailureReturnsNullPtr)
{
	MockedFactory factory;
	IDWriteFontFileFake fakeFile;
	EXPECT_CALL(factory.Mock, CreateFontFace(DWRITE_FONT_FACE_TYPE_TYPE1,
		1, NotNull(), 0, DWRITE_FONT_SIMULATIONS_OBLIQUE, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	FontFile ^fontFile = FontFile::FromPointer(&fakeFile);
	array<FontFile ^> ^fontFiles = gcnew array<FontFile ^>(1);
	fontFiles[0] = fontFile;
	FontFace ^fontFace = factory.Factory->CreateFontFace(FontFaceType::Type1, fontFiles, 0, FontSimulations::Oblique);
	AssertLastResultFailed();
	ASSERT_TRUE(nullptr == fontFace);
	delete fontFiles;
	delete fontFile;
}

FACTORY_TEST(CreateFontFileReference)
{
	MockedFactory factory;
	IDWriteFontFileFake fakeFile;
	EXPECT_CALL(factory.Mock, CreateFontFileReference(NotNull(), 0, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<2>(&fakeFile), Return(S_OK)));
	FontFile ^file = factory.Factory->CreateFontFileReference(gcnew String("Slartibartfast.fnt"));
	AssertLastResultSucceeded();
	ASSERT_TRUE(file != nullptr);
	ASSERT_EQ(&fakeFile, file->InternalPointer);
	delete file;
}

FACTORY_TEST(CreateFontFileReferenceFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock, CreateFontFileReference(NotNull(), 0, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	FontFile ^file = factory.Factory->CreateFontFileReference(gcnew String("Slartibartfast.fnt"));
	AssertLastResultFailed();
	ASSERT_TRUE(file == nullptr);
}

static GlyphRun ^CreateGlyphRun(IDWriteFontFaceFake &fakeFace)
{
	GlyphRun ^glyphRun = gcnew GlyphRun;
	glyphRun->FontFace = FontFace::FromPointer(&fakeFace);
	return glyphRun;
}

static void ReleaseGlyphRun(GlyphRun ^glyphRun)
{
	delete glyphRun->FontFace;
	delete glyphRun;
}

FACTORY_TEST(CreateGlyphRunAnalysisFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock, CreateGlyphRunAnalysis(NotNull(), 640.0f, 0,
			DWRITE_RENDERING_MODE_CLEARTYPE_NATURAL, DWRITE_MEASURING_MODE_GDI_NATURAL,
			12.5f, 33.3f, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	IDWriteFontFaceFake fakeFace;
	GlyphRun ^glyphRun = CreateGlyphRun(fakeFace);
	GlyphRunAnalysis ^analysis = factory.Factory->CreateGlyphRunAnalysis(glyphRun, 640.0f,
		RenderingMode::ClearTypeNatural, MeasuringMode::GdiNatural,
		12.5f, 33.3f);
	AssertLastResultFailed();
	ASSERT_TRUE(analysis == nullptr);
	ReleaseGlyphRun(glyphRun);
}

static Matrix3x2 ExpectedMatrix()
{
	Matrix3x2 matrix;
	matrix.M11 = 1.0f;
	matrix.M12 = 0.0f;
	matrix.M21 = 0.0f;
	matrix.M22 = 1.0f;
	matrix.M31 = 12.5f;
	matrix.M32 = 55.5f;
	return matrix;
}

FACTORY_TEST(CreateGlyphRunAnalysisWithTransform)
{
	MockedFactory factory;
	IDWriteGlyphRunAnalysisFake fakeAnalysis;
	EXPECT_CALL(factory.Mock, CreateGlyphRunAnalysis(NotNull(), 640.0f, NotNull(),
			DWRITE_RENDERING_MODE_CLEARTYPE_NATURAL, DWRITE_MEASURING_MODE_GDI_NATURAL,
			12.5f, 33.3f, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<7>(&fakeAnalysis), Return(S_OK)));
	IDWriteFontFaceFake fakeFace;
	GlyphRun ^glyphRun = CreateGlyphRun(fakeFace);
	GlyphRunAnalysis ^analysis = factory.Factory->CreateGlyphRunAnalysis(glyphRun, 640.0f,
		ExpectedMatrix(), RenderingMode::ClearTypeNatural, MeasuringMode::GdiNatural,
		12.5f, 33.3f);
	AssertLastResultSucceeded();
	ASSERT_TRUE(analysis != nullptr);
	ASSERT_EQ(&fakeAnalysis, analysis->InternalPointer);
	delete analysis;
	ReleaseGlyphRun(glyphRun);
}

FACTORY_TEST(CreateMonitorRenderingParameters)
{
	MockedFactory factory;
	IDWriteRenderingParamsFake fakeParams;
	EXPECT_CALL(factory.Mock, CreateMonitorRenderingParams(NotNull(), NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<1>(&fakeParams), Return(S_OK)));
	RenderingParameters ^params = factory.Factory->CreateMonitorRenderingParameters(IntPtr(0x666));
	AssertLastResultSucceeded();
	ASSERT_TRUE(params != nullptr);
	ASSERT_EQ(&fakeParams, params->InternalPointer);
	delete params;
}

FACTORY_TEST(CreateMonitorRenderingParametersFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock, CreateMonitorRenderingParams(NotNull(), NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	RenderingParameters ^params = factory.Factory->CreateMonitorRenderingParameters(IntPtr(0x666));
	AssertLastResultFailed();
	ASSERT_TRUE(params == nullptr);
}

FACTORY_TEST(CreateNumberSubstitution)
{
	MockedFactory factory;
	IDWriteNumberSubstitutionFake fakeSub;
	EXPECT_CALL(factory.Mock,
		CreateNumberSubstitution(DWRITE_NUMBER_SUBSTITUTION_METHOD_CONTEXTUAL, NotNull(), TRUE, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<3>(&fakeSub), Return(S_OK)));
	NumberSubstitution ^sub = factory.Factory->
		CreateNumberSubstitution(NumberSubstitutionMethod::Contextual, gcnew String("Slartibartfast"), true);
	AssertLastResultSucceeded();
	ASSERT_TRUE(sub != nullptr);
	ASSERT_EQ(&fakeSub, sub->InternalPointer);
	delete sub;
}

FACTORY_TEST(CreateNumberSubstitutionFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock,
		CreateNumberSubstitution(DWRITE_NUMBER_SUBSTITUTION_METHOD_CONTEXTUAL, NotNull(), TRUE, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	NumberSubstitution ^sub = factory.Factory->
		CreateNumberSubstitution(NumberSubstitutionMethod::Contextual, gcnew String("Slartibartfast"), true);
	AssertLastResultFailed();
	ASSERT_TRUE(sub == nullptr);
}

FACTORY_TEST(CreateRenderingParameters)
{
	MockedFactory factory;
	IDWriteRenderingParamsFake fakeParams;
	EXPECT_CALL(factory.Mock, CreateRenderingParams(NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<0>(&fakeParams), Return(S_OK)));
	RenderingParameters ^params = factory.Factory->CreateRenderingParameters();
	AssertLastResultSucceeded();
	ASSERT_TRUE(params != nullptr);
	ASSERT_EQ(&fakeParams, params->InternalPointer);
	delete params;
}

FACTORY_TEST(CreateRenderingParametersFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock, CreateRenderingParams(NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	RenderingParameters ^params = factory.Factory->CreateRenderingParameters();
	AssertLastResultFailed();
	ASSERT_TRUE(params == nullptr);
}

FACTORY_TEST(CreateTextAnalyzer)
{
	MockedFactory factory;
	IDWriteTextAnalyzerFake fakeAnalyzer;
	EXPECT_CALL(factory.Mock, CreateTextAnalyzer(NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<0>(&fakeAnalyzer), Return(S_OK)));
	TextAnalyzer ^analyzer = factory.Factory->CreateTextAnalyzer();
	AssertLastResultSucceeded();
	ASSERT_TRUE(analyzer != nullptr);
	ASSERT_EQ(&fakeAnalyzer, analyzer->InternalPointer);
	delete analyzer;
}

FACTORY_TEST(CreateTextAnalyzerFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock, CreateTextAnalyzer(NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	TextAnalyzer ^analyzer = factory.Factory->CreateTextAnalyzer();
	AssertLastResultFailed();
	ASSERT_TRUE(analyzer == nullptr);
}

FACTORY_TEST(CreateTextFormat)
{
	MockedFactory factory;
	IDWriteTextFormatFake fakeFormat;
	EXPECT_CALL(factory.Mock, CreateTextFormat(NotNull(), 0,
			DWRITE_FONT_WEIGHT_ULTRA_BOLD, DWRITE_FONT_STYLE_OBLIQUE,
			DWRITE_FONT_STRETCH_ULTRA_EXPANDED, 12.5f, NotNull(), NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<7>(&fakeFormat), Return(S_OK)));
	TextFormat ^format = factory.Factory->CreateTextFormat(gcnew String("Slanted"),
		FontWeight::UltraBold, FontStyle::Oblique, FontStretch::UltraExpanded,
		12.5f, gcnew String("Slartibartfast"));
	AssertLastResultSucceeded();
	ASSERT_TRUE(format != nullptr);
	ASSERT_EQ(&fakeFormat, format->InternalPointer);
	delete format;
}

FACTORY_TEST(CreateTextFormatFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock, CreateTextFormat(NotNull(), 0,
			DWRITE_FONT_WEIGHT_ULTRA_BOLD, DWRITE_FONT_STYLE_OBLIQUE,
			DWRITE_FONT_STRETCH_ULTRA_EXPANDED, 12.5f, NotNull(), NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	TextFormat ^format = factory.Factory->CreateTextFormat(gcnew String("Slanted"),
		FontWeight::UltraBold, FontStyle::Oblique, FontStretch::UltraExpanded,
		12.5f, gcnew String("Slartibartfast"));
	AssertLastResultFailed();
	ASSERT_TRUE(format == nullptr);
}

FACTORY_TEST(CreateTextFormatWithCollection)
{
	MockedFactory factory;
	IDWriteTextFormatFake fakeFormat;
	IDWriteFontCollectionMock fakeCollection;
	EXPECT_CALL(factory.Mock, CreateTextFormat(NotNull(), &fakeCollection,
			DWRITE_FONT_WEIGHT_ULTRA_BOLD, DWRITE_FONT_STYLE_OBLIQUE,
			DWRITE_FONT_STRETCH_ULTRA_EXPANDED, 12.5f, NotNull(), NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<7>(&fakeFormat), Return(S_OK)));
	FontCollection ^fontCollection = FontCollection::FromPointer(&fakeCollection);
	TextFormat ^format = factory.Factory->CreateTextFormat(gcnew String("Slanted"),
		fontCollection, FontWeight::UltraBold, FontStyle::Oblique, FontStretch::UltraExpanded,
		12.5f, gcnew String("Slartibartfast"));
	AssertLastResultSucceeded();
	ASSERT_TRUE(format != nullptr);
	ASSERT_EQ(&fakeFormat, format->InternalPointer);
	delete format;
	delete fontCollection;
}

FACTORY_TEST(CreateTextLayout)
{
	MockedFactory factory;
	IDWriteTextLayoutMock fakeLayout;
	IDWriteTextFormatFake fakeFormat;
	EXPECT_CALL(factory.Mock, CreateTextLayout(NotNull(), Gt(0U), &fakeFormat, 12.5f, 33.3f, NotNull()))
		.Times(1)
		.WillOnce(DoAll(SetArgumentPointee<5>(&fakeLayout), Return(S_OK)));
	TextFormat ^format = TextFormat::FromPointer(&fakeFormat);
	TextLayout ^layout = factory.Factory->CreateTextLayout(gcnew String("Text"), format, 12.5f, 33.3f);
	AssertLastResultSucceeded();
	ASSERT_TRUE(layout != nullptr);
	ASSERT_EQ(&fakeLayout, layout->InternalPointer);
	delete layout;
	delete format;
}

FACTORY_TEST(CreateTextLayoutFailureReturnsNullPtr)
{
	MockedFactory factory;
	EXPECT_CALL(factory.Mock, CreateTextLayout(NotNull(), Gt(0U), NotNull(), 12.5f, 33.3f, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	IDWriteTextFormatFake fakeFormat;
	TextFormat ^format = TextFormat::FromPointer(&fakeFormat);
	TextLayout ^layout = factory.Factory->CreateTextLayout(gcnew String("Text"), format, 12.5f, 33.3f);
	AssertLastResultFailed();
	ASSERT_TRUE(layout == nullptr);
	delete format;
}

FACTORY_TEST(CreateTypography)
{
	MockedFactory factory;
	IDWriteTypographyFake fakeTypography;
	EXPECT_CALL(factory.Mock, CreateTypography(NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<0>(&fakeTypography), Return(S_OK)));
	Typography ^typography = factory.Factory->CreateTypography();
	AssertLastResultSucceeded();
	ASSERT_TRUE(nullptr != typography);
	ASSERT_EQ(&fakeTypography, typography->InternalPointer);
	delete typography;
}

FACTORY_TEST(CreateTypographyFailureReturnsNullPtr)
{
	MockedFactory factory;
	IDWriteTypographyFake fakeTypography;
	EXPECT_CALL(factory.Mock, CreateTypography(NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	Typography ^typography = factory.Factory->CreateTypography();
	AssertLastResultFailed();
	ASSERT_TRUE(nullptr == typography);
}

FACTORY_TEST(GetGdiInterop)
{
	MockedFactory factory;
	IDWriteGdiInteropFake fakeGdiInterop;
	EXPECT_CALL(factory.Mock, GetGdiInterop(NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<0>(&fakeGdiInterop), Return(S_OK)));
	GdiInterop ^gdiInterop = factory.Factory->GetGdiInterop();
	AssertLastResultSucceeded();
	ASSERT_TRUE(nullptr != gdiInterop);
	ASSERT_EQ(&fakeGdiInterop, gdiInterop->InternalPointer);
	delete gdiInterop;
}

FACTORY_TEST(GetGdiInteropFailureReturnsNullPtr)
{
	MockedFactory factory;
	IDWriteTypographyFake fakeGdiInterop;
	EXPECT_CALL(factory.Mock, GetGdiInterop(NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	GdiInterop ^gdiInterop = factory.Factory->GetGdiInterop();
	AssertLastResultFailed();
	ASSERT_TRUE(nullptr == gdiInterop);
}

ref class FontCollectionLoaderFake : public IFontCollectionLoader
{
public:
	FontCollectionLoaderFake()
	{
	}

	virtual FontFileEnumerator ^CreateEnumeratorFromKey(SlimDX::DirectWrite::Factory ^factory, IntPtr collectionKey, int collectionKeySize)
	{
		return nullptr;
	}
};

FACTORY_TEST(RegisterFontCollectionLoader)
{
	MockedFactory factory;
	FontCollectionLoaderFake ^loader = gcnew FontCollectionLoaderFake;
	EXPECT_CALL(factory.Mock, RegisterFontCollectionLoader(NotNull()))
		.Times(1).WillOnce(Return(S_OK));
	ASSERT_TRUE(factory.Factory->RegisterFontCollectionLoader(loader).IsSuccess);
	AssertLastResultSucceeded();
	delete loader;	
}

FACTORY_TEST(UnregisterFontCollectionLoader)
{
	MockedFactory factory;
	FontCollectionLoaderFake ^loader = gcnew FontCollectionLoaderFake;
	EXPECT_CALL(factory.Mock, UnregisterFontCollectionLoader(NotNull()))
		.Times(1).WillOnce(Return(S_OK));
	ASSERT_TRUE(factory.Factory->UnregisterFontCollectionLoader(loader).IsSuccess);
	AssertLastResultSucceeded();
	delete loader;	
}

FACTORY_TEST(CreateCustomFontCollection)
{
	MockedFactory factory;
	IDWriteFontCollectionMock mockCollection;
	EXPECT_CALL(factory.Mock, CreateCustomFontCollection(NotNull(), 0, 0, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<3>(&mockCollection), Return(S_OK)));
	FontCollectionLoaderFake ^loader = gcnew FontCollectionLoaderFake;
	FontCollection ^collection = factory.Factory->CreateCustomFontCollection(loader, IntPtr(0), 0);
	AssertLastResultSucceeded();
	ASSERT_TRUE(collection != nullptr);
	ASSERT_EQ(&mockCollection, collection->InternalPointer);
	delete collection;
	delete loader;
}

FACTORY_TEST(CreateCustomFontCollectionFailureReturnsNullPtr)
{
	MockedFactory factory;
	IDWriteFontCollectionMock mockCollection;
	EXPECT_CALL(factory.Mock, CreateCustomFontCollection(NotNull(), 0, 0, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	FontCollectionLoaderFake ^loader = gcnew FontCollectionLoaderFake;
	SlimDX::Configuration::ThrowOnError = false;
	FontCollection ^collection = factory.Factory->CreateCustomFontCollection(loader, IntPtr(0), 0);
	AssertLastResultFailed();
	ASSERT_TRUE(collection == nullptr);
	delete loader;
}

FACTORY_TEST(CreateCustomRenderingParameters)
{
	MockedFactory factory;
	IDWriteRenderingParamsFake mockRenderingParams;
	EXPECT_CALL(factory.Mock, CreateCustomRenderingParams(1.1f, 2.2f, 3.3f, DWRITE_PIXEL_GEOMETRY_FLAT, DWRITE_RENDERING_MODE_ALIASED, NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<5>(&mockRenderingParams), Return(S_OK)));
	RenderingParameters ^params = factory.Factory->CreateCustomRenderingParameters(1.1f, 2.2f, 3.3f, PixelGeometry::Flat, RenderingMode::Aliased);
	AssertLastResultSucceeded();
	ASSERT_TRUE(params != nullptr);
	ASSERT_EQ(&mockRenderingParams, params->InternalPointer);
	delete params;
}

FACTORY_TEST(CreateCustomRenderingParametersFailureReturnsNullPtr)
{
	MockedFactory factory;
	IDWriteRenderingParamsFake mockRenderingParams;
	EXPECT_CALL(factory.Mock, CreateCustomRenderingParams(1.1f, 2.2f, 3.3f, DWRITE_PIXEL_GEOMETRY_FLAT, DWRITE_RENDERING_MODE_ALIASED, NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	RenderingParameters ^params = factory.Factory->CreateCustomRenderingParameters(1.1f, 2.2f, 3.3f, PixelGeometry::Flat, RenderingMode::Aliased);
	AssertLastResultFailed();
	ASSERT_TRUE(params == nullptr);
}

ref class FontFileLoaderFake : public IFontFileLoader
{
public:
	virtual FontFileStream ^CreateStreamFromKey(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize)
	{
		return nullptr;
	}
};

FACTORY_TEST(CreateCustomFontFileReference)
{
	MockedFactory factory;
	IDWriteFontFileFake mockFile;
	EXPECT_CALL(factory.Mock, CreateCustomFontFileReference(0, 0, NotNull(), NotNull()))
		.Times(1).WillOnce(DoAll(SetArgumentPointee<3>(&mockFile), Return(S_OK)));
	FontFileLoaderFake ^loader = gcnew FontFileLoaderFake;
	FontFile ^file = factory.Factory->CreateCustomFontFileReference(IntPtr(0), 0, loader);
	AssertLastResultSucceeded();
	ASSERT_TRUE(file != nullptr);
	ASSERT_EQ(&mockFile, file->InternalPointer);
	delete file;
	delete loader;
}

FACTORY_TEST(CreateCustomFontFileReferenceFailureReturnsNullPtr)
{
	MockedFactory factory;
	IDWriteFontFileFake mockFile;
	EXPECT_CALL(factory.Mock, CreateCustomFontFileReference(0, 0, NotNull(), NotNull()))
		.Times(1).WillOnce(Return(E_FAIL));
	SlimDX::Configuration::ThrowOnError = false;
	FontFileLoaderFake ^loader = gcnew FontFileLoaderFake;
	FontFile ^file = factory.Factory->CreateCustomFontFileReference(IntPtr(0), 0, loader);
	AssertLastResultFailed();
	ASSERT_TRUE(file == nullptr);
	delete loader;
}
