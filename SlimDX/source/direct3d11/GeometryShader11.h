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
#include "StreamOutputElement11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class ClassLinkage;

		/// <summary>
		/// Represents a geometry shader.
		/// </summary>
		/// <unmanaged>ID3D11GeometryShader</unmanaged>
		public ref class GeometryShader : public DeviceChild
		{
			COMOBJECT(ID3D11GeometryShader, GeometryShader);

		public:
			static initonly int NoRasterizedStream = D3D11_SO_NO_RASTERIZED_STREAM;

			/// <summary>
			/// Initializes a new instance of the <see cref="GeometryShader"/> class.
			/// </summary>
			/// <param name="device">The device used to create the shader.</param>
			/// <param name="shaderBytecode">The compiled shader bytecode.</param>
			GeometryShader( Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode );

			/// <summary>
			/// Initializes a new instance of the <see cref="GeometryShader"/> class.
			/// </summary>
			/// <param name="device">The device used to create the shader.</param>
			/// <param name="shaderBytecode">The compiled shader bytecode.</param>
			/// <param name="linkage">A dynamic class linkage interface.</param>
			GeometryShader( Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode, ClassLinkage^ linkage );

			/// <summary>
			/// Initializes a new instance of the <see cref="GeometryShader"/> class.
			/// </summary>
			/// <param name="device">The device used to create the shader.</param>
			/// <param name="shaderBytecode">The compiled shader bytecode.</param>
			/// <param name="elements">An array of <see cref="StreamOutputElement"/> instances describing the layout of the output buffers.</param>
			/// <param name="bufferedStrides">An array of buffer strides; each stride is the size of an element for that buffer.</param>
			/// <param name="rasterizedStream">The index number of the stream to be sent to the rasterizer stage. Set to NoRasterizedStream if no stream is to be rasterized.</param>
			GeometryShader( Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode, array<StreamOutputElement>^ elements, array<int>^ bufferedStrides, int rasterizedStream );

			/// <summary>
			/// Initializes a new instance of the <see cref="GeometryShader"/> class.
			/// </summary>
			/// <param name="device">The device used to create the shader.</param>
			/// <param name="shaderBytecode">The compiled shader bytecode.</param>
			/// <param name="elements">An array of <see cref="StreamOutputElement"/> instances describing the layout of the output buffers.</param>
			/// <param name="bufferedStrides">An array of buffer strides; each stride is the size of an element for that buffer.</param>
			/// <param name="rasterizedStream">The index number of the stream to be sent to the rasterizer stage. Set to NoRasterizedStream if no stream is to be rasterized.</param>
			/// <param name="linkage">A dynamic class linkage interface.</param>
			GeometryShader( Direct3D11::Device^ device, D3DCompiler::ShaderBytecode^ shaderBytecode, array<StreamOutputElement>^ elements, array<int>^ bufferedStrides, int rasterizedStream, ClassLinkage^ linkage );
		};
	}
};