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

#include "../stack_array.h"

#include "Direct3D11Exception.h"
#include "InfoQueue11.h"
#include "Device11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	InfoQueue::InfoQueue( Device^ device )
	{
		if( device == nullptr )
			throw gcnew ArgumentNullException( "device" );
	
		void *result = 0;
		if( RECORD_D3D11( device->InternalPointer->QueryInterface( IID_ID3D11InfoQueue, &result ) ).IsFailure )
			throw gcnew Direct3D11Exception( Result::Last );
		
		Construct( reinterpret_cast<ID3D11InfoQueue*>( result ) );
	}

	/*Result InfoQueue::AddApplicationMessage(MessageSeverity severity, System::String^ description)
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes(description);
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = InternalPointer->AddApplicationMessage(static_cast<D3D11_MESSAGE_SEVERITY>(severity), reinterpret_cast<LPCSTR>(pinnedName));
		return RECORD_D3D11(hr);
	}

	Result InfoQueue::AddMessage(MessageCategory category, MessageSeverity severity, DebugMessageID messageId, String^ description)
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes(description);
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = InternalPointer->AddMessage(static_cast<D3D11_MESSAGE_CATEGORY>(category), static_cast<D3D11_MESSAGE_SEVERITY>(severity), 
			static_cast<D3D11_MESSAGE_ID>(messageId), reinterpret_cast<LPCSTR>(pinnedName));

		return RECORD_D3D11(hr);
	}

	Result InfoQueue::AddMessage(DebugMessage message)
	{
		return AddMessage(message.Category, message.Severity, message.MessageID, message.Description);
	}

	Result InfoQueue::AddRetrievalFilters(... array<InfoQueueFilter>^ filters)
	{
		if (filters == nullptr || filters->Length == 0)
			return RECORD_D3D11(S_OK);

		int categoryCount = 0;
		int severityCount = 0;
		int idCount = 0;

		for (int i = 0; i < filters->Length; i++)
			filters[i].Count(categoryCount, severityCount, idCount);

		stack_array<D3D11_INFO_QUEUE_FILTER> input = stackalloc(D3D11_INFO_QUEUE_FILTER, filters->Length);
		stack_array<D3D11_MESSAGE_CATEGORY> categories = stackalloc(D3D11_MESSAGE_CATEGORY, categoryCount);
		stack_array<D3D11_MESSAGE_SEVERITY> severities = stackalloc(D3D11_IMESSAGE_SEVERITY, severityCount);
		stack_array<D3D11_MESSAGE_ID> messageIds = stackalloc(D3D11_MESSAGE_ID, idCount);

		categoryCount = 0;
		severityCount = 0;
		idCount = 0;

		for (int i = 0; i < filters->Length; i++)
		{
			input[i] = filters[i].Marshal(&categories[categoryCount], &severities[severityCount], &messageIds[idCount]);
			filters[i].Count(categoryCount, severityCount, idCount);
		}

		HRESULT hr = InternalPointer->AddRetrievalFilterEntries(
	}

	Result InfoQueue::AddStorageFilters(... array<InfoQueueFilter>^ filters)
	{
	}*/
}
}