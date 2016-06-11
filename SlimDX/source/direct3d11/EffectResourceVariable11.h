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
#include "ShaderResourceView11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a shader resource effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectShaderResourceVariable</unmanaged>
		public ref class EffectResourceVariable : public EffectVariable
		{	
		private:
			ID3DX11EffectShaderResourceVariable* m_Pointer;
			
		internal:
			EffectResourceVariable( ID3DX11EffectShaderResourceVariable* pointer );
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectResourceVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectResourceVariable( System::IntPtr pointer );
			
			/// <summary>
			/// Gets the value of the variable.
			/// </summary>
			/// <returns>A view of the shader resource.</returns>
			ShaderResourceView^ GetResource();

			/// <summary>
			/// Gets an array of resource views from the variable.
			/// </summary>
			/// <param name="views">An array of resource views to hold the retrieved shader resources.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetResourceArray(array<ShaderResourceView^>^ views);

			/// <summary>
			/// Gets an array of resource views from the variable.
			/// </summary>
			/// <param name="views">An array of resource views to hold the retrieved shader resources.</param>
			/// <param name="offset">The offset into the array to begin storing values.</param>
			/// <param name="count">The number of values to get.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetResourceArray(array<ShaderResourceView^>^ views, int offset, int count);

			/// <summary>
			/// Sets the value of the shader resource.
			/// </summary>
			/// <param name="view">A view of the resource to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetResource( ShaderResourceView^ view );

			/// <summary>
			/// Sets an array of shader resource values.
			/// </summary>
			/// <param name="views">An array of resource views to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetResourceArray( array<ShaderResourceView^>^ views );

			/// <summary>
			/// Sets an array of shader resource values.
			/// </summary>
			/// <param name="views">An array of resource views to set.</param>
			/// <param name="offset">The offset into the array to begin setting values.</param>
			/// <param name="count">The number of values to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetResourceArray( array<ShaderResourceView^>^ views, int offset, int count );
		};
	}
};