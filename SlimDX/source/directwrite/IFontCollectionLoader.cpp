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

#include "FactoryDW.h"
#include "FontFileEnumerator.h"
#include "IFontCollectionLoader.h"
#include "../SlimDXException.h"

using namespace SlimDX;

namespace SlimDX
{
	namespace DirectWrite
	{
		IFontCollectionLoaderShim *IFontCollectionLoaderShim::CreateInstance(IFontCollectionLoader ^wrappedInterface)
		{
			return new IFontCollectionLoaderShim(wrappedInterface);
		}

		IFontCollectionLoaderShim::IFontCollectionLoaderShim(IFontCollectionLoader ^wrappedInterface)
			: m_WrappedInterface(wrappedInterface),
			m_refCount(1)
		{
		}

		HRESULT IFontCollectionLoaderShim::QueryInterface(const IID &iid, LPVOID *ppv)
		{
			if (iid == IID_IDWriteFontCollectionLoader)
			{
				AddRef();
				*reinterpret_cast<IDWriteFontCollectionLoader**>(ppv) = this;
				return S_OK;
			}

			return E_NOTIMPL;
		}

		ULONG IFontCollectionLoaderShim::AddRef()
		{
			return ++m_refCount;
		}

		ULONG IFontCollectionLoaderShim::Release()
		{
			if (--m_refCount == 0)
			{
				delete this;
			}
			return m_refCount;
		}

		HRESULT IFontCollectionLoaderShim::CreateEnumeratorFromKey(IDWriteFactory *nativeFactory,
			void const *collectionKey,
			UINT32 collectionKeySize,
			IDWriteFontFileEnumerator **fontFileEnumerator)
		{
			try
			{
				Factory ^factory = Factory::FromPointer(nativeFactory);
				FontFileEnumerator ^enumerator = m_WrappedInterface->CreateEnumeratorFromKey(factory,
					IntPtr(const_cast<void *>(collectionKey)), collectionKeySize);
				delete factory;
				*fontFileEnumerator = enumerator->InternalPointer;
			}
			catch (SlimDXException ^ex)
			{
				return ex->ResultCode.Code;
			}
			catch (Exception ^)
			{
				return E_FAIL;
			}

			return S_OK;
		}
	}
}
