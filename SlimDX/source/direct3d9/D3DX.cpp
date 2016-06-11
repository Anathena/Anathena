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

#include "../DataStream.h"

#include "Direct3D9Exception.h"

#include "VertexDeclaration.h"
#include "Enums.h"
#include "D3DX.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	int D3DX::GetDeclarationVertexSize( array<VertexElement>^ elements, int stream )
	{
		if( elements == nullptr )
			throw gcnew ArgumentNullException( "elements" );

		pin_ptr<VertexElement> pinnedElements = &elements[0];
		return static_cast<int>( D3DXGetDeclVertexSize( reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedElements ), stream ) );
	}

	int D3DX::GetFVFVertexSize( VertexFormat fvf )
	{
		return static_cast<int>( D3DXGetFVFVertexSize( static_cast<DWORD>( fvf ) ) );
	}

	array<VertexElement>^ D3DX::DeclaratorFromFVF( VertexFormat fvf )
	{
		D3DVERTEXELEMENT9 elementBuffer[MAX_FVF_DECL_SIZE];

		HRESULT hr = D3DXDeclaratorFromFVF( static_cast<DWORD>( fvf ), elementBuffer );
		
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

	VertexFormat D3DX::FVFFromDeclarator( array<VertexElement>^ declarator )
	{
		DWORD result;
		pin_ptr<VertexElement> pinnedDecl = &declarator[0];

		HRESULT hr = D3DXFVFFromDeclarator( reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ), &result );
		RECORD_D3D9( hr );

		return static_cast<VertexFormat>( result );
	}

	array<VertexElement>^ D3DX::GenerateOutputDeclaration( array<VertexElement>^ declaration )
	{
		D3DVERTEXELEMENT9 elementBuffer[MAX_FVF_DECL_SIZE];
		pin_ptr<VertexElement> pinnedDecl = &declaration[0];

		HRESULT hr = D3DXGenerateOutputDecl( elementBuffer, reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ) );
		
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

	int D3DX::GetDeclarationLength( array<VertexElement>^ declaration )
	{
		pin_ptr<VertexElement> pinnedDecl = &declaration[0];

		return D3DXGetDeclLength( reinterpret_cast<const D3DVERTEXELEMENT9*>( pinnedDecl ) );
	}

	Result D3DX::GetRectanglePatchSize( float segmentCount, [Out] int% triangleCount, [Out] int% vertexCount )
	{
		DWORD tris;
		DWORD verts;

		HRESULT hr = D3DXRectPatchSize( &segmentCount, &tris, &verts );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			triangleCount = 0;
			vertexCount = 0;
		}
		else
		{
			triangleCount = tris;
			vertexCount = verts;
		}

		return Result::Last;
	}

	Result D3DX::GetTrianglePatchSize( float segmentCount, [Out] int% triangleCount, [Out] int% vertexCount )
	{
		DWORD tris;
		DWORD verts;

		HRESULT hr = D3DXTriPatchSize( &segmentCount, &tris, &verts );

		if( RECORD_D3D9( hr ).IsFailure )
		{
			triangleCount = 0;
			vertexCount = 0;
		}
		else
		{
			triangleCount = tris;
			vertexCount = verts;
		}

		return Result::Last;
	}

	Format D3DX::MakeFourCC( Byte c1, Byte c2, Byte c3, Byte c4 )
	{
		int fourcc = (c4 << 24) | (c3 << 16) | (c2 << 8) | (c1);
		return static_cast<Format>( fourcc );
	}

	bool D3DX::DebugMute( bool mute )
	{
		return D3DXDebugMute( mute ) > 0;
	}

	array<int>^ D3DX::OptimizeFaces( array<int>^ indices, int faceCount, int vertexCount )
	{
		array<int>^ results = gcnew array<int>( faceCount );

		pin_ptr<int> pinnedResults = &results[0];
		pin_ptr<int> pinnedIndices = &indices[0];

		HRESULT hr = D3DXOptimizeFaces( reinterpret_cast<LPCVOID>( pinnedIndices ), faceCount, vertexCount,
			TRUE, reinterpret_cast<DWORD*>( pinnedResults ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return results;
	}

	array<int>^ D3DX::OptimizeFaces( array<Int16>^ indices, int faceCount, int vertexCount )
	{
		array<int>^ results = gcnew array<int>( faceCount );

		pin_ptr<int> pinnedResults = &results[0];
		pin_ptr<Int16> pinnedIndices = &indices[0];

		HRESULT hr = D3DXOptimizeFaces( reinterpret_cast<LPCVOID>( pinnedIndices ), faceCount, vertexCount,
			FALSE, reinterpret_cast<DWORD*>( pinnedResults ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return results;
	}

	array<int>^ D3DX::OptimizeVertices( array<int>^ indices, int faceCount, int vertexCount )
	{
		array<int>^ results = gcnew array<int>( vertexCount );

		pin_ptr<int> pinnedResults = &results[0];
		pin_ptr<int> pinnedIndices = &indices[0];

		HRESULT hr = D3DXOptimizeVertices( reinterpret_cast<LPCVOID>( pinnedIndices ), faceCount, vertexCount,
			TRUE, reinterpret_cast<DWORD*>( pinnedResults ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return results;
	}

	array<int>^ D3DX::OptimizeVertices( array<Int16>^ indices, int faceCount, int vertexCount )
	{
		array<int>^ results = gcnew array<int>( vertexCount );

		pin_ptr<int> pinnedResults = &results[0];
		pin_ptr<Int16> pinnedIndices = &indices[0];

		HRESULT hr = D3DXOptimizeVertices( reinterpret_cast<LPCVOID>( pinnedIndices ), faceCount, vertexCount,
			FALSE, reinterpret_cast<DWORD*>( pinnedResults ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return results;
	}

	bool D3DX::CheckVersion()
	{
		if( D3DXCheckVersion( D3D_SDK_VERSION, D3DX_SDK_VERSION ) )
			return true;
		else
			return false;
	}

	float D3DX::FresnelTerm( float cosTheta, float refractionIndex )
	{
		return D3DXFresnelTerm( cosTheta, refractionIndex );
	}

	array<Vector3>^ D3DX::GetVectors( DataStream^ stream, int vertexCount, VertexFormat format )
	{
		return GetVectors( stream, vertexCount, GetFVFVertexSize( format ) );
	}

	array<Vector3>^ D3DX::GetVectors( DataStream^ stream, int vertexCount, int stride )
	{
		array<Vector3>^ results = gcnew array<Vector3>( vertexCount );
		int jump = stride - 12;

		for( int i = 0; i < vertexCount; i++ )
		{
			results[i] = stream->Read<Vector3>();
			stream->Position += jump;
		}

		return results;
	}
}
}