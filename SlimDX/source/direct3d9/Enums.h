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

#include <windows.h>

#include "external/atir2vb.h"
#include "D3DXEnums.h"

namespace SlimDX
{
	namespace Direct3D9
	{
#ifdef XMLDOCS
		ref class Device;
#endif

		// NOTE: The enumerations defined in this file are in alphabetical order (haha, not anymore). When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.

		/// <summary>
		/// Defines stream sources for hardware instancing.
		/// </summary>
		/// <unmanaged>D3DSTREAMSOURCE</unmanaged>
		public enum class StreamSource : System::Int32
		{
			/// <summary>
			/// Indexed data.
			/// </summary>
			IndexedData,

			/// <summary>
			/// Instance data.
			/// </summary>
			InstanceData
		};

		/// <summary>
		/// Defines texture filtering modes for a texture stage.
		/// </summary>
		/// <unmanaged>D3DTEXTUREFILTERTYPE</unmanaged>
		public enum class TextureFilter : System::Int32
		{
			/// <summary>
			/// Mipmapping is disabled.
			/// </summary>
			None = D3DTEXF_NONE,

			/// <summary>
			/// Point filtering is used.
			/// </summary>
			Point = D3DTEXF_POINT,

			/// <summary>
			/// Bilinear interpolation is used.
			/// </summary>
			Linear = D3DTEXF_LINEAR,

			/// <summary>
			/// Anisotropic interpolation is used.
			/// </summary>
			Anisotropic = D3DTEXF_ANISOTROPIC,

			/// <summary>
			/// A 4 sample tent filter is used.
			/// </summary>
			PyramidalQuad = D3DTEXF_PYRAMIDALQUAD,

			/// <summary>
			/// A 4 sample Guassian filter is used.
			/// </summary>
			GaussianQuad = D3DTEXF_GAUSSIANQUAD,

			/// <summary>
			/// Convolution filter for monochrome textures. Available for Direct3D9Ex only.
			/// </summary>
			ConvolutionMono = D3DTEXF_CONVOLUTIONMONO
		};

		/// <summary>
		/// Defines stencil buffer operations.
		/// </summary>
		/// <unmanaged>D3DSTENCILOP</unmanaged>
		public enum class StencilOperation
		{
			/// <summary>
			/// Keep the current stencil value.
			/// </summary>
			Keep = D3DSTENCILOP_KEEP,

			/// <summary>
			/// Zero the stencil value.
			/// </summary>
			Zero = D3DSTENCILOP_ZERO,

			/// <summary>
			/// Replace the stencil value.
			/// </summary>
			Replace = D3DSTENCILOP_REPLACE,

			/// <summary>
			/// Increment and clamp the stencil value.
			/// </summary>
			IncrementSaturate = D3DSTENCILOP_INCRSAT,

			/// <summary>
			/// Decrement and clamp the stencil value.
			/// </summary>
			DecrementSaturate = D3DSTENCILOP_DECRSAT,

			/// <summary>
			/// Invert the stencil value.
			/// </summary>
			Invert = D3DSTENCILOP_INVERT,

			/// <summary>
			/// Increment the stencil value.
			/// </summary>
			Increment = D3DSTENCILOP_INCR,

			/// <summary>
			/// Decrement the stencil value.
			/// </summary>
			Decrement = D3DSTENCILOP_DECR
		};

		/// <summary>
		/// Defines swap effects.
		/// </summary>
		/// <unmanaged>D3DSWAPEFFECT</unmanaged>
		public enum class SwapEffect : System::Int32
		{
			/// <summary>
			/// Discards the data in the back buffers and render targets after a presentation.
			/// </summary>
			Discard = D3DSWAPEFFECT_DISCARD,

			/// <summary>
			/// Flips between multiple back buffers.
			/// </summary>
			Flip = D3DSWAPEFFECT_FLIP,

			/// <summary>
			/// Copies the data between back buffers.
			/// </summary>
			Copy = D3DSWAPEFFECT_COPY,

			/// <summary>
			/// Use a dedicated area of video memory that can be overlayed on the primary surface. No copy is performed
			/// when the overlay is displayed. The overlay operation is performed in hardware, without modifying
			/// the data in the primary surface. Available in Direct3D 9Ex on Windows 7 only.
			/// </summary>
			Overlay = D3DSWAPEFFECT_OVERLAY,

			/// <summary>
			/// Designates when an application is adopting flip mode, during which time an application's frame
			/// is passed instead of copied to the Desktop Window Manager(DWM) for composition when the application
			/// is presenting in windowed mode. Flip mode allows an application to more efficiently use memory
			/// bandwidth as well as enabling an application to take advantage of full-screen-present statistics.
			/// Flip mode does not affect full-screen behavior. Available in Direct3D 9Ex on Windows 7 only.
			/// </summary>
			FlipEx = D3DSWAPEFFECT_FLIPEX
		};

		/// <summary>
		/// Defines the texture-addressing modes.
		/// </summary>
		/// <unmanaged>D3DTEXTUREADDRESS</unmanaged>
		public enum class TextureAddress : System::Int32
		{
			/// <summary>
			/// Tile the texture at every integer junction.
			/// </summary>
			Wrap = D3DTADDRESS_WRAP,

			/// <summary>
			/// The texture is flipped at every integer junction.
			/// </summary>
			Mirror = D3DTADDRESS_MIRROR,

			/// <summary>
			/// Texture coordinates outside the range are clamped to the maximum values.
			/// </summary>
			Clamp = D3DTADDRESS_CLAMP,

			/// <summary>
			/// Texture coordinates outside the range are set to the border color.
			/// </summary>
			Border = D3DTADDRESS_BORDER,

			/// <summary>
			/// Similar to a combination of Wrap, Mirror, and Clamp.
			/// </summary>
			MirrorOnce = D3DTADDRESS_MIRRORONCE
		};

		/// <summary>
		/// Defines the possible shading modes.
		/// </summary>
		/// <unmanaged>D3DSHADEMODE</unmanaged>
		public enum class ShadeMode : System::Int32
		{
			/// <summary>
			/// Flat shading.
			/// </summary>
			Flat = D3DSHADE_FLAT,

			/// <summary>
			/// Gouraud shading.
			/// </summary>
			Gouraud = D3DSHADE_GOURAUD
		};

		/// <summary>
		/// Defines texture coordinate transformation values.
		/// </summary>
		/// <unmanaged>D3DTEXTURETRANSFORMFLAGS</unmanaged>
		public enum class TextureTransform : System::Int32
		{
			/// <summary>
			/// Texture coordinates are passed directly to the rasterizer.
			/// </summary>
			Disable = D3DTTFF_DISABLE,

			/// <summary>
			/// The rasterizer should expect 1D coordinates.
			/// </summary>
			Count1 = D3DTTFF_COUNT1,

			/// <summary>
			/// The rasterizer should expect 2D coordinates.
			/// </summary>
			Count2 = D3DTTFF_COUNT2,

			/// <summary>
			/// The rasterizer should expect 3D coordinates.
			/// </summary>
			Count3 = D3DTTFF_COUNT3,

			/// <summary>
			/// The rasterizer should expect 4D coordinates.
			/// </summary>
			Count4 = D3DTTFF_COUNT4,

			/// <summary>
			/// Forces all coordinates to be projected before being sent to the rasterizer.
			/// </summary>
			Projected = D3DTTFF_PROJECTED
		};

		/// <summary>
		/// Defines possible transformation states.
		/// </summary>
		/// <unmanaged>D3DTRANSFORMSTATETYPE</unmanaged>
		public enum class TransformState : System::Int32
		{
			/// <summary>
			/// Identifies the transformation matrix being set as the view transformation matrix.
			/// </summary>
			View = D3DTS_VIEW,

			/// <summary>
			/// Identifies the transformation matrix being set as the projection transformation matrix.
			/// </summary>
			Projection = D3DTS_PROJECTION,

			/// <summary>
			/// Identifies the transformation matrix being set as a world transformation matrix.
			/// </summary>
			World = D3DTS_WORLD,

			/// <summary>
			/// Identifies the transformation matrix being set as a world transformation matrix.
			/// </summary>
			World1 = D3DTS_WORLD1,

			/// <summary>
			/// Identifies the transformation matrix being set as a world transformation matrix.
			/// </summary>
			World2 = D3DTS_WORLD2,

			/// <summary>
			/// Identifies the transformation matrix being set as a world transformation matrix.
			/// </summary>
			World3 = D3DTS_WORLD3,

			/// <summary>
			/// Identifies the transformation matrix being set for the specified texture stage.
			/// </summary>
			Texture0 = D3DTS_TEXTURE0,

			/// <summary>
			/// Identifies the transformation matrix being set for the specified texture stage.
			/// </summary>
			Texture1 = D3DTS_TEXTURE1,

			/// <summary>
			/// Identifies the transformation matrix being set for the specified texture stage.
			/// </summary>
			Texture2 = D3DTS_TEXTURE2,

			/// <summary>
			/// Identifies the transformation matrix being set for the specified texture stage.
			/// </summary>
			Texture3 = D3DTS_TEXTURE3,

			/// <summary>
			/// Identifies the transformation matrix being set for the specified texture stage.
			/// </summary>
			Texture4 = D3DTS_TEXTURE4,

			/// <summary>
			/// Identifies the transformation matrix being set for the specified texture stage.
			/// </summary>
			Texture5 = D3DTS_TEXTURE5,

			/// <summary>
			/// Identifies the transformation matrix being set for the specified texture stage.
			/// </summary>
			Texture6 = D3DTS_TEXTURE6,

			/// <summary>
			/// Identifies the transformation matrix being set for the specified texture stage.
			/// </summary>
			Texture7 = D3DTS_TEXTURE7
		};

		/// <summary>
		/// Specifies wrapping options for texture coordinates.
		/// </summary>
		/// <unmanaged href="bb206256">D3DWRAPCOORD</unmanaged>
		[System::Flags]
		public enum class TextureWrapping : System::Int32
		{
			/// <summary>
			/// No wrapping is performed.
			/// </summary>
			None = 0,

			/// <summary>
			/// Wrap the 0 coordinate.
			/// </summary>
			WrapCoordinate0 = D3DWRAPCOORD_0,

			/// <summary>
			/// Wrap the 1 coordinate.
			/// </summary>
			WrapCoordinate1 = D3DWRAPCOORD_1,

			/// <summary>
			/// Wrap the 2 coordinate.
			/// </summary>
			WrapCoordinate2 = D3DWRAPCOORD_2,

			/// <summary>
			/// Wrap the 3 coordinate.
			/// </summary>
			WrapCoordinate3 = D3DWRAPCOORD_3,

			/// <summary>
			/// Wrap all coordinates.
			/// </summary>
			All = WrapCoordinate0 | WrapCoordinate1 | WrapCoordinate2 | WrapCoordinate3
		};

		/// <summary>
		/// Usage options that identify how resources are to be used.
		/// </summary>
		/// <unmanaged>D3DUSAGE</unmanaged>
		[System::Flags]
		public enum class Usage : System::Int32
		{
			/// <summary>
			/// No specified usage options.
			/// </summary>
			None = 0,

			/// <summary>
			/// The resource will automatically generate mipmaps.
			/// </summary>
			AutoGenerateMipMap = D3DUSAGE_AUTOGENMIPMAP,

			/// <summary>
			/// The resource will be a depth/stencil buffer.
			/// </summary>
			DepthStencil = D3DUSAGE_DEPTHSTENCIL,

			/// <summary>
			/// The resource will be a displacement map.
			/// </summary>
			DisplacementMap = D3DUSAGE_DMAP,

			/// <summary>
			/// Set to indicate that the vertex buffer content will never require clipping.
			/// </summary>
			DoNotClip = D3DUSAGE_DONOTCLIP,

			/// <summary>
			/// Indicates that the resource requires dynamic memory use.
			/// </summary>
			Dynamic = D3DUSAGE_DYNAMIC,

			/// <summary>
			/// Allow a shared surface created by a secure application to be opened by a non-secure 
			/// application that has the shared handle. Available for Direct3D9Ex only.
			/// </summary>
			NonSecure = D3DUSAGE_NONSECURE,

			/// <summary>
			/// Indicates that the vertex buffer is to be used for drawing N-patches.
			/// </summary>
			NPatches = D3DUSAGE_NPATCHES,

			/// <summary>
			/// Indicates that the buffer will be used to draw points.
			/// </summary>
			Points = D3DUSAGE_POINTS,

			/// <summary>
			/// The resource will be a render target.
			/// </summary>
			RenderTarget = D3DUSAGE_RENDERTARGET,

			/// <summary>
			/// Indicates that the vertex buffer is to be used for drawing high-order primitives.
			/// </summary>
			RTPatches = D3DUSAGE_RTPATCHES,

			/// <summary>
			/// Informs the system that the application will only ever write to the buffer.
			/// </summary>
			WriteOnly = D3DUSAGE_WRITEONLY,

			/// <summary>
			/// Indicates that vertex processing for this resource should be done in software.
			/// </summary>
			SoftwareProcessing = D3DUSAGE_SOFTWAREPROCESSING,

			/// <summary>
			/// Indicates that the resource will be used for composition. Available for Direct3D9Ex only.
			/// </summary>
			TextApi = D3DUSAGE_TEXTAPI,

			/// <summary>
			/// Query the resource abour a legacy bump map.
			/// </summary>
			QueryLegacyBumpMap = D3DUSAGE_QUERY_LEGACYBUMPMAP,

			/// <summary>
			/// Query the resource to verify if a texture supports gamma correction during a read operation.
			/// </summary>
			QuerySrgbRead = D3DUSAGE_QUERY_SRGBREAD,

			/// <summary>
			/// Query the resource format to see if it supports texture filters.
			/// </summary>
			QueryFilter = D3DUSAGE_QUERY_FILTER,

			/// <summary>
			/// Query the resource to verify if a texture supports gamma correction during a write operation.
			/// </summary>
			QuerySrgbWrite = D3DUSAGE_QUERY_SRGBWRITE,

			/// <summary>
			/// Query the resource to verify support for post pixel shader blending support.
			/// </summary>
			QueryPostPixelShaderBlending = D3DUSAGE_QUERY_POSTPIXELSHADER_BLENDING,

			/// <summary>
			/// Query the resource to verify support for vertex shader texture sampling.
			/// </summary>
			QueryVertexTexture = D3DUSAGE_QUERY_VERTEXTEXTURE,

			/// <summary>
			/// Query the resource to verify support for texture wrapping and mip-mapping.
			/// </summary>
			QueryWrapAndMip = D3DUSAGE_QUERY_WRAPANDMIP,

			/// <summary>
			/// The resource will be a Render-To-Vertex-Buffer (R2VB) target.
			/// </summary>
			R2VBTarget = RenderTarget | DisplacementMap,
		};

		/// <summary>
		/// Defines flags used to control the number or matrices that the system applies when performing
		/// multimatrix vertex blending.
		/// </summary>
		/// <unmanaged>D3DVERTEXBLENDFLAGS</unmanaged>
		public enum class VertexBlend : System::Int32
		{
			/// <summary>
			/// Disable vertex blending.
			/// </summary>
			Disable = D3DVBF_DISABLE,

			/// <summary>
			/// Enable vertex blending between two matrices.
			/// </summary>
			Weights1 = D3DVBF_1WEIGHTS,

			/// <summary>
			/// Enable vertex blending between three matrices.
			/// </summary>
			Weights2 = D3DVBF_2WEIGHTS,

			/// <summary>
			/// Enable vertex blending between four matrices.
			/// </summary>
			Weights3 = D3DVBF_3WEIGHTS,

			/// <summary>
			/// Enable vertex blending between one matrix and a weight of 1.0.
			/// </summary>
			Weights0 = D3DVBF_0WEIGHTS,

			/// <summary>
			/// Vertex blending is performed using vertex tweening.
			/// </summary>
			Tweening = D3DVBF_TWEENING
		};

		/// <summary>
		/// Specifies codes that can be used to define Flexible-Vertex-Formats (FVF).
		/// </summary>
		/// <unmanaged>D3DFVF</unmanaged>
		[System::Flags]
		public enum class VertexFormat : System::Int32
		{
			/// <summary>
			/// No vertex format defined.
			/// </summary>
			None = 0,

			/***/

			/// <summary>
			/// Vertex format includes a vertex normal vector.
			/// </summary>
			Normal = D3DFVF_NORMAL,

			/// <summary>
			/// Vertex format contains a point size.
			/// </summary>
			PointSize = D3DFVF_PSIZE,

			/// <summary>
			/// Vertex format includes a diffuse color component.
			/// </summary>
			Diffuse = D3DFVF_DIFFUSE,

			/// <summary>
			/// Vertex format includes a specular color component.
			/// </summary>
			Specular = D3DFVF_SPECULAR,

			/// <summary>
			/// Vertex format includes the position of an untransformed vertex.
			/// </summary>
			Position = D3DFVF_XYZ,

			/// <summary>
			/// Vertex format includes the position of a transformed vertex.
			/// </summary>
			PositionRhw = D3DFVF_XYZRHW,

			/// <summary>
			/// Vertex format contains position and weighting values for multimatrix blending operations.
			/// </summary>
			PositionBlend1 = D3DFVF_XYZB1,

			/// <summary>
			/// Vertex format contains position and weighting values for multimatrix blending operations.
			/// </summary>
			PositionBlend2 = D3DFVF_XYZB2,

			/// <summary>
			/// Vertex format contains position and weighting values for multimatrix blending operations.
			/// </summary>
			PositionBlend3 = D3DFVF_XYZB3,

			/// <summary>
			/// Vertex format contains position and weighting values for multimatrix blending operations.
			/// </summary>
			PositionBlend4 = D3DFVF_XYZB4,

			/// <summary>
			/// Vertex format contains position and weighting values for multimatrix blending operations.
			/// </summary>
			PositionBlend5 = D3DFVF_XYZB5,

			/// <summary>
			/// Vertex format contains transformed and clipped data.
			/// </summary>
			PositionW = D3DFVF_XYZW,

			/// <summary>
			/// Vertex format contains a position and a normal.
			/// </summary>
			PositionNormal = Position | Normal,

			/***/

			/// <summary>
			/// Vertex format contains no texture coordinate sets.
			/// </summary>
			Texture0 = D3DFVF_TEX0,

			/// <summary>
			/// Vertex format contains 1 texture coordinate set.
			/// </summary>
			Texture1 = D3DFVF_TEX1,

			/// <summary>
			/// Vertex format contains 2 texture coordinate sets.
			/// </summary>
			Texture2 = D3DFVF_TEX2,

			/// <summary>
			/// Vertex format contains 3 texture coordinate sets.
			/// </summary>
			Texture3 = D3DFVF_TEX3,

			/// <summary>
			/// Vertex format contains 4 texture coordinate sets.
			/// </summary>
			Texture4 = D3DFVF_TEX4,

			/// <summary>
			/// Vertex format contains 5 texture coordinate sets.
			/// </summary>
			Texture5 = D3DFVF_TEX5,

			/// <summary>
			/// Vertex format contains 6 texture coordinate sets.
			/// </summary>
			Texture6 = D3DFVF_TEX6,

			/// <summary>
			/// Vertex format contains 7 texture coordinate sets.
			/// </summary>
			Texture7 = D3DFVF_TEX7,

			/// <summary>
			/// Vertex format contains 8 texture coordinate sets.
			/// </summary>
			Texture8 = D3DFVF_TEX8,

			/***/

			/// <summary>
			/// Mask for position bits.
			/// </summary>
			PositionMask = D3DFVF_POSITION_MASK,

			/// <summary>
			/// Mask for texture flag bits.
			/// </summary>
			TextureCountMask = D3DFVF_TEXCOUNT_MASK,

			/***/

			/// <summary>
			/// The number of bits by which to shift an integer value that identifies the number of texture 
			/// coordinates for a vertex.
			/// </summary>
			TextureCountShift = D3DFVF_TEXCOUNT_SHIFT,

			/// <summary>
			/// The last beta field in the vertex position data will be of type UByte4. The data in the beta 
			/// fields are used with matrix palette skinning to specify matrix indices.
			/// </summary>
			LastBetaUByte4 = D3DFVF_LASTBETA_UBYTE4,

			/// <summary>
			/// The last beta field in the vertex position data will be of type Color. The data in the beta 
			/// fields are used with matrix palette skinning to specify matrix indices.
			/// </summary>
			LastBetaColor = D3DFVF_LASTBETA_D3DCOLOR
		};

		/// <summary>
		/// Specifies the flexible vertex format capabilities of the device.
		/// </summary>
		/// <unmanaged HREF="bb172513">D3DFVFCAPS</unmanaged>
		[System::Flags]
		public enum class VertexFormatCaps : System::Int32
		{
			/// <summary>
			/// Masks the low WORD of FVFCaps. These bits, cast to the WORD data type, describe the total 
			/// number of texture coordinate sets that the device can simultaneously use for multiple texture 
			/// blending.
			/// </summary>
			TextureCoordCountMask = D3DFVFCAPS_TEXCOORDCOUNTMASK,

