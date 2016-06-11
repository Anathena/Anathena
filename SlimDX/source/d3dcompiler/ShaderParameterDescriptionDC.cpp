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

#include "ShaderParameterDescriptionDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{ 
	ShaderParameterDescription::ShaderParameterDescription( const D3D11_SIGNATURE_PARAMETER_DESC& description )
	{
		m_SemanticName = gcnew System::String( description.SemanticName );
		m_SemanticIndex = description.SemanticIndex;
		m_Register = description.Register;
		m_SystemValueType = static_cast<SystemValueType>( description.SystemValueType );
		m_ComponentType = static_cast<RegisterComponentType>( description.ComponentType );
		m_Mask = static_cast<RegisterComponentMaskFlags>( description.Mask );
		m_ReadWriteMask = static_cast<RegisterComponentMaskFlags>( description.ReadWriteMask );
		m_Stream = static_cast<int>( description.Stream );
	}
	
	System::String^ ShaderParameterDescription::SemanticName::get()
	{
		return m_SemanticName;
	}

	System::UInt32 ShaderParameterDescription::SemanticIndex::get()
	{
		return m_SemanticIndex;
	}

	System::UInt32 ShaderParameterDescription::Register::get()
	{
		return m_Register;
	}

	SystemValueType ShaderParameterDescription::SystemType::get()
	{
		return m_SystemValueType;
	}

	RegisterComponentType ShaderParameterDescription::ComponentType::get()
	{
		return m_ComponentType;
	}

	RegisterComponentMaskFlags ShaderParameterDescription::UsageMask::get()
	{
		return m_Mask;
	}

	RegisterComponentMaskFlags ShaderParameterDescription::ReadWriteMask::get()
	{
		return m_ReadWriteMask;
	}

	int ShaderParameterDescription::Stream::get()
	{
		return m_Stream;
	}

	bool ShaderParameterDescription::operator == ( ShaderParameterDescription left, ShaderParameterDescription right )
	{
		return ShaderParameterDescription::Equals( left, right );
	}

	bool ShaderParameterDescription::operator != ( ShaderParameterDescription left, ShaderParameterDescription right )
	{
		return !ShaderParameterDescription::Equals( left, right );
	}

	int ShaderParameterDescription::GetHashCode()
	{
		return m_SemanticName->GetHashCode() + m_SemanticIndex.GetHashCode() + m_Register.GetHashCode() + m_SystemValueType.GetHashCode() + m_ComponentType.GetHashCode() + m_Mask.GetHashCode() + m_ReadWriteMask.GetHashCode();
	}

	bool ShaderParameterDescription::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ShaderParameterDescription>( value ) );
	}

	bool ShaderParameterDescription::Equals( ShaderParameterDescription value )
	{
		return ( m_SemanticName == value.m_SemanticName && m_SemanticIndex == value.m_SemanticIndex && m_Register == value.m_Register && m_SystemValueType == value.m_SystemValueType && m_ComponentType == value.m_ComponentType && m_Mask == value.m_Mask && m_ReadWriteMask == value.m_ReadWriteMask );
	}

	bool ShaderParameterDescription::Equals( ShaderParameterDescription% value1, ShaderParameterDescription% value2 )
	{
		return ( value1.m_SemanticName == value2.m_SemanticName && value1.m_SemanticIndex == value2.m_SemanticIndex && value1.m_Register == value2.m_Register && value1.m_SystemValueType == value2.m_SystemValueType && value1.m_ComponentType == value2.m_ComponentType && value1.m_Mask == value2.m_Mask && value1.m_ReadWriteMask == value2.m_ReadWriteMask );
	}
}
}
