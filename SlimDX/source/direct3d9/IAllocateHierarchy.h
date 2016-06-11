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
		ref class Frame;
		ref class MeshData;

		/// <summary>
		/// Defines an interface for allocating and freeing frame and mesh container objects. Methods on this interface are called during
		/// loading and destroying of frame hierarchies.
		/// </summary>
		/// <unmanaged>ID3DXAllocateHierarchy</unmanaged>
		public interface struct IAllocateHierarchy
		{
			/// <summary>
			/// Requests the allocation of a mesh container object.
			/// </summary>
			/// <param name="name">Name of the mesh.</param>
			/// <param name="meshData">The actual mesh data.</param>
			/// <param name="materials">Set of materials used in the mesh.</param>
			/// <param name="effectInstances">Set of effects used in the mesh.</param>
			/// <param name="adjacency">Adjancey array used in the mesh.</param>
			/// <param name="skinInfo">Skin mesh object containing skin data.</param>
			/// <returns>The created mesh container.</returns>
			virtual MeshContainer^ CreateMeshContainer( System::String^ name, MeshData^ meshData, array<ExtendedMaterial>^ materials, array<EffectInstance>^ effectInstances, array<int>^ adjacency, SkinInfo^ skinInfo ) = 0;
			
			/// <summary>
			/// Requests the allocation of a frame object.
			/// </summary>
			/// <param name="name">Name of the frame to be created.</param>
			/// <returns>The created frame object.</returns>
			virtual Frame^ CreateFrame( System::String^ name ) = 0;
			
			/// <summary>
			/// Requests deallocation of a frame object.
			/// </summary>
			/// <param name="frame">The frame to be deallocated.</param>
			virtual void DestroyFrame( Frame^ frame ) = 0;
			
			/// <summary>
			/// Requests deallocation of a mesh container object.
			/// </summary>
			/// <param name="container">The mesh container to be deallocated.</param>
			virtual void DestroyMeshContainer( MeshContainer^ container ) = 0;
		};

		class IAllocateHierarchyShim : public ID3DXAllocateHierarchy
		{
		private:
			gcroot<IAllocateHierarchy^> m_WrappedInterface;

		public:
			IAllocateHierarchyShim( IAllocateHierarchy^ wrappedInterface );

			HRESULT WINAPI CreateFrame( LPCSTR Name, LPD3DXFRAME *ppNewFrame );
			HRESULT WINAPI CreateMeshContainer( LPCSTR Name, const D3DXMESHDATA *pMeshData, const D3DXMATERIAL *pMaterials, const D3DXEFFECTINSTANCE *pEffectInstances, DWORD NumMaterials, const DWORD *pAdjacency, LPD3DXSKININFO pSkinInfo, LPD3DXMESHCONTAINER *ppNewMeshContainer );
			HRESULT WINAPI DestroyFrame( LPD3DXFRAME pFrameToFree );
			HRESULT WINAPI DestroyMeshContainer( LPD3DXMESHCONTAINER pMeshContainerToFree );
		};
	}
}