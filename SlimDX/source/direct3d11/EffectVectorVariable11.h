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

#include "../math/Vector2.h"
#include "../math/Vector3.h"
#include "../math/Vector4.h"
#include "../math/Color4.h"

#include "EffectVariable11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a vector effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectVectorVariable</unmanaged>
		public ref class EffectVectorVariable : public EffectVariable
		{
		private:
			ID3DX11EffectVectorVariable* m_Pointer;
			
		internal:
			EffectVectorVariable( ID3DX11EffectVectorVariable* pointer );
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectVectorVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectVectorVariable( System::IntPtr pointer );
			
			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="value">The floating point vector value to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( Vector2 value );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="value">The floating point vector value to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( Vector3 value );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="value">The floating point vector value to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( Vector4 value );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="x">The X-component of the vector.</param>
			/// <param name="y">The Y-component of the vector.</param>
			/// <param name="z">The Z-component of the vector.</param>
			/// <param name="w">The W-component of the vector.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( int x, int y, int z, int w );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="x">The X-component of the vector.</param>
			/// <param name="y">The Y-component of the vector.</param>
			/// <param name="z">The Z-component of the vector.</param>
			/// <param name="w">The W-component of the vector.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( bool x, bool y, bool z, bool w );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="value">The color value to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( Color4 value );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="value">The array of floating point vectors to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( array<Vector4>^ value );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="value">The array of color values to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( array<Color4>^ value );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="value">The array of four-element integer vectors to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( array<int>^ value );

			/// <summary>
			/// Sets the value of the vector variable.
			/// </summary>
			/// <param name="value">The array of four-element boolean vectors to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Set( array<bool>^ value );

			/// <summary>
			/// Gets the value of the variable as a floating point vector.
			/// </summary>
			/// <returns>The floating point vector value of the variable.</returns>
			Vector4 GetVector();

			/// <summary>
			/// Gets the value of the variable as a color value.
			/// </summary>
			/// <returns>The color value of the variable.</returns>
			Color4 GetColor();

			/// <summary>
			/// Gets the value of the variable as an array of floating point vectors.
			/// </summary>
			/// <param name="count">The number of values to get.</param>
			/// <returns>The floating point vector values of the variable.</returns>
			array<Vector4>^ GetVectorArray(int count);

			/// <summary>
			/// Gets the value of the variable as an array of color values.
			/// </summary>
			/// <param name="count">The number of values to get.</param>
			/// <returns>The color values of the variable.</returns>
			array<Color4>^ GetColorArray(int count);

			/// <summary>
			/// Gets the value of the variable as an array of boolean vectors.
			/// </summary>
			/// <param name="count">The number of 4-element vectors to get.</param>
			/// <returns>The boolean vector values of the variable.</returns>
			array<bool>^ GetBoolVectorArray(int count);

			/// <summary>
			/// Gets the value of the variable as an array of integer vectors.
			/// </summary>
			/// <param name="count">The number of 4-element vectors to get.</param>
			/// <returns>The integer vector values of the variable.</returns>
			array<int>^ GetIntVectorArray(int count);
		};
	}
};