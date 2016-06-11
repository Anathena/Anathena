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

#include "EffectVariableDescription11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 	
	EffectVariableDescription::EffectVariableDescription( const D3DX11_EFFECT_VARIABLE_DESC& native )
	{
		m_Name = gcnew String( native.Name );
		m_Semantic = gcnew String( native.Semantic );
		m_Flags = static_cast<EffectVariableFlags>( native.Flags );
		m_Annotations = native.Annotations;
		m_BufferOffset = native.BufferOffset;
		m_ExplicitBindPoint = native.ExplicitBindPoint;
	}

	String^ EffectVariableDescription::Name::get()
	{
		return m_Name;
	}
	
	String^ EffectVariableDescription::Semantic::get()
	{
		return m_Semantic;
	}

	EffectVariableFlags EffectVariableDescription::Flags::get()
	{
		return m_Flags;
	}
	
	int EffectVariableDescription::AnnotationCount::get()
	{
		return m_Annotations;
	}
	
	int EffectVariableDescription::ConstantBufferOffset::get()
	{
		return m_BufferOffset;
	}
	
	int EffectVariableDescription::ExplicitBindPoint::get()
	{
		return m_ExplicitBindPoint;
	}

	bool EffectVariableDescription::operator == ( EffectVariableDescription left, EffectVariableDescription right )
	{
		return EffectVariableDescription::Equals( left, right );
	}

	bool EffectVariableDescription::operator != ( EffectVariableDescription left, EffectVariableDescription right )
	{
		return !EffectVariableDescription::Equals( left, right );
	}

	int EffectVariableDescription::GetHashCode()
	{
		return m_Name->GetHashCode() + m_Semantic->GetHashCode() + m_Flags.GetHashCode() + m_Annotations.GetHashCode() + m_BufferOffset.GetHashCode() + m_ExplicitBindPoint.GetHashCode();
	}

	bool EffectVariableDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectVariableDescription>( value ) );
	}

	bool EffectVariableDescription::Equals( EffectVariableDescription value )
	{
		return ( m_Name == value.m_Name && m_Semantic == value.m_Semantic && m_Flags == value.m_Flags && m_Annotations == value.m_Annotations && m_BufferOffset == value.m_BufferOffset && m_ExplicitBindPoint == value.m_ExplicitBindPoint );
	}

	bool EffectVariableDescription::Equals( EffectVariableDescription% value1, EffectVariableDescription% value2 )
	{
		return ( value1.m_Name == value2.m_Name && value1.m_Semantic == value2.m_Semantic && value1.m_Flags == value2.m_Flags && value1.m_Annotations == value2.m_Annotations && value1.m_BufferOffset == value2.m_BufferOffset && value1.m_ExplicitBindPoint == value2.m_ExplicitBindPoint );
	}
}
}
