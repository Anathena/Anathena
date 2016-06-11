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
#include <dsound.h>

#include "../Result.h"

#include "ResultCodeDS.h"

namespace SlimDX
{
namespace DirectSound
{
	ResultCode::ResultCode()
	{
	}

	Result ResultCode::Success::get()
	{
		return Result( DS_OK );
	}

	Result ResultCode::Failure::get()
	{
		return Result( E_FAIL );
	}

	Result ResultCode::NoVirtualization::get()
	{
		return Result( DS_NO_VIRTUALIZATION );
	}

	Result ResultCode::AccessDenied::get()
	{
		return Result( DSERR_ACCESSDENIED );
	}

	Result ResultCode::Allocated::get()
	{
		return Result( DSERR_ALLOCATED );
	}

	Result ResultCode::AlreadyInitialized::get()
	{
		return Result( DSERR_ALREADYINITIALIZED );
	}

	Result ResultCode::BadFormat::get()
	{
		return Result( DSERR_BADFORMAT );
	}

	Result ResultCode::BadSendBufferGuid::get()
	{
		return Result( DSERR_BADSENDBUFFERGUID );
	}

	Result ResultCode::BufferLost::get()
	{
		return Result( DSERR_BUFFERLOST );
	}

	Result ResultCode::BufferTooSmall::get()
	{
		return Result( DSERR_BUFFERTOOSMALL );
	}

	Result ResultCode::ControlUnavailable::get()
	{
		return Result( DSERR_CONTROLUNAVAIL );
	}

	Result ResultCode::DirectSound8Required::get()
	{
		return Result( DSERR_DS8_REQUIRED );
	}

	Result ResultCode::EffectsUnavailable::get()
	{
		return Result( DSERR_FXUNAVAILABLE );
	}

	Result ResultCode::Generic::get()
	{
		return Result( DSERR_GENERIC );
	}

	Result ResultCode::InvalidCall::get()
	{
		return Result( DSERR_INVALIDCALL );
	}

	Result ResultCode::InvalidParameter::get()
	{
		return Result( DSERR_INVALIDPARAM );
	}

	Result ResultCode::NoAggregation::get()
	{
		return Result( DSERR_NOAGGREGATION );
	}

	Result ResultCode::NoDriver::get()
	{
		return Result( DSERR_NODRIVER );
	}

	Result ResultCode::NoInterface::get()
	{
		return Result( DSERR_NOINTERFACE );
	}

	Result ResultCode::ObjectNotFound::get()
	{
		return Result( DSERR_OBJECTNOTFOUND );
	}

	Result ResultCode::OtherApplicationHasPriority::get()
	{
		return Result( DSERR_OTHERAPPHASPRIO );
	}

	Result ResultCode::PriorityLevelNeeded::get()
	{
		return Result( DSERR_PRIOLEVELNEEDED );
	}

	Result ResultCode::SendLoop::get()
	{
		return Result( DSERR_SENDLOOP );
	}

	Result ResultCode::Uninitialized::get()
	{
		return Result( DSERR_UNINITIALIZED );
	}

	Result ResultCode::Unsupported::get()
	{
		return Result( DSERR_UNSUPPORTED );
	}
}
}