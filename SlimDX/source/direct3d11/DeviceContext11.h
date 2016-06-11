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

#include "../math/Color4.h"
#include "../dxgi/Enums.h"

#include "../DataBox.h"
#include "../ComObject.h"

#include "DeviceChild11.h"
#include "Enums11.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Asynchronous;
		ref class Device;
		ref class CommandList;
		ref class Resource;
		ref class DepthStencilView;
		ref class RenderTargetView;
		ref class ShaderResourceView;
		ref class UnorderedAccessView;
		ref class Buffer;
		ref class Predicate;
		ref class Texture1D;
		ref class Texture2D;
		ref class Texture3D;
		value class ResourceRegion;

		ref class GeometryShaderWrapper;
		ref class InputAssemblerWrapper;
		ref class OutputMergerWrapper;
		ref class RasterizerWrapper;
		ref class PixelShaderWrapper;
		ref class StreamOutputWrapper;
		ref class VertexShaderWrapper;
		ref class DomainShaderWrapper;
		ref class HullShaderWrapper;
		ref class ComputeShaderWrapper;

		/// <summary>Represents a device context which generates rendering commands.</summary>
		/// <unmanaged>ID3D11DeviceContext</unmanaged>
		public ref class DeviceContext : DeviceChild
		{
			COMOBJECT_CUSTOM(ID3D11DeviceContext, DeviceContext);

		private:
			InputAssemblerWrapper^ inputAssembler;
			OutputMergerWrapper^ outputMerger;
			StreamOutputWrapper^ streamOutput;
			RasterizerWrapper^ rasterizer;
			VertexShaderWrapper^ vertexShader;
			PixelShaderWrapper^ pixelShader;
			GeometryShaderWrapper^ geometryShader;
			DomainShaderWrapper^ domainShader;
			HullShaderWrapper^ hullShader;
			ComputeShaderWrapper^ computeShader;

			void InitializeSubclasses();

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="DeviceContext"/> class. This instance will be a deferred rendering context.
			/// </summary>
			/// <param name="device">The device used to create the context.</param>
			DeviceContext( SlimDX::Direct3D11::Device^ device );

			/// <summary>
			/// Marks the beginning of a series of asynchronous commands.
			/// </summary>
			/// <param name="data">An asynchronous data provider.</param>
			void Begin( Asynchronous^ data );

			/// <summary>
			/// Clears the specified depth-stencil resource.
			/// </summary>
			/// <param name="view">The view of the resource to clear.</param>
			/// <param name="flags">Flags indicating which parts of the resource to clear.</param>
			/// <param name="depth">The depth value to clear to, if applicable.</param>
			/// <param name="stencil">The stencil value to clear to, if applicable.</param>
			void ClearDepthStencilView( DepthStencilView^ view, DepthStencilClearFlags flags, float depth, System::Byte stencil );
			
			/// <summary>
			/// Clears the specified render target resource.
			/// </summary>
			/// <param name="view">The view of the resource to clear.</param>
			/// <param name="color">The color to clear to.</param>
			void ClearRenderTargetView( RenderTargetView^ view, Color4 color );

			/// <summary>
			/// Restores all device state to defaults.
			/// </summary>
			void ClearState();

			/// <summary>
			/// Clears an unordered access resource with the given values.
			/// </summary>
			/// <param name="unorderedAccessView">The view of the resource to clear.</param>
			/// <param name="values">Values that will be copied to corresponding channels on the resource.</param>
			void ClearUnorderedAccessView( UnorderedAccessView^ unorderedAccessView, array<int>^ values );

			/// <summary>
			/// Clears an unordered access resource with the given values.
			/// </summary>
			/// <param name="unorderedAccessView">The view of the resource to clear.</param>
			/// <param name="values">Values that will be copied to corresponding channels on the resource.</param>
			void ClearUnorderedAccessView( UnorderedAccessView^ unorderedAccessView, array<float>^ values );
			
			/// <summary>
			/// Copies resource data using the GPU.
			/// </summary>
			/// <param name="source">The source resource.</param>
			/// <param name="destination">The destination resource.</param>
			void CopyResource( Resource^ source, Resource^ destination );

			/// <summary>
			/// Copies data from a buffer holding variable length data.
			/// </summary>
			/// <param name="unorderedAccessView">The view of the source resource.</param>
			/// <param name="destinationBuffer">The destination buffer.</param>
			/// <param name="destinationOffset">Aligned byte offset from the start of the destination buffer to which data will be copied.</param>
			void CopyStructureCount( UnorderedAccessView^ unorderedAccessView, Buffer^ destinationBuffer, int destinationOffset );

			/// <summary>
			/// Copies a portion of a resource's data using the GPU.
			/// </summary>
			/// <param name="source">The source resource.</param>
			/// <param name="sourceSubresource">The source subresource.</param>
			/// <param name="region">The source region.</param>
			/// <param name="destination">The destination resource.</param>
			/// <param name="destinationSubresource">The destination subresource.</param>
			/// <param name="x">The X offset between the source region origin and the destination location.</param>
			/// <param name="y">The Y offset between the source region origin and the destination location.</param>
			/// <param name="z">The Z offset between the source region origin and the destination location.</param>
			void CopySubresourceRegion( Resource^ source, int sourceSubresource, ResourceRegion region, Resource^ destination, int destinationSubresource, int x, int y, int z );

			/// <summary>
			/// Copies an entire region of a resource's subresource data using the GPU.
			/// </summary>
			/// <param name="source">The source resource.</param>
			/// <param name="sourceSubresource">The source subresource.</param>
			/// <param name="destination">The destination resource.</param>
			/// <param name="destinationSubresource">The destination subresource.</param>
			/// <param name="x">The X offset between the source region origin and the destination location.</param>
			/// <param name="y">The Y offset between the source region origin and the destination location.</param>
			/// <param name="z">The Z offset between the source region origin and the destination location.</param>
			void CopySubresourceRegion( Resource^ source, int sourceSubresource, Resource^ destination, int destinationSubresource, int x, int y, int z );
			
			/// <summary>
			/// Copies a multisampled resource into a non-multisampled resource.
			/// </summary>
			/// <param name="source">The source resource.</param>
			/// <param name="sourceSubresource">The source subresource.</param>
			/// <param name="destination">The destination resource.</param>
			/// <param name="destinationSubresource">The destination subresource.</param>
			/// <param name="format">The format that indicates how the multisampled resource will be resolved.</param>
			void ResolveSubresource( Resource^ source, int sourceSubresource, Resource^ destination, int destinationSubresource, DXGI::Format format );
			
			/// <summary>
			/// Copies data from the CPU to to a non-mappable subresource region.
			/// </summary>
			/// <param name="source">The source data.</param>
			/// <param name="resource">The destination resource.</param>
			/// <param name="subresource">The destination subresource.</param>
			void UpdateSubresource( DataBox^ source, Resource^ resource, int subresource );

			/// <summary>
			/// Copies data from the CPU to to a non-mappable subresource region.
			/// </summary>
			/// <param name="source">The source data.</param>
			/// <param name="resource">The destination resource.</param>
			/// <param name="subresource">The destination subresource.</param>
			/// <param name="region">The destination region within the resource.</param>
			void UpdateSubresource( DataBox^ source, Resource^ resource, int subresource, ResourceRegion region );

			/// <summary>
			/// Executes a command list from a thread group.
			/// </summary>
			/// <param name="threadGroupCountX">The number of groups dispatched in the x direction.</param>
			/// <param name="threadGroupCountY">The number of groups dispatched in the y direction.</param>
			/// <param name="threadGroupCountZ">The number of groups dispatched in the z direction.</param>
			void Dispatch( int threadGroupCountX, int threadGroupCountY, int threadGroupCountZ );

			/// <summary>
			/// Execute a command list to draw GPU-generated primitives over one of more thread groups.
			/// </summary>
			/// <param name="arguments">A buffer containing arguments for the thread groups.</param>
			/// <param name="alignedOffset">A byte-aligned offset between the start of the buffer and the arguments.</param>
			void DispatchIndirect( Buffer^ arguments, int alignedOffset );

			/// <summary>
			/// Renders non-indexed, non-instanced primitive data.
			/// </summary>
			/// <param name="vertexCount">The number of vertices to use.</param>
			/// <param name="startVertexLocation">The index of the first vertex.</param>
			void Draw( int vertexCount, int startVertexLocation );

			/// <summary>
			/// Renders non-indexed, instanced primitive data.
			/// </summary>
			/// <param name="vertexCountPerInstance">The number of vertices to use per instance.</param>
			/// <param name="instanceCount">The number of instances to render.</param>
			/// <param name="startVertexLocation">The index of the first vertex.</param>
			/// <param name="startInstanceLocation">The index of the first instance.</param>
			void DrawInstanced( int vertexCountPerInstance, int instanceCount, int startVertexLocation, int startInstanceLocation );

			/// <summary>
			/// Renders non-indexed, instanced, GPU-generated primitive data.
			/// </summary>
			/// <param name="arguments">A buffer containing arguments for the draw call.</param>
			/// <param name="alignedOffset">A byte-aligned offset between the start of the buffer and the arguments.</param>
			void DrawInstancedIndirect( Buffer^ arguments, int alignedOffset );
			
			/// <summary>
			/// Renders indexed, non-instanced primitive data.
			/// </summary>
			/// <param name="indexCount">The number of indices to use.</param>
			/// <param name="startIndexLocation">The index of the first index.</param>
			/// <param name="baseVertexLocation">The index of the first vertex.</param>
			void DrawIndexed( int indexCount, int startIndexLocation, int baseVertexLocation );

			/// <summary>
			/// Renders indexed, instanced primitive data.
			/// </summary>
			/// <param name="indexCountPerInstance">The number of indices to use per instance.</param>
			/// <param name="instanceCount">The number of instances to render.</param>
			/// <param name="startIndexLocation">The index of the first index.</param>
			/// <param name="baseVertexLocation">The index of the first vertex.</param>
			/// <param name="startInstanceLocation">The index of the first instance.</param>
			void DrawIndexedInstanced( int indexCountPerInstance, int instanceCount, int startIndexLocation, int baseVertexLocation, int startInstanceLocation );
			
			/// <summary>
			/// Renders indexed, instanced, GPU-generated primitive data.
			/// </summary>
			/// <param name="arguments">A buffer containing arguments for the draw call.</param>
			/// <param name="alignedOffset">A byte-aligned offset between the start of the buffer and the arguments.</param>
			void DrawIndexedInstancedIndirect( Buffer^ arguments, int alignedOffset );

			/// <summary>
			/// Renders primitive data of an unknown size that was created by a geometry shader.
			/// </summary>
			void DrawAuto();

			/// <summary>
			/// Marks the end of a series of asynchronous commands.
			/// </summary>
			/// <param name="data">An asynchronous data provider.</param>
			void End( Asynchronous^ data );

			/// <summary>
			/// Queues commands from a command list onto a device.
			/// </summary>
			/// <param name="commands">The list of commands to execute.</param>
			/// <param name="restoreState">A flag indicating whether the immediate context state is saved (prior) and restored (after) the execution of a command list.</param>
			void ExecuteCommandList( CommandList^ commands, bool restoreState );

			/// <summary>
			/// Create a command list and record graphics commands into it.
			/// </summary>
			/// <param name="restoreState">A flag indicating whether the immediate context state is saved (prior) and restored (after) the execution of a command list.</param>
			/// <returns>The created command list containing the queued rendering commands.</returns>
			CommandList^ FinishCommandList( bool restoreState );

			/// <summary>
			/// Sends queued commands in the command buffer to the GPU.
			/// </summary>
			void Flush();
			
			/// <summary>
			/// Generates mipmaps for the specified shader resource.
			/// </summary>
			/// <param name="view">A view of the resource for which to generate mipmaps.</param>
			void GenerateMips( ShaderResourceView^ view );

			/// <summary>
			/// Gets the minimum level-of-detail for a resource.
			/// </summary>
			/// <param name="resource">The resource for which level-of-detail information is to be retrieved.</param>
			/// <returns>The minimum level-of-detail for the specified resource.</returns>
			float GetMinimumLod( Resource^ resource );

			/// <summary>
			/// Sets the minimum level-of-detail for a resource.
			/// </summary>
			/// <param name="resource">The resource for which level-of-detail information is to be set.</param>
			/// <param name="minimumLod">The level-of-detail for the resource.</param>
			void SetMinimumLod( Resource^ resource, float minimumLod );

			/// <summary>
			/// Determines if data is available for an asynchronous object.
			/// </summary>
			/// <param name="data">The asynchronous data provider.</param>
			/// <returns><c>true</c> if query data is available for retrieval; otherwise, <c>false</c>.</returns>
			bool IsDataAvailable( Asynchronous^ data );

			/// <summary>
			/// Determines if data is available for an asynchronous object.
			/// </summary>
			/// <param name="data">The asynchronous data provider.</param>
			/// <param name="flags">Flags specifying how the command should operate.</param>
			/// <returns><c>true</c> if query data is available for retrieval; otherwise, <c>false</c>.</returns>
			bool IsDataAvailable( Asynchronous^ data, AsynchronousFlags flags );

			/// <summary>
			/// Gets data from the GPU asynchronously.
			/// </summary>
			/// <param name="data">The asynchronous data provider.</param>
			/// <returns>The data retrieved from the GPU.</returns>
			DataStream^ GetData( Asynchronous^ data );

			/// <summary>
			/// Gets data from the GPU asynchronously.
			/// </summary>
			/// <param name="data">The asynchronous data provider.</param>
			/// <param name="flags">Flags specifying how the command should operate.</param>
			/// <returns>The data retrieved from the GPU.</returns>
			DataStream^ GetData( Asynchronous^ data, AsynchronousFlags flags );

			/// <summary>
			/// Gets data from the GPU asynchronously.
			/// </summary>
			/// <param name="data">The asynchronous data provider.</param>
			/// <returns>The data retrieved from the GPU.</returns>
			generic<typename T> where T : value class
			T GetData( Asynchronous^ data );

			/// <summary>
			/// Gets data from the GPU asynchronously.
			/// </summary>
			/// <param name="data">The asynchronous data provider.</param>
			/// <param name="flags">Flags specifying how the command should operate.</param>
			/// <returns>The data retrieved from the GPU.</returns>
			generic<typename T> where T : value class
			T GetData( Asynchronous^ data, AsynchronousFlags flags );

			/// <summary>
			/// Gets the rendering predicate state.
			/// </summary>
			/// <param name="predicate">When the method completes, contains the predicate interface currently in use.</param>
			/// <param name="predicateValue">When the method completes, contains the predicate comparison value.</param>
			void GetPredication( [Out] Predicate^ %predicate, [Out] bool %predicateValue );

			/// <summary>
			/// Maps a GPU resource into CPU-accessible memory.
			/// </summary>
			/// <param name="resource">The resource to map.</param>
			/// <param name="mipSlice">A zero-based index into an array of subtextures; 0 indicates the first, most detailed subtexture (or mipmap level).</param>
			/// <param name="arraySlice">The zero-based index of the first texture to use (in an array of textures).</param>
			/// <param name="mode">Specifies the CPU's read and write permissions for the resource. </param>
			/// <param name="flags">Flags that specify what the CPU should do when the GPU is busy.</param>
			/// <returns>The mapped resource data.</returns>
			DataBox^ MapSubresource( Texture1D^ resource, int mipSlice, int arraySlice, MapMode mode, MapFlags flags );

			/// <summary>
			/// Maps a GPU resource into CPU-accessible memory.
			/// </summary>
			/// <param name="resource">The resource to map.</param>
			/// <param name="mipSlice">A zero-based index into an array of subtextures; 0 indicates the first, most detailed subtexture (or mipmap level).</param>
			/// <param name="arraySlice">The zero-based index of the first texture to use (in an array of textures).</param>
			/// <param name="mode">Specifies the CPU's read and write permissions for the resource. </param>
			/// <param name="flags">Flags that specify what the CPU should do when the GPU is busy.</param>
			/// <returns>The mapped resource data.</returns>
			DataBox^ MapSubresource( Texture2D^ resource, int mipSlice, int arraySlice, MapMode mode, MapFlags flags );

			/// <summary>
			/// Maps a GPU resource into CPU-accessible memory.
			/// </summary>
			/// <param name="resource">The resource to map.</param>
			/// <param name="mipSlice">A zero-based index into an array of subtextures; 0 indicates the first, most detailed subtexture (or mipmap level).</param>
			/// <param name="arraySlice">The zero-based index of the first texture to use (in an array of textures).</param>
			/// <param name="mode">Specifies the CPU's read and write permissions for the resource. </param>
			/// <param name="flags">Flags that specify what the CPU should do when the GPU is busy.</param>
			/// <returns>The mapped resource data.</returns>
			DataBox^ MapSubresource( Texture3D^ resource, int mipSlice, int arraySlice, MapMode mode, MapFlags flags );

			/// <summary>
			/// Maps a GPU resource into CPU-accessible memory.
			/// </summary>
			/// <param name="resource">The resource to map.</param>
			/// <param name="mode">Specifies the CPU's read and write permissions for the resource. </param>
			/// <param name="flags">Flags that specify what the CPU should do when the GPU is busy.</param>
			/// <returns>The mapped resource data.</returns>
			DataBox^ MapSubresource( Buffer^ resource, MapMode mode, MapFlags flags );

			/// <summary>
			/// Maps a GPU resource into CPU-accessible memory.
			/// </summary>
			/// <param name="resource">The resource to map.</param>
			/// <param name="subresource">Index of the subresource level to lock.</param>
			/// <param name="mode">Specifies the CPU's read and write permissions for the resource. </param>
			/// <param name="flags">Flags that specify what the CPU should do when the GPU is busy.</param>
			/// <returns>The mapped resource data.</returns>
			DataBox^ MapSubresource( Resource^ resource, int subresource, MapMode mode, MapFlags flags );

			/// <summary>
			/// Releases a previously mapped resource.
			/// </summary>
			/// <param name="resource">The resource to unmap.</param>
			/// <param name="subresource">Index of the subresource to unmap.</param>
			void UnmapSubresource( Resource^ resource, int subresource );
			
			/// <summary>
			/// Sets a rendering predicate.
			/// </summary>
			/// <param name="predicate">The predicate to set.</param>
			/// <param name="predicateValue">If true, rendering will be affected when the predicate's conditions are met. Otherwise,
			/// rendering will be affected when the predicate's conditions are not met.</param>
			void SetPredication( Predicate^ predicate, bool predicateValue );

			/// <summary>
			/// Gets the type of the device context.
			/// </summary>
			property DeviceContextType Type
			{
				DeviceContextType get();
			}

			/// <summary>
			/// Gets the device's input assembler interface.
			/// </summary>
			property InputAssemblerWrapper^ InputAssembler
			{
				InputAssemblerWrapper^ get();
			}

			/// <summary>
			/// Gets the device's output merger interface.
			/// </summary>
			property OutputMergerWrapper^ OutputMerger
			{
				OutputMergerWrapper^ get();
			}

			/// <summary>
			/// Gets the device's stream output interface.
			/// </summary>
			property StreamOutputWrapper^ StreamOutput
			{
				StreamOutputWrapper^ get();
			}

			/// <summary>
			/// Gets the device's rasterizer interface.
			/// </summary>
			property RasterizerWrapper^ Rasterizer
			{
				RasterizerWrapper^ get();
			}

			/// <summary>
			/// Gets the device's vertex shader interface.
			/// </summary>
			property VertexShaderWrapper^ VertexShader
			{
				VertexShaderWrapper^ get();
			}

			/// <summary>
			/// Gets the device's pixel shader interface.
			/// </summary>
			property PixelShaderWrapper^ PixelShader
			{
				PixelShaderWrapper^ get();
			}

			/// <summary>
			/// Gets the device's geometry shader interface.
			/// </summary>
			property GeometryShaderWrapper^ GeometryShader
			{
				GeometryShaderWrapper^ get();
			}

			/// <summary>
			/// Gets the device's domain shader interface.
			/// </summary>
			property DomainShaderWrapper^ DomainShader
			{
				DomainShaderWrapper^ get();
			}

			/// <summary>
			/// Gets the device's hull shader interface.
			/// </summary>
			property HullShaderWrapper^ HullShader
			{
				HullShaderWrapper^ get();
			}

			/// <summary>
			/// Gets the device's compute shader interface.
			/// </summary>
			property ComputeShaderWrapper^ ComputeShader
			{
				ComputeShaderWrapper^ get();
			}
		};
	}
}