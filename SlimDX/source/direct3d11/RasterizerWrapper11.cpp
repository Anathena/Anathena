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

#include "../InternalHelpers.h"
#include "../stack_array.h"

#include "RasterizerWrapper11.h"
#include "RasterizerState11.h"

using namespace System;
using namespace msclr::interop;

namespace SlimDX
{
namespace Direct3D11
{ 
	RasterizerWrapper::RasterizerWrapper( ID3D11DeviceContext* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "device" );
		deviceContext = device;
	}
	
	void RasterizerWrapper::State::set( RasterizerState^ value )
	{
		if( value == nullptr )
			deviceContext->RSSetState( 0 );
		else
			deviceContext->RSSetState( value->InternalPointer );
	}
	
	RasterizerState^ RasterizerWrapper::State::get()
	{
		ID3D11RasterizerState* state = 0;
		deviceContext->RSGetState( &state );
		return RasterizerState::FromPointer( state );
	}
	
	void RasterizerWrapper::SetViewports( Viewport viewport )
	{
		D3D11_VIEWPORT nativeVP = { viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinZ, viewport.MaxZ };
		deviceContext->RSSetViewports( 1, &nativeVP );
	}

	void RasterizerWrapper::SetViewports( ... array<Viewport>^ viewports )
	{
		if( viewports == nullptr ) 
		{
			deviceContext->RSSetViewports( 0, 0 );
		}
		else
		{
			D3D11_VIEWPORT nativeVPs[D3D11_VIEWPORT_AND_SCISSORRECT_OBJECT_COUNT_PER_PIPELINE];
			for( int i = 0; i < viewports->Length; ++i )
			{
				nativeVPs[ i ].TopLeftX = viewports[ i ].X;
				nativeVPs[ i ].TopLeftY = viewports[ i ].Y;
				nativeVPs[ i ].Width = viewports[ i ].Width;
				nativeVPs[ i ].Height = viewports[ i ].Height;
				nativeVPs[ i ].MinDepth = viewports[ i ].MinZ;
				nativeVPs[ i ].MaxDepth = viewports[ i ].MaxZ;
			}
			
			deviceContext->RSSetViewports( viewports->Length, nativeVPs );
		}
	}

	void RasterizerWrapper::GetViewports( array<Viewport>^ viewports )
	{
		UINT count = viewports->Length;
		stack_array<D3D11_VIEWPORT> nativeVPs = stackalloc( D3D11_VIEWPORT, viewports->Length );
		deviceContext->RSGetViewports( &count, &nativeVPs[0] );

		for( UINT i = 0; i < count; i++ )
			viewports[i] = Viewport( nativeVPs[i].TopLeftX, nativeVPs[i].TopLeftY, nativeVPs[i].Width, nativeVPs[i].Height, nativeVPs[i].MinDepth, nativeVPs[i].MaxDepth );
	}
	
	array<Viewport>^ RasterizerWrapper::GetViewports()
	{
		UINT count = 0;
		deviceContext->RSGetViewports( &count, 0 );
		
		stack_array<D3D11_VIEWPORT> nativeVPs = stackalloc( D3D11_VIEWPORT, count );
		deviceContext->RSGetViewports( &count, &nativeVPs[0] );
		
		array<Viewport>^ viewports = gcnew array<Viewport>( count );
		for( UINT i = 0; i < count; i++ )
			viewports[i] = Viewport( nativeVPs[i].TopLeftX, nativeVPs[i].TopLeftY, nativeVPs[i].Width, nativeVPs[i].Height, nativeVPs[i].MinDepth, nativeVPs[i].MaxDepth );
		return viewports;
	}

	Viewport RasterizerWrapper::GetViewport()
	{
		D3D11_VIEWPORT vp;
		UINT count = 1;
		
		deviceContext->RSGetViewports(&count, &vp);
		return Viewport(vp.TopLeftX, vp.TopLeftY, vp.Width, vp.Height, vp.MinDepth, vp.MaxDepth);
	}

	void RasterizerWrapper::SetScissorRectangles( System::Drawing::Rectangle scissorRectangle )
	{
		D3D11_RECT rect = { scissorRectangle.Left, scissorRectangle.Top, scissorRectangle.Right, scissorRectangle.Bottom };
		deviceContext->RSSetScissorRects( 1, &rect );
	}

	void RasterizerWrapper::SetScissorRectangles( ... array<System::Drawing::Rectangle>^ scissorRectangles )
	{
		if( scissorRectangles == nullptr ) 
		{
			deviceContext->RSSetScissorRects( 0, 0 );
		}
		else
		{
			D3D11_RECT rects[D3D11_VIEWPORT_AND_SCISSORRECT_OBJECT_COUNT_PER_PIPELINE];
			for( int i = 0; i < scissorRectangles->Length; ++i )
			{
				rects[i].left = scissorRectangles[i].Left;
				rects[i].right = scissorRectangles[i].Right;
				rects[i].top = scissorRectangles[i].Top;
				rects[i].bottom = scissorRectangles[i].Bottom;
			}

			deviceContext->RSSetScissorRects( scissorRectangles->Length, rects );
		}
	}

	void RasterizerWrapper::GetScissorRectangles( array<System::Drawing::Rectangle>^ scissorRectangles )
	{
		UINT count = scissorRectangles->Length;
		stack_array<D3D11_RECT> rects = stackalloc( D3D11_RECT, scissorRectangles->Length );
		deviceContext->RSGetScissorRects( &count, &rects[0] );

		for( UINT i = 0; i < count; i++ )
			scissorRectangles[i] = marshal_as<System::Drawing::Rectangle>( rects[i] );
	}
	
	array<System::Drawing::Rectangle>^ RasterizerWrapper::GetScissorRectangles()
	{
		UINT count = 0;
		deviceContext->RSGetScissorRects( &count, 0 );
		
		stack_array<D3D10_RECT> rects = stackalloc( D3D10_RECT, count );
		deviceContext->RSGetScissorRects( &count, &rects[0] );
		
		array<System::Drawing::Rectangle>^ scissorRectangles = gcnew array<System::Drawing::Rectangle>( count );
		for( UINT i = 0; i < count; i++ )
			scissorRectangles[i] = System::Drawing::Rectangle( rects[i].left, rects[i].top, rects[i].right - rects[i].left, rects[i].bottom - rects[i].top );
		return scissorRectangles;
	}
}
}
