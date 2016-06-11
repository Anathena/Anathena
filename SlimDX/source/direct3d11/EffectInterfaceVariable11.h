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
#include "EffectClassInstanceVariable11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a class instance effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectInterfaceVariable</unmanaged>
		public ref class EffectInterfaceVariable : public EffectVariable
		{	
		private:
			ID3DX11EffectInterfaceVariable* m_Pointer;
			
		internal:
			EffectInterfaceVariable( ID3DX11EffectInterfaceVariable* pointer );
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectInterfaceVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectInterfaceVariable( System::IntPtr pointer );

			/// <summary>
			/// Gets or sets the class instance for the variable.
			/// </summary>
			property EffectClassInstanceVariable^ ClassInstance
			{
				EffectClassInstanceVariable^ get();
				void set(EffectClassInstanceVariable^ value);
			}
		};
	}
};