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

#include "Direct3D9Exception.h"

#include "Device.h"
#include "Texture.h"
#include "IndexBuffer.h"
#include "VertexBuffer.h"
#include "Mesh.h"
#include "BaseMesh.h"
#include "SkinInfo.h"

using namespace System;
using namespace System::IO;
using namespace System::Drawing;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	Mesh^ BaseMesh::Clone( SlimDX::Direct3D9::Device^ device, MeshFlags flags, array<VertexElement>^ elements )
	{
		ID3DXMesh* mesh;
		pin_ptr<VertexElement> pinned_elements = &elements[0];

		HRESULT hr = InternalPointer->CloneMesh( static_cast<DWORD>( flags ), reinterpret_cast<const D3DVERTEXELEMENT9*>( pinned_elements ),
			device->InternalPointer, &mesh );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return Mesh::FromPointer( mesh );
	}

	Mesh^ BaseMesh::Clone( SlimDX::Direct3D9::Device^ device, MeshFlags flags, SlimDX::Direct3D9::VertexFormat fvf )
	{
		ID3DXMesh* mesh;

		HRESULT hr = InternalPointer->CloneMeshFVF( static_cast<DWORD>( flags ), static_cast<DWORD>( fvf ), 
			device->InternalPointer, &mesh );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return Mesh::FromPointer( mesh );
	}

	Result BaseMesh::DrawSubset( int subset )
	{
		HRESULT hr = InternalPointer->DrawSubset( subset );
		return RECORD_D3D9( hr );
	}

	SlimDX::Direct3D9::Device^ BaseMesh::Device::get()
	{
		IDirect3DDevice9* device;
		HRESULT hr = InternalPointer->GetDevice( &device );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::Device::FromPointer( device );
	}

	IndexBuffer^ BaseMesh::IndexBuffer::get()
	{
		IDirect3DIndexBuffer9* ib;
		HRESULT hr = InternalPointer->GetIndexBuffer( &ib );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::IndexBuffer::FromPointer( ib, this );
	}

	VertexBuffer^ BaseMesh::VertexBuffer::get()
	{
		IDirect3DVertexBuffer9* vb;
		HRESULT hr = InternalPointer->GetVertexBuffer( &vb );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::VertexBuffer::FromPointer( vb, this );
	}

	array<VertexElement>^ BaseMesh::GetDeclaration()
	{
		D3DVERTEXELEMENT9 elementBuffer[MAX_FVF_DECL_SIZE];
		HRESULT hr = InternalPointer->GetDeclaration( elementBuffer );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		// Apparently the returned decl does not include an End element. This is bizarre and confusing,
		// not to mention completely unexpected. We patch it up here.
		int count = D3DXGetDeclLength( elementBuffer ) + 1;
		array<VertexElement>^ elements = gcnew array<VertexElement>( count );
		pin_ptr<VertexElement> pinnedElements = &elements[0];
		memcpy( pinnedElements, elementBuffer, count * sizeof(D3DVERTEXELEMENT9) );
		elements[count - 1] = VertexElement::VertexDeclarationEnd;

		return elements;
	}

	array<AttributeRange>^ BaseMesh::GetAttributeTable()
	{
		DWORD count = 0;
		HRESULT hr = InternalPointer->GetAttributeTable( NULL, &count );

		if( RECORD_D3D9( hr ).IsFailure || count == 0 )
			return nullptr;

		array<AttributeRange>^ attribTable = gcnew array<AttributeRange>( count );
		pin_ptr<AttributeRange> pinnedTable = &attribTable[0];
		hr = InternalPointer->GetAttributeTable( reinterpret_cast<D3DXATTRIBUTERANGE*>( pinnedTable ), &count );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return attribTable;
	}

	DataStream^ BaseMesh::LockIndexBuffer( LockFlags flags )
	{
		void* data;
		HRESULT hr = InternalPointer->LockIndexBuffer( static_cast<DWORD>( flags ), &data );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		// determine the size of the buffer
		int size = 6 * FaceCount;
		if( (CreationOptions & MeshFlags::Use32Bit) == MeshFlags::Use32Bit )
			size *= 2;

		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		return gcnew DataStream( data, size, true, !readOnly, false );
	}

	Result BaseMesh::UnlockIndexBuffer()
	{
		HRESULT hr = InternalPointer->UnlockIndexBuffer();
		return RECORD_D3D9( hr );
	}

	DataStream^ BaseMesh::LockVertexBuffer( LockFlags flags )
	{
		void* data;
		HRESULT hr = InternalPointer->LockVertexBuffer( static_cast<DWORD>( flags ), &data );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		// determine the size of the buffer
		int size = BytesPerVertex * VertexCount;

		bool readOnly = (flags & LockFlags::ReadOnly) == LockFlags::ReadOnly;
		return gcnew DataStream( data, size, true, !readOnly, false );
	}

	Result BaseMesh::UnlockVertexBuffer()
	{
		HRESULT hr = InternalPointer->UnlockVertexBuffer();
		return RECORD_D3D9( hr );
	}

	array<int>^ BaseMesh::GenerateAdjacency( float epsilon )
	{
		// allocate the array to write the adjacency into
		array<int>^ adjacency = gcnew array<int>( 3 * FaceCount );
		pin_ptr<int> pinnedAdj = &adjacency[0];

		HRESULT hr = InternalPointer->GenerateAdjacency( epsilon, reinterpret_cast<DWORD*>( pinnedAdj ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return adjacency;
	}

	array<int>^ BaseMesh::ConvertAdjacencyToPointReps( array<int>^ adjacency )
	{
		array<int>^ points = gcnew array<int>( VertexCount );
		pin_ptr<int> pinnedAdj = &adjacency[0];
		pin_ptr<int> pinnedPoints = &points[0];

		HRESULT hr = InternalPointer->ConvertAdjacencyToPointReps( reinterpret_cast<const DWORD*>( pinnedAdj ),
			reinterpret_cast<DWORD*>( pinnedPoints ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return points;
	}

	array<int>^ BaseMesh::ConvertPointRepsToAdjacency( array<int>^ points )
	{
		array<int>^ adjacency = gcnew array<int>( 3 * FaceCount );
		pin_ptr<int> pinnedAdj = &adjacency[0];
		pin_ptr<int> pinnedPoints = &points[0];

		HRESULT hr = InternalPointer->ConvertPointRepsToAdjacency( reinterpret_cast<const DWORD*>( pinnedPoints ),
			reinterpret_cast<DWORD*>( pinnedAdj ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return adjacency;
	}

	Result BaseMesh::UpdateSemantics( array<VertexElement>^ elements )
	{
		pin_ptr<VertexElement> pinnedElements = &elements[0];

		HRESULT hr = InternalPointer->UpdateSemantics( reinterpret_cast<D3DVERTEXELEMENT9*>( pinnedElements ) );
		return RECORD_D3D9( hr );
	}

	IndexBuffer^ BaseMesh::ConvertSubsetToSingleStrip( int attributeId, MeshFlags options, [Out] int% indexCount )
	{
		IDirect3DIndexBuffer9 *result;
		DWORD count;

		HRESULT hr = D3DXConvertMeshSubsetToSingleStrip( InternalPointer, attributeId, static_cast<DWORD>( options ),
			&result, &count );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			indexCount = 0;
			return nullptr;
		}

		indexCount = count;
		return SlimDX::Direct3D9::IndexBuffer::FromPointer( result );
	}

	IndexBuffer^ BaseMesh::ConvertSubsetToStrips( int attributeId, MeshFlags options, [Out] int% indexCount,
		[Out] array<int>^% stripLengths )
	{
		IDirect3DIndexBuffer9 *result;
		ID3DXBuffer *buffer;
		DWORD numIndices;
		DWORD numStrips;

		HRESULT hr = D3DXConvertMeshSubsetToStrips( InternalPointer, attributeId, static_cast<DWORD>( options ),
			&result, &numIndices, &buffer, &numStrips );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			indexCount = 0;
			stripLengths = nullptr;
			return nullptr;
		}

		indexCount = numIndices;
		stripLengths = Utilities::ReadRange<int>( buffer, numStrips );
		return SlimDX::Direct3D9::IndexBuffer::FromPointer( result );
	}

	bool BaseMesh::Intersects( Ray ray, [Out] float% distance, [Out] int% faceIndex, [Out] array<IntersectInformation>^% hits )
	{
		ID3DXBuffer *allHits;
		BOOL result;
		FLOAT dist;
		DWORD count;
		DWORD face;

		HRESULT hr = D3DXIntersect( InternalPointer, reinterpret_cast<const D3DXVECTOR3*>( &ray.Position ),
			reinterpret_cast<const D3DXVECTOR3*>( &ray.Direction ), &result, &face, NULL, NULL, &dist, &allHits, &count );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			hits = nullptr;
			faceIndex = 0;
			distance = 0;
			return false;
		}

		faceIndex = face;
		distance = dist;

		if( allHits == NULL )
			hits = nullptr;
		else
			hits = Utilities::ReadRange<IntersectInformation>( allHits, count );

		if( result )
			return true;
		else
			return false;
	}

	bool BaseMesh::Intersects( Ray ray, [Out] float% distance )
	{
		BOOL result;
		FLOAT dist;

		HRESULT hr = D3DXIntersect( InternalPointer, reinterpret_cast<const D3DXVECTOR3*>( &ray.Position ),
			reinterpret_cast<const D3DXVECTOR3*>( &ray.Direction ), &result, NULL, NULL, NULL, &dist, NULL, NULL );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			distance = 0;
			return false;
		}

		distance = dist;
		if( result )
			return true;
		else
			return false;
	}

	bool BaseMesh::Intersects( Ray ray )
	{
		BOOL result;

		HRESULT hr = D3DXIntersect( InternalPointer, reinterpret_cast<const D3DXVECTOR3*>( &ray.Position ),
			reinterpret_cast<const D3DXVECTOR3*>( &ray.Direction ), &result, NULL, NULL, NULL, NULL, NULL, NULL );

		if( RECORD_D3D9( hr ).IsFailure || !result )
			return false;

		return true;
	}

	bool BaseMesh::IntersectsSubset( Ray ray, int attributeId, [Out] float% distance, [Out] int% faceIndex, [Out] array<IntersectInformation>^% hits )
	{
		ID3DXBuffer *allHits;
		BOOL result;
		FLOAT dist;
		DWORD count;
		DWORD face;

		HRESULT hr = D3DXIntersectSubset( InternalPointer, attributeId, reinterpret_cast<const D3DXVECTOR3*>( &ray.Position ),
			reinterpret_cast<const D3DXVECTOR3*>( &ray.Direction ), &result, &face, NULL, NULL, &dist, &allHits, &count );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			hits = nullptr;
			faceIndex = 0;
			distance = 0;
			return false;
		}

		faceIndex = face;
		distance = dist;
		hits = Utilities::ReadRange<IntersectInformation>( allHits, count );

		if( result )
			return true;
		else
			return false;
	}

	bool BaseMesh::IntersectsSubset( Ray ray, int attributeId, [Out] float% distance )
	{
		BOOL result;
		FLOAT dist;

		HRESULT hr = D3DXIntersectSubset( InternalPointer, attributeId, reinterpret_cast<const D3DXVECTOR3*>( &ray.Position ),
			reinterpret_cast<const D3DXVECTOR3*>( &ray.Direction ), &result, NULL, NULL, NULL, &dist, NULL, NULL );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			distance = 0;
			return false;
		}

		distance = dist;
		if( result )
			return true;
		else
			return false;
	}

	bool BaseMesh::IntersectsSubset( Ray ray, int attributeId )
	{
		BOOL result;

		HRESULT hr = D3DXIntersectSubset( InternalPointer, attributeId, reinterpret_cast<const D3DXVECTOR3*>( &ray.Position ),
			reinterpret_cast<const D3DXVECTOR3*>( &ray.Direction ), &result, NULL, NULL, NULL, NULL, NULL, NULL );

		if( RECORD_D3D9( hr ).IsFailure || !result )
			return false;

		return true;
	}

	int BaseMesh::FaceCount::get()
	{
		return InternalPointer->GetNumFaces();
	}

	int BaseMesh::VertexCount::get()
	{
		return InternalPointer->GetNumVertices();
	}

	SlimDX::Direct3D9::VertexFormat BaseMesh::VertexFormat::get()
	{
		return static_cast<SlimDX::Direct3D9::VertexFormat>( InternalPointer->GetFVF() );
	}

	int BaseMesh::BytesPerVertex::get()
	{
		return InternalPointer->GetNumBytesPerVertex();
	}

	MeshFlags BaseMesh::CreationOptions::get()
	{
		return static_cast<MeshFlags>( InternalPointer->GetOptions() );
	}
}
}