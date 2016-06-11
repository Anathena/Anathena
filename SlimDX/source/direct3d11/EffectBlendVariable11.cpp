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

#include "Direct3D11Exception.h"

#include "Enums11.h"
#include "BlendStateDescription11.h"
#include "EffectBlendVariable11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectBlendVariable::EffectBlendVariable( ID3DX11EffectBlendVariable* pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = pointer;
	}
	
	EffectBlendVariable::EffectBlendVariable( IntPtr pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectBlendVariable*>( pointer.ToPointer() );
	}

	BlendState^ EffectBlendVariable::GetBlendState(int index)
	{
		ID3D11BlendState *result;

		HRESULT hr = m_Pointer->GetBlendState(index, &result);
		if (RECORD_D3D11(hr).IsFailure)
			return nullptr;

		return BlendState::FromPointer(result);
	}

	Result EffectBlendVariable::SetBlendState(int index, BlendState^ blendState)
	{
		HRESULT hr = m_Pointer->SetBlendState(index, blendState->InternalPointer);
		return RECORD_D3D11(hr);
	}

	BlendStateDescription EffectBlendVariable::GetBackingStore(int index)
	{
		D3D11_BLEND_DESC desc;

		HRESULT hr = m_Pointer->GetBackingStore(index, &desc);
		if (RECORD_D3D11(hr).IsFailure)
			return BlendStateDescription();

		return BlendStateDescription(desc);
	}

	Result EffectBlendVariable::UndoSetBlendState(int index)
	{
		HRESULT hr = m_Pointer->UndoSetBlendState(index);
		return RECORD_D3D11(hr);
	}
}
}
