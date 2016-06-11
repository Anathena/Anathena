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

#include "XFile.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class Frame;
		ref class MeshContainer;

		///<summary>
		/// This interface is implemented by applications who wish to load additional user
		/// data embedded in .X files. D3DX calls this interface when such data is encountered.
		/// </summary>
		/// <unmanaged>ID3DXLoadUserData</unmanaged>
		public interface struct ILoadUserData
		{
			virtual void LoadFrameData( Frame^ frame, XFileData^ data ) = 0;
			virtual void LoadMeshData( MeshContainer^ meshContainer, XFileData^ data ) = 0;
			virtual void LoadTopLevelData( XFileData^ data ) = 0;
		};

		class ILoadUserDataShim : public ID3DXLoadUserData
		{
		private:
			gcroot<ILoadUserData^> m_WrappedInterface;

		public:
			ILoadUserDataShim( ILoadUserData^ wrappedInterface );

			HRESULT WINAPI LoadFrameChildData( LPD3DXFRAME pFrame, LPD3DXFILEDATA pXofChildData );
			HRESULT WINAPI LoadMeshChildData( LPD3DXMESHCONTAINER pMeshContainer, LPD3DXFILEDATA pXofChildData );
			HRESULT WINAPI LoadTopLevelData( LPD3DXFILEDATA pXofChildData );
		};
	}
}