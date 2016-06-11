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

#ifdef XMLDOCS
#include "Enums.h"
#endif

namespace SlimDX
{
	namespace DirectSound
	{
		/// <summary>Defines result codes that are returned by DirectSound 8 functions.</summary>
		/// <unmanaged href="bb219706">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();

		public:
			/// <summary>
			/// The function completed successfully.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219706">DS_OK</unmanaged>
			property static Result Success
			{
				Result get();
			}

			/// <summary>
			/// Represents a generic failure result.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">E_FAIL</unmanaged>
			property static Result Failure
			{
				Result get();
			}

			/// <summary>
			/// The 3D algorithm was substituted.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219706">DS_NO_VIRTUALIZATION</unmanaged>
			property static Result NoVirtualization
			{
				Result get();
			}

			/// <summary>
			/// Access is denied.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_ACCESSDENIED</unmanaged>
			property static Result AccessDenied
			{
				Result get();
			}

			/// <summary>
			/// The call failed because resources (such as a priority level) were already being used by another caller.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_ALLOCATED</unmanaged>
			property static Result Allocated
			{
				Result get();
			}

			/// <summary>
			/// This object is already initialized.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_ALREADYINITIALIZED</unmanaged>
			property static Result AlreadyInitialized
			{
				Result get();
			}

			/// <summary>
			/// The specified WAVE format is not supported.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_BADFORMAT</unmanaged>
			property static Result BadFormat
			{
				Result get();
			}

			/// <summary>
			/// The GUID specified in an audiopath file does not match a valid MIXIN buffer.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_BADSENDBUFFERGUID</unmanaged>
			property static Result BadSendBufferGuid
			{
				Result get();
			}

			/// <summary>
			/// The buffer memory has been lost, and must be restored.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_BUFFERLOST</unmanaged>
			property static Result BufferLost
			{
				Result get();
			}

			/// <summary>
			/// Tried to create a <see cref="BufferFlags">ControlEffects</see> buffer shorter than DSBSIZE_FX_MIN milliseconds.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_BUFFERTOOSMALL</unmanaged>
			property static Result BufferTooSmall
			{
				Result get();
			}

			/// <summary>
			/// The control (vol, pan, etc.) requested by the caller is not available.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_CONTROLUNAVAIL</unmanaged>
			property static Result ControlUnavailable
			{
				Result get();
			}

			/// <summary>
			/// Attempt to use DirectSound 8 functionality on an older DirectSound object.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_DS8_REQUIRED</unmanaged>
			property static Result DirectSound8Required
			{
				Result get();
			}

			/// <summary>
			/// The effects requested could not be found on the system, or they were found
			/// but in the wrong order, or in the wrong hardware/software locations.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706"></unmanaged>
			property static Result EffectsUnavailable
			{
				Result get();
			};

			/// <summary>
			/// An undetermined error occurred inside the DirectSound subsystem.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_GENERIC</unmanaged>
			property static Result Generic
			{
				Result get();
			}

			/// <summary>
			/// This call is not valid for the current state of this object.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_INVALIDCALL</unmanaged>
			property static Result InvalidCall
			{
				Result get();
			}

			/// <summary>
			///An invalid parameter was passed to the returning function.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_INVALIDPARAM</unmanaged>
			property static Result InvalidParameter
			{
				Result get();
			}

			/// <summary>
			/// This object does not support aggregation.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_NOAGGREGATION</unmanaged>
			property static Result NoAggregation
			{
				Result get();
			}

			/// <summary>
			/// No sound driver is available for use.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_NODRIVER</unmanaged>
			property static Result NoDriver
			{
				Result get();
			}

			/// <summary>
			/// The requested COM interface is not available.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_NOINTERFACE</unmanaged>
			property static Result NoInterface
			{
				Result get();
			}

			/// <summary>
			/// The object requested was not found.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_OBJECTNOTFOUND</unmanaged>
			property static Result ObjectNotFound
			{
				Result get();
			}

			/// <summary>
			/// Another app has a higher priority level, preventing this call from succeeding.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_OTHERAPPHASPRIO</unmanaged>
			property static Result OtherApplicationHasPriority
			{
				Result get();
			}

			/// <summary>
			/// The caller does not have the priority level required for the function to succeed
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_PRIOLEVELNEEDED</unmanaged>
			property static Result PriorityLevelNeeded
			{
				Result get();
			}

			/// <summary>
			/// A circular loop of send effects was detected.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_SENDLOOP</unmanaged>
			property static Result SendLoop
			{
				Result get();
			}

			/// <summary>
			/// This object has not been initialized.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_UNINITIALIZED</unmanaged>
			property static Result Uninitialized
			{
				Result get();
			}

			/// <summary>
			/// The function called is not supported at this time.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219706">DSERR_UNSUPPORTED</unmanaged>
			property static Result Unsupported
			{
				Result get();
			}
		};
	}
}