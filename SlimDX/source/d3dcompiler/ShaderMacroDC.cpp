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

#include "ShaderMacroDC.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace D3DCompiler
{
	stack_array<D3D10_SHADER_MACRO> ShaderMacro::Marshal( array<ShaderMacro>^ macros, [Out] array<GCHandle>^% handles )
	{
		if( macros == nullptr )
		{
			handles = nullptr;
			return stack_array<D3D10_SHADER_MACRO>();
		}

		// this array is null terminated, so we need to patch in an extra value
		stack_array<D3D10_SHADER_MACRO> result( macros->Length + 1 );
		handles = gcnew array<GCHandle>( macros->Length * 2 );

		for( int i = 0; i < macros->Length; ++i )
		{
			array<Byte>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( macros[i].Name );
			array<Byte>^ defBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( macros[i].Value );

			handles[2 * i] = GCHandle::Alloc( nameBytes, GCHandleType::Pinned );
			handles[2 * i + 1] = GCHandle::Alloc( defBytes, GCHandleType::Pinned );

			result[i].Name = reinterpret_cast<LPCSTR>( handles[2 * i].AddrOfPinnedObject().ToPointer() );
			result[i].Definition = reinterpret_cast<LPCSTR>( handles[2 * i + 1].AddrOfPinnedObject().ToPointer() );
		}

		result[macros->Length].Name = NULL;
		result[macros->Length].Definition = NULL;

		return result;
	}

	void ShaderMacro::Unmarshal( array<GCHandle>^ handles )
	{
		if( handles != nullptr )
		{
			for( int i = 0; i < handles->Length; ++i )
				handles[i].Free();
		}
	}

	ShaderMacro::ShaderMacro( String^ name )
	{
		Name = name;
		Value = "";
	}

	ShaderMacro::ShaderMacro( String^ name, String^ value )
	{
		Name = name;
		Value = value;
	}

	bool ShaderMacro::operator == ( ShaderMacro left, ShaderMacro right )
	{
		return ShaderMacro::Equals( left, right );
	}

	bool ShaderMacro::operator != ( ShaderMacro left, ShaderMacro right )
	{
		return !ShaderMacro::Equals( left, right );
	}

	int ShaderMacro::GetHashCode()
	{
		return Name->GetHashCode() + Value->GetHashCode();
	}

	bool ShaderMacro::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<ShaderMacro>( value ) );
	}

	bool ShaderMacro::Equals( ShaderMacro value )
	{
		return ( Name == value.Name && Value == value.Value );
	}

	bool ShaderMacro::Equals( ShaderMacro% value1, ShaderMacro% value2 )
	{
		return ( value1.Name == value2.Name && value1.Value == value2.Value );
	}
}
}
