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
#include "stdafx.h"

#include "../stack_array.h"

#include "../dxgi/Factory1.h"

#include "Direct3D11Exception.h"

#include "DeviceContext11.h"
#include "CounterCapabilities11.h"
#include "CounterMetadata11.h"
#include "Device11.h"

using namespace System;
using namespace System::Reflection;

namespace SlimDX
{
namespace Direct3D11
{
	Device::Device( DXGI::Adapter^ adapter )
	{
		Initialize( adapter, DriverType::Unknown, DeviceCreationFlags::None, NULL, 0 );
	}

	Device::Device( DXGI::Adapter^ adapter, DeviceCreationFlags flags )
	{
		Initialize( adapter, DriverType::Unknown, flags, NULL, 0 );
	}

	Device::Device( DXGI::Adapter^ adapter, DeviceCreationFlags flags, ... array<Direct3D11::FeatureLevel>^ featureLevels )
	{
		if( featureLevels == nullptr || featureLevels->Length == 0 )
			Initialize( adapter, DriverType::Unknown, flags, NULL, 0 );
		else
		{
			pin_ptr<Direct3D11::FeatureLevel> pinnedLevels = &featureLevels[0];
			Initialize( adapter, DriverType::Unknown, flags, reinterpret_cast<const D3D_FEATURE_LEVEL*>( pinnedLevels ), featureLevels->Length );
		}
	}

	Device::Device( DriverType driverType )
	{
		Initialize( nullptr, driverType, DeviceCreationFlags::None, NULL, 0 );
	}

	Device::Device( DriverType driverType, DeviceCreationFlags flags )
	{
		Initialize( nullptr, driverType, flags, NULL, 0 );
	}

	Device::Device( DriverType driverType, DeviceCreationFlags flags, ... array<Direct3D11::FeatureLevel>^ featureLevels )
	{
		if( featureLevels == nullptr || featureLevels->Length == 0 )
			Initialize( nullptr, driverType, flags, NULL, 0 );
		else
		{
			pin_ptr<Direct3D11::FeatureLevel> pinnedLevels = &featureLevels[0];
			Initialize( nullptr, driverType, flags, reinterpret_cast<const D3D_FEATURE_LEVEL*>( pinnedLevels ), featureLevels->Length );
		}
	}

