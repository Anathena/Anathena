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
	namespace Direct3D10
	{
		ref class VertexShader;
		ref class Buffer;
		ref class ShaderResourceView;
		ref class SamplerState;

		public ref class VertexShaderWrapper
		{
		private:
			ID3D10Device* m_Device;
			
		internal:
			VertexShaderWrapper( ID3D10Device* device );

		public:
			/// <summary>
			/// Assigns a vertex shader to the device.
			/// </summary>
			/// <param name="shader">The shader to assign to the device. Assign null to disable the vertex shader.</param>
			void Set( VertexShader^ shader );

			/// <summary>
			/// Gets the vertex shader assigned to the device.
			/// </summary>
			/// <returns>The vertex shader (null if no shader is assigned).</returns>
			VertexShader^ Get();

			array<Buffer^>^ GetConstantBuffers( int startSlot, int count );
			array<SamplerState^>^ GetSamplers( int startSlot, int count );
			array<ShaderResourceView^>^ GetShaderResources( int startSlot, int count );

			void SetConstantBuffer( Buffer^ constantBuffer, int slot );
			void SetConstantBuffers( array<Buffer^>^ constantBuffers, int startSlot, int count );

			void SetSampler( SamplerState^ sampler, int slot );
			void SetSamplers( array<SamplerState^>^ samplers, int startSlot, int count );

			void SetShaderResource( ShaderResourceView^ resourceView, int slot );
			void SetShaderResources( array<ShaderResourceView^>^ resourceViews, int startSlot, int count );
		};
	}
};