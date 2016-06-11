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
#include "stdafx.h"

#include "../Utilities.h"

#include "Matrix.h"
#include "Plane.h"
#include "Quaternion.h"
#include "Vector2.h"
#include "Vector3.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	D3DXMATRIX Matrix::ToD3DXMATRIX( Matrix matrix )
	{
		D3DXMATRIX result;
		result._11 = matrix.M11;
		result._12 = matrix.M12;
		result._13 = matrix.M13;
		result._14 = matrix.M14;
		result._21 = matrix.M21;
		result._22 = matrix.M22;
		result._23 = matrix.M23;
		result._24 = matrix.M24;
		result._31 = matrix.M31;
		result._32 = matrix.M32;
		result._33 = matrix.M33;
		result._34 = matrix.M34;
		result._41 = matrix.M41;
		result._42 = matrix.M42;
		result._43 = matrix.M43;
		result._44 = matrix.M44;

		return result;
	}

	Matrix Matrix::FromD3DXMATRIX( const D3DXMATRIX &matrix )
	{
		Matrix result;
		result.M11 = matrix._11;
		result.M12 = matrix._12;
		result.M13 = matrix._13;
		result.M14 = matrix._14;
		result.M21 = matrix._21;
		result.M22 = matrix._22;
		result.M23 = matrix._23;
		result.M24 = matrix._24;
		result.M31 = matrix._31;
		result.M32 = matrix._32;
		result.M33 = matrix._33;
		result.M34 = matrix._34;
		result.M41 = matrix._41;
		result.M42 = matrix._42;
		result.M43 = matrix._43;
		result.M44 = matrix._44;

		return result;
	}

	float Matrix::default::get( int row, int column )
	{
		if( row < 0 || row > 3 )
			throw gcnew ArgumentOutOfRangeException( "row", "Rows and columns for matrices run from 0 to 3, inclusive." );

		if( column < 0 || column > 3 )
			throw gcnew ArgumentOutOfRangeException( "column", "Rows and columns for matrices run from 0 to 3, inclusive." );

		int index = row * 4 + column;
		switch( index )
		{
		case 0:  return M11;
		case 1:  return M12;
		case 2:  return M13;
		case 3:  return M14;
		case 4:  return M21;
		case 5:  return M22;
		case 6:  return M23;
		case 7:  return M24;
		case 8:  return M31;
		case 9:  return M32;
		case 10: return M33;
		case 11: return M34;
		case 12: return M41;
		case 13: return M42;
		case 14: return M43;
		case 15: return M44;
		}

		return 0.0f;
	}
	
	void Matrix::default::set( int row, int column, float value ) 
	{
		if( row < 0 || row > 3 )
			throw gcnew ArgumentOutOfRangeException( "row", "Rows and columns for matrices run from 0 to 3, inclusive." );

		if( column < 0 || column > 3 )
			throw gcnew ArgumentOutOfRangeException( "column", "Rows and columns for matrices run from 0 to 3, inclusive." );

		int index = row * 4 + column;
		switch( index )
		{
		case 0:  M11 = value; break;
		case 1:  M12 = value; break;
		case 2:  M13 = value; break;
		case 3:  M14 = value; break;
		case 4:  M21 = value; break;
		case 5:  M22 = value; break;
		case 6:  M23 = value; break;
		case 7:  M24 = value; break;
		case 8:  M31 = value; break;
		case 9:  M32 = value; break;
		case 10: M33 = value; break;
		case 11: M34 = value; break;
		case 12: M41 = value; break;
		case 13: M42 = value; break;
		case 14: M43 = value; break;
		case 15: M44 = value; break;
		}
	}

	Vector4 Matrix::Rows::get( int row )
	{
		return Vector4( default[row, 0], default[row, 1], default[row, 2], default[row, 3] );
	}

	void Matrix::Rows::set( int row, Vector4 value )
	{
		default[row, 0] = value.X;
		default[row, 1] = value.Y;
		default[row, 2] = value.Z;
		default[row, 3] = value.W;
	}

	Vector4 Matrix::Columns::get( int column )
	{
		return Vector4( default[0, column], default[1, column], default[2, column], default[3, column] );
	}

	void Matrix::Columns::set( int column, Vector4 value )
	{
		default[0, column] = value.X;
		default[1, column] = value.Y;
		default[2, column] = value.Z;
		default[3, column] = value.W;
	}

	Matrix Matrix::Identity::get()
	{
		Matrix result;
		result.M11 = 1.0f;
		result.M22 = 1.0f;
		result.M33 = 1.0f;
		result.M44 = 1.0f;

		return result;
	}

	bool Matrix::IsIdentity::get()
	{
		if( M11 != 1.0f|| M22 != 1.0f || M33 != 1.0f || M44 != 1.0f )
			return false;

		if( M12 != 0.0f || M13 != 0.0f || M14 != 0.0f ||
			M21 != 0.0f || M23 != 0.0f || M24 != 0.0f ||
			M31 != 0.0f || M32 != 0.0f || M34 != 0.0f ||
			M41 != 0.0f || M42 != 0.0f || M43 != 0.0f )
			return false;

		return true;
	}

	array<float>^ Matrix::ToArray()
	{
		array<float>^ result = gcnew array<float>( 16 );
		result[0] = M11;
		result[1] = M12;
		result[2] = M13;
		result[3] = M14;
		result[4] = M21;
		result[5] = M22;
		result[6] = M23;
		result[7] = M24;
		result[8] = M31;
		result[9] = M32;
		result[10] = M33;
		result[11] = M34;
		result[12] = M41;
		result[13] = M42;
		result[14] = M43;
		result[15] = M44;

		return result;
	}

	void Matrix::Invert()
	{
		pin_ptr<Matrix> pinnedThis = this;
		D3DXMatrixInverse( (D3DXMATRIX*) pinnedThis, NULL, (D3DXMATRIX*) pinnedThis );
	}

	bool Matrix::Decompose( [Out] Vector3% scale, [Out] Quaternion% rotation, [Out] Vector3% translation )
	{
		Vector3 localScale, localTrans;
		Quaternion localRot;
		pin_ptr<Matrix> pinnedThis = this;

		HRESULT hr = D3DXMatrixDecompose( (D3DXVECTOR3*) &localScale, (D3DXQUATERNION*) &localRot,
			(D3DXVECTOR3*) &localTrans, (D3DXMATRIX*) pinnedThis );

		scale = localScale;
		rotation = localRot;
		translation = localTrans;

		return hr == S_OK;
	}
	
	float Matrix::Determinant()
	{
		float temp1 = (M33 * M44) - (M34 * M43);
		float temp2 = (M32 * M44) - (M34 * M42);
		float temp3 = (M32 * M43) - (M33 * M42);
		float temp4 = (M31 * M44) - (M34 * M41);
		float temp5 = (M31 * M43) - (M33 * M41);
		float temp6 = (M31 * M42) - (M32 * M41);
		
		return ((((M11 * (((M22 * temp1) - (M23 * temp2)) + (M24 * temp3))) - (M12 * (((M21 * temp1) - 
			(M23 * temp4)) + (M24 * temp5)))) + (M13 * (((M21 * temp2) - (M22 * temp4)) + (M24 * temp6)))) - 
			(M14 * (((M21 * temp3) - (M22 * temp5)) + (M23 * temp6))));
	}

	Matrix Matrix::Add( Matrix left, Matrix right )
	{
		Matrix result;
		result.M11 = left.M11 + right.M11;
		result.M12 = left.M12 + right.M12;
		result.M13 = left.M13 + right.M13;
		result.M14 = left.M14 + right.M14;
		result.M21 = left.M21 + right.M21;
		result.M22 = left.M22 + right.M22;
		result.M23 = left.M23 + right.M23;
		result.M24 = left.M24 + right.M24;
		result.M31 = left.M31 + right.M31;
		result.M32 = left.M32 + right.M32;
		result.M33 = left.M33 + right.M33;
		result.M34 = left.M34 + right.M34;
		result.M41 = left.M41 + right.M41;
		result.M42 = left.M42 + right.M42;
		result.M43 = left.M43 + right.M43;
		result.M44 = left.M44 + right.M44;
		return result;
	}

	void Matrix::Add( Matrix% left, Matrix% right, [Out] Matrix% result )
	{
		Matrix r;
		r.M11 = left.M11 + right.M11;
		r.M12 = left.M12 + right.M12;
		r.M13 = left.M13 + right.M13;
		r.M14 = left.M14 + right.M14;
		r.M21 = left.M21 + right.M21;
		r.M22 = left.M22 + right.M22;
		r.M23 = left.M23 + right.M23;
		r.M24 = left.M24 + right.M24;
		r.M31 = left.M31 + right.M31;
		r.M32 = left.M32 + right.M32;
		r.M33 = left.M33 + right.M33;
		r.M34 = left.M34 + right.M34;
		r.M41 = left.M41 + right.M41;
		r.M42 = left.M42 + right.M42;
		r.M43 = left.M43 + right.M43;
		r.M44 = left.M44 + right.M44;

		result = r;
	}

	Matrix Matrix::Subtract( Matrix left, Matrix right )
	{
		Matrix result;
		result.M11 = left.M11 - right.M11;
		result.M12 = left.M12 - right.M12;
		result.M13 = left.M13 - right.M13;
		result.M14 = left.M14 - right.M14;
		result.M21 = left.M21 - right.M21;
		result.M22 = left.M22 - right.M22;
		result.M23 = left.M23 - right.M23;
		result.M24 = left.M24 - right.M24;
		result.M31 = left.M31 - right.M31;
		result.M32 = left.M32 - right.M32;
		result.M33 = left.M33 - right.M33;
		result.M34 = left.M34 - right.M34;
		result.M41 = left.M41 - right.M41;
		result.M42 = left.M42 - right.M42;
		result.M43 = left.M43 - right.M43;
		result.M44 = left.M44 - right.M44;
		return result;
	}

	void Matrix::Subtract( Matrix% left, Matrix% right, [Out] Matrix% result )
	{
		Matrix r;
		r.M11 = left.M11 - right.M11;
		r.M12 = left.M12 - right.M12;
		r.M13 = left.M13 - right.M13;
		r.M14 = left.M14 - right.M14;
		r.M21 = left.M21 - right.M21;
		r.M22 = left.M22 - right.M22;
		r.M23 = left.M23 - right.M23;
		r.M24 = left.M24 - right.M24;
		r.M31 = left.M31 - right.M31;
		r.M32 = left.M32 - right.M32;
		r.M33 = left.M33 - right.M33;
		r.M34 = left.M34 - right.M34;
		r.M41 = left.M41 - right.M41;
		r.M42 = left.M42 - right.M42;
		r.M43 = left.M43 - right.M43;
		r.M44 = left.M44 - right.M44;

		result = r;
	}

	Matrix Matrix::Negate( Matrix matrix )
	{
		Matrix result;
		result.M11 = -matrix.M11;
		result.M12 = -matrix.M12;
		result.M13 = -matrix.M13;
		result.M14 = -matrix.M14;
		result.M21 = -matrix.M21;
		result.M22 = -matrix.M22;
		result.M23 = -matrix.M23;
		result.M24 = -matrix.M24;
		result.M31 = -matrix.M31;
		result.M32 = -matrix.M32;
		result.M33 = -matrix.M33;
		result.M34 = -matrix.M34;
		result.M41 = -matrix.M41;
		result.M42 = -matrix.M42;
		result.M43 = -matrix.M43;
		result.M44 = -matrix.M44;
		return result;
	}

	void Matrix::Negate( Matrix% matrix, [Out] Matrix% result )
	{
		Matrix r;
		r.M11 = -matrix.M11;
		r.M12 = -matrix.M12;
		r.M13 = -matrix.M13;
		r.M14 = -matrix.M14;
		r.M21 = -matrix.M21;
		r.M22 = -matrix.M22;
		r.M23 = -matrix.M23;
		r.M24 = -matrix.M24;
		r.M31 = -matrix.M31;
		r.M32 = -matrix.M32;
		r.M33 = -matrix.M33;
		r.M34 = -matrix.M34;
		r.M41 = -matrix.M41;
		r.M42 = -matrix.M42;
		r.M43 = -matrix.M43;
		r.M44 = -matrix.M44;

		result = r;
	}

	Matrix Matrix::Multiply( Matrix left, Matrix right )
	{
		Matrix result;
		result.M11 = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31) + (left.M14 * right.M41);
		result.M12 = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32) + (left.M14 * right.M42);
		result.M13 = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33) + (left.M14 * right.M43);
		result.M14 = (left.M11 * right.M14) + (left.M12 * right.M24) + (left.M13 * right.M34) + (left.M14 * right.M44);
		result.M21 = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31) + (left.M24 * right.M41);
		result.M22 = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32) + (left.M24 * right.M42);
		result.M23 = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33) + (left.M24 * right.M43);
		result.M24 = (left.M21 * right.M14) + (left.M22 * right.M24) + (left.M23 * right.M34) + (left.M24 * right.M44);
		result.M31 = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31) + (left.M34 * right.M41);
		result.M32 = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32) + (left.M34 * right.M42);
		result.M33 = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33) + (left.M34 * right.M43);
		result.M34 = (left.M31 * right.M14) + (left.M32 * right.M24) + (left.M33 * right.M34) + (left.M34 * right.M44);
		result.M41 = (left.M41 * right.M11) + (left.M42 * right.M21) + (left.M43 * right.M31) + (left.M44 * right.M41);
		result.M42 = (left.M41 * right.M12) + (left.M42 * right.M22) + (left.M43 * right.M32) + (left.M44 * right.M42);
		result.M43 = (left.M41 * right.M13) + (left.M42 * right.M23) + (left.M43 * right.M33) + (left.M44 * right.M43);
		result.M44 = (left.M41 * right.M14) + (left.M42 * right.M24) + (left.M43 * right.M34) + (left.M44 * right.M44);
		return result;
	}

	void Matrix::Multiply( Matrix% left, Matrix% right, [Out] Matrix% result )
	{
		Matrix r;
		r.M11 = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31) + (left.M14 * right.M41);
		r.M12 = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32) + (left.M14 * right.M42);
		r.M13 = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33) + (left.M14 * right.M43);
		r.M14 = (left.M11 * right.M14) + (left.M12 * right.M24) + (left.M13 * right.M34) + (left.M14 * right.M44);
		r.M21 = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31) + (left.M24 * right.M41);
		r.M22 = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32) + (left.M24 * right.M42);
		r.M23 = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33) + (left.M24 * right.M43);
		r.M24 = (left.M21 * right.M14) + (left.M22 * right.M24) + (left.M23 * right.M34) + (left.M24 * right.M44);
		r.M31 = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31) + (left.M34 * right.M41);
		r.M32 = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32) + (left.M34 * right.M42);
		r.M33 = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33) + (left.M34 * right.M43);
		r.M34 = (left.M31 * right.M14) + (left.M32 * right.M24) + (left.M33 * right.M34) + (left.M34 * right.M44);
		r.M41 = (left.M41 * right.M11) + (left.M42 * right.M21) + (left.M43 * right.M31) + (left.M44 * right.M41);
		r.M42 = (left.M41 * right.M12) + (left.M42 * right.M22) + (left.M43 * right.M32) + (left.M44 * right.M42);
		r.M43 = (left.M41 * right.M13) + (left.M42 * right.M23) + (left.M43 * right.M33) + (left.M44 * right.M43);
		r.M44 = (left.M41 * right.M14) + (left.M42 * right.M24) + (left.M43 * right.M34) + (left.M44 * right.M44);
	
		result = r;
	}

	void Matrix::Multiply( Matrix* left, Matrix* right, Matrix* result, int count )
	{
		for( int i = 0; i < count; ++i )
		{
			D3DXMatrixMultiply( reinterpret_cast<D3DXMATRIX*>( &left[i] ),
				reinterpret_cast<const D3DXMATRIX*>( &right[i] ),
				reinterpret_cast<const D3DXMATRIX*>( &result[i] ) );
		}
	}

	void Matrix::Multiply( array<Matrix>^ left, array<Matrix>^ right, array<Matrix>^ result, int offset, int count )
	{
		if( left->Length != right->Length )
			throw gcnew ArgumentException( "Left and right arrays must be the same size.", "right" );
		if( right->Length != result->Length )
			throw gcnew ArgumentException( "Result array must be the same size as input arrays.", "result" );
		Utilities::CheckArrayBounds( left, offset, count );

		pin_ptr<Matrix> pinnedLeft = &left[offset];
		pin_ptr<Matrix> pinnedRight = &right[offset];
		pin_ptr<Matrix> pinnedResult = &result[offset];

		Multiply( pinnedLeft, pinnedRight, pinnedResult, count );
	}

	void Matrix::Multiply( array<Matrix>^ left, Matrix right, array<Matrix>^ result, int offset, int count )
	{
		if( left->Length != result->Length )
			throw gcnew ArgumentException( "Result array must be the same size as the input array.", "result" );
		Utilities::CheckArrayBounds( left, offset, count );

		pin_ptr<Matrix> pinnedLeft = &left[offset];
		pin_ptr<Matrix> pinnedResult = &result[offset];
		for( int i = 0; i < count; ++i )
		{
			D3DXMatrixMultiply( reinterpret_cast<D3DXMATRIX*>( &pinnedLeft[i] ),
				reinterpret_cast<const D3DXMATRIX*>( &right ),
				reinterpret_cast<const D3DXMATRIX*>( &pinnedResult[i] ) );
		}
	}

	Matrix Matrix::Multiply( Matrix left, float right )
	{
		Matrix result;
		result.M11 = left.M11 * right;
		result.M12 = left.M12 * right;
		result.M13 = left.M13 * right;
		result.M14 = left.M14 * right;
		result.M21 = left.M21 * right;
		result.M22 = left.M22 * right;
		result.M23 = left.M23 * right;
		result.M24 = left.M24 * right;
		result.M31 = left.M31 * right;
		result.M32 = left.M32 * right;
		result.M33 = left.M33 * right;
		result.M34 = left.M34 * right;
		result.M41 = left.M41 * right;
		result.M42 = left.M42 * right;
		result.M43 = left.M43 * right;
		result.M44 = left.M44 * right;
		return result;
	}

	void Matrix::Multiply( Matrix% left, float right, [Out] Matrix% result )
	{
		Matrix r;
		r.M11 = left.M11 * right;
		r.M12 = left.M12 * right;
		r.M13 = left.M13 * right;
		r.M14 = left.M14 * right;
		r.M21 = left.M21 * right;
		r.M22 = left.M22 * right;
		r.M23 = left.M23 * right;
		r.M24 = left.M24 * right;
		r.M31 = left.M31 * right;
		r.M32 = left.M32 * right;
		r.M33 = left.M33 * right;
		r.M34 = left.M34 * right;
		r.M41 = left.M41 * right;
		r.M42 = left.M42 * right;
		r.M43 = left.M43 * right;
		r.M44 = left.M44 * right;

		result = r;
	}

	Matrix Matrix::Divide( Matrix left, Matrix right )
	{
		Matrix result;
		result.M11 = left.M11 / right.M11;
		result.M12 = left.M12 / right.M12;
		result.M13 = left.M13 / right.M13;
		result.M14 = left.M14 / right.M14;
		result.M21 = left.M21 / right.M21;
		result.M22 = left.M22 / right.M22;
		result.M23 = left.M23 / right.M23;
		result.M24 = left.M24 / right.M24;
		result.M31 = left.M31 / right.M31;
		result.M32 = left.M32 / right.M32;
		result.M33 = left.M33 / right.M33;
		result.M34 = left.M34 / right.M34;
		result.M41 = left.M41 / right.M41;
		result.M42 = left.M42 / right.M42;
		result.M43 = left.M43 / right.M43;
		result.M44 = left.M44 / right.M44;
		return result;
	}

	void Matrix::Divide( Matrix% left, Matrix% right, [Out] Matrix% result )
	{
		Matrix r;
		r.M11 = left.M11 / right.M11;
		r.M12 = left.M12 / right.M12;
		r.M13 = left.M13 / right.M13;
		r.M14 = left.M14 / right.M14;
		r.M21 = left.M21 / right.M21;
		r.M22 = left.M22 / right.M22;
		r.M23 = left.M23 / right.M23;
		r.M24 = left.M24 / right.M24;
		r.M31 = left.M31 / right.M31;
		r.M32 = left.M32 / right.M32;
		r.M33 = left.M33 / right.M33;
		r.M34 = left.M34 / right.M34;
		r.M41 = left.M41 / right.M41;
		r.M42 = left.M42 / right.M42;
		r.M43 = left.M43 / right.M43;
		r.M44 = left.M44 / right.M44;

		result = r;
	}

	Matrix Matrix::Divide( Matrix left, float right )
	{
		Matrix result;
		float inv = 1.0f / right;

		result.M11 = left.M11 * inv;
		result.M12 = left.M12 * inv;
		result.M13 = left.M13 * inv;
		result.M14 = left.M14 * inv;
		result.M21 = left.M21 * inv;
		result.M22 = left.M22 * inv;
		result.M23 = left.M23 * inv;
		result.M24 = left.M24 * inv;
		result.M31 = left.M31 * inv;
		result.M32 = left.M32 * inv;
		result.M33 = left.M33 * inv;
		result.M34 = left.M34 * inv;
		result.M41 = left.M41 * inv;
		result.M42 = left.M42 * inv;
		result.M43 = left.M43 * inv;
		result.M44 = left.M44 * inv;
		return result;
	}

	void Matrix::Divide( Matrix% left, float right, [Out] Matrix% result )
	{
		float inv = 1.0f / right;

		Matrix r;
		r.M11 = left.M11 * inv;
		r.M12 = left.M12 * inv;
		r.M13 = left.M13 * inv;
		r.M14 = left.M14 * inv;
		r.M21 = left.M21 * inv;
		r.M22 = left.M22 * inv;
		r.M23 = left.M23 * inv;
		r.M24 = left.M24 * inv;
		r.M31 = left.M31 * inv;
		r.M32 = left.M32 * inv;
		r.M33 = left.M33 * inv;
		r.M34 = left.M34 * inv;
		r.M41 = left.M41 * inv;
		r.M42 = left.M42 * inv;
		r.M43 = left.M43 * inv;
		r.M44 = left.M44 * inv;

		result = r;
	}

	Matrix Matrix::Lerp( Matrix value1, Matrix value2, float amount )
	{
		Matrix result;
		result.M11 = value1.M11 + ((value2.M11 - value1.M11) * amount);
		result.M12 = value1.M12 + ((value2.M12 - value1.M12) * amount);
		result.M13 = value1.M13 + ((value2.M13 - value1.M13) * amount);
		result.M14 = value1.M14 + ((value2.M14 - value1.M14) * amount);
		result.M21 = value1.M21 + ((value2.M21 - value1.M21) * amount);
		result.M22 = value1.M22 + ((value2.M22 - value1.M22) * amount);
		result.M23 = value1.M23 + ((value2.M23 - value1.M23) * amount);
		result.M24 = value1.M24 + ((value2.M24 - value1.M24) * amount);
		result.M31 = value1.M31 + ((value2.M31 - value1.M31) * amount);
		result.M32 = value1.M32 + ((value2.M32 - value1.M32) * amount);
		result.M33 = value1.M33 + ((value2.M33 - value1.M33) * amount);
		result.M34 = value1.M34 + ((value2.M34 - value1.M34) * amount);
		result.M41 = value1.M41 + ((value2.M41 - value1.M41) * amount);
		result.M42 = value1.M42 + ((value2.M42 - value1.M42) * amount);
		result.M43 = value1.M43 + ((value2.M43 - value1.M43) * amount);
		result.M44 = value1.M44 + ((value2.M44 - value1.M44) * amount);
		return result;
	}

	void Matrix::Lerp( Matrix% value1, Matrix% value2, float amount, [Out] Matrix% result )
	{
		Matrix r;
		r.M11 = value1.M11 + ((value2.M11 - value1.M11) * amount);
		r.M12 = value1.M12 + ((value2.M12 - value1.M12) * amount);
		r.M13 = value1.M13 + ((value2.M13 - value1.M13) * amount);
		r.M14 = value1.M14 + ((value2.M14 - value1.M14) * amount);
		r.M21 = value1.M21 + ((value2.M21 - value1.M21) * amount);
		r.M22 = value1.M22 + ((value2.M22 - value1.M22) * amount);
		r.M23 = value1.M23 + ((value2.M23 - value1.M23) * amount);
		r.M24 = value1.M24 + ((value2.M24 - value1.M24) * amount);
		r.M31 = value1.M31 + ((value2.M31 - value1.M31) * amount);
		r.M32 = value1.M32 + ((value2.M32 - value1.M32) * amount);
		r.M33 = value1.M33 + ((value2.M33 - value1.M33) * amount);
		r.M34 = value1.M34 + ((value2.M34 - value1.M34) * amount);
		r.M41 = value1.M41 + ((value2.M41 - value1.M41) * amount);
		r.M42 = value1.M42 + ((value2.M42 - value1.M42) * amount);
		r.M43 = value1.M43 + ((value2.M43 - value1.M43) * amount);
		r.M44 = value1.M44 + ((value2.M44 - value1.M44) * amount);

		result = r;
	}

	Matrix Matrix::Billboard( Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector )
	{
		Matrix result;
		Vector3 difference = objectPosition - cameraPosition;
		Vector3 crossed;
		Vector3 final;

		float lengthSq = difference.LengthSquared();
		if (lengthSq < 0.0001f)
			difference = -cameraForwardVector;
		else
			difference *= static_cast<float>( 1.0f / Math::Sqrt( lengthSq ) );

		Vector3::Cross( cameraUpVector, difference, crossed );
		crossed.Normalize();
		Vector3::Cross( difference, crossed, final );

		result.M11 = crossed.X;
		result.M12 = crossed.Y;
		result.M13 = crossed.Z;
		result.M14 = 0.0f;
		result.M21 = final.X;
		result.M22 = final.Y;
		result.M23 = final.Z;
		result.M24 = 0.0f;
		result.M31 = difference.X;
		result.M32 = difference.Y;
		result.M33 = difference.Z;
		result.M34 = 0.0f;
		result.M41 = objectPosition.X;
		result.M42 = objectPosition.Y;
		result.M43 = objectPosition.Z;
		result.M44 = 1.0f;

		return result;
	}

	void Matrix::Billboard( Vector3% objectPosition, Vector3% cameraPosition, Vector3% cameraUpVector, Vector3% cameraForwardVector, [Out] Matrix% result )
	{
		Vector3 difference = objectPosition - cameraPosition;
		Vector3 crossed;
		Vector3 final;

		float lengthSq = difference.LengthSquared();
		if (lengthSq < 0.0001f)
			difference = -cameraForwardVector;
		else
			difference *= static_cast<float>( 1.0f / Math::Sqrt( lengthSq ) );

		Vector3::Cross( cameraUpVector, difference, crossed );
		crossed.Normalize();
		Vector3::Cross( difference, crossed, final );

		result.M11 = crossed.X;
		result.M12 = crossed.Y;
		result.M13 = crossed.Z;
		result.M14 = 0.0f;
		result.M21 = final.X;
		result.M22 = final.Y;
		result.M23 = final.Z;
		result.M24 = 0.0f;
		result.M31 = difference.X;
		result.M32 = difference.Y;
		result.M33 = difference.Z;
		result.M34 = 0.0f;
		result.M41 = objectPosition.X;
		result.M42 = objectPosition.Y;
		result.M43 = objectPosition.Z;
		result.M44 = 1.0f;
	}

	Matrix Matrix::RotationX( float angle )
	{
		Matrix result;
		float cos = static_cast<float>( Math::Cos( static_cast<double>( angle ) ) );
		float sin = static_cast<float>( Math::Sin( static_cast<double>( angle ) ) );

		result.M11 = 1.0f;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = cos;
		result.M23 = sin;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = -sin;
		result.M33 = cos;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;

		return result;
	}

	void Matrix::RotationX( float angle, [Out] Matrix% result )
	{
		float cos = static_cast<float>( Math::Cos( static_cast<double>( angle ) ) );
		float sin = static_cast<float>( Math::Sin( static_cast<double>( angle ) ) );

		result.M11 = 1.0f;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = cos;
		result.M23 = sin;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = -sin;
		result.M33 = cos;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
	}
	
	Matrix Matrix::RotationY( float angle )
	{
		Matrix result;
		float cos = static_cast<float>( Math::Cos( static_cast<double>( angle ) ) );
		float sin = static_cast<float>( Math::Sin( static_cast<double>( angle ) ) );

		result.M11 = cos;
		result.M12 = 0.0f;
		result.M13 = -sin;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = 1.0f;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = sin;
		result.M32 = 0.0f;
		result.M33 = cos;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;

		return result;
	}

	void Matrix::RotationY( float angle, [Out] Matrix% result )
	{
		float cos = static_cast<float>( Math::Cos( static_cast<double>( angle ) ) );
		float sin = static_cast<float>( Math::Sin( static_cast<double>( angle ) ) );

		result.M11 = cos;
		result.M12 = 0.0f;
		result.M13 = -sin;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = 1.0f;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = sin;
		result.M32 = 0.0f;
		result.M33 = cos;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
	}
	
	Matrix Matrix::RotationZ( float angle )
	{
		Matrix result;
		float cos = static_cast<float>( Math::Cos( static_cast<double>( angle ) ) );
		float sin = static_cast<float>( Math::Sin( static_cast<double>( angle ) ) );

		result.M11 = cos;
		result.M12 = sin;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = -sin;
		result.M22 = cos;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = 1.0f;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;

		return result;
	}

	void Matrix::RotationZ( float angle, [Out] Matrix% result )
	{
		float cos = static_cast<float>( Math::Cos( static_cast<double>( angle ) ) );
		float sin = static_cast<float>( Math::Sin( static_cast<double>( angle ) ) );

		result.M11 = cos;
		result.M12 = sin;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = -sin;
		result.M22 = cos;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = 1.0f;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
	}

	Matrix Matrix::RotationQuaternion( Quaternion quaternion )
	{
		Matrix result;

		float xx = quaternion.X * quaternion.X;
		float yy = quaternion.Y * quaternion.Y;
		float zz = quaternion.Z * quaternion.Z;
		float xy = quaternion.X * quaternion.Y;
		float zw = quaternion.Z * quaternion.W;
		float zx = quaternion.Z * quaternion.X;
		float yw = quaternion.Y * quaternion.W;
		float yz = quaternion.Y * quaternion.Z;
		float xw = quaternion.X * quaternion.W;
		result.M11 = 1.0f - (2.0f * (yy + zz));
		result.M12 = 2.0f * (xy + zw);
		result.M13 = 2.0f * (zx - yw);
		result.M14 = 0.0f;
		result.M21 = 2.0f * (xy - zw);
		result.M22 = 1.0f - (2.0f * (zz + xx));
		result.M23 = 2.0f * (yz + xw);
		result.M24 = 0.0f;
		result.M31 = 2.0f * (zx + yw);
		result.M32 = 2.0f * (yz - xw);
		result.M33 = 1.0f - (2.0f * (yy + xx));
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;

		return result;
	}

	void Matrix::RotationQuaternion( Quaternion% rotation, [Out] Matrix% result )
	{
		float xx = rotation.X * rotation.X;
		float yy = rotation.Y * rotation.Y;
		float zz = rotation.Z * rotation.Z;
		float xy = rotation.X * rotation.Y;
		float zw = rotation.Z * rotation.W;
		float zx = rotation.Z * rotation.X;
		float yw = rotation.Y * rotation.W;
		float yz = rotation.Y * rotation.Z;
		float xw = rotation.X * rotation.W;
		result.M11 = 1.0f - (2.0f * (yy + zz));
		result.M12 = 2.0f * (xy + zw);
		result.M13 = 2.0f * (zx - yw);
		result.M14 = 0.0f;
		result.M21 = 2.0f * (xy - zw);
		result.M22 = 1.0f - (2.0f * (zz + xx));
		result.M23 = 2.0f * (yz + xw);
		result.M24 = 0.0f;
		result.M31 = 2.0f * (zx + yw);
		result.M32 = 2.0f * (yz - xw);
		result.M33 = 1.0f - (2.0f * (yy + xx));
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
	}

	Matrix Matrix::RotationAxis( Vector3 axis, float angle )
	{
		if( axis.LengthSquared() != 1.0f )
			axis.Normalize();

		Matrix result;
		float x = axis.X;
		float y = axis.Y;
		float z = axis.Z;
		float cos = static_cast<float>( Math::Cos( static_cast<double>( angle ) ) );
		float sin = static_cast<float>( Math::Sin( static_cast<double>( angle ) ) );
		float xx = x * x;
		float yy = y * y;
		float zz = z * z;
		float xy = x * y;
		float xz = x * z;
		float yz = y * z;

		result.M11 = xx + (cos * (1.0f - xx));
		result.M12 = (xy - (cos * xy)) + (sin * z);
		result.M13 = (xz - (cos * xz)) - (sin * y);
		result.M14 = 0.0f;
		result.M21 = (xy - (cos * xy)) - (sin * z);
		result.M22 = yy + (cos * (1.0f - yy));
		result.M23 = (yz - (cos * yz)) + (sin * x);
		result.M24 = 0.0f;
		result.M31 = (xz - (cos * xz)) + (sin * y);
		result.M32 = (yz - (cos * yz)) - (sin * x);
		result.M33 = zz + (cos * (1.0f - zz));
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;

		return result;
	}

	void Matrix::RotationAxis( Vector3% axis, float angle, [Out] Matrix% result )
	{
		if( axis.LengthSquared() != 1.0f )
			axis.Normalize();

		float x = axis.X;
		float y = axis.Y;
		float z = axis.Z;
		float cos = static_cast<float>( Math::Cos( static_cast<double>( angle ) ) );
		float sin = static_cast<float>( Math::Sin( static_cast<double>( angle ) ) );
		float xx = x * x;
		float yy = y * y;
		float zz = z * z;
		float xy = x * y;
		float xz = x * z;
		float yz = y * z;

		result.M11 = xx + (cos * (1.0f - xx));
		result.M12 = (xy - (cos * xy)) + (sin * z);
		result.M13 = (xz - (cos * xz)) - (sin * y);
		result.M14 = 0.0f;
		result.M21 = (xy - (cos * xy)) - (sin * z);
		result.M22 = yy + (cos * (1.0f - yy));
		result.M23 = (yz - (cos * yz)) + (sin * x);
		result.M24 = 0.0f;
		result.M31 = (xz - (cos * xz)) + (sin * y);
		result.M32 = (yz - (cos * yz)) - (sin * x);
		result.M33 = zz + (cos * (1.0f - zz));
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
	}

	Matrix Matrix::RotationYawPitchRoll( float yaw, float pitch, float roll )
	{
		Matrix result;
		Quaternion quaternion;
		Quaternion::RotationYawPitchRoll( yaw, pitch, roll, quaternion );
		RotationQuaternion( quaternion, result );
		return result;
	}

	void Matrix::RotationYawPitchRoll( float yaw, float pitch, float roll, [Out] Matrix% result )
	{
		Quaternion quaternion;
		Quaternion::RotationYawPitchRoll( yaw, pitch, roll, quaternion );
		RotationQuaternion( quaternion, result );
	}

	Matrix Matrix::Translation( float x, float y, float z )
	{
		Matrix result;
		result.M11 = 1.0f;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = 1.0f;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = 1.0f;
		result.M34 = 0.0f;
		result.M41 = x;
		result.M42 = y;
		result.M43 = z;
		result.M44 = 1.0f;
		return result;
	}

	void Matrix::Translation( float x, float y, float z, [Out] Matrix% result )
	{
		result.M11 = 1.0f;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = 1.0f;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = 1.0f;
		result.M34 = 0.0f;
		result.M41 = x;
		result.M42 = y;
		result.M43 = z;
		result.M44 = 1.0f;
	}

	Matrix Matrix::Translation( Vector3 translation )
	{
		Matrix result;
		result.M11 = 1.0f;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = 1.0f;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = 1.0f;
		result.M34 = 0.0f;
		result.M41 = translation.X;
		result.M42 = translation.Y;
		result.M43 = translation.Z;
		result.M44 = 1.0f;
		return result;
	}

	void Matrix::Translation( Vector3% translation, [Out] Matrix% result )
	{
		result.M11 = 1.0f;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = 1.0f;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = 1.0f;
		result.M34 = 0.0f;
		result.M41 = translation.X;
		result.M42 = translation.Y;
		result.M43 = translation.Z;
		result.M44 = 1.0f;
	}

	Matrix Matrix::Scaling ( float x, float y, float z )
	{
		Matrix result;
		result.M11 = x;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = y;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = z;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
		return result;
	}

	void Matrix::Scaling( float x, float y, float z, [Out] Matrix% result )
	{
		result.M11 = x;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = y;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = z;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
	}

	Matrix Matrix::Scaling ( Vector3 scaling )
	{
		Matrix result;
		result.M11 = scaling.X;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = scaling.Y;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = scaling.Z;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
		return result;
	}

	void Matrix::Scaling( Vector3% scaling, [Out] Matrix% result )
	{
		result.M11 = scaling.X;
		result.M12 = 0.0f;
		result.M13 = 0.0f;
		result.M14 = 0.0f;
		result.M21 = 0.0f;
		result.M22 = scaling.Y;
		result.M23 = 0.0f;
		result.M24 = 0.0f;
		result.M31 = 0.0f;
		result.M32 = 0.0f;
		result.M33 = scaling.Z;
		result.M34 = 0.0f;
		result.M41 = 0.0f;
		result.M42 = 0.0f;
		result.M43 = 0.0f;
		result.M44 = 1.0f;
	}

	Matrix Matrix::AffineTransformation( float scaling, Vector3 rotationCenter, Quaternion rotation, Vector3 translation )
	{
		Matrix result;
		D3DXMatrixAffineTransformation( (D3DXMATRIX*) &result, scaling, (D3DXVECTOR3*) &rotationCenter, (D3DXQUATERNION*) &rotation, (D3DXVECTOR3*) &translation );
		return result;
	}

	void Matrix::AffineTransformation( float scaling, Vector3% rotationCenter, Quaternion% rotation, Vector3% translation, [Out] Matrix% result )
	{
		pin_ptr<Vector3> pinRotationCenter = &rotationCenter;
		pin_ptr<Quaternion> pinRotation = &rotation;
		pin_ptr<Vector3> pinTranslation = &translation;
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixAffineTransformation( (D3DXMATRIX*) pinResult, scaling, (D3DXVECTOR3*) pinRotationCenter, (D3DXQUATERNION*) pinRotation, (D3DXVECTOR3*) pinTranslation );
	}
	
	Matrix Matrix::AffineTransformation2D( float scaling, Vector2 rotationCenter, float rotation, Vector2 translation )
	{
		Matrix result;
		D3DXMatrixAffineTransformation2D( (D3DXMATRIX*) &result, scaling, (D3DXVECTOR2*) &rotationCenter, rotation, (D3DXVECTOR2*) &translation );
		return result;
	}

	void Matrix::AffineTransformation2D( float scaling, Vector2% rotationCenter, float rotation, Vector2% translation, [Out] Matrix% result )
	{
		pin_ptr<Vector2> pinRotationCenter = &rotationCenter;
		pin_ptr<Vector2> pinTranslation = &translation;
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixAffineTransformation2D( (D3DXMATRIX*) pinResult, scaling, (D3DXVECTOR2*) pinRotationCenter, rotation, (D3DXVECTOR2*) pinTranslation );
	}
	
	Matrix Matrix::Transformation( Vector3 scalingCenter, Quaternion scalingRotation, Vector3 scaling, Vector3 rotationCenter, Quaternion rotation, Vector3 translation )
	{
		Matrix result;
		D3DXMatrixTransformation( (D3DXMATRIX*) &result, (D3DXVECTOR3*) &scalingCenter, (D3DXQUATERNION*) &scalingRotation, (D3DXVECTOR3*) &scaling, (D3DXVECTOR3*) &rotationCenter, (D3DXQUATERNION*) &rotation, (D3DXVECTOR3*) &translation );
		return result;
	}

	void Matrix::Transformation( Vector3% scalingCenter, Quaternion% scalingRotation, Vector3% scaling, Vector3% rotationCenter, Quaternion% rotation, Vector3% translation, [Out] Matrix% result )
	{
		pin_ptr<Vector3> pinScalingCenter = &scalingCenter;
		pin_ptr<Quaternion> pinScalingRotation = &scalingRotation;
		pin_ptr<Vector3> pinScaling = &scaling;
		pin_ptr<Vector3> pinRotationCenter = &rotationCenter;
		pin_ptr<Quaternion> pinRotation = &rotation;
		pin_ptr<Vector3> pinTranslation = &translation;
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixTransformation( (D3DXMATRIX*) pinResult, (D3DXVECTOR3*) pinScalingCenter, (D3DXQUATERNION*) pinScalingRotation, 
			(D3DXVECTOR3*) pinScaling, (D3DXVECTOR3*) pinRotationCenter, (D3DXQUATERNION*) pinRotation, (D3DXVECTOR3*) pinTranslation );
	}
	
	Matrix Matrix::Transformation2D( Vector2 scalingCenter, float scalingRotation, Vector2 scaling, Vector2 rotationCenter, float rotation, Vector2 translation )
	{
		Matrix result;
		D3DXMatrixTransformation2D( (D3DXMATRIX*) &result, (D3DXVECTOR2*) &scalingCenter, scalingRotation, (D3DXVECTOR2*) &scaling, (D3DXVECTOR2*) &rotationCenter, rotation, (D3DXVECTOR2*) &translation );
		return result;
	}

	void Matrix::Transformation2D( Vector2% scalingCenter, float scalingRotation, Vector2% scaling, Vector2% rotationCenter, float rotation, Vector2% translation, [Out] Matrix% result )
	{
		pin_ptr<Vector2> pinScalingCenter = &scalingCenter;
		pin_ptr<Vector2> pinScaling = &scaling;
		pin_ptr<Vector2> pinRotationCenter = &rotationCenter;
		pin_ptr<Vector2> pinTranslation = &translation;
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixTransformation2D( (D3DXMATRIX*) pinResult, (D3DXVECTOR2*) pinScalingCenter, scalingRotation, 
			(D3DXVECTOR2*) pinScaling, (D3DXVECTOR2*) pinRotationCenter, rotation, (D3DXVECTOR2*) pinTranslation );
	}
	
	Matrix Matrix::LookAtLH(Vector3 eye, Vector3 target, Vector3 up )
	{
		Matrix result;
		D3DXMatrixLookAtLH( (D3DXMATRIX*) &result, (D3DXVECTOR3*) &eye, (D3DXVECTOR3*) &target, (D3DXVECTOR3*) &up );
		return result;
	}

	void Matrix::LookAtLH(Vector3% eye, Vector3% target, Vector3% up, [Out] Matrix% result )
	{
		pin_ptr<Vector3> pinEye = &eye;
		pin_ptr<Vector3> pinTarget = &target;
		pin_ptr<Vector3> pinUp = &up;
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixLookAtLH( (D3DXMATRIX*) pinResult, (D3DXVECTOR3*) pinEye, (D3DXVECTOR3*) pinTarget, (D3DXVECTOR3*) pinUp );
	}
	
	Matrix Matrix::LookAtRH(Vector3 eye, Vector3 target, Vector3 up )
	{
		Matrix result;
		D3DXMatrixLookAtRH( (D3DXMATRIX*) &result, (D3DXVECTOR3*) &eye, (D3DXVECTOR3*) &target, (D3DXVECTOR3*) &up );
		return result;
	}

	void Matrix::LookAtRH(Vector3% eye, Vector3% target, Vector3% up, [Out] Matrix% result )
	{
		pin_ptr<Vector3> pinEye = &eye;
		pin_ptr<Vector3> pinTarget = &target;
		pin_ptr<Vector3> pinUp = &up;
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixLookAtRH( (D3DXMATRIX*) pinResult, (D3DXVECTOR3*) pinEye, (D3DXVECTOR3*) pinTarget, (D3DXVECTOR3*) pinUp );
	}
	
	Matrix Matrix::OrthoLH( float width, float height, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixOrthoLH( (D3DXMATRIX*) &result, width, height, znear, zfar );
		return result;
	}

	void Matrix::OrthoLH( float width, float height, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixOrthoLH( (D3DXMATRIX*) pinResult, width, height, znear, zfar );
	}
	
	Matrix Matrix::OrthoRH( float width, float height, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixOrthoRH( (D3DXMATRIX*) &result, width, height, znear, zfar );
		return result;
	}

	void Matrix::OrthoRH( float width, float height, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixOrthoRH( (D3DXMATRIX*) pinResult, width, height, znear, zfar );
	}
	
	Matrix Matrix::OrthoOffCenterLH( float left, float right, float bottom, float top, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixOrthoOffCenterLH( (D3DXMATRIX*) &result, left, right, bottom, top, znear, zfar );
		return result;
	}

	void Matrix::OrthoOffCenterLH( float left, float right, float bottom, float top, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixOrthoOffCenterLH( (D3DXMATRIX*) pinResult, left, right, bottom, top, znear, zfar );
	}
	
	Matrix Matrix::OrthoOffCenterRH( float left, float right, float bottom, float top, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixOrthoOffCenterRH( (D3DXMATRIX*) &result, left, right, bottom, top, znear, zfar );
		return result;
	}

	void Matrix::OrthoOffCenterRH( float left, float right, float bottom, float top, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixOrthoOffCenterRH( (D3DXMATRIX*) pinResult, left, right, bottom, top, znear, zfar );
	}
	
	Matrix Matrix::PerspectiveLH( float width, float height, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixPerspectiveLH( (D3DXMATRIX*) &result, width, height, znear, zfar );
		return result;
	}

	void Matrix::PerspectiveLH( float width, float height, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixPerspectiveLH( (D3DXMATRIX*) pinResult, width, height, znear, zfar );
	}
	
	Matrix Matrix::PerspectiveRH( float width, float height, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixPerspectiveRH( (D3DXMATRIX*) &result, width, height, znear, zfar );
		return result;
	}

	void Matrix::PerspectiveRH( float width, float height, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixPerspectiveRH( (D3DXMATRIX*) pinResult, width, height, znear, zfar );
	}

	Matrix Matrix::PerspectiveFovLH( float fov, float aspect, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixPerspectiveFovLH( (D3DXMATRIX*) &result, fov, aspect, znear, zfar );
		return result;
	}

	void Matrix::PerspectiveFovLH( float fov, float aspect, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixPerspectiveFovLH( (D3DXMATRIX*) pinResult, fov, aspect, znear, zfar );
	}
	
	Matrix Matrix::PerspectiveFovRH( float fov, float aspect, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixPerspectiveFovRH( (D3DXMATRIX*) &result, fov, aspect, znear, zfar );
		return result;
	}

	void Matrix::PerspectiveFovRH( float fov, float aspect, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixPerspectiveFovRH( (D3DXMATRIX*) pinResult, fov, aspect, znear, zfar );
	}
	
	Matrix Matrix::PerspectiveOffCenterLH( float left, float right, float bottom, float top, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixPerspectiveOffCenterLH( (D3DXMATRIX*) &result, left, right, bottom, top, znear, zfar );
		return result;
	}

	void Matrix::PerspectiveOffCenterLH( float left, float right, float bottom, float top, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixPerspectiveOffCenterLH( (D3DXMATRIX*) pinResult, left, right, bottom, top, znear, zfar );
	}
	
	Matrix Matrix::PerspectiveOffCenterRH( float left, float right, float bottom, float top, float znear, float zfar )
	{
		Matrix result;
		D3DXMatrixPerspectiveOffCenterRH( (D3DXMATRIX*) &result, left, right, bottom, top, znear, zfar );
		return result;
	}

	void Matrix::PerspectiveOffCenterRH( float left, float right, float bottom, float top, float znear, float zfar, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixPerspectiveOffCenterRH( (D3DXMATRIX*) pinResult, left, right, bottom, top, znear, zfar );
	}

	Matrix Matrix::Reflection( Plane plane )
	{
		Matrix result;
		plane.Normalize();
		float x = plane.Normal.X;
		float y = plane.Normal.Y;
		float z = plane.Normal.Z;
		float x2 = -2.0f * x;
		float y2 = -2.0f * y;
		float z2 = -2.0f * z;
		result.M11 = (x2 * x) + 1.0f;
		result.M12 = y2 * x;
		result.M13 = z2 * x;
		result.M14 = 0.0f;
		result.M21 = x2 * y;
		result.M22 = (y2 * y) + 1.0f;
		result.M23 = z2 * y;
		result.M24 = 0.0f;
		result.M31 = x2 * z;
		result.M32 = y2 * z;
		result.M33 = (z2 * z) + 1.0f;
		result.M34 = 0.0f;
		result.M41 = x2 * plane.D;
		result.M42 = y2 * plane.D;
		result.M43 = z2 * plane.D;
		result.M44 = 1.0f;
		return result;
	}

	void Matrix::Reflection( Plane% plane, [Out] Matrix% result )
	{
		plane.Normalize();
		float x = plane.Normal.X;
		float y = plane.Normal.Y;
		float z = plane.Normal.Z;
		float x2 = -2.0f * x;
		float y2 = -2.0f * y;
		float z2 = -2.0f * z;
		result.M11 = (x2 * x) + 1.0f;
		result.M12 = y2 * x;
		result.M13 = z2 * x;
		result.M14 = 0.0f;
		result.M21 = x2 * y;
		result.M22 = (y2 * y) + 1.0f;
		result.M23 = z2 * y;
		result.M24 = 0.0f;
		result.M31 = x2 * z;
		result.M32 = y2 * z;
		result.M33 = (z2 * z) + 1.0f;
		result.M34 = 0.0f;
		result.M41 = x2 * plane.D;
		result.M42 = y2 * plane.D;
		result.M43 = z2 * plane.D;
		result.M44 = 1.0f;
	}
	
	Matrix Matrix::Shadow( Vector4 light, Plane plane )
	{
		Matrix result;
		plane.Normalize();
		float dot = ((plane.Normal.X * light.X) + (plane.Normal.Y * light.Y)) + (plane.Normal.Z * light.Z);
		float x = -plane.Normal.X;
		float y = -plane.Normal.Y;
		float z = -plane.Normal.Z;
		float d = -plane.D;
		result.M11 = (x * light.X) + dot;
		result.M21 = y * light.X;
		result.M31 = z * light.X;
		result.M41 = d * light.X;
		result.M12 = x * light.Y;
		result.M22 = (y * light.Y) + dot;
		result.M32 = z * light.Y;
		result.M42 = d * light.Y;
		result.M13 = x * light.Z;
		result.M23 = y * light.Z;
		result.M33 = (z * light.Z) + dot;
		result.M43 = d * light.Z;
		result.M14 = 0.0f;
		result.M24 = 0.0f;
		result.M34 = 0.0f;
		result.M44 = dot;
		return result;
	}

	void Matrix::Shadow( Vector4% light, Plane% plane, [Out] Matrix% result )
	{
		plane.Normalize();
		float dot = ((plane.Normal.X * light.X) + (plane.Normal.Y * light.Y)) + (plane.Normal.Z * light.Z);
		float x = -plane.Normal.X;
		float y = -plane.Normal.Y;
		float z = -plane.Normal.Z;
		float d = -plane.D;
		result.M11 = (x * light.X) + dot;
		result.M21 = y * light.X;
		result.M31 = z * light.X;
		result.M41 = d * light.X;
		result.M12 = x * light.Y;
		result.M22 = (y * light.Y) + dot;
		result.M32 = z * light.Y;
		result.M42 = d * light.Y;
		result.M13 = x * light.Z;
		result.M23 = y * light.Z;
		result.M33 = (z * light.Z) + dot;
		result.M43 = d * light.Z;
		result.M14 = 0.0f;
		result.M24 = 0.0f;
		result.M34 = 0.0f;
		result.M44 = dot;
	}

	Matrix Matrix::Invert( Matrix mat )
	{
		Matrix result;
		D3DXMatrixInverse( (D3DXMATRIX*) &result, NULL, (D3DXMATRIX*) &mat );
		return result;
	}

	void Matrix::Invert( Matrix% mat, [Out] Matrix% result )
	{
		pin_ptr<Matrix> pinMatrix = &mat;
		pin_ptr<Matrix> pinResult = &result;
		D3DXMatrixInverse( (D3DXMATRIX*) pinResult, NULL, (D3DXMATRIX*) pinMatrix );
	}

	Matrix Matrix::Transpose( Matrix mat )
	{
		Matrix result;
		result.M11 = mat.M11;
		result.M12 = mat.M21;
		result.M13 = mat.M31;
		result.M14 = mat.M41;
		result.M21 = mat.M12;
		result.M22 = mat.M22;
		result.M23 = mat.M32;
		result.M24 = mat.M42;
		result.M31 = mat.M13;
		result.M32 = mat.M23;
		result.M33 = mat.M33;
		result.M34 = mat.M43;
		result.M41 = mat.M14;
		result.M42 = mat.M24;
		result.M43 = mat.M34;
		result.M44 = mat.M44;
		return result;
	}

	void Matrix::Transpose( Matrix% mat, [Out] Matrix% result )
	{
		Matrix r;
		r.M11 = mat.M11;
		r.M12 = mat.M21;
		r.M13 = mat.M31;
		r.M14 = mat.M41;
		r.M21 = mat.M12;
		r.M22 = mat.M22;
		r.M23 = mat.M32;
		r.M24 = mat.M42;
		r.M31 = mat.M13;
		r.M32 = mat.M23;
		r.M33 = mat.M33;
		r.M34 = mat.M43;
		r.M41 = mat.M14;
		r.M42 = mat.M24;
		r.M43 = mat.M34;
		r.M44 = mat.M44;

		result = r;
	}

	Matrix Matrix::operator * ( Matrix left, Matrix right )
	{
		Matrix result;
		result.M11 = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31) + (left.M14 * right.M41);
		result.M12 = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32) + (left.M14 * right.M42);
		result.M13 = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33) + (left.M14 * right.M43);
		result.M14 = (left.M11 * right.M14) + (left.M12 * right.M24) + (left.M13 * right.M34) + (left.M14 * right.M44);
		result.M21 = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31) + (left.M24 * right.M41);
		result.M22 = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32) + (left.M24 * right.M42);
		result.M23 = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33) + (left.M24 * right.M43);
		result.M24 = (left.M21 * right.M14) + (left.M22 * right.M24) + (left.M23 * right.M34) + (left.M24 * right.M44);
		result.M31 = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31) + (left.M34 * right.M41);
		result.M32 = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32) + (left.M34 * right.M42);
		result.M33 = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33) + (left.M34 * right.M43);
		result.M34 = (left.M31 * right.M14) + (left.M32 * right.M24) + (left.M33 * right.M34) + (left.M34 * right.M44);
		result.M41 = (left.M41 * right.M11) + (left.M42 * right.M21) + (left.M43 * right.M31) + (left.M44 * right.M41);
		result.M42 = (left.M41 * right.M12) + (left.M42 * right.M22) + (left.M43 * right.M32) + (left.M44 * right.M42);
		result.M43 = (left.M41 * right.M13) + (left.M42 * right.M23) + (left.M43 * right.M33) + (left.M44 * right.M43);
		result.M44 = (left.M41 * right.M14) + (left.M42 * right.M24) + (left.M43 * right.M34) + (left.M44 * right.M44);
		return result;
	}

	Matrix Matrix::operator * ( Matrix left, float right )
	{
		Matrix result;
		result.M11 = left.M11 * right;
		result.M12 = left.M12 * right;
		result.M13 = left.M13 * right;
		result.M14 = left.M14 * right;
		result.M21 = left.M21 * right;
		result.M22 = left.M22 * right;
		result.M23 = left.M23 * right;
		result.M24 = left.M24 * right;
		result.M31 = left.M31 * right;
		result.M32 = left.M32 * right;
		result.M33 = left.M33 * right;
		result.M34 = left.M34 * right;
		result.M41 = left.M41 * right;
		result.M42 = left.M42 * right;
		result.M43 = left.M43 * right;
		result.M44 = left.M44 * right;
		return result;
	}

	Matrix Matrix::operator * ( float right, Matrix left )
	{
		return left * right;
	}

	Matrix Matrix::operator / ( Matrix left, Matrix right )
	{
		Matrix result;
		result.M11 = left.M11 / right.M11;
		result.M12 = left.M12 / right.M12;
		result.M13 = left.M13 / right.M13;
		result.M14 = left.M14 / right.M14;
		result.M21 = left.M21 / right.M21;
		result.M22 = left.M22 / right.M22;
		result.M23 = left.M23 / right.M23;
		result.M24 = left.M24 / right.M24;
		result.M31 = left.M31 / right.M31;
		result.M32 = left.M32 / right.M32;
		result.M33 = left.M33 / right.M33;
		result.M34 = left.M34 / right.M34;
		result.M41 = left.M41 / right.M41;
		result.M42 = left.M42 / right.M42;
		result.M43 = left.M43 / right.M43;
		result.M44 = left.M44 / right.M44;
		return result;
	}

	Matrix Matrix::operator / ( Matrix left, float right )
	{
		Matrix result;
		result.M11 = left.M11 / right;
		result.M12 = left.M12 / right;
		result.M13 = left.M13 / right;
		result.M14 = left.M14 / right;
		result.M21 = left.M21 / right;
		result.M22 = left.M22 / right;
		result.M23 = left.M23 / right;
		result.M24 = left.M24 / right;
		result.M31 = left.M31 / right;
		result.M32 = left.M32 / right;
		result.M33 = left.M33 / right;
		result.M34 = left.M34 / right;
		result.M41 = left.M41 / right;
		result.M42 = left.M42 / right;
		result.M43 = left.M43 / right;
		result.M44 = left.M44 / right;
		return result;
	}

	Matrix Matrix::operator + ( Matrix left, Matrix right )
	{
		Matrix result;
		result.M11 = left.M11 + right.M11;
		result.M12 = left.M12 + right.M12;
		result.M13 = left.M13 + right.M13;
		result.M14 = left.M14 + right.M14;
		result.M21 = left.M21 + right.M21;
		result.M22 = left.M22 + right.M22;
		result.M23 = left.M23 + right.M23;
		result.M24 = left.M24 + right.M24;
		result.M31 = left.M31 + right.M31;
		result.M32 = left.M32 + right.M32;
		result.M33 = left.M33 + right.M33;
		result.M34 = left.M34 + right.M34;
		result.M41 = left.M41 + right.M41;
		result.M42 = left.M42 + right.M42;
		result.M43 = left.M43 + right.M43;
		result.M44 = left.M44 + right.M44;
		return result;
	}

	Matrix Matrix::operator - ( Matrix left, Matrix right )
	{
		Matrix result;
		result.M11 = left.M11 - right.M11;
		result.M12 = left.M12 - right.M12;
		result.M13 = left.M13 - right.M13;
		result.M14 = left.M14 - right.M14;
		result.M21 = left.M21 - right.M21;
		result.M22 = left.M22 - right.M22;
		result.M23 = left.M23 - right.M23;
		result.M24 = left.M24 - right.M24;
		result.M31 = left.M31 - right.M31;
		result.M32 = left.M32 - right.M32;
		result.M33 = left.M33 - right.M33;
		result.M34 = left.M34 - right.M34;
		result.M41 = left.M41 - right.M41;
		result.M42 = left.M42 - right.M42;
		result.M43 = left.M43 - right.M43;
		result.M44 = left.M44 - right.M44;
		return result;
	}

	Matrix Matrix::operator - ( Matrix matrix )
	{
		Matrix result;
		result.M11 = -matrix.M11;
		result.M12 = -matrix.M12;
		result.M13 = -matrix.M13;
		result.M14 = -matrix.M14;
		result.M21 = -matrix.M21;
		result.M22 = -matrix.M22;
		result.M23 = -matrix.M23;
		result.M24 = -matrix.M24;
		result.M31 = -matrix.M31;
		result.M32 = -matrix.M32;
		result.M33 = -matrix.M33;
		result.M34 = -matrix.M34;
		result.M41 = -matrix.M41;
		result.M42 = -matrix.M42;
		result.M43 = -matrix.M43;
		result.M44 = -matrix.M44;
		return result;
	}

	bool Matrix::operator == ( Matrix left, Matrix right )
	{
		return Matrix::Equals( left, right );
	}

	bool Matrix::operator != ( Matrix left, Matrix right )
	{
		return !Matrix::Equals( left, right );
	}

	String^ Matrix::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "[[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]]",
			M11.ToString(CultureInfo::CurrentCulture), M12.ToString(CultureInfo::CurrentCulture), M13.ToString(CultureInfo::CurrentCulture), M14.ToString(CultureInfo::CurrentCulture),
			M21.ToString(CultureInfo::CurrentCulture), M22.ToString(CultureInfo::CurrentCulture), M23.ToString(CultureInfo::CurrentCulture), M24.ToString(CultureInfo::CurrentCulture),
			M31.ToString(CultureInfo::CurrentCulture), M32.ToString(CultureInfo::CurrentCulture), M33.ToString(CultureInfo::CurrentCulture), M34.ToString(CultureInfo::CurrentCulture),
			M41.ToString(CultureInfo::CurrentCulture), M42.ToString(CultureInfo::CurrentCulture), M43.ToString(CultureInfo::CurrentCulture), M44.ToString(CultureInfo::CurrentCulture) );
	}

	int Matrix::GetHashCode()
	{
		return M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() + M14.GetHashCode() +
			   M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() + M24.GetHashCode() +
			   M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode() + M34.GetHashCode() +
			   M41.GetHashCode() + M42.GetHashCode() + M43.GetHashCode() + M44.GetHashCode();
	}

	bool Matrix::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Matrix>( value ) );
	}

	bool Matrix::Equals( Matrix value )
	{
		return ( M11 == value.M11 && M12 == value.M12 && M13 == value.M13 && M14 == value.M14 &&
				 M21 == value.M21 && M22 == value.M22 && M23 == value.M23 && M24 == value.M24 &&
				 M31 == value.M31 && M32 == value.M32 && M33 == value.M33 && M34 == value.M34 &&
				 M41 == value.M41 && M42 == value.M42 && M43 == value.M43 && M44 == value.M44 );
	}

	bool Matrix::Equals( Matrix% value1, Matrix% value2 )
	{
		return ( value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 &&
				 value1.M21 == value2.M21 && value1.M22 == value2.M22 && value1.M23 == value2.M23 && value1.M24 == value2.M24 &&
				 value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M33 == value2.M33 && value1.M34 == value2.M34 &&
				 value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43 && value1.M44 == value2.M44 );
	}
}