			/// <summary>
			/// It is preferable that vertex elements not be stripped. That is, if the vertex format contains 
			/// elements that are not used with the current render states, there is no need to regenerate the 
			/// vertices. If this capability flag is not present, stripping extraneous elements from the vertex 
			/// format provides better performance. 
			/// </summary>
			DoNotStripElements = D3DFVFCAPS_DONOTSTRIPELEMENTS,

			/// <summary>
			/// Point size is determined by either the render state or the vertex data. If an FVF is used, 
			/// point size can come from point size data in the vertex declaration.
			/// </summary>
			PointSize = D3DFVFCAPS_PSIZE
		};

		/// <summary>
		/// Specifies vertex processing capabilities supported by the device.
		/// </summary>
		/// <unmanaged href="bb172513">D3DVTXCAPS</unmanaged>
		[System::Flags]
		public enum class VertexProcessingCaps
		{
			/// <summary>
			/// Device can do texture generation.
			/// </summary>
			TextureGen = D3DVTXPCAPS_TEXGEN,

			/// <summary>
			/// Indicates that the device supports the color material states.
			/// </summary>
			MaterialSource7 = D3DVTXPCAPS_MATERIALSOURCE7,

			/// <summary>
			/// Device can support directional lights.
			/// </summary>
			DirectionalLights = D3DVTXPCAPS_DIRECTIONALLIGHTS,

			/// <summary>
			/// Device can support positional lights.
			/// </summary>
			PositionalLights = D3DVTXPCAPS_POSITIONALLIGHTS,

			/// <summary>
			/// Device supports local viewer.
			/// </summary>
			LocalViewer = D3DVTXPCAPS_LOCALVIEWER,

			/// <summary>
			/// Device supports vertex tweening.
			/// </summary>
			Tweening = D3DVTXPCAPS_TWEENING,

			/// <summary>
			/// Device supports sphere maps.
			/// </summary>
			TexGenSphereMap = D3DVTXPCAPS_TEXGEN_SPHEREMAP,

			/// <summary>
			/// Device does not support texture generation in non-local viewer mode.
			/// </summary>
			NoTexGenNonLocalViewer = D3DVTXPCAPS_NO_TEXGEN_NONLOCALVIEWER
		};

		/// <summary>
		/// Specifies vertex shader capabilities supported by the device.
		/// </summary>
		/// <unmanaged>D3DVS20CAPS</unmanaged>
		[System::Flags]
		public enum class VertexShaderCaps : System::Int32
		{
			/// <summary>
			/// No extra vertex shader capabilities specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Instruction predication is supported.
			/// </summary>
			Predication = D3DVS20CAPS_PREDICATION
		};

		/// <summary>
		/// Specifies possible XFile formats.
		/// </summary>
		/// <unmanaged>D3DXF_FILEFORMAT</unmanaged>
		[System::Flags]
		public enum class XFileFormat : System::Int32
		{
			/// <summary>
			/// Legacy file format.
			/// </summary>
			Binary = D3DXF_FILEFORMAT_BINARY,

			/// <summary>
			/// Text file format.
			/// </summary>
			Text = D3DXF_FILEFORMAT_TEXT,

			/// <summary>
			/// Compressed file.
			/// </summary>
			Compressed = D3DXF_FILEFORMAT_COMPRESSED
		};

		/// <summary>
		/// Z-buffer usage types.
		/// </summary>
		/// <unmanaged>D3DZBUFFERTYPE</unmanaged>
		public enum class ZBufferType
		{
			/// <summary>
			/// Used to enable the depth buffer.
			/// </summary>
			UseZBuffer = D3DZB_TRUE,

			/// <summary>
			/// Used to disable the depth buffer.
			/// </summary>
			DontUseZBuffer = D3DZB_FALSE,

			/// <summary>
			/// Used to enable a W-buffer.
			/// </summary>
			UseWBuffer = D3DZB_USEW
		};

		/// <summary>Defines the basis type of a high-order patch surface.</summary>
		/// <unmanaged>D3DBASISTYPE</unmanaged>
		public enum class Basis : System::Int32
		{
			/// <summary>
			/// Input vertices are treated as a series of Bezier patches.
			/// </summary>
			Bezier = D3DBASIS_BEZIER,

			/// <summary>
			/// Input vertices are treated as control points of a B-spline surface.
			/// </summary>
			BSpline = D3DBASIS_BSPLINE,

			/// <summary>
			/// An interpolation basis defines the surface so that the surface goes through all the input vertices specified.
			/// </summary>
			CatmullRom = D3DBASIS_CATMULL_ROM,
		};
		
		/// <summary>Defines the supported blend mode.</summary>
		/// <unmanaged>D3DBLEND</unmanaged>
		public enum class Blend : System::Int32
		{
			/// <summary>
			/// Blend factor is (0, 0, 0, 0).
			/// </summary>
			Zero = D3DBLEND_ZERO,

			/// <summary>
			/// Blend factor is (1, 1, 1, 1).
			/// </summary>
			One = D3DBLEND_ONE,

			/// <summary>
			/// Blend factor is the source color.
			/// </summary>
			SourceColor = D3DBLEND_SRCCOLOR,

			/// <summary>
			/// Blend factor is one minus the source color.
			/// </summary>
			InverseSourceColor = D3DBLEND_INVSRCCOLOR,

			/// <summary>
			/// Blend factor is the source alpha.
			/// </summary>
			SourceAlpha = D3DBLEND_SRCALPHA,

			/// <summary>
			/// Blend factor is one minus the source alpha.
			/// </summary>
			InverseSourceAlpha = D3DBLEND_INVSRCALPHA,

			/// <summary>
			/// Blend factor is the destination alpha.
			/// </summary>
			DestinationAlpha = D3DBLEND_DESTALPHA,

			/// <summary>
			/// Blend factor is one minus the destination alpha.
			/// </summary>
			InverseDestinationAlpha = D3DBLEND_INVDESTALPHA,

			/// <summary>
			/// Blend factor is the destination color.
			/// </summary>
			DestinationColor = D3DBLEND_DESTCOLOR,

			/// <summary>
			/// Blend factor is one minus the destination color.
			/// </summary>
			InverseDestinationColor = D3DBLEND_INVDESTCOLOR,

			/// <summary>
			/// Blend factor is (f, f, f, 1); where f = min(As, 1 - Ad).
			/// </summary>
			SourceAlphaSaturated = D3DBLEND_SRCALPHASAT,

			/// <summary>
			/// Source blend factor is one minus the source alpha, and destination blend factor
			/// is one minus the destination alpha.
			/// </summary>
			BothInverseSourceAlpha = D3DBLEND_BOTHINVSRCALPHA,

			/// <summary>
			/// Constant color blending factor used by the frame-buffer blender.
			/// </summary>
			BlendFactor = D3DBLEND_BLENDFACTOR,

			/// <summary>
			/// Inverted constant color blending factor used by the frame-buffer blender.
			/// </summary>
			InverseBlendFactor = D3DBLEND_INVBLENDFACTOR,

			/// <summary>
			/// Blend factor is the output color of the pixel shader. Only available in Direct3D9Ex.
			/// </summary>
			SourceColor2 = D3DBLEND_SRCCOLOR2,

			/// <summary>
			/// Blend factor is one minus the output color of the pixel shader. Only available in Direct3D9Ex.
			/// </summary>
			InverseSourceColor2 = D3DBLEND_INVSRCCOLOR2
		};

		/// <summary>
		/// Defines possible source blending capabilities.
		/// </summary>
		/// <unmanaged href="bb172513">D3DPBLENDCAPS</unmanaged>
		[System::Flags]
		public enum class BlendCaps : System::Int32
		{
			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.Zero.
			/// </summary>
			Zero = D3DPBLENDCAPS_ZERO,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.One.
			/// </summary>
			One = D3DPBLENDCAPS_ONE,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.SourceColor.
			/// </summary>
			SourceColor = D3DPBLENDCAPS_SRCCOLOR,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.InverseSourceColor.
			/// </summary>
			InverseSourceColor = D3DPBLENDCAPS_INVSRCCOLOR,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.SourceAlpha.
			/// </summary>
			SourceAlpha = D3DPBLENDCAPS_SRCALPHA,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.InverseSourceAlpha.
			/// </summary>
			InverseSourceAlpha = D3DPBLENDCAPS_INVSRCALPHA,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.DestinationAlpha.
			/// </summary>
			DestinationAlpha = D3DPBLENDCAPS_DESTALPHA,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.InverseDestinationAlpha.
			/// </summary>
			InverseDestinationAlpha = D3DPBLENDCAPS_INVDESTALPHA,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.DestinationColor.
			/// </summary>
			DestinationColor = D3DPBLENDCAPS_DESTCOLOR,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.InverseDestinationColor.
			/// </summary>
			InverseDestinationColor = D3DPBLENDCAPS_INVDESTCOLOR,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.SourceAlphaSaturated.
			/// </summary>
			SourceAlphaSaturated = D3DPBLENDCAPS_SRCALPHASAT,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.BothInverseSourceAlpha.
			/// </summary>
			BothInverseSourceAlpha = D3DPBLENDCAPS_BOTHINVSRCALPHA,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Blend"/>.BlendFactor and <see cref="SlimDX::Direct3D9::Blend"/>.InverseBlendFactor.
			/// </summary>
			BlendFactor = D3DPBLENDCAPS_BLENDFACTOR
		};
		
		/// <summary>Defines the supported blend operations.</summary>
		/// <unmanaged>D3DBLENDOP</unmanaged>
		public enum class BlendOperation : System::Int32
		{
			/// <summary>
			/// The result is the destination added to the source.
			/// </summary>
			Add = D3DBLENDOP_ADD,

			/// <summary>
			/// The result is the destination subtracted from the source.
			/// </summary>
			Subtract = D3DBLENDOP_SUBTRACT,

			/// <summary>
			/// The result is the source subtracted from the destination.
			/// </summary>
			ReverseSubtract = D3DBLENDOP_REVSUBTRACT,

			/// <summary>
			/// The result is the minimum of the source and destination.
			/// </summary>
			Minimum = D3DBLENDOP_MIN,

			/// <summary>
			/// The result is the maximum of the source and destination.
			/// </summary>
			Maximum = D3DBLENDOP_MAX,
		};

		/// <summary>Driver-specific capability flags.</summary>
		/// <unmanaged>D3DCAPS</unmanaged>
		[System::Flags]
		public enum class Caps : System::Int32
		{
			/// <summary>
			/// No extra capabilities defined.
			/// </summary>
			None = 0,

			/// <summary>
			/// Display hardware is capable of returning the current scan line.
			/// </summary>
			ReadScanline = D3DCAPS_READ_SCANLINE,
		};

		/// <summary>Driver-specific capability flags.</summary>
		/// <unmanaged>D3DCAPS2</unmanaged>
		[System::Flags]
		public enum class Caps2 : System::Int32
		{
			/// <summary>
			/// No extra capabilities defined.
			/// </summary>
			None = 0,

			/// <summary>
			/// The driver supports dynamic gamma ramp adjustment in fullscreen mode.
			/// </summary>
			FullScreenGamma = D3DCAPS2_FULLSCREENGAMMA,

			/// <summary>
			/// The system has a calibrator installed that can automatically adjust the gamma ramp so that
			/// the result is identical on all systems that have a calibrator.
			/// </summary>
			CanCalibrateGamma = D3DCAPS2_CANCALIBRATEGAMMA,

			/// <summary>
			/// The driver is capable of managing resources.
			/// </summary>
			CanManageResource = D3DCAPS2_CANMANAGERESOURCE,

			/// <summary>
			/// The driver supports dynamic textures.
			/// </summary>
			DynamicTextures = D3DCAPS2_DYNAMICTEXTURES,

			/// <summary>
			/// The driver is capable of automatically generating mipmaps.
			/// </summary>
			CanAutoGenerateMipMap = D3DCAPS2_CANAUTOGENMIPMAP
		};

		/// <summary>Driver-specific capability flags.</summary>
		/// <unmanaged>D3DCAPS3</unmanaged>
		[System::Flags]
		public enum class Caps3 : System::Int32
		{
			/// <summary>
			/// No extra capabilities defined.
			/// </summary>
			None = 0,

			/// <summary>
			/// Indicates that the device can respect the AlphaBlendEnable render state in fullscreen mode
			/// while using the Flip or Discard swap effect.
			/// </summary>
			AlphaFullScreenFlipOrDiscard = D3DCAPS3_ALPHA_FULLSCREEN_FLIP_OR_DISCARD,

			/// <summary>
			/// Indicates that the device can perform gamma correction from a windowed back buffer to
			/// an sRGB desktop.
			/// </summary>
			LinearToSrgbPresentation = D3DCAPS3_LINEAR_TO_SRGB_PRESENTATION,

			/// <summary>
			/// Device can accelerate a memory copy from system memory to local video memory.
			/// </summary>
			CopyToVideoMemory = D3DCAPS3_COPY_TO_VIDMEM,

			/// <summary>
			/// Device can accelerate a memory copy from local video memory to system memory.
			/// </summary>
			CopyToSystemMemory = D3DCAPS3_COPY_TO_SYSTEMMEM,
		};

		/// <summary>
		/// Defines possible character sets for fonts.
		/// </summary>
		/// <unmanaged>CHARSET</unmanaged>
		public enum class CharacterSet : System::Int32
		{
			/// <summary>
			/// The ANSI character set.
			/// </summary>
			Ansi = ANSI_CHARSET,

			/// <summary>
			/// The default system character set.
			/// </summary>
			Default = DEFAULT_CHARSET,

			/// <summary>
			/// The symbol character set.
			/// </summary>
			Symbol = SYMBOL_CHARSET,

			/// <summary>
			/// The ShiftJIS character set.
			/// </summary>
			ShiftJIS = SHIFTJIS_CHARSET,

			/// <summary>
			/// The Hangul character set.
			/// </summary>
			Hangul = HANGUL_CHARSET,

			/// <summary>
			/// The GB2312 character set.
			/// </summary>
			GB2312 = GB2312_CHARSET,

			/// <summary>
			/// The Chinese character set.
			/// </summary>
			ChineseBig5 = CHINESEBIG5_CHARSET,

			/// <summary>
			/// The OEM character set.
			/// </summary>
			Oem = OEM_CHARSET,

			/// <summary>
			/// The Johab character set.
			/// </summary>
			Johab = JOHAB_CHARSET,

			/// <summary>
			/// The Hebrew character set.
			/// </summary>
			Hebrew = HEBREW_CHARSET,

			/// <summary>
			/// The Arabic character set.
			/// </summary>
			Arabic = ARABIC_CHARSET,

			/// <summary>
			/// The Greek character set.
			/// </summary>
			Greek = GREEK_CHARSET,

			/// <summary>
			/// The Turkish character set.
			/// </summary>
			Turkish = TURKISH_CHARSET,

			/// <summary>
			/// The Vietnamese character set.
			/// </summary>
			Vietnamese = VIETNAMESE_CHARSET,

			/// <summary>
			/// The Thai character set.
			/// </summary>
			Thai = THAI_CHARSET,

			/// <summary>
			/// The East Europe character set.
			/// </summary>
			EastEurope = EASTEUROPE_CHARSET,

			/// <summary>
			/// The Russian character set.
			/// </summary>
			Russian = RUSSIAN_CHARSET,

			/// <summary>
			/// The Baltic character set.
			/// </summary>
			Baltic = BALTIC_CHARSET,

			/// <summary>
			/// The Mac character set.
			/// </summary>
			Mac = MAC_CHARSET
		};

		/// <summary>
		/// These flags identify a surface to reset when calling <see cref="SlimDX::Direct3D9::Device">Device.Clear</see>.
		/// </summary>
		/// <unmanaged>D3DCLEAR</unmanaged>
		[System::Flags]
		public enum class ClearFlags : System::Int32
		{
			/// <summary>
			/// Don't clear any surfaces.
			/// </summary>
			None = 0,

			/// <summary>
			/// Clear the stencil surface.
			/// </summary>
			Stencil = D3DCLEAR_STENCIL,

			/// <summary>
			/// Clear the render target.
			/// </summary>
			Target = D3DCLEAR_TARGET,

			/// <summary>
			/// Clear the depth buffer.
			/// </summary>
			ZBuffer = D3DCLEAR_ZBUFFER,

			All = Stencil | Target | ZBuffer
		};

		/// <summary>
		/// Specifies a set of values that describe the current clip status.
		/// </summary>
		/// <unmanaged>D3DCS</unmanaged>
		[System::Flags]
		public enum class ClipFlags : System::Int32
		{
			/// <summary>
			/// Combination of all clip flags.
			/// </summary>
			All = D3DCS_ALL,

			/// <summary>
			/// All vertices are clipped by the left plane of the viewing frustum.
			/// </summary>
			Left = D3DCS_LEFT,

			/// <summary>
			/// All vertices are clipped by the right plane of the viewing frustum.
			/// </summary>
			Right = D3DCS_RIGHT,

			/// <summary>
			/// All vertices are clipped by the top plane of the viewing frustum.
			/// </summary>
			Top = D3DCS_TOP,

			/// <summary>
			/// All vertices are clipped by the bottom plane of the viewing frustum.
			/// </summary>
			Bottom = D3DCS_BOTTOM,

			/// <summary>
			/// All vertices are clipped by the front plane of the viewing frustum.
			/// </summary>
			Front = D3DCS_FRONT,

			/// <summary>
			/// All vertices are clipped by the back plane of the viewing frustum.
			/// </summary>
			Back = D3DCS_BACK,

			/// <summary>
			/// Application defined clipping plane.
			/// </summary>
			Plane0 = D3DCS_PLANE0,

			/// <summary>
			/// Application defined clipping plane.
			/// </summary>
			Plane1 = D3DCS_PLANE1,

			/// <summary>
			/// Application defined clipping plane.
			/// </summary>
			Plane2 = D3DCS_PLANE2,

			/// <summary>
			/// Application defined clipping plane.
			/// </summary>
			Plane3 = D3DCS_PLANE3,

			/// <summary>
			/// Application defined clipping plane.
			/// </summary>
			Plane4 = D3DCS_PLANE4,

			/// <summary>
			/// Application defined clipping plane.
			/// </summary>
			Plane5 = D3DCS_PLANE5
		};
		
		/// <summary>
		/// Defines the location at which a color or color component must be accessed for lighting calculations.
		/// </summary>
		/// <unmanaged>D3DMATERIALCOLORSOURCE</unmanaged>
		public enum class ColorSource : System::Int32
		{
			/// <summary>
			/// Use the color from the current material.
			/// </summary>
			Material = D3DMCS_MATERIAL,

			/// <summary>
			/// Use the diffuse vertex color.
			/// </summary>
			Color1 = D3DMCS_COLOR1,

			/// <summary>
			/// Use the specular vertex color.
			/// </summary>
			Color2 = D3DMCS_COLOR2,
		};
		
		/// <summary>
		/// Flags that enable a per-channel write for the render target color buffer.
		/// </summary>
		/// <unmanaged href="bb172599">D3DCOLORWRITEENABLE</unmanaged>
		[System::Flags]
		public enum class ColorWriteEnable : System::Int32
		{
			/// <summary>
			/// Allow writes to the alpha channel.
			/// </summary>
			Alpha = D3DCOLORWRITEENABLE_ALPHA,

			/// <summary>
			/// Allow writes to the blue channel.
			/// </summary>
			Blue = D3DCOLORWRITEENABLE_BLUE,

			/// <summary>
			/// Allow writes to the green channel.
			/// </summary>
			Green = D3DCOLORWRITEENABLE_GREEN,

			/// <summary>
			/// Allow writes to the red channel.
			/// </summary>
			Red = D3DCOLORWRITEENABLE_RED,

			/// <summary>
			/// Allow writes to all channels.
			/// </summary>
			All = Alpha | Blue | Green | Red
		};

		/// <summary>Specifies possible compare functions.</summary>
		/// <unmanaged>D3DCMPFUNC</unmanaged>
		public enum class Compare
		{
			/// <summary>
			/// Always fail the test.
			/// </summary>
			Never = D3DCMP_NEVER,

			/// <summary>
			/// Accept the new pixel if its value is less than the value of the current pixel.
			/// </summary>
			Less = D3DCMP_LESS,

			/// <summary>
			/// Accept the new pixel if its value equals the value of the current pixel.
			/// </summary>
			Equal = D3DCMP_EQUAL,

			/// <summary>
			/// Accept the new pixel if its value is less than or equal to the value of the current pixel.
			/// </summary>
			LessEqual = D3DCMP_LESSEQUAL,

			/// <summary>
			/// Accept the new pixel if its value is greater than the value of the current pixel.
			/// </summary>
			Greater = D3DCMP_GREATER,

			/// <summary>
			/// Accept the new pixel if its value does not equal the value of the current pixel.
			/// </summary>
			NotEqual = D3DCMP_NOTEQUAL,

			/// <summary>
			/// Accept the new pixel if its value is greater than or equal to the value of the current pixel.
			/// </summary>
			GreaterEqual = D3DCMP_GREATEREQUAL,

			/// <summary>
			/// Always pass the test.
			/// </summary>
			Always = D3DCMP_ALWAYS
		};

