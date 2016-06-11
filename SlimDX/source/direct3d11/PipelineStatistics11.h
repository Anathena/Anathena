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

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Contains query information about graphics pipeline activity.
		/// </summary>
		/// <unmanaged>D3D11_QUERY_DATA_PIPELINE_STATISTICS</unmanaged>
		public value class PipelineStatistics : System::IEquatable<PipelineStatistics>
		{
		internal:
			PipelineStatistics( const D3D11_QUERY_DATA_PIPELINE_STATISTICS& native );
			
			D3D11_QUERY_DATA_PIPELINE_STATISTICS CreateNativeVersion();
			
		public:
			/// <summary>
			/// Number of vertices read by input assembler.
			/// </summary>
			property System::Int64 InputAssemblerVertices;

			/// <summary>
			/// Number of primitives read by the input assembler. This number can be different depending on the primitive topology used. For example, a triangle strip with 6 vertices will produce 4 triangles, however a triangle list with 6 vertices will produce 2 triangles.
			/// </summary>
			property System::Int64 InputAssemblerPrimitives;

			/// <summary>
			/// Number of times a vertex shader was invoked. Direct3D invokes the vertex shader once per vertex.
			/// </summary>
			property System::Int64 VertexShaderInvocations;

			/// <summary>
			/// Number of times a geometry shader was invoked. When the geometry shader is set to <c>null</c>, this statistic may or may not increment depending on the hardware manufacturer.
			/// </summary>
			property System::Int64 GeometryShaderInvocations;

			/// <summary>
			/// Number of primitives output by a geometry shader.
			/// </summary>
			property System::Int64 GeometryShaderPrimitives;

			/// <summary>
			/// Number of primitives that were sent to the rasterizer. When the rasterizer is disabled, this will not increment.
			/// </summary>
			property System::Int64 RasterizedPrimitives;

			/// <summary>
			/// Number of primitives that were rendered. This may be larger or smaller than CInvocations because after a primitive is clipped sometimes it is either broken up into more than one primitive or completely culled.
			/// </summary>
			property System::Int64 RenderedPrimitives;

			/// <summary>
			/// Number of times a pixel shader was invoked.
			/// </summary>
			property System::Int64 PixelShaderInvocations;

			/// <summary>
			/// Number of times a hull shader was invoked.
			/// </summary>
			property System::Int64 HullShaderInvocations;

			/// <summary>
			/// Number of times a domain shader was invoked.
			/// </summary>
			property System::Int64 DomainShaderInvocations;

			/// <summary>
			/// Number of times a compute shader was invoked.
			/// </summary>
			property System::Int64 ComputeShaderInvocations;
		
			/// <summary>
			/// Tests for equality between two PipelineStatisticss.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( PipelineStatistics left, PipelineStatistics right );
			
			/// <summary>
			/// Tests for inequality between two PipelineStatisticss.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( PipelineStatistics left, PipelineStatistics right );
			
			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;
			
			/// <summary>
			/// Returns a value indicating whether this instance is equal to the specified object.
			/// </summary>
			/// <param name="obj">An object to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="obj"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( System::Object^ obj ) override;

			/// <summary>
			/// Returns a value indicating whether this instance is equal to the specified object.
			/// </summary>
			/// <param name="other">A <see cref="PipelineStatistics"/> to compare with this instance.</param>
			/// <returns><c>true</c> if <paramref name="other"/> has the same value as this instance; otherwise, <c>false</c>.</returns>
			virtual bool Equals( PipelineStatistics other );

			/// <summary>
			/// Returns a value indicating whether the two BufferDescriptions are equivalent.
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> has the same value as <paramref name="value2"/>; otherwise, <c>false</c>.</returns>
			static bool Equals( PipelineStatistics% value1, PipelineStatistics% value2 );
		};
	}
};