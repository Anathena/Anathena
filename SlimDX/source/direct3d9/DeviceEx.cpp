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
#include "../ComObject.h"

#include "Direct3D9Exception.h"

#include "DeviceEx.h"
#include "PresentParameters.h"
#include "Resource.h"
#include "Surface.h"
#include "VertexBuffer.h"

using namespace System;
using namespace System::Drawing;

namespace SlimDX
{
namespace Direct3D9
{
	DeviceEx::DeviceEx( IDirect3DDevice9Ex* direct3d, ComObject^ owner )
	{
		Construct( direct3d, owner );
	}

	DeviceEx::DeviceEx( IntPtr pointer )
	{
		Construct( pointer, NativeInterface );
	}
	
	DeviceEx^ DeviceEx::FromPointer( IDirect3DDevice9Ex* pointer, ComObject^ owner, ComObjectFlags flags ) 
	{
		return ComObject::ConstructFromPointer<DeviceEx,IDirect3DDevice9Ex>( pointer, owner, flags );
	}
	
	DeviceEx^ DeviceEx::FromPointer( IntPtr pointer )
	{
		return ComObject::ConstructFromUserPointer<DeviceEx>( pointer );
	}

	void DeviceEx::Internal_Constructor( Direct3DEx^ direct3D, int adapter, DeviceType deviceType, System::IntPtr controlHandle,
				CreateFlags createFlags, D3DDISPLAYMODEEX* fullscreenDisplayMode, ... array<PresentParameters^>^ presentParameters )
	{
		IDirect3DDevice9Ex* device;
		stack_array<D3DPRESENT_PARAMETERS> d3dpp = stackalloc( D3DPRESENT_PARAMETERS, presentParameters->Length );

		for( int i = 0; i < presentParameters->Length; ++i )
			d3dpp[i] = presentParameters[i]->ToUnmanaged();

		HRESULT hr = direct3D->InternalPointer->CreateDeviceEx( adapter,
			static_cast<D3DDEVTYPE>( deviceType ),
			static_cast<HWND>( controlHandle.ToPointer() ), 
			static_cast<DWORD>( createFlags ),
			&d3dpp[0], fullscreenDisplayMode, &device );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		for( int i = 0; i < presentParameters->Length; ++i )
		{
			presentParameters[i]->BackBufferCount = d3dpp[i].BackBufferCount;
			presentParameters[i]->BackBufferFormat = static_cast<Format>( d3dpp[i].BackBufferFormat );
			presentParameters[i]->BackBufferWidth = d3dpp[i].BackBufferWidth;
			presentParameters[i]->BackBufferHeight = d3dpp[i].BackBufferHeight;
		}

		Construct(device);
	}

	DeviceEx::DeviceEx( Direct3DEx^ direct3D, int adapter, DeviceType deviceType, System::IntPtr controlHandle,
		CreateFlags createFlags, PresentParameters^ presentParameters )
	{
		Internal_Constructor( direct3D, adapter, deviceType, controlHandle, createFlags, NULL, presentParameters );
	}

	DeviceEx::DeviceEx( Direct3DEx^ direct3D, int adapter, DeviceType deviceType, System::IntPtr controlHandle,
		CreateFlags createFlags, PresentParameters^ presentParameters, DisplayModeEx fullscreenDisplayMode )
	{
		D3DDISPLAYMODEEX nativeMode = fullscreenDisplayMode.ToUnmanaged();
		Internal_Constructor( direct3D, adapter, deviceType, controlHandle, createFlags, &nativeMode, presentParameters );
	}

	DeviceEx::DeviceEx( Direct3DEx^ direct3D, int adapter, DeviceType deviceType, System::IntPtr controlHandle,
		CreateFlags createFlags, array<PresentParameters^>^ presentParameters )
	{
		Internal_Constructor( direct3D, adapter, deviceType, controlHandle, createFlags, NULL, presentParameters );
	}

