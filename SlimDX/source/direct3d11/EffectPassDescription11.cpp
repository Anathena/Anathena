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

#include "EffectPassDescription11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectPassDescription::EffectPassDescription( const D3DX11_PASS_DESC& native )
	{
		m_Name = gcnew String( native.Name );
		m_Annotations = native.Annotations;
		m_StencilRef = native.StencilRef;
		m_SampleMask = native.SampleMask;
		m_BlendFactor = Color4( native.BlendFactor[ 3 ], native.BlendFactor[ 0 ], native.BlendFactor[ 1 ], native.BlendFactor[ 2 ] );

		if (native.pIAInputSignature && native.IAInputSignatureSize)
			m_Signature = gcnew SlimDX::D3DCompiler::ShaderSignature(gcnew DataStream(native.pIAInputSignature, native.IAInputSignatureSize, true, false, false));
	}
	
	String^ EffectPassDescription::Name::get()
	{
		return m_Name;
	}
	
	int EffectPassDescription::AnnotationCount::get()
	{
		return m_Annotations;
	}
	
	SlimDX::D3DCompiler::ShaderSignature^ EffectPassDescription::Signature::get()
	{
		return m_Signature;
	}
	
	int EffectPassDescription::StencilReference::get()
	{
		return m_StencilRef;
	}
	
	int EffectPassDescription::SampleMask::get()
	{
		return m_SampleMask;
	}
	
	Color4 EffectPassDescription::BlendFactor::get()
	{
		return m_BlendFactor;
	}

	bool EffectPassDescription::operator == ( EffectPassDescription left, EffectPassDescription right )
	{
		return EffectPassDescription::Equals( left, right );
	}

	bool EffectPassDescription::operator != ( EffectPassDescription left, EffectPassDescription right )
	{
		return !EffectPassDescription::Equals( left, right );
	}

	int EffectPassDescription::GetHashCode()
	{
		return m_Name->GetHashCode() + m_Annotations.GetHashCode() + m_Signature->GetHashCode() + m_StencilRef.GetHashCode() + m_SampleMask.GetHashCode() + m_BlendFactor.GetHashCode();
	}

	bool EffectPassDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectPassDescription>( value ) );
	}

	bool EffectPassDescription::Equals( EffectPassDescription value )
	{
		return ( m_Name == value.m_Name && m_Annotations == value.m_Annotations && m_Signature == value.m_Signature && m_StencilRef == value.m_StencilRef && m_SampleMask == value.m_SampleMask && m_BlendFactor == value.m_BlendFactor );
	}

	bool EffectPassDescription::Equals( EffectPassDescription% value1, EffectPassDescription% value2 )
	{
		return ( value1.m_Name == value2.m_Name && value1.m_Annotations == value2.m_Annotations && value1.m_Signature == value2.m_Signature && value1.m_StencilRef == value2.m_StencilRef && value1.m_SampleMask == value2.m_SampleMask && value1.m_BlendFactor == value2.m_BlendFactor );
	}
}
}
