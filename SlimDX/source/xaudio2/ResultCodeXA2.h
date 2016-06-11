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

#include "../Result.h"

namespace SlimDX
{
	namespace XAudio2
	{
		/// <summary>Defines result codes that are returned by XAudio 2 functions.</summary>
		/// <unmanaged href="ms679692">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();
			
		public:
			/// <summary>
			/// Returned by XAudio2 for certain API usage errors (invalid calls etc) that
			/// are hard to avoid completely and should be handled by a title at runtime.
			/// (API usage errors that are completely avoidable, such as invalid parameters,
			/// cause an ASSERT in debug builds and undefined behavior in retail builds,
			/// so no error code is defined for them.)
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="cc308077">XAUDIO2_E_INVALID_CALL</unmanaged>
			property static Result InvalidCall
			{
				Result get();
			}

			/// <summary>
			/// The decoder suffered an unrecoverable error.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="cc308077">XAUDIO2_E_XMA_DECODER_ERROR</unmanaged>
			property static Result XmaDecoderError
			{
				Result get();
			}

			/// <summary>
			/// An effect failed to instantiate.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="cc308077">XAUDIO2_E_XAPO_CREATION_FAILED</unmanaged>
			property static Result EffectCreationFailed
			{
				Result get();
			}

			/// <summary>
			/// An audio device became unusable (possibly due to being unplugged, or some other event).
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="cc308077">XAUDIO2_E_DEVICE_INVALIDATED</unmanaged>
			property static Result DeviceInvalidated
			{
				Result get();
			}

			/// <summary>
			/// Represents the result of a successful operation.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="aa378137">S_OK</unmanaged>
			property static Result Success
			{
				Result get();
			}

			/// <summary>
			/// Represents a generic failure result.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="aa378137">E_FAIL</unmanaged>
			property static Result Failure
			{
				Result get();
			}
		};
	}
}