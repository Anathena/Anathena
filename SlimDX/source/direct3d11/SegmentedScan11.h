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
#pragma once

#include "../ComObject.h"

#include "../dxgi/Enums.h"

#include "../ComObject.h"

#include "Enums11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class DeviceContext;
		ref class UnorderedAccessView;

		/// <summary>
		/// Represents a segmented scan context.
		/// </summary>
		/// <unmanaged>ID3DX11SegmentedScan</unmanaged>
		public ref class SegmentedScan : public ComObject 
		{
			COMOBJECT(ID3DX11SegmentedScan, SegmentedScan);
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="SegmentedScan"/> class.
			/// </summary>
			/// <param name="deviceContext">The device context with which to associate the scan object.</param>
			/// <param name="maxElementScanSize">Maximum single scan size, in elements.</param>
			SegmentedScan( DeviceContext^ deviceContext, int maxElementScanSize );

			/// <summary>
			/// Sets the scan direction.
			/// </summary>
			/// <param name="direction">The direction in which to perform the scans.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetScanDirection( ScanDirection direction );

			/// <summary>
			/// Performs a segmented scan of a sequence.
			/// </summary>
			/// <param name="elementType">The type of element in the sequence.</param>
			/// <param name="operation">The binary operation to perform.</param>
			/// <param name="elementCount">Size of scan in elements.</param>
			/// <param name="source">Input sequence on the device. Set <paramref name="source"/> and <paramref name="destination"/> to the same value for in-place scans.</param>
			/// <param name="elementFlags">Compact array of bits with one bit per element of <paramref name="source"/>. A set value indicates the start of a new segment.</param>
			/// <param name="destination">Output sequence on the device.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result PerformSegmentedScan( ScanDataType elementType, ScanOpCode operation, int elementCount, UnorderedAccessView^ source, UnorderedAccessView^ elementFlags, UnorderedAccessView^ destination );
		};
	}
};