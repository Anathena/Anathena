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

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class ProgressiveMesh;

		[System::Obsolete("SimplificationMesh is deprecated and may be removed in future releases.")]
		public ref class SimplificationMesh : ComObject
		{
			COMOBJECT(ID3DXSPMesh, SimplificationMesh);

		public:
			SimplificationMesh( Mesh^ mesh, array<AttributeWeights>^ vertexAttributeWeights, array<float>^ vertexWeights );
			SimplificationMesh( Mesh^ mesh, array<AttributeWeights>^ vertexAttributeWeights );
			SimplificationMesh( Mesh^ mesh, array<float>^ vertexWeights );
			SimplificationMesh( Mesh^ mesh );
			
			property SlimDX::Direct3D9::Device^ Device
			{
				SlimDX::Direct3D9::Device^ get();
			}

			Mesh^ Clone( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration, [Out] array<int>^% vertexRemap );
			Mesh^ Clone( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration );

			Mesh^ Clone( SlimDX::Direct3D9::Device^ device, MeshFlags options, VertexFormat fvf, [Out] array<int>^% vertexRemap );
			Mesh^ Clone( SlimDX::Direct3D9::Device^ device, MeshFlags options, VertexFormat fvf );

			ProgressiveMesh^ CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration, [Out] array<int>^% vertexRemap, [Out] array<float>^% errorsByFace );
			ProgressiveMesh^ CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration, [Out] array<int>^% vertexRemap );
			ProgressiveMesh^ CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, array<VertexElement>^ vertexDeclaration );

			ProgressiveMesh^ CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, VertexFormat fvf, [Out] array<int>^% vertexRemap, [Out] array<float>^% errorsByFace );
			ProgressiveMesh^ CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, VertexFormat fvf, [Out] array<int>^% vertexRemap );
			ProgressiveMesh^ CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags options, VertexFormat fvf );

			array<VertexElement>^ GetDeclaration();

			array<AttributeWeights>^ GetVertexAttributeWeights();
			array<float>^ GetVertexWeights();

			Result ReduceFaces( int faces );
			Result ReduceVertices( int vertices );

			property VertexFormat VertexFormat { SlimDX::Direct3D9::VertexFormat get(); }
			property int MaximumFaceCount { int get(); }
			property int MaximumVertexCount { int get(); }
			property int FaceCount { int get(); }
			property int VertexCount { int get(); }
			property MeshFlags CreationOptions { MeshFlags get(); }
		};
	}
}