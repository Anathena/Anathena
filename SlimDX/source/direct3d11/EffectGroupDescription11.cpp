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

#include "EffectGroupDescription11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectGroupDescription::EffectGroupDescription( const D3DX11_GROUP_DESC& native )
	{
		m_Name = gcnew String( native.Name );
		m_Techniques = native.Techniques;
		m_Annotations = native.Annotations;
	}
	
	String^ EffectGroupDescription::Name::get()
	{
		return m_Name;
	}
	
	int EffectGroupDescription::TechniqueCount::get()
	{
		return m_Techniques;
	}
	
	int EffectGroupDescription::AnnotationCount::get()
	{
		return m_Annotations;
	}

	bool EffectGroupDescription::operator == ( EffectGroupDescription left, EffectGroupDescription right )
	{
		return EffectGroupDescription::Equals( left, right );
	}

	bool EffectGroupDescription::operator != ( EffectGroupDescription left, EffectGroupDescription right )
	{
		return !EffectGroupDescription::Equals( left, right );
	}

	int EffectGroupDescription::GetHashCode()
	{
		return m_Name->GetHashCode() + m_Techniques.GetHashCode() + m_Annotations.GetHashCode();
	}

	bool EffectGroupDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectGroupDescription>( value ) );
	}

	bool EffectGroupDescription::Equals( EffectGroupDescription value )
	{
		return ( m_Name == value.m_Name && m_Techniques == value.m_Techniques && m_Annotations == value.m_Annotations );
	}

	bool EffectGroupDescription::Equals( EffectGroupDescription% value1, EffectGroupDescription% value2 )
	{
		return ( value1.m_Name == value2.m_Name && value1.m_Techniques == value2.m_Techniques && value1.m_Annotations == value2.m_Annotations );
	}
}
}
