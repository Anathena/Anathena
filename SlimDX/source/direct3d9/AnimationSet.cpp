#include "stdafx.h"
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
#include <d3d9.h>
#include <d3dx9.h>
#include <vcclr.h>

#include "../ComObject.h"
#include "../Math/Vector3.h"
#include "../DataStream.h"

#include "Device.h"
#include "Mesh.h"
#include "ProgressiveMesh.h"
#include "PatchMesh.h"
#include "SkinInfo.h"
#include "AnimationFrame.h"
#include "AnimationSet.h"
#include "Direct3D9Exception.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	int AnimationSet::GetAnimationIndex( String^ name )
	{
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<const unsigned char> pinnedName = &nameBytes[0];
		unsigned int result;

		HRESULT hr = InternalPointer->GetAnimationIndexByName( reinterpret_cast<LPCSTR>( pinnedName ), &result );

		if( RECORD_D3D9( hr ).IsFailure )
			return 0;

		return static_cast<int>( result );
	}

	String^ AnimationSet::GetAnimationName( int index )
	{
		LPCSTR result;
		HRESULT hr = InternalPointer->GetAnimationNameByIndex( index, &result );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew String( result );
	}

	IntPtr AnimationSet::GetCallback( double position, CallbackSearchFlags flags, [Out] double% callbackPosition )
	{
		pin_ptr<double> pinPosition = &callbackPosition;
		LPVOID data;

		HRESULT hr = InternalPointer->GetCallback( position, static_cast<DWORD>( flags ), pinPosition, &data );

		if( RECORD_D3D9( hr ).IsFailure )
			return IntPtr::Zero;

		return IntPtr( data );
	}

	AnimationOutput^ AnimationSet::GetTransformation( double periodicPosition, int animation )
	{
		Vector3 scale;
		Vector3 translation;
		Quaternion rotation;

		HRESULT hr = InternalPointer->GetSRT( periodicPosition, animation, reinterpret_cast<D3DXVECTOR3*>( &scale ), 
			reinterpret_cast<D3DXQUATERNION*>( &rotation ), reinterpret_cast<D3DXVECTOR3*>( &translation ) );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		AnimationOutput^ output = gcnew AnimationOutput();
		output->Flags = AnimationOutputFlags::Rotation | AnimationOutputFlags::Scale | AnimationOutputFlags::Translation;
		output->Scaling = scale;
		output->Translation = translation;
		output->Rotation = rotation;

		return output;
	}

	String^ AnimationSet::Name::get()
	{
		return gcnew String( InternalPointer->GetName() );
	}

	int AnimationSet::AnimationCount::get()
	{
		return InternalPointer->GetNumAnimations();
	}

	double AnimationSet::Period::get()
	{
		return InternalPointer->GetPeriod();
	}

	double AnimationSet::GetPeriodicPosition( double position )
	{
		return InternalPointer->GetPeriodicPosition( position );
	}
}
}