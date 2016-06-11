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

#include "PatchMeshInfo.h"
#include "DisplacementParameters.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class BaseMesh;
		ref class Mesh;
		ref class VertexBuffer;
		ref class IndexBuffer;
		
		public ref class PatchMesh : ComObject
		{
			COMOBJECT(ID3DXPatchMesh, PatchMesh);

		public:
			PatchMesh( SlimDX::Direct3D9::Device^ device, PatchInfo info, int patchCount, int vertexCount, array<VertexElement>^ vertexDeclaration );
			PatchMesh( Mesh^ mesh );

			static PatchMesh^ FromXFile( SlimDX::Direct3D9::Device^ device, XFileData^ xfile, MeshFlags flags, [Out] array<ExtendedMaterial>^% materials, [Out] array<EffectInstance>^% effectInstances );
			static PatchMesh^ FromXFile( SlimDX::Direct3D9::Device^ device, XFileData^ xfile, MeshFlags flags, [Out] array<ExtendedMaterial>^% materials );
			static PatchMesh^ FromXFile( SlimDX::Direct3D9::Device^ device, XFileData^ xfile, MeshFlags flags );

			PatchMesh^ Clone( MeshFlags flags, array<VertexElement>^ vertexDeclaration );
			Result GenerateAdjacency( float tolerance );

			array<VertexElement>^ GetDeclaration();
			Result Optimize();

			DataStream^ LockAttributeBuffer( LockFlags flags );
			Result UnlockAttributeBuffer();

			DataStream^ LockIndexBuffer( LockFlags flags );
			Result UnlockIndexBuffer();

			DataStream^ LockVertexBuffer( LockFlags flags );
			Result UnlockVertexBuffer();

			Result GetTessellationSize( float tessellationLevel, bool adaptive, [Out] int% triangleCount, [Out] int% vertexCount );
			Result Tessellate( float tessellationLevel, Mesh^ mesh );
			Result Tessellate( Vector4 translation, int minimumLevel, int maximumLevel, Mesh^ mesh );

			property int ControlVerticesPerPatch { int get(); }
			property int PatchCount { int get(); }
			property int VertexCount { int get(); }
			property PatchMeshType Type { PatchMeshType get(); }

			property SlimDX::Direct3D9::Device^ Device
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

			property PatchInfo PatchInfo
			{
				SlimDX::Direct3D9::PatchInfo get();
			}

			property DisplacementParameters DisplacementParameters
			{
				SlimDX::Direct3D9::DisplacementParameters get();
				void set( SlimDX::Direct3D9::DisplacementParameters value );
			}
		};
	}
}