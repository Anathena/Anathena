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

#include "EffectHandle.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	EffectHandle::EffectHandle( D3DXHANDLE handle )
	{
		m_Handle = handle;
		m_StringData = IntPtr::Zero;

		GC::SuppressFinalize(this);
	}

	EffectHandle::EffectHandle( String^ name )
	{
		m_StringData = Marshal::StringToHGlobalAnsi( name );
		m_HasString = true;
		m_StringDataSize = name->Length;
		GC::AddMemoryPressure( m_StringDataSize );

		m_Handle = reinterpret_cast<D3DXHANDLE>( m_StringData.ToPointer() );
	}

	void EffectHandle::Destruct()
	{
		if( m_HasString )
		{
			Marshal::FreeHGlobal( m_StringData );
			GC::RemoveMemoryPressure( m_StringDataSize );
		}
	}

	EffectHandle::operator EffectHandle^( String^ name )
	{
		return gcnew EffectHandle( name );
	}

	bool EffectHandle::operator == ( EffectHandle^ left, EffectHandle^ right )
	{
		//preferred to ReferenceEquals
		Object^ leftObj = left;
		Object^ rightObj = right;
		if(leftObj == nullptr)
			return rightObj == nullptr;

		return EffectHandle::Equals( left, right );
	}

	bool EffectHandle::operator != ( EffectHandle^ left, EffectHandle^ right )
	{
		return !( left == right );
	}

	int EffectHandle::GetHashCode()
	{
		IntPtr ptr( const_cast<void*>( reinterpret_cast<const void*>( m_Handle ) ) );
		return ptr.GetHashCode();
	}

	bool EffectHandle::Equals( Object^ value )
	{
		if( ((Object^) value) == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<EffectHandle^>( value ) );
	}

	bool EffectHandle::Equals( EffectHandle^ value )
	{
		if( ((Object^) value) == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( m_Handle == value->InternalHandle );
	}

	bool EffectHandle::Equals( EffectHandle^ value1, EffectHandle^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}