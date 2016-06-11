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

#include "Enums.h"
#include "ObjectDxgi.h"

namespace SlimDX
{
	namespace DXGI
	{
		ref class Adapter;

		/// <summary>
		/// Implements methods for generating DXGI objects.
		/// </summary>
		/// <unmanaged>IDXGIFactory</unmanaged>
		public ref class Factory : DXGIObject
		{
			COMOBJECT(IDXGIFactory, Factory);
			
		private protected:
			// Terrible no-op constructor hack for Factory1.
			Factory( bool /*hack*/ ) { }

		public:
			value class AdapterEnumerator sealed : System::Collections::Generic::IEnumerable<Adapter^>, System::Collections::Generic::IEnumerator<Adapter^>
			{
			private:
				Factory^ m_factory;
				Adapter^ m_current;
				int m_index;

			internal:
				AdapterEnumerator(Factory^ factory) : m_factory(factory), m_index(0) { }

			public:
				AdapterEnumerator GetEnumerator() { return *this; }
				virtual System::Collections::Generic::IEnumerator<Adapter^>^ GetEnumerator2() = System::Collections::Generic::IEnumerable<Adapter^>::GetEnumerator { return *this; }
				virtual System::Collections::IEnumerator^ GetEnumerater3() = System::Collections::IEnumerable::GetEnumerator { return *this; }

				virtual bool MoveNext();
				virtual void Reset() { m_index = 0; m_current = nullptr; }

				property Adapter^ Current { virtual Adapter^ get() { return m_current; } }
				property System::Object^ Current2 
				{
					virtual System::Object^ get() = System::Collections::IEnumerator::Current::get { return Current; }
				}
			};

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="Factory"/> class.
			/// </summary>
			Factory();

			/// <summary>
			/// Lazily enumerates the available adapters.
			/// </summary>
			/// <returns>A enumerator that can be used to obtain the available adapters.</returns>
			AdapterEnumerator EnumerateAdapters();
			
			/// <summary>
			/// Gets the number of available adapters.
			/// </summary>
			/// <returns>The number of available adapters.</returns>
			int GetAdapterCount();
			
			/// <summary>
			/// Gets the specified adapter.
			/// </summary>
			/// <param name="index">The index of the desired adapter.</param>
			/// <returns>The specified adapter, or <c>null</c> on failure.</returns>
			Adapter^ GetAdapter( int index );
		
			/// <summary>
			/// Creates a software adapter interface.
			/// </summary>
			/// <param name="softwareModule">The unmanaged HMODULE for the software adapter DLL.</param>
			/// <returns>The specified adapter, or <c>null</c> on failure.</returns>
			[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
			Adapter^ CreateSoftwareAdapter( System::IntPtr softwareModule );
			
			/// <summary>
			/// Creates a software adapater interface.
			/// </summary>
			/// <param name="softwareModule">The module for the software adapter DLL.</param>
			/// <returns>The specified adapter, or <c>null</c> on failure.</returns>
			[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
			Adapter^ CreateSoftwareAdapter( System::Reflection::Module^ softwareModule );
			
			/// <summary>
			/// Gets the window handle associated with the factory (the window through which the user signals fullscreen transitions).
			/// </summary>
			/// <returns>The window handle.</returns>
			System::IntPtr GetWindowAssociation();
			
			/// <summary>
			/// Sets the window handle associated with the factory (the window through which the user signals fullscreen transitions).
			/// </summary>
			/// <param name="handle">The window handle.</param>
			/// <param name="flags">Flags controlling window association behavior.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetWindowAssociation( System::IntPtr handle, WindowAssociationFlags flags );
		};
	}
}