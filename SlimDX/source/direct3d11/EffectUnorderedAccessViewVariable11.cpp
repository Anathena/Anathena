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

#include "../Utilities.h"
#include "../stack_array.h"

#include "Direct3D11Exception.h"

#include "EffectUnorderedAccessViewVariable11.h"
#include "UnorderedAccessView11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectUnorderedAccessViewVariable::EffectUnorderedAccessViewVariable( ID3DX11EffectUnorderedAccessViewVariable* pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = pointer;
	}
	
	EffectUnorderedAccessViewVariable::EffectUnorderedAccessViewVariable( IntPtr pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectUnorderedAccessViewVariable*>( pointer.ToPointer() );
	}

	UnorderedAccessView^ EffectUnorderedAccessViewVariable::GetView()
	{
		ID3D11UnorderedAccessView* view = 0;
		if( RECORD_D3D11( m_Pointer->GetUnorderedAccessView( &view ) ).IsFailure )
			return nullptr;
			
		return UnorderedAccessView::FromPointer( view );
	}

	Result EffectUnorderedAccessViewVariable::GetViewArray(array<UnorderedAccessView^>^ views)
	{
		return GetViewArray(views, 0, views->Length);
	}

	Result EffectUnorderedAccessViewVariable::GetViewArray(array<UnorderedAccessView^>^ views, int offset, int count)
	{
		Utilities::CheckArrayBounds(views, offset, count);

		stack_array<ID3D11UnorderedAccessView*> nativeViews = stackalloc(ID3D11UnorderedAccessView*, count);
		HRESULT hr = m_Pointer->GetUnorderedAccessViewArray(&nativeViews[0], 0, count);
		if (RECORD_D3D11(hr).IsFailure)
			return Result::Last;

		for (int i = 0; i < count; i++)
			views[i + offset] = UnorderedAccessView::FromPointer(nativeViews[i]);

		return Result::Last;
	}
	
	Result EffectUnorderedAccessViewVariable::SetView( UnorderedAccessView^ view )
	{
		if( view == nullptr )
			return RECORD_D3D11( m_Pointer->SetUnorderedAccessView( 0 ) );
		else
			return RECORD_D3D11( m_Pointer->SetUnorderedAccessView( static_cast<ID3D11UnorderedAccessView*>( view->InternalPointer ) ) );
	}

	Result EffectUnorderedAccessViewVariable::SetViewArray( array<UnorderedAccessView^>^ views )
	{
		return SetViewArray(views, 0, views->Length);
	}

	Result EffectUnorderedAccessViewVariable::SetViewArray( array<UnorderedAccessView^>^ views, int offset, int count )
	{
		Utilities::CheckArrayBounds(views, offset, count);

		stack_array<ID3D11UnorderedAccessView*> nativeViews = stackalloc(ID3D11UnorderedAccessView*, count);
		for (int i = 0; i < count; i++)
			nativeViews[i] = views[i + offset]->InternalPointer;

		HRESULT hr = m_Pointer->SetUnorderedAccessViewArray(&nativeViews[0], 0, count);
		return RECORD_D3D11(hr);
	}
}
}
