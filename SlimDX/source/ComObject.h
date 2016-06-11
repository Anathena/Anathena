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
#pragma once

#include "ObjectTable.h"
#include "Configuration.h"
#include "Result.h"
#include "Utilities.h"
#include "InternalHelpers.h"

#ifdef XMLDOCS
using System::IntPtr;
using System::Diagnostics::StackTrace;
#endif

#include "ComObjectMacros.h"

namespace SlimDX
{	
	[System::Flags]
	enum class ComObjectFlags
	{
		None = 0,
		IsAncillary = 1,
		IsExternal = 2
	};
	
	public interface struct IComObject : System::IDisposable
	{
	public:
		property System::IntPtr ComPointer
		{
			virtual System::IntPtr get() = 0;
		}
	};

	/// <summary>
	/// The base class for all SlimDX types which represent COM interfaces.
	/// </summary>
	/// <unmanaged>IUnknown</unmanaged>
	public ref class ComObject abstract : IComObject
	{
	private:
		IUnknown* m_Unknown;
		ComObject^ m_Owner;
		ComObjectFlags m_Flags;
		System::Diagnostics::StackTrace^ m_Source;
		int m_CreationTime;

	private protected:
		ComObject();

		void Construct( IUnknown* pointer );
		void Construct( IUnknown* pointer, ComObject^ owner );
		void Construct( IUnknown* pointer, ComObject^ owner, bool addToTable );
		void Construct( System::IntPtr pointer, System::Guid guid );
		void Destruct();
		
		template< typename M, typename N >
		static M^ ConstructFromPointer( N* pointer, ComObject^ owner, ComObjectFlags flags ) 
		{
			// Since this method is called internally by SlimDX to essentially translate the results of native
			// API calls to their managed counterparts via the object table, we expect that a null pointer
			// might be passed, and that's okay. This differs from ConstructFromUserPointer.
			if( pointer == 0 )
				return nullptr;
			
			M^ tableEntry = safe_cast<M^>( SlimDX::ObjectTable::Find( static_cast<System::IntPtr>( pointer ) ) );
			if( tableEntry != nullptr )
			{
				if( static_cast<int>( flags & ComObjectFlags::IsAncillary ) == 0 ) 
					pointer->Release();
				return tableEntry;
			}

			M^ result = gcnew M( pointer, owner );
			result->SetFlags( flags );
			return result;
		}
		
		template< typename M >
		static M^ ConstructFromUserPointer( System::IntPtr pointer ) 
		{
			// This method gets called as a result of the user invoking the IntPtr overload of FromPointer
			// to create a SlimDX object from an externally-tracked native object. In this scenario, a
			// null pointer is a failure, so we throw.
			if( pointer == System::IntPtr::Zero )
				throw gcnew System::ArgumentNullException( "pointer" );

			M^ tableEntry = safe_cast<M^>( SlimDX::ObjectTable::Find( static_cast<System::IntPtr>( pointer ) ) );
			if( tableEntry != nullptr )
			{
				return tableEntry;
			}

			M^ result = gcnew M( pointer );
			result->SetFlags( ComObjectFlags::IsExternal );
			return result;
		}
		
	internal:
		property IUnknown* UnknownPointer
		{
			IUnknown* get();
		}

		property IUnknown* InternalPointer
		{
			IUnknown* get();
		}

		property ComObject^ Owner 
		{
			ComObject^ get();
			void set( ComObject^ value );
		}
		
		void SetFlags( ComObjectFlags flags );
		void SetSource( System::Diagnostics::StackTrace^ stack );
		void SetCreationTime( int time );
		
	public:
		/// <summary>
		/// Gets a value that indicates whether the object has been disposed.
		/// </summary>
		property bool Disposed
		{
			bool get();
		}
		
		/// <summary>
		/// Gets an <see cref="IntPtr">IntPtr</see> to the underlying native COM interface.
		/// </summary>
		property System::IntPtr ComPointer
		{
			virtual System::IntPtr get();
		}
		
		/// <summary>
		/// Gets a <see cref="StackTrace"/> to the location where the object was created.
		/// </summary>
		property System::Diagnostics::StackTrace^ CreationSource
		{
			System::Diagnostics::StackTrace^ get();
		}
		
		/// <summary>
		/// Gets the timestamp, in milliseconds, of the object'ss creation.
		/// </summary>
		property int CreationTime
		{
			int get();
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether or not the object is in the default allocation pool.
		/// </summary>
		property bool IsDefaultPool;
		
		/// <summary>
		/// Extra tag data stored along with the object. This member is intended for use by users of SlimDX
		/// and has no internal meaning to the library.
		/// </summary>
		property Object^ Tag;

		/// <summary>
		/// Releases all resources used by the <see cref="SlimDX::ComObject"/>.
		/// </summary>
		virtual ~ComObject();
	};
}