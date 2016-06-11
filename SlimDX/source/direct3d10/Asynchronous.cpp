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

#include "Direct3D10Exception.h"

#include "Asynchronous.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D10
{
	bool Asynchronous::IsDataAvailable::get()
	{
		return InternalPointer->GetData( 0, 0, 0 ) == S_OK;
	}
	
	void Asynchronous::Begin()
	{
		InternalPointer->Begin();
	}
	
	void Asynchronous::End()
	{
		InternalPointer->End();
	}
	
	DataStream^ Asynchronous::GetData()
	{
		return GetData( AsynchronousFlags::None );
	}
	
	DataStream^ Asynchronous::GetData( AsynchronousFlags flags )
	{
		int size = InternalPointer->GetDataSize();
		DataStream^ result = gcnew DataStream( size, true, true );
		if( RECORD_D3D10( InternalPointer->GetData( result->PositionPointer, size, static_cast<UINT>( flags ) ) ).IsFailure )
			return nullptr;
		return result;
	}
}
}
