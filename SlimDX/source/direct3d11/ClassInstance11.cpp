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
#include <d3d11.h>

#include "../stack_array.h"

#include "Direct3D11Exception.h"

#include "ClassLinkage11.h"
#include "ClassInstance11.h"
#include "ClassInstanceDescription11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{
	ClassInstance::ClassInstance( ClassLinkage^ linkage, System::String^ typeName, int constantBufferOffset, int constantVectorOffset, int textureOffset, int samplerOffset )
	{
		ID3D11ClassInstance *instance = NULL;
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( typeName );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = linkage->InternalPointer->CreateClassInstance( reinterpret_cast<LPCSTR>( pinnedName ), constantBufferOffset, constantVectorOffset, textureOffset, samplerOffset, &instance );
		if( RECORD_D3D11( hr ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );

		Construct( instance );
	}

	ClassLinkage^ ClassInstance::Linkage::get()
	{
		ID3D11ClassLinkage *linkage;

		InternalPointer->GetClassLinkage( &linkage );
		return ClassLinkage::FromPointer( linkage );
	}

	ClassInstanceDescription ClassInstance::Description::get()
	{
		D3D11_CLASS_INSTANCE_DESC desc;
		InternalPointer->GetDesc( &desc );

		return ClassInstanceDescription( desc );
	}

	String^ ClassInstance::InstanceName::get()
	{
		SIZE_T length = 0;
		InternalPointer->GetInstanceName( NULL, &length );

		if( length <= 0 )
			return nullptr;

		stack_array<char> name = stackalloc( char, length );
		InternalPointer->GetInstanceName( &name[0], &length );

		return gcnew String( &name[0] );
	}

	String^ ClassInstance::TypeName::get()
	{
		SIZE_T length = 0;
		InternalPointer->GetTypeName( NULL, &length );

		if( length <= 0 )
			return nullptr;

		stack_array<char> name = stackalloc( char, length );
		InternalPointer->GetTypeName( &name[0], &length );

		return gcnew String( &name[0] );
	}
}
}