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

#include "Enums.h"

#include "XFileSaveObject.h"
#include "XFileEnumerationObject.h"
#include "XFileData.h"
#include "XFileSaveData.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		public ref class XFile : public ComObject
		{
			COMOBJECT(ID3DXFile, XFile);

		private:
			XFileEnumerationObject^ CreateEnumerationObject_Internal( const void* memory, SIZE_T size );
			Result RegisterTemplates_Internal( const void* memory, SIZE_T size );

		public:
			XFile();
			
			XFileEnumerationObject^ CreateEnumerationObject( System::String^ fileName, System::Runtime::InteropServices::CharSet charSet );
			XFileEnumerationObject^ CreateEnumerationObject( array<System::Byte>^ memory );
			XFileEnumerationObject^ CreateEnumerationObject( System::IO::Stream^ stream );
			XFileSaveObject^ CreateSaveObject( System::String^ fileName, System::Runtime::InteropServices::CharSet charSet, XFileFormat format );

			Result RegisterEnumerationTemplates( XFileEnumerationObject^ enumerationObject );
			Result RegisterTemplates( array<System::Byte>^ memory );
			Result RegisterTemplates( System::IO::Stream^ memory );
			Result RegisterTemplates( System::String^ name );

			static property System::String^ DefaultTemplates
			{
				System::String^ get();
			}

			static property System::String^ SkinTemplates
			{
				System::String^ get();
			}

			static property System::String^ ExtensionTemplates
			{
				System::String^ get();
			}
		};
	}
}