		/// <summary>
		/// Specifies a set of flags that describe the supported compare capabilities of the device.
		/// </summary>
		/// <unmanaged>D3DPCMPCAPS</unmanaged>
		[System::Flags]
		public enum class CompareCaps : System::Int32
		{
			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Compare"/>.Never.
			/// </summary>
			Never = D3DPCMPCAPS_NEVER,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Compare"/>.Less.
			/// </summary>
			Less = D3DPCMPCAPS_LESS,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Compare"/>.Equal.
			/// </summary>
			Equal = D3DPCMPCAPS_EQUAL,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Compare"/>.lessEqual.
			/// </summary>
			LessEqual = D3DPCMPCAPS_LESSEQUAL,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Compare"/>.Greater.
			/// </summary>
			Greater = D3DPCMPCAPS_GREATER,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Compare"/>.NotEqual.
			/// </summary>
			NotEqual = D3DPCMPCAPS_NOTEQUAL,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Compare"/>.GreaterEqual.
			/// </summary>
			GreaterEqual = D3DPCMPCAPS_GREATEREQUAL,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::Compare"/>.Always.
			/// </summary>
			Always = D3DPCMPCAPS_ALWAYS
		};

		/// <summary>A combination of one or more flags that control the device creation behavior.</summary>
		/// <unmanaged>D3DCREATE</unmanaged>
		[System::Flags]
		public enum class CreateFlags : System::Int32
		{
			/// <summary>
			/// No extra creation flags specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Asks the device to drive all heads that the master adapter owns.
			/// </summary>
			AdapterGroupDevice = D3DCREATE_ADAPTERGROUP_DEVICE,

			/// <summary>
			/// Direct3D will managed resources instead of the driver.
			/// </summary>
			DisableDriverManagement = D3DCREATE_DISABLE_DRIVER_MANAGEMENT,

			/// <summary>
			/// Direct3D will managed resources instead of the driver. Errors will still be thrown
			/// for conditions such as insufficient video memory.
			/// </summary>
			DisableExtendedDriverManagement = D3DCREATE_DISABLE_DRIVER_MANAGEMENT_EX,

			/// <summary>
			/// Causes the runtime to not register hotkeys for print screen. Only available in Direct3D9Ex.
			/// </summary>
			DisablePrintScreen = D3DCREATE_DISABLE_PRINTSCREEN,

			/// <summary>
			/// Restrict computation to the main application thread. Only available on Windows Vista.
			/// </summary>
			DisablePsgpThreading = D3DCREATE_DISABLE_PSGP_THREADING,

			/// <summary>
			/// Enables the gathering of presentation statistics. Only available in Direct3D9Ex.
			/// </summary>
			EnablePresentStatistics = D3DCREATE_ENABLE_PRESENTSTATS,

			/// <summary>
			/// Preserve the floating point precision used in the calling thread.
			/// </summary>
			FpuPreserve = D3DCREATE_FPU_PRESERVE,

			/// <summary>
			/// Tells the device to use hardware vertex processing.
			/// </summary>
			HardwareVertexProcessing = D3DCREATE_HARDWARE_VERTEXPROCESSING,

			/// <summary>
			/// Tells the device to use mixed vertex processing.
			/// </summary>
			MixedVertexProcessing = D3DCREATE_MIXED_VERTEXPROCESSING,

			/// <summary>
			/// Indicates that the application requests Direct3D to be multithread safe.
			/// </summary>
			Multithreaded = D3DCREATE_MULTITHREADED,

			/// <summary>
			/// Indicates that Direct3D must not alter the focus window in any way.
			/// </summary>
			NoWindowChanges = D3DCREATE_NOWINDOWCHANGES,

			/// <summary>
			/// Specifies that the device does not support Get* calls for anything that can be stored
			/// in state blocks.
			/// </summary>
			PureDevice = D3DCREATE_PUREDEVICE,

			/// <summary>
			/// Allows screensavers during a fullscreen application.
			/// </summary>
			AllowScreensavers = D3DCREATE_SCREENSAVER,

			/// <summary>
			/// Tells the device to use software vertex processing.
			/// </summary>
			SoftwareVertexProcessing = D3DCREATE_SOFTWARE_VERTEXPROCESSING,
		};

		/// <summary>
		/// Specifies how to combine glyph data from the source and destination surfaces in a ComposeRect operation.
		/// </summary>
		/// <unmanaged>D3DCOMPOSERECTSOP</unmanaged>
		public enum class ComposeRectOperation : System::Int32
		{
			/// <summary>
			/// Copy the source to the destination.
			/// </summary>
			Copy = D3DCOMPOSERECTS_COPY,

			/// <summary>
			/// Bitwise OR the source and the destination.
			/// </summary>
			Or = D3DCOMPOSERECTS_OR,

			/// <summary>
			/// Bitwise AND the source and the destination.
			/// </summary>
			And = D3DCOMPOSERECTS_AND,

			/// <summary>
			/// Copy the negated source to the destination.
			/// </summary>
			Negate = D3DCOMPOSERECTS_NEG
		};
		
		/// <summary>Defines the faces of a cubemap.</summary>
		/// <unmanaged>D3DCUBEMAP_FACES</unmanaged>
		public enum class CubeMapFace : System::Int32
		{
			/// <summary>
			/// Positive x-face of the cubemap.
			/// </summary>
			PositiveX = D3DCUBEMAP_FACE_POSITIVE_X,

			/// <summary>
			/// Negative x-face of the cubemap.
			/// </summary>
			NegativeX = D3DCUBEMAP_FACE_NEGATIVE_X,

			/// <summary>
			/// Positive y-face of the cubemap.
			/// </summary>
			PositiveY = D3DCUBEMAP_FACE_POSITIVE_Y,

			/// <summary>
			/// Negative y-face of the cubemap.
			/// </summary>
			NegativeY = D3DCUBEMAP_FACE_NEGATIVE_Y,

			/// <summary>
			/// Positive z-face of the cubemap.
			/// </summary>
			PositiveZ = D3DCUBEMAP_FACE_POSITIVE_Z,

			/// <summary>
			/// Negative z-face of the cubemap.
			/// </summary>
			NegativeZ = D3DCUBEMAP_FACE_NEGATIVE_Z
		};
		
		/// <summary>Defines the supported culling modes.</summary>
		/// <unmanaged>D3DCULL</unmanaged>
		public enum class Cull : System::Int32
		{
			/// <summary>
			/// Cull back faces with clockwise vertices.
			/// </summary>
			Clockwise = D3DCULL_CW,

			/// <summary>
			/// Cull back faces with counterclockwise vertices.
			/// </summary>
			Counterclockwise = D3DCULL_CCW,

			/// <summary>
			/// Do not cull back faces.
			/// </summary>
			None = D3DCULL_NONE
		};

		/// <summary>Driver cursor capability flags.</summary>
		/// <unmanaged>D3DCURSORCAPS</unmanaged>
		[System::Flags]
		public enum class CursorCaps : System::Int32
		{
			/// <summary>
			/// The driver supports color cursors.
			/// </summary>
			Color = D3DCURSORCAPS_COLOR,

			/// <summary>
			/// The driver supports cursors in low resolution modes.
			/// </summary>
			LowResolution = D3DCURSORCAPS_LOWRES,
		};

		/// <summary>
		/// Defines settings for debug monitor tokens.
		/// </summary>
		/// <unmanaged>D3DDEBUGMONITORTOKENS</unmanaged>
		public enum class DebugMonitorTokens : System::Int32
		{
			/// <summary>
			/// Enable the debug monitor.
			/// </summary>
			Enable = D3DDMT_ENABLE,

			/// <summary>
			/// Disable the debug monitor.
			/// </summary>
			Disable = D3DDMT_DISABLE
		};
		
		/// <summary>
		/// Defines the vertex declaration method which is a predefined operation performed by the
		/// tessellator (or any procedural geometry routine on the vertex data during tessellation).
		/// </summary>
		/// <unmanaged>D3DDECLMETHOD</unmanaged>
		public enum class DeclarationMethod : System::Byte
		{
			/// <summary>
			/// Default value. The tessellator copies the vertex data as is, with no additional calculations.
			/// </summary>
			Default = D3DDECLMETHOD_DEFAULT,

			/// <summary>
			/// Computes the tangent at a point on the rectangle or triangle patch in the U direction.
			/// </summary>
			PartialU = D3DDECLMETHOD_PARTIALU,

			/// <summary>
			/// Computes the tangent at a point on the rectangle or triangle patch in the V direction.
			/// </summary>
			PartialV = D3DDECLMETHOD_PARTIALV,

			/// <summary>
			/// Computes the normal at a point on the rectangle or triangle patch by taking the cross product of the two tangents.
			/// </summary>
			CrossUV = D3DDECLMETHOD_CROSSUV,

			/// <summary>
			/// Copies out the U and V values at a point on the rectangle or triangle patch.
			/// </summary>
			UV = D3DDECLMETHOD_UV,

			/// <summary>
			/// Look up a displacement map.
			/// </summary>
			Lookup = D3DDECLMETHOD_LOOKUP,

			/// <summary>
			/// Lookup a presampled displacement map.
			/// </summary>
			LookupPresampled = D3DDECLMETHOD_LOOKUPPRESAMPLED
		};

		/// <summary>Defines a vertex declaration data type.</summary>
		/// <unmanaged>D3DDECLTYPE</unmanaged>
		public enum class DeclarationType : System::Byte
		{
			/// <summary>
			/// One component float.
			/// </summary>
			Float1 = D3DDECLTYPE_FLOAT1,

			/// <summary>
			/// Two component float.
			/// </summary>
			Float2 = D3DDECLTYPE_FLOAT2,

			/// <summary>
			/// Three component float.
			/// </summary>
			Float3 = D3DDECLTYPE_FLOAT3,

			/// <summary>
			/// Four component float.
			/// </summary>
			Float4 = D3DDECLTYPE_FLOAT4,

			/// <summary>
			/// Four component, packed, unsigned bytes mapped to the 0 to 1 range. Input is a color
			/// and is expanded to RGBA order.
			/// </summary>
			Color = D3DDECLTYPE_D3DCOLOR,

			/// <summary>
			/// Four component, unsigned byte.
			/// </summary>
			Ubyte4 = D3DDECLTYPE_UBYTE4,

			/// <summary>
			/// Two component, signed short.
			/// </summary>
			Short2 = D3DDECLTYPE_SHORT2,

			/// <summary>
			/// Four component, signed short.
			/// </summary>
			Short4 = D3DDECLTYPE_SHORT4,

			/// <summary>
			/// Four component byte with each byte normalized by dividing by 255.0f.
			/// </summary>
			UByte4N = D3DDECLTYPE_UBYTE4N,

			/// <summary>
			/// Normalized, two component, signed short normalized by dividing by 32767.0f.
			/// </summary>
			Short2N = D3DDECLTYPE_SHORT2N,

			/// <summary>
			/// Normalized, four component, signed short normalized by dividing by 32767.0f.
			/// </summary>
			Short4N = D3DDECLTYPE_SHORT4N,

			/// <summary>
			/// Normalized, two component, unsigned short normalized by dividing by 65535.0f.
			/// </summary>
			UShort2N = D3DDECLTYPE_USHORT2N,

			/// <summary>
			/// Normalized, four component, unsigned short normalized by dividing by 65535.0f.
			/// </summary>
			UShort4N = D3DDECLTYPE_USHORT4N,

			/// <summary>
			/// Three component, unsigned, 10 10 10 format.
			/// </summary>
			UDec3 = D3DDECLTYPE_UDEC3,

			/// <summary>
			/// Three component, signed, 10 10 10 format normalized by dividing by 511.0f.
			/// </summary>
			Dec3N = D3DDECLTYPE_DEC3N,

			/// <summary>
			/// Two component, 16 bit, floating point.
			/// </summary>
			HalfTwo = D3DDECLTYPE_FLOAT16_2,

			/// <summary>
			/// Four component, 16 bit, floating point.
			/// </summary>
			HalfFour = D3DDECLTYPE_FLOAT16_4,

			/// <summary>
			/// Type field in the declaration is unused.
			/// </summary>
			Unused = D3DDECLTYPE_UNUSED
		};

		/// <summary>
		/// Specifies the declaration types supported by the device.
		/// </summary>
		/// <unmanaged>D3DDTCAPS</unmanaged>
		[System::Flags]
		public enum class DeclarationTypeCaps
		{
			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.UByte4.
			/// </summary>
			UByte4 = D3DDTCAPS_UBYTE4,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.UByte4N.
			/// </summary>
			UByte4N = D3DDTCAPS_UBYTE4N,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.Short2N.
			/// </summary>
			Short2N = D3DDTCAPS_SHORT2N,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.Short4N.
			/// </summary>
			Short4N = D3DDTCAPS_SHORT4N,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.UShort2N.
			/// </summary>
			UShort2N = D3DDTCAPS_USHORT2N,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.UShort4N.
			/// </summary>
			UShort4N = D3DDTCAPS_USHORT4N,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.UDec3.
			/// </summary>
			UDec3 = D3DDTCAPS_UDEC3,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.Dec3N.
			/// </summary>
			Dec3N = D3DDTCAPS_DEC3N,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.HalfTwo.
			/// </summary>
			HalfTwo = D3DDTCAPS_FLOAT16_2,

			/// <summary>
			/// The device supports <see cref="SlimDX::Direct3D9::DeclarationType"/>.HalfFour.
			/// </summary>
			HalfFour = D3DDTCAPS_FLOAT16_4,
		};

		/// <summary>Identifies the intended use of vertex data.</summary>
		/// <unmanaged>D3DDECLUSAGE</unmanaged>
		public enum class DeclarationUsage : System::Byte
		{
			/// <summary>
			/// Position data ranging from (-1, -1) to (1, 1).
			/// </summary>
			Position = D3DDECLUSAGE_POSITION,

			/// <summary>
			/// Blending weight data.
			/// </summary>
			BlendWeight = D3DDECLUSAGE_BLENDWEIGHT,

			/// <summary>
			/// Blending indices data.
			/// </summary>
			BlendIndices = D3DDECLUSAGE_BLENDINDICES,

			/// <summary>
			/// Vertex normal data.
			/// </summary>
			Normal = D3DDECLUSAGE_NORMAL,

			/// <summary>
			/// Point size data.
			/// </summary>
			PointSize = D3DDECLUSAGE_PSIZE,

			/// <summary>
			/// Texture coordinate data.
			/// </summary>
			TextureCoordinate = D3DDECLUSAGE_TEXCOORD,

			/// <summary>
			/// Vertex tangent data.
			/// </summary>
			Tangent = D3DDECLUSAGE_TANGENT,

			/// <summary>
			/// Vertex binormal data.
			/// </summary>
			Binormal = D3DDECLUSAGE_BINORMAL,

			/// <summary>
			/// Single positive floating point value.
			/// </summary>
			TessellateFactor = D3DDECLUSAGE_TESSFACTOR,

			/// <summary>
			/// Vertex data contains transformed position data ranging from (0, 0) to (viewport width, viewport height).
			/// </summary>
			PositionTransformed = D3DDECLUSAGE_POSITIONT,

			/// <summary>
			/// Vertex data contains diffuse or specular color.
			/// </summary>
			Color = D3DDECLUSAGE_COLOR,

			/// <summary>
			/// Vertex data contains fog data.
			/// </summary>
			Fog = D3DDECLUSAGE_FOG,

			/// <summary>
			/// Vertex data contains depth data.
			/// </summary>
			Depth = D3DDECLUSAGE_DEPTH,

			/// <summary>
			/// Vertex data contains sampler data.
			/// </summary>
			Sample = D3DDECLUSAGE_SAMPLE
		};
		
		/// <summary>Defines the degree of the variables in the equation that describes a curve.</summary>
		/// <unmanaged>D3DDEGREETYPE</unmanaged>
		public enum class Degree : System::Int32
		{
			/// <summary>
			/// Curve is described by variables of first order.
			/// </summary>
			Linear = D3DDEGREE_LINEAR,

			/// <summary>
			/// Curve is described by variables of second order.
			/// </summary>
			Quadratic = D3DDEGREE_QUADRATIC,

			/// <summary>
			/// Curve is described by variables of third order.
			/// </summary>
			Cubic = D3DDEGREE_CUBIC,

			/// <summary>
			/// Curve is described by variables of fourth order.
			/// </summary>
			Quintic = D3DDEGREE_QUINTIC,
		};

		/// <summary>Driver capability flags.</summary>
		/// <unmanaged>D3DDEVCAPS2</unmanaged>
		[System::Flags]
		public enum class DeviceCaps2
		{
			/// <summary>
			/// Device supports stream offsets.
			/// </summary>
			StreamOffset = D3DDEVCAPS2_STREAMOFFSET,

			/// <summary>
			/// Device supports displacement maps for N-patches.
			/// </summary>
			DMapNPatch = D3DDEVCAPS2_DMAPNPATCH,

			/// <summary>
			/// Device supports adaptive tessellation of RT-patches.
			/// </summary>
			AdaptiveTessRTPatch = D3DDEVCAPS2_ADAPTIVETESSRTPATCH,

			/// <summary>
			/// Device supports adaptive tessellation of N-patches.
			/// </summary>
			AdaptiveTessNPatch = D3DDEVCAPS2_ADAPTIVETESSNPATCH,

			/// <summary>
			/// Device supports texture stretching using a texture as the source.
			/// </summary>
			CanStretchRectFromTextures = D3DDEVCAPS2_CAN_STRETCHRECT_FROM_TEXTURES,

			/// <summary>
			/// Devices supports presampled displacement maps for N-patches.
			/// </summary>
			PresampledMapNPatch = D3DDEVCAPS2_PRESAMPLEDDMAPNPATCH,

			/// <summary>
			/// Multiple vertex elements can share the same stream offset in a stream.
			/// </summary>
			VertexElementsCanShareStreamOffset = D3DDEVCAPS2_VERTEXELEMENTSCANSHARESTREAMOFFSET,
		};

		/// <summary>
		/// Flags identifying the capabilities of the device.
		/// </summary>
		/// <unmanaged href="bb172513">D3DDEVCAPS</unmanaged>
		[System::Flags]
		public enum class DeviceCaps : System::Int32
		{
			/// <summary>
			/// Device can use execute buffers from system memory.
			/// </summary>
			ExecuteSystemMemory = D3DDEVCAPS_EXECUTESYSTEMMEMORY,

			/// <summary>
			/// Device can use execute buffers from video memory.
			/// </summary>
			ExecuteVideoMemory = D3DDEVCAPS_EXECUTEVIDEOMEMORY,

			/// <summary>
			/// Device can use buffers from system memory for transformed and lit vertices.
			/// </summary>
			TLVertexSystemMemory = D3DDEVCAPS_TLVERTEXSYSTEMMEMORY,

			/// <summary>
			/// Device can use buffers from video memory for transformed and lit vertices.
			/// </summary>
			TLVertexVideoMemory = D3DDEVCAPS_TLVERTEXVIDEOMEMORY,

			/// <summary>
			/// Device can retrieve textures from system memory.
			/// </summary>
			TextureSystemMemory = D3DDEVCAPS_TEXTURESYSTEMMEMORY,

			/// <summary>
			/// Device can retrieve textures from video memory.
			/// </summary>
			TextureVideoMemory = D3DDEVCAPS_TEXTUREVIDEOMEMORY,

			/// <summary>
			/// Device exports a DrawPrimitive aware HAL.
			/// </summary>
			DrawPrimTLVertex = D3DDEVCAPS_DRAWPRIMTLVERTEX,

			/// <summary>
			/// Device can queue rendering commands after a page flip.
			/// </summary>
			CanRenderAfterFlip = D3DDEVCAPS_CANRENDERAFTERFLIP,

			/// <summary>
			/// Device can retrieve textures from non-local video memory.
			/// </summary>
			TextureNonLocalVideoMemory = D3DDEVCAPS_TEXTURENONLOCALVIDMEM,

			/// <summary>
			/// Device can support at least a DirectX 5-compliant driver.
			/// </summary>
			DrawPrimitives2 = D3DDEVCAPS_DRAWPRIMITIVES2,

			/// <summary>
			/// Device is texturing from separate memory pools.
			/// </summary>
			SeparateTextureMemory = D3DDEVCAPS_SEPARATETEXTUREMEMORIES,

			/// <summary>
			/// Device can support at least a DirectX 7-compliant driver.
			/// </summary>
			DrawPrimitives2Extended = D3DDEVCAPS_DRAWPRIMITIVES2EX,

			/// <summary>
			/// Device can support hardware transformation and lighting.
			/// </summary>
			HWTransformAndLight = D3DDEVCAPS_HWTRANSFORMANDLIGHT,

			/// <summary>
			/// Device supports blits from system-memory textures to nonlocal video-memory textures.
			/// </summary>
			CanBlitSysToNonLocal = D3DDEVCAPS_CANBLTSYSTONONLOCAL,

			/// <summary>
			/// Device has hardware acceleration for scene rasterization.
			/// </summary>
			HWRasterization = D3DDEVCAPS_HWRASTERIZATION,

