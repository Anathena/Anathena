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

#include <dwrite.h>

namespace SlimDX
{
	namespace DirectWrite
	{
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.

		/// <summary>
		/// Indicates the condition at the edges of inline objects or text used to determine line-breaking behavior.
		/// </summary>
		public enum class BreakCondition : System::Int32
		{
			Neutral = DWRITE_BREAK_CONDITION_NEUTRAL,
			CanBreak = DWRITE_BREAK_CONDITION_CAN_BREAK,
			MayNotBreak = DWRITE_BREAK_CONDITION_MAY_NOT_BREAK,
			MustBreak = DWRITE_BREAK_CONDITION_MUST_BREAK
		};

		/// <summary>
		/// Specifies the type of the DirectWrite factory object.
		/// </summary>
		public enum class FactoryType : System::Int32
		{
			Shared = DWRITE_FACTORY_TYPE_SHARED,
			Isolated = DWRITE_FACTORY_TYPE_ISOLATED
		};

		/// <summary>
		/// Indicates the direction of flow for placing lines of text in a paragraph.
		/// </summary>
		public enum class FlowDirection : System::Int32
		{
			TopToBottom = DWRITE_FLOW_DIRECTION_TOP_TO_BOTTOM
		};

		/// <summary>
		/// Indicates the file format of a font face.
		/// </summary>
		public enum class FontFaceType : System::Int32
		{
			Cff = DWRITE_FONT_FACE_TYPE_CFF,
			TrueType = DWRITE_FONT_FACE_TYPE_TRUETYPE,
			TrueTypeCollection = DWRITE_FONT_FACE_TYPE_TRUETYPE_COLLECTION,
			Type1 = DWRITE_FONT_FACE_TYPE_TYPE1,
			Vector = DWRITE_FONT_FACE_TYPE_VECTOR,
			Bitmap = DWRITE_FONT_FACE_TYPE_BITMAP,
			Unknown = DWRITE_FONT_FACE_TYPE_UNKNOWN
		};

