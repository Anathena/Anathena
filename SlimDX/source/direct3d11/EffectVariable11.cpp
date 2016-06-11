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

#include "Direct3D11Exception.h"

#include "EffectConstantBuffer11.h"
#include "EffectMatrixVariable11.h"
#include "EffectResourceVariable11.h"
#include "EffectScalarVariable11.h"
#include "EffectShaderVariable11.h"
#include "EffectStringVariable11.h"
#include "EffectBlendVariable11.h"
#include "EffectClassInstanceVariable11.h"
#include "EffectDepthStencilVariable11.h"
#include "EffectDepthStencilViewVariable11.h"
#include "EffectInterfaceVariable11.h"
#include "EffectVariable11.h"
#include "EffectVariableDescription11.h"
#include "EffectVectorVariable11.h"
#include "EffectUnorderedAccessViewVariable11.h"
#include "EffectRasterizerVariable11.h"
#include "EffectRenderTargetViewVariable11.h"
#include "EffectSamplerVariable11.h"
#include "EffectType11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectVariable::EffectVariable( ID3DX11EffectVariable* pointer )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = pointer;
	}
	
	EffectVariable::EffectVariable( System::IntPtr pointer )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );
		m_Pointer = reinterpret_cast<ID3DX11EffectVariable*>( pointer.ToPointer() );
	}

	EffectVariableDescription EffectVariable::Description::get()
	{
		D3DX11_EFFECT_VARIABLE_DESC nativeDescription;
		if (RECORD_D3D11( m_Pointer->GetDesc( &nativeDescription ) ).IsFailure)
			return EffectVariableDescription();

		return EffectVariableDescription( nativeDescription );
	}

	EffectConstantBuffer^ EffectVariable::ParentConstantBuffer::get()
	{
		ID3DX11EffectConstantBuffer* variable = m_Pointer->GetParentConstantBuffer();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectConstantBuffer( variable );
	}

	bool EffectVariable::IsValid::get()
	{
		return m_Pointer->IsValid() ? true : false;
	}
	
	EffectVariable^ EffectVariable::GetAnnotationByIndex( int index )
	{
		ID3DX11EffectVariable* variable = m_Pointer->GetAnnotationByIndex( index );
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetAnnotationByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectVariable* variable = m_Pointer->GetAnnotationByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetElement( int index )
	{
		ID3DX11EffectVariable* variable = m_Pointer->GetElement( index );
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetMemberByIndex( int index )
	{
		ID3DX11EffectVariable* variable = m_Pointer->GetMemberByIndex( index );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetMemberByName( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectVariable* variable = m_Pointer->GetMemberByName( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}
	
	EffectVariable^ EffectVariable::GetMemberBySemantic( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];
		ID3DX11EffectVariable* variable = m_Pointer->GetMemberBySemantic( reinterpret_cast<LPCSTR>( pinnedName ) );
		if( variable == 0 )
			return nullptr;

		return gcnew EffectVariable( variable );
	}

	EffectBlendVariable^ EffectVariable::AsBlend()
	{
		ID3DX11EffectBlendVariable* variable = m_Pointer->AsBlend();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectBlendVariable( variable );
	}

	EffectClassInstanceVariable^ EffectVariable::AsClassInstance()
	{
		ID3DX11EffectClassInstanceVariable* variable = m_Pointer->AsClassInstance();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectClassInstanceVariable( variable );
	}
	
	EffectConstantBuffer^ EffectVariable::AsConstantBuffer()
	{
		ID3DX11EffectConstantBuffer* variable = m_Pointer->AsConstantBuffer();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectConstantBuffer( variable );
	}

	EffectDepthStencilVariable^ EffectVariable::AsDepthStencil()
	{
		ID3DX11EffectDepthStencilVariable* variable = m_Pointer->AsDepthStencil();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectDepthStencilVariable( variable );
	}

	EffectDepthStencilViewVariable^ EffectVariable::AsDepthStencilView()
	{
		ID3DX11EffectDepthStencilViewVariable* variable = m_Pointer->AsDepthStencilView();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectDepthStencilViewVariable( variable );
	}

	EffectInterfaceVariable^ EffectVariable::AsInterface()
	{
		ID3DX11EffectInterfaceVariable* variable = m_Pointer->AsInterface();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectInterfaceVariable( variable );
	}
	
	EffectMatrixVariable^ EffectVariable::AsMatrix()
	{
		ID3DX11EffectMatrixVariable* variable = m_Pointer->AsMatrix();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectMatrixVariable( variable );
	}

	EffectRasterizerVariable^ EffectVariable::AsRasterizer()
	{
		ID3DX11EffectRasterizerVariable* variable = m_Pointer->AsRasterizer();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectRasterizerVariable( variable );
	}

	EffectRenderTargetViewVariable^ EffectVariable::AsRenderTargetView()
	{
		ID3DX11EffectRenderTargetViewVariable* variable = m_Pointer->AsRenderTargetView();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectRenderTargetViewVariable( variable );
	}

	EffectSamplerVariable^ EffectVariable::AsSampler()
	{
		ID3DX11EffectSamplerVariable* variable = m_Pointer->AsSampler();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectSamplerVariable( variable );
	}
	
	EffectResourceVariable^ EffectVariable::AsResource()
	{
		ID3DX11EffectShaderResourceVariable* variable = m_Pointer->AsShaderResource();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectResourceVariable( variable );
	}
	
	EffectShaderVariable^ EffectVariable::AsShader()
	{
		ID3DX11EffectShaderVariable* variable = m_Pointer->AsShader();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectShaderVariable( variable );
	}
	
	EffectScalarVariable^ EffectVariable::AsScalar()
	{
		ID3DX11EffectScalarVariable* variable = m_Pointer->AsScalar();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectScalarVariable( variable );
	}
	
	EffectVectorVariable^ EffectVariable::AsVector()
	{
		ID3DX11EffectVectorVariable* variable = m_Pointer->AsVector();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectVectorVariable( variable );
	}

	EffectStringVariable^ EffectVariable::AsString()
	{
		ID3DX11EffectStringVariable* variable = m_Pointer->AsString();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectStringVariable( variable );
	}
	
	EffectUnorderedAccessViewVariable^ EffectVariable::AsUnorderedAccessView()
	{
		ID3DX11EffectUnorderedAccessViewVariable* variable = m_Pointer->AsUnorderedAccessView();
		if( variable == 0 || !variable->IsValid() )
			return nullptr;

		return gcnew EffectUnorderedAccessViewVariable( variable );
	}

	EffectType^ EffectVariable::GetVariableType()
	{
		ID3DX11EffectType* type = m_Pointer->GetType();
		if( type == 0 || !type->IsValid() )
			return nullptr;

		return gcnew EffectType( type );
	}

	Result EffectVariable::SetRawValue(DataStream^ data, int count)
	{
		HRESULT hr = m_Pointer->SetRawValue(data->PositionPointer, 0, count);
		return RECORD_D3D11(hr);
	}

	DataStream^ EffectVariable::GetRawValue(int count)
	{
		stack_array<char> data = stackalloc(char, count);

		HRESULT hr = m_Pointer->GetRawValue(&data[0], 0, count);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		return gcnew DataStream(&data[0], count, true, true, true);
	}
}
}