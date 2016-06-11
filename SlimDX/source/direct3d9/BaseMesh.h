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

#include "AttributeRange.h"
#include "IntersectInformation.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class Mesh;
		ref class VertexBuffer;
		ref class IndexBuffer;
		enum class VertexFormat;

		/// <summary>
		/// Provides an interface to manipulate mesh and progressive mesh objects.
		/// </summary>
		/// <unmanaged>ID3DXBaseMesh</unmanaged>
		public ref class BaseMesh abstract : public ComObject
		{
			COMOBJECT_BASE(ID3DXBaseMesh);

		protected:
			BaseMesh() { }

		public:
			Mesh^ Clone( Device^ device, MeshFlags flags, array<VertexElement>^ elements );
			Mesh^ Clone( Device^ device, MeshFlags flags, VertexFormat fvf );

			array<VertexElement>^ GetDeclaration();
			array<AttributeRange>^ GetAttributeTable();

			DataStream^ LockIndexBuffer( LockFlags flags );
			Result UnlockIndexBuffer();

			DataStream^ LockVertexBuffer( LockFlags flags );
			Result UnlockVertexBuffer();

			array<int>^ GenerateAdjacency( float epsilon );
			array<int>^ ConvertAdjacencyToPointReps( array<int>^ adjacency );
			array<int>^ ConvertPointRepsToAdjacency( array<int>^ pointReps );
			Result UpdateSemantics( array<VertexElement>^ elements );

			Result DrawSubset( int subset );

			IndexBuffer^ ConvertSubsetToSingleStrip( int attributeId, MeshFlags options, [Out] int% indexCount );
			IndexBuffer^ ConvertSubsetToStrips( int attributeId, MeshFlags options, [Out] int% indexCount, [Out] array<int>^% stripLengths );

			bool Intersects( Ray ray, [Out] float% distance, [Out] int% faceIndex, [Out] array<IntersectInformation>^% hits );
			bool Intersects( Ray ray, [Out] float% distance );
			bool Intersects( Ray ray );

			bool IntersectsSubset( Ray ray, int attributeId, [Out] float% distance, [Out] int% faceIndex, [Out] array<IntersectInformation>^% hits );
			bool IntersectsSubset( Ray ray, int attributeId, [Out] float% distance );
			bool IntersectsSubset( Ray ray, int attributeId );

			property int FaceCount { int get(); }
			property int VertexCount { int get(); }
			property VertexFormat VertexFormat { SlimDX::Direct3D9::VertexFormat get(); }
			property int BytesPerVertex { int get(); }
			property MeshFlags CreationOptions { MeshFlags get(); }

			property Device^ Device
			{
				SlimDX::Direct3D9::Device^ get();
			}

			property IndexBuffer^ IndexBuffer
			{
				SlimDX::Direct3D9::IndexBuffer^ get();
			}

			property VertexBuffer^ VertexBuffer
			{
				SlimDX::Direct3D9::VertexBuffer^ get();
			}
		};
	}
}