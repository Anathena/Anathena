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

#include <dxgi.h>

#include "AdapterDescription1.h"

using namespace System;

namespace SlimDX
{
namespace DXGI
{ 	
	AdapterDescription1::AdapterDescription1( const DXGI_ADAPTER_DESC1& native )
	{
		m_Description = gcnew String( native.Description );
		m_VendorId = native.VendorId;
		m_DeviceId = native.DeviceId;
		m_SubSysId = native.SubSysId;
		m_Revision = native.Revision;
		m_DedicatedVideoMemory = native.DedicatedVideoMemory;
		m_DedicatedSystemMemory = native.DedicatedSystemMemory;
		m_SharedSystemMemory = native.SharedSystemMemory;
		m_Luid = (static_cast<Int64>(native.AdapterLuid.HighPart) << 32) | native.AdapterLuid.LowPart;
		m_Flags = static_cast<AdapterFlags>(native.Flags);
	}

	String^ AdapterDescription1::Description::get()
	{
		return m_Description;
	}

	int AdapterDescription1::VendorId::get()
	{
		return m_VendorId;
	}

	int AdapterDescription1::DeviceId::get()
	{
		return m_DeviceId;
	}

	int AdapterDescription1::SubsystemId::get()
	{
		return m_SubSysId;
	}

	int AdapterDescription1::Revision::get()
	{
		return m_Revision;
	}

	System::Int64 AdapterDescription1::DedicatedVideoMemory::get()
	{
		return m_DedicatedVideoMemory;
	}

	System::Int64 AdapterDescription1::DedicatedSystemMemory::get()
	{
		return m_DedicatedSystemMemory;
	}

	System::Int64 AdapterDescription1::SharedSystemMemory::get()
	{
		return m_SharedSystemMemory;
	}

	Int64 AdapterDescription1::Luid::get()
	{
		return m_Luid;
	}

	AdapterFlags AdapterDescription1::Flags::get()
	{
		return m_Flags;
	}

	bool AdapterDescription1::operator == ( AdapterDescription1 left, AdapterDescription1 right )
	{
		return AdapterDescription1::Equals( left, right );
	}

	bool AdapterDescription1::operator != ( AdapterDescription1 left, AdapterDescription1 right )
	{
		return !AdapterDescription1::Equals( left, right );
	}

	int AdapterDescription1::GetHashCode()
	{
		return m_Description->GetHashCode() + m_VendorId.GetHashCode() + m_DeviceId.GetHashCode()
			 + m_SubSysId.GetHashCode() + m_Revision.GetHashCode() + m_DedicatedVideoMemory.GetHashCode()
			 + m_DedicatedSystemMemory.GetHashCode() + m_SharedSystemMemory.GetHashCode() + m_Luid.GetHashCode()
			 + m_Flags.GetHashCode();
	}

	bool AdapterDescription1::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<AdapterDescription1>( value ) );
	}

	bool AdapterDescription1::Equals( AdapterDescription1 value )
	{
		return ( m_Description == value.m_Description && m_VendorId == value.m_VendorId && m_DeviceId == value.m_DeviceId
			 && m_SubSysId == value.m_SubSysId && m_Revision == value.m_Revision && m_DedicatedVideoMemory == value.m_DedicatedVideoMemory
			 && m_DedicatedSystemMemory == value.m_DedicatedSystemMemory && m_SharedSystemMemory == value.m_SharedSystemMemory && m_Luid == value.m_Luid
			 && m_Flags == value.m_Flags );
	}

	bool AdapterDescription1::Equals( AdapterDescription1% value1, AdapterDescription1% value2 )
	{
		return ( value1.m_Description == value2.m_Description && value1.m_VendorId == value2.m_VendorId && value1.m_DeviceId == value2.m_DeviceId
			 && value1.m_SubSysId == value2.m_SubSysId && value1.m_Revision == value2.m_Revision && value1.m_DedicatedVideoMemory == value2.m_DedicatedVideoMemory
			 && value1.m_DedicatedSystemMemory == value2.m_DedicatedSystemMemory && value1.m_SharedSystemMemory == value2.m_SharedSystemMemory && value1.m_Luid == value2.m_Luid
			 && value1.m_Flags == value2.m_Flags );
	}
}
}
