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

#include "ShaderBytecodeDC.h"

namespace SlimDX
{
	namespace D3DCompiler
	{
		/// <summary>
		/// Represents a shader signature.
		/// </summary>
		public ref class ShaderSignature
		{
		private:
			DataStream^ data;
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="ShaderSignature"/> class.
			/// </summary>
			/// <param name="data">A <see cref="DataStream"/> containing the raw bytes of the shader signature.</param>
			ShaderSignature( DataStream^ data );

			/// <summary>
			/// Releases all resources used by the <see cref="ShaderSignature"/>.
			/// </summary>
			~ShaderSignature();

			/// <summary>
			/// Extracts the input signature from a compiled shader or effect.
			/// </summary>
			/// <param name="shaderBytecode">The bytecode of the compiled shader or effect.</param>
			/// <returns>The input signature of the shader or effect.</returns>
			static ShaderSignature^ GetInputSignature( ShaderBytecode^ shaderBytecode );

			/// <summary>
			/// Extracts the output signature from a compiled shader or effect.
			/// </summary>
			/// <param name="shaderBytecode">The bytecode of the compiled shader or effect.</param>
			/// <returns>The output signature of the shader or effect.</returns>
			static ShaderSignature^ GetOutputSignature( ShaderBytecode^ shaderBytecode );

			/// <summary>
			/// Extracts the input and output signatures from a compiled shader or effect.
			/// </summary>
			/// <param name="shaderBytecode">The bytecode of the compiled shader or effect.</param>
			/// <returns>The input and output signatures of the shader or effect.</returns>
			static ShaderSignature^ GetInputOutputSignature( ShaderBytecode^ shaderBytecode );

			/// <summary>
			/// Gets the raw data of the shader signature.
			/// </summary>
			property DataStream^ Data
			{
				DataStream^ get() { return data; }
			}

			/// <summary>
			/// Returns the hash code for this instance.
			/// </summary>
			/// <returns>A 32-bit signed integer hash code.</returns>
			virtual int GetHashCode() override;
		};
	}
};