		/// <summary>
		/// Indicates the typographic feature of text supplied by the font.
		/// </summary>
		public enum class FontFeatureTag : System::Int32
		{
			AlternativeFractions = DWRITE_FONT_FEATURE_TAG_ALTERNATIVE_FRACTIONS,
			PetiteCapitalsFromCapitals = DWRITE_FONT_FEATURE_TAG_PETITE_CAPITALS_FROM_CAPITALS,
			SmallCapitalsFromCapitals = DWRITE_FONT_FEATURE_TAG_SMALL_CAPITALS_FROM_CAPITALS,
			ContextualAlternates = DWRITE_FONT_FEATURE_TAG_CONTEXTUAL_ALTERNATES,
			CaseSensitiveForms = DWRITE_FONT_FEATURE_TAG_CASE_SENSITIVE_FORMS,
			GlyphComposition = DWRITE_FONT_FEATURE_TAG_GLYPH_COMPOSITION_DECOMPOSITION,
			ContextualLigatures = DWRITE_FONT_FEATURE_TAG_CONTEXTUAL_LIGATURES,
			CapitalSpacing = DWRITE_FONT_FEATURE_TAG_CAPITAL_SPACING,
			ContextualSwash = DWRITE_FONT_FEATURE_TAG_CONTEXTUAL_SWASH,
			CursivePositioning = DWRITE_FONT_FEATURE_TAG_CURSIVE_POSITIONING,
			Default = DWRITE_FONT_FEATURE_TAG_DEFAULT,
			DiscretionaryLigatures = DWRITE_FONT_FEATURE_TAG_DISCRETIONARY_LIGATURES,
			ExpertForms = DWRITE_FONT_FEATURE_TAG_EXPERT_FORMS,
			Fractions = DWRITE_FONT_FEATURE_TAG_FRACTIONS,
			FullWidth = DWRITE_FONT_FEATURE_TAG_FULL_WIDTH,
			HalfForms = DWRITE_FONT_FEATURE_TAG_HALF_FORMS,
			HalantForms = DWRITE_FONT_FEATURE_TAG_HALANT_FORMS,
			AlternateHalfWidth = DWRITE_FONT_FEATURE_TAG_ALTERNATE_HALF_WIDTH,
			HistoricalForms = DWRITE_FONT_FEATURE_TAG_HISTORICAL_FORMS,
			HorizontalKanaAlternates = DWRITE_FONT_FEATURE_TAG_HORIZONTAL_KANA_ALTERNATES,
			HistoricalLigatures = DWRITE_FONT_FEATURE_TAG_HISTORICAL_LIGATURES,
			HalfWidth = DWRITE_FONT_FEATURE_TAG_HALF_WIDTH,
			HojoKanjiForms = DWRITE_FONT_FEATURE_TAG_HOJO_KANJI_FORMS,
			JIS04Forms = DWRITE_FONT_FEATURE_TAG_JIS04_FORMS,
			JIS78Forms = DWRITE_FONT_FEATURE_TAG_JIS78_FORMS,
			JIS83Forms = DWRITE_FONT_FEATURE_TAG_JIS83_FORMS,
			JIS90Forms = DWRITE_FONT_FEATURE_TAG_JIS90_FORMS,
			Kerning = DWRITE_FONT_FEATURE_TAG_KERNING,
			StandardLigatures = DWRITE_FONT_FEATURE_TAG_STANDARD_LIGATURES,
			LiningFigures = DWRITE_FONT_FEATURE_TAG_LINING_FIGURES,
			LocalizedForms = DWRITE_FONT_FEATURE_TAG_LOCALIZED_FORMS,
			MarkPositioning = DWRITE_FONT_FEATURE_TAG_MARK_POSITIONING,
			MathematicalGreek = DWRITE_FONT_FEATURE_TAG_MATHEMATICAL_GREEK,
			MarkToMarkPositioning = DWRITE_FONT_FEATURE_TAG_MARK_TO_MARK_POSITIONING,
			AlternateAnnotationForms = DWRITE_FONT_FEATURE_TAG_ALTERNATE_ANNOTATION_FORMS,
			NlcKanjiForms = DWRITE_FONT_FEATURE_TAG_NLC_KANJI_FORMS,
			OldStyleFigures = DWRITE_FONT_FEATURE_TAG_OLD_STYLE_FIGURES,
			Ordinals = DWRITE_FONT_FEATURE_TAG_ORDINALS,
			ProportionalAlternateWidth = DWRITE_FONT_FEATURE_TAG_PROPORTIONAL_ALTERNATE_WIDTH,
			PetiteCapitals = DWRITE_FONT_FEATURE_TAG_PETITE_CAPITALS,
			ProportionalFigures = DWRITE_FONT_FEATURE_TAG_PROPORTIONAL_FIGURES,
			ProportionalWidths = DWRITE_FONT_FEATURE_TAG_PROPORTIONAL_WIDTHS,
			QuarterWidths = DWRITE_FONT_FEATURE_TAG_QUARTER_WIDTHS,
			RequiredLigatures = DWRITE_FONT_FEATURE_TAG_REQUIRED_LIGATURES,
			RubyNotationForms = DWRITE_FONT_FEATURE_TAG_RUBY_NOTATION_FORMS,
			StylisticAlternates = DWRITE_FONT_FEATURE_TAG_STYLISTIC_ALTERNATES,
			ScientificInferiors = DWRITE_FONT_FEATURE_TAG_SCIENTIFIC_INFERIORS,
			SmallCapitals = DWRITE_FONT_FEATURE_TAG_SMALL_CAPITALS,
			SimplifiedForms = DWRITE_FONT_FEATURE_TAG_SIMPLIFIED_FORMS,
			StylisticSet1 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_1,
			StylisticSet2 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_2,
			StylisticSet3 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_3,
			StylisticSet4 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_4,
			StylisticSet5 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_5,
			StylisticSet6 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_6,
			StylisticSet7 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_7,
			StylisticSet8 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_8,
			StylisticSet9 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_9,
			StylisticSet10 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_10,
			StylisticSet11 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_11,
			StylisticSet12 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_12,
			StylisticSet13 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_13,
			StylisticSet14 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_14,
			StylisticSet15 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_15,
			StylisticSet16 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_16,
			StylisticSet17 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_17,
			StylisticSet18 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_18,
			StylisticSet19 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_19,
			StylisticSet20 = DWRITE_FONT_FEATURE_TAG_STYLISTIC_SET_20,
			Subscript = DWRITE_FONT_FEATURE_TAG_SUBSCRIPT,
			Superscript = DWRITE_FONT_FEATURE_TAG_SUPERSCRIPT,
			Swash = DWRITE_FONT_FEATURE_TAG_SWASH,
			Titling = DWRITE_FONT_FEATURE_TAG_TITLING,
			TraditionalNameForms = DWRITE_FONT_FEATURE_TAG_TRADITIONAL_NAME_FORMS,
			TabularFigures = DWRITE_FONT_FEATURE_TAG_TABULAR_FIGURES,
			TraditionalForms = DWRITE_FONT_FEATURE_TAG_TRADITIONAL_FORMS,
			ThirdWidths = DWRITE_FONT_FEATURE_TAG_THIRD_WIDTHS,
			Unicase = DWRITE_FONT_FEATURE_TAG_UNICASE,
			SlashedZero = DWRITE_FONT_FEATURE_TAG_SLASHED_ZERO
		};

