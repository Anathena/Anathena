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
#include "ClassInstance11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a class instance effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectClassInstanceVariable</unmanaged>
		public ref class EffectClassInstanceVariable : public EffectVariable
		{	
		internal:
			ID3DX11EffectClassInstanceVariable* m_Pointer;

			EffectClassInstanceVariable( ID3DX11EffectClassInstanceVariable* pointer );
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectClassInstanceVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectClassInstanceVariable( System::IntPtr pointer );

			/// <summary>
			/// Gets the class instance from the variable.
			/// </summary>
			property ClassInstance^ ClassInstance
			{
				SlimDX::Direct3D11::ClassInstance^ get();
			}
		};
	}
};