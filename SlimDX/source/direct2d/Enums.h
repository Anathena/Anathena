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

#include <d2d1.h>

namespace SlimDX
{
	namespace Direct2D
	{
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.

		public enum class AlphaMode : System::Int32
		{
			Unknown = D2D1_ALPHA_MODE_UNKNOWN,
			Premultiplied = D2D1_ALPHA_MODE_PREMULTIPLIED,
			Straight = D2D1_ALPHA_MODE_STRAIGHT,
			Ignore = D2D1_ALPHA_MODE_IGNORE
		};

		public enum class AntialiasMode : System::Int32
		{
			PerPrimitive = D2D1_ANTIALIAS_MODE_PER_PRIMITIVE,
			Aliased = D2D1_ANTIALIAS_MODE_ALIASED
		};

		public enum class ArcSize : System::Int32
		{
			Small = D2D1_ARC_SIZE_SMALL,
			Large = D2D1_ARC_SIZE_LARGE
		};

		public enum class CapStyle : System::Int32
		{
			Flat = D2D1_CAP_STYLE_FLAT,
			Square = D2D1_CAP_STYLE_SQUARE,
			Round = D2D1_CAP_STYLE_ROUND,
			Triangle = D2D1_CAP_STYLE_TRIANGLE
		};

		public enum class CombineMode : System::Int32
		{
			Union = D2D1_COMBINE_MODE_UNION,
			Intersect = D2D1_COMBINE_MODE_INTERSECT,
			Xor = D2D1_COMBINE_MODE_XOR,
			Exclude = D2D1_COMBINE_MODE_EXCLUDE
		};

		public enum class CompatibleRenderTargetOptions : System::Int32
		{
			None = D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS_NONE,
			GdiCompatible = D2D1_COMPATIBLE_RENDER_TARGET_OPTIONS_GDI_COMPATIBLE
		};

		public enum class DashStyle : System::Int32
		{
			Solid = D2D1_DASH_STYLE_SOLID,
			Dash = D2D1_DASH_STYLE_DASH,
			Dot = D2D1_DASH_STYLE_DOT,
			DashDot = D2D1_DASH_STYLE_DASH_DOT,
			DashDotDot = D2D1_DASH_STYLE_DASH_DOT_DOT,
			Custom = D2D1_DASH_STYLE_CUSTOM
		};

		public enum class DebugLevel : System::Int32
		{
			None = D2D1_DEBUG_LEVEL_NONE,
			Error = D2D1_DEBUG_LEVEL_ERROR,
			Warning = D2D1_DEBUG_LEVEL_WARNING,
			Information = D2D1_DEBUG_LEVEL_INFORMATION
		};

		public enum class DeviceContextInitializeMode : System::Int32
		{
			Copy = D2D1_DC_INITIALIZE_MODE_COPY,
			Clear = D2D1_DC_INITIALIZE_MODE_CLEAR
		};

		[System::Flags]
		public enum class DrawTextOptions : System::Int32
		{
			None = D2D1_DRAW_TEXT_OPTIONS_NONE,
			NoSnap = D2D1_DRAW_TEXT_OPTIONS_NO_SNAP,
			Clip = D2D1_DRAW_TEXT_OPTIONS_CLIP
		};

		public enum class ExtendMode : System::Int32
		{
			Clamp = D2D1_EXTEND_MODE_CLAMP,
			Wrap = D2D1_EXTEND_MODE_WRAP,
			Mirror = D2D1_EXTEND_MODE_MIRROR
		};

		public enum class FactoryType : System::Int32
		{
			SingleThreaded = D2D1_FACTORY_TYPE_SINGLE_THREADED,
			Multithreaded = D2D1_FACTORY_TYPE_MULTI_THREADED
		};

		public enum class FeatureLevel : System::Int32
		{
			Default = D2D1_FEATURE_LEVEL_DEFAULT,
			Direct3D9 = D2D1_FEATURE_LEVEL_9,
			Direct3D10 = D2D1_FEATURE_LEVEL_10
		};

		public enum class FigureBegin : System::Int32
		{
			Filled = D2D1_FIGURE_BEGIN_FILLED,
			Hollow = D2D1_FIGURE_BEGIN_HOLLOW
		};

		public enum class FigureEnd : System::Int32
		{
			Open = D2D1_FIGURE_END_OPEN,
			Closed = D2D1_FIGURE_END_CLOSED
		};

