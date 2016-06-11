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
#include "../DataStream.h"
#include "../ComObject.h"

#include "Device.h"
#include "IndexBuffer.h"
#include "Mesh.h"
#include "SkinInfo.h"

#include "Direct3D9Exception.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	SkinInfo::SkinInfo( int vertexCount, array<VertexElement>^ vertexDeclaration, int boneCount )
	{
		ID3DXSkinInfo *result;

		pin_ptr<VertexElement> pinnedDecl = &vertexDeclaration[0];

		HRESULT hr = D3DXCreateSkinInfo( vertexCount, reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ),
			boneCount, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct( result );
	}

	SkinInfo::SkinInfo( BaseMesh^ mesh, int boneCount, array<BoneCombination^>^ boneCombinationTable )
	{
		ID3DXSkinInfo *result;

		int length = boneCombinationTable->Length;
		stack_array<D3DXBONECOMBINATION> bones = stackalloc( D3DXBONECOMBINATION, length );
		for( int i = 0; i < length; i++ )
			bones[i] = boneCombinationTable[i]->ToUnmanaged();

		HRESULT hr = D3DXCreateSkinInfoFromBlendedMesh( mesh->InternalPointer, boneCount, &bones[0], &result );

		for( int i = 0; i < length; i++ )
			delete[] bones[i].BoneId;

		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct( result );
	}

	SkinInfo::SkinInfo( int vertexCount, SlimDX::Direct3D9::VertexFormat fvf, int boneCount )
	{
		ID3DXSkinInfo *result;

		HRESULT hr = D3DXCreateSkinInfoFVF( vertexCount, static_cast<DWORD>( fvf ),
			boneCount, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		Construct( result );
	}

	SkinInfo^ SkinInfo::Clone()
	{
		ID3DXSkinInfo *result;

		HRESULT hr = InternalPointer->Clone( &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew SkinInfo( result, nullptr );
	}

	Mesh^ SkinInfo::ConvertToBlendedMesh( Mesh^ mesh, array<int>^ adjacency,
		[Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap, [Out] int% maxVertexInfluence,
		[Out] array<BoneCombination^>^% boneCombinationTable )
	{
		ID3DXMesh *result;
		ID3DXBuffer *vr;
		ID3DXBuffer *bct;
		DWORD mvi;
		DWORD bcc;
		DWORD *adjacencyIn = NULL;

		faceRemap = gcnew array<int>( mesh->FaceCount );

		array<int>^ adjacencyOut = gcnew array<int>( mesh->FaceCount * 3 );

		pin_ptr<int> pinnedAdjIn;
		pin_ptr<int> pinnedAdjOut = &adjacencyOut[0];
		pin_ptr<int> pinnedFR = &faceRemap[0];

		if( adjacency != nullptr )
		{
			pinnedAdjIn = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdjIn );
		}

		HRESULT hr = InternalPointer->ConvertToBlendedMesh( mesh->InternalPointer, 0, adjacencyIn,
			reinterpret_cast<DWORD*>( pinnedAdjOut ), reinterpret_cast<DWORD*>( pinnedFR ), &vr, &mvi, &bcc, &bct, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			boneCombinationTable = nullptr;
			maxVertexInfluence = 0;
			vertexRemap = nullptr;
			faceRemap = nullptr;
			return nullptr;
		}

		boneCombinationTable = gcnew array<BoneCombination^>( bcc );
		LPD3DXBONECOMBINATION pointer = reinterpret_cast<LPD3DXBONECOMBINATION>( bct->GetBufferPointer() );

		for( DWORD i = 0; i < bcc; i++ )
		{
			boneCombinationTable[i] = BoneCombination::FromUnmanaged( pointer[i] );
			boneCombinationTable[i]->BoneIds = gcnew array<int>( mvi );
			for( DWORD j = 0; j < mvi; j++ )
				boneCombinationTable[i]->BoneIds[j] = pointer[i].BoneId[j];			
		}

		Mesh^ out = Mesh::FromPointer( result );
		if( adjacency != nullptr )
			out->SetAdjacency( adjacencyOut );
		else
			out->SetAdjacency( NULL );

		maxVertexInfluence = mvi;
		vertexRemap = Utilities::ReadRange<int>( vr, result->GetNumVertices() );
		return out;
	}

	Mesh^ SkinInfo::ConvertToBlendedMesh( Mesh^ mesh, array<int>^ adjacency,
		[Out] int% maxVertexInfluence, [Out] array<BoneCombination^>^% boneCombinationTable )
	{
		ID3DXMesh *result;
		ID3DXBuffer *bct;
		DWORD mvi;
		DWORD bcc;
		DWORD *adjacencyIn = NULL;

		array<int>^ adjacencyOut = gcnew array<int>( mesh->FaceCount * 3 );

		pin_ptr<int> pinnedAdjIn;
		pin_ptr<int> pinnedAdjOut = &adjacencyOut[0];

		if( adjacency != nullptr )
		{
			pinnedAdjIn = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdjIn );
		}

		HRESULT hr = InternalPointer->ConvertToBlendedMesh( mesh->InternalPointer, 0, adjacencyIn,
			reinterpret_cast<DWORD*>( pinnedAdjOut ), NULL, NULL, &mvi, &bcc, &bct, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			boneCombinationTable = nullptr;
			maxVertexInfluence = 0;
			return nullptr;
		}

		boneCombinationTable = gcnew array<BoneCombination^>( bcc );
		LPD3DXBONECOMBINATION pointer = reinterpret_cast<LPD3DXBONECOMBINATION>( bct->GetBufferPointer() );

		for( DWORD i = 0; i < bcc; i++ )
		{
			boneCombinationTable[i] = BoneCombination::FromUnmanaged( pointer[i] );
			boneCombinationTable[i]->BoneIds = gcnew array<int>( mvi );
			for( DWORD j = 0; j < mvi; j++ )
				boneCombinationTable[i]->BoneIds[j] = pointer[i].BoneId[j];			
		}

		Mesh^ out = Mesh::FromPointer( result );
		if( adjacency != nullptr )
			out->SetAdjacency( adjacencyOut );
		else
			out->SetAdjacency( NULL );

		maxVertexInfluence = mvi;
		return out;
	}

	Mesh^ SkinInfo::ConvertToIndexedBlendedMesh( Mesh^ mesh, int paletteSize, array<int>^ adjacency,
		[Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap, [Out] int% maxVertexInfluence,
		[Out] array<BoneCombination^>^% boneCombinationTable )
	{
		ID3DXMesh *result;
		ID3DXBuffer *vr;
		ID3DXBuffer *bct;
		DWORD mvi;
		DWORD bcc;
		DWORD *adjacencyIn = NULL;

		faceRemap = gcnew array<int>( mesh->FaceCount );

		array<int>^ adjacencyOut = gcnew array<int>( mesh->FaceCount * 3 );

		pin_ptr<int> pinnedAdjIn;
		pin_ptr<int> pinnedAdjOut = &adjacencyOut[0];

		if( adjacency != nullptr )
		{
			pinnedAdjIn = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdjIn );
		}

		pin_ptr<int> pinnedFR = &faceRemap[0];

		HRESULT hr = InternalPointer->ConvertToIndexedBlendedMesh( mesh->InternalPointer, 0, paletteSize, adjacencyIn,
			reinterpret_cast<DWORD*>( pinnedAdjOut ), reinterpret_cast<DWORD*>( pinnedFR ), &vr, &mvi, &bcc, &bct, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			boneCombinationTable = nullptr;
			maxVertexInfluence = 0;
			vertexRemap = nullptr;
			faceRemap = nullptr;
			return nullptr;
		}

		boneCombinationTable = gcnew array<BoneCombination^>( bcc );
		LPD3DXBONECOMBINATION pointer = reinterpret_cast<LPD3DXBONECOMBINATION>( bct->GetBufferPointer() );

		for( DWORD i = 0; i < bcc; i++ )
		{
			boneCombinationTable[i] = BoneCombination::FromUnmanaged( pointer[i] );
			boneCombinationTable[i]->BoneIds = gcnew array<int>( paletteSize );
			for( int j = 0; j < paletteSize; j++ )
				boneCombinationTable[i]->BoneIds[j] = pointer[i].BoneId[j];			
		}

		Mesh^ out = Mesh::FromPointer( result );
		if( adjacency != nullptr )
			out->SetAdjacency( adjacencyOut );
		else
			out->SetAdjacency( NULL );

		maxVertexInfluence = mvi;
		vertexRemap = Utilities::ReadRange<int>( vr, result->GetNumVertices() );
		return out;
	}

	Mesh^ SkinInfo::ConvertToIndexedBlendedMesh( Mesh^ mesh, int paletteSize, array<int>^ adjacency,
		[Out] int% maxVertexInfluence, [Out] array<BoneCombination^>^% boneCombinationTable )
	{
		ID3DXMesh *result;
		ID3DXBuffer *bct;
		DWORD mvi;
		DWORD bcc;
		DWORD *adjacencyIn = NULL;

		array<int>^ adjacencyOut = gcnew array<int>( mesh->FaceCount * 3 );

		pin_ptr<int> pinnedAdjIn;
		pin_ptr<int> pinnedAdjOut = &adjacencyOut[0];

		if( adjacency != nullptr )
		{
			pinnedAdjIn = &adjacency[0];
			adjacencyIn = reinterpret_cast<DWORD*>( pinnedAdjIn );
		}

		HRESULT hr = InternalPointer->ConvertToIndexedBlendedMesh( mesh->InternalPointer, 0, paletteSize, adjacencyIn,
			reinterpret_cast<DWORD*>( pinnedAdjOut ), NULL, NULL, &mvi, &bcc, &bct, &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			boneCombinationTable = nullptr;
			maxVertexInfluence = 0;
			return nullptr;
		}

		boneCombinationTable = gcnew array<BoneCombination^>( bcc );
		LPD3DXBONECOMBINATION pointer = reinterpret_cast<LPD3DXBONECOMBINATION>( bct->GetBufferPointer() );

		for( DWORD i = 0; i < bcc; i++ )
		{
			boneCombinationTable[i] = BoneCombination::FromUnmanaged( pointer[i] );
			boneCombinationTable[i]->BoneIds = gcnew array<int>( paletteSize );
			for( int j = 0; j < paletteSize; j++ )
				boneCombinationTable[i]->BoneIds[j] = pointer[i].BoneId[j];			
		}

		Mesh^ out = Mesh::FromPointer( result );
		if( adjacency != nullptr )
			out->SetAdjacency( adjacencyOut );
		else
			out->SetAdjacency( NULL );

		maxVertexInfluence = mvi;
		return out;
	}

	int SkinInfo::FindBoneVertexInfluenceIndex( int bone, int vertex )
	{
		DWORD influence;

		HRESULT hr = InternalPointer->FindBoneVertexInfluenceIndex( bone, vertex, &influence );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return 0;

		return influence;
	}

	Result SkinInfo::GetBoneInfluence( int bone, [Out] array<int>^% vertices, [Out] array<float>^% weights )
	{
		int count = GetBoneInfluenceCount( bone );
		vertices = gcnew array<int>( count );
		weights = gcnew array<float>( count );

		pin_ptr<int> pinnedVerts = &vertices[0];
		pin_ptr<float> pinnedWeights = &weights[0];

		HRESULT hr = InternalPointer->GetBoneInfluence( bone, reinterpret_cast<DWORD*>( pinnedVerts ),
			reinterpret_cast<float*>( pinnedWeights ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
		{
			vertices = nullptr;
			weights = nullptr;
		}

		return Result::Last;
	}

	Result SkinInfo::SetBoneInfluence( int bone, array<int>^ vertices, array<float>^ weights )
	{
		pin_ptr<int> pinnedVerts = &vertices[0];
		pin_ptr<float> pinnedWeights = &weights[0];

		HRESULT hr = InternalPointer->SetBoneInfluence( bone, vertices->Length, reinterpret_cast<const DWORD*>( pinnedVerts ),
			reinterpret_cast<const float*>( pinnedWeights ) );
		
		return RECORD_D3D9( hr );
	}

	String^ SkinInfo::GetBoneName( int bone )
	{
		return gcnew String( InternalPointer->GetBoneName( bone ) );
	}

	Result SkinInfo::SetBoneName( int bone, String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = InternalPointer->SetBoneName( bone, reinterpret_cast<LPCSTR>( pinnedName ) );
		return RECORD_D3D9( hr );
	}

	Result SkinInfo::GetBoneVertexInfluence( int bone, int influence, [Out] float% weight, [Out] int% vertex )
	{
		float w;
		DWORD v;

		HRESULT hr = InternalPointer->GetBoneVertexInfluence( bone, influence, &w, &v );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			weight = 0;
			vertex = 0;
		}
		else
		{
			weight = w;
			vertex = v;
		}

		return Result::Last;
	}

	Result SkinInfo::SetBoneVertexInfluence( int bone, int influence, float weight )
	{
		HRESULT hr = InternalPointer->SetBoneVertexInfluence( bone, influence, weight );
		return RECORD_D3D9( hr );
	}

	Matrix SkinInfo::GetBoneOffsetMatrix( int bone )
	{
		return Matrix::FromD3DXMATRIX( *InternalPointer->GetBoneOffsetMatrix( bone ) );
	}

	Result SkinInfo::SetBoneOffsetMatrix( int bone, Matrix matrix )
	{
		HRESULT hr = InternalPointer->SetBoneOffsetMatrix( bone, reinterpret_cast<D3DXMATRIX*>( &matrix ) );
		return RECORD_D3D9( hr );
	}

	array<VertexElement>^ SkinInfo::GetDeclaration()
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

	Result SkinInfo::SetDeclaration( array<VertexElement>^ declaration )
	{
		pin_ptr<VertexElement> pinnedDecl = &declaration[0];

		HRESULT hr = InternalPointer->SetDeclaration( reinterpret_cast<D3DVERTEXELEMENT9*>( pinnedDecl ) );
		return RECORD_D3D9( hr );
	}

	int SkinInfo::GetMaxFaceInfluences( IndexBuffer^ indexBuffer, int faceCount )
	{
		DWORD ret;

		HRESULT hr = InternalPointer->GetMaxFaceInfluences( indexBuffer->InternalPointer, faceCount, &ret );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return 0;

		return ret;
	}

	Result SkinInfo::Remap( array<int>^ remapData )
	{
		pin_ptr<int> pinnedData = &remapData[0];

		HRESULT hr = InternalPointer->Remap( remapData->Length, reinterpret_cast<DWORD*>( pinnedData ) );
		return RECORD_D3D9( hr );
	}

	Result SkinInfo::UpdateSkinnedMesh( array<Matrix>^ boneTransforms, array<Matrix>^ boneInvTransposeTransforms, DataStream^ source, DataStream^ destination )
	{
		pin_ptr<Matrix> pinnedTransforms = &boneTransforms[0];
		pin_ptr<Matrix> pinnedInvTransforms = &boneInvTransposeTransforms[0];

		HRESULT hr = InternalPointer->UpdateSkinnedMesh( reinterpret_cast<const D3DXMATRIX*>( pinnedTransforms ),
			reinterpret_cast<const D3DXMATRIX*>( pinnedInvTransforms ), source->PositionPointer, destination->PositionPointer );
		return RECORD_D3D9( hr );
	}

	int SkinInfo::GetBoneInfluenceCount( int bone )
	{
		return InternalPointer->GetNumBoneInfluences( bone );
	}

	int SkinInfo::MaximumVertexInfluences::get()
	{
		DWORD result;

		HRESULT hr = InternalPointer->GetMaxVertexInfluences( &result );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return 0;

		return result;
	}

	int SkinInfo::BoneCount::get()
	{
		return InternalPointer->GetNumBones();
	}

	float SkinInfo::MinimumBoneInfluence::get()
	{
		return InternalPointer->GetMinBoneInfluence();
	}

	void SkinInfo::MinimumBoneInfluence::set( float value )
	{
		HRESULT hr = InternalPointer->SetMinBoneInfluence( value );
		RECORD_D3D9( hr );
	}

	SlimDX::Direct3D9::VertexFormat SkinInfo::VertexFormat::get()
	{
		return static_cast<SlimDX::Direct3D9::VertexFormat>( InternalPointer->GetFVF() );
	}

	void SkinInfo::VertexFormat::set( SlimDX::Direct3D9::VertexFormat value )
	{
		HRESULT hr = InternalPointer->SetFVF( static_cast<DWORD>( value ) );
		RECORD_D3D9( hr );
	}
}
}
