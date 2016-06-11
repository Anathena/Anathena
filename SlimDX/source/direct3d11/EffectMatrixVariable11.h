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

#include "../math/Matrix.h"

#include "EffectVariable11.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D11
	{
		/// <summary>
		/// Represents a matrix effect variable.
		/// </summary>
		/// <unmanaged>ID3DX11EffectMatrixVariable</unmanaged>
		public ref class EffectMatrixVariable : public EffectVariable
		{
		private:
			ID3DX11EffectMatrixVariable* m_Pointer;
			
		internal:
			EffectMatrixVariable( ID3DX11EffectMatrixVariable* pointer );

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="EffectMatrixVariable"/> class.
			/// </summary>
			/// <param name="pointer">A pointer to the unmanaged interface.</param>
			EffectMatrixVariable( System::IntPtr pointer );

			/// <summary>
			/// Gets the value of the variable.
			/// </summary>
			/// <returns>The matrix value of the variable.</returns>
			Matrix GetMatrix();

			/// <summary>
			/// Gets the value of the variable.
			/// </summary>
			/// <param name="result">When the method completes, contains the value of the variable.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetMatrix([Out] Matrix% result);

			/// <summary>
			/// Gets an array of matrix values from the variable.
			/// </summary>
			/// <param name="matrices">An array of matrices to hold the retrieved values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetMatrixArray(array<Matrix>^ matrices);

			/// <summary>
			/// Gets an array of matrix values from the variable.
			/// </summary>
			/// <param name="matrices">An array of matrices to hold the retrieved values.</param>
			/// <param name="offset">The offset into the array to begin storing values.</param>
			/// <param name="count">The number of values to get.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetMatrixArray(array<Matrix>^ matrices, int offset, int count);

			/// <summary>
			/// Gets the transposed value of the variable.
			/// </summary>
			/// <returns>The transposed matrix value of the variable.</returns>
			Matrix GetMatrixTranspose();

			/// <summary>
			/// Gets the transposed value of the variable.
			/// </summary>
			/// <param name="result">When the method completes, contains the transposed value of the variable.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetMatrixTranspose([Out] Matrix% result);

			/// <summary>
			/// Gets an array of transposed matrix values from the variable.
			/// </summary>
			/// <param name="matrices">An array of matrices to hold the retrieved transposed values.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetMatrixTransposeArray(array<Matrix>^ matrices);

			/// <summary>
			/// Gets an array of transposed matrix values from the variable.
			/// </summary>
			/// <param name="matrices">An array of matrices to hold the retrieved transposed values.</param>
			/// <param name="offset">The offset into the array to begin storing values.</param>
			/// <param name="count">The number of values to get.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetMatrixTransposeArray(array<Matrix>^ matrices, int offset, int count);
			
			/// <summary>
			/// Sets the value of the variable.
			/// </summary>
			/// <param name="matrix">The matrix value to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetMatrix( Matrix matrix );

			/// <summary>
			/// Sets the value of the variable.
			/// </summary>
			/// <param name="matrices">An array of matrices to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetMatrixArray( array<Matrix>^ matrices );

			/// <summary>
			/// Sets the value of the variable.
			/// </summary>
			/// <param name="matrices">An array of matrices to set.</param>
			/// <param name="offset">The offset into the array to begin setting values.</param>
			/// <param name="count">The number of values to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetMatrixArray( array<Matrix>^ matrices, int offset, int count );

			/// <summary>
			/// Transposes and sets the matrix value of the variable.
			/// </summary>
			/// <param name="matrix">The matrix value to transpose and set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetMatrixTranspose( Matrix matrix );

			/// <summary>
			/// Transposes and sets the matrix value of the variable.
			/// </summary>
			/// <param name="matrices">An array of matrices to transpose and set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetMatrixTransposeArray( array<Matrix>^ matrices );

			/// <summary>
			/// Transposes and sets the matrix value of the variable.
			/// </summary>
			/// <param name="matrices">An array of matrices to transpose and set.</param>
			/// <param name="offset">The offset into the array to begin setting values.</param>
			/// <param name="count">The number of values to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetMatrixTransposeArray( array<Matrix>^ matrices, int offset, int count );
		};
	}
};