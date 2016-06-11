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

#include "XACT3Exception.h"

using namespace System;
using namespace System::Runtime::Serialization;

namespace SlimDX
{
namespace XACT3
{
	XACT3Exception::XACT3Exception( SerializationInfo^ info, StreamingContext context )
	: SlimDXException( info, context )
	{
	}

	XACT3Exception::XACT3Exception()
	{
	}

	XACT3Exception::XACT3Exception( String^ message )
	: SlimDXException( message )
	{
	}

	XACT3Exception::XACT3Exception( String^ message, Exception^ innerException )
	: SlimDXException( message, innerException )
	{
	}

	XACT3Exception::XACT3Exception( Result result )
	: SlimDXException( result )
	{
	}
}
}
