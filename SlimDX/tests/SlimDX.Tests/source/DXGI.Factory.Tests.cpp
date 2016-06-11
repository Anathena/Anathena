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

#include "IDXGIFactoryMock.h"

#include "Asserts.h"
#include "ScopedThrowOnError.h"

#include "IDXGIAdapterMock.h"

using namespace testing;
using namespace System;
using namespace SlimDX::DXGI;

TEST( DXGI_FactoryTests, GetAdapterCountCanReturnZero )
{
	IDXGIFactoryMock mockFactory;
	Factory^ factory = Factory::FromPointer( System::IntPtr( &mockFactory ) );
	
	EXPECT_CALL( mockFactory, EnumAdapters( _, _ ) )
		.WillOnce( Return( DXGI_ERROR_NOT_FOUND ) );
	
	int count = factory->GetAdapterCount();
	ASSERT_EQ( 0, count );

	delete factory;
}

TEST( DXGI_FactoryTests, GetAdapterCountCanReturnNonZero )
{
	IDXGIFactoryMock mockFactory;
	IDXGIAdapterMock mockAdapter0;
	IDXGIAdapterMock mockAdapter1;
	IDXGIAdapterMock mockAdapter2;
	Factory^ factory = Factory::FromPointer( System::IntPtr( &mockFactory ) );
	
	EXPECT_CALL( mockFactory, EnumAdapters( 0, _ ) )
		.WillOnce( DoAll( SetArgumentPointee<1>( &mockAdapter0 ), Return( S_OK ) ) );
	EXPECT_CALL( mockFactory, EnumAdapters( 1, _ ) )
		.WillOnce( DoAll( SetArgumentPointee<1>( &mockAdapter1 ), Return( S_OK ) ) );
	EXPECT_CALL( mockFactory, EnumAdapters( 2, _ ) )
		.WillOnce( DoAll( SetArgumentPointee<1>( &mockAdapter2 ), Return( S_OK ) ) );
	EXPECT_CALL( mockFactory, EnumAdapters( 3, _ ) )
		.WillOnce( Return( DXGI_ERROR_NOT_FOUND ) );
	
	int count = factory->GetAdapterCount();
	ASSERT_EQ( 3, count );

	delete factory;
}

//TEST( DXGI_FactoryTests, GetAdapter )
//{
//	IDXGIFactoryMock mockFactory;
//	IDXGIAdapterMock mockAdapter;
//	Factory^ factory = Factory::FromPointer( System::IntPtr( &mockFactory ) );
//	
//	EXPECT_CALL( mockFactory, EnumAdapters( 0, _ ) )
//		.WillOnce( DoAll( SetArgumentPointee<1>( &mockAdapter ), Return( S_OK ) ) );
//	
//	IAdapter^ adapter = factory->GetAdapter( 0 );
//	ASSERT_TRUE( adapter != nullptr );
//	ASSERT_TRUE( adapter->InternalPointer == &mockAdapter );
//
//	delete factory;
//}
//
//TEST( DXGI_FactoryTests, GetAdapterCanThrow )
//{
//	IDXGIFactoryMock mockFactory;
//	Factory^ factory = Factory::FromPointer( System::IntPtr( &mockFactory ) );
//
//	const int adapterIndex = 32;
//	EXPECT_CALL( mockFactory, EnumAdapters( adapterIndex, _ ) )
//		.WillOnce( Return( DXGI_ERROR_NOT_FOUND ) );
//	
//	ASSERT_MANAGED_THROW( factory->GetAdapter( adapterIndex ), DXGIException );
//	
//	delete factory;
//}
//
//TEST( DXGI_FactoryTests, GetAdapterCanFail )
//{
//	IDXGIFactoryMock mockFactory;
//	Factory^ factory = Factory::FromPointer( System::IntPtr( &mockFactory ) );
//
//	const int adapterIndex = 32;
//	EXPECT_CALL( mockFactory, EnumAdapters( adapterIndex, _ ) )
//		.WillOnce( Return( DXGI_ERROR_NOT_FOUND ) );
//	
//	{
//		SCOPED_THROW_ON_ERROR( false );
//		
//		IAdapter^ adapter = nullptr;
//		ASSERT_NO_THROW( adapter = factory->GetAdapter( adapterIndex ) );
//		
//		ASSERT_TRUE( adapter == nullptr );
//	}
//
//	delete factory;
//}