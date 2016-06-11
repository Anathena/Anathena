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
		/// <summary>
		/// Represents a scalar effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectScalarVariable</unmanaged>
		public ref class EffectScalarVariable : public EffectVariable
		{	
		private:
			ID3DX11EffectScalarVariable* m_Pointer;
			
		internal:
			EffectScalarVariable( ID3DX11EffectScalarVariable* pointer );
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectScalarVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectScalarVariable( System::IntPtr pointer );
			
			/// <summary>
			/// Sets the value of the scalar variable.
			/// </summary>
			/// <param name="value">The boolean value to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( bool value );

			/// <summary>
			/// Sets the value of the scalar variable.
			/// </summary>
			/// <param name="value">The integer value to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( int value );

			/// <summary>
			/// Sets the value of the scalar variable.
			/// </summary>
			/// <param name="value">The floating point value to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( float value );

			/// <summary>
			/// Sets the value of the scalar variable.
			/// </summary>
			/// <param name="values">The array of boolean values to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( array<bool>^ values );

			/// <summary>
			/// Sets the value of the scalar variable.
			/// </summary>
			/// <param name="values">The array of integer values to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( array<int>^ values );

			/// <summary>
			/// Sets the value of the scalar variable.
			/// </summary>
			/// <param name="values">The array of floating point values to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( array<float>^ values );
			
			/// <summary>
			/// Gets the value of the variable as a boolean.
			/// </summary>
			/// <returns>The boolean value of the scalar.</returns>
			bool GetBool();

			/// <summary>
			/// Gets the value of the variable as an integer.
			/// </summary>
			/// <returns>The integer value of the scalar.</returns>
			int GetInt();

			/// <summary>
			/// Gets the value of the variable as a floating point.
			/// </summary>
			/// <returns>The floating point value of the scalar.</returns>
			float GetFloat();

			/// <summary>
			/// Gets the value of the variable as an array of booleans.
			/// </summary>
			/// <param name="count">The number of values to get.</param>
			/// <returns>The array of boolean values of the variable.</returns>
			array<bool>^ GetBoolArray( int count );

			/// <summary>
			/// Gets the value of the variable as an array of integers.
			/// </summary>
			/// <param name="count">The number of values to get.</param>
			/// <returns>The array of integer values of the variable.</returns>
			array<int>^ GetIntArray( int count );

			/// <summary>
			/// Gets the value of the variable as an array of floating points.
			/// </summary>
			/// <param name="count">The number of values to get.</param>
			/// <returns>The array of floating point values of the variable.</returns>
			array<float>^ GetFloatArray( int count );
		};
	}
};