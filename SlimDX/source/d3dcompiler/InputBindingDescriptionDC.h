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

#include "EnumsDC.h"

namespace SlimDX
{
	namespace D3DCompiler
	{
		/// <summary>
		/// Describes how a shader resource is bound to a shader input.
		/// </summary>
		/// <unmanaged>D3D11_SHADER_INPUT_BIND_DESC</unmanaged>
		public value class InputBindingDescription
		{
		internal:
			InputBindingDescription( const D3D11_SHADER_INPUT_BIND_DESC &desc );

		public:
			/// <summary>
			/// Name of the shader resource.
			/// </summary>
			property System::String^ Name;

			/// <summary>
			/// Identifies the type of data in the resource.
			/// </summary>
			property ShaderInputType Type;

			/// <summary>
			/// Starting bind point.
			/// </summary>
			property int BindPoint;

			/// <summary>
			/// Number of contiguous bind points for arrays.
			/// </summary>
			property int BindCount;

			/// <summary>
			/// Shader input-parameter options.
			/// </summary>
			property ShaderInputFlags Flags;

			/// <summary>
			/// If the input is a texture, the return type.
			/// </summary>
			property ResourceReturnType ReturnType;

			/// <summary>
			/// Identifies the amount of data in the resource.
			/// </summary>
			property Direct3D11::ShaderResourceViewDimension Dimension;

			/// <summary>
			/// The number of samples for a multisampled texture; otherwise 0.
			/// </summary>
			property int SampleCount;
		};
	}
}