			/// <summary>
			/// Device can support rasterization, transform, lighting, and shading in hardware.
			/// </summary>
			PureDevice = D3DDEVCAPS_PUREDEVICE,

			/// <summary>
			/// Device supports quintic bezier curves and b-splines.
			/// </summary>
			QuinticRTPatches = D3DDEVCAPS_QUINTICRTPATCHES,

			/// <summary>
			/// Device supports rectangular and triangular patches.
			/// </summary>
			RTPatches = D3DDEVCAPS_RTPATCHES,

			/// <summary>
			/// The device does not require caching of any patch information.
			/// </summary>
			RTPatchHandleZero = D3DDEVCAPS_RTPATCHHANDLEZERO,

			/// <summary>
			/// Device supports N-patches.
			/// </summary>
			NPatches = D3DDEVCAPS_NPATCHES,
		};
		
		/// <summary>Defines device types.</summary>
		/// <unmanaged>D3DDEVTYPE</unmanaged>
		public enum class DeviceType : System::Int32
		{
			/// <summary>
			/// Hardware rasterization.
			/// </summary>
			Hardware = D3DDEVTYPE_HAL,

			/// <summary>
			/// Initialize Direct3D on a computer that has neither hardware nor reference rasterization available.
			/// </summary>
			NullReference = D3DDEVTYPE_NULLREF,

			/// <summary>
			/// Direct3D features are implemented in software.
			/// </summary>
			Reference = D3DDEVTYPE_REF,

			/// <summary>
			/// A pluggable software renderer has been registered. Not supported by SlimDX.
			/// </summary>
			Software = D3DDEVTYPE_SW
		};
		
		/// <summary>
		/// Specifies formatting options for text rendering.
		/// </summary>
		/// <unmanaged href="bb773199">DT</unmanaged>
		[System::Flags]
		public enum class DrawTextFormat : System::Int32
		{
			/// <summary>
			/// Align the text to the top.
			/// </summary>
			Top = DT_TOP,

			/// <summary>
			/// Align the text to the left.
			/// </summary>
			Left = DT_LEFT,

			/// <summary>
			/// Align the text to the center.
			/// </summary>
			Center = DT_CENTER,

			/// <summary>
			/// Align the text to the right.
			/// </summary>
			Right = DT_RIGHT,

			/// <summary>
			/// Vertically align the text to the center.
			/// </summary>
			VerticalCenter = DT_VCENTER,

			/// <summary>
			/// Align the text to the bottom.
			/// </summary>
			Bottom = DT_BOTTOM,

			/// <summary>
			/// Allow word breaks.
			/// </summary>
			WordBreak = DT_WORDBREAK,

			/// <summary>
			/// Force all text to a single line.
			/// </summary>
			SingleLine = DT_SINGLELINE,

			/// <summary>
			/// Expand tab characters.
			/// </summary>
			ExpandTabs = DT_EXPANDTABS,

			/// <summary>
			/// Don't clip the text.
			/// </summary>
			NoClip = DT_NOCLIP,

			/// <summary>
			/// Rendering the text in right-to-left reading order.
			/// </summary>
			RtlReading = DT_RTLREADING,
		};
		
		/// <summary>
		/// Specifies possible driver levels.
		/// </summary>
		/// <unmanaged>None</unmanaged>
		public enum class DriverLevel : System::Int32
		{
			/// <summary>
			/// The driver supports at least Direct3D7.
			/// </summary>
			Direct3D7 = 700,

			/// <summary>
			/// The driver supports at least Direct3D8.
			/// </summary>
			Direct3D8 = 800,

			/// <summary>
			/// The driver supports at least Direct3D9.
			/// </summary>
			Direct3D9 = 900,
		};

		/// <summary>
		/// Specifies how the monitor being used to display a fullscreen application is rotated.
		/// </summary>
		/// <unmanaged>D3DDISPLAYROTATION</unmanaged>
		public enum class DisplayRotation : System::Int32
		{
			/// <summary>
			/// Display is not rotated.
			/// </summary>
			Identity = D3DDISPLAYROTATION_IDENTITY,

			/// <summary>
			/// Display is rotated 90 degrees.
			/// </summary>
			Rotation90 = D3DDISPLAYROTATION_90,

			/// <summary>
			/// Display is rotated 180 degrees.
			/// </summary>
			Rotation180 = D3DDISPLAYROTATION_180,

			/// <summary>
			/// Display is rotated 270 degrees.
			/// </summary>
			Rotation270 = D3DDISPLAYROTATION_270
		};
		
		/// <summary>
		/// Defines constants describing the fill mode.
		/// </summary>
		/// <unmanaged>D3DFILLMODE</unmanaged>
		public enum class FillMode : System::Int32
		{
			/// <summary>
			/// Fill points.
			/// </summary>
			Point = D3DFILL_POINT,

			/// <summary>
			/// Fill wireframe.
			/// </summary>
			Wireframe = D3DFILL_WIREFRAME,

			/// <summary>
			/// Fill solid.
			/// </summary>
			Solid = D3DFILL_SOLID
		};

		/// <summary>
		/// Defines the filter capabilities of the device.
		/// </summary>
		/// <unmanaged>D3DPTFILTERCAPS</unmanaged>
		[System::Flags]
		public enum class FilterCaps : System::Int32
		{
			ConvolutionMono = D3DPTFILTERCAPS_CONVOLUTIONMONO,

			/// <summary>
			/// Device supports per-stage point-sample filtering for minifying textures.
			/// </summary>
			MinPoint = D3DPTFILTERCAPS_MINFPOINT,

			/// <summary>
			/// Device supports per-stage linear filtering for minifying textures.
			/// </summary>
			MinLinear = D3DPTFILTERCAPS_MINFLINEAR,

			/// <summary>
			/// Device supports per-stage anisotropic filtering for minifying textures.
			/// </summary>
			MinAnisotropic = D3DPTFILTERCAPS_MINFANISOTROPIC,

			/// <summary>
			/// Device supports per-stage pyramidal sample filtering for minifying textures.
			/// </summary>
			MinPyramidalQuad = D3DPTFILTERCAPS_MINFPYRAMIDALQUAD,

			/// <summary>
			/// Device supports per-stage Gaussian quad filtering for minifying textures.
			/// </summary>
			MinGaussianQuad = D3DPTFILTERCAPS_MINFGAUSSIANQUAD,

			/// <summary>
			/// Device supports per-stage point-sample filtering for mipmaps.
			/// </summary>
			MipPoint = D3DPTFILTERCAPS_MIPFPOINT,

			/// <summary>
			/// Device supports per-stage linear filtering for mipmaps.
			/// </summary>
			MipLinear = D3DPTFILTERCAPS_MIPFLINEAR,

			/// <summary>
			/// Device supports per-stage point-sample filtering for magnifying textures.
			/// </summary>
			MagPoint = D3DPTFILTERCAPS_MAGFPOINT,

			/// <summary>
			/// Device supports per-stage linaer filtering for magnifying textures.
			/// </summary>
			MagLinear = D3DPTFILTERCAPS_MAGFLINEAR,

			/// <summary>
			/// Device supports per-stage anisotropic filtering for magnifying textures.
			/// </summary>
			MagAnisotropic = D3DPTFILTERCAPS_MAGFANISOTROPIC,

			/// <summary>
			/// Device supports per-stage pyramidal sample filtering for magnifying textures.
			/// </summary>
			MagPyramidalQuad = D3DPTFILTERCAPS_MAGFPYRAMIDALQUAD,

			/// <summary>
			/// Device supports per-stage Gaussian quad filtering for magnifying textures.
			/// </summary>
			MagGaussianQuad = D3DPTFILTERCAPS_MAGFGAUSSIANQUAD,
		};

		/// <summary>
		/// Device constants that describe the fog mode.
		/// </summary>
		/// <unmanaged>D3DFOGMODE</unmanaged>
		public enum class FogMode : System::Int32
		{
			/// <summary>
			/// No fog effect.
			/// </summary>
			None = D3DFOG_NONE,

			/// <summary>
			/// Fog effect intensifies exponentially.
			/// </summary>
			Exponential = D3DFOG_EXP,

			/// <summary>
			/// Fog effect intesifies exponentially with the square of the distance.
			/// </summary>
			ExponentialSquared = D3DFOG_EXP2,

			/// <summary>
			/// Fog effect intesifies linearly between the start and end points.
			/// </summary>
			Linear = D3DFOG_LINEAR
		};
		
		/// <summary>
		/// Specifies quality options for font rendering.
		/// </summary>
		/// <unmanaged href="ms901140">QUALITY</unmanaged>
		public enum class FontQuality : System::Int32
		{
			/// <summary>
			/// Default quality levels.
			/// </summary>
			Default = DEFAULT_QUALITY,

			/// <summary>
			/// Draft quality.
			/// </summary>
			Draft = DRAFT_QUALITY,

			/// <summary>
			/// Proof quality.
			/// </summary>
			Proof = PROOF_QUALITY,

			/// <summary>
			/// Non-antialiased quality.
			/// </summary>
			NonAntialiased = NONANTIALIASED_QUALITY,

			/// <summary>
			/// Antialiased quality.
			/// </summary>
			Antialiased = ANTIALIASED_QUALITY,

			/// <summary>
			/// Clear type quality.
			/// </summary>
			ClearType = CLEARTYPE_QUALITY,

			/// <summary>
			/// Clear type natural quality.
			/// </summary>
			ClearTypeNatural = CLEARTYPE_NATURAL_QUALITY,
		};

		/// <summary>
		/// Specifies weights for font rendering.
		/// </summary>
		/// <unmanaged href="ms901140">FW</unmanaged>
		public enum class FontWeight : System::Int32
		{
			/// <summary>
			/// The font weight doesn't matter.
			/// </summary>
			DoNotCare = FW_DONTCARE,

			/// <summary>
			/// Make the font thin.
			/// </summary>
			Thin = FW_THIN,

			/// <summary>
			/// Make the font extra light.
			/// </summary>
			ExtraLight = FW_EXTRALIGHT,

			/// <summary>
			/// Make the font ultra light.
			/// </summary>
			UltraLight = FW_ULTRALIGHT,

			/// <summary>
			/// Make the font light.
			/// </summary>
			Light = FW_LIGHT,

			/// <summary>
			/// Use a normal weight.
			/// </summary>
			Normal = FW_NORMAL,

			/// <summary>
			/// Use a regular weight.
			/// </summary>
			Regular = FW_REGULAR,

			/// <summary>
			/// Use a medium weight.
			/// </summary>
			Medium = FW_MEDIUM,

			/// <summary>
			/// Use a semi-bold weight.
			/// </summary>
			SemiBold = FW_SEMIBOLD,

			/// <summary>
			/// Use a demi-bold weight.
			/// </summary>
			DemiBold = FW_DEMIBOLD,

			/// <summary>
			/// Use a bold weight.
			/// </summary>
			Bold = FW_BOLD,

			/// <summary>
			/// Use an extra bold weight.
			/// </summary>
			ExtraBold = FW_EXTRABOLD,

			/// <summary>
			/// Use an ultra bold weight.
			/// </summary>
			UltraBold = FW_ULTRABOLD,

			/// <summary>
			/// Use a heavy weight.
			/// </summary>
			Heavy = FW_HEAVY,

			/// <summary>
			/// Use a black weight.
			/// </summary>
			Black = FW_BLACK,
		};

		/// <summary>
		/// Defines the various types of surface formats.
		/// </summary>
		/// <unmanaged>D3DFORMAT</unmanaged>
		public enum class Format : System::Int32
		{
			/// <summary>
			/// 32-bit surface format using 2 bits for alpha and 10 bits for color components (red, green, blue).
			/// </summary>
			A2R10G10B10 = D3DFMT_A2R10G10B10,

			/// <summary>
			/// 32-bit surface format using 8 bits for each channel (alpha, red, green, blue). 
			/// </summary>
			A8R8G8B8 = D3DFMT_A8R8G8B8,

			/// <summary>
			/// 32-bit surface format using 8 bits for each color component (red, green, blue).
			/// </summary>
			X8R8G8B8 = D3DFMT_X8R8G8B8,

			/// <summary>
			/// 16-bit surface format using 1 bit for alpha and 5 bits for each channel (red, green, blue).
			/// </summary>
			A1R5G5B5 = D3DFMT_A1R5G5B5,

			/// <summary>
			/// 16-bit surface format using 5 bits for each color component (red, green, blue).
			/// </summary>
			X1R5G5B5 = D3DFMT_X1R5G5B5,

			/// <summary>
			/// 16-bit surface format using 5 bits for red and blue and 6 bits for green.
			/// </summary>
			R5G6B5 = D3DFMT_R5G6B5,

			/***/

			/// <summary>
			/// 16-bit z-buffer lockable format with 16 bits for depth.
			/// </summary>
			D16Lockable = D3DFMT_D16_LOCKABLE,

			/// <summary>
			/// 32-bit z-buffer format with 32 bits for depth.
			/// </summary>
			D32 = D3DFMT_D32,

			/// <summary>
			/// 16-bit z-buffer format with 15 bits for depth and 1 bit for stencil.
			/// </summary>
			D15S1 = D3DFMT_D15S1,

			/// <summary>
			/// 32-bit z-buffer format with 24 bits for depth and 8 bits for stencil.
			/// </summary>
			D24S8 = D3DFMT_D24S8,

			/// <summary>
			/// 32-bit z-buffer format with 24 bits for depth.
			/// </summary>
			D24X8 = D3DFMT_D24X8,

			/// <summary>
			/// 32-bit z-buffer format with 24 bits for depth and 4 bits for stencil.
			/// </summary>
			D24X4S4 = D3DFMT_D24X4S4,

			/// <summary>
			/// 32-bit z-buffer lockable format with 32 bits for depth (in standard IEEE floating point format).
			/// </summary>
			D32SingleLockable = D3DFMT_D32F_LOCKABLE,

			/// <summary>
			/// 32-bit z-buffer format with 24 bits for depth (in 24-bit floating point format) and 8 bits for stencil.
			/// </summary>
			D24SingleS8 = D3DFMT_D24FS8,

			/// <summary>
			/// Lockable 32-bit depth buffer. Available for Direct3D9Ex only.
			/// </summary>
			D32Lockable = D3DFMT_D32_LOCKABLE,

			/// <summary>
			/// Lockable 8-bit stencil buffer. Available for Direct3D9Ex only.
			/// </summary>
			S8Lockable = D3DFMT_S8_LOCKABLE,

			/// <summary>
			/// 16-bit z-buffer format with 16 bits for depth.
			/// </summary>
			D16 = D3DFMT_D16,

			/***/

			/// <summary>
			/// Describes a vertex buffer surface.
			/// </summary>
			VertexData = D3DFMT_VERTEXDATA,

			/// <summary>
			/// 16-bit index buffer bit depth.
			/// </summary>
			Index16 = D3DFMT_INDEX16,

			/// <summary>
			/// 32-bit index buffer bit depth.
			/// </summary>
			Index32 = D3DFMT_INDEX32,

			/***/		

			/// <summary>
			/// DXT1 compression texture format.
			/// </summary>
			Dxt1 = D3DFMT_DXT1,

			/// <summary>
			/// DXT2 compression texture format.
			/// </summary>
			Dxt2 = D3DFMT_DXT2,

			/// <summary>
			/// DXT3 compression texture format.
			/// </summary>
			Dxt3 = D3DFMT_DXT3,

			/// <summary>
			/// DXT4 compression texture format.
			/// </summary>
			Dxt4 = D3DFMT_DXT4,

			/// <summary>
			/// DXT5 compression texture format.
			/// </summary>
			Dxt5 = D3DFMT_DXT5,

			/***/

			/// <summary>
			/// 16-bit floating point format using 16 bits for the red channel.
			/// </summary>
			R16F = D3DFMT_R16F,

			/// <summary>
			/// 32-bit floating point format using 16 bits for the red channel and 16 bits for the green channel.
			/// </summary>
			G16R16F = D3DFMT_G16R16F,

			/// <summary>
			/// 64-bit floating point format using 16 bits for each channel (alpha, blue, green, red).
			/// </summary>
			A16B16G16R16F = D3DFMT_A16B16G16R16F,

			/***/

			/// <summary>
			/// Multielement texture.
			/// </summary>
			Multi2Argb8 = D3DFMT_MULTI2_ARGB8,

			/// <summary>
			/// 16-bit packed RGB format consisting of pixel pairs to express color.
			/// </summary>
			G8R8_G8B8 = D3DFMT_G8R8_G8B8,

			/// <summary>
			/// 16-bit packed RGB format consisting of pixel pairs to express color.
			/// </summary>
			R8G8_B8G8 = D3DFMT_R8G8_B8G8,

			/// <summary>
			/// UYUV format (PC98 compliance).
			/// </summary>
			Uyvy = D3DFMT_UYVY,

			/// <summary>
			/// YUY2 format (PC98 compliance).
			/// </summary>
			Yuy2 = D3DFMT_YUY2,

			/***/

			/// <summary>
			/// 32-bit floating point format using 32 bits for the red channel.
			/// </summary>
			R32F = D3DFMT_R32F,

			/// <summary>
			/// 64-bit floating point format using 32 bits for the red channel and 32 bits for the green channel.
			/// </summary>
			G32R32F = D3DFMT_G32R32F,

			/// <summary>
			/// 128-bit floating point format using 32 bits for each channel (alpha, blue, green, red).
			/// </summary>
			A32B32G32R32F = D3DFMT_A32B32G32R32F,

			/***/

			/// <summary>
			/// 16-bit bump map format using 6 bits for luminance and 5 bits each for V and U.
			/// </summary>
			L6V5U5 = D3DFMT_L6V5U5,

			/// <summary>
			/// 32-bit bump map format using 8 bits for each channel (luminance, V, U).
			/// </summary>
			X8L8V8U8 = D3DFMT_X8L8V8U8,

			/// <summary>
			/// 32-bit bump map format using 2 bits for alpha and 10 bits each for W, V, and U.
			/// </summary>
			A2W10V10U10 = D3DFMT_A2W10V10U10,

			/***/

			/// <summary>
			/// 16-bit bump map format using 8 bits each for U and V.
			/// </summary>
			V8U8 = D3DFMT_V8U8,

			/// <summary>
			/// 32-bit bump map format using 8 bits for each channel (Q, W, V, U).
			/// </summary>
			Q8W8V8U8 = D3DFMT_Q8W8V8U8,

			/// <summary>
			/// 32-bit bump map format using 16 bits each for V and U.
			/// </summary>
			V16U16 = D3DFMT_V16U16,

			/// <summary>
			/// 64-bit bump map format using 16 bits for each channel (Q, W, V, U).
			/// </summary>
			Q16W16V16U16 = D3DFMT_Q16W16V16U16,

			/// <summary>
			/// 16-bit normal compression format.
			/// </summary>
			CxV8U8 = D3DFMT_CxV8U8,

			/***/

			/// <summary>
			/// 24-bit RGB format using 8 bits per channel.
			/// </summary>
			R8G8B8 = D3DFMT_R8G8B8,

			/// <summary>
			/// 16-bit ARGB format using 4 bits for each channel.
			/// </summary>
			A4R4G4B4 = D3DFMT_A4R4G4B4,

			/// <summary>
			/// 8-bit RGB format using 3 bits for red and green and 2 bits for blue.
			/// </summary>
			R3G3B2 = D3DFMT_R3G3B2,

			/// <summary>
			/// 8-bit alpha format.
			/// </summary>
			A8 = D3DFMT_A8,

			/// <summary>
			/// 16-bit ARGB format using 8 bits for alpha, 3 bits for red and green, and 2 bits for blue.
			/// </summary>
			A8R3G3B2 = D3DFMT_A8R3G3B2,

			/// <summary>
			/// 16-bit RGB format using 4 bits for each color channel.
			/// </summary>
			X4R4G4B4 = D3DFMT_X4R4G4B4,

			/// <summary>
			/// 32-bit ABGR format using 2 bits for alpha and 10 bits for each color component.
			/// </summary>
			A2B10G10R10 = D3DFMT_A2B10G10R10,

			/// <summary>
			/// 32-bit ABGR format using 8 bits for each channel.
			/// </summary>
			A8B8G8R8 = D3DFMT_A8B8G8R8,

			/// <summary>
			/// 32-bit BGR format using 8 bits for each color channel.
			/// </summary>
			X8B8G8R8 = D3DFMT_X8B8G8R8,

			/// <summary>
			/// 32-bit pixel format using 16 bits each for green and red.
			/// </summary>
			G16R16 = D3DFMT_G16R16,

			/// <summary>
			/// 64-bit pixel ABGR format using 16 bits for each channel.
			/// </summary>
			A16B16G16R16 = D3DFMT_A16B16G16R16,

			/// <summary>
			/// 8-bit color indexed with 8 bits of alpha.
			/// </summary>
			A8P8 = D3DFMT_A8P8,

			/// <summary>
			/// 8-bit color indexed.
			/// </summary>
			P8 = D3DFMT_P8,

			/// <summary>
			/// 16-bit luminance.
			/// </summary>
			L16 = D3DFMT_L16,

