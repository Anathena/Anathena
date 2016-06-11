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

#include "ObjectTable.h"
#include "InternalHelpers.h"
#include "ComObject.h"

using namespace System;
using namespace System::Text;
using namespace System::Threading;
using namespace System::Globalization;
using namespace System::Collections::ObjectModel;
using namespace System::Collections::Generic;
using namespace System::Diagnostics;

namespace SlimDX
{
	static ObjectTable::ObjectTable()
	{
		m_Table = gcnew Dictionary<IntPtr, ComObject^>();
		m_Ancillary = gcnew Dictionary<IntPtr, List<ComObject^>^>();
		m_SyncObject = gcnew Object();

		AppDomain::CurrentDomain->DomainUnload += gcnew System::EventHandler( OnExit );
		AppDomain::CurrentDomain->ProcessExit += gcnew System::EventHandler( OnExit );
	}

	ObjectTable::ObjectTable()
	{
	}

	void ObjectTable::OnExit( Object^ sender, EventArgs^ e )
	{
		SLIMDX_UNREFERENCED_PARAMETER(sender);
		SLIMDX_UNREFERENCED_PARAMETER(e);

		String^ leakString = ReportLeaks();
		Debug::Write( leakString );
	}

	ComObject^ ObjectTable::Find( IntPtr nativeObject )
	{
		Monitor::Enter( m_SyncObject );
		try
		{
			if( m_Table->ContainsKey( nativeObject ) )
			{
				return m_Table[nativeObject];
			}

			return nullptr;
		}
		finally
		{
			Monitor::Exit( m_SyncObject );
		}
	}

	bool ObjectTable::Contains( ComObject^ object )
	{
		Monitor::Enter( m_SyncObject );
		try
		{
			return m_Table->ContainsKey( object->ComPointer );
		}
		finally
		{
			Monitor::Exit( m_SyncObject );
		}
	}

	void ObjectTable::RegisterParent( ComObject^ object, ComObject^ owner )
	{
		if( owner != nullptr ) 
		{
			if( !m_Ancillary->ContainsKey( owner->ComPointer ) )
			{
				m_Ancillary->Add( owner->ComPointer, gcnew List<ComObject^>() );
			}
			m_Ancillary[owner->ComPointer]->Add( object );
		}
	}

	void ObjectTable::Add( ComObject^ object, ComObject^ owner )
	{
		if( object == nullptr )
			throw gcnew ArgumentNullException( "comObject" );

		// Record tracking information
		object->SetCreationTime( static_cast<int>( Configuration::Timer->ElapsedMilliseconds ) );
		if( Configuration::EnableObjectTracking )
			object->SetSource( gcnew StackTrace( 2, true ) );

		// Add to the table
		Monitor::Enter( m_SyncObject );
		try
		{
			m_Table->Add( object->ComPointer, object );
			RegisterParent( object, owner );
			
			ObjectAdded( nullptr, gcnew ObjectTableEventArgs( object ) );
		}
		finally
		{
			Monitor::Exit( m_SyncObject );
		}
	}

	bool ObjectTable::Remove( ComObject^ object )
	{
		if( object == nullptr )
			throw gcnew ArgumentNullException( "comObject" );

		Monitor::Enter( m_SyncObject );
		try
		{
			if( !m_Table->ContainsKey( object->ComPointer ) )
				return false;

			m_Table->Remove( object->ComPointer );
		
			// If the object has ancillary objects, destroy them.
			if( m_Ancillary->ContainsKey( object->ComPointer ) )
			{
				for each( ComObject^ ancillary in m_Ancillary[object->ComPointer])
				{
					// By setting the owner to nullptr just before we release this object,
					// we prevent an exception being thrown about the inability to release
					// ancillary objects (since this delete call causes us to go through
					// the same machinery that users use to Dispose() objects, and they should
					// not be allowed to do that to ancillary objects).
					ancillary->Owner = nullptr;
					delete ancillary;
				}
				
				m_Ancillary[object->ComPointer]->Clear();
			}
						
			ObjectRemoved( nullptr, gcnew ObjectTableEventArgs( object ) );
		}
		finally
		{
			Monitor::Exit( m_SyncObject );
		}
		return true;
	}

	String^ ObjectTable::ReportLeaks()
	{
		StringBuilder^ output = gcnew StringBuilder();

		Monitor::Enter( m_SyncObject );
		try
		{
			for each( KeyValuePair<IntPtr, ComObject^> pair in m_Table )
			{
				output->AppendFormat( CultureInfo::InvariantCulture, "Object of type {0} was not disposed. Stack trace of object creation:\n", pair.Value->GetType() );

				if( pair.Value->CreationSource == nullptr )
					continue;

				for each( StackFrame^ frame in pair.Value->CreationSource->GetFrames() )
				{
					if( frame->GetFileLineNumber() == 0 )
					{
						// Compiler autogenerated functions and the like can cause stack frames with no info;
						// that's the only time the line number is 0 and since it's not a useful frame to see,
						// we'll skip it
						continue;
					}

					output->AppendFormat( CultureInfo::InvariantCulture, "\t{0}({1},{2}): {3}\n",
						frame->GetFileName(),
						frame->GetFileLineNumber(),
						frame->GetFileColumnNumber(),
						frame->GetMethod() );
				}
			}

			output->AppendFormat( CultureInfo::InvariantCulture, "Total of {0} objects still alive.\n", m_Table->Count );
		}
		finally
		{
			Monitor::Exit( m_SyncObject );
		}
		return output->ToString();
	}

	ReadOnlyCollection<ComObject^>^ ObjectTable::Objects::get()
	{
		return gcnew ReadOnlyCollection<ComObject^>( gcnew List<ComObject^>( m_Table->Values ) );
	}

	Object^ ObjectTable::SyncObject::get()
	{
		return m_SyncObject;
	}
}