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
#include <vcclr.h>

#include "Direct3D11Exception.h"

#include "EffectType11.h"
#include "EffectTypeDescription11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectType::EffectType( ID3DX11EffectType* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = pointer;
	}
	
	EffectType::EffectType( System::IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = reinterpret_cast<ID3DX11EffectType*>( pointer.ToPointer() );
	}

	EffectTypeDescription EffectType::Description::get()
	{
		D3DX11_EFFECT_TYPE_DESC nativeDescription;
		if (RECORD_D3D11( m_Pointer->GetDesc( &nativeDescription ) ).IsFailure)
			return EffectTypeDescription();

		return EffectTypeDescription( nativeDescription );
	}

	bool EffectType::IsValid::get()
	{
		return m_Pointer->IsValid() ? true : false;
	}
	
	System::String^ EffectType::GetMemberName( int index )
	{
		LPCSTR result = m_Pointer->GetMemberName( index );
		if ( result == 0 )
			return nullptr;

		return gcnew String(result);
	}

	System::String^ EffectType::GetMemberSemantic( int index )
	{
		LPCSTR result = m_Pointer->GetMemberSemantic( index );
		if ( result == 0 )
			return nullptr;

		return gcnew String(result);
	}

	EffectType^ EffectType::GetMemberTypeByIndex( int index )
	{
		ID3DX11EffectType* type = m_Pointer->GetMemberTypeByIndex( index );
		if( type == 0 )
			return nullptr;

		return gcnew EffectType( type );
	}
	
	EffectType^ EffectType::GetMemberTypeByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectType* type = m_Pointer->GetMemberTypeByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( type == 0 )
			return nullptr;

		return gcnew EffectType( type );
	}
	
	EffectType^ EffectType::GetMemberTypeBySemantic( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectType* type = m_Pointer->GetMemberTypeBySemantic( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( type == 0 )
			return nullptr;

		return gcnew EffectType( type );
	}
}
}