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

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class ComputeShader;
		ref class Buffer;
		ref class ShaderResourceView;
		ref class SamplerState;
		ref class ClassInstance;
		ref class UnorderedAccessView;

		/// <summary>
		/// Defines a wrapper for compute shader related commands on the device.
		/// </summary>
		public ref class ComputeShaderWrapper
		{
		private:
			ID3D11DeviceContext* deviceContext;
			
		internal:
			ComputeShaderWrapper( ID3D11DeviceContext* deviceContext );

		public:
			/// <summary>
			/// Assigns a compute shader to the device.
			/// </summary>
			/// <param name="shader">The shader to assign to the device. Assign <c>null</c> to disable the compute shader.</param>
			void Set( ComputeShader^ shader );

			/// <summary>
			/// Assigns a compute shader to the device.
			/// </summary>
			/// <param name="shader">The shader to assign to the device. Assign <c>null</c> to disable the compute shader.</param>
			/// <param name="classInstances">An array of class-instance interfaces. Each interface used by a shader must have a corresponding class instance or the shader will get disabled.</param>
			void Set( ComputeShader^ shader, array<ClassInstance^>^ classInstances );

			/// <summary>
			/// Gets the compute shader currently assigned to the device.
			/// </summary>
			/// <returns>The compute shader (null if no shader is assigned).</returns>
			ComputeShader^ Get();

			/// <summary>
			/// Gets the compute shader currently assigned to the device.
			/// </summary>
			/// <param name="count">The number of class instances to retrieve.</param>
			/// <param name="classInstances">An array that will be used to contain any class instances currently active.</param>
			/// <returns>The compute shader (null if no shader is assigned).</returns>
			ComputeShader^ Get( int count, [Out] array<ClassInstance^>^ %classInstances );

			/// <summary>
			/// Gets the constant buffers used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array from which to begin retrieving constant buffers.</param>
			/// <param name="count">Number of buffers to retrieve.</param>
			/// <returns>An array of constant buffers.</returns>
			array<Buffer^>^ GetConstantBuffers( int startSlot, int count );

			/// <summary>
			/// Gets the sampler states used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array from which to begin retrieving samplers.</param>
			/// <param name="count">Number of samplers to retrieve.</param>
			/// <returns>An array of sampler states.</returns>
			array<SamplerState^>^ GetSamplers( int startSlot, int count );

			/// <summary>
			/// Gets the shader resources used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array from which to begin retrieving shader resources.</param>
			/// <param name="count">Number of resources to retrieve.</param>
			/// <returns>An array of shader resources.</returns>
			array<ShaderResourceView^>^ GetShaderResources( int startSlot, int count );

			/// <summary>
			/// Gets the unordered resource views used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array from which to begin retrieving views.</param>
			/// <param name="count">Number of views to retrieve.</param>
			/// <returns>An array of unordered access views.</returns>
			array<UnorderedAccessView^>^ GetUnorderedAccessViews( int startSlot, int count );

			/// <summary>
			/// Sets a single constant buffer to be used by the compute shader stage.
			/// </summary>
			/// <param name="slot">Index into the device's zero-based array to which to set the constant buffer.</param>
			void SetConstantBuffer( Buffer^ constantBuffer, int slot );

			/// <summary>
			/// Sets the constant buffers used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array to which to begin setting constant buffers.</param>
			/// <param name="count">Number of buffers from the source array to set.</param>
			void SetConstantBuffers( array<Buffer^>^ constantBuffers, int startSlot, int count );

			/// <summary>
			/// Sets a single sampler to be used by the compute shader stage.
			/// </summary>
			/// <param name="slot">Index into the device's zero-based array to which to set the sampler.</param>
			void SetSampler( SamplerState^ sampler, int slot );

			/// <summary>
			/// Sets the sampler states used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array to which to begin setting samplers.</param>
			/// <param name="count">Number of samplers from the source array to set.</param>
			void SetSamplers( array<SamplerState^>^ samplers, int startSlot, int count );

			/// <summary>
			/// Sets a single shader resource to be used by the compute shader stage.
			/// </summary>
			/// <param name="slot">Index into the device's zero-based array to which to set the resource.</param>
			void SetShaderResource( ShaderResourceView^ resourceView, int slot );

			/// <summary>
			/// Sets the shader resources used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array to which to begin setting shader resources.</param>
			/// <param name="count">Number of resources from the source array to set.</param>
			void SetShaderResources( array<ShaderResourceView^>^ resourceViews, int startSlot, int count );

			/// <summary>
			/// Sets a single unordered access view to be used by the compute shader stage.
			/// </summary>
			/// <param name="slot">Index into the device's zero-based array to which to set the view.</param>
			void SetUnorderedAccessView( UnorderedAccessView^ unorderedAccessView, int slot );

			/// <summary>
			/// Sets the unordered resource views used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array to which to begin setting views.</param>
			/// <param name="count">Number of views from the source array to set.</param>
			void SetUnorderedAccessViews( array<UnorderedAccessView^>^ unorderedAccessViews, int startSlot, int count );

			/// <summary>
			/// Sets a single unordered access view to be used by the compute shader stage.
			/// </summary>
			/// <param name="slot">Index into the device's zero-based array to which to set the view.</param>
			/// <param name="initialLength">Initial append/consume buffer offset. A value of -1 indicates the current offset should be kept. Any other values set the hidden counter for the UAV.</param>
			void SetUnorderedAccessView( UnorderedAccessView^ unorderedAccessView, int slot, int initialLength );

			/// <summary>
			/// Sets the unordered resource views used by the compute shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array to which to begin setting views.</param>
			/// <param name="count">Number of views from the source array to set.</param>
			/// <param name="initialLengths">An array of Append/Consume buffer offsets. A value of -1 indicates the current offset should be kept. Any other values set the hidden counter for that Appendable/Consumeable UAV.</param>
			void SetUnorderedAccessViews( array<UnorderedAccessView^>^ unorderedAccessViews, int startSlot, int count, array<int>^ initialLengths );
		};
	}
};