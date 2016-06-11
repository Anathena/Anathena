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
#include "WaveBank.h"

using namespace System;

namespace SlimDX
{
namespace XACT3
{
	WaveBank::WaveBank( IXACT3WaveBank *pointer )
	{
		InternalPointer = pointer;
		handle = nullptr;
	}

	WaveBank::WaveBank( IXACT3WaveBank *pointer, Microsoft::Win32::SafeHandles::SafeFileHandle^ file )
	{
		InternalPointer = pointer;
		handle = file;
	}

	Result WaveBank::Destroy()
	{
		HRESULT hr = InternalPointer->Destroy();
		if (handle != nullptr)
			delete handle;

		return RECORD_XACT3(hr);
	}

	int WaveBank::GetWaveIndex(String^ friendlyName)
	{
		array<unsigned char>^ friendlyNameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes(friendlyName);
		pin_ptr<unsigned char> pinnedFriendlyName = &friendlyNameBytes[0];

		XACTINDEX result = InternalPointer->GetWaveIndex(reinterpret_cast<PCSTR>(pinnedFriendlyName));
		return result == XACTINDEX_INVALID ? -1 : result;
	}

	WaveProperties WaveBank::GetWaveProperties(int waveIndex)
	{
		XACT_WAVE_PROPERTIES props;

		HRESULT hr = InternalPointer->GetWaveProperties(static_cast<XACTINDEX>(waveIndex), &props);
		RECORD_XACT3(hr);

		return WaveProperties(props);
	}

	Wave^ WaveBank::Play(int waveIndex, ContentPreparationFlags flags, int playOffset, int loopCount)
	{
		IXACT3Wave* result;

		HRESULT hr = InternalPointer->Play(static_cast<XACTINDEX>(waveIndex), static_cast<DWORD>(flags), playOffset, static_cast<XACTLOOPCOUNT>(loopCount), &result);
		if (RECORD_XACT3(hr).IsFailure)
			return nullptr;

		return gcnew Wave(result);
	}

	Wave^ WaveBank::Prepare(int waveIndex, ContentPreparationFlags flags, int playOffset, int loopCount)
	{
		IXACT3Wave* result;

		HRESULT hr = InternalPointer->Prepare(static_cast<XACTINDEX>(waveIndex), static_cast<DWORD>(flags), playOffset, static_cast<XACTLOOPCOUNT>(loopCount), &result);
		if (RECORD_XACT3(hr).IsFailure)
			return nullptr;

		return gcnew Wave(result);
	}

	Result WaveBank::Stop(int waveIndex, StopFlags flags)
	{
		HRESULT hr = InternalPointer->Stop(static_cast<XACTINDEX>(waveIndex), static_cast<DWORD>(flags));
		return RECORD_XACT3(hr);
	}

	WaveBankState WaveBank::State::get()
	{
		DWORD result;

		HRESULT hr = InternalPointer->GetState(&result);
		RECORD_XACT3(hr);

		return static_cast<WaveBankState>(result);
	}

	int WaveBank::WaveCount::get()
	{
		XACTINDEX count;

		HRESULT hr = InternalPointer->GetNumWaves(&count);
		RECORD_XACT3(hr);

		return count;
	}
}
}
