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

#include "../DataBox.h"
#include "../stack_array.h"
#include "../dxgi/Adapter.h"
#include "../dxgi/Factory1.h"

#include "Direct3D10Exception.h"

#include "Buffer.h"
#include "CounterCapabilities.h"
#include "CounterDescription.h"
#include "CounterMetadata.h"
#include "DepthStencilView.h"
#include "Device10.h"
#include "Device10_1.h"
#include "InputAssemblerWrapper.h"
#include "InputLayout.h"
#include "OutputMergerWrapper.h"
#include "Predicate.h"
#include "RasterizerWrapper.h"
#include "RenderTargetView.h"
#include "Resource10.h"
#include "ResourceRegion.h"
#include "ShaderResourceView.h"
#include "StreamOutputWrapper.h"
#include "VertexShaderWrapper.h"
#include "PixelShaderWrapper.h"
#include "GeometryShaderWrapper.h"

using namespace System;
using namespace System::Reflection;

namespace SlimDX
{
namespace Direct3D10
{
	void Device::InitializeSubclasses()
	{
		m_InputAssembler = gcnew InputAssemblerWrapper( InternalPointer );
		m_OutputMerger = gcnew OutputMergerWrapper( InternalPointer );
		m_StreamOutput = gcnew StreamOutputWrapper( InternalPointer );
		m_Rasterizer = gcnew RasterizerWrapper( InternalPointer );
		m_VertexShader = gcnew VertexShaderWrapper( InternalPointer );
		m_PixelShader = gcnew PixelShaderWrapper( InternalPointer );
		m_GeometryShader = gcnew GeometryShaderWrapper( InternalPointer );
	}

	Device::Device( ID3D10Device* pointer, ComObject^ owner )
	{
		Construct( pointer, owner );
		InitializeSubclasses();
	}
	
	Device::Device( IntPtr pointer )
	{
		Construct( pointer, NativeInterface );
		InitializeSubclasses();
	}
	
	Device^ Device::FromPointer( ID3D10Device* pointer, ComObject^ owner, ComObjectFlags flags )
	{
		void* pointer1;
		HRESULT hr = pointer->QueryInterface(IID_ID3D10Device1, &pointer1);
		if(SUCCEEDED(hr))
		{
			pointer->Release();
			return SlimDX::Direct3D10_1::Device1::FromPointer( static_cast<ID3D10Device1*>( pointer1 ) );
		}

		return ComObject::ConstructFromPointer<Device,ID3D10Device>( pointer, owner, flags );
	}
	
	Device^ Device::FromPointer( IntPtr pointer )
	{
		ID3D10Device* origPointer = static_cast<ID3D10Device*>( pointer.ToPointer() );
		void* pointer1;
		HRESULT hr = origPointer->QueryInterface(IID_ID3D10Device1, &pointer1);
		if(SUCCEEDED(hr))
		{
			origPointer->Release();
			return SlimDX::Direct3D10_1::Device1::FromPointer(pointer);
		}

		return ComObject::ConstructFromUserPointer<Device>( pointer );
	}
	