	DeviceEx::DeviceEx( Direct3DEx^ direct3D, int adapter, DeviceType deviceType, System::IntPtr controlHandle,
		CreateFlags createFlags, array<PresentParameters^>^ presentParameters, array<DisplayModeEx>^ fullscreenDisplayModes )
	{
		stack_array<D3DDISPLAYMODEEX> nativeModes = stackalloc( D3DDISPLAYMODEEX, fullscreenDisplayModes->Length );

		for( int i = 0; i < fullscreenDisplayModes->Length; ++i )
			nativeModes[i] = fullscreenDisplayModes[i].ToUnmanaged();

		Internal_Constructor( direct3D, adapter, deviceType, controlHandle, createFlags, &nativeModes[0], presentParameters );
	}

	int DeviceEx::GpuThreadPriority::get()
	{
		int priority;
		HRESULT hr = InternalPointer->GetGPUThreadPriority( &priority );
		RECORD_D3D9( hr );

		return priority;
	}

	void DeviceEx::GpuThreadPriority::set( int priority )
	{
		HRESULT hr = InternalPointer->SetGPUThreadPriority( priority );
		RECORD_D3D9( hr );
	}

	int DeviceEx::MaximumFrameLatency::get()
	{
		int maxLatency;
		HRESULT hr = InternalPointer->GetMaximumFrameLatency( reinterpret_cast<UINT*>( &maxLatency ) );
		RECORD_D3D9( hr );

		return maxLatency;
	}

	void DeviceEx::MaximumFrameLatency::set( int maxLatency )
	{
		HRESULT hr = InternalPointer->SetMaximumFrameLatency( maxLatency );
		RECORD_D3D9( hr );
	}

	DeviceState DeviceEx::CheckDeviceState( System::IntPtr windowHandle )
	{
		HRESULT hr = InternalPointer->CheckDeviceState( static_cast<HWND>( windowHandle.ToPointer() ) );
		return static_cast<DeviceState>( hr );
	}

	ResourceResidency DeviceEx::CheckResourceResidency( Resource^ resource )
	{
		IDirect3DResource9* resourcePointer = resource->InternalPointer;
		HRESULT hr = InternalPointer->CheckResourceResidency( &resourcePointer, 1 );
		return static_cast<ResourceResidency>( hr );
	}

	ResourceResidency DeviceEx::CheckResourceResidency( array<Resource^>^ resources )
	{
		stack_array<IDirect3DResource9*> resourceArray = stackalloc( IDirect3DResource9*, resources->Length );
		for( int i = 0; i < resources->Length; i++ )
			resourceArray[i] = resources[i]->InternalPointer;

		HRESULT hr = InternalPointer->CheckResourceResidency( &resourceArray[0], resources->Length );
		return static_cast<ResourceResidency>( hr );
	}

	DisplayModeEx DeviceEx::GetDisplayModeEx( int swapChain )
	{
		D3DDISPLAYMODEEX nativeMode = {0};
		nativeMode.Size = sizeof(D3DDISPLAYMODEEX);
		HRESULT hr = InternalPointer->GetDisplayModeEx( swapChain, &nativeMode, NULL );
		RECORD_D3D9( hr );

		DisplayModeEx mode = DisplayModeEx::FromUnmanaged( nativeMode );
		return mode;
	}

	DisplayModeEx DeviceEx::GetDisplayModeEx( int swapChain, [Out] DisplayRotation% rotation )
	{
		D3DDISPLAYMODEEX nativeMode = {0};
		nativeMode.Size = sizeof(D3DDISPLAYMODEEX);
		D3DDISPLAYROTATION nativeRotation = D3DDISPLAYROTATION_IDENTITY;

		HRESULT hr = InternalPointer->GetDisplayModeEx( swapChain, &nativeMode, &nativeRotation );
		RECORD_D3D9( hr );

		rotation = static_cast<DisplayRotation>( nativeRotation );
		DisplayModeEx mode = DisplayModeEx::FromUnmanaged( nativeMode );
		return mode;
	}

