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
#include "../Utilities.h"
#include "../DataStream.h"

#include "Device.h"
#include "Texture.h"
#include "IndexBuffer.h"
#include "VertexBuffer.h"
#include "Mesh.h"
#include "SimplificationMesh.h"
#include "ProgressiveMesh.h"

#include "Direct3D9Exception.h"

using namespace System;
using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	SimplificationMesh::SimplificationMesh( Mesh^ mesh, array<AttributeWeights>^ vertexAttributeWeights, array<float>^ vertexWeights )
	{
		ID3DXSPMesh *result;

		DWORD *adjacencyIn = NULL;
		pin_ptr<int> pinnedAdj;
		array<int>^ adjacency = mesh->GetAdjacency();

		if( adjacency != nullptr )
		{
			pinnedAdj = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		pin_ptr<AttributeWeights> pinnedVAW = &vertexAttributeWeights[0];
		pin_ptr<float> pinnedVW = &vertexWeights[0];

		HRESULT hr = D3DXCreateSPMesh( mesh->InternalPointer, adjacencyIn,
			reinterpret_cast<const D3DXATTRIBUTEWEIGHTS*>( pinnedVAW ), reinterpret_cast<const FLOAT *>( pinnedVW ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	SimplificationMesh::SimplificationMesh( Mesh^ mesh, array<AttributeWeights>^ vertexAttributeWeights )
	{
		ID3DXSPMesh *result;

		DWORD *adjacencyIn = NULL;
		pin_ptr<int> pinnedAdj;
		array<int>^ adjacency = mesh->GetAdjacency();

		if( adjacency != nullptr )
		{
			pinnedAdj = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		pin_ptr<AttributeWeights> pinnedVAW = &vertexAttributeWeights[0];

		HRESULT hr = D3DXCreateSPMesh( mesh->InternalPointer, adjacencyIn,
			reinterpret_cast<const D3DXATTRIBUTEWEIGHTS*>( pinnedVAW ), NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	SimplificationMesh::SimplificationMesh( Mesh^ mesh, array<float>^ vertexWeights )
	{
		ID3DXSPMesh *result;

		DWORD *adjacencyIn = NULL;
		pin_ptr<int> pinnedAdj;
		array<int>^ adjacency = mesh->GetAdjacency();

		if( adjacency != nullptr )
		{
			pinnedAdj = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		pin_ptr<float> pinnedVW = &vertexWeights[0];

		HRESULT hr = D3DXCreateSPMesh( mesh->InternalPointer, adjacencyIn,
			NULL, reinterpret_cast<const FLOAT *>( pinnedVW ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	SimplificationMesh::SimplificationMesh( Mesh^ mesh )
	{
		ID3DXSPMesh *result;

		DWORD *adjacencyIn = NULL;
		pin_ptr<int> pinnedAdj;
		array<int>^ adjacency = mesh->GetAdjacency();

		if( adjacency != nullptr )
		{
			pinnedAdj = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdj );
		}

		HRESULT hr = D3DXCreateSPMesh( mesh->InternalPointer, adjacencyIn, NULL, NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct(result);
	}

	Mesh^ SimplificationMesh::Clone( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration, [Out] array<int>^% vertexRemap )
	{
		ID3DXMesh *result;

		pin_ptr<VertexElement> pinnedDecl = &vertexDeclaration[0];
		array<int>^ adjacencyOut = gcnew array<int>( FaceCount * 3 );
		pin_ptr<int> pinnedAdj = &adjacencyOut[0];
		vertexRemap = gcnew array<int>( VertexCount );
		pin_ptr<int> pinnedVR = &vertexRemap[0];

		HRESULT hr = InternalPointer->CloneMesh( static_cast<DWORD>( options ), reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ),
			device->InternalPointer, reinterpret_cast<DWORD*>( pinnedAdj ), reinterpret_cast<DWORD*>( pinnedVR ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			vertexRemap = nullptr;
			return nullptr;
		}

		Mesh^ mesh = Mesh::FromPointer( result );
		mesh->SetAdjacency( adjacencyOut );

		return mesh;
	}

	Mesh^ SimplificationMesh::Clone( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration )
	{
		ID3DXMesh *result;

		pin_ptr<VertexElement> pinnedDecl = &vertexDeclaration[0];
		array<int>^ adjacencyOut = gcnew array<int>( FaceCount * 3 );
		pin_ptr<int> pinnedAdj = &adjacencyOut[0];

		HRESULT hr = InternalPointer->CloneMesh( static_cast<DWORD>( options ), reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ),
			device->InternalPointer, reinterpret_cast<DWORD*>( pinnedAdj ), NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Mesh^ mesh = Mesh::FromPointer( result );
		mesh->SetAdjacency( adjacencyOut );

		return mesh;
	}

	Mesh^ SimplificationMesh::Clone( SlimDX::Direct3D9::Device^ device, MeshFlags options, SlimDX::Direct3D9::VertexFormat fvf, [Out] array<int>^% vertexRemap )
	{
		ID3DXMesh *result;

		array<int>^ adjacencyOut = gcnew array<int>( FaceCount * 3 );
		pin_ptr<int> pinnedAdj = &adjacencyOut[0];
		vertexRemap = gcnew array<int>( VertexCount );
		pin_ptr<int> pinnedVR = &vertexRemap[0];

		HRESULT hr = InternalPointer->CloneMeshFVF( static_cast<DWORD>( options ), static_cast<DWORD>( fvf ),
			device->InternalPointer, reinterpret_cast<DWORD*>( pinnedAdj ), reinterpret_cast<DWORD*>( pinnedVR ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			vertexRemap = nullptr;
			return nullptr;
		}

		Mesh^ mesh = Mesh::FromPointer( result );
		mesh->SetAdjacency( adjacencyOut );

		return mesh;
	}

	Mesh^ SimplificationMesh::Clone( SlimDX::Direct3D9::Device^ device, MeshFlags options, SlimDX::Direct3D9::VertexFormat fvf )
	{
		ID3DXMesh *result;

		array<int>^ adjacencyOut = gcnew array<int>( FaceCount * 3 );
		pin_ptr<int> pinnedAdj = &adjacencyOut[0];

		HRESULT hr = InternalPointer->CloneMeshFVF( static_cast<DWORD>( options ), static_cast<DWORD>( fvf ),
			device->InternalPointer, reinterpret_cast<DWORD*>( pinnedAdj ), NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		Mesh^ mesh = Mesh::FromPointer( result );
		mesh->SetAdjacency( adjacencyOut );

		return mesh;
	}

	ProgressiveMesh^ SimplificationMesh::CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration, [Out] array<int>^% vertexRemap, [Out] array<float>^% errorsByFace )
	{
		ID3DXPMesh *result;

		pin_ptr<VertexElement> pinnedDecl = &vertexDeclaration[0];
		vertexRemap = gcnew array<int>( VertexCount );
		errorsByFace = gcnew array<float>( FaceCount );
		pin_ptr<float> pinnedEBF = &errorsByFace[0];
		pin_ptr<int> pinnedVR = &vertexRemap[0];

		HRESULT hr = InternalPointer->ClonePMesh( static_cast<DWORD>( options ), reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ),
			device->InternalPointer, reinterpret_cast<DWORD*>( pinnedVR ), reinterpret_cast<FLOAT*>( pinnedEBF ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			errorsByFace = nullptr;
			vertexRemap = nullptr;
			return nullptr;
		}

		return ProgressiveMesh::FromPointer( result );
	}

	ProgressiveMesh^ SimplificationMesh::CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration, [Out] array<int>^% vertexRemap )
	{
		ID3DXPMesh *result;

		pin_ptr<VertexElement> pinnedDecl = &vertexDeclaration[0];
		vertexRemap = gcnew array<int>( VertexCount );
		pin_ptr<int> pinnedVR = &vertexRemap[0];

		HRESULT hr = InternalPointer->ClonePMesh( static_cast<DWORD>( options ), reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ),
			device->InternalPointer, reinterpret_cast<DWORD*>( pinnedVR ), NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			vertexRemap = nullptr;
			return nullptr;
		}

		return ProgressiveMesh::FromPointer( result );
	}

	ProgressiveMesh^ SimplificationMesh::CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration )
	{
		ID3DXPMesh *result;

		pin_ptr<VertexElement> pinnedDecl = &vertexDeclaration[0];

		HRESULT hr = InternalPointer->ClonePMesh( static_cast<DWORD>( options ), reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ),
			device->InternalPointer, NULL, NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return ProgressiveMesh::FromPointer( result );
	}

	ProgressiveMesh^ SimplificationMesh::CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, SlimDX::Direct3D9::VertexFormat fvf, [Out] array<int>^% vertexRemap, [Out] array<float>^% errorsByFace )
	{
		ID3DXPMesh *result;

		vertexRemap = gcnew array<int>( VertexCount );
		errorsByFace = gcnew array<float>( FaceCount );

		pin_ptr<float> pinnedEBF = &errorsByFace[0];
		pin_ptr<int> pinnedVR = &vertexRemap[0];

		HRESULT hr = InternalPointer->ClonePMeshFVF( static_cast<DWORD>( options ), static_cast<DWORD>( fvf ),
			device->InternalPointer, reinterpret_cast<DWORD*>( pinnedVR ), reinterpret_cast<FLOAT*>( pinnedEBF ), &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			errorsByFace = nullptr;
			vertexRemap = nullptr;
			return nullptr;
		}

		return ProgressiveMesh::FromPointer( result );
	}

	ProgressiveMesh^ SimplificationMesh::CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, SlimDX::Direct3D9::VertexFormat fvf, [Out] array<int>^% vertexRemap )
	{
		ID3DXPMesh *result;

		vertexRemap = gcnew array<int>( VertexCount );
		pin_ptr<int> pinnedVR = &vertexRemap[0];

		HRESULT hr = InternalPointer->ClonePMeshFVF( static_cast<DWORD>( options ), static_cast<DWORD>( fvf ),
			device->InternalPointer, reinterpret_cast<DWORD*>( pinnedVR ), NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			vertexRemap = nullptr;
			return nullptr;
		}

		return ProgressiveMesh::FromPointer( result );
	}

	ProgressiveMesh^ SimplificationMesh::CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, SlimDX::Direct3D9::VertexFormat fvf )
	{
		ID3DXPMesh *result;

		HRESULT hr = InternalPointer->ClonePMeshFVF( static_cast<DWORD>( options ), static_cast<DWORD>( fvf ),
			device->InternalPointer, NULL, NULL, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return ProgressiveMesh::FromPointer( result );
	}

	array<VertexElement>^ SimplificationMesh::GetDeclaration()
	{
		D3DVERTEXELEMENT9 elementBuffer[MAX_FVF_DECL_SIZE];
		HRESULT hr = InternalPointer->GetDeclaration( elementBuffer );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		int count = D3DXGetDeclLength( elementBuffer ) + 1;
		array<VertexElement>^ elements = gcnew array<VertexElement>( count );
		pin_ptr<VertexElement> pinnedElements = &elements[0];
		memcpy( pinnedElements, elementBuffer, count * sizeof(D3DVERTEXELEMENT9) );
		elements[count - 1] = VertexElement::VertexDeclarationEnd;

		return elements;
	}

	SlimDX::Direct3D9::Device^ SimplificationMesh::Device::get()
	{
		IDirect3DDevice9* device;
		HRESULT hr = InternalPointer->GetDevice( &device );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::Device::FromPointer( device );
	}

	array<AttributeWeights>^ SimplificationMesh::GetVertexAttributeWeights()
	{
		array<AttributeWeights>^ results = gcnew array<AttributeWeights>( MaximumVertexCount );
		pin_ptr<AttributeWeights> pinnedResults = &results[0];

		HRESULT hr = InternalPointer->GetVertexAttributeWeights( reinterpret_cast<D3DXATTRIBUTEWEIGHTS*>( pinnedResults ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return results;
	}

	array<float>^ SimplificationMesh::GetVertexWeights()
	{
		array<float>^ results = gcnew array<float>( MaximumVertexCount );
		pin_ptr<float> pinnedResults = &results[0];

		HRESULT hr = InternalPointer->GetVertexWeights( reinterpret_cast<FLOAT*>( pinnedResults ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return results;
	}

	Result SimplificationMesh::ReduceFaces( int faces )
	{
		HRESULT hr = InternalPointer->ReduceFaces( faces );
		return RECORD_D3D9( hr );
	}

	Result SimplificationMesh::ReduceVertices( int vertices )
	{
		HRESULT hr = InternalPointer->ReduceVertices( vertices );
		return RECORD_D3D9( hr );
	}

	int SimplificationMesh::MaximumFaceCount::get()
	{
		return InternalPointer->GetMaxFaces();
	}

	int SimplificationMesh::MaximumVertexCount::get()
	{
		return InternalPointer->GetMaxVertices();
	}

	int SimplificationMesh::FaceCount::get()
	{
		return InternalPointer->GetNumFaces();
	}

	int SimplificationMesh::VertexCount::get()
	{
		return InternalPointer->GetNumVertices();
	}

	SlimDX::Direct3D9::VertexFormat SimplificationMesh::VertexFormat::get()
	{
		return static_cast<SlimDX::Direct3D9::VertexFormat>( InternalPointer->GetFVF() );
	}

	MeshFlags SimplificationMesh::CreationOptions::get()
	{
		return static_cast<MeshFlags>( InternalPointer->GetOptions() );
	}
}
}