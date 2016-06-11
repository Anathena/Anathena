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

#include "EffectResourceVariable11.h"
#include "ShaderResourceView11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	EffectResourceVariable::EffectResourceVariable( ID3DX11EffectShaderResourceVariable* pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = pointer;
	}
	
	EffectResourceVariable::EffectResourceVariable( IntPtr pointer )
	: EffectVariable( pointer )
	{
		m_Pointer = reinterpret_cast<ID3DX11EffectShaderResourceVariable*>( pointer.ToPointer() );
	}

	ShaderResourceView^ EffectResourceVariable::GetResource()
	{
		ID3D11ShaderResourceView* view = 0;
		if( RECORD_D3D11( m_Pointer->GetResource( &view ) ).IsFailure )
			return nullptr;
			
		return ShaderResourceView::FromPointer( view );
	}

	Result EffectResourceVariable::GetResourceArray(array<ShaderResourceView^>^ views)
	{
		return GetResourceArray(views, 0, views->Length);
	}

	Result EffectResourceVariable::GetResourceArray(array<ShaderResourceView^>^ views, int offset, int count)
	{
		Utilities::CheckArrayBounds(views, offset, count);

		stack_array<ID3D11ShaderResourceView*> nativeViews = stackalloc(ID3D11ShaderResourceView*, count);
		HRESULT hr = m_Pointer->GetResourceArray(&nativeViews[0], 0, count);
		if (RECORD_D3D11(hr).IsFailure)
			return Result::Last;

		for (int i = 0; i < count; i++)
			views[i + offset] = ShaderResourceView::FromPointer(nativeViews[i]);

		return Result::Last;
	}
	
	Result EffectResourceVariable::SetResource( ShaderResourceView^ view )
	{
		if( view == nullptr )
			return RECORD_D3D11( m_Pointer->SetResource( 0 ) );
		else
			return RECORD_D3D11( m_Pointer->SetResource( static_cast<ID3D11ShaderResourceView*>( view->InternalPointer ) ) );
	}

	Result EffectResourceVariable::SetResourceArray( array<ShaderResourceView^>^ views )
	{
		return SetResourceArray(views, 0, views->Length);
	}

	Result EffectResourceVariable::SetResourceArray( array<ShaderResourceView^>^ views, int offset, int count )
	{
		Utilities::CheckArrayBounds(views, offset, count);

		stack_array<ID3D11ShaderResourceView*> nativeViews = stackalloc(ID3D11ShaderResourceView*, count);
		for (int i = 0; i < count; i++)
			nativeViews[i] = views[i + offset] != nullptr ? views[i + offset]->InternalPointer : 0;

		HRESULT hr = m_Pointer->SetResourceArray(&nativeViews[0], 0, count);
		return RECORD_D3D11(hr);
	}
}
}
