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

#include <unknwn.h>

namespace SlimDX
{
	ref class ComObject;

#ifdef XMLDOCS
	ref class Configuration;
#endif

	public ref class ObjectTableEventArgs : System::EventArgs
	{
	public:
		/// <summary>
		/// The <see cref="ComObject">COM object</see> that this notification refers to.
		/// </summary>
		property ComObject^ ComObject;

		ObjectTableEventArgs( SlimDX::ComObject^ comObject )
		{
			ComObject = comObject;
		}
	};

	/// <summary>
	/// Maintains a list of all the <see cref="ComObject">COM objects</see> managed by SlimDX.
	/// </summary>
	public ref class ObjectTable sealed
	{
	private:
		static ObjectTable();
		ObjectTable();

		static System::Collections::Generic::Dictionary<System::IntPtr, ComObject^>^ m_Table;
		static System::Collections::Generic::Dictionary<System::IntPtr, System::Collections::Generic::List<ComObject^>^>^ m_Ancillary;
		static Object^ m_SyncObject;

		static void OnExit( System::Object^ sender, System::EventArgs^ e );

	internal:
		static ComObject^ Find( System::IntPtr nativeObject );
		static bool Contains( ComObject^ object );

		static void RegisterParent(ComObject^ comObject, ComObject^ owner);

	public:
		/// <summary>
		/// Gets a list of all the <see cref="ComObject">COM objects</see> tracked by SlimDX.
		/// </summary>
		/// <remarks>
		/// Access through this member is NOT thread-safe. Use <see cref="SyncObject" />
		/// to protect multithreaded access if necessary.
		/// </remarks>
		static property System::Collections::ObjectModel::ReadOnlyCollection<ComObject^>^ Objects
		{
			System::Collections::ObjectModel::ReadOnlyCollection<ComObject^>^ get();
		}

		/// <summary>
		/// Occurs after a new object has been added to the object table.
		/// </summary>
		static event System::EventHandler<ObjectTableEventArgs^>^ ObjectAdded;

		/// <summary>
		/// Occurs after an object has been removed from the object table.
		/// </summary>
		static event System::EventHandler<ObjectTableEventArgs^>^ ObjectRemoved;

		/// <summary>
		/// Gets the synchronization object used by the ObjectTable. This object can be used to safely
		/// access the internal object list.
		/// </summary>
		/// <remarks>
		/// This member should only be used if it is necesssary to access the <see cref="Objects" /> property
		/// in a thread-safe manner, and should not be used as anything other than a parameter for
		/// functions in <see cref="System::Threading::Monitor" />.
		/// </remarks>
		static property Object^ SyncObject
		{
			Object^ get();
		}
		
		/// <summary>
		/// Adds a <see cref="ComObject">COM object</see> to the table. This will set the object's <see cref="SlimDX::ComObject"/><c>::CreationSource</c> property if
		/// <see cref="SlimDX::Configuration"/><c>::EnableObjectTracking</c>object tracking is on.
		/// </summary>
		/// <param name="comObject">The object to add.</param>
		static void Add( ComObject^ comObject, ComObject^ owner );
		
		/// <summary>
		/// Removes a <see cref="SlimDX::ComObject">COM object</see> from the table.
		/// </summary>
		/// <param name="comObject">The object to remove.</param>
		/// <returns><c>true</c> if the object was in the table and was removed; otherwise, <c>false</c>.</returns>
		static bool Remove( ComObject^ comObject );
		
		/// <summary>
		/// Generates a report of all outstanding COM objects (objects that have not been disposed)
		/// tracked by SlimDX. The report includes the object's type and a stack trace to its creation point.
		/// </summary>
		/// <returns>A string containing the leak report.</returns>
		static System::String^ ReportLeaks();
	};
}