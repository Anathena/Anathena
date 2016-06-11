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

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class MeshContainer;
		ref class MeshData;
		ref class SkinInfo;

		class MeshContainerShim : public D3DXMESHCONTAINER
		{
		private:
			gcroot<MeshContainer^> m_Container;

		public:
			MeshContainerShim( MeshContainer^ container );

			MeshContainer^ GetMeshContainer()
			{
				return m_Container;
			}
		};

		public ref class MeshContainer
		{
		internal:
			property D3DXMESHCONTAINER *Pointer;

			void Destruct();

			System::String^ m_Name;
			MeshContainer^ m_NextContainer;
			MeshData^ m_MeshData;
			SkinInfo^ m_SkinInfo;

		public:
			MeshContainer();
			virtual ~MeshContainer();
			!MeshContainer();

			array<ExtendedMaterial>^ GetMaterials();
			void SetMaterials( array<ExtendedMaterial>^ materials );

			array<EffectInstance>^ GetEffects();
			void SetEffects( array<EffectInstance>^ effects );

			array<int>^ GetAdjacency();
			void SetAdjacency( array<int>^ adjacency );

			property System::String^ Name
			{
				System::String^ get();
				void set( System::String^ value );
			}

			property MeshData^ MeshData
			{
				SlimDX::Direct3D9::MeshData^ get();
				void set( SlimDX::Direct3D9::MeshData^ value );
			}

			property SkinInfo^ SkinInfo
			{
				SlimDX::Direct3D9::SkinInfo^ get();
				void set( SlimDX::Direct3D9::SkinInfo^ value );
			}

			property MeshContainer^ NextMeshContainer
			{
				MeshContainer^ get();
				void set( MeshContainer^ value );
			}
		};
	}
}