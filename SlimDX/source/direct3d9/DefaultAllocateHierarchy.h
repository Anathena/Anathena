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
		/// <summary>
		/// Defines a default allocator for frame and mesh container objects.
		/// </summary>
		public ref class DefaultAllocateHierarchy : IAllocateHierarchy
		{
		private:
			static DefaultAllocateHierarchy^ allocator;

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="DefaultAllocateHierarchy"/> class.
			/// </summary>
			DefaultAllocateHierarchy();

			/// <summary>
			/// Gets a global instance of the default allocator.
			/// </summary>
			static property DefaultAllocateHierarchy^ DefaultAllocator { DefaultAllocateHierarchy^ get(); }

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
			virtual MeshContainer^ CreateMeshContainer( System::String^ name, MeshData^ meshData, array<ExtendedMaterial>^ materials, array<EffectInstance>^ effectInstances, array<int>^ adjacency, SkinInfo^ skinInfo );
			
			/// <summary>
			/// Requests the allocation of a frame object.
			/// </summary>
			/// <param name="name">Name of the frame to be created.</param>
			/// <returns>The created frame object.</returns>
			virtual Frame^ CreateFrame( System::String^ name );
			
			/// <summary>
			/// Requests deallocation of a frame object.
			/// </summary>
			/// <param name="frame">The frame to be deallocated.</param>
			virtual void DestroyFrame( Frame^ frame );
			
			/// <summary>
			/// Requests deallocation of a mesh container object.
			/// </summary>
			/// <param name="container">The mesh container to be deallocated.</param>
			virtual void DestroyMeshContainer( MeshContainer^ container );
		};
	}
}