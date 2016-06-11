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

#include "../DataStream.h"
#include "../stack_array.h"

#include "Direct3D10Exception.h"

#include "EffectConstantBuffer.h"
#include "EffectMatrixVariable.h"
#include "EffectResourceVariable.h"
#include "EffectScalarVariable.h"
#include "EffectShaderVariable.h"
#include "EffectStringVariable.h"
#include "EffectVariable.h"
#include "EffectVariableDescription.h"
#include "EffectVectorVariable.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	EffectVariable::EffectVariable( ID3D10EffectVariable* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = pointer;
	}
	
	EffectVariable::EffectVariable( System::IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = reinterpret_cast<ID3D10EffectVariable*>( pointer.ToPointer() );
	}

	EffectVariableDescription EffectVariable::Description::get()
	{
		D3D10_EFFECT_VARIABLE_DESC nativeDescription;
		if ( RECORD_D3D10( m_Pointer->GetDesc( &nativeDescription ) ).IsFailure )
			return EffectVariableDescription();

		return EffectVariableDescription( nativeDescription );
	}

	bool EffectVariable::IsValid::get()
	{
		return m_Pointer->IsValid() ? true : false;
	}
	
	EffectVariable^ EffectVariable::GetAnnotationByIndex( int index )
	{
		ID3D10EffectVariable* variable = m_Pointer->GetAnnotationByIndex( index );
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetAnnotationByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3D10EffectVariable* variable = m_Pointer->GetAnnotationByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetElement( int index )
	{
		ID3D10EffectVariable* variable = m_Pointer->GetElement( index );
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetMemberByIndex( int index )
	{
		ID3D10EffectVariable* variable = m_Pointer->GetMemberByIndex( index );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetMemberByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3D10EffectVariable* variable = m_Pointer->GetMemberByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetMemberBySemantic( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3D10EffectVariable* variable = m_Pointer->GetMemberBySemantic( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectConstantBuffer^ EffectVariable::AsConstantBuffer()
	{
		ID3D10EffectConstantBuffer* variable = m_Pointer->AsConstantBuffer();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectConstantBuffer( variable );
	}
	
	EffectMatrixVariable^ EffectVariable::AsMatrix()
	{
		ID3D10EffectMatrixVariable* variable = m_Pointer->AsMatrix();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectMatrixVariable( variable );
	}
	
	EffectResourceVariable^ EffectVariable::AsResource()
	{
		ID3D10EffectShaderResourceVariable* variable = m_Pointer->AsShaderResource();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectResourceVariable( variable );
	}
	
	EffectShaderVariable^ EffectVariable::AsShader()
	{
		ID3D10EffectShaderVariable* variable = m_Pointer->AsShader();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectShaderVariable( variable );
	}
	
	EffectScalarVariable^ EffectVariable::AsScalar()
	{
		ID3D10EffectScalarVariable* variable = m_Pointer->AsScalar();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectScalarVariable( variable );
	}
	
	EffectVectorVariable^ EffectVariable::AsVector()
	{
		ID3D10EffectVectorVariable* variable = m_Pointer->AsVector();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectVectorVariable( variable );
	}

	EffectStringVariable^ EffectVariable::AsString()
	{
		ID3D10EffectStringVariable* variable = m_Pointer->AsString();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectStringVariable( variable );
	}

	Result EffectVariable::SetRawValue(DataStream^ data, int count)
	{
		HRESULT hr = m_Pointer->SetRawValue(data->PositionPointer, 0, count);
		return RECORD_D3D10(hr);
	}

	DataStream^ EffectVariable::GetRawValue(int count)
	{
		stack_array<char> data = stackalloc(char, count);

		HRESULT hr = m_Pointer->GetRawValue(&data[0], 0, count);
		if (RECORD_D3D10(hr).IsFailure)
			return nullptr;

		return gcnew DataStream(&data[0], count, true, true, true);
	}
}
}
