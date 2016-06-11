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

#include <d3d11.h>
#include <d3dx11.h>

#include "../stack_array.h"

#include "BlendState11.h"
#include "DepthStencilState11.h"
#include "DepthStencilView11.h"
#include "OutputMergerWrapper11.h"
#include "RenderTargetView11.h"
#include "UnorderedAccessView11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	OutputMergerWrapper::OutputMergerWrapper( ID3D11DeviceContext* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "device" );
		deviceContext = device;
	}
	
	void OutputMergerWrapper::DepthStencilState::set( SlimDX::Direct3D11::DepthStencilState^ value )
	{
		ID3D11DepthStencilState* oldState;
		int oldReference;
		deviceContext->OMGetDepthStencilState( &oldState, reinterpret_cast<UINT*>( &oldReference ) );

		if( oldState != NULL )
			oldState->Release();
	
		if( value == nullptr )
			deviceContext->OMSetDepthStencilState( 0, oldReference );
		else
			deviceContext->OMSetDepthStencilState( value->InternalPointer, oldReference );
	}
	
	SlimDX::Direct3D11::DepthStencilState^ OutputMergerWrapper::DepthStencilState::get()
	{
		ID3D11DepthStencilState* oldState = 0;
		int oldReference = 0;
		deviceContext->OMGetDepthStencilState( &oldState, reinterpret_cast<UINT*>( &oldReference ) );
		
		if( oldState == NULL )
			return nullptr;
		return SlimDX::Direct3D11::DepthStencilState::FromPointer( oldState );
	}
	
	void OutputMergerWrapper::DepthStencilReference::set( int value )
	{
		ID3D11DepthStencilState* oldState = 0;
		int oldReference = 0;
		deviceContext->OMGetDepthStencilState( &oldState, reinterpret_cast<UINT*>( &oldReference ) );
		deviceContext->OMSetDepthStencilState( oldState, value );

		if( oldState != NULL )
			oldState->Release();
	}
	
	int OutputMergerWrapper::DepthStencilReference::get()
	{
		ID3D11DepthStencilState* oldState = 0;
		int oldReference = 0;
		deviceContext->OMGetDepthStencilState( &oldState, reinterpret_cast<UINT*>( &oldReference ) );

		if( oldState != NULL )
			oldState->Release();
		
		return oldReference;
	}
	
	void OutputMergerWrapper::BlendState::set( SlimDX::Direct3D11::BlendState^ value )
	{
		ID3D11BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		deviceContext->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );

		if( oldState != NULL )
			oldState->Release();
		
		if( value == nullptr )
			deviceContext->OMSetBlendState( 0, oldFactor, oldMask );
		else
			deviceContext->OMSetBlendState( value->InternalPointer, oldFactor, oldMask );
	}
	
	SlimDX::Direct3D11::BlendState^ OutputMergerWrapper::BlendState::get()
	{
		ID3D11BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		deviceContext->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
		
		return SlimDX::Direct3D11::BlendState::FromPointer( oldState );
	}
	
	void OutputMergerWrapper::BlendFactor::set( Color4 value )
	{
		ID3D11BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		deviceContext->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
		
		float newFactor[4] = { value.Red, value.Green, value.Blue, value.Alpha };
		deviceContext->OMSetBlendState( oldState, newFactor, oldMask );

		if( oldState != NULL )
			oldState->Release();
	}
	
	Color4 OutputMergerWrapper::BlendFactor::get()
	{
		ID3D11BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		deviceContext->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );

		if( oldState != NULL )
			oldState->Release();
		
		return Color4( oldFactor[3], oldFactor[0], oldFactor[1], oldFactor[2] );
	}
	
	void OutputMergerWrapper::BlendSampleMask::set( int value )
	{
		ID3D11BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		deviceContext->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );
		deviceContext->OMSetBlendState( oldState, oldFactor, value );

		if( oldState != NULL )
			oldState->Release();
	}
	
	int OutputMergerWrapper::BlendSampleMask::get()
	{
		ID3D11BlendState* oldState = 0;
		float oldFactor[4];
		int oldMask = 0;
		deviceContext->OMGetBlendState( &oldState, oldFactor, reinterpret_cast<UINT*>( &oldMask ) );

		if( oldState != NULL )
			oldState->Release();
		
		return oldMask;
	}

	void OutputMergerWrapper::GetDepthStencilState(SlimDX::Direct3D11::DepthStencilState^ %state, int %reference)
	{
		ID3D11DepthStencilState* result = 0;
		UINT ref = 0;
		deviceContext->OMGetDepthStencilState(&result, &ref);

		reference = ref;
		state = SlimDX::Direct3D11::DepthStencilState::FromPointer(result);
	}

	void OutputMergerWrapper::SetDepthStencilState(SlimDX::Direct3D11::DepthStencilState^ state, int reference )
	{
		deviceContext->OMSetDepthStencilState(state == nullptr ? 0 : state->InternalPointer, reference);
	}

	void OutputMergerWrapper::GetBlendState(SlimDX::Direct3D11::BlendState^ %state, Color4 %blendFactor, int %sampleMask)
	{
		ID3D11BlendState *outState;
		float outFactor[4];
		UINT outMask;

		deviceContext->OMGetBlendState(&outState, outFactor, &outMask);
		
		state = SlimDX::Direct3D11::BlendState::FromPointer(outState);
		blendFactor = Color4(outFactor[3], outFactor[0], outFactor[1], outFactor[2]);
		sampleMask = outMask;
	}

	void OutputMergerWrapper::SetBlendState(SlimDX::Direct3D11::BlendState^ state, Color4 blendFactor, int sampleMask)
	{
		deviceContext->OMSetBlendState(state == nullptr ? 0 : state->InternalPointer, reinterpret_cast<float*>(&blendFactor), sampleMask);
	}
	
	void OutputMergerWrapper::SetTargets( RenderTargetView^ renderTargetView )
	{
		SetTargets( nullptr, renderTargetView );
	}
	
	void OutputMergerWrapper::SetTargets( DepthStencilView^ depthStencilView, RenderTargetView^ renderTargetView )
	{
		ID3D11DepthStencilView *nativeDSV = depthStencilView == nullptr ? 0 : static_cast<ID3D11DepthStencilView*>( depthStencilView->InternalPointer );
		ID3D11RenderTargetView *nativeRTV[] = { renderTargetView == nullptr ? 0 : static_cast<ID3D11RenderTargetView*>( renderTargetView->InternalPointer ) };
		
		deviceContext->OMSetRenderTargets( 1, nativeRTV, nativeDSV );
	}

	void OutputMergerWrapper::SetTargets( ... array<RenderTargetView^>^ renderTargets )
	{
		SetTargets( nullptr, renderTargets );
	}

	void OutputMergerWrapper::SetTargets( DepthStencilView^ depthStencilView, ... array<RenderTargetView^>^ renderTargets )
	{
		ID3D11DepthStencilView *nativeDSV = depthStencilView == nullptr ? 0 : static_cast<ID3D11DepthStencilView*>( depthStencilView->InternalPointer );
		ID3D11RenderTargetView* nativeRTVs[D3D11_SIMULTANEOUS_RENDER_TARGET_COUNT];
		
		if( renderTargets == nullptr )
		{
			deviceContext->OMSetRenderTargets( 0, 0, nativeDSV );
		}
		else 
		{
			for( int i = 0; i < renderTargets->Length; ++i )
				nativeRTVs[ i ] = renderTargets[ i ] == nullptr ? 0 : static_cast<ID3D11RenderTargetView*>( renderTargets[ i ]->InternalPointer );
			deviceContext->OMSetRenderTargets( renderTargets->Length, nativeRTVs, nativeDSV );
		}
	}

	void OutputMergerWrapper::SetTargets( RenderTargetView^ renderTargetView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews )
	{
		array<int>^ lengths = gcnew array<int>( unorderedAccessViews->Length );
		for( int i = 0; i < unorderedAccessViews->Length; i++ )
			lengths[i] = -1;

		SetTargets( nullptr, renderTargetView, startSlot, unorderedAccessViews, lengths );
	}
	
	void OutputMergerWrapper::SetTargets( DepthStencilView^ depthStencilView, RenderTargetView^ renderTargetView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews )
	{
		array<int>^ lengths = gcnew array<int>( unorderedAccessViews->Length );
		for( int i = 0; i < unorderedAccessViews->Length; i++ )
			lengths[i] = -1;

		SetTargets( depthStencilView, renderTargetView, startSlot, unorderedAccessViews, lengths );
	}

	void OutputMergerWrapper::SetTargets( int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, ... array<RenderTargetView^>^ renderTargets )
	{
		array<int>^ lengths = gcnew array<int>( unorderedAccessViews->Length );
		for( int i = 0; i < unorderedAccessViews->Length; i++ )
			lengths[i] = -1;

		SetTargets( nullptr, startSlot, unorderedAccessViews, lengths, renderTargets );
	}

	void OutputMergerWrapper::SetTargets( DepthStencilView^ depthStencilView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, ... array<RenderTargetView^>^ renderTargets )
	{
		array<int>^ lengths = gcnew array<int>( unorderedAccessViews->Length );
		for( int i = 0; i < unorderedAccessViews->Length; i++ )
			lengths[i] = -1;

		SetTargets( depthStencilView, startSlot, unorderedAccessViews, lengths, renderTargets );
	}

	void OutputMergerWrapper::SetTargets( RenderTargetView^ renderTargetView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, array<int>^ lengths )
	{
		SetTargets( nullptr, renderTargetView, startSlot, unorderedAccessViews, lengths );
	}
	
	void OutputMergerWrapper::SetTargets( DepthStencilView^ depthStencilView, RenderTargetView^ renderTargetView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, array<int>^ initialLengths )
	{
		ID3D11DepthStencilView *nativeDSV = depthStencilView == nullptr ? 0 : static_cast<ID3D11DepthStencilView*>( depthStencilView->InternalPointer );
		ID3D11RenderTargetView *nativeRTV[] = { renderTargetView == nullptr ? 0 : static_cast<ID3D11RenderTargetView*>( renderTargetView->InternalPointer ) };

		stack_array<ID3D11UnorderedAccessView*> uavs = stackalloc( ID3D11UnorderedAccessView*, unorderedAccessViews->Length );
		for( int i = 0; i < unorderedAccessViews->Length; i++ )
			uavs[i] = unorderedAccessViews[i]->InternalPointer;

		pin_ptr<int> pinnedLengths = &initialLengths[0];
		deviceContext->OMSetRenderTargetsAndUnorderedAccessViews( 1, nativeRTV, nativeDSV, startSlot, unorderedAccessViews->Length, &uavs[0], reinterpret_cast<UINT*>( pinnedLengths ) );
	}

	void OutputMergerWrapper::SetTargets( int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, array<int>^ initialLengths, ... array<RenderTargetView^>^ renderTargets )
	{
		SetTargets( nullptr, startSlot, unorderedAccessViews, initialLengths, renderTargets );
	}

	void OutputMergerWrapper::SetTargets( DepthStencilView^ depthStencilView, int startSlot, array<UnorderedAccessView^>^ unorderedAccessViews, array<int>^ initialLengths, ... array<RenderTargetView^>^ renderTargets )
	{
		ID3D11DepthStencilView *nativeDSV = depthStencilView == nullptr ? 0 : static_cast<ID3D11DepthStencilView*>( depthStencilView->InternalPointer );
		ID3D11RenderTargetView* nativeRTVs[D3D11_SIMULTANEOUS_RENDER_TARGET_COUNT];

		stack_array<ID3D11UnorderedAccessView*> uavs = stackalloc( ID3D11UnorderedAccessView*, unorderedAccessViews->Length );
		for( int i = 0; i < unorderedAccessViews->Length; i++ )
			uavs[i] = unorderedAccessViews[i]->InternalPointer;

		pin_ptr<int> pinnedLengths = &initialLengths[0];
		if( renderTargets == nullptr )
		{
			deviceContext->OMSetRenderTargetsAndUnorderedAccessViews( 0, 0, nativeDSV, startSlot, unorderedAccessViews->Length, &uavs[0], reinterpret_cast<UINT*>( pinnedLengths ) );
		}
		else 
		{
			for( int i = 0; i < renderTargets->Length; ++i )
				nativeRTVs[ i ] = renderTargets[ i ] == nullptr ? 0 : static_cast<ID3D11RenderTargetView*>( renderTargets[ i ]->InternalPointer );
			deviceContext->OMSetRenderTargetsAndUnorderedAccessViews( renderTargets->Length, nativeRTVs, nativeDSV, startSlot, unorderedAccessViews->Length, &uavs[0], reinterpret_cast<UINT*>( pinnedLengths ) );
		}
	}

	DepthStencilView^ OutputMergerWrapper::GetDepthStencilView()
	{
		ID3D11DepthStencilView *view;

		deviceContext->OMGetRenderTargets( 0, NULL, &view );
		return DepthStencilView::FromPointer( view );
	}

	array<RenderTargetView^>^ OutputMergerWrapper::GetRenderTargets( int count )
	{
		stack_array<ID3D11RenderTargetView*> targets = stackalloc( ID3D11RenderTargetView*, count );
		array<RenderTargetView^>^ results = gcnew array<RenderTargetView^>( count );

		deviceContext->OMGetRenderTargets( count, &targets[0], NULL );

		for( int i = 0; i < count; i++ )
			results[i] = RenderTargetView::FromPointer( targets[i] );

		return results;
	}

	array<UnorderedAccessView^>^ OutputMergerWrapper::GetUnorderedAccessViews( int startSlot, int count )
	{
		stack_array<ID3D11UnorderedAccessView*> targets = stackalloc( ID3D11UnorderedAccessView*, count );
		array<UnorderedAccessView^>^ results = gcnew array<UnorderedAccessView^>( count );

		deviceContext->OMGetRenderTargetsAndUnorderedAccessViews( 0, NULL, NULL, startSlot, count, &targets[0] );

		for( int i = 0; i < count; i++ )
			results[i] = UnorderedAccessView::FromPointer( targets[i] );

		return results;
	}
}
}
