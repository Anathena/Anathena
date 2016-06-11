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

#include "Delegates.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class AnimationSet;
		ref class AnimationOutput;
		ref class Frame;

		value class TrackDescription;
		value class EventDescription;

		/// <summary>
		/// Used to control animation functionality, connecting animation sets with the transformation frames that are being animated.
		/// </summary>
		/// <unmanaged>ID3DXAnimationController</unmanaged>
		public ref class AnimationController : public ComObject
		{
			COMOBJECT(ID3DXAnimationController, AnimationController);

		private:
			System::Collections::Generic::List<System::Runtime::InteropServices::GCHandle>^ outputs;

		public:
			AnimationController( int maxAnimationOutputs, int maxAnimationSets, int maxTracks, int maxEvents );
			~AnimationController();

			[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
			Result AdvanceTime( double time, AnimationCallback^ handler );
			AnimationController^ Clone( int maxAnimationOutputs, int maxAnimationSets, int maxTracks, int maxEvents );
			
			generic<typename T> where T : AnimationSet
			T GetAnimationSet( int index );

			generic<typename T> where T : AnimationSet
			T GetAnimationSet( System::String^ name );
			int GetCurrentTrackEvent( int track, EventType eventType );

			EventDescription GetEventDescription( int handle );
			AnimationSet^ GetTrackAnimationSet( int track );
			TrackDescription GetTrackDescription( int track );

			int GetUpcomingPriorityBlend( int handle );
			int GetUpcomingTrackEvent( int track, int handle );

			int KeyPriorityBlend( float newBlendWeight, double startTime, double duration, TransitionType transition );
			int KeyTrackEnable( int track, bool enable, double startTime );
			int KeyTrackPosition( int track, double position, double startTime );
			int KeyTrackSpeed( int track, float newSpeed, double startTime, double duration, TransitionType transition );
			int KeyTrackWeight( int track, float newWeight, double startTime, double duration, TransitionType transition );

			Result RegisterAnimationOutput( System::String^ name, AnimationOutput^ output );
			Result RegisterAnimationOutput( Frame^ frame );
			Result RegisterAnimationSet( AnimationSet^ set );
			Result ResetTime();

			Result SetTrackAnimationSet( int track, AnimationSet^ set );
			Result SetTrackDescription( int track, TrackDescription description );
			Result EnableTrack( int track );
			Result DisableTrack( int track );
			Result SetTrackPosition( int track, double position );
			Result SetTrackPriority( int track, TrackPriority priority );
			Result SetTrackSpeed( int track, float speed );
			Result SetTrackWeight( int track, float weight );

			Result UnkeyAllPriorityBlends();
			Result UnkeyAllTrackEvents( int track );
			Result UnkeyEvent( int handle );
			Result UnregisterAnimationSet( AnimationSet^ set );

			bool ValidateEvent( int handle );

			property double Time { double get(); }
			property int CurrentPriorityBlend { int get(); }
			property float PriorityBlend { float get(); void set( float value ); }
			property int AnimationSetCount { int get(); }
			property int MaxAnimationOutputs { int get(); }
			property int MaxAnimationSets { int get(); }
			property int MaxTracks { int get(); }
			property int MaxEvents { int get(); }
		};
	}
}
