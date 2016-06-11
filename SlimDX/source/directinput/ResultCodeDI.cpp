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

#include <dinput.h>

#include "ResultCodeDI.h"

namespace SlimDX
{
namespace DirectInput
{
	ResultCode::ResultCode()
	{
	}

	Result ResultCode::Acquired::get()
	{
		return Result( DIERR_ACQUIRED );
	}

	Result ResultCode::AlreadyInitialized::get()
	{
		return Result( DIERR_ALREADYINITIALIZED );
	}

	Result ResultCode::BadDriverVersion::get()
	{
		return Result( DIERR_BADDRIVERVER );
	}

	Result ResultCode::BetaDirectInputVersion::get()
	{
		return Result( DIERR_BETADIRECTINPUTVERSION );
	}

	Result ResultCode::BufferOverflow::get()
	{
		return Result( DI_BUFFEROVERFLOW );
	}

	Result ResultCode::DeviceFull::get()
	{
		return Result( DIERR_DEVICEFULL );
	}

	Result ResultCode::DeviceNotRegistered::get()
	{
		return Result( DIERR_DEVICENOTREG );
	}

	Result ResultCode::DownloadSkipped::get()
	{
		return Result( DI_DOWNLOADSKIPPED );
	}

	Result ResultCode::EffectPlaying::get()
	{
		return Result( DIERR_EFFECTPLAYING );
	}

	Result ResultCode::EffectRestarted::get()
	{
		return Result( DI_EFFECTRESTARTED );
	}

	Result ResultCode::Failure::get()
	{
		return Result( E_FAIL );
	}

	Result ResultCode::Generic::get()
	{
		return Result( DIERR_GENERIC );
	}

	Result ResultCode::HandleExists::get()
	{
		return Result( DIERR_HANDLEEXISTS );
	}

	Result ResultCode::HasEffects::get()
	{
		return Result( DIERR_HASEFFECTS );
	}

	Result ResultCode::IncompleteEffect::get()
	{
		return Result( DIERR_INCOMPLETEEFFECT );
	}

	Result ResultCode::InputLost::get()
	{
		return Result( DIERR_INPUTLOST );
	}

	Result ResultCode::InvalidParameter::get()
	{
		return Result( DIERR_INVALIDPARAM );
	}

	Result ResultCode::MapFileFail::get()
	{
		return Result( DIERR_MAPFILEFAIL );
	}

	Result ResultCode::MoreData::get()
	{
		return Result( DIERR_MOREDATA );
	}

	Result ResultCode::NoAggregation::get()
	{
		return Result( DIERR_NOAGGREGATION );
	}

	Result ResultCode::NoEffect::get()
	{
		return Result( DI_NOEFFECT );
	}

	Result ResultCode::NoInterface::get()
	{
		return Result( DIERR_NOINTERFACE );
	}

	Result ResultCode::NotAcquired::get()
	{
		return Result( DIERR_NOTACQUIRED );
	}

	Result ResultCode::NotAttached::get()
	{
		return Result( DI_NOTATTACHED );
	}

	Result ResultCode::NotBuffered::get()
	{
		return Result( DIERR_NOTBUFFERED );
	}

	Result ResultCode::NotDownloaded::get()
	{
		return Result( DIERR_NOTDOWNLOADED );
	}

	Result ResultCode::NotExclusivelyAcquired::get()
	{
		return Result( DIERR_NOTEXCLUSIVEACQUIRED );
	}

	Result ResultCode::NotFound::get()
	{
		return Result( DIERR_NOTFOUND );
	}

	Result ResultCode::NotInitialized::get()
	{
		return Result( DIERR_NOTINITIALIZED );
	}

	Result ResultCode::ObjectNotFound::get()
	{
		return Result( DIERR_OBJECTNOTFOUND );
	}

	Result ResultCode::OldDirectInputVersion::get()
	{
		return Result( DIERR_OLDDIRECTINPUTVERSION );
	}

	Result ResultCode::OtherApplicationHasPriority::get()
	{
		return Result( DIERR_OTHERAPPHASPRIO );
	}

	Result ResultCode::OutOfMemory::get()
	{
		return Result( DIERR_OUTOFMEMORY );
	}

	Result ResultCode::PolledDevice::get()
	{
		return Result( DI_POLLEDDEVICE );
	}

	Result ResultCode::PropertyHasNoEffect::get()
	{
		return Result( DI_PROPNOEFFECT );
	}

	Result ResultCode::ReadOnly::get()
	{
		return Result( DIERR_READONLY );
	}

	Result ResultCode::ReportFull::get()
	{
		return Result( DIERR_REPORTFULL );
	}

	Result ResultCode::SettingsNotSaved::get()
	{
		return Result( DI_SETTINGSNOTSAVED );
	}

	Result ResultCode::Success::get()
	{
		return Result( DI_OK );
	}

	Result ResultCode::Truncated::get()
	{
		return Result( DI_TRUNCATED );
	}

	Result ResultCode::TruncatedAndRestarted::get()
	{
		return Result( DI_TRUNCATEDANDRESTARTED );
	}

	Result ResultCode::Unplugged::get()
	{
		return Result( DIERR_UNPLUGGED );
	}

	Result ResultCode::Unsupported::get()
	{
		return Result( DIERR_UNSUPPORTED );
	}

	Result ResultCode::WriteProtect::get()
	{
		return Result( DI_WRITEPROTECT );
	}

	Result ResultCode::InvalidHandle::get()
	{
		return Result( E_HANDLE );
	}

	Result ResultCode::Pending::get()
	{
		return Result( E_PENDING );
	}

	Result ResultCode::InvalidPointer::get()
	{
		return Result( E_POINTER );
	}
}
}
