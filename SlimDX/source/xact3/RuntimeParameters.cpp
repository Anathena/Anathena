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

#include "../DataStream.h"

#include "RuntimeParameters.h"

using namespace System;

namespace SlimDX
{
namespace XACT3
{
	XACT_RUNTIME_PARAMETERS RuntimeParameters::ToUnmanaged()
	{
		XACT_RUNTIME_PARAMETERS results;
		memset(&results, 0, sizeof(XACT_RUNTIME_PARAMETERS));

		try
		{
			results.lookAheadTime = LookAheadTime;

			if (!String::IsNullOrEmpty(RendererId))
			{
				pin_ptr<const wchar_t> pinnedId = PtrToStringChars(RendererId);
				results.pRendererID = new WCHAR[RendererId->Length];
				
				memcpy(results.pRendererID, pinnedId, sizeof(wchar_t) * RendererId->Length);
			}

			if (AudioDevice != nullptr)
				results.pXAudio2 = AudioDevice->InternalPointer;
			if (MasteringVoice != nullptr)
				results.pMasteringVoice = reinterpret_cast<IXAudio2MasteringVoice*>(MasteringVoice->InternalPointer);

			if (SettingsBuffer != nullptr)
			{
				int size = static_cast<int>(SettingsBuffer->Length - SettingsBuffer->Position);
				results.pGlobalSettingsBuffer = CoTaskMemAlloc(size);
				results.globalSettingsBufferSize = size;
				results.globalSettingsFlags = XACT_FLAG_ENGINE_CREATE_MANAGEDATA;

				DataStream^ ds = dynamic_cast<DataStream^>(SettingsBuffer);
				if (ds != nullptr)
					memcpy(results.pGlobalSettingsBuffer, ds->PositionPointer, size);
				else
				{
					array<Byte>^ buffer = gcnew array<Byte>(size); 
					int bytesRead = 0;
					while (bytesRead < size)
						bytesRead += SettingsBuffer->Read(buffer, bytesRead, size - bytesRead);

					pin_ptr<Byte> pinnedBuffer = &buffer[0];
					memcpy(results.pGlobalSettingsBuffer, pinnedBuffer, size);
				}
			}
		}
		catch (...)
		{
			delete[] results.pRendererID;
			CoTaskMemFree(results.pGlobalSettingsBuffer);

			throw;
		}

		return results;
	}
}
}