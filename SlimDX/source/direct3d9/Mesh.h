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

#include "../math/Matrix.h"
#include "../math/Ray.h"

#include "XFile.h"
#include "BaseMesh.h"

#include "EffectDefault.h"
#include "EffectInstance.h"
#include "ExtendedMaterial.h"
#include "GlyphMetricsFloat.h"
#include "AttributeWeights.h"
#include "WeldEpsilons.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class SkinInfo;

		/// <summary>
		/// Provides an interface to manipulate mesh objects.
		/// </summary>
		/// <unmanaged>ID3DXMesh</unmanaged>
		public ref class Mesh : public BaseMesh
		{
			COMOBJECT(ID3DXMesh, Mesh);

		private:
			array<int>^ adjacency;
			array<ExtendedMaterial>^ materials;
			array<EffectInstance>^ effects;
			SkinInfo^ skinInfo;

			static Mesh^ FromMemory_Internal( SlimDX::Direct3D9::Device^ device, const void* memory, DWORD size, MeshFlags flags );

		internal:
			void SetAdjacency( DWORD *adjacency );

		public:
			Mesh( SlimDX::Direct3D9::Device^ device, int faceCount, int vertexCount, MeshFlags options, array<VertexElement>^ vertexDeclaration );
			Mesh( SlimDX::Direct3D9::Device^ device, int faceCount, int vertexCount, MeshFlags options, SlimDX::Direct3D9::VertexFormat fvf );
			virtual ~Mesh() { }

			static Mesh^ FromMemory( SlimDX::Direct3D9::Device^ device, array<System::Byte>^ memory, MeshFlags flags );
			static Mesh^ FromStream( SlimDX::Direct3D9::Device^ device, System::IO::Stream^ stream, MeshFlags flags );
			static Mesh^ FromFile( SlimDX::Direct3D9::Device^ device, System::String^ fileName, MeshFlags flags );
			static Mesh^ FromXFile( SlimDX::Direct3D9::Device^ device, XFileData^ xfile, MeshFlags flags );

			static Result ToXFile( Mesh^ mesh, System::String^ fileName, XFileFormat format, System::Runtime::InteropServices::CharSet charSet );
			static Result ToXFile( Mesh^ mesh, System::String^ fileName, XFileFormat format );

			static Mesh^ CreateBox( SlimDX::Direct3D9::Device^ device, float width, float height, float depth );
			static Mesh^ CreateCylinder( SlimDX::Direct3D9::Device^ device, float radius1, float radius2, float length, int slices, int stacks );
			static Mesh^ CreateSphere( SlimDX::Direct3D9::Device^ device, float radius, int slices, int stacks );
			static Mesh^ CreateTeapot( SlimDX::Direct3D9::Device^ device );
			static Mesh^ CreateTorus( SlimDX::Direct3D9::Device^ device, float innerRadius, float outerRadius, int sides, int rings );

			static Mesh^ CreateText( SlimDX::Direct3D9::Device^ device, System::Drawing::Font^ font, System::String^ text, float deviation, float extrusion, [Out] array<GlyphMetricsFloat>^% glyphMetrics );
			static Mesh^ CreateText( SlimDX::Direct3D9::Device^ device, System::Drawing::Font^ font, System::String^ text, float deviation, float extrusion );

			static Mesh^ Concatenate( SlimDX::Direct3D9::Device^ device, array<Mesh^>^ meshes, MeshFlags options, array<Matrix>^ geometryTransforms, array<Matrix>^ textureTransforms, array<VertexElement>^ vertexDeclaration );
			static Mesh^ Concatenate( SlimDX::Direct3D9::Device^ device, array<Mesh^>^ meshes, MeshFlags options, array<Matrix>^ geometryTransforms, array<Matrix>^ textureTransforms );
			static Mesh^ Concatenate( SlimDX::Direct3D9::Device^ device, array<Mesh^>^ meshes, MeshFlags options );

			static Mesh^ Simplify( Mesh^ mesh, array<AttributeWeights>^ attributeWeights, array<float>^ vertexWeights, int minimumValue, MeshSimplification options );
			static Mesh^ Simplify( Mesh^ mesh, array<AttributeWeights>^ attributeWeights, int minimumValue, MeshSimplification options );
			static Mesh^ Simplify( Mesh^ mesh, int minimumValue, MeshSimplification options );

			static Mesh^ TessellateNPatches( Mesh^ mesh, float segmentCount, bool quadraticInterpolation );

			void GenerateAdjacency( float epsilon ) new;

			System::String^ Validate();

			DataStream^ LockAttributeBuffer( LockFlags flags );
			Result UnlockAttributeBuffer();
			Result SetAttributeTable( array<AttributeRange>^ table );

			Result OptimizeInPlace( MeshOptimizeFlags flags, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap );
			Result OptimizeInPlace( MeshOptimizeFlags flags );

			Mesh^ Optimize( MeshOptimizeFlags flags, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap );
			Mesh^ Optimize( MeshOptimizeFlags flags );

			Mesh^ Clean( CleanType type, [Out] System::String^% errorsAndWarnings );
			Mesh^ Clean( CleanType type );
			
			Result ComputeNormals();
			Result ComputeTangent( int textureStage, int tangentIndex, int binormalIndex, bool wrap );
			Result ComputeTangentFrame( TangentOptions options );

			Mesh^ ComputeTangentFrame( int textureInSemantic, int textureInIndex, int partialOutSemanticU, 
				int partialOutIndexU, int partialOutSemanticV, int partialOutIndexV, int normalOutSemantic,
				int normalOutIndex, TangentOptions options, float partialEdgeThreshold,
				float singularPointThreshold, float normalEdgeThreshold, [Out] array<int>^% vertexMapping );

			Mesh^ ComputeTangentFrame( int textureInSemantic, int textureInIndex, int partialOutSemanticU, 
				int partialOutIndexU, int partialOutSemanticV, int partialOutIndexV, int normalOutSemantic,
				int normalOutIndex, TangentOptions options, float partialEdgeThreshold,
				float singularPointThreshold, float normalEdgeThreshold );

			Result TessellateRectanglePatch( SlimDX::Direct3D9::VertexBuffer^ vertexBuffer, array<float>^ segmentCounts, 
				array<VertexElement>^ vertexDeclaration, RectanglePatchInfo rectanglePatchInfo );

			Result TessellateTrianglePatch( SlimDX::Direct3D9::VertexBuffer^ vertexBuffer, array<float>^ segmentCounts,
				array<VertexElement>^ vertexDeclaration, TrianglePatchInfo trianglePatchInfo );

			array<ExtendedMaterial>^ GetMaterials() { return materials; }
			void SetMaterials( array<ExtendedMaterial>^ value ) { materials = value; }

			array<EffectInstance>^ GetEffects() { return effects; }
			void SetEffects( array<EffectInstance>^ value ) { effects = value; }

			void SetAdjacency( array<int>^ adjacency ) { this->adjacency = adjacency; }
			array<int>^ GetAdjacency() { return adjacency; }

			Result WeldVertices( WeldFlags flags, WeldEpsilons epsilons, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap );
			Result WeldVertices( WeldFlags flags, [Out] array<int>^% faceRemap, [Out] array<int>^% vertexRemap );
			Result WeldVertices( WeldFlags flags, WeldEpsilons epsilons );
			Result WeldVertices( WeldFlags flags );

			property SkinInfo^ SkinInfo
			{ 
				SlimDX::Direct3D9::SkinInfo^ get() { return skinInfo; }
				void set( SlimDX::Direct3D9::SkinInfo^ value ) { skinInfo = value; }
			}
		};
	}
}