			/// <summary>
			/// 8-bit luminance.
			/// </summary>
			L8 = D3DFMT_L8,

			/// <summary>
			/// 16-bit format using 8 bits each for alpha and luminance.
			/// </summary>
			A8L8 = D3DFMT_A8L8,

			/// <summary>
			/// 8-bit format using 4 bits each for alpha and luminance.
			/// </summary>
			A4L4 = D3DFMT_A4L4,

			/// <summary>
			/// 1-bit monochrome format. Available for Direct3D9Ex only.
			/// </summary>
			A1 = D3DFMT_A1,

			/// <summary>
			/// Binary buffer format indicating that the data has no inherent type. Available for Direct3D9Ex only.
			/// </summary>
			BinaryBuffer = D3DFMT_BINARYBUFFER,

			/***/

			/// <summary>
			/// Surface format for Render-To-Vertex-Buffer (R2VB).
			/// </summary>
			ATI_R2VB = R2VB_FOURCC_R2VB,

			/***/

			/// <summary>
			/// Unknown surface format.
			/// </summary>
			Unknown = D3DFMT_UNKNOWN,
		};

		/// <summary>
		/// Defines constants for query issues.
		/// </summary>
		/// <unmanaged>D3DISSUE</unmanaged>
		[System::Flags]
		public enum class Issue
		{
			/// <summary>
			/// Start a query issue.
			/// </summary>
			Begin = D3DISSUE_BEGIN,

			/// <summary>
			/// End a query issue.
			/// </summary>
			End = D3DISSUE_END
		};
		
		/// <summary>
		/// Defines possible light types.
		/// </summary>
		/// <unmanaged>D3DLIGHTTYPE</unmanaged>
		public enum class LightType : System::Int32
		{
			/// <summary>
			/// Light is a point source. The light has a position in space and radiates in all directions.
			/// </summary>
			Point = D3DLIGHT_POINT,

			/// <summary>
			/// Light is a spotlight source. Illumination is limited to a cone.
			/// </summary>
			Spot = D3DLIGHT_SPOT,

			/// <summary>
			/// Light is a directional light source. This is equivalent to using a point light source at an infinite distance.
			/// </summary>
			Directional = D3DLIGHT_DIRECTIONAL,
		};

		/// <summary>
		/// Specifies the line drawing capabilities of a device.
		/// </summary>
		/// <unmanaged>D3DLINECAPS</unmanaged>
		[System::Flags]
		public enum class LineCaps : System::Int32
		{
			/// <summary>
			/// Supports texture-mapping.
			/// </summary>
			Texture = D3DLINECAPS_TEXTURE,

			/// <summary>
			/// Supports depth test.
			/// </summary>
			DepthTest = D3DLINECAPS_ZTEST,

			/// <summary>
			/// Supports source-blending.
			/// </summary>
			Blend = D3DLINECAPS_BLEND,

			/// <summary>
			/// Supports alpha test comparisons.
			/// </summary>
			AlphaCompare = D3DLINECAPS_ALPHACMP,

			/// <summary>
			/// Supports fog.
			/// </summary>
			Fog = D3DLINECAPS_FOG,

			/// <summary>
			/// Supports antialiasing.
			/// </summary>
			Antialias = D3DLINECAPS_ANTIALIAS,
		};
		
		/// <summary>
		/// Defines flags for buffer locking.
		/// </summary>
		/// <unmanaged>D3DLOCK</unmanaged>
		[System::Flags]
		public enum class LockFlags : System::Int32
		{
			/// <summary>
			/// The application discards all memory within the locked region.
			/// </summary>
			Discard = D3DLOCK_DISCARD,

			/// <summary>
			/// Allows the application to gain back CPU cycles if the driver cannot lock the surface immediately.
			/// </summary>
			DoNotWait = D3DLOCK_DONOTWAIT,

			/// <summary>
			/// Prevents any changes to the dirty state of the resource.
			/// </summary>
			NoDirtyUpdate = D3DLOCK_NO_DIRTY_UPDATE,

			/// <summary>
			/// No locking flags specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Indicates that the last set of data written will not be modified during this lock call.
			/// </summary>
			NoOverwrite = D3DLOCK_NOOVERWRITE,

			/// <summary>
			/// Prevents the allocation of a system wide resource lock.
			/// </summary>
			NoSystemLock = D3DLOCK_NOSYSLOCK,

			/// <summary>
			/// The application will not write to the buffer.
			/// </summary>
			ReadOnly = D3DLOCK_READONLY,

			// this just exists for ProcessVertices
			DoNotCopyData = D3DPV_DONOTCOPYDATA,
		};
		
		/// <summary>
		/// Defines the levels of full-scene multisampling that the device can apply.
		/// </summary>
		/// <unmanaged>D3DMULTISAMPLE_TYPE</unmanaged>
		public enum class MultisampleType : System::Int32
		{
			/// <summary>
			/// No level of multisampling is available.
			/// </summary>
			None = D3DMULTISAMPLE_NONE,

			/// <summary>
			/// Enables the multisampling quality value.
			/// </summary>
			NonMaskable = D3DMULTISAMPLE_NONMASKABLE,

			/// <summary>
			/// Level of full-scene multisampling using 2 samples.
			/// </summary>
			TwoSamples = D3DMULTISAMPLE_2_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 3 samples.
			/// </summary>
			ThreeSamples = D3DMULTISAMPLE_3_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 4 samples.
			/// </summary>
			FourSamples = D3DMULTISAMPLE_4_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 5 samples.
			/// </summary>
			FiveSamples = D3DMULTISAMPLE_5_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 6 samples.
			/// </summary>
			SixSamples = D3DMULTISAMPLE_6_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 7 samples.
			/// </summary>
			SevenSamples = D3DMULTISAMPLE_7_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 8 samples.
			/// </summary>
			EightSamples = D3DMULTISAMPLE_8_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 9 samples.
			/// </summary>
			NineSamples = D3DMULTISAMPLE_9_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 10 samples.
			/// </summary>
			TenSamples = D3DMULTISAMPLE_10_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 11 samples.
			/// </summary>
			ElevenSamples = D3DMULTISAMPLE_11_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 12 samples.
			/// </summary>
			TwelveSamples = D3DMULTISAMPLE_12_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 13 samples.
			/// </summary>
			ThirteenSamples = D3DMULTISAMPLE_13_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 14 samples.
			/// </summary>
			FourteenSamples = D3DMULTISAMPLE_14_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 15 samples.
			/// </summary>
			FifteenSamples = D3DMULTISAMPLE_15_SAMPLES,

			/// <summary>
			/// Level of full-scene multisampling using 16 samples.
			/// </summary>
			SixteenSamples = D3DMULTISAMPLE_16_SAMPLES
		};

		/// <summary>
		/// Defines whether the current tessellation mode is discrete or continuous.
		/// </summary>
		/// <unmanaged>D3DPATCHEDGESTYLE</unmanaged>
		public enum class PatchEdgeStyle : System::Int32
		{
			/// <summary>
			/// Discrete edge style.
			/// </summary>
			Discrete = D3DPATCHEDGE_DISCRETE,

			/// <summary>
			/// Continuous edge style.
			/// </summary>
			Continuous = D3DPATCHEDGE_CONTINUOUS
		};
		
		/// <summary>
		/// Defines pitch and family settings for fonts.
		/// </summary>
		/// <unmanaged href="ms901140">(various font constants)</unmanaged>
		[System::Flags]
		public enum class PitchAndFamily : System::Int32
		{
			/// <summary>
			/// Default pitch.
			/// </summary>
			Default = DEFAULT_PITCH,

			/// <summary>
			/// Fixed pitch.
			/// </summary>
			Fixed = FIXED_PITCH,

			/// <summary>
			/// Variable pitch.
			/// </summary>
			Variable = VARIABLE_PITCH,

			/// <summary>
			/// Mono pitch.
			/// </summary>
			Mono = MONO_FONT,

			/// <summary>
			/// The font family doesn't matter.
			/// </summary>
			DontCare = FF_DONTCARE,

			/// <summary>
			/// Use the Roman family.
			/// </summary>
			Roman = FF_ROMAN,

			/// <summary>
			/// Use the Swiss family.
			/// </summary>
			Swiss = FF_SWISS,

			/// <summary>
			/// Use the Modern family.
			/// </summary>
			Modern = FF_MODERN,

			/// <summary>
			/// Use the Script family.
			/// </summary>
			Script = FF_SCRIPT,

			/// <summary>
			/// Use the Decorative family.
			/// </summary>
			Decorative = FF_DECORATIVE,
		};

		/// <summary>
		/// Specifies pixel shader capabilities supported by the device.
		/// </summary>
		/// <unmanaged>D3DPS20CAPS</unmanaged>
		[System::Flags]
		public enum class PixelShaderCaps : System::Int32
		{
			/// <summary>
			/// No extra pixel shader capabilities specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Arbitrary swizzling is supported.
			/// </summary>
			ArbitrarySwizzle = D3DPS20CAPS_ARBITRARYSWIZZLE,

			/// <summary>
			/// Gradient instructions are supported.
			/// </summary>
			GradientInstructions = D3DPS20CAPS_GRADIENTINSTRUCTIONS,

			/// <summary>
			/// Instruction predication is supported.
			/// </summary>
			Predication = D3DPS20CAPS_PREDICATION,

			/// <summary>
			/// There is no limit on the number of dependent reads.
			/// </summary>
			NoDependentReadLimit = D3DPS20CAPS_NODEPENDENTREADLIMIT,

			/// <summary>
			/// There is no limit on the number of texture instructions.
			/// </summary>
			NoTextureInstructionLimit = D3DPS20CAPS_NOTEXINSTRUCTIONLIMIT,
		};
		
		/// <summary>
		/// Defines the memory class that holds the buffers for a resource.
		/// </summary>
		/// <unmanaged>D3DPOOL</unmanaged>
		public enum class Pool : System::Int32
		{
			/// <summary>
			/// Resources are placed in the memory pool most appropriate for the set of usages requested for the resource.
			/// </summary>
			Default = D3DPOOL_DEFAULT,

			/// <summary>
			/// Resources are copied automatically to device-accessable memory as needed. Managed resources are
			/// backed by system memory and do not need to be recreated when the device is lost.
			/// </summary>
			Managed = D3DPOOL_MANAGED,

			/// <summary>
			/// Resources are placed in system memory that is not accessable by the device. These resources
			/// do not need to be recreated when the device is lost.
			/// </summary>
			SystemMemory = D3DPOOL_SYSTEMMEM,

			/// <summary>
			/// Resources are placed in scratch memory. These resources cannot be used as textures or render
			/// targets.
			/// </summary>
			Scratch = D3DPOOL_SCRATCH,
		};

		/// <summary>
		/// Defines precision levels for font rendering.
		/// </summary>
		/// <unmanaged href="cc215248">OutPrecision</unmanaged>
		public enum class Precision : System::Int32
		{
			/// <summary>
			/// Default precision.
			/// </summary>
			Default = OUT_DEFAULT_PRECIS,

			/// <summary>
			/// String-level precision.
			/// </summary>
			String = OUT_STRING_PRECIS,

			/// <summary>
			/// Character-level precision.
			/// </summary>
			Character = OUT_CHARACTER_PRECIS,

			/// <summary>
			/// Stroke-level precision.
			/// </summary>
			Stroke = OUT_STROKE_PRECIS,

			/// <summary>
			/// TrueType precision.
			/// </summary>
			TrueType = OUT_TT_PRECIS,

			/// <summary>
			/// Device precision.
			/// </summary>
			Device = OUT_DEVICE_PRECIS,

			/// <summary>
			/// Raster precision.
			/// </summary>
			Raster = OUT_RASTER_PRECIS,

			/// <summary>
			/// TrueType only precision.
			/// </summary>
			TrueTypeOnly = OUT_TT_ONLY_PRECIS,

			/// <summary>
			/// Outline precision.
			/// </summary>
			Outline = OUT_OUTLINE_PRECIS,

			/// <summary>
			/// Screen outline precision.
			/// </summary>
			ScreenOutline = OUT_SCREEN_OUTLINE_PRECIS,

			/// <summary>
			/// PostScript only precision.
			/// </summary>
			PostScriptOnly = OUT_PS_ONLY_PRECIS,
		};
		
		/// <summary>
		/// Defines flags for presentation calls.
		/// </summary>
		/// <unmanaged>D3DPRESENT</unmanaged>
		[System::Flags]
		public enum class Present : System::Int32
		{
			/// <summary>
			/// No extra presentation flags.
			/// </summary>
			None = 0,

			/// <summary>
			/// Use the front buffer as both the source and the target surface during rendering.
			/// Available for Direct3D9Ex only.
			/// </summary>
			DoNotFlip = D3DPRESENT_DONOTFLIP,

			/// <summary>
			/// Specifies that the application should not wait for rendering to finish.
			/// </summary>
			DoNotWait = D3DPRESENT_DONOTWAIT,

			/// <summary>
			/// The application will discard all previously queued frames and present the current frame next.
			/// Available in Direct3D9Ex only.
			/// </summary>
			FlipRestart = D3DPRESENT_FLIPRESTART,

			/// <summary>
			/// Clips the rendered content to the monitor/device that the adapter is targetting. Available in
			/// Direct3D9Ex only.
			/// </summary>
			VideoRestrictToMonitor = D3DPRESENT_VIDEO_RESTRICT_TO_MONITOR,

			/// <summary>
			/// The content of the backbuffer to be presented is in linear color space.
			/// </summary>
			LinearContent = D3DPRESENT_LINEAR_CONTENT,

			/// <summary>
			/// PresentInterval.Immediate is enforced on this Present call. This flag can only be specified when
			/// using SwapEffect.FlipEx. Windowed and fullscreen presentation behaviors are the same. This is
			/// especially useful for media apps that want to discard frames that have been detected as late and
			/// present subsequent frames at composition time. An invalid parameter error will be returned if this
			/// flag is improperly specified. When multiple consecutive frames with Present.ForceImmediate are
			/// queued, only the last frame is displayed, for both windowed and fullscreen presentation.
			/// Available in Direct3D9Ex on Windows 7 only
			/// </summary>
			ForceImmediate = D3DPRESENT_FORCEIMMEDIATE,

			/// <summary>
			/// Updates the overlay position or the colorkey data without causing an actual flip and without
			/// changing the duration with which the image is displayed. Available in Direct3D9Ex only.
			/// </summary>
			UpdateOverlayOnly = D3DPRESENT_UPDATEOVERLAYONLY,

			/// <summary>
			/// Turns off the overlay hardware. Available in Direct3D9Ex only.
			/// </summary>
			HideOverlay = D3DPRESENT_HIDEOVERLAY,

			/// <summary>
			/// Redraws the colorkey data. Available in Direct3D9Ex only.
			/// </summary>
			UpdateColorKey = D3DPRESENT_UPDATECOLORKEY
		};
		
		/// <summary>
		/// Specifies possible presentation flags.
		/// </summary>
		/// <unmanaged>D3DPRESENTFLAG</unmanaged>
		[System::Flags]
		public enum class PresentFlags : System::Int32
		{
			/// <summary>
			/// Clips the presentation to the device.
			/// </summary>
			DeviceClip = D3DPRESENTFLAG_DEVICECLIP,

			/// <summary>
			/// Discards the contents of the depth/stencil surface after each presentation.
			/// </summary>
			DiscardDepthStencil = D3DPRESENTFLAG_DISCARD_DEPTHSTENCIL,

			/// <summary>
			/// Set when the application requires the ability to lock the back buffer.
			/// </summary>
			LockableBackBuffer = D3DPRESENTFLAG_LOCKABLE_BACKBUFFER,

			/// <summary>
			/// No extra presentation flags specified.
			/// </summary>
			None = 0,

			/// <summary>
			/// Specifies that the application will perform its own display rotation. Available in Direct3D9Ex only.
			/// </summary>
			NoAutoRotate = D3DPRESENTFLAG_NOAUTOROTATE,

			/// <summary>
			/// Allows the device to use invalid display modes as if they were valid. Available in Direct3D9Ex only.
			/// </summary>
			UnprunedMode = D3DPRESENTFLAG_UNPRUNEDMODE,

			/// <summary>
			/// This is a hint to the driver that the back buffers will contain video data.
			/// </summary>
			Video = D3DPRESENTFLAG_VIDEO,

			/// <summary>
			/// Indicates limited range RGB. In limited range RGB, the RGB range is compressed such that
			/// 16:16:16 is black and 235:235:235 is white. Available in Direct3D9Ex only.
			/// </summary>
			OverlayLimitedRgb = D3DPRESENTFLAG_OVERLAY_LIMITEDRGB,

			/// <summary>
			/// Indicates that the overlay is BT.709, for high-definition TV (HDTV). Available in Direct3D9Ex only.
			/// </summary>
			OverlayYCbCr_BT709 = D3DPRESENTFLAG_OVERLAY_YCbCr_BT709,

			/// <summary>
			/// Indicates that the overlay is extended YCbCr (xvYCC). Available in Direct3D9Ex only.
			/// </summary>
			OverlayYCbCr_xvYCC = D3DPRESENTFLAG_OVERLAY_YCbCr_xvYCC,

			/// <summary>
			/// Indicates that the swapchain contains protected content and automatically causes
			/// the runtime to restrict access to the swapchain so that only the Desktop Windows Manager
			/// (DWM) can use the swapchain. Available in Direct3D9Ex only.
			/// </summary>
			RestrictedContent = D3DPRESENTFLAG_RESTRICTED_CONTENT,

			/// <summary>
			/// Indicates that the driver should restrict access to any shared
			/// resources that are created for DWM interaction. The caller must create an authenticated
			/// channel with the driver. The driver should then allow access to processes that attempt to
			/// open those shared resources. Available in Direct3D9Ex only.
			/// </summary>
			RestrictSharedResourceDriver = D3DPRESENTFLAG_RESTRICT_SHARED_RESOURCE_DRIVER
		};

		/// <summary>
		/// Specifies presentation intervals.
		/// </summary>
		/// <unmanaged>D3DPRESENT</unmanaged>
		[System::Flags]
		public enum class PresentInterval : System::Int32
		{
			/// <summary>
			/// The device will wait for the vertical retrace period.
			/// </summary>
			Default = D3DPRESENT_INTERVAL_DEFAULT,

			/// <summary>
			/// The device will present immediately without waiting for the refresh.
			/// </summary>
			Immediate = static_cast<int>( D3DPRESENT_INTERVAL_IMMEDIATE ),

			/// <summary>
			/// The device will wait for the vertical retrace period.
			/// </summary>
			One = D3DPRESENT_INTERVAL_ONE,

			/// <summary>
			/// Present operations will not be affected more than twice every screen refresh.
			/// </summary>
			Two = D3DPRESENT_INTERVAL_TWO,

			/// <summary>
			/// Present operations will not be affected more than three times every screen refresh.
			/// </summary>
			Three = D3DPRESENT_INTERVAL_THREE,

			/// <summary>
			/// Present operations will not be affected more than four times every screen refresh.
			/// </summary>
			Four = D3DPRESENT_INTERVAL_FOUR,
		};

		/// <summary>
		/// Specifies miscellaneous capabilities for primitive rendering supported by the device.
		/// </summary>
		/// <unmanaged>D3DPMISCCAPS</unmanaged>
		[System::Flags]
		public enum class PrimitiveMiscCaps : System::Int32
		{
			/// <summary>
			/// Device can enabled and disable modification of the depth buffer on pixel operations.
			/// </summary>
			MaskZ = D3DPMISCCAPS_MASKZ,

			/// <summary>
			/// The driver does not perform triangle culling.
			/// </summary>
			CullNone = D3DPMISCCAPS_CULLNONE,

			/// <summary>
			/// The driver supports clockwise culling.
			/// </summary>
			CullCW = D3DPMISCCAPS_CULLCW,

			/// <summary>
			/// The driver supports counterclockwise culling.
			/// </summary>
			CullCCW = D3DPMISCCAPS_CULLCCW,

			/// <summary>
			/// Device supports per-channel writes of the color buffer.
			/// </summary>
			ColorWriteEnable = D3DPMISCCAPS_COLORWRITEENABLE,

			/// <summary>
			/// Device correctly clips scaled points of size greater than 1.0.
			/// </summary>
			ClipPlanesScaledPoints = D3DPMISCCAPS_CLIPPLANESCALEDPOINTS,

			/// <summary>
			/// Device clips post-transformed vertex primitives.
			/// </summary>
			ClipTLVertices = D3DPMISCCAPS_CLIPTLVERTS,

			/// <summary>
			/// Device supports temporary texture arguments.
			/// </summary>
			TssArgTemp = D3DPMISCCAPS_TSSARGTEMP,

			/// <summary>
			/// Device supports alpha blending operations.
			/// </summary>
			BlendOperation = D3DPMISCCAPS_BLENDOP,

			/// <summary>
			/// A reference device that does not render.
			/// </summary>
			NullReference = D3DPMISCCAPS_NULLREFERENCE,

