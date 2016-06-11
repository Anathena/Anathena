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
#include "EffectPass11.h"
#include "EffectPassDescription11.h"
#include "EffectPassShaderDescription11.h"
#include "StateBlockMask11.h"
#include "EffectVariable11.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectPass::EffectPass( ID3DX11EffectPass* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
			
		m_Pointer = pointer;
	}
	
	EffectPass::EffectPass( IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
			
		m_Pointer = reinterpret_cast<ID3DX11EffectPass*>( pointer.ToPointer() );
	}
	
	EffectPassDescription EffectPass::Description::get()
	{
		D3DX11_PASS_DESC nativeDescription;
		if (RECORD_D3D11( m_Pointer->GetDesc( &nativeDescription ) ).IsFailure)
			return EffectPassDescription();

		return EffectPassDescription( nativeDescription );
	}
	
	bool EffectPass::IsValid::get()
	{
		return m_Pointer->IsValid() ? true : false;
	}
	
	EffectPassShaderDescription EffectPass::GeometryShaderDescription::get()
	{
		D3DX11_PASS_SHADER_DESC description;
		
		if( RECORD_D3D11( m_Pointer->GetGeometryShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}
	
	EffectPassShaderDescription EffectPass::VertexShaderDescription::get()
	{
		D3DX11_PASS_SHADER_DESC description;
		
		if( RECORD_D3D11( m_Pointer->GetVertexShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}
	
	EffectPassShaderDescription EffectPass::PixelShaderDescription::get()
	{
		D3DX11_PASS_SHADER_DESC description;
		
		if( RECORD_D3D11( m_Pointer->GetPixelShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}

	EffectPassShaderDescription EffectPass::HullShaderDescription::get()
	{
		D3DX11_PASS_SHADER_DESC description;
		
		if( RECORD_D3D11( m_Pointer->GetHullShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}

	EffectPassShaderDescription EffectPass::DomainShaderDescription::get()
	{
		D3DX11_PASS_SHADER_DESC description;
		
		if( RECORD_D3D11( m_Pointer->GetDomainShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}

	EffectPassShaderDescription EffectPass::ComputeShaderDescription::get()
	{
		D3DX11_PASS_SHADER_DESC description;
		
		if( RECORD_D3D11( m_Pointer->GetComputeShaderDesc( &description ) ).IsFailure )
			return EffectPassShaderDescription();
		else
			return EffectPassShaderDescription( description );
	}
	
	EffectVariable^ EffectPass::GetAnnotationByIndex( int index )
	{
		ID3DX11EffectVariable* variable = m_Pointer->GetAnnotationByIndex( index );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectPass::GetAnnotationByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectVariable* variable = m_Pointer->GetAnnotationByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	Result EffectPass::Apply( DeviceContext^ context )
	{
		return RECORD_D3D11( m_Pointer->Apply( 0, context->InternalPointer ) );
	}

	StateBlockMask^ EffectPass::ComputeStateBlockMask()
	{
		D3DX11_STATE_BLOCK_MASK mask;

		HRESULT hr = m_Pointer->ComputeStateBlockMask(&mask);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		return gcnew StateBlockMask(mask);
	}
}
}
