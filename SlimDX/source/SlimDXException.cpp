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
#include <dxerr.h>

#include "SlimDXException.h"

using namespace System;
using namespace System::Runtime::Serialization;

namespace SlimDX
{
	SlimDXException::SlimDXException( SerializationInfo^ info, StreamingContext context )
	: Exception( info, context )
	{
		m_Result = safe_cast<Result>( info->GetValue( "Result", Result::typeid ) );
	}

	SlimDXException::SlimDXException()
	: Exception( "A SlimDX exception occurred." )
	{
		m_Result = Result( E_FAIL );
	}

	SlimDXException::SlimDXException( String^ message )
	: Exception( message )
	{
		m_Result = Result( E_FAIL );
	}

	SlimDXException::SlimDXException( String^ message, Exception^ innerException )
	: Exception( message, innerException )
	{
		m_Result = Result( E_FAIL );
	}
	
	SlimDXException::SlimDXException( Result result )
	: Exception( result.ToString() )
	{
		m_Result = result;
	}
	
	Result SlimDXException::ResultCode::get()
	{
		return m_Result;
	}
	
	void SlimDXException::GetObjectData(System::Runtime::Serialization::SerializationInfo^ info, System::Runtime::Serialization::StreamingContext context)
	{
		if( info == nullptr )
			throw gcnew System::ArgumentNullException( "info" );

		info->AddValue("Result", m_Result);
		System::Exception::GetObjectData( info, context );
	}
}
