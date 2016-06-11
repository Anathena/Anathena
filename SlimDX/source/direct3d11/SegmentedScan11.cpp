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
#include "DeviceContext11.h"
#include "UnorderedAccessView11.h"

#include "SegmentedScan11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{
	SegmentedScan::SegmentedScan( DeviceContext^ deviceContext, int maxElementScanSize )
	{
		if (deviceContext == nullptr)
			throw gcnew System::ArgumentNullException( "deviceContext" );

		ID3DX11SegmentedScan* nativeScan;
		HRESULT hr = D3DX11CreateSegmentedScan( deviceContext->InternalPointer, maxElementScanSize, &nativeScan );
		if (RECORD_D3D11( hr ).IsFailure)
			throw gcnew Direct3D11Exception( Result::Last );

		Construct( nativeScan );
	}

	Result SegmentedScan::SetScanDirection( ScanDirection value )
	{
		HRESULT hr = InternalPointer->SetScanDirection( static_cast<D3DX11_SCAN_DIRECTION>( value ) );
		return RECORD_D3D11( hr );
	}

	Result SegmentedScan::PerformSegmentedScan( ScanDataType elementType, ScanOpCode operation, int numberOfElements, UnorderedAccessView^ src, UnorderedAccessView^ srcElementFlags, UnorderedAccessView^ dest )
	{
		ID3D11UnorderedAccessView* nativeSrc = src == nullptr ? NULL : src->InternalPointer;
		ID3D11UnorderedAccessView* nativeSrcElementFlags = srcElementFlags == nullptr ? NULL : srcElementFlags->InternalPointer;
		ID3D11UnorderedAccessView* nativeDest = dest == nullptr ? NULL : dest->InternalPointer;

		HRESULT hr = InternalPointer->SegScan( static_cast<D3DX11_SCAN_DATA_TYPE>( elementType ), static_cast<D3DX11_SCAN_OPCODE>( operation ), numberOfElements, nativeSrc, nativeSrcElementFlags, nativeDest );
		return RECORD_D3D11( hr );
	}
}
}
