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

#include "../stack_array.h"
#include "../ComObject.h"
#include "../Utilities.h"
#include "../DataStream.h"

#include "Device.h"
#include "Texture.h"
#include "IndexBuffer.h"
#include "VertexBuffer.h"
#include "Mesh.h"
#include "ProgressiveMesh.h"

#include "Direct3D9Exception.h"

using namespace System;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	StreamShim::StreamShim( Stream^ stream )
	{
		m_WrappedStream = stream;
		position = -1;
	}

	void StreamShim::SetSizeToPosition()
	{
		if( position != -1 )
		{
			if( position > m_WrappedStream->Length )
				m_WrappedStream->SetLength( position );
			m_WrappedStream->Position = position;
			position = -1;
		}
	}

	void StreamShim::Clone( [Out] System::Runtime::InteropServices::ComTypes::IStream^% ppstm )
	{
		ppstm = nullptr;
		throw gcnew ExternalException( nullptr, STG_E_INVALIDFUNCTION );
	}

	void StreamShim::Commit( int grfCommitFlags )
	{
		SLIMDX_UNREFERENCED_PARAMETER(grfCommitFlags);
		m_WrappedStream->Flush();
	}

	void StreamShim::CopyTo( System::Runtime::InteropServices::ComTypes::IStream^ pstm, long long cb, IntPtr pcbRead, IntPtr pcbWritten )
	{
		array<unsigned char>^ buffer = gcnew array<unsigned char>( 4096 );
		long long written = 0;
		int read;

		if( cb != 0 )
		{
			SetSizeToPosition();

			do
			{
				int count = 4096;
				if( written + 4096 > cb )
					count = static_cast<int>( cb - written );

				read = m_WrappedStream->Read( buffer, 0, count );
				if( read == 0 )
					break;

				pstm->Write( buffer, read, IntPtr::Zero );
				written += read;

			} while( written < cb );
		}

		if( pcbRead != IntPtr::Zero )
			Marshal::WriteInt64( pcbRead, written );
		if( pcbWritten != IntPtr::Zero )
			Marshal::WriteInt64( pcbWritten, written );
	}

	void StreamShim::LockRegion( long long libOffset, long long cb, int dwLockType )
	{
		SLIMDX_UNREFERENCED_PARAMETER(libOffset);
		SLIMDX_UNREFERENCED_PARAMETER(cb);
		SLIMDX_UNREFERENCED_PARAMETER(dwLockType);

		throw gcnew ExternalException( nullptr, STG_E_INVALIDFUNCTION );
	}

	void StreamShim::Read( [Out] array<unsigned char>^ pv, int cb, IntPtr pcbRead )
	{
		int read = 0;

		if( cb != 0 )
		{
			SetSizeToPosition();
			read = m_WrappedStream->Read( pv, 0, cb );
		}

		if( pcbRead != IntPtr::Zero )
			Marshal::WriteInt32( pcbRead, read );
	}

	void StreamShim::Revert()
	{
		throw gcnew ExternalException( nullptr, STG_E_INVALIDFUNCTION );
	}

	void StreamShim::Seek( long long dlibMove, int dwOrigin, IntPtr plibNewPosition )
	{
		long long newPosition;

		if( m_WrappedStream->CanWrite )
		{
			SeekOrigin origin = static_cast<SeekOrigin>( dwOrigin );
			if( origin == SeekOrigin::Begin )
				newPosition = dlibMove;
			else if( origin == SeekOrigin::Current )
			{
				newPosition = position;
				if( newPosition == -1 )
					newPosition = m_WrappedStream->Position;
				newPosition += dlibMove;
			}
			else if( origin == SeekOrigin::End )
				newPosition = m_WrappedStream->Length + dlibMove;
			else
				throw gcnew ExternalException( nullptr, STG_E_INVALIDFUNCTION );

			if( newPosition > m_WrappedStream->Length )
				position = newPosition;
			else
			{
				m_WrappedStream->Position = newPosition;
				position = -1;
			}
		}
		else
		{
			try
			{
				newPosition = m_WrappedStream->Seek( dlibMove, static_cast<SeekOrigin>( dwOrigin ) );
			}
			catch( ArgumentException^ )
			{
				throw gcnew ExternalException( nullptr, STG_E_INVALIDFUNCTION );
			}

			position = -1;
		}

		if( plibNewPosition != IntPtr::Zero )
			Marshal::WriteInt64( plibNewPosition, newPosition );
	}

	void StreamShim::SetSize( long long libNewSize )
	{
		m_WrappedStream->SetLength( libNewSize );
	}

	void StreamShim::Stat( [Out] System::Runtime::InteropServices::ComTypes::STATSTG% pstatstg, int grfStatFlag )
	{
		SLIMDX_UNREFERENCED_PARAMETER(grfStatFlag);

		pstatstg = System::Runtime::InteropServices::ComTypes::STATSTG();
		pstatstg.cbSize = m_WrappedStream->Length;
	}

	void StreamShim::UnlockRegion( long long libOffset, long long cb, int dwLockType )
	{
		SLIMDX_UNREFERENCED_PARAMETER(libOffset);
		SLIMDX_UNREFERENCED_PARAMETER(cb);
		SLIMDX_UNREFERENCED_PARAMETER(dwLockType);

		throw gcnew ExternalException( nullptr, STG_E_INVALIDFUNCTION );
	}

	void StreamShim::Write( array<unsigned char>^ pv, int cb, IntPtr pcbWritten )
	{
		if( cb != 0 )
		{
			SetSizeToPosition();
			m_WrappedStream->Write( pv, 0, cb );
		}

		if( pcbWritten != IntPtr::Zero )
			Marshal::WriteInt32( pcbWritten, cb );
	}
	
	ProgressiveMesh::ProgressiveMesh( Mesh^ mesh, array<AttributeWeights>^ attributeWeights,
		array<float>^ vertexWeights, int minimumValue, MeshSimplification options )
	{
		ID3DXPMesh* result = NULL;

		DWORD *adjacencyIn = NULL;
		pin_ptr<int> pinnedAdj;
		array<int>^ adjacency = mesh->GetAdjacency();

		if( adjacency != nullptr )
		{
			pinnedAdj = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		pin_ptr<float> pinnedVW = &vertexWeights[0];
		pin_ptr<AttributeWeights> pinnedAW = &attributeWeights[0];

		HRESULT hr = D3DXGeneratePMesh( mesh->InternalPointer, adjacencyIn,
			reinterpret_cast<const D3DXATTRIBUTEWEIGHTS*>( pinnedAW ), reinterpret_cast<const FLOAT*>( pinnedVW ),
			minimumValue, static_cast<DWORD>( options ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	ProgressiveMesh::ProgressiveMesh( Mesh^ mesh, array<AttributeWeights>^ attributeWeights,
		int minimumValue, MeshSimplification options )
	{
		ID3DXPMesh *result = NULL;

		DWORD *adjacencyIn = NULL;
		pin_ptr<int> pinnedAdj;
		array<int>^ adjacency = mesh->GetAdjacency();

		if( adjacency != nullptr )
		{
			pinnedAdj = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		pin_ptr<AttributeWeights> pinnedAW = &attributeWeights[0];

		HRESULT hr = D3DXGeneratePMesh( mesh->InternalPointer, adjacencyIn,
			reinterpret_cast<const D3DXATTRIBUTEWEIGHTS*>( pinnedAW ), NULL,
			minimumValue, static_cast<DWORD>( options ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	ProgressiveMesh::ProgressiveMesh( Mesh^ mesh, int minimumValue, MeshSimplification options )
	{
		ID3DXPMesh *result = NULL;

		DWORD *adjacencyIn = NULL;
		pin_ptr<int> pinnedAdj;
		array<int>^ adjacency = mesh->GetAdjacency();

		if( adjacency != nullptr )
		{
			pinnedAdj = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		HRESULT hr = D3DXGeneratePMesh( mesh->InternalPointer, adjacencyIn,
			NULL, NULL, minimumValue, static_cast<DWORD>( options ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	ProgressiveMesh^ ProgressiveMesh::FromStream( SlimDX::Direct3D9::Device^ device, Stream^ stream, MeshFlags flags )
	{
		ID3DXPMesh *result = NULL;
		ID3DXBuffer *om = NULL;
		ID3DXBuffer *oe = NULL;
		DWORD numMaterials;

		StreamShim^ shim = gcnew StreamShim( stream );
		pin_ptr<StreamShim^> nativeStream = &shim;

		HRESULT hr = D3DXCreatePMeshFromStream( reinterpret_cast<IStream*>( nativeStream ), static_cast<DWORD>( flags ),
			device->InternalPointer, &om, &oe, &numMaterials, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;
		
		ProgressiveMesh^ mesh = gcnew ProgressiveMesh( result, nullptr );

		mesh->SetMaterials( ExtendedMaterial::FromBuffer( om, numMaterials ) );
		mesh->SetEffects( EffectInstance::FromBuffer( oe, numMaterials ) );

		return mesh;
	}

	ProgressiveMesh^ ProgressiveMesh::CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags flags, array<VertexElement>^ vertexDeclaration )
	{
		ID3DXPMesh* mesh = NULL;
		pin_ptr<VertexElement> pinned_elements = &vertexDeclaration[0];

		HRESULT hr = InternalPointer->ClonePMesh( static_cast<DWORD>( flags ), reinterpret_cast<const D3DVERTEXELEMENT9*>( pinned_elements ),
			device->InternalPointer, &mesh );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew ProgressiveMesh( mesh, nullptr );
	}

	ProgressiveMesh^ ProgressiveMesh::CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags flags, SlimDX::Direct3D9::VertexFormat format )
	{
		ID3DXPMesh* mesh = NULL;

		HRESULT hr = InternalPointer->ClonePMeshFVF( static_cast<DWORD>( flags ), static_cast<DWORD>( format ), 
			device->InternalPointer, &mesh );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew ProgressiveMesh( mesh, nullptr );
	}

	Result ProgressiveMesh::GenerateVertexHistory( array<int>^ vertexHistory )
	{
		pin_ptr<int> pinned = &vertexHistory[0];

		HRESULT hr = InternalPointer->GenerateVertexHistory( reinterpret_cast<DWORD*>( pinned ) );
		return RECORD_D3D9( hr );
	}

	array<int>^ ProgressiveMesh::GetAdjacency()
	{
		array<int>^ adjacency = gcnew array<int>( 3 * MaximumFaceCount );
		pin_ptr<int> pinnedAdj = &adjacency[0];

		HRESULT hr = InternalPointer->GetAdjacency( reinterpret_cast<DWORD*>( pinnedAdj ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return adjacency;
	}

	Mesh^ ProgressiveMesh::Optimize( MeshOptimizeFlags flags )
	{
		ID3DXMesh *result = NULL;

		array<int>^ adjacencyOut = gcnew array<int>( FaceCount * 3 );
		pin_ptr<int> pinnedAdj = &adjacencyOut[0];

		HRESULT hr = InternalPointer->Optimize( static_cast<DWORD>( flags ), reinterpret_cast<DWORD*>( pinnedAdj ), NULL, NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Mesh^ mesh = Mesh::FromPointer( result );
		mesh->SetAdjacency( adjacencyOut );

		return mesh;
	}

	Mesh^ ProgressiveMesh::Optimize( MeshOptimizeFlags flags, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap )
	{
		ID3DXMesh *result = NULL;
		ID3DXBuffer *buffer = NULL;
		faceRemap = gcnew array<int>( FaceCount );
		pin_ptr<int> pinnedFR = &faceRemap[0];

		array<int>^ adjacencyOut = gcnew array<int>( FaceCount * 3 );
		pin_ptr<int> pinnedAdj = &adjacencyOut[0];

		HRESULT hr = InternalPointer->Optimize( static_cast<DWORD>( flags ), reinterpret_cast<DWORD*>( pinnedAdj ), 
			reinterpret_cast<DWORD*>( pinnedFR ), &buffer, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			vertexRemap = nullptr;
			return nullptr;
		}

		vertexRemap = Utilities::ReadRange<int>( buffer, result->GetNumVertices() );
		
		Mesh^ mesh = Mesh::FromPointer( result );
		mesh->SetAdjacency( adjacencyOut );

		return mesh;
	}

	Result ProgressiveMesh::OptimizeBaseLevelOfDetail( MeshOptimizeFlags flags )
	{
		HRESULT hr = InternalPointer->OptimizeBaseLOD( static_cast<DWORD>( flags ), NULL );
		return RECORD_D3D9( hr );
	}

	Result ProgressiveMesh::OptimizeBaseLevelOfDetail( MeshOptimizeFlags flags, [Out] array<int>^% faceRemap )
	{
		faceRemap = gcnew array<int>( FaceCount );
		pin_ptr<int> pinnedFR = &faceRemap[0];
		HRESULT hr = InternalPointer->OptimizeBaseLOD( static_cast<DWORD>( flags ), reinterpret_cast<DWORD*>( pinnedFR ) );
		return RECORD_D3D9( hr );
	}

	Result ProgressiveMesh::ToStream( ProgressiveMesh^ mesh, Stream^ stream )
	{
		StreamShim^ shim = gcnew StreamShim( stream );
		pin_ptr<StreamShim^> nativeStream = &shim;

		array<ExtendedMaterial>^ materials = mesh->GetMaterials();
		array<EffectInstance>^ effects = mesh->GetEffects();

		stack_array<D3DXMATERIAL> nativeMaterials;
		stack_array<D3DXEFFECTINSTANCE> nativeEffects;

		if( materials != nullptr )
		{
			nativeMaterials = stack_array<D3DXMATERIAL>( materials->Length );
			for( int i = 0; i < materials->Length; i++ )
				nativeMaterials[i] = ExtendedMaterial::ToUnmanaged( materials[i] );
		}

		if( effects != nullptr )
		{
			nativeEffects = stack_array<D3DXEFFECTINSTANCE>( effects->Length );
			for( int i = 0; i < effects->Length; i++ )
				nativeEffects[i] = EffectInstance::ToUnmanaged( effects[i] );
		}

		HRESULT hr = mesh->InternalPointer->Save( reinterpret_cast<IStream*>( nativeStream ), &nativeMaterials[0], &nativeEffects[0], materials->Length );
		
		for( int i = 0; i < materials->Length; i++ )
			Utilities::FreeNativeString( nativeMaterials[i].pTextureFilename );

		for( int i = 0; i < effects->Length; i++ )
		{
			for( UINT j = 0; j < nativeEffects[i].NumDefaults; j++ )
				Utilities::FreeNativeString( nativeEffects[i].pDefaults[j].pParamName );

			delete[] nativeEffects[i].pDefaults;
		}

		return RECORD_D3D9( hr );
	}

	Result ProgressiveMesh::SetFaceCount( int faceCount )
	{
		HRESULT hr = InternalPointer->SetNumFaces( faceCount );
		return RECORD_D3D9( hr );
	}

	Result ProgressiveMesh::SetVertexCount( int vertexCount )
	{
		HRESULT hr = InternalPointer->SetNumVertices( vertexCount );
		return RECORD_D3D9( hr );
	}

	Result ProgressiveMesh::TrimFaces( int newFaceMinimum, int newFaceMaximum )
	{
		HRESULT hr = InternalPointer->TrimByFaces( newFaceMinimum, newFaceMaximum, NULL, NULL );
		return RECORD_D3D9( hr );
	}

	Result ProgressiveMesh::TrimFaces( int newFaceMinimum, int newFaceMaximum, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap )
	{
		faceRemap = gcnew array<int>( FaceCount );
		vertexRemap = gcnew array<int>( VertexCount );
		pin_ptr<int> pinnedFR = &faceRemap[0];
		pin_ptr<int> pinnedVR = &vertexRemap[0];

		HRESULT hr = InternalPointer->TrimByFaces( newFaceMinimum, newFaceMaximum, reinterpret_cast<DWORD*>( pinnedFR ), 
			reinterpret_cast<DWORD*>( pinnedVR ) );
		return RECORD_D3D9( hr );
	}

	Result ProgressiveMesh::TrimVertices( int newVertexMinimum, int newVertexMaximum )
	{
		HRESULT hr = InternalPointer->TrimByVertices( newVertexMinimum, newVertexMaximum, NULL, NULL );
		return RECORD_D3D9( hr );
	}

	Result ProgressiveMesh::TrimVertices( int newVertexMinimum, int newVertexMaximum, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap )
	{
		faceRemap = gcnew array<int>( FaceCount );
		vertexRemap = gcnew array<int>( VertexCount );
		pin_ptr<int> pinnedFR = &faceRemap[0];
		pin_ptr<int> pinnedVR = &vertexRemap[0];

		HRESULT hr = InternalPointer->TrimByVertices( newVertexMinimum, newVertexMaximum, reinterpret_cast<DWORD*>( pinnedFR ), 
			reinterpret_cast<DWORD*>( pinnedVR ) );
		return RECORD_D3D9( hr );
	}

	int ProgressiveMesh::MaximumFaceCount::get()
	{
		return InternalPointer->GetMaxFaces();
	}

	int ProgressiveMesh::MaximumVertexCount::get()
	{
		return InternalPointer->GetMaxVertices();
	}

	int ProgressiveMesh::MinimumFaceCount::get()
	{
		return InternalPointer->GetMinFaces();
	}

	int ProgressiveMesh::MinimumVertexCount::get()
	{
		return InternalPointer->GetMinVertices();
	}
}
}