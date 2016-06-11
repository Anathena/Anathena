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

#include <d3d11.h>

#include "Direct3D11Exception.h"

#include "Device11.h"
#include "DeviceChild11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{
	SlimDX::Direct3D11::Device^ DeviceChild::Device::get()
	{
		ID3D11Device* device = 0;
		InternalPointer->GetDevice( &device );
		return SlimDX::Direct3D11::Device::FromPointer( device );
	}

	System::String^ DeviceChild::DebugName::get()
	{
		char name[1024];
		UINT size = sizeof(name) - 1;

		if (FAILED(InternalPointer->GetPrivateData(WKPDID_D3DDebugObjectName, &size, name)))
			return "";

		name[size] = 0;
		return gcnew System::String(name);
	}
	
	void DeviceChild::DebugName::set(System::String^ value)
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
