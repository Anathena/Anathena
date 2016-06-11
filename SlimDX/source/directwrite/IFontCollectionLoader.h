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

#include <dwrite.h>

extern const IID IID_IDWriteFontCollectionLoader;

namespace SlimDX
{
	namespace DirectWrite
	{
		using namespace System;
		ref class Factory;
		ref class FontFileEnumerator;

		/// <summary>
		/// The font collection loader interface is used to construct a collection of fonts given a particular type of key.
		/// The font collection loader interface is recommended to be implemented by a singleton object.
		/// IMPORTANT: font collection loader implementations must not register themselves with a DirectWrite factory
		/// inside their constructors and must not unregister themselves in their destructors, because
		/// registration and unregistraton operations increment and decrement the object reference count respectively.
		/// Instead, registration and unregistration of font file loaders with DirectWrite factory should be performed
		/// outside of the font file loader implementation as a separate step.
		/// </summary>
		public interface struct IFontCollectionLoader
		{
			/// <summary>
			/// Creates a font file enumerator object that encapsulates a collection of font files.
			/// The font system calls back to this interface to create a font collection.
			/// </summary>
			/// <param name="factory">Factory associated with the loader.</param>
			/// <param name="collectionKey">Font collection key that uniquely identifies the collection of font files within
			/// the scope of the font collection loader being used.</param>
			/// <param name="collectionKeySize">Size of the font collection key in bytes.</param>
			/// <returns>
			/// Pointer to the newly created font file enumerator.
			/// </returns>
			FontFileEnumerator ^CreateEnumeratorFromKey(Factory ^factory, IntPtr collectionKey, int collectionKeySize);
		};

		class IFontCollectionLoaderShim : public IDWriteFontCollectionLoader
		{
		public:
			static IFontCollectionLoaderShim *CreateInstance(IFontCollectionLoader ^wrappedInterface);

			STDMETHOD(QueryInterface)(REFIID riid, void **ppvObject);
			STDMETHOD_(ULONG, AddRef)();
			STDMETHOD_(ULONG, Release)();

			STDMETHOD(CreateEnumeratorFromKey)(IDWriteFactory *factory, void const *collectionKey, UINT32 collectionKeySize, IDWriteFontFileEnumerator **fontFileEnumerator);

		private:
			IFontCollectionLoaderShim(IFontCollectionLoader ^wrappedInterface);

			int m_refCount;
			gcroot<IFontCollectionLoader ^> m_WrappedInterface;
		};
	}
}