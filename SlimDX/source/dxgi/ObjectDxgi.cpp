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
#include "DXGIException.h"
#include "ObjectDxgi.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Globalization;

namespace SlimDX
{
namespace DXGI
{
	generic<typename T> where T : DXGIObject, ref class
	T DXGIObject::GetParent()
	{
		IUnknown* unknown = 0;
		GUID guid = Utilities::GetNativeGuidForType(T::typeid);
		RECORD_DXGI(InternalPointer->GetParent(guid, reinterpret_cast<void**>(&unknown)));
		if(Result::Last.IsFailure)
			return T();

		if(ObjectTable::Find(IntPtr(unknown)) != nullptr)
		{
			unknown->Release();
			return safe_cast<T>(ObjectTable::Find(IntPtr(unknown)));
		}

		BindingFlags flags = BindingFlags::Static | BindingFlags::InvokeMethod | BindingFlags::NonPublic;
		array<System::Object^>^ args = gcnew array<System::Object^>( 2 );
		args[ 0 ] = IntPtr( unknown );
		args[ 1 ] = this;

		// Trying to invoke "FromPointer" directly will choose the IntPtr overload since it's more
		// cumbersome to pass a native pointer as an argument here. The IntPtr overload is intended
		// to be the user-pointer overload, however, which isn't what we want; thus the thunk.
		T result = safe_cast<T>( T::typeid->InvokeMember( "FromPointerReflectionThunk", flags, nullptr, nullptr, args, CultureInfo::InvariantCulture ) );
		return result;
	}

	System::String^ DXGIObject::DebugName::get()
	{
		char name[1024];
		UINT size = sizeof(name) - 1;

		if (FAILED(InternalPointer->GetPrivateData(WKPDID_D3DDebugObjectName, &size, name)))
			return "";

		name[size] = 0;
		return gcnew System::String(name);
	}
	
	void DXGIObject::DebugName::set(System::String^ value)
	{
		if (!String::IsNullOrEmpty(value))
		{
			array<Byte>^ valueBytes = System::Text::ASCIIEncoding::ASCII->GetBytes(value);
			pin_ptr<Byte> pinnedValue = &valueBytes[0];

			InternalPointer->SetPrivateData(WKPDID_D3DDebugObjectName, value->Length, pinnedValue);
		}
		else
		{
			InternalPointer->SetPrivateData(WKPDID_D3DDebugObjectName, 0, 0);
		}
	}
}
}