	Result DeviceEx::PresentEx( SlimDX::Direct3D9::Present flags )
	{
		HRESULT hr = InternalPointer->PresentEx( 0, 0, 0, 0, static_cast<DWORD>( flags ) );
		RECORD_D3D9( hr );

		return Result::Last;
	}

	Result DeviceEx::PresentEx( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle )
	{
		RECT nativeSourceRect = { sourceRectangle.Left, sourceRectangle.Top, sourceRectangle.Right, sourceRectangle.Bottom };
		RECT nativeDestRect = { destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom };
	
		RECT *sourcePtr = NULL;
		RECT *destPtr = NULL;

		if( sourceRectangle != Rectangle::Empty )
			sourcePtr = &nativeSourceRect;
		if( destinationRectangle != Rectangle::Empty )
			destPtr = &nativeDestRect;
	
		HRESULT hr = InternalPointer->PresentEx( sourcePtr, destPtr, NULL, NULL, static_cast<DWORD>( flags ) );

		return RECORD_D3D9( hr );
	}

	Result DeviceEx::PresentEx( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, System::IntPtr windowOverride )
	{
		RECT nativeSourceRect = { sourceRectangle.Left, sourceRectangle.Top, sourceRectangle.Right, sourceRectangle.Bottom };
		RECT nativeDestRect = { destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom };
	
		RECT *sourcePtr = NULL;
		RECT *destPtr = NULL;

		if( sourceRectangle != Rectangle::Empty )
			sourcePtr = &nativeSourceRect;
		if( destinationRectangle != Rectangle::Empty )
			destPtr = &nativeDestRect;
	
		HRESULT hr = InternalPointer->PresentEx( sourcePtr, destPtr, static_cast<HWND>( windowOverride.ToPointer() ), NULL, static_cast<DWORD>( flags ) );

		return RECORD_D3D9( hr );
	}

	Result DeviceEx::PresentEx( SlimDX::Direct3D9::Present flags, System::Drawing::Rectangle sourceRectangle, System::Drawing::Rectangle destinationRectangle, System::IntPtr windowOverride, System::Drawing::Region^ region )
	{
		RECT nativeSourceRect = { sourceRectangle.Left, sourceRectangle.Top, sourceRectangle.Right, sourceRectangle.Bottom };
		RECT nativeDestRect = { destinationRectangle.Left, destinationRectangle.Top, destinationRectangle.Right, destinationRectangle.Bottom };

		RGNDATA nativeRegion;
		Graphics^ graphics = Graphics::FromHwnd( windowOverride );
		int count = GetRegionData( static_cast<HRGN>( region->GetHrgn(graphics).ToPointer() ), 0, NULL );
		GetRegionData( static_cast<HRGN>( region->GetHrgn(graphics).ToPointer() ), count, &nativeRegion );
		delete graphics;
	
		RECT *sourcePtr = NULL;
		RECT *destPtr = NULL;

		if( sourceRectangle != Rectangle::Empty )
			sourcePtr = &nativeSourceRect;
		if( destinationRectangle != Rectangle::Empty )
			destPtr = &nativeDestRect;
	
		HRESULT hr = InternalPointer->PresentEx( sourcePtr, destPtr, static_cast<HWND>( windowOverride.ToPointer() ), &nativeRegion, static_cast<DWORD>( flags ) );

		return RECORD_D3D9( hr );
	}
	
	Result DeviceEx::ResetEx( ... array<PresentParameters^>^ presentParameters )
	{
		stack_array<D3DPRESENT_PARAMETERS> d3dpp = stackalloc( D3DPRESENT_PARAMETERS, presentParameters->Length );
		for( int p = 0; p < presentParameters->Length; ++p )
			d3dpp[p] = presentParameters[p]->ToUnmanaged();

		HRESULT hr = InternalPointer->ResetEx( &d3dpp[0], NULL );
		RECORD_D3D9( hr );

		for( int p = 0; p < presentParameters->Length; ++p )
		{
			presentParameters[p]->BackBufferCount = d3dpp[p].BackBufferCount;
			presentParameters[p]->BackBufferFormat = static_cast<Format>( d3dpp[p].BackBufferFormat );
			presentParameters[p]->BackBufferWidth = d3dpp[p].BackBufferWidth;
			presentParameters[p]->BackBufferHeight = d3dpp[p].BackBufferHeight;
		}

		return Result::Last;
	}