		/// <summary>
		/// The type of font represented by a single font file.
		/// </summary>
		public enum class FontFileType : System::Int32
		{
			Unknown = DWRITE_FONT_FILE_TYPE_UNKNOWN,
			Cff = DWRITE_FONT_FILE_TYPE_CFF,
			TrueType = DWRITE_FONT_FILE_TYPE_TRUETYPE,
			TrueTypeCollection = DWRITE_FONT_FILE_TYPE_TRUETYPE_COLLECTION,
			Type1Pfm = DWRITE_FONT_FILE_TYPE_TYPE1_PFM,
			Type1Pfb = DWRITE_FONT_FILE_TYPE_TYPE1_PFB,
			Vector = DWRITE_FONT_FILE_TYPE_VECTOR,
			Bitmap = DWRITE_FONT_FILE_TYPE_BITMAP,
		};

		/// <summary>
		/// Specifies algorithmic style simulations to be applied to the font face.
		/// </summary>
		[System::Flags]
		public enum class FontSimulations : System::Int32
		{
			None = DWRITE_FONT_SIMULATIONS_NONE,
			Bold = DWRITE_FONT_SIMULATIONS_BOLD,
			Oblique = DWRITE_FONT_SIMULATIONS_OBLIQUE,
		};

		/// <summary>
		/// Represents the degree to which a font has been stretched compared to a font's normal aspect ratio.
		/// </summary>
		public enum class FontStretch : System::Int32
		{
			Undefined =  DWRITE_FONT_STRETCH_UNDEFINED,
			UltraCondensed =  DWRITE_FONT_STRETCH_ULTRA_CONDENSED,
			ExtraCondensed =  DWRITE_FONT_STRETCH_EXTRA_CONDENSED,
			Condensed =  DWRITE_FONT_STRETCH_CONDENSED,
			SemiCondensed =  DWRITE_FONT_STRETCH_SEMI_CONDENSED,
			Normal =  DWRITE_FONT_STRETCH_NORMAL,
			Medium = DWRITE_FONT_STRETCH_MEDIUM,
			SemiExpanded =  DWRITE_FONT_STRETCH_SEMI_EXPANDED,
			Expanded =  DWRITE_FONT_STRETCH_EXPANDED,
			ExtraExpanded =  DWRITE_FONT_STRETCH_EXTRA_EXPANDED,
			UltraExpanded =  DWRITE_FONT_STRETCH_ULTRA_EXPANDED
		};

		/// <summary>
		/// Represents the style of a font face as normal, italic, or oblique.
		/// </summary>
		public enum class FontStyle : System::Int32
		{
			Normal = DWRITE_FONT_STYLE_NORMAL,
			Oblique = DWRITE_FONT_STYLE_OBLIQUE,
			Italic = DWRITE_FONT_STYLE_ITALIC
		};

