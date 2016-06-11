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

#include <windows.h>

#include "../InternalHelpers.h"
#include "MessagePump.h"

using namespace System;
using namespace System::Windows::Forms;

namespace SlimDX
{
namespace Windows
{
	bool MessagePump::IsApplicationIdle::get()
	{
		MSG msg;
		return !PeekMessage( &msg, NULL, 0, 0, 0 );
	}

	void MessagePump::Run( MainLoop^ mainLoop )
	{
		IdleHandler^ handler = gcnew IdleHandler( mainLoop );
		Application::Idle += gcnew EventHandler( handler, &IdleHandler::OnIdle );

		Application::Run();
	}

	void MessagePump::Run( Form^ form, MainLoop^ mainLoop )
	{
		IdleHandler^ handler = gcnew IdleHandler( mainLoop );
		Application::Idle += gcnew EventHandler( handler, &IdleHandler::OnIdle );

		Application::Run( form );
	}

	void MessagePump::Run( ApplicationContext^ context, MainLoop^ mainLoop )
	{
		IdleHandler^ handler = gcnew IdleHandler( mainLoop );
		Application::Idle += gcnew EventHandler( handler, &IdleHandler::OnIdle );

		Application::Run( context );
	}
}
}