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

#include <d3d10.h>
#include <d3dx10.h>

#include "../stack_array.h"

#include "BlendState.h"
#include "DepthStencilState.h"
#include "DepthStencilView.h"
#include "OutputMergerWrapper.h"
#include "RenderTargetView.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	OutputMergerWrapper::OutputMergerWrapper( ID3D10Device* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "device" );
		m_Device = device;
	}
	
	void OutputMergerWrapper::DepthStencilState::set( SlimDX::Direct3D10::DepthStencilState^ value )
	{
		ID3D10DepthStencilState* oldState;
		int oldReference;
		m_Device->OMGetDepthStencilState( &oldState, reinterpret_cast<UINT*>( &oldReference ) );
		if( oldState != NULL )
			oldState->Release();
	
		if( value == nullptr )
			m_Device->OMSetDepthStencilState( 0, oldReference );
		else
			m_Device->OMSetDepthStencilState( value->InternalPointer, oldReference );
	}
	
	SlimDX::Direct3D10::DepthStencilState^ OutputMergerWrapper::DepthStencilState::get()
	{
		ID3D10DepthStencilState* oldState = 0;
		int oldReference = 0;
		m_Device->OMGetDepthStencilState( &oldState, reinterpret_cast<UINT*>( &oldReference ) );
		
		return SlimDX::Direct3D10::DepthStencilState::FromPointer( oldState );
	}
	
	void OutputMergerWrapper::DepthStencilReference::set( int value )
	{
		ID3D10DepthStencilState* oldState = 0;
		int oldReference = 0;
		m_Device->OMGetDepthStencilState( &oldState, reinterpret_cast<UINT*>( &oldReference ) );
	
		m_Device->OMSetDepthStencilState( oldState, value );
		if( oldState != NULL )
			oldState->Release();
	}
	
	int OutputMergerWrapper::DepthStencilReference::get()
	{
		ID3D10DepthStencilState* oldState = 0;
		int oldReference = 0;
		m_Device->OMGetDepthStencilState( &oldState, reinterpret_cast<UINT*>( &oldReference ) );
		if( oldState != NULL )
			oldState->Release();
		
		return oldReference;
	}
	
	void OutputMergerWrapper::BlendState::set( SlimDX::Direct3D10::BlendState^ value )
	{
		ID3D10BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		m_Device->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
		if( oldState != NULL )
			oldState->Release();
		
		if( value == nullptr )
			m_Device->OMSetBlendState( 0, oldFactor, oldMask );
		else 
			m_Device->OMSetBlendState( value->InternalPointer, oldFactor, oldMask );
	}
	
	SlimDX::Direct3D10::BlendState^ OutputMergerWrapper::BlendState::get()
	{
		ID3D10BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		m_Device->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
		
		return SlimDX::Direct3D10::BlendState::FromPointer( oldState );
	}
	
	void OutputMergerWrapper::BlendFactor::set( Color4 value )
	{
		ID3D10BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		m_Device->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
		
		float newFactor[4] = { value.Red, value.Green, value.Blue, value.Alpha };
		m_Device->OMSetBlendState( oldState, newFactor, oldMask );
		if( oldState != NULL )
			oldState->Release();
	}
	
	Color4 OutputMergerWrapper::BlendFactor::get()
	{
		ID3D10BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		m_Device->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
		if( oldState != NULL )
			oldState->Release();
		
		return Color4( oldFactor[3], oldFactor[0], oldFactor[1], oldFactor[2] );
	}
	
	void OutputMergerWrapper::BlendSampleMask::set( int value )
	{
		ID3D10BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		m_Device->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
	
		m_Device->OMSetBlendState( oldState, oldFactor, value );
		if( oldState != NULL )
			oldState->Release();
	}
	
	int OutputMergerWrapper::BlendSampleMask::get()
	{
		ID3D10BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		m_Device->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
		if( oldState != NULL )
			oldState->Release();
		
		return oldMask;
	}
	
	void OutputMergerWrapper::SetTargets( RenderTargetView^ renderTargetView )
	{
		SetTargets( nullptr, renderTargetView );
	}
	
	void OutputMergerWrapper::SetTargets( DepthStencilView^ depthStencilView, RenderTargetView^ renderTargetView )
	{
		ID3D10DepthStencilView *nativeDSV = depthStencilView == nullptr ? 0 : static_cast<ID3D10DepthStencilView*>( depthStencilView->InternalPointer );
		ID3D10RenderTargetView *nativeRTV[] = { renderTargetView == nullptr ? 0 : static_cast<ID3D10RenderTargetView*>( renderTargetView->InternalPointer ) };
		
		m_Device->OMSetRenderTargets( 1, nativeRTV, nativeDSV );
	}

	void OutputMergerWrapper::SetTargets( ... array<RenderTargetView^>^ renderTargets )
	{
		SetTargets( nullptr, renderTargets );
	}

	void OutputMergerWrapper::SetTargets( DepthStencilView^ depthStencilView, ... array<RenderTargetView^>^ renderTargets )
	{
		ID3D10DepthStencilView *nativeDSV = depthStencilView == nullptr ? 0 : static_cast<ID3D10DepthStencilView*>( depthStencilView->InternalPointer );
		ID3D10RenderTargetView* nativeRTVs[D3D10_SIMULTANEOUS_RENDER_TARGET_COUNT];
		
		if( renderTargets == nullptr )
		{
			m_Device->OMSetRenderTargets( 0, 0, nativeDSV );
		}
		else 
		{
			for( int i = 0; i < renderTargets->Length; ++i )
				nativeRTVs[ i ] = renderTargets[ i ] == nullptr ? 0 : static_cast<ID3D10RenderTargetView*>( renderTargets[ i ]->InternalPointer );
			m_Device->OMSetRenderTargets( renderTargets->Length, nativeRTVs, nativeDSV );
		}
	}
	
	DepthStencilView^ OutputMergerWrapper::GetDepthStencilView()
	{
		ID3D10DepthStencilView *view;

		m_Device->OMGetRenderTargets( 0, 0, &view );
		return DepthStencilView::FromPointer( view );
	}

	array<RenderTargetView^>^ OutputMergerWrapper::GetRenderTargets( int count )
	{
		stack_array<ID3D10RenderTargetView*> targets = stackalloc( ID3D10RenderTargetView*, count );
		array<RenderTargetView^>^ results = gcnew array<RenderTargetView^>( count );

		m_Device->OMGetRenderTargets( count, &targets[0], 0 );

		for( int i = 0; i < count; i++ )
			results[i] = RenderTargetView::FromPointer( targets[i] );

		return results;
	}
}
}