		/// <summary>
		/// Describes common values for degree of blackness, or thickness of strokes of characters in a font.
		/// </summary>
		public enum class FontWeight : System::Int32
		{
			Thin = DWRITE_FONT_WEIGHT_THIN,
			ExtraLight = DWRITE_FONT_WEIGHT_EXTRA_LIGHT,
			UltraLight = DWRITE_FONT_WEIGHT_ULTRA_LIGHT,
			Light = DWRITE_FONT_WEIGHT_LIGHT,
			Normal = DWRITE_FONT_WEIGHT_NORMAL,
			Regular = DWRITE_FONT_WEIGHT_REGULAR,
			Medium = DWRITE_FONT_WEIGHT_MEDIUM,
			DemiBold = DWRITE_FONT_WEIGHT_DEMI_BOLD,
			SemiBold = DWRITE_FONT_WEIGHT_SEMI_BOLD,
			Bold = DWRITE_FONT_WEIGHT_BOLD,
			ExtraBold = DWRITE_FONT_WEIGHT_EXTRA_BOLD,
			UltraBold = DWRITE_FONT_WEIGHT_ULTRA_BOLD,
			Black = DWRITE_FONT_WEIGHT_BLACK,
			Heavy = DWRITE_FONT_WEIGHT_HEAVY,
			ExtraBlack = DWRITE_FONT_WEIGHT_EXTRA_BLACK,
			UltraBlack = DWRITE_FONT_WEIGHT_ULTRA_BLACK
		};

		/// <summary>
		/// The informational string enumeration which identifies a string embedded in a font file.
		/// </summary>
		public enum class InformationalStringId : System::Int32
		{
			None = DWRITE_INFORMATIONAL_STRING_NONE,
			CopyrightNotice = DWRITE_INFORMATIONAL_STRING_COPYRIGHT_NOTICE,
			VersionStrings = DWRITE_INFORMATIONAL_STRING_VERSION_STRINGS,
			Trademark = DWRITE_INFORMATIONAL_STRING_TRADEMARK,
			Manufacturer = DWRITE_INFORMATIONAL_STRING_MANUFACTURER,
			Designer = DWRITE_INFORMATIONAL_STRING_DESIGNER,
			DesignerUrl = DWRITE_INFORMATIONAL_STRING_DESIGNER_URL,
			Description = DWRITE_INFORMATIONAL_STRING_DESCRIPTION,
			FontVendorUrl = DWRITE_INFORMATIONAL_STRING_FONT_VENDOR_URL,
			LicenseDescription = DWRITE_INFORMATIONAL_STRING_LICENSE_DESCRIPTION,
			LicenseInfoUrl = DWRITE_INFORMATIONAL_STRING_LICENSE_INFO_URL,
			Win32FamilyNames = DWRITE_INFORMATIONAL_STRING_WIN32_FAMILY_NAMES,
			Win32SubfamilyNames = DWRITE_INFORMATIONAL_STRING_WIN32_SUBFAMILY_NAMES,
			PreferredFamilyNames = DWRITE_INFORMATIONAL_STRING_PREFERRED_FAMILY_NAMES,
			PreferredSubfamilyNames = DWRITE_INFORMATIONAL_STRING_PREFERRED_SUBFAMILY_NAMES,
			SampleText = DWRITE_INFORMATIONAL_STRING_SAMPLE_TEXT 
		};

		/// <summary>
		/// The method used for line spacing in a text layout.
		/// </summary>
		public enum class LineSpacingMethod : System::Int32
		{
			Default = DWRITE_LINE_SPACING_METHOD_DEFAULT,
			Uniform = DWRITE_LINE_SPACING_METHOD_UNIFORM 
		};

		/// <summary>
		/// Specifies the measuring method used for text layout.
		/// </summary>
		public enum class MeasuringMode : System::Int32
		{
			Natural = DWRITE_MEASURING_MODE_NATURAL,
			GdiClassic = DWRITE_MEASURING_MODE_GDI_CLASSIC,
			GdiNatural = DWRITE_MEASURING_MODE_GDI_NATURAL
		};

		/// <summary>
		/// Specifies how to apply number substitution on digits and related punctuation.
		/// </summary>
		public enum class NumberSubstitutionMethod : System::Int32
		{
			FromCulture = DWRITE_NUMBER_SUBSTITUTION_METHOD_FROM_CULTURE,
			Contextual = DWRITE_NUMBER_SUBSTITUTION_METHOD_CONTEXTUAL,
			None = DWRITE_NUMBER_SUBSTITUTION_METHOD_NONE,
			National = DWRITE_NUMBER_SUBSTITUTION_METHOD_NATIONAL,
			Traditional = DWRITE_NUMBER_SUBSTITUTION_METHOD_TRADITIONAL 
		};

