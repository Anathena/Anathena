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

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Buffer;
		ref class ShaderResourceView;
		
		/// <summary>
		/// Represents a constant buffer or texture buffer inside an effect.
		/// </summary>
		/// <unmanaged>ID3DX11EffectConstantBuffer</unmanaged>
		public ref class EffectConstantBuffer : public EffectVariable
		{	
		private:
			ID3DX11EffectConstantBuffer* m_Pointer;
			
		internal:
			EffectConstantBuffer( ID3DX11EffectConstantBuffer* pointer );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectConstantBuffer"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectConstantBuffer( System::IntPtr pointer );

			/// <summary>
			/// Gets or sets the constant buffer represented by this interface.
			/// </summary>
			property Buffer^ ConstantBuffer
			{
				Buffer^ get();
				void set(Buffer^ value);
			}

			/// <summary>
			/// Gets or sets the texture buffer represented by this interface.
			/// </summary>
			property ShaderResourceView^ TextureBuffer
			{
				ShaderResourceView^ get();
				void set(ShaderResourceView^ value);
			}

			/// <summary>
			/// Reverts a previously set constant buffer.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result UndoSetConstantBuffer();

			/// <summary>
			/// Reverts a previously set texture buffer.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result UndoSetTextureBuffer();
		};
	}
};