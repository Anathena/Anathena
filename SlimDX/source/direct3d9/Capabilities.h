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

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>Describes the vertex shader capabilities of a device.</summary>
		/// <unmanaged>D3DVSHADERCAPS2_0</unmanaged>
		public value class VertexShader20Caps : System::IEquatable<VertexShader20Caps>
		{
		public:
			/// <summary>
			/// Generic vertex shader capabilities.
			/// </summary>
			property VertexShaderCaps Caps;

			/// <summary>
			/// The depth of dynamic flow control nesting.
			/// </summary>
            property int DynamicFlowControlDepth;

			/// <summary>
			/// The number of temporary registers supported.
			/// </summary>
            property int TempCount;

			/// <summary>
			/// The depth of static flow control nesting.
			/// </summary>
            property int StaticFlowControlDepth;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( VertexShader20Caps left, VertexShader20Caps right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( VertexShader20Caps left, VertexShader20Caps right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to a specified object. 
			/// </summary>
			/// <param name="obj">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to the specified object. 
			/// </summary>
			/// <param name="other">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( VertexShader20Caps other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( VertexShader20Caps% value1, VertexShader20Caps% value2 );
		};

		/// <summary>Describes the pixel shader capabilities of a device.</summary>
		/// <unmanaged>D3DPSHADERCAPS2_0</unmanaged>
		public value class PixelShader20Caps : System::IEquatable<PixelShader20Caps>
		{
		public:
			/// <summary>
			/// Generic pixel shader caps.
			/// </summary>
			property PixelShaderCaps Caps;

			/// <summary>
			/// The depth of dynamic flow control nesting.
			/// </summary>
            property int DynamicFlowControlDepth;

			/// <summary>
			/// The number of temporary registers supported.
			/// </summary>
            property int TempCount;

			/// <summary>
			/// The depth of static flow control nesting.
			/// </summary>
            property int StaticFlowControlDepth;

			/// <summary>
			/// Number of supported instruction slots.
			/// </summary>
			property int InstructionSlotCount;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( PixelShader20Caps left, PixelShader20Caps right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( PixelShader20Caps left, PixelShader20Caps right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to a specified object. 
			/// </summary>
			/// <param name="obj">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to the specified object. 
			/// </summary>
			/// <param name="other">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( PixelShader20Caps other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( PixelShader20Caps% value1, PixelShader20Caps% value2 );
		};

		/// <summary>Represents the capabilities of the hardware exposed through the Direct3D object.</summary>
		/// <unmanaged>D3DCAPS9</unmanaged>
		public ref class Capabilities : System::IEquatable<Capabilities^>
		{
		internal:
			Capabilities( D3DCAPS9& caps );

		public:
			/// <summary>
			/// Specifies the type of the device, which identifies which type of resources are used to process vertices.
			/// </summary>
			property DeviceType DeviceType;

			/// <summary>
			/// The ordinal of the adapter on which the device resides.
			/// </summary>
            property int AdapterOrdinal;

			/// <summary>
			/// Driver-specific capabilities for the device.
			/// </summary>
            property Caps Caps;

			/// <summary>
			/// Driver-specific capabilities for the device.
			/// </summary>
            property Caps2 Caps2;

			/// <summary>
			/// Driver-specific capabilities for the device.
			/// </summary>
            property Caps3 Caps3;

			/// <summary>
			/// Specifies the set of presentation swap chain intervals that are allowed on the device.
			/// </summary>
            property PresentInterval PresentationIntervals;

			/// <summary>
			/// Indicates the amount of hardware support available for cursors.
			/// </summary>
            property CursorCaps CursorCaps;

			/// <summary>
			/// A set of flags describing the general capabilities of the device.
			/// </summary>
            property DeviceCaps DeviceCaps;

			/// <summary>
			/// Miscellaneous driver primitive capabilities.
			/// </summary>
            property PrimitiveMiscCaps PrimitiveMiscCaps;

			/// <summary>
			/// Information on raster-drawing capabilities.
			/// </summary>
            property RasterCaps RasterCaps;

			/// <summary>
			/// Z-Buffer comparison capabilities.
			/// </summary>
            property CompareCaps DepthCompareCaps;

			/// <summary>
			/// Specifies possible source blending capabilities.
			/// </summary>
            property BlendCaps SourceBlendCaps;

			/// <summary>
			/// Specifies possible destination blending capabilities.
			/// </summary>
            property BlendCaps DestinationBlendCaps;

			/// <summary>
			/// Alpah test comparison capabilities.
			/// </summary>
            property CompareCaps AlphaCompareCaps;

			/// <summary>
			/// Shading operations capabilities.
			/// </summary>
            property ShadeCaps ShadeCaps;

			/// <summary>
			/// Miscellaneous texture mapping capabilities.
			/// </summary>
            property TextureCaps TextureCaps;

			/// <summary>
			/// Specifies texture filtering capabilities.
			/// </summary>
            property FilterCaps TextureFilterCaps;

			/// <summary>
			/// Specifies cube texture filtering capabilities.
			/// </summary>
            property FilterCaps CubeTextureFilterCaps;

			/// <summary>
			/// Specifies volume texture filtering capabilities.
			/// </summary>
            property FilterCaps VolumeTextureFilterCaps;

			/// <summary>
			/// Texture-addressing capabilities for texture objects.
			/// </summary>
            property TextureAddressCaps TextureAddressCaps;

			/// <summary>
			/// Texture-addressing capabilities for volume texture objects.
			/// </summary>
            property SlimDX::Direct3D9::TextureAddressCaps VolumeTextureAddressCaps;

			/// <summary>
			/// Defines capabilities for line drawing primitives.
			/// </summary>
            property LineCaps LineCaps;

			/// <summary>
			/// Maximum possible texture width.
			/// </summary>
            property int MaxTextureWidth;

			/// <summary>
			/// Maximum possible texture height.
			/// </summary>
            property int MaxTextureHeight;

			/// <summary>
			/// Maximum value for any of the three dimensions (width, height, and depth) of a volume texture.
			/// </summary>
            property int MaxVolumeExtent;

			/// <summary>
			/// Represents the maximum range of the integer bits of the post-normalized texture coordinates.
			/// </summary>
            property int MaxTextureRepeat;

			/// <summary>
			/// Maximum texture aspect ratio supported by the hardware, typically a power of two.
			/// </summary>
            property int MaxTextureAspectRatio;

			/// <summary>
			/// Maximum valid value for the anisotropy texture-stage state.
			/// </summary>
            property int MaxAnisotropy;

			/// <summary>
			/// Maximum W-based depth value that the device supports.
			/// </summary>
            property float MaxVertexW;

			/// <summary>
			/// Screen space coordinate of the guard band clipping region. Coordinates inside this rectangle 
			/// but outside the viewport rectangle are automatically clipped.
			/// </summary>
            property float GuardBandLeft;

			/// <summary>
			/// Screen space coordinate of the guard band clipping region. Coordinates inside this rectangle 
			/// but outside the viewport rectangle are automatically clipped.
			/// </summary>
            property float GuardBandTop;

			/// <summary>
			/// Screen space coordinate of the guard band clipping region. Coordinates inside this rectangle 
			/// but outside the viewport rectangle are automatically clipped.
			/// </summary>
            property float GuardBandRight;

			/// <summary>
			/// Screen space coordinate of the guard band clipping region. Coordinates inside this rectangle 
			/// but outside the viewport rectangle are automatically clipped.
			/// </summary>
            property float GuardBandBottom;

			/// <summary>
			/// Number of pixels to adjust the extents rectangle outward to accommodate antialiasing kernels. 
			/// </summary>
            property float ExtentsAdjust;

			/// <summary>
			/// Specifies supported stencil buffer operations. Stencil operations are assumed to be 
			/// valid for all three stencil-buffer operation render states.
			/// </summary>
            property StencilCaps StencilCaps;

			/// <summary>
			/// Capabilities of the flexible vertex format system.
			/// </summary>
            property VertexFormatCaps FVFCaps;

			/// <summary>
			/// Describes the texture operations supported by the device.
			/// </summary>
            property TextureOperationCaps TextureOperationCaps;

			/// <summary>
			/// Maximum number of texture blending stages supported in the fixed function pipeline. In
			/// the programmable pipeline, this corresponds to the number of unique texture registers
			/// used by pixel shader instructions.
			/// </summary>
            property int MaxTextureBlendStages;

			/// <summary>
			/// Maximum number of textures that can be simulatenously bound to the fixed function pipeline
			/// sampler stages.
			/// </summary>
            property int MaxSimultaneousTextures;

			/// <summary>
			/// Specifies the vertex processing capabilities of the device.
			/// </summary>
            property VertexProcessingCaps VertexProcessingCaps;

			/// <summary>
			/// Maximum number of lights that can be activated simultaneously.
			/// </summary>
            property int MaxActiveLights;

			/// <summary>
			/// Maximum number of user defined clip planes supported.
			/// </summary>
            property int MaxUserClipPlanes;

			/// <summary>
			/// Maximum number of matrices that the device can apply when performing multimatrix vertex blending.
			/// </summary>
            property int MaxVertexBlendMatrices;

			/// <summary>
			/// Maximum matrix index that can be indexed into using the per-vertex indices.
			/// </summary>
            property int MaxVertexBlendMatrixIndex;

			/// <summary>
			/// Maximum size of a point primitive.
			/// </summary>
            property float MaxPointSize;

			/// <summary>
			/// Maximum number of primitives for each <see cref="SlimDX::Direct3D9::Device">Device.DrawPrimitive</see> call.
			/// </summary>
            property int MaxPrimitiveCount;

			/// <summary>
			/// Maximum size of indices supported for hardware vertex processing.
			/// </summary>
            property int MaxVertexIndex;

			/// <summary>
			/// Maximum number of concurrent data streams.
			/// </summary>
            property int MaxStreams;

			/// <summary>
			/// Maximum stride for data streams.
			/// </summary>
            property int MaxStreamStride;

			/// <summary>
			/// The vertex shader version.
			/// </summary>
            property System::Version^ VertexShaderVersion;

			/// <summary>
			/// The number of vertex shaders registers that are reserved for constants.
			/// </summary>
            property int MaxVertexShaderConstants;

			/// <summary>
			/// The pixel shader version.
			/// </summary>
            property System::Version^ PixelShaderVersion;

			/// <summary>
			/// Maximum value of the pixel shader arithmetic component.
			/// </summary>
            property float PixelShader1xMaxValue;

			/// <summary>
			/// Device driver capabilities for adaptive tessellation.
			/// </summary>
			property SlimDX::Direct3D9::DeviceCaps2 DeviceCaps2;

			/// <summary>
			/// Maximum tessellation level for N-patches.
			/// </summary>
            property float MaxNPatchTessellationLevel;

			/// <summary>
			/// Indicates which device is master for this subordinate.
			/// </summary>
            property int MasterAdapterOrdinal;

			/// <summary>
			/// Indicates the order in which heads are referenced by the API.
			/// </summary>
            property int AdapterOrdinalInGroup;

			/// <summary>
			/// The number of adapters in the group.
			/// </summary>
            property int NumberOfAdaptersInGroup;

			/// <summary>
			/// Possible data types contained in a vertex declaration.
			/// </summary>
            property DeclarationTypeCaps DeclarationTypes;

			/// <summary>
			/// Number of simultaenous render targets.
			/// </summary>
            property int SimultaneousRTCount;

			/// <summary>
			/// Describes the operations supported by <see cref="SlimDX::Direct3D9::Device">Device.StretchRectangle</see>.
			/// </summary>
            property FilterCaps StretchRectFilterCaps;

			/// <summary>
			/// Extended capabilities for vertex shader version 2.0.
			/// </summary>
            property VertexShader20Caps VS20Caps;

			/// <summary>
			/// Extended capabilities for pixel shader version 2.0.
			/// </summary>
            property PixelShader20Caps PS20Caps;

			/// <summary>
			/// Vertex shader texture filtering capabilities.
			/// </summary>
            property FilterCaps VertexTextureFilterCaps;

			/// <summary>
			/// Maximum number of vertex shader instructions that can be run when using flow control.
			/// </summary>
			property int MaxVShaderInstructionsExecuted;

			/// <summary>
			/// Maximum number of pixel shader instructions that can be run when using flow control.
			/// </summary>
			property int MaxPShaderInstructionsExecuted;

			/// <summary>
			/// Maximum number of vertex shader instruction slots.
			/// </summary>
			property int MaxVertexShader30InstructionSlots;

			/// <summary>
			/// Maximum number of pixel shader instruction slots.
			/// </summary>
			property int MaxPixelShader30InstructionSlots;

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( Capabilities^ left, Capabilities^ right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( Capabilities^ left, Capabilities^ right );

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to a specified object. 
			/// </summary>
			/// <param name="obj">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value that indicates whether the current instance is equal to the specified object. 
			/// </summary>
			/// <param name="other">Object to make the comparison with.</param>
			/// <returns><c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
			virtual bool Equals( Capabilities^ other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( Capabilities^ value1, Capabilities^ value2 );
		};
	}
}