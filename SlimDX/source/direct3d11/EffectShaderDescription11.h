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

#include "Enums11.h"
#include "../d3dcompiler/ShaderSignatureDC.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Provides a description for an effect shader variable.
		/// </summary>
		/// <unmanaged></unmanaged>
		public value class EffectShaderDescription : System::IEquatable<EffectShaderDescription>
		{
		private:
			D3DCompiler::ShaderSignature^ m_InputSignature;
			bool m_IsInline;
			D3DCompiler::ShaderBytecode^ m_Bytecode;
			array<System::String^>^ m_SODecl;
			int m_RasterizedStream;
			int m_NumInputSignatureEntries;
			int m_NumOutputSignatureEntries;
			int m_NumPatchConstantSignatureEntries;

		internal:
			EffectShaderDescription( const D3DX11_EFFECT_SHADER_DESC& native );
		
		public:
			/// <summary>
			/// The shader input signature. Only valid on a vertex shader or geometry shader.
			/// </summary>
			property D3DCompiler::ShaderSignature^ Signature
			{
				D3DCompiler::ShaderSignature^ get();
			}
			
			/// <summary>
			/// Indicates whether the shader is defined inline.
			/// </summary>
			property bool IsInline
			{
				bool get();
			}
			
			/// <summary>
			/// The compiled shader bytecode.
			/// </summary>
			property D3DCompiler::ShaderBytecode^ Bytecode
			{
				D3DCompiler::ShaderBytecode^ get();
			}
			
			/// <summary>
			/// Stream out declarations.
			/// </summary>
			property array<System::String^>^ StreamOutputDeclaration
			{
				array<System::String^>^ get();
			}
			
			/// <summary>
			/// Indicates which stream is rasterized. D3D11 geometry shaders can output up to four streams of data, one of which can be rasterized.
			/// </summary>
			property int RasterizedStream
			{
				int get();
			}

			/// <summary>
			/// Number of entries in the input signature.
			/// </summary>
			property int InputParameterCount
			{
				int get();
			}
			
			/// <summary>
			/// Number of entries in the output signature.
			/// </summary>
			property int OutputParameterCount
			{
				int get();
			}

			/// <summary>
			/// Number of entries in the patch constant signature.
			/// </summary>
			property int PatchConstantCount
			{
				int get();
			}

			/// <summary>
			/// Tests for equality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator == ( EffectShaderDescription left, EffectShaderDescription right );

			/// <summary>
			/// Tests for inequality between two objects.
			/// </summary>
			/// <param name="left">The first value to compare.</param>
			/// <param name="right">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
			static bool operator != ( EffectShaderDescription left, EffectShaderDescription right );

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
			virtual bool Equals( EffectShaderDescription other );

			/// <summary>
			/// Determines whether the specified object instances are considered equal. 
			/// </summary>
			/// <param name="value1">The first value to compare.</param>
			/// <param name="value2">The second value to compare.</param>
			/// <returns><c>true</c> if <paramref name="value1"/> is the same instance as <paramref name="value2"/> or 
			/// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
			static bool Equals( EffectShaderDescription% value1, EffectShaderDescription% value2 );
		};
	}
};