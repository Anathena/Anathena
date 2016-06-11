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
#include "BlendState11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a blend state effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectBlendVariable</unmanaged>
		public ref class EffectBlendVariable : public EffectVariable
		{	
		private:
			ID3DX11EffectBlendVariable* m_Pointer;
			
		internal:
			EffectBlendVariable( ID3DX11EffectBlendVariable* pointer );
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectBlendVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectBlendVariable( System::IntPtr pointer );

			/// <summary>
			/// Gets the blend state from the variable.
			/// </summary>
			/// <param name="index">Index into an array of blend-state interfaces. If there is only one blend-state interface, use 0.</param>
			/// <returns>The retrieved blend state.</returns>
			BlendState^ GetBlendState(int index);

			/// <summary>
			/// Sets the blend state for the variable.
			/// </summary>
			/// <param name="index">Index into an array of blend-state interfaces. If there is only one blend-state interface, use 0.</param>
			/// <param name="blendState">The blend state to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetBlendState(int index, BlendState^ blendState);

			/// <summary>
			/// Gets the saved backing store description.
			/// </summary>
			/// <param name="index">Index into an array of blend-state interfaces. If there is only one blend-state interface, use 0.</param>
			/// <returns>The saved backing store description.</returns>
			BlendStateDescription GetBackingStore(int index);

			/// <summary>
			/// Reverts a previously set blend state.
			/// </summary>
			/// <param name="index">Index into an array of blend-state interfaces. If there is only one blend-state interface, use 0.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result UndoSetBlendState(int index);
		};
	}
};