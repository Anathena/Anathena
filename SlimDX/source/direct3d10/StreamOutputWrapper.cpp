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

#include "StreamOutputWrapper.h"
#include "Buffer.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{ 
	StreamOutputWrapper::StreamOutputWrapper( ID3D10Device* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "device" );
		m_Device = device;
	}

	void StreamOutputWrapper::SetTargets( ... array<StreamOutputBufferBinding>^ bufferBindings )
	{
		if( bufferBindings == nullptr )
		{
			m_Device->SOSetTargets( 0, 0, 0 );	
		}
		else 
		{
			ID3D10Buffer* buffers[D3D10_SO_BUFFER_SLOT_COUNT];
			UINT offsets[D3D10_SO_BUFFER_SLOT_COUNT];
			
			for( int i = 0; i < D3D10_SO_BUFFER_SLOT_COUNT; ++i )
			{
				buffers[ i ] = 0;
				offsets[ i ] = 0;
			}
			
			for( int i = 0; i < bufferBindings->Length; ++i )
			{
				buffers[i] = static_cast<ID3D10Buffer*>( bufferBindings[ i ].Buffer->InternalPointer );
				offsets[i] = bufferBindings[ i ].Offset;
			}
			
			m_Device->SOSetTargets( bufferBindings->Length, buffers, offsets );
		}
	}
}
}