			/// <summary>
			/// Device supports independent write masks for multiple render targets.
			/// </summary>
			IndependentWriteMasks = D3DPMISCCAPS_INDEPENDENTWRITEMASKS,

			/// <summary>
			/// Device supports per-stage constants.
			/// </summary>
			PerStageConstant = D3DPMISCCAPS_PERSTAGECONSTANT,

			/// <summary>
			/// Device supports conversion to sRGB after blending. Available for Direct3D9Ex only.
			/// </summary>
			PostBlendSrgbConvert = D3DPMISCCAPS_POSTBLENDSRGBCONVERT,

			/// <summary>
			/// Device supports fog and specular alpha.
			/// </summary>
			FogAndSpecularAlpha = D3DPMISCCAPS_FOGANDSPECULARALPHA,

			/// <summary>
			/// Device supports separate blend settings for the alpha channel.
			/// </summary>
			SeparateAlphaBlend = D3DPMISCCAPS_SEPARATEALPHABLEND,

			/// <summary>
			/// Device supports different bit depths for multiple render targets.
			/// </summary>
			MrtIndependentBitDepths = D3DPMISCCAPS_MRTINDEPENDENTBITDEPTHS,

			/// <summary>
			/// Device supports post pixel shader operations for multiple render targets.
			/// </summary>
			MrtPostPixelShaderBlending = D3DPMISCCAPS_MRTPOSTPIXELSHADERBLENDING,

			/// <summary>
			/// Device clamps fog blend factor per vertex.
			/// </summary>
			FogVertexClamped = D3DPMISCCAPS_FOGVERTEXCLAMPED
		};
		
		/// <summary>
		/// Defines the primitives supported by Direct3D.
		/// </summary>
		/// <unmanaged>D3DPRIMITIVETYPE</unmanaged>
		public enum class PrimitiveType : System::Int32
		{
			/// <summary>
			/// Renders the vertices as a collection of isolated points.
			/// </summary>
			PointList = D3DPT_POINTLIST,

			/// <summary>
			/// Renders the vertices as a list of isolated line segments.
			/// </summary>
			LineList = D3DPT_LINELIST,

			/// <summary>
			/// Renders the vertices as a single polyline.
			/// </summary>
			LineStrip = D3DPT_LINESTRIP,

			/// <summary>
			/// Renders the vertices as a sequence of isolated triangles.
			/// </summary>
			TriangleList = D3DPT_TRIANGLELIST,

			/// <summary>
			/// Renders the vertices as a triangle strip.
			/// </summary>
			TriangleStrip = D3DPT_TRIANGLESTRIP,

			/// <summary>
			/// Renders the vertices as a triangle fan.
			/// </summary>
			TriangleFan = D3DPT_TRIANGLEFAN
		};

		/// <summary>
		/// Identifies possible query types.
		/// </summary>
		/// <unmanaged>D3DQUERYTYPE</unmanaged>
		public enum class QueryType : System::Int32
		{
			/// <summary>
			/// Query for driver hints about data layout for vertex caching.
			/// </summary>
			VCache = D3DQUERYTYPE_VCACHE,

			/// <summary>
			/// Query the resource manager.
			/// </summary>
			ResourceManager = D3DQUERYTYPE_RESOURCEMANAGER,

			/// <summary>
			/// Query vertex statistics.
			/// </summary>
			VertexStats = D3DQUERYTYPE_VERTEXSTATS,

			/// <summary>
			/// Query for any and all asynchronous events that have been issued from API calls.
			/// </summary>
			Event = D3DQUERYTYPE_EVENT,

			/// <summary>
			/// Query for occluded pixels.
			/// </summary>
			Occlusion = D3DQUERYTYPE_OCCLUSION,

			/// <summary>
			/// Queryies the 64-bit timestamp.
			/// </summary>
			Timestamp = D3DQUERYTYPE_TIMESTAMP,

			/// <summary>
			/// Notifies the application that the timestamp frequency has changed.
			/// </summary>
			TimestampDisjoint = D3DQUERYTYPE_TIMESTAMPDISJOINT,

			/// <summary>
			/// Queries the device to see if timestamp frequencies can change mid-call.
			/// </summary>
			TimestampFreq = D3DQUERYTYPE_TIMESTAMPFREQ,

			/// <summary>
			/// Query for pipeline timings.
			/// </summary>
			PipelineTimings = D3DQUERYTYPE_PIPELINETIMINGS,

			/// <summary>
			/// Query for interface timings.
			/// </summary>
			InterfaceTimings = D3DQUERYTYPE_INTERFACETIMINGS,

			/// <summary>
			/// Query for vertex shader timings.
			/// </summary>
			VertexTimings = D3DQUERYTYPE_VERTEXTIMINGS,

			/// <summary>
			/// Query for pixel shader timings.
			/// </summary>
			PixelTimings = D3DQUERYTYPE_PIXELTIMINGS,

			/// <summary>
			/// Query for bandwidth timings.
			/// </summary>
			BandwidthTimings = D3DQUERYTYPE_BANDWIDTHTIMINGS,

			/// <summary>
			/// Measure the cache hit-rate performance for textures and indexed vertices.
			/// </summary>
			CacheUtilization = D3DQUERYTYPE_CACHEUTILIZATION,
		};

		/// <summary>
		/// Specifies sampler values for Render-To-Vertex-Buffer (R2VB).
		/// </summary>
		/// <unmanaged>R2VB_VSMP</unmanaged>
		public enum class R2VBSampler : System::Int32
		{
			/// <summary>
			/// Override the stream with displacement map sampler.
			/// </summary>
			OverrideDMap = R2VB_VSMP_OVR_DMAP,

			/// <summary>
			/// Override the stream with vertex texture 0 sampler.
			/// </summary>
			OverrideVtx0 = R2VB_VSMP_OVR_VTX0,

			/// <summary>
			/// Override the stream with vertex texture 1 sampler.
			/// </summary>
			OverrideVtx1 = R2VB_VSMP_OVR_VTX1,

			/// <summary>
			/// Override the stream with vertex texture 2 sampler.
			/// </summary>
			OverrideVtx2 = R2VB_VSMP_OVR_VTX2,

			/// <summary>
			/// Override the stream with vertex texture 3 sampler.
			/// </summary>
			OverrideVtx3 = R2VB_VSMP_OVR_VTX3,

			/// <summary>
			/// Number of available texture samplers.
			/// </summary>
			Count = R2VB_VSMP_NUM,
		};

		/// <summary>
		/// Specifies information about raster capabilities of the device.
		/// </summary>
		/// <unmanaged>D3DPRASTERCAPS</unmanaged>
		[System::Flags]
		public enum class RasterCaps : System::Int32
		{
			/// <summary>
			/// Device can dither to improve color resolution.
			/// </summary>
			Dither = D3DPRASTERCAPS_DITHER,

			/// <summary>
			/// Device can perform Z-test operations.
			/// </summary>
			DepthTest = D3DPRASTERCAPS_ZTEST,

			/// <summary>
			/// Device calculates the fog value during the lighting operation and interpolates the value during rasterization.
			/// </summary>
			FogVertex = D3DPRASTERCAPS_FOGVERTEX,

			/// <summary>
			/// Device calculates the fog value by referring to a lookup table.
			/// </summary>
			FogTable = D3DPRASTERCAPS_FOGTABLE,

			/// <summary>
			/// Device supports level-of-detail bias adjustments.
			/// </summary>
			MipMapLodBias = D3DPRASTERCAPS_MIPMAPLODBIAS,

			/// <summary>
			/// Device can perform hidden surface removal (HSR) without requiring the application to
			/// sort polygons and without requiring the allocation of a depth buffer.
			/// </summary>
			ZBufferLessHsr = D3DPRASTERCAPS_ZBUFFERLESSHSR,

			/// <summary>
			/// Device supports range-based fog.
			/// </summary>
			FogRange = D3DPRASTERCAPS_FOGRANGE,

			/// <summary>
			/// Device supports anisotropic filtering.
			/// </summary>
			Anisotropy = D3DPRASTERCAPS_ANISOTROPY,

			/// <summary>
			/// Device supports depth buffering using w.
			/// </summary>
			WBuffer = D3DPRASTERCAPS_WBUFFER,

			/// <summary>
			/// Device supports w-based fog.
			/// </summary>
			WFog = D3DPRASTERCAPS_WFOG,

			/// <summary>
			/// Device supports z-based fog.
			/// </summary>
			ZFog = D3DPRASTERCAPS_ZFOG,

			/// <summary>
			/// Device iterates colors perspective correctly.
			/// </summary>
			ColorPerspective = D3DPRASTERCAPS_COLORPERSPECTIVE,

			/// <summary>
			/// Device supports scissor testing.
			/// </summary>
			ScissorTest = D3DPRASTERCAPS_SCISSORTEST,

			/// <summary>
			/// Device performs true slope-scale depth bias.
			/// </summary>
			SlopeScaleDepthBias = D3DPRASTERCAPS_SLOPESCALEDEPTHBIAS,

			/// <summary>
			/// Device supports legacy depth bias.
			/// </summary>
			DepthBias = D3DPRASTERCAPS_DEPTHBIAS,

			/// <summary>
			/// Device supports toggling multisampling on and off.
			/// </summary>
			MultisampleToggle = D3DPRASTERCAPS_MULTISAMPLE_TOGGLE,
		};
		
		/// <summary>
		/// Render states define set-up states for all kinds of vertex and pixel processing. 
		/// Some render states set up vertex processing, and some set up pixel processing.
		/// Render states can be saved and restored using stateblocks.
		/// </summary>
		/// <unmanaged>D3DRENDERSTATETYPE</unmanaged>
		public enum class RenderState : System::Int32
		{
			/// <summary>
			/// Depth-buffering state. Use values from <see cref="SlimDX::Direct3D9::ZBufferType"/> to set this state. The
			/// default value for this state is <see cref="SlimDX::Direct3D9::ZBufferType"/>.UseZBuffer if a depth stencil was created
			/// along with the device, or <see cref="SlimDX::Direct3D9::ZBufferType"/>.DontUseZBuffer otherwise.
			/// </summary>
			ZEnable = D3DRS_ZENABLE,

			/// <summary>
			/// Defines the current fill mode of the device. Use values from <see cref="FillMode"/> to set this state.
			/// The default value is <see cref="SlimDX::Direct3D9::FillMode"/>.Solid.
			/// </summary>
			FillMode = D3DRS_FILLMODE,

			/// <summary>
			/// Defines the current shade mode of the device. Use values from <see cref="SlimDX::Direct3D9::ShadeMode"/> to set this state.
			/// The default value is <see cref="SlimDX::Direct3D9::ShadeMode"/>.Gouraud.
			/// </summary>
			ShadeMode = D3DRS_SHADEMODE,

			/// <summary>
			/// Set this state to <c>true</c> to enable writes to the depth buffer, and <c>false</c> to disable
			/// them. The default value is <c>true</c>.
			/// </summary>
			ZWriteEnable = D3DRS_ZWRITEENABLE,

			/// <summary>
			/// Set this state to <c>true</c> to enable alpha testing, and <c>false</c> to disable it. The
			/// default value is <c>false</c>.
			/// </summary>
			AlphaTestEnable = D3DRS_ALPHATESTENABLE,

			/// <summary>
			/// Set this state to <c>true</c> to enable drawing of the last pixel in a line, and <c>false</c>
			/// to disable it. The default value is <c>true</c>.
			/// </summary>
			LastPixel = D3DRS_LASTPIXEL,

			/// <summary>
			/// Defines the current source blending mode of the device. Use values from <see cref="Blend"/> to
			/// set this state. The default value is <see cref="SlimDX::Direct3D9::Blend"/>.One.
			/// </summary>
			SourceBlend = D3DRS_SRCBLEND,

			/// <summary>
			/// Defines the current destination blending mode of the device. Use values from <see cref="SlimDX::Direct3D9::Blend"/> to
			/// set this state. The default value is <see cref="SlimDX::Direct3D9::Blend"/>.Zero.
			/// </summary>
			DestinationBlend = D3DRS_DESTBLEND,

			/// <summary>
			/// Specifies how back-facing triangles are culled. Use values from <see cref="SlimDX::Direct3D9::Cull"/> to
			/// set this state. The default value is <see cref="SlimDX::Direct3D9::Cull"/>.Counterclockwise.
			/// </summary>
			CullMode = D3DRS_CULLMODE,

			/// <summary>
			/// Specifies the current depth testing function. Use values from <see cref="SlimDX::Direct3D9::Compare"/> to
			/// set this state. The default value is <see cref="SlimDX::Direct3D9::Compare"/>.LessEqual.
			/// </summary>
			ZFunc = D3DRS_ZFUNC,

			/// <summary>
			/// An integer value that specifies the reference alpha against which pixels are tested when
			/// alpha blending is enabled. The default value is 0.
			/// </summary>
			AlphaRef = D3DRS_ALPHAREF,

			/// <summary>
			/// Specifies the current alpha testing function. Use values from <see cref="SlimDX::Direct3D9::Compare"/> to
			/// set this state. The default value is <see cref="SlimDX::Direct3D9::Compare"/>.Always.
			/// </summary>
			AlphaFunc = D3DRS_ALPHAFUNC,

			/// <summary>
			/// Set this state to <c>true</c> to enable dithering, and <c>false</c> to disable it. The default
			/// value is <c>false</c>.
			/// </summary>
			DitherEnable = D3DRS_DITHERENABLE,

			/// <summary>
			/// Set this state to <c>true</c> to enable alpha blending, and <c>false</c> to disable it. The default
			/// value is <c>false</c>.
			/// </summary>
			AlphaBlendEnable = D3DRS_ALPHABLENDENABLE,

			/// <summary>
			/// Set this state to <c>true</c> to enable fog blending, and <c>false</c> to disable it. The default
			/// value is <c>false</c>.
			/// </summary>
			FogEnable = D3DRS_FOGENABLE,

			/// <summary>
			/// Set this state to <c>true</c> to enable specular highlights, and <c>false</c> to disable them. 
			/// The default value is <c>false</c>.
			/// </summary>
			SpecularEnable = D3DRS_SPECULARENABLE,

			/// <summary>
			/// Specifies current fog color. Use integer color values to set this state. The default value is
			/// 0 (black).
			/// </summary>
			FogColor = D3DRS_FOGCOLOR,

			/// <summary>
			/// Specifies the current fog formula to be used for pixel fog. Use values from <see cref="SlimDX::Direct3D9::FogMode"/>
			/// to set this state. The default value is <see cref="SlimDX::Direct3D9::FogMode"/>.None.
			/// </summary>
			FogTableMode = D3DRS_FOGTABLEMODE,

			/// <summary>
			/// A floating point value that defines the depth at which pixel or vertex fog effects begin.
			/// The default value is 0.0f.
			/// </summary>
			FogStart = D3DRS_FOGSTART,

			/// <summary>
			/// A floating point value that defines the depth at which pixel or vertex fog effects end.
			/// The default value is 1.0f.
			/// </summary>
			FogEnd = D3DRS_FOGEND,

			/// <summary>
			/// A floating point value that defines the density of fog used in exponential fog modes.
			/// The default value is 1.0f.
			/// </summary>
			FogDensity = D3DRS_FOGDENSITY,

			/// <summary>
			/// Set this state to <c>true</c> to enable range based fog, and <c>false</c> to use depth based fog. 
			/// The default value is <c>false</c>.
			/// </summary>
			RangeFogEnable = D3DRS_RANGEFOGENABLE,

			/// <summary>
			/// Set this state to <c>true</c> to enable stenciling, and <c>false</c> to disable it. 
			/// The default value is <c>false</c>.
			/// </summary>
			StencilEnable = D3DRS_STENCILENABLE,

			/// <summary>
			/// Specifies the stencil operation to perform if the stencil test fails. Use values from 
			/// <see cref="SlimDX::Direct3D9::StencilOperation"/> to set this state. The default value is <see cref="SlimDX::Direct3D9::StencilOperation"/>.Keep.
			/// </summary>
			StencilFail = D3DRS_STENCILFAIL,

			/// <summary>
			/// Specifies the stencil operation to perform if the stencil test passes and the depth test fails. 
			/// Use values from <see cref="SlimDX::Direct3D9::StencilOperation"/> to set this state. The default value is 
			/// <see cref="SlimDX::Direct3D9::StencilOperation"/>.Keep.
			/// </summary>
			StencilZFail = D3DRS_STENCILZFAIL,

			/// <summary>
			/// Specifies the stencil operation to perform if both the stencil and depth tests pass. Use values from 
			/// <see cref="SlimDX::Direct3D9::StencilOperation"/> to set this state. The default value is <see cref="SlimDX::Direct3D9::StencilOperation"/>.Keep.
			/// </summary>
			StencilPass = D3DRS_STENCILPASS,

			/// <summary>
			/// Specifies the comparison function for stencil tests. Use values from <see cref="SlimDX::Direct3D9::Compare"/> 
			/// to set this state. The default value is <see cref="SlimDX::Direct3D9::Compare"/>.Always.
			/// </summary>
			StencilFunc = D3DRS_STENCILFUNC,

			/// <summary>
			/// An integer value that specifies the reference value against which pixels are tested when
			/// stencil testing is enabled. The default value is 0.
			/// </summary>
			StencilRef = D3DRS_STENCILREF,

			/// <summary>
			/// An integer value that specifies the mask for stencil values. The default value is 0xFFFFFFFF.
			/// </summary>
			StencilMask = D3DRS_STENCILMASK,

			/// <summary>
			/// An integer value that specifies the write mask used for values written into the stencil buffer.
			/// The default value is 0xFFFFFFFF.
			/// </summary>
			StencilWriteMask = D3DRS_STENCILWRITEMASK,

			/// <summary>
			/// Specifies the color used for multiple texture blending. Use integer color values to set this 
			/// state. The default value is 0xFFFFFFFF (white).
			/// </summary>
			TextureFactor = D3DRS_TEXTUREFACTOR,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap0 = D3DRS_WRAP0,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap1 = D3DRS_WRAP1,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap2 = D3DRS_WRAP2,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap3 = D3DRS_WRAP3,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap4 = D3DRS_WRAP4,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap5 = D3DRS_WRAP5,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap6 = D3DRS_WRAP6,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap7 = D3DRS_WRAP7,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap8 = D3DRS_WRAP8,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap9 = D3DRS_WRAP9,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap10 = D3DRS_WRAP10,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap11 = D3DRS_WRAP11,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap12 = D3DRS_WRAP12,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap13 = D3DRS_WRAP13,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap14 = D3DRS_WRAP14,

			/// <summary>
			/// Texture wrapping behavior for multiple sets of texture coordinates. Use values from
			/// <see cref="SlimDX::Direct3D9::TextureWrapping"/> to set this state. The default value for this state is <see cref="SlimDX::Direct3D9::TextureWrapping"/>.None.
			/// </summary>
			Wrap15 = D3DRS_WRAP15,

			/// <summary>
			/// Set this state to <c>true</c> to enable primitive clipping, and <c>false</c>
			/// to disable it. The default value is <c>true</c>.
			/// </summary>
			Clipping = D3DRS_CLIPPING,

			/// <summary>
			/// Set this state to <c>true</c> to enable lighting, and <c>false</c>
			/// to disable it. The default value is <c>true</c>.
			/// </summary>
			Lighting = D3DRS_LIGHTING,

			/// <summary>
			/// Specifies the ambient light color. Use integer color values to set this state. The default value 
			/// is 0 (black).
			/// </summary>
			Ambient = D3DRS_AMBIENT,

			/// <summary>
			/// Specifies the current fog formula to be used for vertex fog. Use values from <see cref="FogMode"/>
			/// to set this state. The default value is <see cref="SlimDX::Direct3D9::FogMode"/>.None.
			/// </summary>
			FogVertexMode = D3DRS_FOGVERTEXMODE,

			/// <summary>
			/// Set this state to <c>true</c> to enable colored vertices, and <c>false</c>
			/// to disable them. The default value is <c>true</c>.
			/// </summary>
			ColorVertex = D3DRS_COLORVERTEX,

			/// <summary>
			/// Set this state to <c>true</c> to enable camera-relative specular highlights, and <c>false</c>
			/// to use orthogonal specular highlights. The default value is <c>true</c>.
			/// </summary>
			LocalViewer = D3DRS_LOCALVIEWER,

			/// <summary>
			/// Set this state to <c>true</c> to enable automatic normalization of vertex normals, and <c>false</c>
			/// to disable it. The default value is <c>false</c>.
			/// </summary>
			NormalizeNormals = D3DRS_NORMALIZENORMALS,

			/// <summary>
			/// Specifies the source for diffuse colors. Use values from <see cref="ColorSource"/> to set
			/// this state. The default value is <see cref="SlimDX::Direct3D9::ColorSource"/>.Color1.
			/// </summary>
			DiffuseMaterialSource = D3DRS_DIFFUSEMATERIALSOURCE,

			/// <summary>
			/// Specifies the source for specular colors. Use values from <see cref="ColorSource"/> to set
			/// this state. The default value is <see cref="SlimDX::Direct3D9::ColorSource"/>.Color2.
			/// </summary>
			SpecularMaterialSource = D3DRS_SPECULARMATERIALSOURCE,

