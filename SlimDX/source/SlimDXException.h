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
#pragma once

#include "Configuration.h"
#include "InternalHelpers.h"
#include "Result.h"

#define RECORD_SDX(x) Result::Record<SlimDXException^>( (x), (nullptr), (nullptr) )

namespace SlimDX
{
	/// <summary>
	/// The base class for errors that occur in SlimDX.
	/// </summary>
	/// <unmanaged>None</unmanaged>
	[System::Serializable]
	public ref class SlimDXException : public System::Exception
	{
	private:
		Result m_Result;
	
	protected:
		SlimDXException( System::Runtime::Serialization::SerializationInfo^ info, System::Runtime::Serialization::StreamingContext context );

	public:
		/// <summary>
		/// Gets the <see cref="Result">Result code</see> for the exception. This value indicates
		/// the specific type of failure that occured within SlimDX.
		/// </summary>
		property Result ResultCode
		{
			Result get();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDXException"/> class.
		/// </summary>
		SlimDXException();
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDXException"/> class.
		/// </summary>
		/// <param name="message">The message describing the exception.</param>
		SlimDXException( System::String^ message );
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDXException"/> class.
		/// </summary>
		/// <param name="message">The message describing the exception.</param>
		/// <param name="innerException">The exception that caused this exception.</param>
		SlimDXException( System::String^ message, System::Exception^ innerException );
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SlimDXException"/> class.
		/// </summary>
		/// <param name="result">The result code that caused this exception.</param>
		SlimDXException( Result result );
		
		/// <summary>
		/// When overridden in a derived class, sets the <see cref="System::Runtime::Serialization::SerializationInfo"/> with information about the exception.
		/// </summary>
		/// <param name="info">The <see cref="System::Runtime::Serialization::SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="System::Runtime::Serialization::StreamingContext"/> that contains contextual information about the source or destination.</param>
		[System::Security::Permissions::SecurityPermission(System::Security::Permissions::SecurityAction::LinkDemand, Flags = System::Security::Permissions::SecurityPermissionFlag::SerializationFormatter)]
		virtual void GetObjectData(System::Runtime::Serialization::SerializationInfo^ info, System::Runtime::Serialization::StreamingContext context) override;
	};
}