		public enum class FillMode : System::Int32
		{
			Alternate = D2D1_FILL_MODE_ALTERNATE,
			Winding = D2D1_FILL_MODE_WINDING 
		};

		public enum class Gamma : System::Int32
		{
			StandardRgb = D2D1_GAMMA_2_2,
			Linear = D2D1_GAMMA_1_0
		};

		public enum class GeometryRelation : System::Int32
		{
			Unknown = D2D1_GEOMETRY_RELATION_UNKNOWN,
			Disjoint = D2D1_GEOMETRY_RELATION_DISJOINT,
			IsContained = D2D1_GEOMETRY_RELATION_IS_CONTAINED,
			Contains = D2D1_GEOMETRY_RELATION_CONTAINS,
			Overlap = D2D1_GEOMETRY_RELATION_OVERLAP
		};

		public enum class InterpolationMode : System::Int32
		{
			NearestNeighbor = D2D1_BITMAP_INTERPOLATION_MODE_NEAREST_NEIGHBOR,
			Linear = D2D1_BITMAP_INTERPOLATION_MODE_LINEAR
		};

		[System::Flags]
		public enum class LayerOptions : System::Int32
		{
			None = D2D1_LAYER_OPTIONS_NONE,
			InitializeForClearType = D2D1_LAYER_OPTIONS_INITIALIZE_FOR_CLEARTYPE
		};

		public enum class LineJoin : System::Int32
		{
			Miter = D2D1_LINE_JOIN_MITER,
			Bevel = D2D1_LINE_JOIN_BEVEL,
			Round = D2D1_LINE_JOIN_ROUND,
			MiterOrBevel = D2D1_LINE_JOIN_MITER_OR_BEVEL
		};

		[System::Flags]
		public enum class OpacityMaskContent : System::Int32
		{
			Graphics = D2D1_OPACITY_MASK_CONTENT_GRAPHICS,
			NaturalText = D2D1_OPACITY_MASK_CONTENT_TEXT_NATURAL,
			GdiCompatibleText = D2D1_OPACITY_MASK_CONTENT_TEXT_GDI_COMPATIBLE
		};

		[System::Flags]
		public enum class PathSegment : System::Int32
		{
			None = D2D1_PATH_SEGMENT_NONE,
			ForceUnstroked = D2D1_PATH_SEGMENT_FORCE_UNSTROKED,
			ForceRoundLineJoine = D2D1_PATH_SEGMENT_FORCE_ROUND_LINE_JOIN
		};

		public enum class PresentOptions : System::Int32
		{
			None = D2D1_PRESENT_OPTIONS_NONE,
			RetainContents = D2D1_PRESENT_OPTIONS_RETAIN_CONTENTS,
			Immediately = D2D1_PRESENT_OPTIONS_IMMEDIATELY
		};

		public enum class RenderTargetType : System::Int32
		{
			Default = D2D1_RENDER_TARGET_TYPE_DEFAULT,
			Software = D2D1_RENDER_TARGET_TYPE_SOFTWARE,
			Hardware = D2D1_RENDER_TARGET_TYPE_HARDWARE
		};

		[System::Flags]
		public enum class RenderTargetUsage : System::Int32
		{
			None = D2D1_RENDER_TARGET_USAGE_NONE,
			ForceBitmapRemoting = D2D1_RENDER_TARGET_USAGE_FORCE_BITMAP_REMOTING,
			GdiCompatible = D2D1_RENDER_TARGET_USAGE_GDI_COMPATIBLE
		};

		public enum class SimplificationType : System::Int32
		{
			CubicsAndLines = D2D1_GEOMETRY_SIMPLIFICATION_OPTION_CUBICS_AND_LINES,
			Lines = D2D1_GEOMETRY_SIMPLIFICATION_OPTION_LINES
		};

		public enum class SweepDirection : System::Int32
		{
			Counterclockwise =  D2D1_SWEEP_DIRECTION_COUNTER_CLOCKWISE,
			Clockwise =  D2D1_SWEEP_DIRECTION_CLOCKWISE
		};

		public enum class TextAntialiasMode : System::Int32
		{
			Default = D2D1_TEXT_ANTIALIAS_MODE_DEFAULT,
			ClearType = D2D1_TEXT_ANTIALIAS_MODE_CLEARTYPE,
			Grayscale = D2D1_TEXT_ANTIALIAS_MODE_GRAYSCALE,
			Aliased = D2D1_TEXT_ANTIALIAS_MODE_ALIASED
		};
	}
}