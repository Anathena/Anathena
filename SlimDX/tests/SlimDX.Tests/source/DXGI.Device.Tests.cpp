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

#include "IDXGIDeviceMock.h"

#include "Asserts.h"
#include "ComObjectMock.h"
#include "ScopedThrowOnError.h"

#include "IDXGIAdapterMock.h"

using namespace testing;
using namespace System;
using namespace SlimDX;
using namespace SlimDX::DXGI;

TEST( DXGI_DeviceTests, Constructor )
{
	IDXGIDeviceMock mockDevice;
	IComObject^ mockComObject = gcnew ComObjectMock( &mockDevice );
	
	EXPECT_CALL( mockDevice, QueryInterface( _, _ ) )
		.WillOnce( DoAll( SetArgumentPointee<1>( &mockDevice ), Return( S_OK ) ) );

	Device^ device = nullptr;
	ASSERT_NO_THROW( device = gcnew Device( mockComObject ) );
	
	delete mockComObject;
	delete device;
}

TEST( DXGI_DeviceTests, ConstructorCanThrow )
{
	IDXGIDeviceMock mockDevice;
	IComObject^ mockComObject = gcnew ComObjectMock( &mockDevice );

	EXPECT_CALL( mockDevice, QueryInterface( _, _ ) )
		.WillOnce( DoAll( SetArgumentPointee<1>( reinterpret_cast<void*>( 0 ) ), Return( E_NOINTERFACE ) ) );

	Device^ device = nullptr;
	ASSERT_MANAGED_THROW( device = gcnew Device( mockComObject ), DXGIException );
	
	delete mockComObject;
}
