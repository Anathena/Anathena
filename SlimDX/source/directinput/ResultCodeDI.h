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

#ifdef XMLDOCS
#include "../ComObject.h"
#include "EffectDI.h"
#include "DeviceDI.h"
#endif

namespace SlimDX
{
	namespace DirectInput
	{
		/// <summary>Defines result codes that are returned by DirectInput 8 functions.</summary>
		/// <unmanaged href="bb219815">HRESULT</unmanaged>
		public ref class ResultCode sealed
		{
		private:
			ResultCode();
				
		public:
			/// <summary>
			/// The operation cannot be performed while the device is acquired.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_ACQUIRED</unmanaged>
			property static Result Acquired
			{
				Result get();
			};

			/// <summary>
			/// This object is already initialized.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_ALREADYINITIALIZED</unmanaged>
			property static Result AlreadyInitialized
			{
				Result get();
			};

			/// <summary>
			/// The object could not be created due to an incompatible driver version
			/// or mismatched or incomplete driver components.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_BADDRIVERVER</unmanaged>
			property static Result BadDriverVersion
			{
				Result get();
			};

			/// <summary>
			/// The application was written for an unsupported prerelease version of DirectInput.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_BETADIRECTINPUTVERSION</unmanaged>
			property static Result BetaDirectInputVersion
			{
				Result get();
			};

			/// <summary>
			/// The device buffer overflowed.  Some input was lost.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_BUFFEROVERFLOW</unmanaged>
			property static Result BufferOverflow
			{
				Result get();
			};

			/// <summary>
			/// The device is full.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_DEVICEFULL</unmanaged>
			property static Result DeviceFull
			{
				Result get();
			};

			/// <summary>
			/// The device or device instance or effect is not registered with DirectInput.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_DEVICENOTREG</unmanaged>
			property static Result DeviceNotRegistered
			{
				Result get();
			};

			/// <summary>
			/// The parameters of the effect were successfully updated by
			/// <see cref="Effect::SetParameters(EffectParameters, EffectParameterFlags)" />, but the effect was not
			/// downloaded because the device is not exclusively acquired
			/// or because the <see cref="EffectParameterFlags">NoDownload</see> flag was passed.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_DOWNLOADSKIPPED</unmanaged>
			property static Result DownloadSkipped
			{
				Result get();
			};

			/// <summary>
			/// An attempt was made to modify parameters of an effect while it is
			/// playing.  Not all hardware devices support altering the parameters
			/// of an effect while it is playing.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_EFFECTPLAYING</unmanaged>
			property static Result EffectPlaying
			{
				Result get();
			};

			/// <summary>
			/// The parameters of the effect were successfully updated by
			/// <see cref="Effect::SetParameters(EffectParameters, EffectParameterFlags)" />, but in order to change
			/// the parameters, the effect needed to be restarted.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_EFFECTRESTARTED</unmanaged>
			property static Result EffectRestarted
			{
				Result get();
			};

			/// <summary>
			/// Represents a generic failure result.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="aa378137">E_FAIL</unmanaged>
			property static Result Failure
			{
				Result get();
			};

			/// <summary>
			/// An undetermined error occured inside the DInput subsystem.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_GENERIC</unmanaged>
			property static Result Generic
			{
				Result get();
			};

			/// <summary>
			/// The device already has an event notification associated with it.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_HANDLEEXISTS</unmanaged>
			property static Result HandleExists
			{
				Result get();
			};

			/// <summary>
			/// The device cannot be reinitialized because there are still effects attached to it.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_HASEFFECTS</unmanaged>
			property static Result HasEffects
			{
				Result get();
			};

			/// <summary>
			/// The effect could not be downloaded because essential information
			/// is missing.  For example, no axes have been associated with the
			/// effect, or no type-specific information has been created.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_INCOMPLETEEFFECT</unmanaged>
			property static Result IncompleteEffect
			{
				Result get();
			};

			/// <summary>
			/// Access to the device has been lost.  It must be re-acquired.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_INPUTLOST</unmanaged>
			property static Result InputLost
			{
				Result get();
			};

			/// <summary>
			/// An invalid parameter was passed to the returning function,
			/// or the object was not in a state that allowed the function
			/// to be called.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_INVALIDPARAM</unmanaged>
			property static Result InvalidParameter
			{
				Result get();
			};

			/// <summary>
			/// A mapper file function failed because reading or writing the user or IHV settings file failed.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_MAPFILEFAIL</unmanaged>
			property static Result MapFileFail
			{
				Result get();
			};

			/// <summary>
			/// Not all the requested information fit into the buffer.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_MOREDATA</unmanaged>
			property static Result MoreData
			{
				Result get();
			};

			/// <summary>
			/// This object does not support aggregation.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_NOAGGREGATION</unmanaged>
			property static Result NoAggregation
			{
				Result get();
			};

			/// <summary>
			/// The operation had no effect.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_NOEFFECT</unmanaged>
			property static Result NoEffect
			{
				Result get();
			};

			/// <summary>
			/// The specified interface is not supported by the object.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_NOINTERFACE</unmanaged>
			property static Result NoInterface
			{
				Result get();
			};

			/// <summary>
			/// The operation cannot be performed unless the device is acquired.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_NOTACQUIRED</unmanaged>
			property static Result NotAcquired
			{
				Result get();
			};

			/// <summary>
			/// The device exists but is not currently attached.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_NOTATTACHED</unmanaged>
			property static Result NotAttached
			{
				Result get();
			};

			/// <summary>
			/// Attempted to read buffered device data from a device that is not buffered.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_NOTBUFFERED</unmanaged>
			property static Result NotBuffered
			{
				Result get();
			};

			/// <summary>
			/// The effect is not downloaded.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_NOTDOWNLOADED</unmanaged>
			property static Result NotDownloaded
			{
				Result get();
			};

			/// <summary>
			/// The operation cannot be performed unless the device is acquired in exclusive mode.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_NOTEXCLUSIVEACQUIRED</unmanaged>
			property static Result NotExclusivelyAcquired
			{
				Result get();
			};

			/// <summary>
			/// The requested object does not exist.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_NOTFOUND</unmanaged>
			property static Result NotFound
			{
				Result get();
			};

			/// <summary>
			/// This object has not been initialized.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_NOTINITIALIZED</unmanaged>
			property static Result NotInitialized
			{
				Result get();
			};

			/// <summary>
			/// The requested object does not exist.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_OBJECTNOTFOUND</unmanaged>
			property static Result ObjectNotFound
			{
				Result get();
			};

			/// <summary>
			/// The application requires a newer version of DirectInput.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_OLDDIRECTINPUTVERSION</unmanaged>
			property static Result OldDirectInputVersion
			{
				Result get();
			};

			/// <summary>
			/// Another app has a higher priority level, preventing this call from succeeding.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_OTHERAPPHASPRIO</unmanaged>
			property static Result OtherApplicationHasPriority
			{
				Result get();
			};

			/// <summary>
			/// The DInput subsystem couldn't allocate sufficient memory to complete the caller's request.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_OUTOFMEMORY</unmanaged>
			property static Result OutOfMemory
			{
				Result get();
			};

			/// <summary>
			/// The device is a polled device.  As a result, device buffering
			/// will not collect any data and event notifications will not be
			/// signalled until GetCurrentState is called.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_POLLEDDEVICE</unmanaged>
			property static Result PolledDevice
			{
				Result get();
			};

			/// <summary>
			/// The change in device properties had no effect.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_PROPNOEFFECT</unmanaged>
			property static Result PropertyHasNoEffect
			{
				Result get();
			};

			/// <summary>
			/// The specified property cannot be changed.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_READONLY</unmanaged>
			property static Result ReadOnly
			{
				Result get();
			};

			/// <summary>
			/// <see cref="Device::SendData" /> failed because more information was requested
			/// to be sent than can be sent to the device.  Some devices have
			/// restrictions on how much data can be sent to them.  (For example,
			/// there might be a limit on the number of buttons that can be
			/// pressed at once.)
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_REPORTFULL</unmanaged>
			property static Result ReportFull
			{
				Result get();
			};

			/// <summary>
			/// The settings have been successfully applied but could not be persisted. 
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_SETTINGSNOTSAVED</unmanaged>
			property static Result SettingsNotSaved
			{
				Result get();
			};

			/// <summary>
			/// The operation completed successfully.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_OK</unmanaged>
			property static Result Success
			{
				Result get();
			};

			/// <summary>
			/// The parameters of the effect were successfully updated by
			/// <see cref="Effect::SetParameters(EffectParameters, EffectParameterFlags)" />, but some of them were
			/// beyond the capabilities of the device and were truncated.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_TRUNCATED</unmanaged>
			property static Result Truncated
			{
				Result get();
			};

			/// <summary>
			/// Equal to <see cref="ResultCode::Truncated" /> | <see cref="ResultCode::EffectRestarted" />.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_TRUNCATEDANDRESTARTED</unmanaged>
			property static Result TruncatedAndRestarted
			{
				Result get();
			};

			/// <summary>
			/// The operation could not be completed because the device is not plugged in.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_UNPLUGGED</unmanaged>
			property static Result Unplugged
			{
				Result get();
			};

			/// <summary>
			/// The function called is not supported at this time.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">DIERR_UNSUPPORTED</unmanaged>
			property static Result Unsupported
			{
				Result get();
			};

			/// <summary>
			/// A SUCCESS code indicating that settings cannot be modified.
			/// </summary>
			/// <remarks>This is a successful result.</remarks>
			/// <unmanaged href="bb219815">DI_WRITEPROTECT</unmanaged>
			property static Result WriteProtect
			{
				Result get();
			};

			/// <summary>
			/// The HWND parameter is not a valid top-level window that belongs to the process.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">E_HANDLE</unmanaged>
			property static Result InvalidHandle
			{
				Result get();
			}

			/// <summary>
			/// Data is not yet available.
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">E_PENDING</unmanaged>
			property static Result Pending
			{
				Result get();
			}

			/// <summary>
			/// An invalid pointer, usually null, was passed as a parameter. 
			/// </summary>
			/// <remarks>This is a failed result.</remarks>
			/// <unmanaged href="bb219815">E_POINTER</unmanaged>
			property static Result InvalidPointer
			{
				Result get();
			}
		};
	}
}
