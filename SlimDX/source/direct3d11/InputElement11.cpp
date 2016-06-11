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

#include "Direct3D11Exception.h"

#include "Device11.h"
#include "InputLayout11.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D11
{
	InputElement::InputElement( System::String^ name, int index, DXGI::Format format, int slot )
	{
		m_SemanticName = name;
		m_SemanticIndex = index;
		m_Format = format;
		m_InputSlot = slot;
		m_AlignedByteOffset = AppendAligned;
		m_InputSlotClass = InputClassification::PerVertexData;
		m_InstanceDataStepRate = 0;
	}

	InputElement::InputElement( System::String^ name, int index, DXGI::Format format, int offset, int slot )
	{
		m_SemanticName = name;
		m_SemanticIndex = index;
		m_Format = format;
		m_InputSlot = slot;
		m_AlignedByteOffset = offset;
		m_InputSlotClass = InputClassification::PerVertexData;
		m_InstanceDataStepRate = 0;
	}

	InputElement::InputElement( System::String^ name, int index, DXGI::Format format, int offset, int slot, InputClassification slotClass, int stepRate )
	{
		m_SemanticName = name;
		m_SemanticIndex = index;
		m_Format = format;
		m_InputSlot = slot;
		m_AlignedByteOffset = offset;
		m_InputSlotClass = slotClass;
		m_InstanceDataStepRate = stepRate;
	}
	
	D3D11_INPUT_ELEMENT_DESC InputElement::CreateNativeVersion()
	{
		D3D11_INPUT_ELEMENT_DESC native;
	
		native.SemanticName = Utilities::AllocateNativeString( m_SemanticName );
		native.SemanticIndex = m_SemanticIndex;
		native.Format = static_cast<DXGI_FORMAT>( m_Format );
		native.InputSlot = m_InputSlot;
		native.AlignedByteOffset = m_AlignedByteOffset;
		native.InputSlotClass = static_cast<D3D11_INPUT_CLASSIFICATION>( m_InputSlotClass );
		native.InstanceDataStepRate = m_InstanceDataStepRate;
		
		return native;
	}
	
	String^ InputElement::SemanticName::get()
	{
		return m_SemanticName;
	}
	
	void InputElement::SemanticName::set( String^ value )
	{
		m_SemanticName = value;
	}
	
	int InputElement::SemanticIndex::get()
	{
		return m_SemanticIndex;
	}
	
	void InputElement::SemanticIndex::set( int value )
	{
		m_SemanticIndex = value;
	}
	
	DXGI::Format InputElement::Format::get()
	{
		return m_Format;
	}
	
	void InputElement::Format::set( DXGI::Format value )
	{
		m_Format = value;
	}
	
	int InputElement::Slot::get()
	{
		return m_InputSlot;
	}
	
	void InputElement::Slot::set( int value )
	{
		m_InputSlot = value;
	}
	
	int InputElement::AlignedByteOffset::get()
	{
		return m_AlignedByteOffset;
	}
	
	void InputElement::AlignedByteOffset::set( int value )
	{
		m_AlignedByteOffset = value;
	}
	
	InputClassification InputElement::Classification::get()
	{
		return m_InputSlotClass;
	}
	
	void InputElement::Classification::set( InputClassification value )
	{
		m_InputSlotClass = value;	
	}
	
	int InputElement::InstanceDataStepRate::get()
	{
		return m_InstanceDataStepRate;
	}
	
	void InputElement::InstanceDataStepRate::set( int value )
	{
		m_InstanceDataStepRate = value;
	}

	int InputElement::AppendAligned::get()
	{
		return D3D11_APPEND_ALIGNED_ELEMENT;
	}

	bool InputElement::operator == ( InputElement left, InputElement right )
	{
		return InputElement::Equals( left, right );
	}

	bool InputElement::operator != ( InputElement left, InputElement right )
	{
		return !InputElement::Equals( left, right );
	}

	int InputElement::GetHashCode()
	{
		return m_SemanticName->GetHashCode() + m_SemanticIndex.GetHashCode() + m_Format.GetHashCode()
			 + m_InputSlot.GetHashCode() + m_AlignedByteOffset.GetHashCode() + m_InputSlotClass.GetHashCode()
			 + m_InstanceDataStepRate.GetHashCode();
	}

	bool InputElement::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<InputElement>( value ) );
	}

	bool InputElement::Equals( InputElement value )
	{
		return ( m_SemanticName == value.m_SemanticName && m_SemanticIndex == value.m_SemanticIndex && m_Format == value.m_Format
			 && m_InputSlot == value.m_InputSlot && m_AlignedByteOffset == value.m_AlignedByteOffset && m_InputSlotClass == value.m_InputSlotClass
			 && m_InstanceDataStepRate == value.m_InstanceDataStepRate );
	}

	bool InputElement::Equals( InputElement% value1, InputElement% value2 )
	{
		return ( value1.m_SemanticName == value2.m_SemanticName && value1.m_SemanticIndex == value2.m_SemanticIndex && value1.m_Format == value2.m_Format
			 && value1.m_InputSlot == value2.m_InputSlot && value1.m_AlignedByteOffset == value2.m_AlignedByteOffset && value1.m_InputSlotClass == value2.m_InputSlotClass
			 && value1.m_InstanceDataStepRate == value2.m_InstanceDataStepRate );
	}
}
}
