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
#include <windows.h>
#include <dsound.h>

#include "../ComObject.h"

#include "DirectSoundException.h"

#include "Enums.h"
#include "AcousticEchoCancel.h"

using namespace System;

namespace SlimDX
{
namespace DirectSound
{
	Result AcousticEchoCancel::Reset()
	{
		HRESULT hr = InternalPointer->Reset();
		return RECORD_DSOUND( hr );
	}

	bool AcousticEchoCancel::Enabled::get()
	{
		DSCFXAec param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return false;

		return param.fEnable == TRUE;
	}

	void AcousticEchoCancel::Enabled::set( bool value )
	{
		DSCFXAec param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fEnable = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	bool AcousticEchoCancel::NoiseFill::get()
	{
		DSCFXAec param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return false;

		return param.fNoiseFill == TRUE;
	}

	void AcousticEchoCancel::NoiseFill::set( bool value )
	{
		DSCFXAec param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.fNoiseFill = value;
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	AcousticEchoCancelMode AcousticEchoCancel::Mode::get()
	{
		DSCFXAec param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return AcousticEchoCancelMode::PassThrough;

		return static_cast<AcousticEchoCancelMode>( param.dwMode );
	}

	void AcousticEchoCancel::Mode::set( AcousticEchoCancelMode value )
	{
		DSCFXAec param;

		HRESULT hr = InternalPointer->GetAllParameters( &param );
		if( RECORD_DSOUND( hr ).IsFailure )
			return;

		param.dwMode = static_cast<DWORD>( value );
		hr = InternalPointer->SetAllParameters( &param );
		RECORD_DSOUND( hr );
	}

	bool AcousticEchoCancel::Uninitialized::get()
	{
		DWORD status = 0;
		HRESULT hr = InternalPointer->GetStatus( &status );
		RECORD_DSOUND( hr );

		return ( status & DSCFX_AEC_STATUS_HISTORY_UNINITIALIZED ) != 0;
	}

	bool AcousticEchoCancel::ContinuouslyConverged::get()
	{
		DWORD status = 0;
		HRESULT hr = InternalPointer->GetStatus( &status );
		RECORD_DSOUND( hr );

		return ( status & DSCFX_AEC_STATUS_HISTORY_CONTINUOUSLY_CONVERGED ) != 0;
	}

	bool AcousticEchoCancel::CurrentlyConverged::get()
	{
		DWORD status = 0;
		HRESULT hr = InternalPointer->GetStatus( &status );
		RECORD_DSOUND( hr );

		return ( status & DSCFX_AEC_STATUS_CURRENTLY_CONVERGED ) != 0;
	}

	bool AcousticEchoCancel::PreviouslyDiverged::get()
	{
		DWORD status = 0;
		HRESULT hr = InternalPointer->GetStatus( &status );
		RECORD_DSOUND( hr );

		return ( status & DSCFX_AEC_STATUS_HISTORY_PREVIOUSLY_DIVERGED ) != 0;
	}
}
}