	Result DeviceEx::ResetEx( PresentParameters^ presentParameters, DisplayModeEx fullscreenDisplayMode )
	{
		D3DPRESENT_PARAMETERS d3dpp = presentParameters->ToUnmanaged();
		D3DDISPLAYMODEEX nativeDisplayMode = fullscreenDisplayMode.ToUnmanaged();
		HRESULT hr = InternalPointer->ResetEx( &d3dpp, &nativeDisplayMode );
		RECORD_D3D9( hr );

		presentParameters->BackBufferCount = d3dpp.BackBufferCount;
		presentParameters->BackBufferFormat = static_cast<Format>( d3dpp.BackBufferFormat );
		presentParameters->BackBufferWidth = d3dpp.BackBufferWidth;
		presentParameters->BackBufferHeight = d3dpp.BackBufferHeight;

		return Result::Last;
	}

	Result DeviceEx::ResetEx( DisplayModeEx fullscreenDisplayMode, ... array<PresentParameters^>^ presentParameters )
	{
		stack_array<D3DPRESENT_PARAMETERS> d3dpp = stackalloc( D3DPRESENT_PARAMETERS, presentParameters->Length );
		for( int p = 0; p < presentParameters->Length; ++p )
			d3dpp[p] = presentParameters[p]->ToUnmanaged();

		D3DDISPLAYMODEEX nativeDisplayMode = fullscreenDisplayMode.ToUnmanaged();
		HRESULT hr = InternalPointer->ResetEx( &d3dpp[0], &nativeDisplayMode );
		RECORD_D3D9( hr );

		for( int p = 0; p < presentParameters->Length; ++p )
		{
			presentParameters[p]->BackBufferCount = d3dpp[p].BackBufferCount;
			presentParameters[p]->BackBufferFormat = static_cast<Format>( d3dpp[p].BackBufferFormat );
			presentParameters[p]->BackBufferWidth = d3dpp[p].BackBufferWidth;
			presentParameters[p]->BackBufferHeight = d3dpp[p].BackBufferHeight;
		}

		return Result::Last;
	}

	void DeviceEx::WaitForVBlank( int swapChain )
	{
		//This method will always return D3D_OK.
		InternalPointer->WaitForVBlank( swapChain );
	}

	Result DeviceEx::SetConvolutionMonoKernel(int width, int height, array<float>^ rowWeights, array<float>^ columnWeights)
	{
		pin_ptr<float> pinnedRows = rowWeights == nullptr ? nullptr : &rowWeights[0];
		pin_ptr<float> pinnedColumns = columnWeights == nullptr ? nullptr : &columnWeights[0];

		HRESULT hr = InternalPointer->SetConvolutionMonoKernel(width, height, pinnedRows, pinnedColumns);
		return RECORD_D3D9(hr);
	}

	Result DeviceEx::ComposeRects(Surface^ source, Surface^ destination, int rectangleCount, VertexBuffer^ sourceRectangleDescriptors, VertexBuffer^ destinationRectangleDescriptors, ComposeRectOperation operation, int xOffset, int yOffset)
	{
		HRESULT hr = InternalPointer->ComposeRects(source->InternalPointer, destination->InternalPointer, sourceRectangleDescriptors->InternalPointer,
			rectangleCount, destinationRectangleDescriptors->InternalPointer, static_cast<D3DCOMPOSERECTSOP>(operation), xOffset, yOffset);

		return RECORD_D3D9(hr);
	}
}
}