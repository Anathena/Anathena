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

#include "../ComObject.h"

#include "../dxgi/Adapter.h"
#include "../dxgi/Enums.h"
#include "../dxgi/SwapChainDxgi.h"
#include "../dxgi/SwapChainDescription.h"
#include "../math/Color4.h"

#include "Enums.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	ref class DataBox;
	
	namespace Direct3D10
	{
		ref class Buffer;
		ref class DepthStencilView;
		ref class GeometryShaderWrapper;
		ref class InputAssemblerWrapper;
		ref class InputLayout;
		ref class OutputMergerWrapper;
		ref class Predicate;
		ref class RasterizerWrapper;
		ref class RenderTargetView;
		ref class Resource;
		ref class PixelShaderWrapper;
		ref class ShaderResourceView;
		ref class StreamOutputWrapper;
		ref class VertexShaderWrapper;
		value class CounterCapabilities;
		value class CounterDescription;
		value class CounterMetadata;
		value class RenderTargetViewDescription;
		value class ResourceRegion;

		/// <summary>
		/// A virtual adapter for performing rendering.
		/// </summary>
		/// <unmanaged>ID3D10Device</unmanaged>
		public ref class Device : ComObject
		{
			COMOBJECT_CUSTOM(ID3D10Device, Device);
		
		private:
			InputAssemblerWrapper^ m_InputAssembler;
			OutputMergerWrapper^ m_OutputMerger;
			StreamOutputWrapper^ m_StreamOutput;
			RasterizerWrapper^ m_Rasterizer;
			VertexShaderWrapper^ m_VertexShader;
			PixelShaderWrapper^ m_PixelShader;
			GeometryShaderWrapper^ m_GeometryShader;

		protected:
			//no-op ctor for use by Device1
			Device() { }

			void InitializeSubclasses();

		public:
			/// <summary>
			/// The maximum number of multisample quality levels supported.
			/// </summary>
			literal int MultisampleCountMaximum = D3D10_MAX_MULTISAMPLE_SAMPLE_COUNT;

			/// <summary>
			/// Gets the parent factory associated with the device.
			/// </summary>
			property SlimDX::DXGI::Factory^ Factory
			{
				SlimDX::DXGI::Factory^ get();
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
			/// Gets the device's creation flags.
			/// </summary>
			property DeviceCreationFlags CreationFlags
			{
				DeviceCreationFlags get();
			}

			property Result DeviceRemovedReason
			{
				Result get();
			}

			/// <summary>
			/// Gets or sets a friendly debug name for the object. This name is used in debug messages
			/// as well as in the PIX object browser.
			/// </summary>
			property System::String^ DebugName
			{
				System::String^ get();
				void set(System::String^ value);
			}
			
			/// <summary>
			/// Constructs a new Device object.
			/// </summary>
			/// <param name="flags">Flags that indicate which device API layers to enable.</param>
			Device( DeviceCreationFlags flags );

			/// <summary>
			/// Constructs a new Device object.
			/// </summary>
			/// <param name="driverType">The device driver type.</param>
			/// <param name="flags">Flags that indicate which device API layers to enable.</param>
			Device( DriverType driverType, DeviceCreationFlags flags );

			/// <summary>
			/// Constructs a new Device object.
			/// </summary>
			/// <param name="adapter">The display adapter to associate the device with when creating a hardware device.</param>
			/// <param name="driverType">The device driver type.</param>
			/// <param name="flags">Flags that indicate which device API layers to enable.</param>
			Device( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags );

			/// <summary>
			/// Gets information about the device's performance counters.
			/// </summary>
			/// <returns>Information about the device's performance counters.</returns>
			CounterCapabilities GetCounterCapabilities();

			/// <summary>
			/// Gets metadata (name, type, measurement units, et cetera) for a specific counter.
			/// </summary>
			/// <param name="description">The description of the counter to retrieve information about.</param>
			/// <returns>Metadata for the specified counter.</returns>
			CounterMetadata GetCounterMetadata( CounterDescription description );
			
			/// <summary>
			/// Gets information about the supported applications of a specified format.
			/// </summary>
			/// <param name="format">The format to check support for.</param>
			/// <returns>FormatSupport flags indicating usage contexts in which the specified format is supported.</returns>
			FormatSupport CheckFormatSupport( DXGI::Format format );

			/// <summary>
			/// Gets the number of quality levels supported during multisampling of resources using a specified format.
			/// </summary>
			/// <param name="format">The format to check support for.</param>
			/// <param name="sampleCount">The multisample count to check support for.</param>
			/// <returns>The number of quality levels supported. 0 if an error occured
			/// or if the format/samplecount pair is not supported.</returns>
			int CheckMultisampleQualityLevels( DXGI::Format format, int sampleCount );

			/// <summary>
			/// Gives the device access to a shared resource created on a different device.
			/// </summary>
			/// <typeparam name="T">The type of the resource.</typeparam>
			/// <param name="handle">A handle to the shared resource.</param>
			/// <returns>The new reference to the shared resource.</returns>
			generic<typename T> where T : ComObject
			T OpenSharedResource(System::IntPtr handle);
			
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
			/// Clears all resource bindings from the device.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ClearAllObjects();
			
			/// <summary>
			/// Copies resource data using the GPU.
			/// </summary>
			/// <param name="source">The source resource.</param>
			/// <param name="destination">The destination resource.</param>
			void CopyResource( Resource^ source, Resource^ destination );

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
			/// Renders non-indexed, non-instanced primitive data.
			/// </summary>
			/// <param name="vertexCount">The number of vertices to use.</param>
			/// <param name="startVertexLocation">The index of the first vertex.</param>
			void Draw( int vertexCount, int startVertexLocation );

			/// <summary>
			/// Renders non-indexed, instanced primitive data.
			/// </summary>
			/// <param name="vertexCountPerInstance">The number of vertices to use per index.</param>
			/// <param name="instanceCount">The number of instances to render.</param>
			/// <param name="startVertexLocation">The index of the first vertex.</param>
			/// <param name="startInstanceLocation">The index of the first instance.</param>
			void DrawInstanced( int vertexCountPerInstance, int instanceCount, int startVertexLocation, int startInstanceLocation );
			
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
			/// <param name="indexCountPerInstance">The number of indices to use per index.</param>
			/// <param name="instanceCount">The number of instances to render.</param>
			/// <param name="startIndexLocation">The index of the first index.</param>
			/// <param name="baseVertexLocation">The index of the first vertex.</param>
			/// <param name="startInstanceLocation">The index of the first instance.</param>
			void DrawIndexedInstanced( int indexCountPerInstance, int instanceCount, int startIndexLocation, int baseVertexLocation, int startInstanceLocation );
			
			/// <summary>
			/// Renders primitive data of an unknown size that was created by a geometry shader.
			/// </summary>
			void DrawAuto();

			/// <summary>
			/// Sends queued commands in the command buffer to the GPU.
			/// </summary>
			void Flush();
			
			/// <summary>
			/// Generates mipmaps for the specified shader resource.
			/// </summary>
			/// <param name="view">A view of the resource to generate mipmaps for.</param>
			void GenerateMips( ShaderResourceView^ view );

			void GetPredication( [Out] Predicate^ %predicate, [Out] bool %predicateValue ); 
			
			/// <summary>
			/// Sets a rendering predicate.
			/// </summary>
			/// <param name="predicate">The predicate to set.</param>
			/// <param name="predicateValue">If true, rendering will be affected when the predicate's conditions are met. Otherwise,
			/// rendering will be affected when the predicate's conditions are not met.</param>
			void SetPredication( Predicate^ predicate, bool predicateValue );
			
			/// <summary>
			/// Creates a new device and swap chain.
			/// </summary>
			/// <param name="adapter">The display adapter to associate the device with when creating a hardware device.</param>
			/// <param name="driverType">The device driver type.</param>
			/// <param name="flags">Flags that indicate which device API layers to enable.</param>
			/// <param name="swapChainDescription">Swap chain properties.</param>
			/// <param name="device">The newly-created device.</param>
			/// <param name="swapChain">The newly-created swap chain.</param>
			/// <returns>A result code.</returns>
			static Result CreateWithSwapChain( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain );
		};
	}
};