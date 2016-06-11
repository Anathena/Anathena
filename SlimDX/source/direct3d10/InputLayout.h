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

#include "../d3dcompiler/ShaderSignatureDC.h"
#include "../d3dcompiler/ShaderBytecodeDC.h"

#include "DeviceChild10.h"
#include "InputElement.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Device;
		ref class ShaderSignature;
		ref class ShaderBytecode;
		
		public ref class InputLayout : public DeviceChild
		{
			COMOBJECT(ID3D10InputLayout, InputLayout);

		private:
			void Init( SlimDX::Direct3D10::Device^ device, const void *shader, int length, array<InputElement>^ elements );
		
		public:
			[System::Obsolete("Use the constructor overload taking a ShaderSignature as the second argument instead.")]
			InputLayout( SlimDX::Direct3D10::Device^ device, array<InputElement>^ elements, D3DCompiler::ShaderSignature^ shaderSignature );

			/// <summary>
			/// Initializes a new instance of the <see cref="InputLayout" /> object to describe the
			/// input-buffer data for the input-assembler stage.
			/// </summary>
			/// <unmanaged>ID3D10Device::CreateInputLayout</unmanaged>
			InputLayout( SlimDX::Direct3D10::Device^ device, D3DCompiler::ShaderSignature^ shaderSignature, array<InputElement>^ elements );

			InputLayout( SlimDX::Direct3D10::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode, array<InputElement>^ elements );
		};
	}
};