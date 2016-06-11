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

#include "XACT3Exception.h"
#include "Wave.h"

namespace SlimDX
{
namespace XACT3
{
	Wave::Wave(IXACT3Wave *wave)
	{
		InternalPointer = wave;
	}

	Result Wave::Destroy()
	{
		HRESULT hr = InternalPointer->Destroy();
		return RECORD_XACT3(hr);
	}

	Result Wave::Pause(bool pause)
	{
		HRESULT hr = InternalPointer->Pause(pause);
		return RECORD_XACT3(hr);
	}

	Result Wave::Play()
	{
		HRESULT hr = InternalPointer->Play();
		return RECORD_XACT3(hr);
	}

	Result Wave::SetMatrixCoefficients(int srcChannelCount, int dstChannelCount, array<float>^ matrixCoefficients)
	{
		pin_ptr<float> pinnedMatrix = &matrixCoefficients[0];

		HRESULT hr = InternalPointer->SetMatrixCoefficients(srcChannelCount, dstChannelCount, pinnedMatrix);
		return RECORD_XACT3(hr);
	}

	Result Wave::SetPitch(int pitch)
	{
		HRESULT hr = InternalPointer->SetPitch(static_cast<XACTPITCH>(pitch));
		return RECORD_XACT3(hr);
	}

	Result Wave::SetVolume(float volume)
	{
		HRESULT hr = InternalPointer->SetVolume(volume);
		return RECORD_XACT3(hr);
	}

	Result Wave::Stop(StopFlags flags)
	{
		HRESULT hr = InternalPointer->Stop((DWORD)flags);
		return RECORD_XACT3(hr);
	}

	WaveInstanceProperties Wave::Properties::get()
	{
		XACT_WAVE_INSTANCE_PROPERTIES result;

		HRESULT hr = InternalPointer->GetProperties(&result);
		RECORD_XACT3(hr);

		return WaveInstanceProperties(result);
	}

	WaveState Wave::State::get()
	{
		DWORD result;

		HRESULT hr = InternalPointer->GetState(&result);
		RECORD_XACT3(hr);

		return static_cast<WaveState>(result);
	}
}
}
