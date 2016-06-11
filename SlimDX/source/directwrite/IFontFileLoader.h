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

namespace SlimDX
{
	namespace DirectWrite
	{
		ref class FontFileStream;

		/// <summary>
		/// Font file loader interface handles loading font file resources of a particular type from a key.
		/// The font file loader interface is recommended to be implemented by a singleton object.
		/// IMPORTANT: font file loader implementations must not register themselves with DirectWrite factory
		/// inside their constructors and must not unregister themselves in their destructors, because
		/// registration and underregistration operations increment and decrement the object reference count respectively.
		/// Instead, registration and underregistration of font file loaders with DirectWrite factory should be performed
		/// outside of the font file loader implementation as a separate step.
		/// </summary>
		public interface struct IFontFileLoader
		{
			/// <summary>
			/// Creates a font file stream object that encapsulates an open file resource.
			/// The resource is closed when the last reference to fontFileStream is released.
			/// </summary>
			/// <param name="fontFileReferenceKey">Font file reference key that uniquely identifies the font file resource
			/// within the scope of the font loader being used.</param>
			/// <param name="fontFileReferenceKeySize">Size of font file reference key in bytes.</param>
			/// <returns>
			/// Reference to the newly created font file stream.
			/// </returns>
			FontFileStream ^CreateStreamFromKey(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize);
		};

		class IFontFileLoaderShim : public IDWriteFontFileLoader
		{
		public:
			static IFontFileLoaderShim *CreateInstance(IFontFileLoader ^loader);

			STDMETHOD(QueryInterface)(REFIID riid, void **ppvObject);
			STDMETHOD_(ULONG, AddRef)();
			STDMETHOD_(ULONG, Release)();

			STDMETHOD(CreateStreamFromKey)(void const *fontFileReferenceKey, UINT32 fontFileReferenceKeySize, IDWriteFontFileStream **fontFileStream);

		private:
			IFontFileLoaderShim(IFontFileLoader ^wrappedInterface);

			int m_refCount;
			gcroot<IFontFileLoader ^> m_wrappedInterface;
		};
	}
}
