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

#include "D3DCompilerException.h"

#include "ShaderReflectionTypeDC.h"
#include "ShaderTypeDescriptionDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{ 
	ShaderReflectionType::ShaderReflectionType( ID3D11ShaderReflectionType* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = pointer;

		Init();
	}
	
	ShaderReflectionType::ShaderReflectionType( System::IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = reinterpret_cast<ID3D11ShaderReflectionType*>( pointer.ToPointer() );

		Init();
	}

	void ShaderReflectionType::Init()
	{
		ID3D11ShaderReflectionType* type = m_Pointer->GetBaseClass();
		if (type != NULL)
			baseClass = gcnew ShaderReflectionType( type );

		type = m_Pointer->GetSubType();
		if (type != NULL)
			subType = gcnew ShaderReflectionType( type );
	}

	ShaderTypeDescription ShaderReflectionType::Description::get()
	{
		D3D11_SHADER_TYPE_DESC nativeDescription;
		HRESULT hr = m_Pointer->GetDesc( &nativeDescription );
		RECORD_D3DC( hr );

		return ShaderTypeDescription( nativeDescription );
	}

	ShaderReflectionType^ ShaderReflectionType::GetInterface( int index )
	{
		ID3D11ShaderReflectionType* type = m_Pointer->GetInterfaceByIndex( index );
		if( type == 0 )
			return nullptr;
		return gcnew ShaderReflectionType( type );
	}

	ShaderReflectionType^ ShaderReflectionType::GetMemberType( int index )
	{
		ID3D11ShaderReflectionType* type = m_Pointer->GetMemberTypeByIndex( index );
		if( type == 0 )
			return nullptr;

		return gcnew ShaderReflectionType( type );
	}

	ShaderReflectionType^ ShaderReflectionType::GetMemberType( System::String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		ID3D11ShaderReflectionType* type = m_Pointer->GetMemberTypeByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( type == 0 )
			return nullptr;

		return gcnew ShaderReflectionType( type );
	}

	System::String^ ShaderReflectionType::GetMemberTypeName( int index )
	{
		LPCSTR result = m_Pointer->GetMemberTypeName( index );
		if ( result == 0 )
			return nullptr;

		return gcnew String(result);
	}

	bool ShaderReflectionType::ImplementsInterface( ShaderReflectionType^ base )
	{
		return m_Pointer->ImplementsInterface( base->m_Pointer ) != S_FALSE;
	}

	bool ShaderReflectionType::IsOfType( ShaderReflectionType^ type )
	{
		return m_Pointer->IsOfType( type->m_Pointer ) != S_FALSE;
	}

	bool ShaderReflectionType::IsTypeEqual( ShaderReflectionType^ type )
	{
		return m_Pointer->IsEqual( type->m_Pointer ) != S_FALSE;
	}
}
}