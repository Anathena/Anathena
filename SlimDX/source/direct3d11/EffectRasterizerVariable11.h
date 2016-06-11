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
#include "RasterizerState11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a rasterizer state effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectRasterizerVariable</unmanaged>
		public ref class EffectRasterizerVariable : public EffectVariable
		{	
		private:
			ID3DX11EffectRasterizerVariable* m_Pointer;
			
		internal:
			EffectRasterizerVariable( ID3DX11EffectRasterizerVariable* pointer );
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectRasterizerVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectRasterizerVariable( System::IntPtr pointer );

			/// <summary>
			/// Gets the rasterizer state from the variable.
			/// </summary>
			/// <param name="index">Index into an array of rasterizer-state interfaces. If there is only one rasterizer-state interface, use 0.</param>
			/// <returns>The retrieved rasterizer state.</returns>
			RasterizerState^ GetRasterizerState(int index);

			/// <summary>
			/// Sets the rasterizer state for the variable.
			/// </summary>
			/// <param name="index">Index into an array of rasterizer-state interfaces. If there is only one rasterizer-state interface, use 0.</param>
			/// <param name="rasterizerState">The rasterizer state to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetRasterizerState(int index, RasterizerState^ rasterizerState);

			/// <summary>
			/// Gets the saved backing store description.
			/// </summary>
			/// <param name="index">Index into an array of rasterizer-state interfaces. If there is only one rasterizer-state interface, use 0.</param>
			/// <returns>The saved backing store description.</returns>
			RasterizerStateDescription GetBackingStore(int index);

			/// <summary>
			/// Reverts a previously set rasterizer state.
			/// </summary>
			/// <param name="index">Index into an array of rasterizer-state interfaces. If there is only one rasterizer-state interface, use 0.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result UndoSetRasterizerState(int index);
		};
	}
};