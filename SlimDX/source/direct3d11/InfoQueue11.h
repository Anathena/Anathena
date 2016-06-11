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

#include "../ComObject.h"

#include "Enums11.h"
#include "DebugMessageID11.h"
#include "InfoQueueFilter11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class Device;

		public ref class InfoQueue : ComObject
		{
			COMOBJECT(ID3D11InfoQueue, InfoQueue);

		public:
			InfoQueue(Device^ device);

			/*Result AddApplicationMessage(MessageSeverity severity, System::String^ description);
			Result AddMessage(MessageCategory category, MessageSeverity severity, DebugMessageID messageId, System::String^ description);
			Result AddMessage(DebugMessage message);
			Result AddRetrievalFilters(... array<InfoQueueFilter>^ filters);
			Result AddStorageFilters(... array<InfoQueueFilter>^ filters);

			void ClearRetrievalFilters();
			void ClearStorageFilters();
			void ClearStoredMessages();

			bool IsBreakingOn(MessageCategory category);
			bool IsBreakingOn(DebugMessageID messageId);
			bool IsBreakingOn(MessageSeverity severity);

			DebugMessage GetMessage(int index);
			InfoQueueFilter PeekRetrievalFilter();
			InfoQueueFilter PeekStorageFilter();

			void PopRetrievalFilter();
			void PopStorageFilter();

			void PushCopyOfRetrievalFilter();
			void PushCopyOfStorageFilter();
			void PushEmptyRetrievalFilter();
			void PushEmptyStorageFilter();

			void PushRetrievalFilter(InfoQueueFilter filter);
			void PushStorageFilter(InfoQueueFilter filter);

			Result BreakOn(MessageCategory category, bool enable);
			Result BreakOn(DebugMessageID messageId, bool enable);
			Result BreakOn(MessageSeverity severity, bool enable);

			property bool MuteOutput
			{
				bool get();
				void set(bool value);
			}

			property System::Int64 MessageCountLimit
			{
				System::Int64 get();
				void set(System::Int64 value);
			}

			property System::Int64 NumberOfMessagesAllowedByStorageFilter
			{
				System::Int64 get();
			}

			property System::Int64 NumberOfMessagesDeniedByStorageFilter
			{
				System::Int64 get();
			}

			property System::Int64 NumberOfMessagesDiscaredByMessageCountLimit
			{
				System::Int64 get();
			}

			property System::Int64 NumberOfStoredMessages
			{
				System::Int64 get();
			}

			property System::Int64 NumberOfStoredMessagesAllowedByRetrievalFilter
			{
				System::Int64 get();
			}

			property int RetrievalFilterStackSize
			{
				int get();
			}

			property int StorageFilterStackSize
			{
				int get();
			}*/
		};
	}
}