			/// <summary>
			/// Specifies the source for ambient colors. Use values from <see cref="ColorSource"/> to set
			/// this state. The default value is <see cref="SlimDX::Direct3D9::ColorSource"/>.Material.
			/// </summary>
			AmbientMaterialSource = D3DRS_AMBIENTMATERIALSOURCE,

			/// <summary>
			/// Specifies the source for emissive colors. Use values from <see cref="ColorSource"/> to set
			/// this state. The default value is <see cref="SlimDX::Direct3D9::ColorSource"/>.Material.
			/// </summary>
			EmissiveMaterialSource = D3DRS_EMISSIVEMATERIALSOURCE,

			/// <summary>
			/// An integer value that specifies the number of matrices to use to perform geometry blending.
			/// Use values from <see cref="VertexBlend"/> to set this state. The default value is
			/// <see cref="SlimDX::Direct3D9::VertexBlend"/>.Disable.
			/// </summary>
			VertexBlend = D3DRS_VERTEXBLEND,

			/// <summary>
			/// An integer value that specifies which user-defined clip planes are active. Use
			/// </summary>
			ClipPlaneEnable = D3DRS_CLIPPLANEENABLE,

			/// <summary>
			/// A floating point value that specifies the size of points.
			/// </summary>
			PointSize = D3DRS_POINTSIZE,

			/// <summary>
			/// A floating point value that specifies the minimum size of point primitives.
			/// </summary>
			PointSizeMin = D3DRS_POINTSIZE_MIN,

			/// <summary>
			/// Set this state to <c>true</c> to enable point sprites, and <c>false</c>
			/// to disable them. The default value is <c>false</c>.
			/// </summary>
			PointSpriteEnable = D3DRS_POINTSPRITEENABLE,

			/// <summary>
			/// Set this state to <c>true</c> to enable point scaling, and <c>false</c>
			/// to disable it. The default value is <c>false</c>.
			/// </summary>
			PointScaleEnable = D3DRS_POINTSCALEENABLE,

			/// <summary>
			/// A floating point value that specifies attenuation when point scaling is applied. The default
			/// value is 1.0f.
			/// </summary>
			PointScaleA = D3DRS_POINTSCALE_A,

			/// <summary>
			/// A floating point value that specifies attenuation when point scaling is applied. The default
			/// value is 0.0f.
			/// </summary>
			PointScaleB = D3DRS_POINTSCALE_B,

			/// <summary>
			/// A floating point value that specifies attenuation when point scaling is applied. The default
			/// value is 0.0f.
			/// </summary>
			PointScaleC = D3DRS_POINTSCALE_C,

			/// <summary>
			/// Set this state to <c>true</c> to enable point multisampling, and <c>false</c>
			/// to disable it. The default value is <c>false</c>.
			/// </summary>
			MultisampleAntialias = D3DRS_MULTISAMPLEANTIALIAS,

			/// <summary>
			/// An integer mask that controls the samples in a multisampling render target.
			/// </summary>
			MultisampleMask = D3DRS_MULTISAMPLEMASK,

			/// <summary>
			/// Specifies the current patch edge style. Use values from <see cref="PatchEdgeStyle"/> to
			/// set this state. The default value is <see cref="PatchEdgeStyle"/>.Discrete".
			/// </summary>
			PatchEdgeStyle = D3DRS_PATCHEDGESTYLE,

			/// <summary>
			/// Specifies the current debug monitor token. Use values from <see cref="DebugMonitorToken"/> to
			/// set this state. The default value is <see cref="DebugMonitorToken"/>.Enable".
			/// </summary>
			DebugMonitorToken = D3DRS_DEBUGMONITORTOKEN,

			/// <summary>
			/// A floating point value that specifies the maximum size of point primitives.
			/// </summary>
			PointSizeMax = D3DRS_POINTSIZE_MAX,

			/// <summary>
			/// Set this state to <c>true</c> to enable indexed vertex blending, and <c>false</c>
			/// to disable it. The default value is <c>false</c>.
			/// </summary>
			IndexedVertexBlendEnable = D3DRS_INDEXEDVERTEXBLENDENABLE,

			/// <summary>
			/// Specifies the current color write state of the device. Use values from <see cref="ColorWriteEnable"/>
			/// to set this state. The default value is <see cref="ColorWriteEnable"/>.All".
			/// </summary>
			ColorWriteEnable = D3DRS_COLORWRITEENABLE,

			/// <summary>
			/// A floating point value that specifies the tweening factor. The default value is 0.0f.
			/// </summary>
			TweenFactor = D3DRS_TWEENFACTOR,

			/// <summary>
			/// Specifies the blending operation to use when alpha blending is enabled. Use values from
			/// <see cref="BlendOperation"/> to set this state. The default value is <see cref="BlendOperation"/>.Add".
			/// </summary>
			BlendOperation = D3DRS_BLENDOP,

			/// <summary>
			/// Specifies the N-patch position interpolation degree. Use values from <see cref="Degree"/> to
			/// set this state. The default value is <see cref="Degree"/>.Cubic".
			/// </summary>
			PositionDegree = D3DRS_POSITIONDEGREE,

			/// <summary>
			/// Specifies the N-patch normal interpolation degree. Use values from <see cref="Degree"/> to
			/// set this state. The default value is <see cref="Degree"/>.Linear".
			/// </summary>
			NormalDegree = D3DRS_NORMALDEGREE,

			/// <summary>
			/// Set this state to <c>true</c> to enable scissor testing, and <c>false</c>
			/// to disable it. The default value is <c>false</c>.
			/// </summary>
			ScissorTestEnable = D3DRS_SCISSORTESTENABLE,

			/// <summary>
			/// An integer value that specifies how much bias can be applied to co-planar primitives to reduce
			/// Z-fighting. The default value is 0.
			/// </summary>
			SlopeScaleDepthBias = D3DRS_SLOPESCALEDEPTHBIAS,

			/// <summary>
			/// Set this state to <c>true</c> to enable antialiased lines, and <c>false</c>
			/// to disable them. The default value is <c>false</c>.
			/// </summary>
			AntialiasedLineEnable = D3DRS_ANTIALIASEDLINEENABLE,

			/// <summary>
			/// A floating point value that specifies the minium tessellation level. The default value is 1.0f.
			/// </summary>
			MinTessellationLevel = D3DRS_MINTESSELLATIONLEVEL,

			/// <summary>
			/// A floating point value that specifies the maximum tessellation level. The default value is 1.0f.
			/// </summary>
			MaxTessellationLevel = D3DRS_MAXTESSELLATIONLEVEL,

			/// <summary>
			/// A floating point value that specifies the amount to adaptively tessellate, in the X direction.
			/// The default value is 0.0f.
			/// </summary>
			AdaptiveTessX = D3DRS_ADAPTIVETESS_X,

			/// <summary>
			/// A floating point value that specifies the amount to adaptively tessellate, in the Y direction.
			/// The default value is 0.0f.
			/// </summary>
			AdaptiveTessY = D3DRS_ADAPTIVETESS_Y,

			/// <summary>
			/// A floating point value that specifies the amount to adaptively tessellate, in the Z direction.
			/// The default value is 0.0f.
			/// </summary>
			AdaptiveTessZ = D3DRS_ADAPTIVETESS_Z,

			/// <summary>
			/// A floating point value that specifies the amount to adaptively tessellate, in the W direction.
			/// The default value is 1.0f.
			/// </summary>
			AdaptiveTessW = D3DRS_ADAPTIVETESS_W,

			/// <summary>
			/// Set this state to <c>true</c> to enable adaptive tessellation, and <c>false</c>
			/// to disable it. The default value is <c>false</c>.
			/// </summary>
			EnableAdaptiveTessellation = D3DRS_ENABLEADAPTIVETESSELLATION,

			/// <summary>
			/// Set this state to <c>true</c> to enable two sided stenciling, and <c>false</c>
			/// to disable it. The default value is <c>false</c>.
			/// </summary>
			TwoSidedStencilMode = D3DRS_TWOSIDEDSTENCILMODE,

			/// <summary>
			/// Specifies the operation to perform if the stencil test fails. Use values from
			/// <see cref="StencilOperation"/> to set the state. The default value is <see cref="StencilOperation"/>.Keep".
			/// </summary>
			CcwStencilFail = D3DRS_CCW_STENCILFAIL,

			/// <summary>
			/// Specifies the operation to perform if the stencil test passes and depth test fails. Use values from
			/// <see cref="StencilOperation"/> to set the state. The default value is <see cref="StencilOperation"/>.Keep".
			/// </summary>
			CcwStencilZFail = D3DRS_CCW_STENCILZFAIL,

			/// <summary>
			/// Specifies the operation to perform if both the stencil and depth tests pass. Use values from
			/// <see cref="StencilOperation"/> to set the state. The default value is <see cref="StencilOperation"/>.Keep".
			/// </summary>
			CcwStencilPass = D3DRS_CCW_STENCILPASS,

			/// <summary>
			/// Specifies the comparison function for stencil tests.. Use values from
			/// <see cref="Compare"/> to set the state. The default value is <see cref="Compare"/>.Always".
			/// </summary>
			CcwStencilFunc = D3DRS_CCW_STENCILFUNC,

			/// <summary>
			/// Specifies additional color write enable settings for the device. Use values from <see cref="ColorWriteEnable"/>
			/// to set this state. The default value is <see cref="ColorWriteEnable"/>.All".
			/// </summary>
			ColorWriteEnable1 = D3DRS_COLORWRITEENABLE1,

			/// <summary>
			/// Specifies additional color write enable settings for the device. Use values from <see cref="ColorWriteEnable"/>
			/// to set this state. The default value is <see cref="ColorWriteEnable"/>.All".
			/// </summary>
			ColorWriteEnable2 = D3DRS_COLORWRITEENABLE2,

			/// <summary>
			/// Specifies additional color write enable settings for the device. Use values from <see cref="ColorWriteEnable"/>
			/// to set this state. The default value is <see cref="ColorWriteEnable"/>.All".
			/// </summary>
			ColorWriteEnable3 = D3DRS_COLORWRITEENABLE3,

			/// <summary>
			/// Specifies a color to use as a constant blend factor for alpha blending. Use integer color values 
			/// to set this state. The default value is 0xFFFFFFFF (white).
			/// </summary>
			BlendFactor = D3DRS_BLENDFACTOR,

			/// <summary>
			/// Set this state to <c>true</c> to enable sRGB gamma correction, and <c>false</c>
			/// to disable it. The default value is <c>false</c>.
			/// </summary>
			SrgbWriteEnable = D3DRS_SRGBWRITEENABLE,

			/// <summary>
			/// A floating point value that is used for comparison of depth values.
			/// </summary>
			DepthBias = D3DRS_DEPTHBIAS,

			/// <summary>
			/// Set this state to <c>true</c> to enable separate blending modes for the alpha channel, and <c>false</c>
			/// to disable them. The default value is <c>false</c>.
			/// </summary>
			SeparateAlphaBlendEnable = D3DRS_SEPARATEALPHABLENDENABLE,

			/// <summary>
			/// Specifies the source alpha blending function. Use values from <see cref="Blend"/> to set this state.
			/// The default value is <see cref="SlimDX::Direct3D9::Blend"/>.One.
			/// </summary>
			SourceBlendAlpha = D3DRS_SRCBLENDALPHA,

			/// <summary>
			/// Specifies the destination alpha blending function. Use values from <see cref="Blend"/> to set this state.
			/// The default value is <see cref="SlimDX::Direct3D9::Blend"/>.Zero.
			/// </summary>
			DestinationBlendAlpha = D3DRS_DESTBLENDALPHA,

			/// <summary>
			/// Specifies the operation to perform for separate alpha blending. Use values from <see cref="BlendOperation"/>
			/// to set this state. The default value is <see cref="BlendOperation"/>.Add.
			/// </summary>
			BlendOperationAlpha = D3DRS_BLENDOPALPHA
		};
		
		/// <summary>
		/// Defines resource types.
		/// </summary>
		/// <unmanaged>D3DRESOURCETYPE</unmanaged>
		public enum class ResourceType : System::Int32
		{
			/// <summary>
			/// Surface resource.
			/// </summary>
			Surface = D3DRTYPE_SURFACE,

			/// <summary>
			/// Volume resource.
			/// </summary>
			Volume = D3DRTYPE_VOLUME,

			/// <summary>
			/// Texture resource.
			/// </summary>
			Texture = D3DRTYPE_TEXTURE,

			/// <summary>
			/// Volume texture resource.
			/// </summary>
			VolumeTexture = D3DRTYPE_VOLUMETEXTURE,

			/// <summary>
			/// Cube texture resource.
			/// </summary>
			CubeTexture = D3DRTYPE_CUBETEXTURE,

			/// <summary>
			/// Vertex buffer resource.
			/// </summary>
			VertexBuffer = D3DRTYPE_VERTEXBUFFER,

			/// <summary>
			/// Index buffer resource.
			/// </summary>
			IndexBuffer = D3DRTYPE_INDEXBUFFER,
		};
		
		/// <summary>
		/// Sampler states define texture sampling operations such as texture addressing and texture filtering. 
		/// Some sampler states set-up vertex processing, and some set-up pixel processing. Sampler states can 
		/// be saved and restored using stateblocks 
		/// </summary>
		/// <unmanaged>D3DSAMPLERSTATETYPE</unmanaged>
		public enum class SamplerState : System::Int32
		{
			/// <summary>
			/// Specifies the texture address mode for the U coordinate. Use values from <see cref="TextureAddress"/>
			/// to set this state. The default value is <see cref="TextureAddress"/>.Wrap.
			/// </summary>
			AddressU = D3DSAMP_ADDRESSU,

			/// <summary>
			/// Specifies the texture address mode for the V coordinate. Use values from <see cref="TextureAddress"/>
			/// to set this state. The default value is <see cref="TextureAddress"/>.Wrap.
			/// </summary>
			AddressV = D3DSAMP_ADDRESSV,

			/// <summary>
			/// Specifies the texture address mode for the W coordinate. Use values from <see cref="TextureAddress"/>
			/// to set this state. The default value is <see cref="TextureAddress"/>.Wrap.
			/// </summary>
			AddressW = D3DSAMP_ADDRESSW,

			/// <summary>
			/// Specifies the texture border color. Use packed integer color values to set this state.
			/// The default value is 0 (black).
			/// </summary>
			BorderColor = D3DSAMP_BORDERCOLOR,

			/// <summary>
			/// Specifies the magnification filter. Use values from <see cref="TextureFilter"/> to set this
			/// state. The default value is <see cref="TextureFilter"/>.Point.
			/// </summary>
			MagFilter = D3DSAMP_MAGFILTER,

			/// <summary>
			/// Specifies the minification filter. Use values from <see cref="TextureFilter"/> to set this
			/// state. The default value is <see cref="TextureFilter"/>.Point.
			/// </summary>
			MinFilter = D3DSAMP_MINFILTER,

			/// <summary>
			/// Specifies the mipmap filter. Use values from <see cref="TextureFilter"/> to set this
			/// state. The default value is <see cref="TextureFilter"/>.Point.
			/// </summary>
			MipFilter = D3DSAMP_MIPFILTER,

			/// <summary>
			/// An integer value that specifies the level-of-detail bias. The default value is 0.
			/// </summary>
			MipMapLodBias = D3DSAMP_MIPMAPLODBIAS,

			/// <summary>
			/// An integer value that specifies the maximum mipmap level. The default value is 0.
			/// </summary>
			MaxMipLevel = D3DSAMP_MAXMIPLEVEL,

			/// <summary>
			/// An integer value that specifies the maximum anisotropy level. The default value is 1.
			/// </summary>
			MaxAnisotropy = D3DSAMP_MAXANISOTROPY,

			/// <summary>
			/// An integer value that specifies sRGB gamma correction. The default value is 0.
			/// </summary>
			SrgbTexture = D3DSAMP_SRGBTEXTURE,

			/// <summary>
			/// An integer value that specifies which element of a multielement texture to use. The default value is 0.
			/// </summary>
			ElementIndex = D3DSAMP_ELEMENTINDEX,

			/// <summary>
			/// An integer value that specifies the vertex offset into a presampled displacement map. The default value is 0.
			/// </summary>
			DisplacementMapOffset = D3DSAMP_DMAPOFFSET,
		};

		/// <summary>
		/// Specifies the shading operations that a device supports.
		/// </summary>
		/// <unmanaged>D3DPSHADECAPS</unmanaged>
		[System::Flags]
		public enum class ShadeCaps : System::Int32
		{
			/// <summary>
			/// Device supports colored Gouraud shading.
			/// </summary>
			ColorGouraudRgb = D3DPSHADECAPS_COLORGOURAUDRGB,

			/// <summary>
			/// Device supports Gouraud shading of specular highlights.
			/// </summary>
			SpecularGouraudRgb = D3DPSHADECAPS_SPECULARGOURAUDRGB,

			/// <summary>
			/// Device supports Gouraud-blended transparency.
			/// </summary>
			AlphaGouraudBlend = D3DPSHADECAPS_ALPHAGOURAUDBLEND,

			/// <summary>
			/// Device supports Gouraud fog.
			/// </summary>
			FogGouraud = D3DPSHADECAPS_FOGGOURAUD
		};

		/// <summary>
		/// Flags indicating the method the rasterizer uses to create an image on a surface.
		/// </summary>
		/// <unmanaged>D3DSCANLINEORDERING</unmanaged>
		public enum class ScanlineOrdering : System::Int32
		{
			/// <summary>
			/// All scanlines are used to build the final image.
			/// </summary>
			Progressive = D3DSCANLINEORDERING_PROGRESSIVE,

			/// <summary>
			/// Every other line is interlaced with the previous one to build the final image.
			/// </summary>
			Interlaced = D3DSCANLINEORDERING_INTERLACED
		};
		
		/// <summary>
		/// Predefined sets of pipeline states used by state blocks.
		/// </summary>
		/// <unmanaged>D3DSTATEBLOCKTYPE</unmanaged>
		public enum class StateBlockType : System::Int32
		{
			/// <summary>
			/// Capture the current device state.
			/// </summary>
			All = D3DSBT_ALL,

			/// <summary>
			/// Capture the current pixel state.
			/// </summary>
			PixelState = D3DSBT_PIXELSTATE,

			/// <summary>
			/// Capture the current vertex state.
			/// </summary>
			VertexState = D3DSBT_VERTEXSTATE,
		};

		/// <summary>
		/// Defines a set of stencil operations supported by a device.
		/// </summary>
		/// <unmanaged>D3DSTENCILCAPS</unmanaged>
		[System::Flags]
		public enum class StencilCaps : System::Int32
		{
			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.Keep.
			/// </summary>
			Keep = D3DSTENCILCAPS_KEEP,

			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.Zero.
			/// </summary>
			Zero = D3DSTENCILCAPS_ZERO,

			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.Replace.
			/// </summary>
			Replace = D3DSTENCILCAPS_REPLACE,

			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.IncrementClamp.
			/// </summary>
			IncrementClamp = D3DSTENCILCAPS_INCRSAT,

			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.DecrementClamp.
			/// </summary>
			DecrementClamp = D3DSTENCILCAPS_DECRSAT,

			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.Invert.
			/// </summary>
			Invert = D3DSTENCILCAPS_INVERT,

			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.Increment.
			/// </summary>
			Increment = D3DSTENCILCAPS_INCR,

			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.Decrement.
			/// </summary>
			Decrement = D3DSTENCILCAPS_DECR,

			/// <summary>
			/// The device supports <see cref="StencilOperation"/>.TwoSided.
			/// </summary>
			TwoSided = D3DSTENCILCAPS_TWOSIDED,
		};
		
		/// <summary>
		/// Specifies possible stream frequencies.
		/// </summary>
		/// <unmanaged>D3DSTREAMSOURCE</unmanaged>
		public enum class StreamFrequency : System::Int32
		{
			/// <summary>
			/// The stream uses indexed data.
			/// </summary>
			IndexedData = D3DSTREAMSOURCE_INDEXEDDATA,

			/// <summary>
			/// The stream uses instance data.
			/// </summary>
			InstanceData = D3DSTREAMSOURCE_INSTANCEDATA
		};

		/// <summary>
		/// Specifies the set of texture addressing modes supported by the device.
		/// </summary>
		/// <unmanaged>D3DPTADDRESSCAPS</unmanaged>
		[System::Flags]
		public enum class TextureAddressCaps : System::Int32
		{
			/// <summary>
			/// The device supports <see cref="TextureAddress"/>.Wrap.
			/// </summary>
			Wrap = D3DPTADDRESSCAPS_WRAP,

			/// <summary>
			/// The device supports <see cref="TextureAddress"/>.Mirror.
			/// </summary>
			Mirror = D3DPTADDRESSCAPS_MIRROR,

			/// <summary>
			/// The device supports <see cref="TextureAddress"/>.Clamp.
			/// </summary>
			Clamp = D3DPTADDRESSCAPS_CLAMP,

