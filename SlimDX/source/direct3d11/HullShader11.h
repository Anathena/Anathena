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

#include "DeviceChild11.h"
#include "../d3dcompiler/ShaderBytecodeDC.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class ClassLinkage;

		/// <summary>
		/// Represents a hull shader.
		/// </summary>
		/// <unmanaged>ID3D11HullShader</unmanaged>
		public ref class HullShader : public DeviceChild
		{
			COMOBJECT(ID3D11HullShader, HullShader);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="HullShader"/> class.
			/// </summary>
			/// <param name="device">The device used to create the shader.</param>
			/// <param name="shaderBytecode">The compiled shader bytecode.</param>
			HullShader( Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode );

			/// <summary>
			/// Initializes a new instance of the <see cref="HullShader"/> class.
			/// </summary>
			/// <param name="device">The device used to create the shader.</param>
			/// <param name="shaderBytecode">The compiled shader bytecode.</param>
			/// <param name="linkage">A dynamic class linkage interface.</param>
			HullShader( Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode, ClassLinkage^ linkage );
		};
	}
};