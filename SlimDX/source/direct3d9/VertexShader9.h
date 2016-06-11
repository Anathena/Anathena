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

#include "ShaderBytecode.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>
		/// Represents a vertex shader.
		/// </summary>
		/// <unmanaged>IDirect3DVertexShader9</unmanaged>
		public ref class VertexShader : public ComObject
		{
			COMOBJECT(IDirect3DVertexShader9, VertexShader);

			ShaderBytecode^ m_function;

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="VertexShader"/> class.
			/// </summary>
			/// <param name="device">The device used to create the shader.</param>
			/// <param name="function">The shader function stream.</param>
			VertexShader( Device^ device, ShaderBytecode^ function );

			/// <summary>
			/// Releases all resources used by the <see cref="VertexShader"/>.
			/// </summary>
			virtual ~VertexShader() { delete m_function; }
			
			/// <summary>
			/// The maximum level of nesting of dynamic flow control instructions.
			/// </summary>
			literal int MaxDynamicFlowControlDepth = D3DVS20_MAX_DYNAMICFLOWCONTROLDEPTH;

			/// <summary>
			/// The minimum level of nesting of dynamic flow control instructions.
			/// </summary>
			literal int MinDynamicFlowControlDepth = D3DVS20_MIN_DYNAMICFLOWCONTROLDEPTH;

			/// <summary>
			/// The maximum number of temporary registers supported.
			/// </summary>
			literal int MaxTemps = D3DVS20_MAX_NUMTEMPS;

			/// <summary>
			/// The minimum number of temporary registers supported.
			/// </summary>
			literal int MinTemps = D3DVS20_MIN_NUMTEMPS;

			/// <summary>
			/// The maximum level of nesting of static flow control instructions.
			/// </summary>
			literal int MaxStaticFlowControlDepth = D3DVS20_MAX_STATICFLOWCONTROLDEPTH;

			/// <summary>
			/// The maximum level of nesting of static flow control instructions.
			/// </summary>
			literal int MinStaticFlowControlDepth = D3DVS20_MIN_STATICFLOWCONTROLDEPTH;

			/// <summary>
			/// Gets the device associated with the shader.
			/// </summary>
			property SlimDX::Direct3D9::Device^ Device
			{
				SlimDX::Direct3D9::Device^ get();
			}

			/// <summary>
			/// Gets the shader function stream.
			/// </summary>
			property ShaderBytecode^ Function
			{
				ShaderBytecode^ get();
			}
		};
	}
}
