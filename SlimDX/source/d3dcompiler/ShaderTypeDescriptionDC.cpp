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

#include "ShaderTypeDescriptionDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{ 	
	ShaderTypeDescription::ShaderTypeDescription( const D3D11_SHADER_TYPE_DESC& native )
	{
		m_Class = static_cast<ShaderVariableClass>( native.Class );
		m_Type = static_cast<ShaderVariableType>( native.Type );
		m_Rows = native.Rows;
		m_Columns = native.Columns;
		m_Elements = native.Elements;
		m_Members = native.Members;
		m_Offset = native.Offset;
	}

	ShaderVariableClass ShaderTypeDescription::Class::get()
	{
		return m_Class;
	}
	
	ShaderVariableType ShaderTypeDescription::Type::get()
	{
		return m_Type;
	}
	
	int ShaderTypeDescription::Rows::get()
	{
		return m_Rows;
	}
	
	int ShaderTypeDescription::Columns::get()
	{
		return m_Columns;
	}

	int ShaderTypeDescription::Elements::get()
	{
		return m_Elements;
	}
	
	int ShaderTypeDescription::Members::get()
	{
		return m_Members;
	}
	
	int ShaderTypeDescription::Offset::get()
	{
		return m_Offset;
	}

	bool ShaderTypeDescription::operator == ( ShaderTypeDescription left, ShaderTypeDescription right )
	{
		return ShaderTypeDescription::Equals( left, right );
	}

	bool ShaderTypeDescription::operator != ( ShaderTypeDescription left, ShaderTypeDescription right )
	{
		return !ShaderTypeDescription::Equals( left, right );
	}

	int ShaderTypeDescription::GetHashCode()
	{
		return m_Class.GetHashCode() + m_Type.GetHashCode() + m_Rows.GetHashCode() + m_Columns.GetHashCode() + m_Elements.GetHashCode() + m_Members.GetHashCode() + m_Offset.GetHashCode();
	}

	bool ShaderTypeDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ShaderTypeDescription>( value ) );
	}

	bool ShaderTypeDescription::Equals( ShaderTypeDescription value )
	{
		return ( m_Class == value.m_Class && m_Type == value.m_Type && m_Rows == value.m_Rows && m_Columns == value.m_Columns && m_Elements == value.m_Elements && m_Members == value.m_Members && m_Offset == value.m_Offset );
	}

	bool ShaderTypeDescription::Equals( ShaderTypeDescription% value1, ShaderTypeDescription% value2 )
	{
		return ( value1.m_Class == value2.m_Class && value1.m_Rows == value2.m_Rows && value1.m_Columns == value2.m_Columns && value1.m_Type == value2.m_Type && value1.m_Elements == value2.m_Elements && value1.m_Members == value2.m_Members && value1.m_Offset == value2.m_Offset );
	}
}
}