		/// <summary>
		/// Specifies alignment of paragraph text along the flow direction axis, relative to the flow's beginning and trailing edge of the layout box.
		/// </summary>
		public enum class ParagraphAlignment : System::Int32
		{
			Near = DWRITE_PARAGRAPH_ALIGNMENT_NEAR,
			Far = DWRITE_PARAGRAPH_ALIGNMENT_FAR,
			Center = DWRITE_PARAGRAPH_ALIGNMENT_CENTER
		};

		/// <summary>
		/// Represents the internal structure of a device pixel (that is the physical arrangement of red, green,
		/// and blue color components) that is assumed for purposes of rendering text. 
		/// </summary>
		public enum class PixelGeometry : System::Int32
		{
			Flat = DWRITE_PIXEL_GEOMETRY_FLAT,
			Rgb = DWRITE_PIXEL_GEOMETRY_RGB,
			Bgr = DWRITE_PIXEL_GEOMETRY_BGR 
		};

		/// <summary>
		/// Specifies the direction in which reading progresses. 
		/// </summary>
		public enum class ReadingDirection : System::Int32
		{
			LeftToRight = DWRITE_READING_DIRECTION_LEFT_TO_RIGHT,
			RightToLeft = DWRITE_READING_DIRECTION_RIGHT_TO_LEFT 
		};

		/// <summary>
		/// Represents a method of rendering glyphs. 
		/// </summary>
		public enum class RenderingMode : System::Int32
		{
			Default = DWRITE_RENDERING_MODE_DEFAULT,
			Aliased = DWRITE_RENDERING_MODE_ALIASED,
			ClearTypeGdiClassic = DWRITE_RENDERING_MODE_CLEARTYPE_GDI_CLASSIC,
			ClearTypeGdiNatural = DWRITE_RENDERING_MODE_CLEARTYPE_GDI_NATURAL,
			ClearTypeNatural = DWRITE_RENDERING_MODE_CLEARTYPE_NATURAL,
			ClearTypeNaturalSymmetric = DWRITE_RENDERING_MODE_CLEARTYPE_NATURAL_SYMMETRIC,
			Outline = DWRITE_RENDERING_MODE_OUTLINE 
		};

		/// <summary>
		/// Indicates additional shaping requirements for text.
		/// </summary>
		public enum class ScriptShapes : System::Int32
		{
			Default = DWRITE_SCRIPT_SHAPES_DEFAULT,
			NoVisual = DWRITE_SCRIPT_SHAPES_NO_VISUAL 
		};

		/// <summary>
		/// Specifies alignment of paragraph text along the reading direction axis relative to the leading and trailing edge of the layout box.
		/// </summary>
		public enum class TextAlignment : System::Int32
		{
			Leading = DWRITE_TEXT_ALIGNMENT_LEADING,
			Trailing = DWRITE_TEXT_ALIGNMENT_TRAILING,
			Center = DWRITE_TEXT_ALIGNMENT_CENTER
		};

		public enum class TextureType : System::Int32
		{
			Aliased1x1 = DWRITE_TEXTURE_ALIASED_1x1,
			ClearType3x1 = DWRITE_TEXTURE_CLEARTYPE_3x1
		};

		/// <summary>
		/// Specifies the text granularity used to trim text overflowing the layout box.
		/// </summary>
		public enum class TrimmingGranularity : System::Int32
		{
			None = DWRITE_TRIMMING_GRANULARITY_NONE,
			Character = DWRITE_TRIMMING_GRANULARITY_CHARACTER,
			Word = DWRITE_TRIMMING_GRANULARITY_WORD 
		};

		/// <summary>
		/// Specifies the word wrapping to be used in a particular multiline paragraph.
		/// </summary>
		public enum class WordWrapping : System::Int32
		{
			Wrap = DWRITE_WORD_WRAPPING_WRAP,
			NoWrap = DWRITE_WORD_WRAPPING_NO_WRAP 
		};
	}
}
