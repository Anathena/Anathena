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

#include "../DataStream.h"
#include "../ComObject.h"

#include "../Math/Matrix.h"
#include "../Math/Vector3.h"

#include "Direct3D9Exception.h"

#include "AnimationFrame.h"
#include "AnimationSet.h"
#include "AnimationController.h"
#include "TrackDescription.h"
#include "EventDescription.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;
using namespace System::Reflection;

namespace SlimDX
{
namespace Direct3D9
{
	AnimationController::AnimationController( int maxAnimationOutputs, int maxAnimationSets, int maxTracks, int maxEvents )
	{
		ID3DXAnimationController *pointer;

		HRESULT hr = D3DXCreateAnimationController( maxAnimationOutputs, maxAnimationSets, maxTracks, maxEvents, &pointer );		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( Result::Last );

		outputs = gcnew List<GCHandle>();

		Construct( pointer );
	}

	AnimationController::~AnimationController()
	{
		if (outputs != nullptr)
		{
			for each (GCHandle handle in outputs)
				handle.Free();
			outputs = nullptr;
		}
	}

	Result AnimationController::AdvanceTime( double time, AnimationCallback^ handler )
	{
		LPD3DXANIMATIONCALLBACKHANDLER callback = NULL;
		if(handler != nullptr)
			callback = reinterpret_cast< LPD3DXANIMATIONCALLBACKHANDLER >( Marshal::GetFunctionPointerForDelegate( handler ).ToPointer() );

		HRESULT hr = InternalPointer->AdvanceTime( time, callback );
		return RECORD_D3D9( hr );
	}

	AnimationController^ AnimationController::Clone( int maxAnimationOutputs, int maxAnimationSets, int maxTracks, int maxEvents )
	{
		LPD3DXANIMATIONCONTROLLER pointer;

		HRESULT hr = InternalPointer->CloneAnimationController( maxAnimationOutputs, maxAnimationSets, maxTracks, maxEvents, &pointer );

		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return gcnew AnimationController( pointer, nullptr );
	}

	// helper method to create a generic animation set
	generic<typename T> where T : AnimationSet
	T CreateAnimationSet(LPD3DXANIMATIONSET set)
	{
		Type^ type = T::typeid;
		if (type == AnimationSet::typeid)
			type = InternalAnimationSet::typeid;

		MethodInfo^ method = type->GetMethod("FromPointer", BindingFlags::Public | BindingFlags::Static);
		T result = safe_cast<T>(method->Invoke(nullptr, gcnew array<Object^> { IntPtr(set) }));

		return result;
	}

	generic<typename T> where T : AnimationSet
	T AnimationController::GetAnimationSet( int index )
	{
		LPD3DXANIMATIONSET set;

		HRESULT hr = InternalPointer->GetAnimationSet( index, &set );

		if( RECORD_D3D9( hr ).IsFailure )
			return T();

		return CreateAnimationSet<T>(set);
	}

	generic<typename T> where T : AnimationSet
	T AnimationController::GetAnimationSet( String^ name )
	{
		LPD3DXANIMATIONSET set;
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		HRESULT hr = InternalPointer->GetAnimationSetByName( reinterpret_cast<LPCSTR>( pinnedName ), &set );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return T();

		return CreateAnimationSet<T>(set);
	}

	int AnimationController::GetCurrentTrackEvent( int track, EventType eventType )
	{
		return InternalPointer->GetCurrentTrackEvent( track, static_cast<D3DXEVENT_TYPE>( eventType ) );
	}

	EventDescription AnimationController::GetEventDescription( int handle )
	{
		EventDescription result;

		HRESULT hr = InternalPointer->GetEventDesc( handle, reinterpret_cast<LPD3DXEVENT_DESC>( &result ) );
		RECORD_D3D9( hr );

		return result;
	}

	AnimationSet^ AnimationController::GetTrackAnimationSet( int track )
	{
		LPD3DXANIMATIONSET set;

		HRESULT hr = InternalPointer->GetTrackAnimationSet( track, &set );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return InternalAnimationSet::FromPointer( set, this );
	}

	TrackDescription AnimationController::GetTrackDescription( int track )
	{
		TrackDescription result;

		HRESULT hr = InternalPointer->GetTrackDesc( track, reinterpret_cast<LPD3DXTRACK_DESC>( &result ) );
		RECORD_D3D9( hr );

		return result;
	}

	int AnimationController::GetUpcomingPriorityBlend( int handle )
	{
		return InternalPointer->GetUpcomingPriorityBlend( handle );
	}

	int AnimationController::GetUpcomingTrackEvent( int track, int handle )
	{
		return InternalPointer->GetUpcomingTrackEvent( track, handle );
	}

	int AnimationController::KeyPriorityBlend( float newBlendWeight, double startTime, double duration, TransitionType transition )
	{
		return InternalPointer->KeyPriorityBlend( newBlendWeight, startTime, duration, static_cast<D3DXTRANSITION_TYPE>( transition ) );
	}

	int AnimationController::KeyTrackEnable( int track, bool enable, double startTime )
	{
		return InternalPointer->KeyTrackEnable( track, enable, startTime );
	}

	int AnimationController::KeyTrackPosition( int track, double position, double startTime )
	{
		return InternalPointer->KeyTrackPosition( track, position, startTime );
	}

	int AnimationController::KeyTrackSpeed( int track, float newSpeed, double startTime, double duration, TransitionType transition )
	{
		return InternalPointer->KeyTrackSpeed( track, newSpeed, startTime, duration, static_cast<D3DXTRANSITION_TYPE>( transition ) );
	}