	Device::Device( DeviceCreationFlags flags )
	{
		ID3D10Device* device = 0;
		RECORD_D3D10( D3D10CreateDevice( 0, D3D10_DRIVER_TYPE_HARDWARE, 0, static_cast<UINT>( flags ), D3D10_SDK_VERSION, &device ) );
		if( Result::Last.IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( device );
		InitializeSubclasses();
	}
	
	Device::Device( DriverType driverType, DeviceCreationFlags flags )
	{
		ID3D10Device* device = 0;
		RECORD_D3D10( D3D10CreateDevice( 0, static_cast<D3D10_DRIVER_TYPE>( driverType ), 0, static_cast<UINT>( flags ), D3D10_SDK_VERSION, &device ) );
		if( Result::Last.IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( device );
		InitializeSubclasses();
	}
	
	Device::Device( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags )
	{
		IDXGIAdapter* nativeAdapter = adapter == nullptr ? 0 : adapter->InternalPointer;
		ID3D10Device* device = 0;
		RECORD_D3D10( D3D10CreateDevice( nativeAdapter, static_cast<D3D10_DRIVER_TYPE>( driverType ), 0, static_cast<UINT>( flags ), D3D10_SDK_VERSION, &device ) );
		if( Result::Last.IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		Construct( device );
		InitializeSubclasses();
	}

	SlimDX::DXGI::Factory^ Device::Factory::get()
	{
		IDXGIDevice *device = 0;
		if (RECORD_D3D10(InternalPointer->QueryInterface(IID_IDXGIDevice, reinterpret_cast<void**>(&device))).IsFailure)
			return nullptr;

		IDXGIAdapter *adapter = 0;
		HRESULT hr = device->GetAdapter(&adapter);
		if (FAILED(hr))
			device->Release();

		if (RECORD_D3D10(hr).IsFailure)
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

	InputAssemblerWrapper^ Device::InputAssembler::get()
	{
		return m_InputAssembler;
	}
	
	OutputMergerWrapper^ Device::OutputMerger::get()
	{
		return m_OutputMerger;
	}
	
	StreamOutputWrapper^ Device::StreamOutput::get()
	{
		return m_StreamOutput;
	}
	
	RasterizerWrapper^ Device::Rasterizer::get()
	{
		return m_Rasterizer;
	}

	VertexShaderWrapper^ Device::VertexShader::get()
	{
		return m_VertexShader;
	}

	PixelShaderWrapper^ Device::PixelShader::get()
	{
		return m_PixelShader;
	}

	GeometryShaderWrapper^ Device::GeometryShader::get()
	{
		return m_GeometryShader;
	}
	
	DeviceCreationFlags Device::CreationFlags::get()
	{
		return static_cast<DeviceCreationFlags>( InternalPointer->GetCreationFlags() );
	}

	Result Device::DeviceRemovedReason::get()
	{
		return Result( InternalPointer->GetDeviceRemovedReason() );
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
		D3D10_COUNTER_INFO info;
		InternalPointer->CheckCounterInfo( &info );
		return CounterCapabilities( info );
	}
	
	CounterMetadata Device::GetCounterMetadata( CounterDescription description )
	{
		D3D10_COUNTER_DESC nativeDescription = description.CreateNativeVersion();
		D3D10_COUNTER_TYPE type;
		UINT count = 0;
		UINT nameLength = 0;
		UINT unitsLength = 0;
		UINT descriptionLength = 0;
		if( RECORD_D3D10( InternalPointer->CheckCounter( &nativeDescription, &type, &count, 0, &nameLength, 0, &unitsLength, 0, &descriptionLength ) ).IsFailure )
			return CounterMetadata();
		
		stack_array<char> nameChars = stackalloc( char, nameLength );
		stack_array<char> unitsChars = stackalloc( char, unitsLength );
		stack_array<char> descriptionChars = stackalloc( char, descriptionLength );

		if( RECORD_D3D10( InternalPointer->CheckCounter( &nativeDescription, &type, &count, &nameChars[ 0 ], &nameLength, &unitsChars[ 0 ], &unitsLength, &descriptionChars[ 0 ], &descriptionLength ) ).IsFailure )
			return CounterMetadata();
			
		return CounterMetadata( static_cast<CounterType>( type ), count, gcnew String( &nameChars[ 0 ] ), gcnew String( &unitsChars[ 0 ] ), gcnew String( &descriptionChars[ 0 ] ) );	
	}
	
	FormatSupport Device::CheckFormatSupport( DXGI::Format format )
	{
		UINT support = 0;
		InternalPointer->CheckFormatSupport( static_cast<DXGI_FORMAT>( format ), &support );
		return static_cast<FormatSupport>( support );
	}
	
	int Device::CheckMultisampleQualityLevels( DXGI::Format format, int sampleCount )
	{
		UINT result = 0;
		InternalPointer->CheckMultisampleQualityLevels( static_cast<DXGI_FORMAT>( format ), sampleCount, &result );
		return result;
	}

	generic<typename T> where T : ComObject
	T Device::OpenSharedResource(System::IntPtr handle)
	{
		GUID guid = Utilities::GetNativeGuidForType( T::typeid );
		ID3D10Resource* resultPointer;

		HRESULT hr = InternalPointer->OpenSharedResource( handle.ToPointer(), guid, (void**)&resultPointer );
		if( RECORD_D3D10( hr ).IsFailure )
			return T();

		MethodInfo^ method = T::typeid->GetMethod( "FromPointer", BindingFlags::Public | BindingFlags::Static );
		T result = safe_cast<T>( method->Invoke( nullptr, gcnew array<Object^> { IntPtr( resultPointer ) } ) );

		resultPointer->Release();
		return result;
	}
	
	void Device::ClearDepthStencilView( DepthStencilView^ view, DepthStencilClearFlags flags, float depth, Byte stencil )
	{
		InternalPointer->ClearDepthStencilView( view->InternalPointer, static_cast<UINT>( flags ), depth, stencil );
	}
	
	void Device::ClearRenderTargetView( RenderTargetView^ view, Color4 color )
	{
		const float nativeColor[] = { color.Red, color.Green, color.Blue, color.Alpha };
		InternalPointer->ClearRenderTargetView( view->InternalPointer, nativeColor );
	}
	
	void Device::ClearState()
	{
		InternalPointer->ClearState();
	}
	
	Result Device::ClearAllObjects()
	{
		return RECORD_D3D10( D3DX10UnsetAllDeviceObjects( InternalPointer ) );
	}
	
	void Device::CopyResource( Resource^ source, Resource^ destination )
	{
		InternalPointer->CopyResource( destination->InternalPointer, source->InternalPointer );
	}
	
	void Device::CopySubresourceRegion( Resource^ source, int sourceSubresource, ResourceRegion region, Resource^ destination, int destinationSubresource, int x, int y, int z )
	{
		D3D10_BOX nativeRegion = region.CreateNativeVersion();
		InternalPointer->CopySubresourceRegion( destination->InternalPointer, destinationSubresource, x, y, z, source->InternalPointer, sourceSubresource, &nativeRegion );
	}
	
	void Device::CopySubresourceRegion( Resource^ source, int sourceSubresource, Resource^ destination, int destinationSubresource, int x, int y, int z )
	{
		InternalPointer->CopySubresourceRegion( destination->InternalPointer, destinationSubresource, x, y, z, source->InternalPointer, sourceSubresource, NULL );
	}
	
	void Device::ResolveSubresource( Resource^ source, int sourceSubresource, Resource^ destination, int destinationSubresource, DXGI::Format format )
	{
		InternalPointer->ResolveSubresource( destination->InternalPointer, destinationSubresource, source->InternalPointer, sourceSubresource, static_cast<DXGI_FORMAT>( format ) );
	}
	
	void Device::UpdateSubresource( DataBox^ source, Resource^ resource, int subresource ) 
	{
		InternalPointer->UpdateSubresource( resource->InternalPointer, static_cast<UINT>( subresource), 0, source->Data->PositionPointer, source->RowPitch,source->SlicePitch);
	}

	void Device::UpdateSubresource( DataBox^ source, Resource^ resource, int subresource, ResourceRegion region ) 
	{
		D3D10_BOX nativeRegion = region.CreateNativeVersion();
		InternalPointer->UpdateSubresource( resource->InternalPointer, static_cast<UINT>( subresource), &nativeRegion, source->Data->PositionPointer, source->RowPitch,source->SlicePitch);
	}

	void Device::Draw( int vertexCount, int startVertexLocation )
	{
		InternalPointer->Draw( vertexCount, startVertexLocation );
	}
	
	void Device::DrawInstanced( int vertexCountPerInstance, int instanceCount, int startVertexLocation, int startInstanceLocation )
	{
		InternalPointer->DrawInstanced( vertexCountPerInstance, instanceCount, startVertexLocation, startInstanceLocation );
	}
	
	void Device::DrawIndexed( int indexCount, int startIndexLocation, int baseVertexLocation )
	{
		InternalPointer->DrawIndexed( indexCount, startIndexLocation, baseVertexLocation );
	}
	
	void Device::DrawIndexedInstanced( int indexCountPerInstance, int instanceCount, int startIndexLocation, int baseVertexLocation, int startInstanceLocation )
	{
		InternalPointer->DrawIndexedInstanced( indexCountPerInstance, instanceCount, startIndexLocation, baseVertexLocation, startInstanceLocation );
	}
	
	void Device::DrawAuto()
	{
		InternalPointer->DrawAuto();
	}
	
	void Device::Flush()
	{
		InternalPointer->Flush();
	}
	
	void Device::GenerateMips( ShaderResourceView^ view )
	{
		InternalPointer->GenerateMips( view->InternalPointer );
	}

	void Device::GetPredication( [Out] Predicate^ %predicate, bool %predicateValue )
	{
		ID3D10Predicate* pointer;
		BOOL value;

		InternalPointer->GetPredication( &pointer, &value );

		predicate = Predicate::FromPointer( pointer );
		predicateValue = value > 0;
	}

	void Device::SetPredication( Predicate^ predicate, bool predicateValue )
	{
		InternalPointer->SetPredication( predicate->InternalPointer, predicateValue );
	}
	
	Result Device::CreateWithSwapChain( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain )
	{
		IDXGIAdapter* nativeAdapter = adapter == nullptr ? 0 : adapter->InternalPointer;
		ID3D10Device* resultDevice = 0;
		IDXGISwapChain* resultSwapChain = 0;
		DXGI_SWAP_CHAIN_DESC nativeDescription = swapChainDescription.CreateNativeVersion();
		
		if( RECORD_D3D10( D3D10CreateDeviceAndSwapChain( nativeAdapter, static_cast<D3D10_DRIVER_TYPE>( driverType ), 0, static_cast<UINT>( flags ), D3D10_SDK_VERSION, &nativeDescription, &resultSwapChain, &resultDevice ) ).IsSuccess )
		{
			device = FromPointer( resultDevice );
			swapChain = DXGI::SwapChain::FromPointer( resultSwapChain );
		}
		else {
			device = nullptr;
			swapChain = nullptr;
		}
		
		return Result::Last;
	}
}
}
