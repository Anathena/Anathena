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

#include <xaudio2.h>
#include <vcclr.h>

#include "../ComObject.h"
#include "../Result.h"

#include "XAudio2.h"
#include "EngineCallback.h"

using namespace System;

namespace SlimDX
{
namespace XAudio2
{
	EngineCallbackShim::EngineCallbackShim( XAudio2^ wrappedInterface )
	{
		m_WrappedInterface = wrappedInterface;
	}

	void EngineCallbackShim::OnCriticalError( HRESULT error )
	{
		m_WrappedInterface->InvokeCriticalError( gcnew ErrorEventArgs( Result( error ) ) );
	}

	void EngineCallbackShim::OnProcessingPassEnd()
	{
		m_WrappedInterface->InvokeProcessingPassEnd();
	}

	void EngineCallbackShim::OnProcessingPassStart()
	{
		m_WrappedInterface->InvokeProcessingPassStart();
	}
}
}