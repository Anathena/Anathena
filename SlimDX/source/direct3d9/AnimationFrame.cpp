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
#include "stdafx.h"

#include "../ComObject.h"
#include "../DataStream.h"
#include "../Utilities.h"

#include "../math/BoundingSphere.h"

#include "Direct3D9Exception.h"

#include "Device.h"
#include "Mesh.h"
#include "SkinInfo.h"
#include "ProgressiveMesh.h"
#include "PatchMesh.h"
#include "AnimationController.h"
#include "AnimationFrame.h"
#include "MeshData.h"
#include "MeshContainer.h"

#include "IAllocateHierarchy.h"
#include "ILoadUserData.h"
#include "ISaveUserData.h"

using namespace System;
using namespace System::IO;
using namespace System::Collections::ObjectModel;

namespace SlimDX
{
namespace Direct3D9
{
	FrameShim::FrameShim( Frame^ frame, const D3DXFRAME &pFrame ) : D3DXFRAME( pFrame )
	{
		m_Frame = frame;
	}

	FrameShim::FrameShim( Frame^ frame )
	{
		m_Frame = frame;

		if( frame->Pointer != NULL )
			TransformationMatrix = frame->Pointer->TransformationMatrix;
	}

	Frame::Frame()
	{		
		// Manual Allocation: ugly, but necessary
		// we clean up in the destructor and finalizer
		Pointer = new FrameShim(this);
		Pointer->Name = NULL;
		Pointer->pFrameFirstChild = NULL;
		Pointer->pFrameSibling = NULL;
		Pointer->pMeshContainer = NULL;
        		
		m_Name = String::Empty;
		m_FirstChild = nullptr;
		m_Sibling = nullptr;
		m_MeshContainer = nullptr;
	}

	Frame::~Frame()
	{
		Destruct();
	}

	Frame::!Frame()
	{
		Destruct();
	}

	void Frame::Destruct()
	{
		if( Pointer != NULL )
		{
			Utilities::FreeNativeString( Pointer->Name );

			delete Pointer;
			Pointer = NULL;
		}
	}

	void Frame::AppendChild( Frame^ child )
	{
		if( child == this )
			throw gcnew Direct3D9Exception("Child frame can't be the same as the parent. This will cause a StackOverflowException.");

		if( m_FirstChild != nullptr )
		{
			Frame^ current = FirstChild;
			Frame^ lastValidChild = nullptr;
			while( current != nullptr )
			{
				lastValidChild = current;
				current = current->Sibling;
			}
		
			lastValidChild->Sibling = child;		
		}
		else
			FirstChild = child;
	}

	Frame^ Frame::FindChild( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		FrameShim* pFrame = static_cast<FrameShim*>( D3DXFrameFind( Pointer, reinterpret_cast<LPCSTR>( pinnedName ) ) );

		if( pFrame == NULL )
			return nullptr;

		return pFrame->GetFrame();
	}

	BoundingSphere Frame::CalculateBoundingSphere( Frame^ root )
	{
		Vector3 objectCenter;
		float radius;

		HRESULT hr = D3DXFrameCalculateBoundingSphere( root->Pointer, reinterpret_cast<D3DXVECTOR3*>( &objectCenter ), &radius );

		if( RECORD_D3D9( hr ).IsFailure )
			return BoundingSphere( Vector3( 0, 0, 0 ), 0.0f );

		return BoundingSphere( objectCenter, radius );
	}

	Result Frame::DestroyHierarchy( Frame^ root, IAllocateHierarchy^ allocator )
	{
		IAllocateHierarchyShim shim( allocator );					
		HRESULT hr = D3DXFrameDestroy( root->Pointer, &shim );

		return RECORD_D3D9( hr );
	}

	int Frame::CountNamedFrames( Frame^ root )
	{		
		return D3DXFrameNumNamedMatrices( root->Pointer );
	}

	void Frame::RegisterNamedMatrices( Frame^ root, AnimationController^ controller )
	{
		if( root == nullptr )
			throw gcnew ArgumentNullException( "root" );
		if( controller == nullptr )
			throw gcnew ArgumentNullException( "controller" );

		Frame::RegisterAnimations( root, controller->InternalPointer );
	}

	String^ Frame::Name::get()
	{
		return m_Name;
	}

	void Frame::Name::set( String^ value )
	{
		m_Name = value;

		Utilities::FreeNativeString( Pointer->Name );
		Pointer->Name = Utilities::AllocateNativeString( m_Name );
	}

	Matrix Frame::TransformationMatrix::get()
	{
		return Matrix::FromD3DXMATRIX( Pointer->TransformationMatrix );
	}

