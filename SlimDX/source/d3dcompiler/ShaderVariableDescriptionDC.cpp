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

#include "ShaderVariableDescriptionDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{ 
	ShaderVariableDescription::ShaderVariableDescription( const D3D11_SHADER_VARIABLE_DESC& description )
	{
		m_Name = gcnew System::String( description.Name );
		m_StartOffset = static_cast<int>( description.StartOffset );
		m_Size = static_cast<int>( description.Size );
		m_Flags = static_cast<ShaderVariableFlags>( description.uFlags );
		defaultValue = IntPtr( description.DefaultValue );
	}
	
	System::String^ ShaderVariableDescription::Name::get()
	{
		return m_Name;
	}

	int ShaderVariableDescription::StartOffset::get()
	{
		return m_StartOffset;
	}

	int ShaderVariableDescription::Size::get()
	{
		return m_Size;
	}

	IntPtr ShaderVariableDescription::DefaultValue::get()
	{
		return defaultValue;
	}

	ShaderVariableFlags ShaderVariableDescription::Flags::get()
	{
		return m_Flags;
	}

	bool ShaderVariableDescription::operator == ( ShaderVariableDescription left, ShaderVariableDescription right )
	{
		return ShaderVariableDescription::Equals( left, right );
	}

	bool ShaderVariableDescription::operator != ( ShaderVariableDescription left, ShaderVariableDescription right )
	{
		return !ShaderVariableDescription::Equals( left, right );
	}

	int ShaderVariableDescription::GetHashCode()
	{
		return m_Name->GetHashCode() + m_StartOffset.GetHashCode() + m_Size.GetHashCode() + m_Flags.GetHashCode();
	}

	bool ShaderVariableDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ShaderVariableDescription>( value ) );
	}

	bool ShaderVariableDescription::Equals( ShaderVariableDescription value )
	{
		return ( m_Name == value.m_Name && m_StartOffset == value.m_StartOffset && m_Size == value.m_Size && m_Flags == value.m_Flags );
	}

	bool ShaderVariableDescription::Equals( ShaderVariableDescription% value1, ShaderVariableDescription% value2 )
	{
		return ( value1.m_Name == value2.m_Name && value1.m_StartOffset == value2.m_StartOffset && value1.m_Size == value2.m_Size && value1.m_Flags == value2.m_Flags );
	}
}
}