	void Device::Initialize( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags, const D3D_FEATURE_LEVEL *featureLevels, int count )
	{
		IDXGIAdapter* nativeAdapter = adapter == nullptr ? 0 : adapter->InternalPointer;
		ID3D11Device* device = 0;
		ID3D11DeviceContext* context = NULL;

		HRESULT hr = D3D11CreateDevice( nativeAdapter, static_cast<D3D_DRIVER_TYPE>( driverType ), NULL, static_cast<UINT>( flags ), 
			featureLevels, count, D3D11_SDK_VERSION, &device, NULL, &context );

		if( RECORD_D3D11( hr ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );

		Construct( device );

		immediateContext = DeviceContext::FromPointer( context, this );
	}

	SlimDX::DXGI::Factory^ Device::Factory::get()
	{
		IDXGIDevice *device = 0;
		if (RECORD_D3D11(InternalPointer->QueryInterface(IID_IDXGIDevice, reinterpret_cast<void**>(&device))).IsFailure)
			return nullptr;

		IDXGIAdapter *adapter = 0;
		HRESULT hr = device->GetAdapter(&adapter);
		if (FAILED(hr))
			device->Release();

		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		SlimDX::DXGI::Factory^ result = nullptr;

		IDXGIFactory1 *factory1;
		hr = adapter->GetParent(IID_IDXGIFactory1, reinterpret_cast<void**>(&factory1));
		if (SUCCEEDED(hr))
			result = SlimDX::DXGI::Factory1::FromPointer(factory1, this);
		else
		{
			IDXGIFactory *factory;
			hr = adapter->GetParent(IID_IDXGIFactory, reinterpret_cast<void**>(&factory));
			if (SUCCEEDED(hr))
				result = SlimDX::DXGI::Factory::FromPointer(factory, this);
		}

		adapter->Release();
		device->Release();

		return result;
	}

	DeviceCreationFlags Device::CreationFlags::get()
	{
		return static_cast<DeviceCreationFlags>( InternalPointer->GetCreationFlags() );
	}

	Result Device::DeviceRemovedReason::get()
	{
		return Result( InternalPointer->GetDeviceRemovedReason() );
	}

	Direct3D11::FeatureLevel Device::FeatureLevel::get()
	{
		return static_cast<Direct3D11::FeatureLevel>( InternalPointer->GetFeatureLevel() );
	}

	DeviceContext^ Device::ImmediateContext::get()
	{
		if (immediateContext == nullptr)
		{
			ID3D11DeviceContext *context;
			InternalPointer->GetImmediateContext(&context);
			immediateContext = DeviceContext::FromPointer(context, this);
		}

		return immediateContext;
	}
	
	bool Device::IsReferenceDevice::get()
	{
		ID3D11SwitchToRef *switcher;

		HRESULT hr = InternalPointer->QueryInterface(IID_ID3D11SwitchToRef, reinterpret_cast<void**>(&switcher));
		if (FAILED(hr))
			return false;

		bool result = switcher->GetUseRef() != 0;
		switcher->Release();

		return result;
	}

	System::String^ Device::DebugName::get()
	{
		char name[1024];
		UINT size = sizeof(name) - 1;

		if (FAILED(InternalPointer->GetPrivateData(WKPDID_D3DDebugObjectName, &size, name)))
			return "";

		name[size] = 0;
		return gcnew System::String(name);
	}
	
	void Device::DebugName::set(System::String^ value)
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

	CounterCapabilities Device::GetCounterCapabilities()
	{
		D3D11_COUNTER_INFO info;
		InternalPointer->CheckCounterInfo( &info );
		return CounterCapabilities( info );
	}
	
	CounterMetadata Device::GetCounterMetadata( CounterKind counterKind )
	{
		D3D11_COUNTER_DESC nativeDescription;
		nativeDescription.Counter = static_cast<D3D11_COUNTER>( counterKind );

		D3D11_COUNTER_TYPE type;
		UINT count = 0;
		UINT nameLength = 0;
		UINT unitsLength = 0;
		UINT descriptionLength = 0;
		if( RECORD_D3D11( InternalPointer->CheckCounter( &nativeDescription, &type, &count, 0, &nameLength, 0, &unitsLength, 0, &descriptionLength ) ).IsFailure )
			return CounterMetadata();
		
		stack_array<char> nameChars = stackalloc( char, nameLength );
		stack_array<char> unitsChars = stackalloc( char, unitsLength );
		stack_array<char> descriptionChars = stackalloc( char, descriptionLength );

		if( RECORD_D3D11( InternalPointer->CheckCounter( &nativeDescription, &type, &count, &nameChars[ 0 ], &nameLength, &unitsChars[ 0 ], &unitsLength, &descriptionChars[ 0 ], &descriptionLength ) ).IsFailure )
			return CounterMetadata();
			
		return CounterMetadata( static_cast<CounterType>( type ), count, gcnew String( &nameChars[ 0 ] ), gcnew String( &unitsChars[ 0 ] ), gcnew String( &descriptionChars[ 0 ] ) );	
	}
	
	FormatSupport Device::CheckFormatSupport( DXGI::Format format )
	{
		UINT support = 0;
		InternalPointer->CheckFormatSupport( static_cast<DXGI_FORMAT>( format ), &support );
		return static_cast<FormatSupport>( support );
	}

	ComputeShaderFormatSupport Device::CheckComputeShaderFormatSupport( DXGI::Format format )
	{
		D3D11_FEATURE_DATA_FORMAT_SUPPORT2 support;
		support.InFormat = static_cast<DXGI_FORMAT>( format );

		HRESULT hr = InternalPointer->CheckFeatureSupport( D3D11_FEATURE_FORMAT_SUPPORT2, &support, sizeof( D3D11_FEATURE_DATA_FORMAT_SUPPORT2 ) );
		if( RECORD_D3D11( hr ).IsFailure )
			return ComputeShaderFormatSupport::None;

		return static_cast<ComputeShaderFormatSupport>( support.OutFormatSupport2 );
	}

	bool Device::CheckFeatureSupport( Feature feature )
	{
		if( feature == Feature::ShaderDoubles )
		{
			D3D11_FEATURE_DATA_DOUBLES support;

			HRESULT hr = InternalPointer->CheckFeatureSupport( D3D11_FEATURE_DOUBLES, &support, sizeof( D3D11_FEATURE_DATA_DOUBLES ) );
			if( RECORD_D3D11( hr ).IsFailure )
				return false;

			return support.DoublePrecisionFloatShaderOps != 0;
		}
		else if( feature == Feature::ComputeShaders )
		{
			D3D11_FEATURE_DATA_D3D10_X_HARDWARE_OPTIONS support;

			HRESULT hr = InternalPointer->CheckFeatureSupport( D3D11_FEATURE_D3D10_X_HARDWARE_OPTIONS, &support, sizeof( D3D11_FEATURE_DATA_D3D10_X_HARDWARE_OPTIONS ) );
			if( RECORD_D3D11( hr ).IsFailure )
				return false;

			return support.ComputeShaders_Plus_RawAndStructuredBuffers_Via_Shader_4_x != 0;
		}

		return false;
	}

	Result Device::CheckThreadingSupport( [Out] bool% supportsConcurrentResources, [Out] bool% supportsCommandLists )
	{
		D3D11_FEATURE_DATA_THREADING support;

		HRESULT hr = InternalPointer->CheckFeatureSupport( D3D11_FEATURE_THREADING, &support, sizeof( D3D11_FEATURE_DATA_THREADING ) );
		if( RECORD_D3D11( hr ).IsFailure )
		{
			supportsConcurrentResources = false;
			supportsCommandLists = false;
		}
		else
		{
			supportsConcurrentResources = support.DriverConcurrentCreates != 0;
			supportsCommandLists = support.DriverCommandLists != 0;
		}

		return Result::Last;
	}
	
	int Device::CheckMultisampleQualityLevels( DXGI::Format format, int sampleCount )
	{
		UINT result = 0;
		InternalPointer->CheckMultisampleQualityLevels( static_cast<DXGI_FORMAT>( format ), sampleCount, &result );
		return result;
	}

	Direct3D11::FeatureLevel Device::GetSupportedFeatureLevel()
	{
		D3D_FEATURE_LEVEL outputLevel;

		HRESULT hr = D3D11CreateDevice( NULL, D3D_DRIVER_TYPE_HARDWARE, NULL, 0, NULL, 0, D3D11_SDK_VERSION, NULL, &outputLevel, NULL );
		if( RECORD_D3D11( hr ).IsFailure )
			return static_cast<Direct3D11::FeatureLevel>(0);

		return static_cast<Direct3D11::FeatureLevel>( outputLevel );
	}

	Direct3D11::FeatureLevel Device::GetSupportedFeatureLevel( DXGI::Adapter^ adapter )
	{
		D3D_FEATURE_LEVEL outputLevel;

		HRESULT hr = D3D11CreateDevice( adapter->InternalPointer, D3D_DRIVER_TYPE_UNKNOWN, NULL, 0, NULL, 0, D3D11_SDK_VERSION, NULL, &outputLevel, NULL );
		if( RECORD_D3D11( hr ).IsFailure )
			return static_cast<Direct3D11::FeatureLevel>(0);

		return static_cast<Direct3D11::FeatureLevel>( outputLevel );
	}

	generic<typename T> where T : ComObject
	T Device::OpenSharedResource(System::IntPtr handle)
	{
		GUID guid = Utilities::GetNativeGuidForType( T::typeid );
		ID3D11Resource* resultPointer;

		HRESULT hr = InternalPointer->OpenSharedResource( handle.ToPointer(), guid, (void**)&resultPointer );
		if( RECORD_D3D11( hr ).IsFailure )
			return T();

		MethodInfo^ method = T::typeid->GetMethod( "FromPointer", BindingFlags::Public | BindingFlags::Static );
		T result = safe_cast<T>( method->Invoke( nullptr, gcnew array<Object^> { IntPtr( resultPointer ) } ) );

		resultPointer->Release();
		return result;
	}

#pragma warning(disable : 4947)
	Result Device::CreateWithSwapChain( DXGI::Adapter^ adapter, DeviceCreationFlags flags, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain )
	{
		return CreateWithSwapChain( adapter, DriverType::Unknown, flags, nullptr, swapChainDescription, device, swapChain );
	}

	Result Device::CreateWithSwapChain( DXGI::Adapter^ adapter, DeviceCreationFlags flags, array<Direct3D11::FeatureLevel>^ featureLevels, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain )
	{
		return CreateWithSwapChain( adapter, DriverType::Unknown, flags, featureLevels, swapChainDescription, device, swapChain );
	}

	Result Device::CreateWithSwapChain( DriverType driverType, DeviceCreationFlags flags, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain )
	{
		return CreateWithSwapChain( nullptr, driverType, flags, nullptr, swapChainDescription, device, swapChain );
	}

	Result Device::CreateWithSwapChain( DriverType driverType, DeviceCreationFlags flags, array<Direct3D11::FeatureLevel>^ featureLevels, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain )
	{
		return CreateWithSwapChain( nullptr, driverType, flags, featureLevels, swapChainDescription, device, swapChain );
	}

	// DEPRECATED METHODS //

	Result Device::CreateWithSwapChain( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain )
	{
		return CreateWithSwapChain( adapter, driverType, flags, nullptr, swapChainDescription, device, swapChain );
	}
#pragma warning(default : 4947)

	Result Device::CreateWithSwapChain( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags, 
		array<Direct3D11::FeatureLevel>^ featureLevels, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain )
	{
		IDXGIAdapter* nativeAdapter = adapter == nullptr ? 0 : adapter->InternalPointer;
		ID3D11Device* resultDevice = 0;
		ID3D11DeviceContext* context = 0;
		IDXGISwapChain* resultSwapChain = 0;
		DXGI_SWAP_CHAIN_DESC nativeDescription = swapChainDescription.CreateNativeVersion();

		const D3D_FEATURE_LEVEL* nativeLevels = NULL;
		pin_ptr<Direct3D11::FeatureLevel> pinnedLevels;
		int count = 0;

		if( featureLevels != nullptr && featureLevels->Length > 0 )
		{
			pinnedLevels = &featureLevels[0];
			nativeLevels = reinterpret_cast<const D3D_FEATURE_LEVEL*>( pinnedLevels );
			count = featureLevels->Length;
		}
		
		HRESULT hr = D3D11CreateDeviceAndSwapChain( nativeAdapter, static_cast<D3D_DRIVER_TYPE>( driverType ), 0, static_cast<UINT>( flags ), 
			nativeLevels, count, D3D11_SDK_VERSION, 
			&nativeDescription, &resultSwapChain, &resultDevice, NULL, &context );

		if( RECORD_D3D11( hr ).IsFailure )
		{
			device = nullptr;
			swapChain = nullptr;
		}
		else
		{
			device = FromPointer( resultDevice );
			swapChain = DXGI::SwapChain::FromPointer( resultSwapChain );
			context->Release();
		}

		return Result::Last;
	}
}
}
