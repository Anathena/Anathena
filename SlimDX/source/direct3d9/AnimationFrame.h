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

#include "XFile.h"

namespace SlimDX
{
	value class BoundingSphere;

	namespace Direct3D9
	{
		ref class Frame;
		ref class AnimationController;
		ref class MeshData;
		ref class MeshContainer;
		ref class Device;

		class MeshContainerShim;

		interface struct IAllocateHierarchy;
		interface struct ISaveUserData;
		interface struct ILoadUserData;

		class FrameShim : public D3DXFRAME
		{
		private:
			gcroot<Frame^> m_Frame;

		public:
			FrameShim( Frame^ frame );
			FrameShim( Frame^ frame, const D3DXFRAME &pFrame );

			Frame^ GetFrame() { return m_Frame; }
		};

		///<summary>Describes a transform frame in a transformation hierarchy.</summary>
		///<unmanaged>D3DXFRAME</unmanaged>
		public ref class Frame : System::IEquatable<Frame^>
		{
		internal:
			property D3DXFRAME *Pointer;

			void Destruct();

			System::String^ m_Name;
			Frame^ m_FirstChild;
			Frame^ m_Sibling;
			MeshContainer^ m_MeshContainer;

			static Frame^ BuildHierarchyFromUnmanaged( FrameShim* pFrame );
			static void BuildManagedFrames( Frame^% frame, FrameShim* pFrame );
			static void BuildManagedMeshes( MeshContainer^% mesh, MeshContainerShim* pMesh );
			static void RegisterAnimations( Frame^ frame, LPD3DXANIMATIONCONTROLLER animation );

		private:
			static Frame^ LoadHierarchyFromX_Internal( Device^ device, const void* memory, DWORD size, MeshFlags options, IAllocateHierarchy^ allocator, ILoadUserData^ userDataLoader, [Out] AnimationController^% animationController );

		public:
			Frame();
			virtual ~Frame();
			!Frame();

			static Frame^ LoadHierarchyFromX( Device^ device, System::String^ fileName, MeshFlags options, IAllocateHierarchy^ allocator, ILoadUserData^ userDataLoader, [Out] AnimationController^% animationController );
			static Frame^ LoadHierarchyFromX( Device^ device, array<System::Byte>^ memory, MeshFlags options, IAllocateHierarchy^ allocator, ILoadUserData^ userDataLoader, [Out] AnimationController^% animationController );
			static Frame^ LoadHierarchyFromX( Device^ device, System::IO::Stream^ stream, MeshFlags options, IAllocateHierarchy^ allocator, ILoadUserData^ userDataLoader, [Out] AnimationController^% animationController );

			static Result SaveHierarchyToFile( System::String^ fileName, XFileFormat format, Frame^ root, AnimationController^ animationController, ISaveUserData^ userDataSaver );
			static Result SaveHierarchyToFile( System::String^ fileName, XFileFormat format, Frame^ root, AnimationController^ animationController );

			Frame^ FindChild( System::String^ name );
			void AppendChild( Frame^ child );

			static BoundingSphere CalculateBoundingSphere( Frame^ root );
			static Result DestroyHierarchy( Frame^ root, IAllocateHierarchy^ allocator );
			static int CountNamedFrames( Frame^ root );
			static void RegisterNamedMatrices( Frame^ root, AnimationController^ controller );

			property System::String^ Name
			{
				System::String^ get();
				void set( System::String^ value );
			}

			property Matrix TransformationMatrix
			{
				Matrix get();
				void set( Matrix value );
			}

			property MeshContainer^ MeshContainer
			{
				SlimDX::Direct3D9::MeshContainer^ get();
				void set( SlimDX::Direct3D9::MeshContainer^ value );
			}

			property Frame^ Sibling
			{
				Frame^ get();
				void set( Frame^ value );
			}

			property Frame^ FirstChild
			{
				Frame^ get();
				void set( Frame^ value );
			}

			static bool operator == ( Frame^ left, Frame^ right );
			static bool operator != ( Frame^ left, Frame^ right );

			virtual System::String^ ToString() override;
			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( Frame^ other );
			static bool Equals( Frame^ value1, Frame^ value2 );
		};
	}
}
