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

#include "Direct2DException.h"

#include "FactoryD2D.h"

const IID IID_ID2D1Factory = __uuidof(ID2D1Factory);

using namespace System;

namespace SlimDX
{
namespace Direct2D
{ 
	Factory::Factory()
	{
		Init( FactoryType::SingleThreaded, DebugLevel::None );
	}

	Factory::Factory( FactoryType factoryType )
	{
		Init( factoryType, DebugLevel::None );
	}

	Factory::Factory( FactoryType factoryType, DebugLevel debugLevel )
	{
		Init( factoryType, debugLevel );
	}

	void Factory::Init( FactoryType factoryType, DebugLevel debugLevel )
	{
		ID2D1Factory *factory = NULL;

		D2D1_FACTORY_OPTIONS options;
		options.debugLevel = static_cast<D2D1_DEBUG_LEVEL>( debugLevel );

		if( RECORD_D2D( D2D1CreateFactory( static_cast<D2D1_FACTORY_TYPE>( factoryType ), IID_ID2D1Factory, &options, (void**) &factory ) ).IsFailure )
			throw gcnew Direct2DException( Result::Last );

		Construct( factory );
	}

	Result Factory::ReloadSystemMetrics()
	{
		return RECORD_D2D( InternalPointer->ReloadSystemMetrics() );
	}

	System::Drawing::SizeF Factory::DesktopDpi::get()
	{
		float x, y;
		InternalPointer->GetDesktopDpi( &x, &y );

		return System::Drawing::SizeF( x, y );
	}
}
}