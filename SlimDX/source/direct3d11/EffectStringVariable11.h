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

#include "../ComObject.h"

#include "EffectVariable11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a string effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectStringVariable</unmanaged>
		public ref class EffectStringVariable : public EffectVariable
		{	
		private:
			ID3DX11EffectStringVariable* m_Pointer;
			
		internal:
			EffectStringVariable( ID3DX11EffectStringVariable* pointer );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectStringVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectStringVariable( System::IntPtr pointer );
			
			/// <summary>
			/// Gets the value of the variable.
			/// </summary>
			/// <returns>A string representing the value of the variable.</returns>
			System::String^ GetString();

			/// <summary>
			/// Gets an array of string values from the variable.
			/// </summary>
			/// <param name="values">An array of strings to hold the retrieved values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetStringArray(array<System::String^>^ values);

			/// <summary>
			/// Gets an array of string values from the variable.
			/// </summary>
			/// <param name="values">An array of strings to hold the retrieved values.</param>
			/// <param name="offset">The offset into the array to begin storing values.</param>
			/// <param name="count">The number of values to get.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetStringArray(array<System::String^>^ values, int offset, int count);
		};
	}
};