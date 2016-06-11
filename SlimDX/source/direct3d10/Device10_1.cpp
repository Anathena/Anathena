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

#include <d3d10_1.h>

#include "../dxgi/Adapter.h"

#include "Device10_1.h"
#include "Direct3D10Exception.h"

using namespace System;
using namespace SlimDX::Direct3D10;

namespace SlimDX
{
namespace Direct3D10_1
{
	Device1::Device1( ID3D10Device1* pointer, ComObject^ owner )
		: Device( pointer, owner )
	{
	}
	
	Device1::Device1( IntPtr pointer )
		: Device( pointer )
	{
	}
	
	Device1^ Device1::FromPointer( ID3D10Device1* pointer, ComObject^ owner, ComObjectFlags flags )
	{
		return ComObject::ConstructFromPointer<Device1,ID3D10Device1>( pointer, owner, flags );
	}
	
	Device1^ Device1::FromPointer( IntPtr pointer )
	{
		return ComObject::ConstructFromUserPointer<Device1>( pointer );
	}

	Device1::Device1( DeviceCreationFlags flags, SlimDX::Direct3D10_1::FeatureLevel hardwareLevel )
	{
		ID3D10Device1* device = 0;
		RECORD_D3D10( D3D10CreateDevice1( 0, D3D10_DRIVER_TYPE_HARDWARE, 0, static_cast<UINT>( flags ),
			static_cast<D3D10_FEATURE_LEVEL1>( hardwareLevel ), D3D10_1_SDK_VERSION, &device ) );
		if( Result::Last.IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( device );
		InitializeSubclasses();
	}
	
	Device1::Device1( SlimDX::Direct3D10::DriverType driverType, DeviceCreationFlags flags, SlimDX::Direct3D10_1::FeatureLevel hardwareLevel )
	{
		ID3D10Device1* device = 0;
		RECORD_D3D10( D3D10CreateDevice1( 0, static_cast<D3D10_DRIVER_TYPE>( driverType ), 0, static_cast<UINT>( flags ),
			static_cast<D3D10_FEATURE_LEVEL1>( hardwareLevel ), D3D10_1_SDK_VERSION, &device ) );
		if( Result::Last.IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( device );
		InitializeSubclasses();
	}
	
	Device1::Device1( DXGI::Adapter^ adapter, SlimDX::Direct3D10::DriverType driverType, DeviceCreationFlags flags, SlimDX::Direct3D10_1::FeatureLevel hardwareLevel )
	{
		IDXGIAdapter* nativeAdapter = adapter == nullptr ? 0 : adapter->InternalPointer;
		ID3D10Device1* device = 0;
		RECORD_D3D10( D3D10CreateDevice1( nativeAdapter, static_cast<D3D10_DRIVER_TYPE>( driverType ), 0,
			static_cast<UINT>( flags ), static_cast<D3D10_FEATURE_LEVEL1>( hardwareLevel ), D3D10_1_SDK_VERSION, &device ) );
		if( Result::Last.IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( device );
		InitializeSubclasses();
	}

	SlimDX::Direct3D10_1::FeatureLevel Device1::FeatureLevel::get()
	{
		return static_cast<SlimDX::Direct3D10_1::FeatureLevel>( InternalPointer->GetFeatureLevel() );
	}
	
	Result Device1::CreateWithSwapChain( DXGI::Adapter^ adapter, SlimDX::Direct3D10::DriverType driverType, DeviceCreationFlags flags, SlimDX::Direct3D10_1::FeatureLevel hardwareLevel, DXGI::SwapChainDescription swapChainDescription, [Out] Device1^ %device, [Out] DXGI::SwapChain^ %swapChain )
	{
		IDXGIAdapter* nativeAdapter = adapter == nullptr ? 0 : adapter->InternalPointer;
		ID3D10Device1* resultDevice = 0;
		IDXGISwapChain* resultSwapChain = 0;
		DXGI_SWAP_CHAIN_DESC nativeDescription = swapChainDescription.CreateNativeVersion();
		
		if( RECORD_D3D10( D3D10CreateDeviceAndSwapChain1( nativeAdapter, static_cast<D3D10_DRIVER_TYPE>( driverType ),
			0, static_cast<UINT>( flags ), static_cast<D3D10_FEATURE_LEVEL1>( hardwareLevel ), D3D10_1_SDK_VERSION,
			&nativeDescription, &resultSwapChain, &resultDevice ) ).IsSuccess )
		{
			device = FromPointer( resultDevice );
			swapChain = DXGI::SwapChain::FromPointer( resultSwapChain );
		}
		else
		{
			device = nullptr;
			swapChain = nullptr;
		}
		
		return Result::Last;
	}
}
}