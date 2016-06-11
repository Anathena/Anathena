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
		ref class BaseMesh;
		ref class Mesh;
		ref class VertexBuffer;
		ref class IndexBuffer;

		ref class StreamShim : System::Runtime::InteropServices::ComTypes::IStream
		{
		private:
			System::IO::Stream^ m_WrappedStream;

			long long position;
			void SetSizeToPosition();

		public:
			StreamShim( System::IO::Stream^ stream );

			virtual void Clone( [Out] System::Runtime::InteropServices::ComTypes::IStream^% ppstm );
			virtual void Commit( int grfCommitFlags );
			virtual void CopyTo( System::Runtime::InteropServices::ComTypes::IStream^ pstm, long long cb, System::IntPtr pcbRead, System::IntPtr pcbWritten );
			virtual void LockRegion( long long libOffset, long long cb, int dwLockType );
			virtual void Read( [Out] array<unsigned char>^ pv, int cb, System::IntPtr pcbRead );
			virtual void Revert();
			virtual void Seek( long long dlibMove, int dwOrigin, System::IntPtr plibNewPosition );
			virtual void SetSize( long long libNewSize );
			virtual void Stat( [Out] System::Runtime::InteropServices::ComTypes::STATSTG% pstatstg, int grfStatFlag );
			virtual void UnlockRegion( long long libOffset, long long cb, int dwLockType );
			virtual void Write( array<unsigned char>^ pv, int cb, System::IntPtr pcbWritten );
		};

		public ref class ProgressiveMesh : public BaseMesh
		{
			COMOBJECT(ID3DXPMesh, ProgressiveMesh);

		private:
			array<ExtendedMaterial>^ materials;
			array<EffectInstance>^ effects;

		public:
			ProgressiveMesh( Mesh^ mesh, array<AttributeWeights>^ attributeWeights, array<float>^ vertexWeights, int minimumValue, MeshSimplification options );
			ProgressiveMesh( Mesh^ mesh, array<AttributeWeights>^ attributeWeights, int minimumValue, MeshSimplification options );
			ProgressiveMesh( Mesh^ mesh, int minimumValue, MeshSimplification options );
			virtual ~ProgressiveMesh() { }

			static ProgressiveMesh^ FromStream( SlimDX::Direct3D9::Device^ device, System::IO::Stream^ stream, MeshFlags flags );
			static Result ToStream( ProgressiveMesh^ mesh, System::IO::Stream^ stream );

			ProgressiveMesh^ CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags flags, array<VertexElement>^ vertexDeclaration );
			ProgressiveMesh^ CloneProgressive( SlimDX::Direct3D9::Device^ device, MeshFlags flags, SlimDX::Direct3D9::VertexFormat format );

			Result GenerateVertexHistory( array<int>^ vertexHistory );
			array<int>^ GetAdjacency();
		
			Mesh^ Optimize( MeshOptimizeFlags flags );
			Mesh^ Optimize( MeshOptimizeFlags flags, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap );

			Result OptimizeBaseLevelOfDetail( MeshOptimizeFlags flags );
			Result OptimizeBaseLevelOfDetail( MeshOptimizeFlags flags, [Out] array<int>^% faceRemap );

			Result SetFaceCount( int faceCount );
			Result SetVertexCount( int vertexCount );

			Result TrimFaces( int newFaceMinimum, int newFaceMaximum );
			Result TrimFaces( int newFaceMinimum, int newFaceMaximum, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap );
			Result TrimVertices( int newVertexMinimum, int newVertexMaximum );
			Result TrimVertices( int newVertexMinimum, int newVertexMaximum, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap );

			array<ExtendedMaterial>^ GetMaterials() { return materials; }
			void SetMaterials( array<ExtendedMaterial>^ value ) { materials = value; }

			array<EffectInstance>^ GetEffects() { return effects; }
			void SetEffects( array<EffectInstance>^ value ) { effects = value; }

			property int MaximumFaceCount { int get(); }
			property int MaximumVertexCount { int get(); }
			property int MinimumFaceCount { int get(); }
			property int MinimumVertexCount { int get(); }
		};
	}
}