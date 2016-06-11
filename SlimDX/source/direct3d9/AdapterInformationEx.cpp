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

#include "Direct3D9Exception.h"
#include "Direct3DEx.h"
#include "AdapterDetails.h"
#include "AdapterInformationEx.h"
#include "DisplayModeEx.h"

using namespace System;
using namespace System::Collections::Generic;

namespace SlimDX
{
namespace Direct3D9
{
	AdapterInformationEx::AdapterInformationEx( IDirect3D9Ex *direct3D, unsigned int adapter, bool checkWhql )
	{
		m_direct3D = direct3D;
		m_Adapter = static_cast<int>( adapter );
		details = gcnew AdapterDetails( direct3D, adapter, checkWhql );
	}

	IntPtr AdapterInformationEx::Monitor::get()
	{
		return IntPtr( m_direct3D->GetAdapterMonitor( m_Adapter ) );
	}

	DisplayModeEx AdapterInformationEx::CurrentDisplayMode::get()
	{
		D3DDISPLAYMODEEX d3ddm = {0};
		d3ddm.Size = sizeof(D3DDISPLAYMODEEX);
        HRESULT hr = m_direct3D->GetAdapterDisplayModeEx( m_Adapter, &d3ddm, NULL );
		RECORD_D3D9( hr );

		return DisplayModeEx::FromUnmanaged(d3ddm);
	}

    Capabilities^ AdapterInformationEx::GetCaps( DeviceType type )
    {
		D3DCAPS9 caps;
		HRESULT hr = m_direct3D->GetDeviceCaps( m_Adapter, static_cast<D3DDEVTYPE>( type ), &caps );
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew Capabilities( caps );
    }

    DisplayModeExCollection^ AdapterInformationEx::GetDisplayModes( DisplayModeFilter filter )
    {
        return gcnew DisplayModeExCollection( m_direct3D, m_Adapter, filter );
    }
}
}