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

#include "../ComObject.h"
#include "../Utilities.h"

#include "Direct3D.h"
#include "AdapterDetails.h"

#include "Direct3D9Exception.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	AdapterDetails::AdapterDetails( IDirect3D9 *direct3D, unsigned int adapter, bool checkWhql )
	{
		D3DADAPTER_IDENTIFIER9 ident = {0};
		DWORD flags = checkWhql ? D3DENUM_WHQL_LEVEL : 0;

		HRESULT hr = direct3D->GetAdapterIdentifier( adapter, flags, &ident );
		RECORD_D3D9( hr );
		
		this->adapter = adapter;
		driverName = gcnew String( ident.Driver );
		description = gcnew String( ident.Description );
		deviceName = gcnew String( ident.DeviceName );
		driverVersion = gcnew Version(
			HIWORD( ident.DriverVersion.HighPart ),
			LOWORD( ident.DriverVersion.HighPart ),
			HIWORD( ident.DriverVersion.LowPart ),
			LOWORD( ident.DriverVersion.LowPart )
		);
		vendorId = ident.VendorId;
		deviceId = ident.DeviceId;
		subsystemId = ident.SubSysId;
		revision = ident.Revision;
		deviceIdentifier = Utilities::ConvertNativeGuid( ident.DeviceIdentifier );
		whqlLevel = ident.WHQLLevel;
	}
	
	DateTime AdapterDetails::CertificationDate::get()
	{
		if( whqlLevel == 0 )
			return DateTime::MaxValue;
		if( whqlLevel == 1 )
			return DateTime::MinValue;
		return DateTime( 1999 + (whqlLevel >> 16), (whqlLevel & 0xFF00) >> 8, whqlLevel & 0xFF );
	}
}
}