	int AnimationController::KeyTrackWeight( int track, float newWeight, double startTime, double duration, TransitionType transition )
	{
		return InternalPointer->KeyTrackWeight( track, newWeight, startTime, duration, static_cast<D3DXTRANSITION_TYPE>( transition ) );
	}

	Result AnimationController::RegisterAnimationOutput( Frame^ frame )
	{
		HRESULT hr = InternalPointer->RegisterAnimationOutput( frame->Pointer->Name, &frame->Pointer->TransformationMatrix, NULL, NULL, NULL );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::RegisterAnimationOutput( String^ name, AnimationOutput^ output )
	{
		D3DXMATRIX *matrix = NULL;
		D3DXVECTOR3 *scale = NULL;
		D3DXVECTOR3 *translation = NULL;
		D3DXQUATERNION *rotation = NULL;
		array<unsigned char>^ nameBytes = System::Text::ASCIIEncoding::ASCII->GetBytes( name );
		pin_ptr<unsigned char> pinnedName = &nameBytes[0];

		GCHandle handle = GCHandle::Alloc(output, GCHandleType::Pinned);
		char *ptr = reinterpret_cast<char*>(handle.AddrOfPinnedObject().ToPointer()) + sizeof(AnimationOutputFlags);
		outputs->Add(handle);

		if( (output->Flags & AnimationOutputFlags::Transformation) == AnimationOutputFlags::Transformation )
			matrix = reinterpret_cast<D3DXMATRIX*>(ptr);

		ptr += sizeof(D3DXMATRIX);
		if( (output->Flags & AnimationOutputFlags::Scale) == AnimationOutputFlags::Scale )
			scale = reinterpret_cast<D3DXVECTOR3*>(ptr);

		ptr += sizeof(D3DXVECTOR3);
		if( (output->Flags & AnimationOutputFlags::Translation) == AnimationOutputFlags::Translation )
			translation = reinterpret_cast<D3DXVECTOR3*>(ptr);

		ptr += sizeof(D3DXVECTOR3);
		if( (output->Flags & AnimationOutputFlags::Rotation) == AnimationOutputFlags::Rotation )
			rotation = reinterpret_cast<D3DXQUATERNION*>(ptr);

		HRESULT hr = InternalPointer->RegisterAnimationOutput( reinterpret_cast<LPCSTR>( pinnedName ), matrix, scale, rotation, translation );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::RegisterAnimationSet( AnimationSet^ set )
	{
		HRESULT hr = InternalPointer->RegisterAnimationSet( set->InternalPointer );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::ResetTime()
	{
		HRESULT hr = InternalPointer->ResetTime();
		return RECORD_D3D9( hr );
	}

	Result AnimationController::SetTrackAnimationSet( int track, AnimationSet^ set )
	{
		HRESULT hr = InternalPointer->SetTrackAnimationSet( track, set->InternalPointer );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::SetTrackDescription( int track, TrackDescription description )
	{
		HRESULT hr = InternalPointer->SetTrackDesc( track, reinterpret_cast<LPD3DXTRACK_DESC>( &description ) );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::EnableTrack( int track )
	{
		HRESULT hr = InternalPointer->SetTrackEnable( track, true );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::DisableTrack( int track )
	{
		HRESULT hr = InternalPointer->SetTrackEnable( track, false );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::SetTrackPosition( int track, double position )
	{
		HRESULT hr = InternalPointer->SetTrackPosition( track, position );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::SetTrackPriority( int track, TrackPriority priority )
	{
		HRESULT hr = InternalPointer->SetTrackPriority( track, static_cast<D3DXPRIORITY_TYPE>( priority ) );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::SetTrackSpeed( int track, float speed )
	{
		HRESULT hr = InternalPointer->SetTrackSpeed( track, speed );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::SetTrackWeight( int track, float weight )
	{
		HRESULT hr = InternalPointer->SetTrackWeight( track, weight );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::UnkeyAllPriorityBlends()
	{
		HRESULT hr = InternalPointer->UnkeyAllPriorityBlends();
		return RECORD_D3D9( hr );
	}

	Result AnimationController::UnkeyAllTrackEvents( int track )
	{
		HRESULT hr = InternalPointer->UnkeyAllTrackEvents( track );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::UnkeyEvent( int handle )
	{
		HRESULT hr = InternalPointer->UnkeyEvent( handle );
		return RECORD_D3D9( hr );
	}

	Result AnimationController::UnregisterAnimationSet( AnimationSet^ set )
	{
		HRESULT hr = InternalPointer->UnregisterAnimationSet( set->InternalPointer );
		return RECORD_D3D9( hr );
	}

	bool AnimationController::ValidateEvent( int handle )
	{
		HRESULT hr = InternalPointer->ValidateEvent( handle );

		return (hr == S_OK);
	}

	double AnimationController::Time::get()
	{
		return InternalPointer->GetTime();
	}

	int AnimationController::CurrentPriorityBlend::get()
	{
		return InternalPointer->GetCurrentPriorityBlend();
	}

	float AnimationController::PriorityBlend::get()
	{
		return InternalPointer->GetPriorityBlend();
	}

	void AnimationController::PriorityBlend::set( float value )
	{
		InternalPointer->SetPriorityBlend( value );
	}

	int AnimationController::AnimationSetCount::get()
	{
		return InternalPointer->GetNumAnimationSets();
	}

	int AnimationController::MaxAnimationOutputs::get()
	{
		return InternalPointer->GetMaxNumAnimationOutputs();
	}

	int AnimationController::MaxAnimationSets::get()
	{
		return InternalPointer->GetMaxNumAnimationSets();
	}

	int AnimationController::MaxTracks::get()
	{
		return InternalPointer->GetMaxNumTracks();
	}

	int AnimationController::MaxEvents::get()
	{
		return InternalPointer->GetMaxNumEvents();
	}
}
}
