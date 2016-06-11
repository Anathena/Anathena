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
#include <d3d9.h>
#include <d3dx9.h>
#include <vcclr.h>

#include "../ComObject.h"
#include "../DataStream.h"
#include "../Utilities.h"
#include "../SlimDXException.h"

#include "Device.h"
#include "Mesh.h"
#include "MeshContainer.h"
#include "ISaveUserData.h"
#include "AnimationFrame.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	ISaveUserDataShim::ISaveUserDataShim( ISaveUserData^ wrappedInterface )
	{
		m_WrappedInterface = wrappedInterface;
	}

	HRESULT ISaveUserDataShim::AddFrameChildData( const D3DXFRAME *pFrame, LPD3DXFILESAVEOBJECT pXofSave, LPD3DXFILESAVEDATA pXofFrameData )
	{
		try
		{
			XFileSaveObject^ saveObject = XFileSaveObject::FromPointer( pXofSave );
			XFileSaveData^ saveData = XFileSaveData::FromPointer( pXofFrameData );
			m_WrappedInterface->AddFrameChildData( const_cast<FrameShim*>( static_cast<const FrameShim*>( pFrame ) )->GetFrame(), saveObject, saveData );
			delete saveObject;
			delete saveData;
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return D3D_OK;
	}

	HRESULT ISaveUserDataShim::AddMeshChildData( const D3DXMESHCONTAINER *pMeshContainer, LPD3DXFILESAVEOBJECT pXofSave, LPD3DXFILESAVEDATA pXofMeshData )
	{
		try
		{
			XFileSaveObject^ saveObject = XFileSaveObject::FromPointer( pXofSave );
			XFileSaveData^ saveData = XFileSaveData::FromPointer( pXofMeshData );
			m_WrappedInterface->AddMeshChildData( const_cast<MeshContainerShim*>( static_cast<const MeshContainerShim*>( pMeshContainer ) )->GetMeshContainer(), saveObject, saveData );
			delete saveObject;
			delete saveData;
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return D3D_OK;
	}

	HRESULT ISaveUserDataShim::AddTopLevelDataObjectsPost( LPD3DXFILESAVEOBJECT pXofSave )
	{
		try
		{
			XFileSaveObject^ saveObject = XFileSaveObject::FromPointer( pXofSave );
			m_WrappedInterface->AddTopLevelDataPost( saveObject );
			delete saveObject;
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return D3D_OK;
	}

	HRESULT ISaveUserDataShim::AddTopLevelDataObjectsPre( LPD3DXFILESAVEOBJECT pXofSave )
	{
		try
		{
			XFileSaveObject^ saveObject = XFileSaveObject::FromPointer( pXofSave );
			m_WrappedInterface->AddTopLevelDataPre( saveObject );
			delete saveObject;
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return D3D_OK;
	}

	HRESULT ISaveUserDataShim::RegisterTemplates( LPD3DXFILE pXFileApi )
	{
		try
		{
			XFile^ xFile = XFile::FromPointer( pXFileApi );
			m_WrappedInterface->RegisterTemplates( xFile );
			delete xFile;
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return D3D_OK;
	}

	HRESULT ISaveUserDataShim::SaveTemplates( LPD3DXFILESAVEOBJECT pXofSave )
	{
		try
		{
			XFileSaveObject^ saveObject = XFileSaveObject::FromPointer( pXofSave );
			m_WrappedInterface->SaveTemplates( saveObject );
			delete saveObject;
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return D3D_OK;
	}
}
}