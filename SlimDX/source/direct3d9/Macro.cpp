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
#include <d3d9.h>
#include <d3dx9.h>
#include <vcclr.h>

#include "../stack_array.h"

#include "Macro.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	bool Macro::operator == ( Macro left, Macro right )
	{
		return Macro::Equals( left, right );
	}

	bool Macro::operator != ( Macro left, Macro right )
	{
		return !Macro::Equals( left, right );
	}

	int Macro::GetHashCode()
	{
		return Name->GetHashCode() + Definition->GetHashCode();
	}

	bool Macro::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Macro>( value ) );
	}

	bool Macro::Equals( Macro value )
	{
		return ( Name == value.Name && Definition == value.Definition );
	}

	bool Macro::Equals( Macro% value1, Macro% value2 )
	{
		return ( value1.Name == value2.Name && value1.Definition == value2.Definition );
	}

	// helper function to resolve array<Macro>^ to D3DXMACRO*
	stack_array<D3DXMACRO> Macro::Marshal( array<Macro>^ macros, [Out] array<GCHandle>^% handles )
	{
		if( macros == nullptr )
		{
			handles = nullptr;
			return stack_array<D3DXMACRO>();
		}

		//this array is null terminated, so we need to patch in an extra value
		stack_array<D3DXMACRO> result( macros->Length + 1 );
		handles = gcnew array<GCHandle>( macros->Length * 2 );

		for( int i = 0; i < macros->Length; ++i )
		{
			array<Byte>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( macros[i].Name );
			array<Byte>^ defBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( macros[i].Definition );

			handles[2 * i] = GCHandle::Alloc( nameBytes, GCHandleType::Pinned );
			handles[2 * i + 1] = GCHandle::Alloc( defBytes, GCHandleType::Pinned );

			result[i].Name = reinterpret_cast<LPCSTR>( handles[2 * i].AddrOfPinnedObject().ToPointer() );
			result[i].Definition = reinterpret_cast<LPCSTR>( handles[2 * i + 1].AddrOfPinnedObject().ToPointer() );
		}

		result[macros->Length].Name = NULL;
		result[macros->Length].Definition = NULL;

		return result;
	}

	void Macro::Unmarshal( array<GCHandle>^ handles )
	{
		if( handles != nullptr )
		{
			for( int i = 0; i < handles->Length; ++i )
				handles[i].Free();
		}
	}
}
}