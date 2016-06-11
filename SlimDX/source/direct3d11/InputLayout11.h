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

#include "../d3dcompiler/ShaderSignatureDC.h"

#include "DeviceChild11.h"
#include "InputElement11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		ref class ShaderSignature;
		ref class ShaderBytecode;
		
		/// <summary>Accesses the input data for the input-assembler stage.</summary>
		/// <unmanaged>ID3D11InputLayout</unmanaged>
		public ref class InputLayout : public DeviceChild
		{
			COMOBJECT(ID3D11InputLayout, InputLayout);

		private:
			void Init( SlimDX::Direct3D11::Device^ device, const void *shader, int length, array<InputElement>^ elements );
		
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="InputLayout" /> object to describe the
			/// input-buffer data for the input-assembler stage.
			/// </summary>
			/// <unmanaged>ID3D11Device::CreateInputLayout</unmanaged>
			/// <param name="device">The device used to create the layout.</param>
			/// <param name="elements">An array of input elements describing the layout of the input data.</param>
			/// <param name="shaderSignature">The shader signature used to validate the input elements.</param>
			[System::Obsolete("Use the constructor overload taking a ShaderSignature as the second argument instead.")]
			InputLayout( SlimDX::Direct3D11::Device^ device, array<InputElement>^ elements, D3DCompiler::ShaderSignature^ shaderSignature );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="InputLayout" /> object to describe the
			/// input-buffer data for the input-assembler stage.
			/// </summary>
			/// <unmanaged>ID3D11Device::CreateInputLayout</unmanaged>
			/// <param name="device">The device used to create the layout.</param>
			/// <param name="elements">An array of input elements describing the layout of the input data.</param>
			/// <param name="shaderSignature">The shader signature used to validate the input elements.</param>
			InputLayout( SlimDX::Direct3D11::Device^ device, D3DCompiler::ShaderSignature^ shaderSignature, array<InputElement>^ elements );

			/// <summary>
			/// Initializes a new instance of the <see cref="InputLayout" /> object to describe the
			/// input-buffer data for the input-assembler stage.
			/// </summary>
			/// <unmanaged>ID3D11Device::CreateInputLayout</unmanaged>
			/// <param name="device">The device used to create the layout.</param>
			/// <param name="elements">An array of input elements describing the layout of the input data.</param>
			/// <param name="shaderBytecode">The compiled shader used to validate the input elements.</param>
			InputLayout( SlimDX::Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode, array<InputElement>^ elements );
		};
	}
};