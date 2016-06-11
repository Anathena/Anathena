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

#include "EffectTypeDescription11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 	
	EffectTypeDescription::EffectTypeDescription( const D3DX11_EFFECT_TYPE_DESC& native )
	{
		m_TypeName = gcnew String( native.TypeName );
		m_Class = static_cast<D3DCompiler::ShaderVariableClass>( native.Class );
		m_Type = static_cast<D3DCompiler::ShaderVariableType>( native.Type );
		m_Elements = native.Elements;
		m_Members = native.Members;
		m_Rows = native.Rows;
		m_Columns = native.Columns;
		m_PackedSize = native.PackedSize;
		m_UnpackedSize = native.UnpackedSize;
		m_Stride = native.Stride;
	}

	String^ EffectTypeDescription::TypeName::get()
	{
		return m_TypeName;
	}
	
	D3DCompiler::ShaderVariableClass EffectTypeDescription::Class::get()
	{
		return m_Class;
	}
	
	D3DCompiler::ShaderVariableType EffectTypeDescription::Type::get()
	{
		return m_Type;
	}
	
	int EffectTypeDescription::Elements::get()
	{
		return m_Elements;
	}
	
	int EffectTypeDescription::Members::get()
	{
		return m_Members;
	}
	
	int EffectTypeDescription::Rows::get()
	{
		return m_Rows;
	}
	
	int EffectTypeDescription::Columns::get()
	{
		return m_Columns;
	}
	
	int EffectTypeDescription::PackedSize::get()
	{
		return m_PackedSize;
	}
	
	int EffectTypeDescription::UnpackedSize::get()
	{
		return m_UnpackedSize;
	}
	
	int EffectTypeDescription::Stride::get()
	{
		return m_Stride;
	}

	bool EffectTypeDescription::operator == ( EffectTypeDescription left, EffectTypeDescription right )
	{
		return EffectTypeDescription::Equals( left, right );
	}

	bool EffectTypeDescription::operator != ( EffectTypeDescription left, EffectTypeDescription right )
	{
		return !EffectTypeDescription::Equals( left, right );
	}

	int EffectTypeDescription::GetHashCode()
	{
		return m_TypeName->GetHashCode() + m_Class.GetHashCode() + m_Type.GetHashCode() + m_Elements.GetHashCode() + m_Members.GetHashCode() + m_Rows.GetHashCode() + m_Columns.GetHashCode() + m_PackedSize.GetHashCode() + m_UnpackedSize.GetHashCode() + m_Stride.GetHashCode();
	}

	bool EffectTypeDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectTypeDescription>( value ) );
	}

	bool EffectTypeDescription::Equals( EffectTypeDescription value )
	{
		return ( m_TypeName == value.m_TypeName && m_Class == value.m_Class && m_Type == value.m_Type && m_Elements == value.m_Elements && m_Members == value.m_Members && m_Rows == value.m_Rows && m_Columns == value.m_Columns && m_PackedSize == value.m_PackedSize && m_UnpackedSize == value.m_UnpackedSize && m_Stride == value.m_Stride );
	}

	bool EffectTypeDescription::Equals( EffectTypeDescription% value1, EffectTypeDescription% value2 )
	{
		return ( value1.m_TypeName == value2.m_TypeName && value1.m_Class == value2.m_Class && value1.m_Type == value2.m_Type && value1.m_Elements == value2.m_Elements && value1.m_Members == value2.m_Members && value1.m_Rows == value2.m_Rows && value1.m_Columns == value2.m_Columns && value1.m_PackedSize == value2.m_PackedSize && value1.m_UnpackedSize == value2.m_UnpackedSize && value1.m_Stride == value2.m_Stride );
	}
}
}