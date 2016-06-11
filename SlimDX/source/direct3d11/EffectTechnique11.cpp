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

#include "EffectPass11.h"
#include "EffectTechnique11.h"
#include "EffectTechniqueDescription11.h"
#include "StateBlockMask11.h"
#include "EffectVariable11.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectTechnique::EffectTechnique( ID3DX11EffectTechnique* pointer )
	{
		m_Pointer = pointer;
	}
	
	EffectTechnique::EffectTechnique( IntPtr pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectTechnique*>( pointer.ToPointer() );
	}
	
	EffectTechniqueDescription EffectTechnique::Description::get()
	{
		D3DX11_TECHNIQUE_DESC nativeDescription;
		if (RECORD_D3D11( m_Pointer->GetDesc( &nativeDescription ) ).IsFailure)
			return EffectTechniqueDescription();

		return EffectTechniqueDescription( nativeDescription );
	}
	
	bool EffectTechnique::IsValid::get()
	{
		return m_Pointer->IsValid() ? true : false;
	}
	
	EffectVariable^ EffectTechnique::GetAnnotationByIndex( int index )
	{
		ID3DX11EffectVariable* variable = m_Pointer->GetAnnotationByIndex( index );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectTechnique::GetAnnotationByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectVariable* variable = m_Pointer->GetAnnotationByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectPass^ EffectTechnique::GetPassByIndex( int index )
	{
		ID3DX11EffectPass* pass = m_Pointer->GetPassByIndex( index );
		if( pass == 0 )
			return nullptr;

		return gcnew EffectPass( pass );
	}
	
	EffectPass^ EffectTechnique::GetPassByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectPass* pass = m_Pointer->GetPassByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( pass == 0 )
			return nullptr;

		return gcnew EffectPass( pass );
	}

	StateBlockMask^ EffectTechnique::ComputeStateBlockMask()
	{
		D3DX11_STATE_BLOCK_MASK mask;

		HRESULT hr = m_Pointer->ComputeStateBlockMask(&mask);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		return gcnew StateBlockMask(mask);
	}
}
}
