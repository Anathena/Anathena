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

#include "../xapo/ParameterizedProcessor.h"

#include "EffectDescriptor.h"

const IID IID_CXAPOBase = __uuidof(IXAPO);

using namespace SlimDX::XAPO;

namespace SlimDX
{
namespace XAudio2
{
	XAUDIO2_EFFECT_DESCRIPTOR EffectDescriptor::ToUnmanaged()
	{
		XAUDIO2_EFFECT_DESCRIPTOR desc;
		desc.InitialState = InitiallyStarted;
		desc.OutputChannels = OutputChannelCount;

		ParameterizedProcessor^ pp = dynamic_cast<ParameterizedProcessor^>(Effect);
		if (pp != nullptr)
			desc.pEffect = static_cast<IXAPOParameters*>(pp->ParamPointer);
		else
		{
			BaseProcessor^ bp = dynamic_cast<BaseProcessor^>(Effect);
			if (bp != nullptr)
				desc.pEffect = bp->ImplPointer;
			else
			{
				shim = new XAPOShim(Effect);
				desc.pEffect = static_cast<IXAPOParameters*>(shim);
			}
		}

		return desc;
	}

	void EffectDescriptor::Free()
	{
		if (shim != NULL)
			shim->Release();
		shim = NULL;
	}
}
}