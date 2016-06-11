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

#include "EffectDescription10.h"

namespace SlimDX
{
namespace Direct3D10
{ 	
	EffectDescription::EffectDescription( const D3D10_EFFECT_DESC& native )
	{
		m_IsChildEffect = native.IsChildEffect ? true : false;
		m_ConstantBuffers = native.ConstantBuffers;
		m_SharedConstantBuffers = native.SharedConstantBuffers;
		m_GlobalVariables = native.GlobalVariables;
		m_SharedGlobalVariables = native.SharedGlobalVariables;
		m_Techniques = native.Techniques;
	}

	bool EffectDescription::IsChildEffect::get()
	{
		return m_IsChildEffect;
	}

	int EffectDescription::ConstantBufferCount::get()
	{
		return m_ConstantBuffers;
	}
	
	int EffectDescription::SharedConstantBufferCount::get()
	{
		return m_SharedConstantBuffers;
	}
	
	int EffectDescription::GlobalVariableCount::get()
	{
		return m_GlobalVariables;
	}
	
	int EffectDescription::SharedGlobalVariableCount::get()
	{
		return m_SharedGlobalVariables;
	}
	
	int EffectDescription::TechniqueCount::get()
	{
		return m_Techniques;
	}

	bool EffectDescription::operator == ( EffectDescription left, EffectDescription right )
	{
		return EffectDescription::Equals( left, right );
	}

	bool EffectDescription::operator != ( EffectDescription left, EffectDescription right )
	{
		return !EffectDescription::Equals( left, right );
	}

	int EffectDescription::GetHashCode()
	{
		return m_IsChildEffect.GetHashCode() + m_ConstantBuffers.GetHashCode() + m_SharedConstantBuffers.GetHashCode() + m_GlobalVariables.GetHashCode() + m_SharedGlobalVariables.GetHashCode() + m_Techniques.GetHashCode();
	}

	bool EffectDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectDescription>( value ) );
	}

	bool EffectDescription::Equals( EffectDescription value )
	{
		return ( m_IsChildEffect == value.m_IsChildEffect && m_ConstantBuffers == value.m_ConstantBuffers && m_SharedConstantBuffers == value.m_SharedConstantBuffers && m_GlobalVariables == value.m_GlobalVariables && m_SharedGlobalVariables == value.m_SharedGlobalVariables && m_Techniques == value.m_Techniques );
	}

	bool EffectDescription::Equals( EffectDescription% value1, EffectDescription% value2 )
	{
		return ( value1.m_IsChildEffect == value2.m_IsChildEffect && value1.m_ConstantBuffers == value2.m_ConstantBuffers && value1.m_SharedConstantBuffers == value2.m_SharedConstantBuffers && value1.m_GlobalVariables == value2.m_GlobalVariables && value1.m_SharedGlobalVariables == value2.m_SharedGlobalVariables && value1.m_Techniques == value2.m_Techniques );
	}
}
}
