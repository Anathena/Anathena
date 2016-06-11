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
#pragma once

extern const IID IID_IDWriteTextAnalyzer;

#include "../ComObject.h"

namespace SlimDX
{
	namespace DirectWrite
	{
		ref class TextAnalysisSink;
		ref class TextAnalysisSource;

		/// <summary>
		/// Analyzes various text properties for complex script processing.
		/// </summary>
		public ref class TextAnalyzer : public ComObject
		{
			COMOBJECT(IDWriteTextAnalyzer, TextAnalyzer);

		public:
			/// <summary>
			/// Analyzes a text range for script boundaries, reading text attributes
			/// from the source and reporting the Unicode script ID to the sink 
			/// callback SetScript.
			/// </summary>
			/// <param name="analysisSource">Source object to analyze.</param>
			/// <param name="textPosition">Starting position within the source object.</param>
			/// <param name="textLength">Length to analyze.</param>
			/// <param name="analysisSink">Callback object.</param>
			/// <returns>
			/// Standard Result error code.
			/// </returns>
			Result AnalyzeScript(TextAnalysisSource ^analysisSource, int textPosition, int textLength, TextAnalysisSink ^analysisSink);

			/// <summary>
			/// Analyzes a text range for script directionality, reading attributes
			/// from the source and reporting levels to the sink callback SetBidiLevel.
			/// </summary>
			/// <param name="analysisSource">Source object to analyze.</param>
			/// <param name="textPosition">Starting position within the source object.</param>
			/// <param name="textLength">Length to analyze.</param>
			/// <param name="analysisSink">Callback object.</param>
			/// <returns>7
			/// Standard Result error code.
			/// </returns>
			/// <remarks>
			/// While the function can handle multiple paragraphs, the text range
			/// should not arbitrarily split the middle of paragraphs. Otherwise the
			/// returned levels may be wrong, since the Bidi algorithm is meant to
			/// apply to the paragraph as a whole.
			/// </remarks>
			/// <remarks>
			/// Embedded control codes (LRE/LRO/RLE/RLO/PDF) are taken into account.
			/// </remarks>
			Result AnalyzeBidi(TextAnalysisSource ^analysisSource, int textPosition, int textLength, TextAnalysisSink ^analysisSink);

#if 0
			/// <summary>
			/// Analyzes a text range for spans where number substitution is applicable,
			/// reading attributes from the source and reporting substitutable ranges
			/// to the sink callback SetNumberSubstitution.
			/// </summary>
			/// <param name="analysisSource">Source object to analyze.</param>
			/// <param name="textPosition">Starting position within the source object.</param>
			/// <param name="textLength">Length to analyze.</param>
			/// <param name="analysisSink">Callback object.</param>
			/// <returns>
			/// Standard Result error code.
			/// </returns>
			/// <remarks>
			/// While the function can handle multiple ranges of differing number
			/// substitutions, the text ranges should not arbitrarily split the
			/// middle of numbers. Otherwise it will treat the numbers separately
			/// and will not translate any intervening punctuation.
			/// </remarks>
			/// <remarks>
			/// Embedded control codes (LRE/LRO/RLE/RLO/PDF) are taken into account.
			/// </remarks>
			Result AnalyzeNumberSubstitution(TextAnalysisSource ^analysisSource, int textPosition, int textLength, TextAnalysisSink ^analysisSink);

			/// <summary>
			/// Analyzes a text range for potential breakpoint opportunities, reading
			/// attributes from the source and reporting breakpoint opportunities to
			/// the sink callback SetLineBreakpoints.
			/// </summary>
			/// <param name="analysisSource">Source object to analyze.</param>
			/// <param name="textPosition">Starting position within the source object.</param>
			/// <param name="textLength">Length to analyze.</param>
			/// <param name="analysisSink">Callback object.</param>
			/// <returns>
			/// Standard Result error code.
			/// </returns>
			/// <remarks>
			/// While the function can handle multiple paragraphs, the text range
			/// should not arbitrarily split the middle of paragraphs, unless the
			/// given text span is considered a whole unit. Otherwise the
			/// returned properties for the first and last characters will
			/// inappropriately allow breaks.
			/// </remarks>
			/// <remarks>
			/// Special cases include the first, last, and surrogate characters. Any
			/// text span is treated as if adjacent to inline objects on either side.
			/// So the rules with contingent-break opportunities are used, where the 
			/// edge between text and inline objects is always treated as a potential
			/// break opportunity, dependent on any overriding rules of the adjacent
			/// objects to prohibit or force the break (see Unicode TR #14).
			/// Surrogate pairs never break between.
			/// </remarks>
			Result AnalyzeLineBreakpoints(TextAnalysisSource ^analysisSource, int textPosition, int textLength, TextAnalysisSink ^analysisSink);

			/// <summary>
			/// Parses the input text string and maps it to the set of glyphs and associated glyph data
			/// according to the font and the writing system's rendering rules.
			/// </summary>
			/// <param name="textString">The string to convert to glyphs.</param>
			/// <param name="textLength">The length of textString.</param>
			/// <param name="fontFace">The font face to get glyphs from.</param>
			/// <param name="isSideways">Set to true if the text is intended to be
			/// drawn vertically.</param>
			/// <param name="isRightToLeft">Set to TRUE for right-to-left text.</param>
			/// <param name="scriptAnalysis">Script analysis result from AnalyzeScript.</param>
			/// <param name="localeName">The locale to use when selecting glyphs.
			/// e.g. the same character may map to different glyphs for ja-jp vs zh-chs.
			/// If this is NULL then the default mapping based on the script is used.</param>
			/// <param name="numberSubstitution">Optional number substitution which
			/// selects the appropriate glyphs for digits and related numeric characters,
			/// depending on the results obtained from AnalyzeNumberSubstitution. Passing
			/// null indicates that no substitution is needed and that the digits should
			/// receive nominal glyphs.</param>
			/// <param name="features">An array of pointers to the sets of typographic 
			/// features to use in each feature range.</param>
			/// <param name="featureRangeLengths">The length of each feature range, in characters.  
			/// The sum of all lengths should be equal to textLength.</param>
			/// <param name="featureRanges">The number of feature ranges.</param>
			/// <param name="maxGlyphCount">The maximum number of glyphs that can be
			/// returned.</param>
			/// <param name="clusterMap">The mapping from character ranges to glyph 
			/// ranges.</param>
			/// <param name="textProps">Per-character output properties.</param>
			/// <param name="glyphIndices">Output glyph indices.</param>
			/// <param name="glyphProps">Per-glyph output properties.</param>
			/// <param name="actualGlyphCount">The actual number of glyphs returned if
			/// the call succeeds.</param>
			/// <returns>
			/// Standard HRESULT error code.
			/// </returns>
			/// <remarks>
			/// Note that the mapping from characters to glyphs is, in general, many-
			/// to-many.  The recommended estimate for the per-glyph output buffers is
			/// (3 * textLength / 2 + 16).  This is not guaranteed to be sufficient.
			///
			/// The value of the actualGlyphCount parameter is only valid if the call
			/// succeeds.  In the event that maxGlyphCount is not big enough
			/// E_NOT_SUFFICIENT_BUFFER, which is equivalent to HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER),
			/// will be returned.  The application should allocate a larger buffer and try again.
			/// </remarks>
			// [Out] int %actualGlyphCount
			int GetGlyphs(
				String ^textString,
				FontFace ^fontFace,
				bool isSideways,
				bool isRightToLeft,
				ScriptAnalysis scriptAnalysis,
				String ^localeName,
				NumberSubstitution ^numberSubstitution,
				array<array<FontFeature> ^> ^features,
				array<int> ^featureRangeLengths,
				UINT32 maxGlyphCount,
				array<int> ^clusterMap,
				array<ShapingTextProperties> ^textProps,
				array<int> ^glyphIndices,
				array<ShapingGlyphProperties> ^glyphProps);

			/// <summary>
			/// Place glyphs output from the GetGlyphs method according to the font 
			/// and the writing system's rendering rules.
			/// </summary>
			/// <param name="textString">The original string the glyphs came from.</param>
			/// <param name="clusterMap">The mapping from character ranges to glyph 
			/// ranges. Returned by GetGlyphs.</param>
			/// <param name="textProps">Per-character properties. Returned by 
			/// GetGlyphs.</param>
			/// <param name="textLength">The length of textString.</param>
			/// <param name="glyphIndices">Glyph indices. See GetGlyphs</param>
			/// <param name="glyphProps">Per-glyph properties. See GetGlyphs</param>
			/// <param name="glyphCount">The number of glyphs.</param>
			/// <param name="fontFace">The font face the glyphs came from.</param>
			/// <param name="fontEmSize">Logical font size in DIP's.</param>
			/// <param name="isSideways">Set to true if the text is intended to be
			/// drawn vertically.</param>
			/// <param name="isRightToLeft">Set to TRUE for right-to-left text.</param>
			/// <param name="scriptAnalysis">Script analysis result from AnalyzeScript.</param>
			/// <param name="localeName">The locale to use when selecting glyphs.
			/// e.g. the same character may map to different glyphs for ja-jp vs zh-chs.
			/// If this is NULL then the default mapping based on the script is used.</param>
			/// <param name="features">An array of pointers to the sets of typographic 
			/// features to use in each feature range.</param>
			/// <param name="featureRangeLengths">The length of each feature range, in characters.  
			/// The sum of all lengths should be equal to textLength.</param>
			/// <param name="featureRanges">The number of feature ranges.</param>
			/// <param name="glyphAdvances">The advance width of each glyph.</param>
			/// <param name="glyphOffsets">The offset of the origin of each glyph.</param>
			/// <returns>
			/// Standard HRESULT error code.
			/// </returns>
			STDMETHOD(GetGlyphPlacements)(
				__in_ecount(textLength) WCHAR const* textString,
				__in_ecount(textLength) UINT16 const* clusterMap,
				__in_ecount(textLength) DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
				UINT32 textLength,
				__in_ecount(glyphCount) UINT16 const* glyphIndices,
				__in_ecount(glyphCount) DWRITE_SHAPING_GLYPH_PROPERTIES const* glyphProps,
				UINT32 glyphCount,
				IDWriteFontFace * fontFace,
				FLOAT fontEmSize,
				BOOL isSideways,
				BOOL isRightToLeft,
				__in DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis,
				__in_z_opt WCHAR const* localeName,
				__in_ecount_opt(featureRanges) DWRITE_TYPOGRAPHIC_FEATURES const** features,
				__in_ecount_opt(featureRanges) UINT32 const* featureRangeLengths,
				UINT32 featureRanges,
				__out_ecount(glyphCount) FLOAT* glyphAdvances,
				__out_ecount(glyphCount) DWRITE_GLYPH_OFFSET* glyphOffsets
				) PURE;

			/// <summary>
			/// Place glyphs output from the GetGlyphs method according to the font 
			/// and the writing system's rendering rules.
			/// </summary>
			/// <param name="textString">The original string the glyphs came from.</param>
			/// <param name="clusterMap">The mapping from character ranges to glyph 
			/// ranges. Returned by GetGlyphs.</param>
			/// <param name="textProps">Per-character properties. Returned by 
			/// GetGlyphs.</param>
			/// <param name="textLength">The length of textString.</param>
			/// <param name="glyphIndices">Glyph indices. See GetGlyphs</param>
			/// <param name="glyphProps">Per-glyph properties. See GetGlyphs</param>
			/// <param name="glyphCount">The number of glyphs.</param>
			/// <param name="fontFace">The font face the glyphs came from.</param>
			/// <param name="fontEmSize">Logical font size in DIP's.</param>
			/// <param name="pixelsPerDip">Number of physical pixels per DIP. For example, if the DPI of the rendering surface is 96 this 
			/// value is 1.0f. If the DPI is 120, this value is 120.0f/96.</param>
			/// <param name="transform">Optional transform applied to the glyphs and their positions. This transform is applied after the
			/// scaling specified by the font size and pixelsPerDip.</param>
			/// <param name="useGdiNatural">
			/// When set to FALSE, the metrics are the same as the metrics of GDI aliased text.
			/// When set to TRUE, the metrics are the same as the metrics of text measured by GDI using a font
			/// created with CLEARTYPE_NATURAL_QUALITY.
			/// </param>
			/// <param name="isSideways">Set to true if the text is intended to be
			/// drawn vertically.</param>
			/// <param name="isRightToLeft">Set to TRUE for right-to-left text.</param>
			/// <param name="scriptAnalysis">Script analysis result from AnalyzeScript.</param>
			/// <param name="localeName">The locale to use when selecting glyphs.
			/// e.g. the same character may map to different glyphs for ja-jp vs zh-chs.
			/// If this is NULL then the default mapping based on the script is used.</param>
			/// <param name="features">An array of pointers to the sets of typographic 
			/// features to use in each feature range.</param>
			/// <param name="featureRangeLengths">The length of each feature range, in characters.  
			/// The sum of all lengths should be equal to textLength.</param>
			/// <param name="featureRanges">The number of feature ranges.</param>
			/// <param name="glyphAdvances">The advance width of each glyph.</param>
			/// <param name="glyphOffsets">The offset of the origin of each glyph.</param>
			/// <returns>
			/// Standard HRESULT error code.
			/// </returns>
			STDMETHOD(GetGdiCompatibleGlyphPlacements)(
				__in_ecount(textLength) WCHAR const* textString,
				__in_ecount(textLength) UINT16 const* clusterMap,
				__in_ecount(textLength) DWRITE_SHAPING_TEXT_PROPERTIES* textProps,
				UINT32 textLength,
				__in_ecount(glyphCount) UINT16 const* glyphIndices,
				__in_ecount(glyphCount) DWRITE_SHAPING_GLYPH_PROPERTIES const* glyphProps,
				UINT32 glyphCount,
				IDWriteFontFace * fontFace,
				FLOAT fontEmSize,
				FLOAT pixelsPerDip,
				__in_opt DWRITE_MATRIX const* transform,
				BOOL useGdiNatural,
				BOOL isSideways,
				BOOL isRightToLeft,
				__in DWRITE_SCRIPT_ANALYSIS const* scriptAnalysis,
				__in_z_opt WCHAR const* localeName,
				__in_ecount_opt(featureRanges) DWRITE_TYPOGRAPHIC_FEATURES const** features,
				__in_ecount_opt(featureRanges) UINT32 const* featureRangeLengths,
				UINT32 featureRanges,
				__out_ecount(glyphCount) FLOAT* glyphAdvances,
				__out_ecount(glyphCount) DWRITE_GLYPH_OFFSET* glyphOffsets
				) PURE;
#endif
		};
	}
}
