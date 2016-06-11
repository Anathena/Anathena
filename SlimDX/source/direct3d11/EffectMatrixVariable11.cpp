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

#include "Direct3D11Exception.h"
#include "EffectMatrixVariable11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectMatrixVariable::EffectMatrixVariable( ID3DX11EffectMatrixVariable* pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = pointer;
	}
	
	EffectMatrixVariable::EffectMatrixVariable( IntPtr pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectMatrixVariable*>( pointer.ToPointer() );
	}

	Matrix EffectMatrixVariable::GetMatrix()
	{
		Matrix result;
		GetMatrix(result);

		return result;
	}

	Result EffectMatrixVariable::GetMatrix([Out] Matrix% result)
	{
		Matrix matrix;

		HRESULT hr = m_Pointer->GetMatrix(reinterpret_cast<float*>(&matrix));
		if (RECORD_D3D11(hr).IsFailure)
			result = Matrix::Identity;
		else
			result = matrix;

		return Result::Last;
	}

	Result EffectMatrixVariable::GetMatrixArray(array<Matrix>^ matrices)
	{
		return GetMatrixArray(matrices, 0, matrices->Length);
	}

	Result EffectMatrixVariable::GetMatrixArray(array<Matrix>^ matrices, int offset, int count)
	{
		Utilities::CheckArrayBounds(matrices, offset, count);
		pin_ptr<Matrix> pinnedMatrices = &matrices[offset];

		HRESULT hr = m_Pointer->GetMatrixArray(reinterpret_cast<float*>(pinnedMatrices), 0, count);
		return RECORD_D3D11(hr);
	}
	
	Matrix EffectMatrixVariable::GetMatrixTranspose()
	{
		Matrix result;
		GetMatrixTranspose(result);

		return result;
	}

	Result EffectMatrixVariable::GetMatrixTranspose([Out] Matrix% result)
	{
		Matrix matrix;

		HRESULT hr = m_Pointer->GetMatrixTranspose(reinterpret_cast<float*>(&matrix));
		if (RECORD_D3D11(hr).IsFailure)
			result = Matrix::Identity;
		else
			result = matrix;

		return Result::Last;
	}

	Result EffectMatrixVariable::GetMatrixTransposeArray(array<Matrix>^ matrices)
	{
		return GetMatrixTransposeArray(matrices, 0, matrices->Length);
	}

	Result EffectMatrixVariable::GetMatrixTransposeArray(array<Matrix>^ matrices, int offset, int count)
	{
		Utilities::CheckArrayBounds(matrices, offset, count);
		pin_ptr<Matrix> pinnedMatrices = &matrices[offset];

		HRESULT hr = m_Pointer->GetMatrixTransposeArray(reinterpret_cast<float*>(pinnedMatrices), 0, count);
		return RECORD_D3D11(hr);
	}

	Result EffectMatrixVariable::SetMatrix( Matrix matrix )
	{
		return RECORD_D3D11( m_Pointer->SetMatrix( reinterpret_cast<float*>( &matrix ) ) );
	}
	
	Result EffectMatrixVariable::SetMatrixArray( array<Matrix>^ matrices )
	{
		return SetMatrixArray(matrices, 0, matrices->Length);
	}

	Result EffectMatrixVariable::SetMatrixArray( array<Matrix>^ matrices, int offset, int count )
	{
		Utilities::CheckArrayBounds(matrices, offset, count);

		pin_ptr<Matrix> pinnedMatrices = &matrices[offset];
		return RECORD_D3D11( m_Pointer->SetMatrixArray( reinterpret_cast<float*>( pinnedMatrices ), 0, count ) );
	}

	Result EffectMatrixVariable::SetMatrixTranspose( Matrix matrix )
	{
		return RECORD_D3D11( m_Pointer->SetMatrixTranspose( reinterpret_cast<float*>( &matrix ) ) );
	}
	
	Result EffectMatrixVariable::SetMatrixTransposeArray( array<Matrix>^ matrices )
	{
		return SetMatrixTransposeArray(matrices, 0, matrices->Length);
	}

	Result EffectMatrixVariable::SetMatrixTransposeArray( array<Matrix>^ matrices, int offset, int count )
	{
		Utilities::CheckArrayBounds(matrices, offset, count);

		pin_ptr<Matrix> pinnedMatrices = &matrices[offset];
		return RECORD_D3D11( m_Pointer->SetMatrixTransposeArray( reinterpret_cast<float*>( pinnedMatrices ), 0, count ) );
	}
}
}
