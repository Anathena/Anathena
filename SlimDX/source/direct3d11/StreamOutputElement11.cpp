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

#include "Direct3D11Exception.h"

#include "Device11.h"
#include "StreamOutputElement11.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D11
{
	StreamOutputElement::StreamOutputElement( int stream, String^ name, int index, int startComponent, int componentCount, int outputSlot )
	{
		Stream = stream;
		SemanticName = name;
		SemanticIndex = index;
		StartComponent = startComponent;
		ComponentCount = componentCount;
		OutputSlot = outputSlot;
	}
	
	D3D11_SO_DECLARATION_ENTRY StreamOutputElement::CreateNativeVersion()
	{
		D3D11_SO_DECLARATION_ENTRY native;

		native.Stream = Stream;
		native.SemanticName = Utilities::AllocateNativeString( SemanticName );
		native.SemanticIndex = SemanticIndex;
		native.StartComponent = static_cast<BYTE>(StartComponent);
		native.ComponentCount = static_cast<BYTE>(ComponentCount);
		native.OutputSlot = static_cast<BYTE>(OutputSlot);

		return native;
	}
	
	bool StreamOutputElement::operator == ( StreamOutputElement left, StreamOutputElement right )
	{
		return StreamOutputElement::Equals( left, right );
	}

	bool StreamOutputElement::operator != ( StreamOutputElement left, StreamOutputElement right )
	{
		return !StreamOutputElement::Equals( left, right );
	}

	int StreamOutputElement::GetHashCode()
	{
		return SemanticName->GetHashCode() + SemanticIndex.GetHashCode() + StartComponent.GetHashCode()
			 + OutputSlot.GetHashCode() + ComponentCount.GetHashCode()
			 + Stream.GetHashCode();
	}

	bool StreamOutputElement::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<StreamOutputElement>( value ) );
	}

	bool StreamOutputElement::Equals( StreamOutputElement value )
	{
		return ( SemanticName == value.SemanticName && SemanticIndex == value.SemanticIndex && StartComponent == value.StartComponent
			 && OutputSlot == value.OutputSlot && ComponentCount == value.ComponentCount
			 && Stream == value.Stream );
	}

	bool StreamOutputElement::Equals( StreamOutputElement% value1, StreamOutputElement% value2 )
	{
		return ( value1.SemanticName == value2.SemanticName && value1.SemanticIndex == value2.SemanticIndex && value1.StartComponent == value2.StartComponent
			 && value1.OutputSlot == value2.OutputSlot && value1.ComponentCount == value2.ComponentCount
			 && value1.Stream == value2.Stream );
	}
}
}
