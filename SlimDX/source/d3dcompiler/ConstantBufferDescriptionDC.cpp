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

#include "ConstantBufferDescriptionDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{ 
	ConstantBufferDescription::ConstantBufferDescription( const D3D11_SHADER_BUFFER_DESC& description )
	{
		m_Name = gcnew System::String( description.Name );
		m_Type = static_cast<ConstantBufferType>( description.Type );
		m_Variables = static_cast<int>( description.Variables );
		m_Size = static_cast<int>( description.Size );
		m_Flags = static_cast<ShaderVariableFlags>( description.uFlags );
	}
	
	System::String^ ConstantBufferDescription::Name::get()
	{
		return m_Name;
	}

	ConstantBufferType ConstantBufferDescription::Type::get()
	{
		return m_Type;
	}

	int ConstantBufferDescription::Variables::get()
	{
		return m_Variables;
	}

	int ConstantBufferDescription::Size::get()
	{
		return m_Size;
	}

	ShaderVariableFlags ConstantBufferDescription::Flags::get()
	{
		return m_Flags;
	}

	bool ConstantBufferDescription::operator == ( ConstantBufferDescription left, ConstantBufferDescription right )
	{
		return ConstantBufferDescription::Equals( left, right );
	}

	bool ConstantBufferDescription::operator != ( ConstantBufferDescription left, ConstantBufferDescription right )
	{
		return !ConstantBufferDescription::Equals( left, right );
	}

	int ConstantBufferDescription::GetHashCode()
	{
		return m_Name->GetHashCode() + m_Type.GetHashCode() + m_Variables.GetHashCode() + m_Size.GetHashCode() + m_Flags.GetHashCode();
	}

	bool ConstantBufferDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ConstantBufferDescription>( value ) );
	}

	bool ConstantBufferDescription::Equals( ConstantBufferDescription value )
	{
		return ( m_Name == value.m_Name && m_Type == value.m_Type && m_Variables == value.m_Variables && m_Size == value.m_Size && m_Flags == value.m_Flags );
	}

	bool ConstantBufferDescription::Equals( ConstantBufferDescription% value1, ConstantBufferDescription% value2 )
	{
		return ( value1.m_Name == value2.m_Name && value1.m_Type == value2.m_Type && value1.m_Variables == value2.m_Variables && value1.m_Size == value2.m_Size && value1.m_Flags == value2.m_Flags );
	}
}
}