	void Frame::TransformationMatrix::set( Matrix value )
	{
		Pointer->TransformationMatrix = Matrix::ToD3DXMATRIX( value );
	}

	SlimDX::Direct3D9::MeshContainer^ Frame::MeshContainer::get()
	{
		return m_MeshContainer;
	}

	void Frame::MeshContainer::set( SlimDX::Direct3D9::MeshContainer^ value )
	{
		m_MeshContainer = value;

		if(value == nullptr)
			Pointer->pMeshContainer = NULL;
		else
			Pointer->pMeshContainer = value->Pointer;
	}

	Frame^ Frame::Sibling::get()
	{
		return m_Sibling;
	}

	void Frame::Sibling::set( Frame^ value )
	{
		m_Sibling = value;

		if(value == nullptr)
			Pointer->pFrameSibling = NULL;
		else
			Pointer->pFrameSibling = value->Pointer;
	}

	Frame^ Frame::FirstChild::get()
	{
		return m_FirstChild;
	}

	void Frame::FirstChild::set( Frame^ value )
	{
		m_FirstChild = value;

		if(value == nullptr)
			Pointer->pFrameFirstChild = NULL;
		else
			Pointer->pFrameFirstChild = value->Pointer;
	}

	Frame^ Frame::LoadHierarchyFromX_Internal( Device^ device, const void* memory, DWORD size, MeshFlags options, IAllocateHierarchy^ allocator, ILoadUserData^ userDataLoader, [Out] AnimationController^% animationController )
	{
		IAllocateHierarchyShim allocatorShim( allocator );
		ID3DXLoadUserData *userDataLoaderShimPtr = NULL;
		ILoadUserDataShim userDataLoaderShim( userDataLoader );
		LPD3DXFRAME result = NULL;
		LPD3DXANIMATIONCONTROLLER animationResult;

		animationController = nullptr;

		if( userDataLoader != nullptr )
			userDataLoaderShimPtr = &userDataLoaderShim;

		HRESULT hr = D3DXLoadMeshHierarchyFromXInMemory( memory, size, static_cast<DWORD>( options ), device->InternalPointer,
			&allocatorShim, userDataLoaderShimPtr, &result, &animationResult);

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		if( animationResult != NULL )
			animationController = AnimationController::FromPointer( animationResult );

		Frame^ frame = Frame::BuildHierarchyFromUnmanaged( static_cast<FrameShim*>( result ) );

		if( animationResult != NULL )
			Frame::RegisterAnimations( frame, animationResult );

		return frame;
	}

	Frame^ Frame::LoadHierarchyFromX( Device^ device, String^ fileName, MeshFlags options, 
		IAllocateHierarchy^ allocator, ILoadUserData^ userDataLoader, [Out] AnimationController^% animationController )
	{
		IAllocateHierarchyShim allocatorShim( allocator );
		ID3DXLoadUserData *userDataLoaderShimPtr = NULL;
		ILoadUserDataShim userDataLoaderShim( userDataLoader );
		LPD3DXFRAME result = NULL;
		LPD3DXANIMATIONCONTROLLER animationResult;

		animationController = nullptr;
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		if( userDataLoader != nullptr )
			userDataLoaderShimPtr = &userDataLoaderShim;

		HRESULT hr = D3DXLoadMeshHierarchyFromX( reinterpret_cast<LPCWSTR>( pinnedName ), static_cast<DWORD>( options ), device->InternalPointer,
			&allocatorShim, userDataLoaderShimPtr, &result, &animationResult);

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		if( animationResult != NULL )
			animationController = AnimationController::FromPointer( animationResult );

		Frame^ frame = Frame::BuildHierarchyFromUnmanaged( static_cast<FrameShim*>( result ) );

		if( animationResult != NULL )
			Frame::RegisterAnimations( frame, animationResult );

		return frame;
	}

	Frame^ Frame::LoadHierarchyFromX( Device^ device, array<Byte>^ memory, MeshFlags options, 
		IAllocateHierarchy^ allocator, ILoadUserData^ userDataLoader, [Out] AnimationController^% animationController )
	{
		pin_ptr<unsigned char> pinnedMemory = &memory[0];
		return Frame::LoadHierarchyFromX_Internal( device, pinnedMemory, memory->Length, options, allocator,
			userDataLoader, animationController );
	}

