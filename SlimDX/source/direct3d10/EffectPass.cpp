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

#include <d3d10.h>
#include <d3dx10.h>
#include <vcclr.h>

#include "Direct3D10Exception.h"

#include "EffectPass.h"
#include "EffectPassDescription.h"
#include "EffectPassShaderDescription.h"
#include "EffectVariable.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
namespace Direct3D10
{ 
	EffectPass::EffectPass( ID3D10EffectPass* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
			
		m_Pointer = pointer;
	}
	
	EffectPass::EffectPass( IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
			
		m_Pointer = reinterpret_cast<ID3D10EffectPass*>( pointer.ToPointer() );
	}
	
	EffectPassDescription EffectPass::Description::get()
	{
		D3D10_PASS_DESC nativeDescription;
		if ( RECORD_D3D10( m_Pointer->GetDesc( &nativeDescription ) ).IsFailure )
			return EffectPassDescription();

		return EffectPassDescription( nativeDescription );
	}
	
	bool EffectPass::IsValid::get()
	{
		return m_Pointer->IsValid() ? true : false;
	}
	
	EffectPassShaderDescription EffectPass::GeometryShaderDescription::get()
	{
		D3D10_PASS_SHADER_DESC description;
		
		if( RECORD_D3D10( m_Pointer->GetGeometryShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}
	
	EffectPassShaderDescription EffectPass::VertexShaderDescription::get()
	{
		D3D10_PASS_SHADER_DESC description;
		
		if( RECORD_D3D10( m_Pointer->GetVertexShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}
	
	EffectPassShaderDescription EffectPass::PixelShaderDescription::get()
	{
		D3D10_PASS_SHADER_DESC description;
		
		if( RECORD_D3D10( m_Pointer->GetPixelShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}
	
	EffectVariable^ EffectPass::GetAnnotationByIndex( int index )
	{
		ID3D10EffectVariable* variable = m_Pointer->GetAnnotationByIndex( index );
		if( variable == 0 )
			return nullptr;
		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectPass::GetAnnotationByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3D10EffectVariable* variable = m_Pointer->GetAnnotationByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;
		return gcnew EffectVariable( variable );
	}
	
	Result EffectPass::Apply()
	{
		return RECORD_D3D10( m_Pointer->Apply( 0 ) );
	}
}
}
