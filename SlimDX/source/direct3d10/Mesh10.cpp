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
#include <d3dx10.h>
#include <vcclr.h>

#include "../stack_array.h"

#include "Direct3D10Exception.h"

#include "Buffer.h"
#include "Device10.h"
#include "Mesh10.h"
#include "MeshBuffer.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Collections::ObjectModel;

namespace SlimDX
{
namespace Direct3D10
{
	Mesh::Mesh( Device^ device, array<InputElement>^ elements, System::String^ positionElement, int vertexCount, int faceCount, MeshFlags flags )
	{
		D3D10_INPUT_ELEMENT_DESC nativeElements[D3D10_IA_VERTEX_INPUT_STRUCTURE_ELEMENT_COUNT];
		for( int i = 0; i < elements->Length; ++i )
			nativeElements[i] = elements[i].CreateNativeVersion();
		
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( positionElement );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];

		ID3DX10Mesh* mesh = 0;
		if( RECORD_D3D10( D3DX10CreateMesh( device->InternalPointer, nativeElements, elements->Length, reinterpret_cast<LPCSTR>( pinnedName ), static_cast<UINT>( vertexCount ), static_cast<UINT>( faceCount ), static_cast<UINT>( flags ), &mesh ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );

		Construct( mesh );
	}

	MeshFlags Mesh::Flags::get() 
	{
		return static_cast<MeshFlags>( InternalPointer->GetFlags() );
	}

	int Mesh::FaceCount::get() 
	{
		return InternalPointer->GetFaceCount();
	}

	int Mesh::VertexCount::get() 
	{
		return InternalPointer->GetVertexCount();
	}

	int Mesh::VertexBufferCount::get() 
	{
		return InternalPointer->GetVertexBufferCount();
	}
	
	Mesh^ Mesh::Clone( array<InputElement>^ elements, System::String^ positionElement, MeshFlags flags ) 
	{
		D3D10_INPUT_ELEMENT_DESC nativeElements[D3D10_IA_VERTEX_INPUT_STRUCTURE_ELEMENT_COUNT];
		for( int i = 0; i < elements->Length; ++i )
			nativeElements[i] = elements[i].CreateNativeVersion();
		
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( positionElement );
		pin_ptr<unsigned char> pinnedName = &nameBytes[ 0 ];

		ID3DX10Mesh* mesh = 0;
		if( RECORD_D3D10( InternalPointer->CloneMesh( static_cast<UINT>( flags ), reinterpret_cast<LPCSTR>( pinnedName ), nativeElements, elements->Length, &mesh ) ).IsFailure )
			return nullptr;
		
		return gcnew Mesh( mesh, nullptr );
	}

	Result Mesh::GenerateAdjacencyAndPointRepresentation( float epsilon )
	{
		return RECORD_D3D10( InternalPointer->GenerateAdjacencyAndPointReps( epsilon ) );
	}

	Result Mesh::GenerateAttributeBufferFromTable()
	{
		return RECORD_D3D10( InternalPointer->GenerateAttributeBufferFromTable() );
	}

	Result Mesh::GenerateGeometryShaderAdjacency()
	{
		return RECORD_D3D10( InternalPointer->GenerateGSAdjacency() );
	}

	Result Mesh::Commit()
	{
		return RECORD_D3D10( InternalPointer->CommitToDevice() );
	}

	Result Mesh::Discard( MeshDiscardFlags flags )
	{
		return RECORD_D3D10( InternalPointer->Discard( static_cast<D3DX10_MESH_DISCARD_FLAGS>( flags ) ) );
	}

	Result Mesh::DrawSubset( int id )
	{
		return RECORD_D3D10( InternalPointer->DrawSubset( id ) );
	}

	Result Mesh::DrawSubsetInstanced( int id, int count, int startLocation)
	{
		return RECORD_D3D10( InternalPointer->DrawSubsetInstanced( id, count, startLocation ) );
	}
	
	MeshBuffer^ Mesh::GetIndexBuffer()
	{
		ID3DX10MeshBuffer* buffer = 0;
		if( RECORD_D3D10( InternalPointer->GetIndexBuffer( &buffer ) ).IsFailure )
			return nullptr;
		return MeshBuffer::FromPointer( buffer );
	}

	MeshBuffer^ Mesh::GetVertexBuffer( int index )
	{
		ID3DX10MeshBuffer* buffer = 0;
		if( RECORD_D3D10( InternalPointer->GetVertexBuffer( index, &buffer ) ).IsFailure )
			return nullptr;
		return MeshBuffer::FromPointer( buffer );
	}

	MeshBuffer^ Mesh::GetPointRepresentationBuffer()
	{
		ID3DX10MeshBuffer* buffer = 0;
		if( RECORD_D3D10( InternalPointer->GetPointRepBuffer( &buffer ) ).IsFailure )
			return nullptr;
		return MeshBuffer::FromPointer( buffer );
	}

	MeshBuffer^ Mesh::GetAdjacencyBuffer()
	{
		ID3DX10MeshBuffer* buffer = 0;
		if( RECORD_D3D10( InternalPointer->GetAdjacencyBuffer( &buffer ) ).IsFailure )
			return nullptr;
		return MeshBuffer::FromPointer( buffer );
	}

	MeshBuffer^ Mesh::GetAttributeBuffer()
	{
		ID3DX10MeshBuffer* buffer = 0;
		if( RECORD_D3D10( InternalPointer->GetAttributeBuffer( &buffer ) ).IsFailure )
			return nullptr;
		return MeshBuffer::FromPointer( buffer );
	}
	
	Result Mesh::SetIndexData( DataStream^ data, int count ) 
	{
		return RECORD_D3D10( InternalPointer->SetIndexData( data->PositionPointer, count ) );
	}

	Result Mesh::SetVertexData( int index, DataStream^ data ) 
	{
		return RECORD_D3D10( InternalPointer->SetVertexData( index, data->PositionPointer ) );
	}

	Result Mesh::SetPointRepresentationData( DataStream^ data ) 
	{
		return RECORD_D3D10( InternalPointer->SetPointRepData( reinterpret_cast<const UINT*>( data->PositionPointer ) ) );
	}

	Result Mesh::SetAdjacencyData( DataStream^ data ) 
	{
		return RECORD_D3D10( InternalPointer->SetAdjacencyData( reinterpret_cast<const UINT*>( data->PositionPointer ) ) );
	}

	Result Mesh::SetAttributeData( DataStream^ data ) 
	{
		return RECORD_D3D10( InternalPointer->SetAttributeData( reinterpret_cast<const UINT*>( data->PositionPointer ) ) );
	}

	Result Mesh::SetAttributeTable( array<MeshAttributeRange>^ ranges ) 
	{
		stack_array<D3DX10_ATTRIBUTE_RANGE> nativeRanges = stackalloc( D3DX10_ATTRIBUTE_RANGE, ranges->Length );

		for( int rangeIndex = 0; rangeIndex < ranges->Length; ++rangeIndex )
			nativeRanges[rangeIndex] = ranges[rangeIndex].CreateNativeVersion();

		return RECORD_D3D10( InternalPointer->SetAttributeTable( &nativeRanges[0], ranges->Length ) );
	}

	ReadOnlyCollection<MeshAttributeRange>^ Mesh::GetAttributeTable()
	{
		UINT count = 0;
		if( RECORD_D3D10( InternalPointer->GetAttributeTable( 0, &count ) ).IsFailure )
			return nullptr;

		stack_array<D3DX10_ATTRIBUTE_RANGE> ranges = stackalloc( D3DX10_ATTRIBUTE_RANGE, count );
		if( RECORD_D3D10( InternalPointer->GetAttributeTable( reinterpret_cast<D3DX10_ATTRIBUTE_RANGE*>( &ranges[0] ), &count ) ).IsFailure )
			return nullptr;
		
		List<MeshAttributeRange>^ list = gcnew List<MeshAttributeRange>( count );
		for( UINT rangeIndex = 0; rangeIndex < count; ++rangeIndex ) 
			list->Add(MeshAttributeRange( ranges[rangeIndex] ));
		return gcnew ReadOnlyCollection<MeshAttributeRange>( list );
	}

	Buffer^ Mesh::GetDeviceIndexBuffer() 
	{
		ID3D10Buffer* buffer = 0;
		if( RECORD_D3D10( InternalPointer->GetDeviceIndexBuffer( &buffer ) ).IsFailure )
			return nullptr;
		return Buffer::FromPointer( buffer );
	}

	Buffer^ Mesh::GetDeviceVertexBuffer( int index ) 
	{
		ID3D10Buffer* buffer = 0;
		if( RECORD_D3D10( InternalPointer->GetDeviceVertexBuffer( index, &buffer ) ).IsFailure )
			return nullptr;
		return Buffer::FromPointer( buffer );
	}
	
	Result Mesh::Optimize( MeshOptimizeFlags flags )
	{
		RECORD_D3D10( InternalPointer->Optimize( static_cast<UINT>( flags ), 0, 0 ) );
		return Result::Last;
	}
	
	Result Mesh::Optimize( MeshOptimizeFlags flags, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap )
	{
		stack_array<UINT> nativeFaceRemap = stackalloc( UINT, FaceCount );
		ID3D10Blob* nativeVertexRemap = 0;
		
		if( RECORD_D3D10( InternalPointer->Optimize( static_cast<UINT>( flags ), &nativeFaceRemap[0], &nativeVertexRemap ) ).IsFailure )
		{
			faceRemap = nullptr;
			vertexRemap = nullptr;
		}
		else 
		{
			faceRemap = gcnew array<int>( FaceCount );
			int size = static_cast<int>( nativeFaceRemap.size() );
			for( int faceIndex = 0; faceIndex < size; ++faceIndex )
				faceRemap[faceIndex] = static_cast<int>( nativeFaceRemap[faceIndex] );
			
			vertexRemap = gcnew array<int>( VertexCount );
			DWORD* currentVertex = reinterpret_cast<DWORD*>( nativeVertexRemap->GetBufferPointer() );
			for( int vertexIndex = 0; vertexIndex < VertexCount; ++vertexIndex )
				vertexRemap[vertexIndex] = *currentVertex++;	
			
			nativeVertexRemap->Release();
		}
		
		return Result::Last;
	}
}
}
