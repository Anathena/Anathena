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

#include "Direct3D11Exception.h"

#include "DeviceContext11.h"
#include "EffectVariable11.h"
#include "EffectTechnique11.h"
#include "EffectGroupDescription11.h"
#include "EffectGroup11.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectGroup::EffectGroup( ID3DX11EffectGroup* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
			
		InternalPointer = pointer;
	}
	
	EffectGroup::EffectGroup( IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
			
		InternalPointer = reinterpret_cast<ID3DX11EffectGroup*>( pointer.ToPointer() );
	}
	
	EffectGroupDescription EffectGroup::Description::get()
	{
		D3DX11_GROUP_DESC nativeDescription;
		if (RECORD_D3D11( InternalPointer->GetDesc( &nativeDescription ) ).IsFailure)
			return EffectGroupDescription();

		return EffectGroupDescription( nativeDescription );
	}
	
	bool EffectGroup::IsValid::get()
	{
		return InternalPointer->IsValid() ? true : false;
	}
	
	EffectVariable^ EffectGroup::GetAnnotationByIndex( int index )
	{
		ID3DX11EffectVariable* variable = InternalPointer->GetAnnotationByIndex( index );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectGroup::GetAnnotationByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		ID3DX11EffectVariable* variable = InternalPointer->GetAnnotationByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}

	EffectTechnique^ EffectGroup::GetTechniqueByIndex( int index )
	{
		ID3DX11EffectTechnique* technique = InternalPointer->GetTechniqueByIndex( index );
		if( technique == 0 )
			return nullptr;
			
		return gcnew EffectTechnique( technique );
	}

	EffectTechnique^ EffectGroup::GetTechniqueByName( System::String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectTechnique* technique = InternalPointer->GetTechniqueByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( technique == 0 )
			return nullptr;
			
		return gcnew EffectTechnique( technique );
	}
}
}
