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

#include "SoundBank.h"
#include "Cue.h"

using namespace System;

namespace SlimDX
{
namespace XACT3
{
	SoundBank::SoundBank( IXACT3SoundBank *pointer )
	{
		InternalPointer = pointer;
	}

	Result SoundBank::Destroy()
	{
		HRESULT hr = InternalPointer->Destroy();
		return RECORD_XACT3(hr);
	}

	int SoundBank::GetCueIndex(String^ friendlyName)
	{
		array<unsigned char>^ friendlyNameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes(friendlyName);
		pin_ptr<unsigned char> pinnedFriendlyName = &friendlyNameBytes[0];

		XACTINDEX result = InternalPointer->GetCueIndex(reinterpret_cast<PCSTR>(pinnedFriendlyName));
		return result == XACTINDEX_INVALID ? -1 : result;
	}

	CueProperties SoundBank::GetCueProperties(int cueIndex)
	{
		XACT_CUE_PROPERTIES result;

		HRESULT hr = InternalPointer->GetCueProperties(static_cast<XACTINDEX>(cueIndex), &result);
		RECORD_XACT3(hr);

		return CueProperties(result);
	}

	Cue^ SoundBank::Prepare(int cueIndex, int timeOffset)
	{
		IXACT3Cue *cue;

		HRESULT hr = InternalPointer->Prepare(static_cast<XACTINDEX>(cueIndex), 0, timeOffset, &cue);
		if (RECORD_XACT3(hr).IsFailure)
			return nullptr;

		return gcnew Cue(cue);
	}

	Cue^ SoundBank::Play(int cueIndex, int timeOffset)
	{
		IXACT3Cue *cue = 0;

		HRESULT hr = InternalPointer->Play(static_cast<XACTINDEX>(cueIndex), 0, timeOffset, &cue);
		if (RECORD_XACT3(hr).IsFailure)
			return nullptr;

		return gcnew Cue(cue);
	}

	Result SoundBank::Stop(int cueIndex, StopFlags flags)
	{
		HRESULT hr = InternalPointer->Stop(static_cast<XACTINDEX>(cueIndex), static_cast<DWORD>(flags));
		return RECORD_XACT3(hr);
	}

	SoundBankState SoundBank::State::get()
	{
		DWORD result;

		HRESULT hr = InternalPointer->GetState(&result);
		RECORD_XACT3(hr);

		return static_cast<SoundBankState>(result);
	}

	int SoundBank::CueCount::get()
	{
		XACTINDEX result;

		HRESULT hr = InternalPointer->GetNumCues(&result);
		RECORD_XACT3(hr);

		return result;
	}
}
}
