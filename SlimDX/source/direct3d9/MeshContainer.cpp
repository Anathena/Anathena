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

#include "Device.h"
#include "Mesh.h"
#include "MeshData.h"
#include "MeshContainer.h"
#include "PatchMesh.h"
#include "ProgressiveMesh.h"
#include "SkinInfo.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	MeshContainerShim::MeshContainerShim( SlimDX::Direct3D9::MeshContainer ^container ) : D3DXMESHCONTAINER( )
	{
		m_Container = container;
	}

	MeshContainer::MeshContainer()
	{
		// Manual Allocation: ugly, but necessary
		// we clean up in both the destructor and the finalizer
		Pointer = new MeshContainerShim(this);
		Pointer->Name = NULL;
		Pointer->MeshData = D3DXMESHDATA();
		Pointer->pMaterials = NULL;
		Pointer->pEffects = NULL;
		Pointer->NumMaterials = 0;
		Pointer->pAdjacency = NULL;
		Pointer->pSkinInfo = NULL;
		Pointer->pNextMeshContainer = NULL;

		m_Name = String::Empty;
		m_NextContainer = nullptr;
		m_SkinInfo = nullptr;
	}

	MeshContainer::~MeshContainer()
	{
		Destruct();
	}

	MeshContainer::!MeshContainer()
	{
		Destruct();
	}

	void MeshContainer::Destruct()
	{
		if( Pointer != NULL )
		{
			if( Pointer->pMaterials != NULL )
			{
				for( UINT i = 0; i < Pointer->NumMaterials; i++ )
					Utilities::FreeNativeString( Pointer->pMaterials[i].pTextureFilename );

				delete[] Pointer->pMaterials;
				Pointer->pMaterials = NULL;
			}

			delete[] Pointer->pAdjacency;
			Pointer->pAdjacency = NULL;

			Utilities::FreeNativeString( Pointer->Name );

			if( Pointer->pEffects != NULL )
			{
				for( unsigned int i = 0; i < Pointer->NumMaterials; i++ )
				{
					for( UINT j = 0; j < Pointer->pEffects[i].NumDefaults; j++ )
						Utilities::FreeNativeString( Pointer->pEffects[i].pDefaults[j].pParamName );

					delete[] Pointer->pEffects[i].pDefaults;
				}

				delete[] Pointer->pEffects;
				Pointer->pEffects = NULL;
			}

			if(m_MeshData != nullptr )
			{
				delete m_MeshData->Mesh;
				delete m_MeshData->PatchMesh;
				delete m_MeshData->ProgressiveMesh;
				m_MeshData = nullptr;
			}

			delete m_SkinInfo;

			delete Pointer;
			Pointer = NULL;
		}
	}

	array<ExtendedMaterial>^ MeshContainer::GetMaterials()
	{
		array<ExtendedMaterial>^ results = gcnew array<ExtendedMaterial>( Pointer->NumMaterials );

		for( unsigned int i = 0; i < Pointer->NumMaterials; i++ )
			results[i] = ExtendedMaterial::FromUnmanaged( Pointer->pMaterials[i] );

		return results;
	}

	void MeshContainer::SetMaterials( array<ExtendedMaterial>^ materials )
	{
		if( Pointer->pMaterials != NULL )
		{
			for( UINT i = 0; i < Pointer->NumMaterials; i++ )
				Utilities::FreeNativeString( Pointer->pMaterials[i].pTextureFilename );
			delete[] Pointer->pMaterials;
		}

		Pointer->NumMaterials = materials->Length;
		// Manual Allocation: ugly, but necessary
		// we clean up whenever a new set of materials is given to us, and in
		// both the destructor and finalizer
		Pointer->pMaterials = new D3DXMATERIAL[Pointer->NumMaterials];

		for( int i = 0; i < materials->Length; i++ )
			Pointer->pMaterials[i] = ExtendedMaterial::ToUnmanaged( materials[i] );
	}

	array<EffectInstance>^ MeshContainer::GetEffects()
	{
		array<EffectInstance>^ results = gcnew array<EffectInstance>( Pointer->NumMaterials );

		for( unsigned int i = 0; i < Pointer->NumMaterials; i++ )
			results[i] = EffectInstance::FromUnmanaged( Pointer->pEffects[i] );

		return results;
	}

	void MeshContainer::SetEffects( array<EffectInstance>^ effects )
	{
		if( Pointer->pEffects != NULL )
		{
			for( UINT i = 0; i < Pointer->NumMaterials; i++ )
			{
				for( UINT j = 0; j < Pointer->pEffects[i].NumDefaults; j++ )
					Utilities::FreeNativeString( Pointer->pEffects[i].pDefaults[j].pParamName );

				delete[] Pointer->pEffects[i].pDefaults;
			}

			delete[] Pointer->pEffects;
		}

		Pointer->NumMaterials = effects->Length;
		// Manual Allocation: ugly, but necessary
		// we clean up whenever a new set of effects is given, and
		// in both the destructor and finalizer
		Pointer->pEffects = new D3DXEFFECTINSTANCE[effects->Length];

		for( int i = 0; i < effects->Length; i++ )
			Pointer->pEffects[i] = EffectInstance::ToUnmanaged( effects[i] );
	}

	array<int>^ MeshContainer::GetAdjacency()
	{
		int count = 0;

		if( MeshData->Type == MeshDataType::Mesh )
			count = MeshData->Mesh->FaceCount;
		else if( MeshData->Type == MeshDataType::ProgressiveMesh )
			count = MeshData->ProgressiveMesh->FaceCount;
		else if( MeshData->Type == MeshDataType::PatchMesh )
			count = MeshData->PatchMesh->PatchCount;
		count *= 3;

		array<int>^ results = gcnew array<int>( count );

		for( int i = 0; i < count; i++ )
			results[i] = Pointer->pAdjacency[i];

		return results;
	}

	void MeshContainer::SetAdjacency( array<int>^ adjacency )
	{
		if( Pointer->pAdjacency != NULL )
			delete[] Pointer->pAdjacency;

		int count = adjacency->Length;
		// Manual Allocation: ugly, but necessary
		// we clean up whenever a new set is given to us, and
		// in both the destructor and finalizer
		Pointer->pAdjacency = new DWORD[count];

		for( int i = 0; i < count; i++ )
			Pointer->pAdjacency[i] = adjacency[i];
	}

	String^ MeshContainer::Name::get()
	{
		return m_Name;
	}

	void MeshContainer::Name::set( String^ value )
	{
		m_Name = value;

		Utilities::FreeNativeString( Pointer->Name );
		Pointer->Name = Utilities::AllocateNativeString( value );
	}

	MeshData^ MeshContainer::MeshData::get()
	{
		return m_MeshData;
	}

	void MeshContainer::MeshData::set( SlimDX::Direct3D9::MeshData^ value )
	{
		m_MeshData = value;

		Pointer->MeshData.Type = static_cast<D3DXMESHDATATYPE>( value->Type );
		
		if( Pointer->MeshData.Type == D3DXMESHTYPE_MESH )
			Pointer->MeshData.pMesh = value->Mesh->InternalPointer;
		else if( Pointer->MeshData.Type == D3DXMESHTYPE_PMESH )
			Pointer->MeshData.pPMesh = value->ProgressiveMesh->InternalPointer;
		else if( Pointer->MeshData.Type == D3DXMESHTYPE_PATCHMESH )
			Pointer->MeshData.pPatchMesh = value->PatchMesh->InternalPointer;
	}

	SlimDX::Direct3D9::SkinInfo^ MeshContainer::SkinInfo::get()
	{
		return m_SkinInfo;
	}

	void MeshContainer::SkinInfo::set( SlimDX::Direct3D9::SkinInfo^ value )
	{
		m_SkinInfo = value;

		if( value == nullptr )
			Pointer->pSkinInfo = NULL;
		else
		{
			Pointer->pSkinInfo = value->InternalPointer;
		}
	}

	MeshContainer^ MeshContainer::NextMeshContainer::get()
	{
		return m_NextContainer;
	}

	void MeshContainer::NextMeshContainer::set( MeshContainer^ value )
	{
		m_NextContainer = value;

		if( value == nullptr )
			Pointer->pNextMeshContainer = NULL;
		else
			Pointer->pNextMeshContainer = value->Pointer;
	}
}
}
