#include "stdafx.h"
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

#include <d3d11.h>
#include <d3dx11effect.h>

#include "Direct3D11Exception.h"
#include "EffectScalarVariable11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectScalarVariable::EffectScalarVariable( ID3DX11EffectScalarVariable* pointer )
		: EffectVariable( pointer )
	{
		m_Pointer = pointer;
	}

	EffectScalarVariable::EffectScalarVariable( IntPtr pointer )
		: EffectVariable( pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectScalarVariable*>( pointer.ToPointer() );
	}

	Result EffectScalarVariable::Set( bool value )
	{
		return RECORD_D3D11( m_Pointer->SetBool( value ) );
	}

	Result EffectScalarVariable::Set( array<bool>^ values )
	{
		UINT offset = 0L;

		array<BOOL>^ boolData = Array::ConvertAll<bool, int>(values, gcnew Converter<bool, int>(Convert::ToInt32));
		pin_ptr<BOOL> pinned_values = &boolData[0];
		return RECORD_D3D11( m_Pointer->SetBoolArray(pinned_values, offset, values->Length));
	}

	Result EffectScalarVariable::Set( float value )
	{
		return RECORD_D3D11( m_Pointer->SetFloat( value ) );
	}

	Result EffectScalarVariable::Set( array<float>^ values )
	{
		UINT offset = 0L;
		pin_ptr<float> pinned_values = &values[0];
		return RECORD_D3D11( m_Pointer->SetFloatArray( pinned_values, offset, values->Length ) );
	}

	Result EffectScalarVariable::Set( int value )
	{
		return RECORD_D3D11( m_Pointer->SetInt( value ) );
	}

	Result EffectScalarVariable::Set( array<int>^ values )
	{
		UINT offset = 0L;
		pin_ptr<int> pinned_values = &values[0];
		return RECORD_D3D11( m_Pointer->SetIntArray( pinned_values, offset, values->Length ) );
	}

	bool EffectScalarVariable::GetBool()
	{
		BOOL value = FALSE;
		RECORD_D3D11( m_Pointer->GetBool( &value ) );
		return value > 0;
	}

	array<bool>^ EffectScalarVariable::GetBoolArray( int count )
	{
		if( count < 0 )
			throw gcnew ArgumentOutOfRangeException( "count" );

		array<BOOL>^ values = gcnew array<BOOL>( count );
		pin_ptr<BOOL> pinned_values = &values[0];
		HRESULT hr = m_Pointer->GetIntArray( pinned_values, 0, static_cast<UINT>( count ) );
		if( RECORD_D3D11( hr ).IsFailure )
			return nullptr;

        // now we go from BOOL to bool
        return Array::ConvertAll<int, bool>(values, gcnew Converter<int, bool>(Convert::ToBoolean));
	}

	int EffectScalarVariable::GetInt()
	{
		int value = 0;
		RECORD_D3D11( m_Pointer->GetInt( &value ) );
		return value;
	}

	array<int>^ EffectScalarVariable::GetIntArray( int count )
	{
		if( count < 0 )
			throw gcnew ArgumentOutOfRangeException( "count" );

		array<int>^ values = gcnew array<int>( count );
		pin_ptr<int> pinned_values = &values[0];
		HRESULT hr = m_Pointer->GetIntArray( pinned_values, 0, static_cast<UINT>( count ) );
		if( RECORD_D3D11( hr ).IsFailure )
			return nullptr;

		return values;
	}

	float EffectScalarVariable::GetFloat()
	{
		float value = 0;
		RECORD_D3D11( m_Pointer->GetFloat( &value ) );
		return value;
	}

	array<float>^ EffectScalarVariable::GetFloatArray( int count )
	{
		if( count < 0 )
			throw gcnew ArgumentOutOfRangeException( "count" );

		array<float>^ values = gcnew array<float>( count );
		pin_ptr<float> pinned_values = &values[0];
		HRESULT hr = m_Pointer->GetFloatArray( pinned_values, 0, static_cast<UINT>( count ) );
		if( RECORD_D3D11( hr ).IsFailure )
			return nullptr;

		return values;
	}
}
}
