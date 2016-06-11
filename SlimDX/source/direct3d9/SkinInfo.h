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

#include "BoneCombination.h"

namespace SlimDX
{
	value class Matrix;

	namespace Direct3D9
	{
		value class VertexElement;
		enum class VertexFormat;

		/// <summary>Applications use the methods of the SkinInfo class to manipulate bone matrices,
		/// which are used to skin vertex data for animation.</summary>
		/// <unmanaged>ID3DXSkinInfo</unmanaged>
		public ref class SkinInfo : public ComObject
		{
			COMOBJECT(ID3DXSkinInfo, SkinInfo);

		public:
			SkinInfo( int vertexCount, array<VertexElement>^ vertexDeclaration, int boneCount );
			SkinInfo( BaseMesh^ mesh, int boneCount, array<BoneCombination^>^ boneCombinationTable );
			SkinInfo( int vertexCount, VertexFormat fvf, int boneCount );

			SkinInfo^ Clone();

			Mesh^ ConvertToBlendedMesh( Mesh^ mesh, array<int>^ adjacency, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap, [Out] int% maxVertexInfluence, [Out] array<BoneCombination^>^% boneCombinationTable );
			Mesh^ ConvertToBlendedMesh( Mesh^ mesh, array<int>^ adjacency, [Out] int% maxVertexInfluence, [Out] array<BoneCombination^>^% boneCombinationTable );

			Mesh^ ConvertToIndexedBlendedMesh( Mesh^ mesh, int paletteSize, array<int>^ adjacency, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap, [Out] int% maxVertexInfluence, [Out] array<BoneCombination^>^% boneCombinationTable );
			Mesh^ ConvertToIndexedBlendedMesh( Mesh^ mesh, int paletteSize, array<int>^ adjacency, [Out] int% maxVertexInfluence, [Out] array<BoneCombination^>^% boneCombinationTable );

			int FindBoneVertexInfluenceIndex( int bone, int vertex );

			Result GetBoneInfluence( int bone, [Out] array<int>^% vertices, [Out] array<float>^% weights );
			Result SetBoneInfluence( int bone, array<int>^ vertices, array<float>^ weights );

			System::String^ GetBoneName( int bone );
			Result SetBoneName( int bone, System::String^ name );

			Result GetBoneVertexInfluence( int bone, int influence, [Out] float% weight, [Out] int% vertex );
			Result SetBoneVertexInfluence( int bone, int influence, float weight );

			Matrix GetBoneOffsetMatrix( int bone );
			Result SetBoneOffsetMatrix( int bone, Matrix transform );

			array<VertexElement>^ GetDeclaration();
			Result SetDeclaration( array<VertexElement>^ declaration );

			int GetMaxFaceInfluences( IndexBuffer^ indexBuffer, int faceCount );
			int GetBoneInfluenceCount( int bone );

			Result Remap( array<int>^ remapData );

			Result UpdateSkinnedMesh( array<Matrix>^ boneTransforms, array<Matrix>^ boneInvTransposeTransforms, DataStream^ source, DataStream^ destination );

			property int MaximumVertexInfluences { int get(); }
			property int BoneCount { int get(); }

			property float MinimumBoneInfluence
			{
				float get();
				void set( float value );
			}

			property VertexFormat VertexFormat
			{
				SlimDX::Direct3D9::VertexFormat get();
				void set( SlimDX::Direct3D9::VertexFormat format );
			}
		};
	}
}
