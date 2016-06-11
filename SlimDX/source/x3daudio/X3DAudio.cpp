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

#include <windows.h>
#include <x3daudio.h>
#include <xaudio2.h>

#include "X3DAudio.h"

using namespace System;

namespace SlimDX
{
namespace X3DAudio
{
	X3DAUDIO_VECTOR Vector3ToX3DAudio( Vector3 value );

	X3DAudio::X3DAudio( SlimDX::Multimedia::Speakers speakers, float speedOfSound )
	{
		// Manual Allocation: cleaned up in the finalizer / destructor
		handle = new HandleWrapper();
		X3DAudioInitialize( static_cast<UINT32>( speakers ), speedOfSound, handle->Handle );
	}

	void X3DAudio::Destruct()
	{
		if( handle != NULL )
		{
			delete handle;
			handle = NULL;
		}
	}

	DspSettings^ X3DAudio::Calculate( Listener^ listener, Emitter^ emitter, CalculateFlags flags, int sourceChannelCount, int destinationChannelCount )
	{
		if( handle == NULL )
			return nullptr;

		X3DAUDIO_CONE cone;
		X3DAUDIO_LISTENER nativeListener = listener->ToUnmanaged();
		
		if( listener->Cone != nullptr )
		{
			cone = listener->Cone->ToUnmanaged();
			nativeListener.pCone = &cone;
		}
		else
			nativeListener.pCone = NULL;

		X3DAUDIO_EMITTER nativeEmitter;
		X3DAUDIO_CONE cone2;
		X3DAUDIO_DISTANCE_CURVE volumeCurve;
		X3DAUDIO_DISTANCE_CURVE lfeCurve;
		X3DAUDIO_DISTANCE_CURVE lpfDirectCurve;
		X3DAUDIO_DISTANCE_CURVE lpfReverbCurve;
		X3DAUDIO_DISTANCE_CURVE reverbCurve;

		pin_ptr<float> pinChannelAzimuths;
		pin_ptr<CurvePoint> pinVolumeCurve;
		pin_ptr<CurvePoint> pinLfeCurve;
		pin_ptr<CurvePoint> pinLpfDirectCurve;
		pin_ptr<CurvePoint> pinLpfReverbCurve;
		pin_ptr<CurvePoint> pinReverbCurve;

		if( emitter->Cone != nullptr )
		{
			cone2 = emitter->Cone->ToUnmanaged();
			nativeEmitter.pCone = &cone2;
		}
		else
			nativeEmitter.pCone = NULL;

		nativeEmitter.OrientFront = Vector3ToX3DAudio( emitter->OrientFront );
		nativeEmitter.OrientTop = Vector3ToX3DAudio( emitter->OrientTop );
		nativeEmitter.Position = Vector3ToX3DAudio( emitter->Position );
		nativeEmitter.Velocity = Vector3ToX3DAudio( emitter->Velocity );
		nativeEmitter.InnerRadius = emitter->InnerRadius;
		nativeEmitter.InnerRadiusAngle = emitter->InnerRadiusAngle;
		nativeEmitter.ChannelCount = emitter->ChannelCount;
		nativeEmitter.ChannelRadius = emitter->ChannelRadius;
		nativeEmitter.CurveDistanceScaler = emitter->CurveDistance;
		nativeEmitter.DopplerScaler = emitter->Doppler;

		if( emitter->ChannelAzimuths != nullptr )
		{
			pinChannelAzimuths = &emitter->ChannelAzimuths[0];
			nativeEmitter.pChannelAzimuths = reinterpret_cast<FLOAT32*>( pinChannelAzimuths );
		}
		else
			nativeEmitter.pChannelAzimuths = NULL;

		if( emitter->VolumeCurve != nullptr )
		{
			pinVolumeCurve = &emitter->VolumeCurve[0];
			volumeCurve.PointCount = emitter->VolumeCurve->Length;
			volumeCurve.pPoints = reinterpret_cast<X3DAUDIO_DISTANCE_CURVE_POINT*>( pinVolumeCurve );
			nativeEmitter.pVolumeCurve = &volumeCurve;
		}
		else
			nativeEmitter.pVolumeCurve = NULL;

		if( emitter->LfeCurve != nullptr )
		{
			pinLfeCurve = &emitter->LfeCurve[0];
			lfeCurve.PointCount = emitter->LfeCurve->Length;
			lfeCurve.pPoints = reinterpret_cast<X3DAUDIO_DISTANCE_CURVE_POINT*>( pinLfeCurve );
			nativeEmitter.pLFECurve = &lfeCurve;
		}
		else
			nativeEmitter.pLFECurve = NULL;

		if( emitter->LpfDirectCurve != nullptr )
		{
			pinLpfDirectCurve = &emitter->LpfDirectCurve[0];
			lpfDirectCurve.PointCount = emitter->LpfDirectCurve->Length;
			lpfDirectCurve.pPoints = reinterpret_cast<X3DAUDIO_DISTANCE_CURVE_POINT*>( pinLpfDirectCurve );
			nativeEmitter.pLPFDirectCurve = &lpfDirectCurve;
		}
		else
			nativeEmitter.pLPFDirectCurve = NULL;

		if( emitter->LpfReverbCurve != nullptr )
		{
			pinLpfReverbCurve = &emitter->LpfReverbCurve[0];
			lpfReverbCurve.PointCount = emitter->LpfReverbCurve->Length;
			lpfReverbCurve.pPoints = reinterpret_cast<X3DAUDIO_DISTANCE_CURVE_POINT*>( pinLpfReverbCurve );
			nativeEmitter.pLPFReverbCurve = &lpfReverbCurve;
		}
		else
			nativeEmitter.pLPFReverbCurve = NULL;

		if( emitter->ReverbCurve != nullptr )
		{
			pinReverbCurve = &emitter->ReverbCurve[0];
			reverbCurve.PointCount = emitter->ReverbCurve->Length;
			reverbCurve.pPoints = reinterpret_cast<X3DAUDIO_DISTANCE_CURVE_POINT*>( pinReverbCurve );
			nativeEmitter.pReverbCurve = &reverbCurve;
		}
		else
			nativeEmitter.pReverbCurve = NULL;

		array<float>^ delayTimes = gcnew array<float>( destinationChannelCount );
		array<float>^ matrix = gcnew array<float>( sourceChannelCount * destinationChannelCount );

		pin_ptr<float> pinDelayTimes = &delayTimes[0];
		pin_ptr<float> pinMatrix = &matrix[0];

		X3DAUDIO_DSP_SETTINGS nativeSettings;
		nativeSettings.SrcChannelCount = sourceChannelCount;
		nativeSettings.DstChannelCount = destinationChannelCount;
		nativeSettings.pDelayTimes = reinterpret_cast<FLOAT32*>( pinDelayTimes );
		nativeSettings.pMatrixCoefficients = reinterpret_cast<FLOAT32*>( pinMatrix );

		X3DAudioCalculate( handle->Handle, &nativeListener, &nativeEmitter, static_cast<UINT32>( flags ), &nativeSettings );

		DspSettings^ settings = gcnew DspSettings( nativeSettings );

		settings->MatrixCoefficients = matrix;
		settings->DelayTimes = delayTimes;

		return settings;
	}
}
}