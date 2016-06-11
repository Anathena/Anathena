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

namespace SlimDX
{
	namespace D3DCompiler
	{
		value class ConstantBufferDescription;
		ref class ShaderReflectionVariable;

		/// <summary>
		/// Defines an interface that provides access to a shader constant buffer.
		/// </summary>
		/// <unmanaged>ID3D11ShaderReflectionConstantBuffer</unmanaged>
		public ref class ConstantBuffer
		{
		private:
			ID3D11ShaderReflectionConstantBuffer* m_Pointer;
			
		internal:
			ConstantBuffer( ID3D11ShaderReflectionConstantBuffer* pointer );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="ConstantBuffer"/> class.
			/// </summary>
			//// <param name="pointer">The unmanaged ID3D11ShaderReflectionConstantBuffer pointer.</param>
			ConstantBuffer( System::IntPtr pointer );
			
			/// <summary>
			/// Gets the constant buffer's description.
			/// </summary>
			property ConstantBufferDescription Description
			{
				ConstantBufferDescription get();
			}

			/// <summary>
			/// Gets the shader reflection variable at the given index.
			/// </summary>
			/// <param name="index">The zero-based index of the shader variable to retrieve.</param>
			/// <returns>The shader reflection variable at the given index.</returns>
			ShaderReflectionVariable^ GetVariable( int index );

			/// <summary>
			/// Gets the shader reflection variable with the given name.
			/// </summary>
			/// <param name="name">The name of the shader variable to retrieve.</param>
			/// <returns>The shader reflection variable with the given name.</returns>
			ShaderReflectionVariable^ GetVariable( System::String^ name );
		};
	}
}