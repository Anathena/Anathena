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

#include "ComObject.h"
#include "Configuration.h"

using namespace System;

namespace SlimDX
{
	ComObject::ComObject()
	{
	}

	ComObject::~ComObject()
	{
		if( m_Owner != nullptr )
			return;

		Destruct();
	}

	bool ComObject::Disposed::get()
	{
		if( m_Unknown == 0 )
			return true;

		if( !ObjectTable::Contains( this ) )
			return true;

		return false;
	}
	
	IntPtr ComObject::ComPointer::get()
	{
		return IntPtr( m_Unknown );
	}

	ComObject^ ComObject::Owner::get()
	{
		return m_Owner;
	}
	
	void ComObject::Owner::set( ComObject^ value )
	{
		m_Owner = value;
	}

	IUnknown* ComObject::UnknownPointer::get()
	{
		return m_Unknown;
	}
	
	IUnknown* ComObject::InternalPointer::get()
	{
		return m_Unknown;
	}
	
	void ComObject::SetFlags( ComObjectFlags flags )
	{
		m_Flags = flags;
	}
	
	void ComObject::SetSource( System::Diagnostics::StackTrace^ stack )
	{
		m_Source = stack;
	}

	System::Diagnostics::StackTrace^ ComObject::CreationSource::get()
	{
		return m_Source;
	}

	void ComObject::SetCreationTime( int time )
	{
		m_CreationTime = time;
	}

	int ComObject::CreationTime::get()
	{
		return m_CreationTime;
	}

	void ComObject::Construct( IUnknown* pointer, ComObject^ owner, bool addToTable )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );

		m_Unknown = pointer;

		if (addToTable)
			ObjectTable::Add( this, owner );
		else
			ObjectTable::RegisterParent( this, owner );
	}
	
	void ComObject::Construct( IUnknown* pointer )
	{
		Construct( pointer, nullptr );
	}
	
	void ComObject::Construct( IUnknown* pointer, ComObject^ owner )
	{
		if( pointer == 0 )
			throw gcnew ArgumentNullException( "pointer" );

		m_Unknown = pointer;
		ObjectTable::Add( this, owner );
	}

	void ComObject::Construct( IntPtr pointer, Guid guid )
	{
		if( pointer == IntPtr::Zero )
			throw gcnew ArgumentNullException( "pointer" );

		void* result = 0;
		IUnknown* unknown = static_cast<IUnknown*>( pointer.ToPointer() );
		HRESULT hr = unknown->QueryInterface( Utilities::ConvertManagedGuid( guid ), &result );
		if( FAILED( hr ) )
			throw gcnew InvalidCastException( "Failed to QueryInterface on user-supplied pointer." );

		Construct( unknown );
	}

	void ComObject::Destruct()
	{
		if( m_Unknown == 0 || !ObjectTable::Remove( this ) )
		{
			// test for CreationTime is to avoid throwing ObjectDisposedException when
			// a constructor for an object fails
			if( Configuration::DetectDoubleDispose && CreationTime != 0 )
			{
				Type^ myType = GetType();
				throw gcnew ObjectDisposedException( myType->FullName );
			}
		}
		else
		{
			if( static_cast<int>( m_Flags & ComObjectFlags::IsAncillary ) == 0 )
				m_Unknown->Release();

			m_Unknown = 0;
		}
	}
}