	Frame^ Frame::LoadHierarchyFromX( Device^ device, Stream^ stream, MeshFlags options, 
		IAllocateHierarchy^ allocator, ILoadUserData^ userDataLoader, [Out] AnimationController^% animationController )
	{
		DataStream^ ds = nullptr;
		array<Byte>^ data = Utilities::ReadStream( stream, &ds );

		if( data == nullptr )
		{
			DWORD size = static_cast<DWORD>( ds->RemainingLength );
			return Frame::LoadHierarchyFromX_Internal( device, reinterpret_cast<Byte*>( ds->SeekToEnd() ),
				size, options, allocator, userDataLoader, animationController );
		}

		return Frame::LoadHierarchyFromX( device, data, options, allocator, userDataLoader, animationController );
	}

	Result Frame::SaveHierarchyToFile( String^ fileName, XFileFormat format, Frame^ root, AnimationController^ animationController, ISaveUserData^ userDataSaver )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );
		ISaveUserDataShim shim( userDataSaver );

		LPD3DXANIMATIONCONTROLLER animation = NULL;
		if(animationController != nullptr)
			animation = animationController->InternalPointer;

		HRESULT hr = D3DXSaveMeshHierarchyToFile( reinterpret_cast<LPCWSTR>( pinnedName ), static_cast<DWORD>( format ),
			root->Pointer, animation, &shim );
		
		return RECORD_D3D9( hr );
	}

	Result Frame::SaveHierarchyToFile( String^ fileName, XFileFormat format, Frame^ root, AnimationController^ animationController )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		LPD3DXANIMATIONCONTROLLER animation = NULL;
		if(animationController != nullptr)
			animation = animationController->InternalPointer;

		HRESULT hr = D3DXSaveMeshHierarchyToFile( reinterpret_cast<LPCWSTR>( pinnedName ), static_cast<DWORD>( format ),
			root->Pointer, animation, NULL );
		
		return RECORD_D3D9( hr );
	}

	void Frame::RegisterAnimations( Frame^ frame, LPD3DXANIMATIONCONTROLLER animation )
	{
		if( frame->Name != nullptr )
			animation->RegisterAnimationOutput( frame->Pointer->Name, &frame->Pointer->TransformationMatrix, NULL, NULL, NULL );

		if( frame->FirstChild != nullptr )
			RegisterAnimations( frame->FirstChild, animation );

		if( frame->Sibling != nullptr )
			RegisterAnimations( frame->Sibling, animation );
	}

	Frame^ Frame::BuildHierarchyFromUnmanaged( FrameShim* pFrame )
	{		
		Frame ^ frame;
		BuildManagedFrames(frame, pFrame);
		return frame;
	}

	void Frame::BuildManagedFrames( Frame^% frame, FrameShim* pFrame )
	{
		frame = pFrame->GetFrame();

		if( pFrame->pMeshContainer != NULL )
		{
			SlimDX::Direct3D9::MeshContainer^ mc;
			BuildManagedMeshes( mc, static_cast<MeshContainerShim*>( pFrame->pMeshContainer ) );
			frame->MeshContainer = mc;
		}

		if( pFrame->pFrameFirstChild != NULL )
		{
			Frame^ fc;
			BuildManagedFrames( fc, static_cast<FrameShim*>( pFrame->pFrameFirstChild ) );
			frame->FirstChild = fc;
		}

		if( pFrame->pFrameSibling != NULL )
		{
			Frame^ sb;
			BuildManagedFrames( sb, static_cast<FrameShim*>( pFrame->pFrameSibling ) );
			frame->Sibling = sb;
		}
	}

	void Frame::BuildManagedMeshes( SlimDX::Direct3D9::MeshContainer^% mesh, MeshContainerShim* pMesh )
	{
		mesh = pMesh->GetMeshContainer();

		if( pMesh->pNextMeshContainer != NULL )
		{
			SlimDX::Direct3D9::MeshContainer^ nc;
			BuildManagedMeshes( nc, static_cast<MeshContainerShim*>( pMesh->pNextMeshContainer ) );
			mesh->NextMeshContainer = nc;
		}
	}

	String^ Frame::ToString()
	{
		return m_Name;
	}

	bool Frame::operator == ( Frame^ left, Frame^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return Frame::Equals( left, right );
	}

	bool Frame::operator != ( Frame^ left, Frame^ right )
	{
		return !( left == right );
	}

	int Frame::GetHashCode()
	{
		IntPtr pointer( Pointer );
		return pointer.GetHashCode();
	}

	bool Frame::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Frame^>( value ) );
	}

	bool Frame::Equals( Frame^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( Pointer == value->Pointer );
	}

	bool Frame::Equals( Frame^ value1, Frame^ value2 )
	{
		return value1->Equals( value2 );
	}
  }
}
