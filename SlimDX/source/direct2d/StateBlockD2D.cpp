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

#include <d2d1.h>
#include <d2d1helper.h>
#include <dwrite.h>

#include "Direct2DException.h"

#include "StateBlockD2D.h"
#include "../directwrite/RenderingParameters.h"

const IID IID_ID2D1DrawingStateBlock = __uuidof(ID2D1DrawingStateBlock);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{
	StateBlock::StateBlock( SlimDX::Direct2D::Factory^ factory )
	{
		ID2D1DrawingStateBlock *stateBlock = NULL;

		HRESULT hr = factory->InternalPointer->CreateDrawingStateBlock( &stateBlock );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( stateBlock );
	}

	StateBlock::StateBlock( SlimDX::Direct2D::Factory^ factory, DrawingStateDescription description )
	{
		ID2D1DrawingStateBlock *stateBlock = NULL;

		HRESULT hr = factory->InternalPointer->CreateDrawingStateBlock( reinterpret_cast<D2D1_DRAWING_STATE_DESCRIPTION*>( &description ), NULL, &stateBlock );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( stateBlock );
	}

	StateBlock::StateBlock( SlimDX::Direct2D::Factory^ factory, DrawingStateDescription description, SlimDX::DirectWrite::RenderingParameters^ textRenderingParameters )
	{
		ID2D1DrawingStateBlock *stateBlock = NULL;

		HRESULT hr = factory->InternalPointer->CreateDrawingStateBlock( reinterpret_cast<D2D1_DRAWING_STATE_DESCRIPTION*>( &description ),
			textRenderingParameters->InternalPointer, &stateBlock );

		if( RECORD_D2D( hr ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( stateBlock );
	}

	DrawingStateDescription StateBlock::Description::get()
	{
		DrawingStateDescription description;
		InternalPointer->GetDescription( reinterpret_cast<D2D1_DRAWING_STATE_DESCRIPTION*>( &description ) );

		return description;
	}

	void StateBlock::Description::set( DrawingStateDescription value )
	{
		InternalPointer->SetDescription( reinterpret_cast<D2D1_DRAWING_STATE_DESCRIPTION*>( &value ) );
	}

	SlimDX::DirectWrite::RenderingParameters^ StateBlock::TextRenderingParameters::get()
	{
		IDWriteRenderingParams *pointer = NULL;
		InternalPointer->GetTextRenderingParams( &pointer );

		return SlimDX::DirectWrite::RenderingParameters::FromPointer( pointer );
	}

	void StateBlock::TextRenderingParameters::set( SlimDX::DirectWrite::RenderingParameters^ value )
	{
		InternalPointer->SetTextRenderingParams( value->InternalPointer );
	}
}
}