			/// <summary>
			/// The device supports <see cref="TextureAddress"/>.Border.
			/// </summary>
			Border = D3DPTADDRESSCAPS_BORDER,

			/// <summary>
			/// The device can separate the texture-addressing modes of the u and v coordinates of the texture.
			/// </summary>
			IndependentUV = D3DPTADDRESSCAPS_INDEPENDENTUV,

			/// <summary>
			/// The device supports <see cref="TextureAddress"/>.MirrorOnce.
			/// </summary>
			MirrorOnce = D3DPTADDRESSCAPS_MIRRORONCE,
		};
		
		/// <summary>
		/// Specifies possible texture arguments.
		/// </summary>
		/// <unmanaged>D3DTA</unmanaged>
		[System::Flags]
		public enum class TextureArgument : System::Int32
		{
			/// <summary>
			/// Mask value for all arguments.
			/// </summary>
			SelectMask = D3DTA_SELECTMASK,

			/// <summary>
			/// The texture argument is the diffuse color.
			/// </summary>
			Diffuse = D3DTA_DIFFUSE,

			/// <summary>
			/// The texture argument is the result of the previous blending stage.
			/// </summary>
			Current = D3DTA_CURRENT,

			/// <summary>
			/// The texture argument is the texture color.
			/// </summary>
			Texture = D3DTA_TEXTURE,

			/// <summary>
			/// The texture argument is the texture factor.
			/// </summary>
			TFactor = D3DTA_TFACTOR,

			/// <summary>
			/// The texture argument is the specular color.
			/// </summary>
			Specular = D3DTA_SPECULAR,

			/// <summary>
			/// The texture argument is a temporary color register.
			/// </summary>
			Temp = D3DTA_TEMP,

			/// <summary>
			/// Selects a constant from a texture stage.
			/// </summary>
			Constant = D3DTA_CONSTANT,

			/// <summary>
			/// Specifies that the function should take the complement of the argument.
			/// </summary>
			Complement = D3DTA_COMPLEMENT,

			/// <summary>
			/// Replicate the alpha information to all channels before the method completes.
			/// </summary>
			AlphaReplicate = D3DTA_ALPHAREPLICATE,
		};

		/// <summary>
		/// Specifies miscellaneous texture mapping capabilities supported by the device.
		/// </summary>
		/// <unmanaged>D3DPTEXTURECAPS</unmanaged>
		[System::Flags]
		public enum class TextureCaps : System::Int32
		{
			/// <summary>
			/// Perspective correction is supported.
			/// </summary>
			Perspective = D3DPTEXTURECAPS_PERSPECTIVE,

			/// <summary>
			/// Specifies that the device requires power of 2 texture sizes.
			/// </summary>
			Pow2 = D3DPTEXTURECAPS_POW2,

			/// <summary>
			/// Alpha in texture pixels is supported.
			/// </summary>
			Alpha = D3DPTEXTURECAPS_ALPHA,

			/// <summary>
			/// Specifies that all textures must be square.
			/// </summary>
			SquareOnly = D3DPTEXTURECAPS_SQUAREONLY,

			/// <summary>
			/// Texture indices are not scaled by their size prior to rendering.
			/// </summary>
			TextureRepeatNotScaledBySize = D3DPTEXTURECAPS_TEXREPEATNOTSCALEDBYSIZE,

			/// <summary>
			/// Device can draw alpha from texture palettes.
			/// </summary>
			AlphaPalette = D3DPTEXTURECAPS_ALPHAPALETTE,

			/// <summary>
			/// Determines whether power of 2 texture sizes are required by the device.
			/// </summary>
			NonPow2Conditional = D3DPTEXTURECAPS_NONPOW2CONDITIONAL,

			/// <summary>
			/// The device supports the <see cref="TextureTransform"/>.Projected texture transformation flag.
			/// </summary>
			Projected = D3DPTEXTURECAPS_PROJECTED,

			/// <summary>
			/// Supports cube map textures.
			/// </summary>
			CubeMap = D3DPTEXTURECAPS_CUBEMAP,

			/// <summary>
			/// Supports volume textures.
			/// </summary>
			VolumeMap = D3DPTEXTURECAPS_VOLUMEMAP,

			/// <summary>
			/// Device supports mipmaps.
			/// </summary>
			MipMap = D3DPTEXTURECAPS_MIPMAP,

			/// <summary>
			/// Device supports mipmaps for volume textures.
			/// </summary>
			MipVolumeMap = D3DPTEXTURECAPS_MIPVOLUMEMAP,

			/// <summary>
			/// Device supports mipmaps for cube textures.
			/// </summary>
			MipCubeMap = D3DPTEXTURECAPS_MIPCUBEMAP,

			/// <summary>
			/// Specifies whether cube maps must have powers of 2 sizes.
			/// </summary>
			CubeMapPow2 = D3DPTEXTURECAPS_CUBEMAP_POW2,

			/// <summary>
			/// Specifies whether volume textures must have powers of 2 sizes.
			/// </summary>
			VolumeMapPow2 = D3DPTEXTURECAPS_VOLUMEMAP_POW2,

			/// <summary>
			/// Specifies that the device does not support a projected bump environment lookup operation.
			/// </summary>
			NoProjectedBumpEnvironment = D3DPTEXTURECAPS_NOPROJECTEDBUMPENV,
		};
		
		/// <summary>
		/// Driver texture coordinate capability flags.
		/// </summary>
		/// <unmanaged>D3DTSS_TCI</unmanaged>
		[System::Flags]
		public enum class TextureCoordIndex : System::Int32
		{
			/// <summary>
			/// Use the specified texture coordinates contained in the vertex format.
			/// </summary>
			PassThru = D3DTSS_TCI_PASSTHRU,

			/// <summary>
			/// Use the vertex normal, transformed to camera space, as the input texture coordinates.
			/// </summary>
			CameraSpaceNormal = D3DTSS_TCI_CAMERASPACENORMAL,

			/// <summary>
			/// Use the vertex position, transformed to camera space, as the input texture coordinates.
			/// </summary>
			CameraSpacePosition = D3DTSS_TCI_CAMERASPACEPOSITION,

			/// <summary>
			/// Use the reflection vector, transformed to camera space, as the input texture coordinates.
			/// </summary>
			CameraSpaceReflectionVector = D3DTSS_TCI_CAMERASPACEREFLECTIONVECTOR,

			/// <summary>
			/// Use the specified coordinates for sphere mapping.
			/// </summary>
			SphereMap = D3DTSS_TCI_SPHEREMAP,
		};
		

		/// <summary>
		/// Defines per-stage texture blending operations.
		/// </summary>
		/// <unmanaged>D3DTEXTUREOP</unmanaged>
		public enum class TextureOperation : System::Int32
		{
			/// <summary>
			/// Disabled output from the current stage and all higher stages.
			/// </summary>
			Disable = D3DTOP_DISABLE,

			/// <summary>
			/// Use the current stage's first color or alpha argument, unmodified, as the output.
			/// </summary>
			SelectArg1 = D3DTOP_SELECTARG1,

			/// <summary>
			/// Use the current stage's second color or alpha argument, unmodified, as the output.
			/// </summary>
			SelectArg2 = D3DTOP_SELECTARG2,

			/// <summary>
			/// Multiply the components of the arguments.
			/// </summary>
			Modulate = D3DTOP_MODULATE,

			/// <summary>
			/// Multiply the components of the arguments, and then multiply them by 2.
			/// </summary>
			Modulate2X = D3DTOP_MODULATE2X,

			/// <summary>
			/// Multiply the components of the arguments, and then multiply them by 2.
			/// </summary>
			Modulate4X = D3DTOP_MODULATE4X,

			/// <summary>
			/// Add the components of the arguments.
			/// </summary>
			Add = D3DTOP_ADD,

			/// <summary>
			/// Add the components of the arguments with a -0.5 bias.
			/// </summary>
			AddSigned = D3DTOP_ADDSIGNED,

			/// <summary>
			/// Add the components of the arguments with a -0.5 bias, and then multiply them by 2.
			/// </summary>
			AddSigned2X = D3DTOP_ADDSIGNED2X,

			/// <summary>
			/// Subtract the components of the second argument from the components of the first argument.
			/// </summary>
			Subtract = D3DTOP_SUBTRACT,

			/// <summary>
			/// Add the first and second arguments, and then subtract their product from the sum.
			/// </summary>
			AddSmooth = D3DTOP_ADDSMOOTH,

			/// <summary>
			/// Linearly blend the current stage, using the interpolated alpha from each vertex.
			/// </summary>
			BlendDiffuseAlpha = D3DTOP_BLENDDIFFUSEALPHA,

			/// <summary>
			/// Linearly blend the current stage, using the alpha from the stage's texture.
			/// </summary>
			BlendTextureAlpha = D3DTOP_BLENDTEXTUREALPHA,

			/// <summary>
			/// Linearly blend the current stage, using the current texture factor.
			/// </summary>
			BlendFactorAlpha = D3DTOP_BLENDFACTORALPHA,

			/// <summary>
			/// Linearly blend the current stage, using a premultiplied alpha.
			/// </summary>
			BlendTextureAlphaPM = D3DTOP_BLENDTEXTUREALPHAPM,

			/// <summary>
			/// Linearly blend the current stage, using the alpha from the previous stage.
			/// </summary>
			BlendCurrentAlpha = D3DTOP_BLENDCURRENTALPHA,

			/// <summary>
			/// Premodulate the arguments with the next stage's values.
			/// </summary>
			Premodulate = D3DTOP_PREMODULATE,

			/// <summary>
			/// Modulate the color of the second argument, using the alpha from the first argument, and then
			/// add the result to argument one.
			/// </summary>
			ModulateAlphaAddColor = D3DTOP_MODULATEALPHA_ADDCOLOR,

			/// <summary>
			/// Modulate the arguments, then add the alpha of the first argument.
			/// </summary>
			ModulateColorAddAlpha = D3DTOP_MODULATECOLOR_ADDALPHA,

			/// <summary>
			/// Modulate the color of the second argument, using the inverted alpha from the first argument, and then
			/// add the result to argument one.
			/// </summary>
			ModulateInvAlphaAddColor = D3DTOP_MODULATEINVALPHA_ADDCOLOR,

			/// <summary>
			/// Modulate the arguments, inverting the first argument, then add the alpha of the first argument.
			/// </summary>
			ModulateInvColorAddAlpha = D3DTOP_MODULATEINVCOLOR_ADDALPHA,

			/// <summary>
			/// Perform per-pixel bump mapping, using the environment map in the next stage.
			/// </summary>
			BumpEnvironmentMap = D3DTOP_BUMPENVMAP,

			/// <summary>
			/// Perform per-pixel bump mapping, using the environment map in the next stage, with luminance.
			/// </summary>
			BumpEnvironmentMapLuminance = D3DTOP_BUMPENVMAPLUMINANCE,

			/// <summary>
			/// Modulate the components of each argument as signed components, add their products; then 
			/// replicate the sum to all color channels, including alpha. 
			/// </summary>
			DotProduct3 = D3DTOP_DOTPRODUCT3,

			/// <summary>
			/// Performs a multiply-accumulate operation. It takes the last two arguments, multiplies them 
			/// together, and adds them to the remaining input/source argument, and places that into the result. 
			/// </summary>
			MultiplyAdd = D3DTOP_MULTIPLYADD,

			/// <summary>
			/// Linearly interpolates between the second and third source arguments by a proportion specified 
			/// in the first source argument. 
			/// </summary>
			Lerp = D3DTOP_LERP,
		};

		/// <summary>
		/// Specifies the texture operations supported by the device.
		/// </summary>
		/// <unmanaged>D3DTEXOPCAPS</unmanaged>
		[System::Flags]
		public enum class TextureOperationCaps : System::Int32
		{
			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.Disable.
			/// </summary>
			Disable = D3DTEXOPCAPS_DISABLE,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.SelectArg1.
			/// </summary>
			SelectArg1 = D3DTEXOPCAPS_SELECTARG1,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.SelectArg2.
			/// </summary>
			SelectArg2 = D3DTEXOPCAPS_SELECTARG2,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.Modulate.
			/// </summary>
			Modulate = D3DTEXOPCAPS_MODULATE,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.Modulate2X.
			/// </summary>
			Modulate2X = D3DTEXOPCAPS_MODULATE2X,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.Modulate4X.
			/// </summary>
			Modulate4X = D3DTEXOPCAPS_MODULATE4X,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.Add.
			/// </summary>
			Add = D3DTEXOPCAPS_ADD,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.AddSigned.
			/// </summary>
			AddSigned = D3DTEXOPCAPS_ADDSIGNED,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.AddSigned2X.
			/// </summary>
			AddSigned2X = D3DTEXOPCAPS_ADDSIGNED2X,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.Subtract.
			/// </summary>
			Subtract = D3DTEXOPCAPS_SUBTRACT,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.AddSmooth.
			/// </summary>
			AddSmooth = D3DTEXOPCAPS_ADDSMOOTH,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.BlendDiffuseAlpha.
			/// </summary>
			BlendDiffuseAlpha = D3DTEXOPCAPS_BLENDDIFFUSEALPHA,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.BlendTextureAlpha.
			/// </summary>
			BlendTextureAlpha = D3DTEXOPCAPS_BLENDTEXTUREALPHA,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.BlendFactorAlpha.
			/// </summary>
			BlendFactorAlpha = D3DTEXOPCAPS_BLENDFACTORALPHA,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.BlendTextureAlphaPM.
			/// </summary>
			BlendTextureAlphaPM = D3DTEXOPCAPS_BLENDTEXTUREALPHAPM,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.BlendCurrentAlpha.
			/// </summary>
			BlendCurrentAlpha = D3DTEXOPCAPS_BLENDCURRENTALPHA,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.Premodulate.
			/// </summary>
			Premodulate = D3DTEXOPCAPS_PREMODULATE,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.ModulateAlphaAddColor.
			/// </summary>
			ModulateAlphaAddColor = D3DTEXOPCAPS_MODULATEALPHA_ADDCOLOR,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.ModulateColorAddAlpha.
			/// </summary>
			ModulateColorAddAlpha = D3DTEXOPCAPS_MODULATECOLOR_ADDALPHA,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.ModulateInvAlphaAddColor.
			/// </summary>
			ModulateInvAlphaAddColor = D3DTEXOPCAPS_MODULATEINVALPHA_ADDCOLOR,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.ModulateInvColorAddAlpha.
			/// </summary>
			ModulateInvColorAddAlpha = D3DTEXOPCAPS_MODULATEINVCOLOR_ADDALPHA,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.BumpEnvironmentMap.
			/// </summary>
			BumpEnvironmentMap = D3DTEXOPCAPS_BUMPENVMAP,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.BumpEnvironmentMapLuminance.
			/// </summary>
			BumpEnvironmentMapLuminance = D3DTEXOPCAPS_BUMPENVMAPLUMINANCE,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.DotProduct3.
			/// </summary>
			DotProduct3 = D3DTEXOPCAPS_DOTPRODUCT3,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.MultiplyAdd.
			/// </summary>
			MultiplyAdd = D3DTEXOPCAPS_MULTIPLYADD,

			/// <summary>
			/// The device supports <see cref="TextureOperation"/>.Lerp.
			/// </summary>
			Lerp = D3DTEXOPCAPS_LERP
		};
		
		/// <summary>
		/// Texture stage states define multi-blender texture operations. Some sampler states set up 
		/// vertex processing, and some set up pixel processing. Texture stage states can be saved and 
		/// restored using stateblocks.
		/// </summary>
		/// <unmanaged>D3DTEXTURESTAGESTATETYPE</unmanaged>
		public enum class TextureStage : System::Int32
		{
			/// <summary>
			/// Texture-stage state is a texture color blending operation. Use values from <see cref="TextureOperation"/>
			/// to set this state. The default value for the first stage is <see cref="TextureOperation"/>.Modulate;
			/// for all other stages the default is <see cref="TextureOperation"/>.Disable.
			/// </summary>
			ColorOperation = D3DTSS_COLOROP,

			/// <summary>
			/// Texture-stage state is the first color argument for the stage. Use values from <see cref="TextureArgument"/>
			/// to set this state. The default value is <see cref="TextureArgument"/>.Texture.
			/// </summary>
			ColorArg1 = D3DTSS_COLORARG1,

			/// <summary>
			/// Texture-stage state is the second color argument for the stage. Use values from <see cref="TextureArgument"/>
			/// to set this state. The default value is <see cref="TextureArgument"/>.Current.
			/// </summary>
			ColorArg2 = D3DTSS_COLORARG2,

			/// <summary>
			/// Texture-stage state is a texture alpha blending operation. Use values from <see cref="TextureOperation"/>
			/// to set this state. The default value for the first stage is <see cref="TextureOperation"/>.SelectArg1;
			/// for all other stages the default is <see cref="TextureOperation"/>.Disable"/>.
			/// </summary>
			AlphaOperation = D3DTSS_ALPHAOP,

			/// <summary>
			/// Texture-stage state is the first alpha argument for the stage. Use values from <see cref="TextureArgument"/>
			/// to set this state. The default value is <see cref="TextureArgument"/>.Texture.
			/// </summary>
			AlphaArg1 = D3DTSS_ALPHAARG1,

			/// <summary>
			/// Texture-stage state is the second alpha argument for the stage. Use values from <see cref="TextureArgument"/>
			/// to set this state. The default value is <see cref="TextureArgument"/>.Current.
			/// </summary>
			AlphaArg2 = D3DTSS_ALPHAARG2,

			/// <summary>
			/// Texture-stage state is a floating-point value for the [0][0] coefficient in a bump-mapping 
			/// matrix. The default value is 0.0f. 
			/// </summary>
			BumpEnvironmentMat00 = D3DTSS_BUMPENVMAT00,

			/// <summary>
			/// Texture-stage state is a floating-point value for the [0][1] coefficient in a bump-mapping 
			/// matrix. The default value is 0.0f. 
			/// </summary>
			BumpEnvironmentMat01 = D3DTSS_BUMPENVMAT01,

			/// <summary>
			/// Texture-stage state is a floating-point value for the [1][0] coefficient in a bump-mapping 
			/// matrix. The default value is 0.0f. 
			/// </summary>
			BumpEnvironmentMat10 = D3DTSS_BUMPENVMAT10,

			/// <summary>
			/// Texture-stage state is a floating-point value for the [1][1] coefficient in a bump-mapping 
			/// matrix. The default value is 0.0f. 
			/// </summary>
			BumpEnvironmentMat11 = D3DTSS_BUMPENVMAT11,

			/// <summary>
			/// Index of the texture coordinate set to use with this texture stage. You can specify up to 
			/// eight sets of texture coordinates per vertex. If a vertex does not include a set of texture 
			/// coordinates at the specified index, the system defaults to the u and v coordinates (0,0). 
			/// </summary>
			TexCoordIndex = D3DTSS_TEXCOORDINDEX,

			/// <summary>
			/// Floating-point scale value for bump-map luminance. The default value is 0.0f.
			/// </summary>
			BumpEnvironmentLScale = D3DTSS_BUMPENVLSCALE,

			/// <summary>
			/// Floating-point offset value for bump-map luminance. The default value is 0.0f.
			/// </summary>
			BumpEnvironmentLOffset = D3DTSS_BUMPENVLOFFSET,

			/// <summary>
			/// Specifies transformation options for texture coordinates. Use values from <see cref="TextureTransform"/>
			/// to set this state. The default value is <see cref="TextureTransform"/>.Disable.
			/// </summary>
			TextureTransformFlags = D3DTSS_TEXTURETRANSFORMFLAGS,

			/// <summary>
			/// Settings for the third color operand for triadic operations. Use values from <see cref="TextureArgument"/>
			/// to set this state. The default value is <see cref="TextureArgument"/>.Current.
			/// </summary>
			ColorArg0 = D3DTSS_COLORARG0,

			/// <summary>
			/// Settings for the third alpha operand for triadic operations. Use values from <see cref="TextureArgument"/>
			/// to set this state. The default value is <see cref="TextureArgument"/>.Current.
			/// </summary>
			AlphaArg0 = D3DTSS_ALPHAARG0,

			/// <summary>
			/// Setting to select the destination register for the result of this stage. Use values from
			/// <see cref="TextureArgument"/> to set this state. The default value is <see cref="TextureArgument"/>.Current.
			/// </summary>
			ResultArg = D3DTSS_RESULTARG,

			/// <summary>
			/// Per-stage constant color. Use packed integer colors to set this state.
			/// </summary>
			Constant = D3DTSS_CONSTANT
		};

		/// <summary>
		/// Identifies texture samplers used by vertex shaders.
		/// </summary>
		/// <unmanaged>D3DVERTEXTEXTURESAMPLER</unmanaged>
		public enum class VertexTextureSampler : System::Int32
		{
			Sampler0 = D3DVERTEXTEXTURESAMPLER0,
			Sampler1 = D3DVERTEXTEXTURESAMPLER1,
			Sampler2 = D3DVERTEXTEXTURESAMPLER2,
			Sampler3 = D3DVERTEXTEXTURESAMPLER3,
			DisplacementMapSampler = D3DDMAPSAMPLER,
		};
	}
}
