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
#include "DepthStencilView11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a depth-stencil view effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectDepthStencilViewVariable</unmanaged>
		public ref class EffectDepthStencilViewVariable : public EffectVariable
		{
		private:
			ID3DX11EffectDepthStencilViewVariable* m_Pointer;
			
		internal:
			EffectDepthStencilViewVariable( ID3DX11EffectDepthStencilViewVariable* pointer );
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectDepthStencilViewVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectDepthStencilViewVariable( System::IntPtr pointer );

			/// <summary>
			/// Gets the value of the variable.
			/// </summary>
			/// <returns>The view of the depth-stencil variable.</returns>
			DepthStencilView^ GetView();

			/// <summary>
			/// Gets an array of views of depth-stencil surfaces from the variable.
			/// </summary>
			/// <param name="views">An array of views to hold the retrieved values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetViewArray(array<DepthStencilView^>^ views);

			/// <summary>
			/// Gets an array of views of depth-stencil surfaces from the variable.
			/// </summary>
			/// <param name="views">An array of views to hold the retrieved values.</param>
			/// <param name="offset">The offset into the array to begin storing values.</param>
			/// <param name="count">The number of values to get.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetViewArray(array<DepthStencilView^>^ views, int offset, int count);
			
			/// <summary>
			/// Sets the value of the variable.
			/// </summary>
			/// <param name="view">The view of the depth-stencil surface to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetView( DepthStencilView^ view );

			/// <summary>
			/// Sets the value of the variable.
			/// </summary>
			/// <param name="views">An array of views to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetViewArray(array<DepthStencilView^>^ views);

			/// <summary>
			/// Sets the value of the variable.
			/// </summary>
			/// <param name="views">An array of views to set.</param>
			/// <param name="offset">The offset into the array to begin setting values.</param>
			/// <param name="count">The number of values to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetViewArray(array<DepthStencilView^>^ views, int offset, int count);
		};
	}
};