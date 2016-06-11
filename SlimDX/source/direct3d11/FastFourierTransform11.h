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

#include "Enums11.h"
#include "FastFourierTransformDescription11.h"
#include "FastFourierTransformBufferRequirements11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;
		ref class UnorderedAccessView;

		/// <summary>
		/// An object that encapsulates forward and inverse FFTs.
		/// </summary>
		/// <unmanaged>ID3DX11FFT</unmanaged>
		public ref class FastFourierTransform : public ComObject
		{
			COMOBJECT(ID3DX11FFT, FastFourierTransform);

		private:
			FastFourierTransformBufferRequirements bufferRequirements;
			
		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="FastFourierTransform"/> class.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="description">Information that describes the shape of the FFT data as well as the scaling factors that should be used for forward and inverse transforms.</param>
			FastFourierTransform( DeviceContext^ context, FastFourierTransformDescription description );

			/// <summary>
			/// Initializes a new instance of the <see cref="FastFourierTransform"/> class.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="description">Information that describes the shape of the FFT data as well as the scaling factors that should be used for forward and inverse transforms.</param>
			/// <param name="flags">Flag affecting the behavior of the FFT.</param>
			FastFourierTransform( DeviceContext^ context, FastFourierTransformDescription description, FastFourierTransformCreationFlags flags );

			/// <summary>
			/// Creates a new one-dimensional complex FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create1DComplex( DeviceContext^ context, int x );

			/// <summary>
			/// Creates a new one-dimensional complex FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="flags">Flag affecting the behavior of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create1DComplex( DeviceContext^ context, int x, FastFourierTransformCreationFlags flags );

			/// <summary>
			/// Creates a new one-dimensional real FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create1DReal( DeviceContext^ context, int x );

			/// <summary>
			/// Creates a new one-dimensional real FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="flags">Flag affecting the behavior of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create1DReal( DeviceContext^ context, int x, FastFourierTransformCreationFlags flags );

			/// <summary>
			/// Creates a new two-dimensional complex FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="y">Length of the second dimension of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create2DComplex( DeviceContext^ context, int x, int y );

			/// <summary>
			/// Creates a new two-dimensional complex FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="y">Length of the second dimension of the FFT.</param>
			/// <param name="flags">Flag affecting the behavior of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create2DComplex( DeviceContext^ context, int x, int y, FastFourierTransformCreationFlags flags );

			/// <summary>
			/// Creates a new two-dimensional real FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="y">Length of the second dimension of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create2DReal( DeviceContext^ context, int x, int y );

			/// <summary>
			/// Creates a new two-dimensional real FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="y">Length of the second dimension of the FFT.</param>
			/// <param name="flags">Flag affecting the behavior of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create2DReal( DeviceContext^ context, int x, int y, FastFourierTransformCreationFlags flags );

			/// <summary>
			/// Creates a new three-dimensional complex FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="y">Length of the second dimension of the FFT.</param>
			/// <param name="z">Length of the third dimension of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create3DComplex( DeviceContext^ context, int x, int y, int z );

			/// <summary>
			/// Creates a new three-dimensional complex FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="y">Length of the second dimension of the FFT.</param>
			/// <param name="z">Length of the third dimension of the FFT.</param>
			/// <param name="flags">Flag affecting the behavior of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create3DComplex( DeviceContext^ context, int x, int y, int z, FastFourierTransformCreationFlags flags );

			/// <summary>
			/// Creates a new three-dimensional real FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="y">Length of the second dimension of the FFT.</param>
			/// <param name="z">Length of the third dimension of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create3DReal( DeviceContext^ context, int x, int y, int z );

			/// <summary>
			/// Creates a new three-dimensional real FFT.
			/// </summary>
			/// <param name="context">The device context used to create the FFT.</param>
			/// <param name="x">Length of the first dimension of the FFT.</param>
			/// <param name="y">Length of the second dimension of the FFT.</param>
			/// <param name="z">Length of the third dimension of the FFT.</param>
			/// <param name="flags">Flag affecting the behavior of the FFT.</param>
			/// <returns>The newly created FFT object.</returns>
			static FastFourierTransform^ Create3DReal( DeviceContext^ context, int x, int y, int z, FastFourierTransformCreationFlags flags );

			/// <summary>
			/// Gets the buffer requirements for the FFT.
			/// </summary>
			property FastFourierTransformBufferRequirements BufferRequirements
			{
				FastFourierTransformBufferRequirements get() { return bufferRequirements; }
			}

			/// <summary>
			/// Gets or sets the forward scale of the FFT.
			/// </summary>
			property float ForwardScale
			{
				float get();
				void set(float value);
			}

			/// <summary>
			/// Gets or sets the inverse scale of the FFT.
			/// </summary>
			property float InverseScale
			{
				float get();
				void set(float value);
			}

			/// <summary>
			/// Attaches buffers to an FFT context and performs any required precomputations.
			/// </summary>
			/// <param name="temporaryBuffers">Temporary buffers to attach.</param>
			/// <param name="precomputeBuffers">Buffers to hold precomputed data.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result AttachBuffersAndPrecompute( array<UnorderedAccessView^>^ temporaryBuffers, array<UnorderedAccessView^>^ precomputeBuffers );

			/// <summary>
			/// Performs a forward FFT.
			/// </summary>
			/// <param name="input">The input buffer.</param>
			/// <param name="output">The output buffer.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ForwardTransform( UnorderedAccessView^ input, UnorderedAccessView^ output );

			/// <summary>
			/// Performs a forward FFT.
			/// </summary>
			/// <param name="input">The input buffer.</param>
			/// <returns>The last temporary buffer written to during the operation.</returns>
			UnorderedAccessView^  ForwardTransform( UnorderedAccessView^ input );

			/// <summary>
			/// Performs an inverse FFT.
			/// </summary>
			/// <param name="input">The input buffer.</param>
			/// <param name="output">The output buffer.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result InverseTransform( UnorderedAccessView^ input, UnorderedAccessView^ output );

			/// <summary>
			/// Performs an inverse FFT.
			/// </summary>
			/// <param name="input">The input buffer.</param>
			/// <returns>The last temporary buffer written to during the operation.</returns>
			UnorderedAccessView^  InverseTransform( UnorderedAccessView^ input );
		};
	}
};