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

#include "EffectTechniqueDescription11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectTechniqueDescription::EffectTechniqueDescription( const D3DX11_TECHNIQUE_DESC& native )
	{
		m_Name = gcnew String( native.Name );
		m_Passes = native.Passes;
		m_Annotations = native.Annotations;
	}
	
	String^ EffectTechniqueDescription::Name::get()
	{
		return m_Name;
	}
	
	int EffectTechniqueDescription::PassCount::get()
	{
		return m_Passes;
	}
	
	int EffectTechniqueDescription::AnnotationCount::get()
	{
		return m_Annotations;
	}

	bool EffectTechniqueDescription::operator == ( EffectTechniqueDescription left, EffectTechniqueDescription right )
	{
		return EffectTechniqueDescription::Equals( left, right );
	}

	bool EffectTechniqueDescription::operator != ( EffectTechniqueDescription left, EffectTechniqueDescription right )
	{
		return !EffectTechniqueDescription::Equals( left, right );
	}

	int EffectTechniqueDescription::GetHashCode()
	{
		return m_Name->GetHashCode() + m_Passes.GetHashCode() + m_Annotations.GetHashCode();
	}

	bool EffectTechniqueDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectTechniqueDescription>( value ) );
	}

	bool EffectTechniqueDescription::Equals( EffectTechniqueDescription value )
	{
		return ( m_Name == value.m_Name && m_Passes == value.m_Passes && m_Annotations == value.m_Annotations );
	}

	bool EffectTechniqueDescription::Equals( EffectTechniqueDescription% value1, EffectTechniqueDescription% value2 )
	{
		return ( value1.m_Name == value2.m_Name && value1.m_Passes == value2.m_Passes && value1.m_Annotations == value2.m_Annotations );
	}
}
}
