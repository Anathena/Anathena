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
		ref class PixelShader;
		ref class Buffer;
		ref class ShaderResourceView;
		ref class SamplerState;
		ref class ClassInstance;

		/// <summary>
		/// Defines a wrapper for pixel shader related commands on the device.
		/// </summary>
		public ref class PixelShaderWrapper
		{
		private:
			ID3D11DeviceContext* deviceContext;
			
		internal:
			PixelShaderWrapper( ID3D11DeviceContext* device );

		public:
			/// <summary>
			/// Assigns a pixel shader to the device.
			/// </summary>
			/// <param name="shader">The shader to assign to the device. Assign null to disable the pixel shader.</param>
			void Set( PixelShader^ shader );

			/// <summary>
			/// Assigns a pixel shader to the device.
			/// </summary>
			/// <param name="shader">The shader to assign to the device. Assign <c>null</c> to disable the pixel shader.</param>
			/// <param name="classInstances">An array of class-instance interfaces. Each interface used by a shader must have a corresponding class instance or the shader will get disabled.</param>
			void Set( PixelShader^ shader, array<ClassInstance^>^ classInstances );

			/// <summary>
			/// Gets the pixel shader currently assigned to the device.
			/// </summary>
			/// <returns>The pixel shader (null if no shader is assigned).</returns>
			PixelShader^ Get();

			/// <summary>
			/// Gets the pixel shader currently assigned to the device.
			/// </summary>
			/// <param name="count">The number of class instances to retrieve.</param>
			/// <param name="classInstances">An array that will be used to contain any class instances currently active.</param>
			/// <returns>The pixel shader (null if no shader is assigned).</returns>
			PixelShader^ Get( int count, [Out] array<ClassInstance^>^ %classInstances );

			/// <summary>
			/// Gets the constant buffers used by the pixel shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array from which to begin retrieving constant buffers.</param>
			/// <param name="count">Number of buffers to retrieve.</param>
			/// <returns>An array of constant buffers.</returns>
			array<Buffer^>^ GetConstantBuffers( int startSlot, int count );

			/// <summary>
			/// Gets the sampler states used by the pixel shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array from which to begin retrieving samplers.</param>
			/// <param name="count">Number of samplers to retrieve.</param>
			/// <returns>An array of sampler states.</returns>
			array<SamplerState^>^ GetSamplers( int startSlot, int count );

			/// <summary>
			/// Gets the shader resources used by the pixel shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array from which to begin retrieving shader resources.</param>
			/// <param name="count">Number of resources to retrieve.</param>
			/// <returns>An array of shader resources.</returns>
			array<ShaderResourceView^>^ GetShaderResources( int startSlot, int count );

			/// <summary>
			/// Sets a single constant buffer to be used by the pixel shader stage.
			/// </summary>
			/// <param name="slot">Index into the device's zero-based array to which to set the constant buffer.</param>
			void SetConstantBuffer( Buffer^ constantBuffer, int slot );

			/// <summary>
			/// Sets the constant buffers used by the pixel shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array to which to begin setting constant buffers.</param>
			/// <param name="count">Number of buffers from the source array to set.</param>
			void SetConstantBuffers( array<Buffer^>^ constantBuffers, int startSlot, int count );

			/// <summary>
			/// Sets a single sampler to be used by the pixel shader stage.
			/// </summary>
			/// <param name="slot">Index into the device's zero-based array to which to set the sampler.</param>
			void SetSampler( SamplerState^ sampler, int slot );

			/// <summary>
			/// Sets the sampler states used by the pixel shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array to which to begin setting samplers.</param>
			/// <param name="count">Number of samplers from the source array to set.</param>
			void SetSamplers( array<SamplerState^>^ samplers, int startSlot, int count );

			/// <summary>
			/// Sets a single shader resource to be used by the pixel shader stage.
			/// </summary>
			/// <param name="slot">Index into the device's zero-based array to which to set the resource.</param>
			void SetShaderResource( ShaderResourceView^ resourceView, int slot );

			/// <summary>
			/// Sets the shader resources used by the pixel shader stage.
			/// </summary>
			/// <param name="startSlot">Index into the device's zero-based array to which to begin setting shader resources.</param>
			/// <param name="count">Number of resources from the source array to set.</param>
			void SetShaderResources( array<ShaderResourceView^>^ resourceViews, int startSlot, int count );
		};
	}
};