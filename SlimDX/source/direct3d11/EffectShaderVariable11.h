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

#include "EffectVariable11.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace D3DCompiler
	{
		value class ShaderParameterDescription;
	}

	namespace Direct3D11
	{
		ref class PixelShader;
		ref class VertexShader;
		ref class GeometryShader;
		ref class DomainShader;
		ref class HullShader;
		ref class ComputeShader;

		value class EffectShaderDescription;
		
		/// <summary>
		/// Represents a shader effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectShaderVariable</unmanaged>
		public ref class EffectShaderVariable : public EffectVariable
		{	
		private:
			ID3DX11EffectShaderVariable* m_Pointer;
			
		internal:
			EffectShaderVariable( ID3DX11EffectShaderVariable* pointer );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectShaderVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectShaderVariable( System::IntPtr pointer );
			
			/// <summary>
			/// Gets a pixel shader.
			/// </summary>
			/// <param name="index">The zero-based index of the pixel shader to retrieve.</param>
			/// <returns>The pixel shader at the specified index.</returns>
			PixelShader^ GetPixelShader( int index );

			/// <summary>
			/// Gets a vertex shader.
			/// </summary>
			/// <param name="index">The zero-based index of the vertex shader to retrieve.</param>
			/// <returns>The vertex shader at the specified index.</returns>
			VertexShader^ GetVertexShader( int index );

			/// <summary>
			/// Gets a geometry shader.
			/// </summary>
			/// <param name="index">The zero-based index of the geometry shader to retrieve.</param>
			/// <returns>The geometry shader at the specified index.</returns>
			GeometryShader^ GetGeometryShader( int index );

			/// <summary>
			/// Gets a hull shader.
			/// </summary>
			/// <param name="index">The zero-based index of the hull shader to retrieve.</param>
			/// <returns>The hull shader at the specified index.</returns>
			HullShader^ GetHullShader( int index );

			/// <summary>
			/// Gets a domain shader.
			/// </summary>
			/// <param name="index">The zero-based index of the domain shader to retrieve.</param>
			/// <returns>The domain shader at the specified index.</returns>
			DomainShader^ GetDomainShader( int index );

			/// <summary>
			/// Gets a compute shader.
			/// </summary>
			/// <param name="index">The zero-based index of the compute shader to retrieve.</param>
			/// <returns>The compute shader at the specified index.</returns>
			ComputeShader^ GetComputeShader( int index );
			
			/// <summary>
			/// Gets the description for a single element of the given shader's input signature.
			/// </summary>
			/// <param name="shaderIndex">The zero-based shader index.</param>
			/// <param name="parameterIndex">The index of the signature element.</param>
			/// <returns>The description for the given shader signature element.</returns>
			D3DCompiler::ShaderParameterDescription GetInputParameterDescription( int shaderIndex, int parameterIndex );

			/// <summary>
			/// Gets the description for a single element of the given shader's output signature.
			/// </summary>
			/// <param name="shaderIndex">The zero-based shader index.</param>
			/// <param name="parameterIndex">The index of the signature element.</param>
			/// <returns>The description for the given shader signature element.</returns>
			D3DCompiler::ShaderParameterDescription GetOutputParameterDescription( int shaderIndex, int parameterIndex );

			/// <summary>
			/// Gets the description for a single element of the given shader's patch constant signature.
			/// </summary>
			/// <param name="shaderIndex">The zero-based shader index.</param>
			/// <param name="parameterIndex">The index of the signature element.</param>
			/// <returns>The description for the given shader signature element.</returns>
			D3DCompiler::ShaderParameterDescription GetPatchConstantDescription( int shaderIndex, int parameterIndex );
			
			/// <summary>
			/// Gets the description for the given shader.
			/// </summary>
			/// <param name="shaderIndex">The zero-based shader index.</param>
			/// <returns>The description for the given shader.</returns>
			EffectShaderDescription GetShaderDescription( int shaderIndex );
		};
	}
};