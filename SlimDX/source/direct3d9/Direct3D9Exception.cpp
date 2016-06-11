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

#include "Direct3D9Exception.h"

using namespace System;
using namespace System::Runtime::Serialization;

namespace SlimDX
{
namespace Direct3D9
{
	Direct3D9Exception::Direct3D9Exception( SerializationInfo^ info, StreamingContext context )
	: SlimDXException( info, context )
	{
	}

	Direct3D9Exception::Direct3D9Exception()
	{
	}

	Direct3D9Exception::Direct3D9Exception( String^ message )
	: SlimDXException( message )
	{
	}

	Direct3D9Exception::Direct3D9Exception( String^ message, Exception^ innerException )
	: SlimDXException( message, innerException )
	{
	}

	Direct3D9Exception::Direct3D9Exception( Result result )
	: SlimDXException( result )
	{
	}
}
}
