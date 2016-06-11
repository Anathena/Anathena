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

#include "Direct3D11Exception.h"

#include "Buffer11.h"
#include "EffectConstantBuffer11.h"
#include "ShaderResourceView11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectConstantBuffer::EffectConstantBuffer( ID3DX11EffectConstantBuffer* pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = pointer;
	}
	
	EffectConstantBuffer::EffectConstantBuffer( IntPtr pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectConstantBuffer*>( pointer.ToPointer() );
	}
	
	Buffer^ EffectConstantBuffer::ConstantBuffer::get()
	{
		ID3D11Buffer* buffer = 0;
		if( RECORD_D3D11( m_Pointer->GetConstantBuffer( &buffer ) ).IsFailure )
			return nullptr;
		
		return Buffer::FromPointer( buffer );
	}

	void EffectConstantBuffer::ConstantBuffer::set( Buffer^ buffer )
	{
		if( buffer == nullptr )
			RECORD_D3D11( m_Pointer->SetConstantBuffer(0) );
		else
			RECORD_D3D11( m_Pointer->SetConstantBuffer( static_cast<ID3D11Buffer*>( buffer->InternalPointer ) ) );
	}
	
	ShaderResourceView^ EffectConstantBuffer::TextureBuffer::get()
	{
		ID3D11ShaderResourceView* buffer = 0;
		if( RECORD_D3D11( m_Pointer->GetTextureBuffer( &buffer ) ).IsFailure )
			return nullptr;
			
		return ShaderResourceView::FromPointer( buffer );
	}

	void EffectConstantBuffer::TextureBuffer::set( ShaderResourceView^ buffer )
	{
		if( buffer == nullptr )
			RECORD_D3D11( m_Pointer->SetTextureBuffer(0) );
		else
			RECORD_D3D11( m_Pointer->SetTextureBuffer( static_cast<ID3D11ShaderResourceView*>( buffer->InternalPointer ) ) );
	}

	Result EffectConstantBuffer::UndoSetConstantBuffer()
	{
		HRESULT hr = m_Pointer->UndoSetConstantBuffer();
		return RECORD_D3D11(hr);
	}

	Result EffectConstantBuffer::UndoSetTextureBuffer()
	{
		HRESULT hr = m_Pointer->UndoSetTextureBuffer();
		return RECORD_D3D11(hr);
	}
}
}
