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
	namespace DirectSound
	{
		/// <summary>
		/// The CaptureEffectAcousticEchoCancel object is used to set and retrieve parameters on a capture buffer that supports acoustic echo cancellation. This object requires Microsoft® Windows® XP or later operating systems.
		/// </summary>
		/// <unmanaged>IDirectSoundCaptureFXAec</unmanaged>
		public ref class AcousticEchoCancel : public ComObject
		{
			COMOBJECT(IDirectSoundCaptureFXAec, AcousticEchoCancel);

		public:
			/// <summary>
			/// Sets the enumerator to its initial position, which is before the first element in the collection.
			/// </summary>
			Result Reset();

			/// <summary>
			/// Boolean value that specifies whether the effect is enabled.
			/// </summary>
			property bool Enabled
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Boolean value that specifies whether to enable background comfort noise, which makes the capture signal sound more natural by preventing periods of dead silence. By default, background comfort noise is not enabled.
			/// </summary>
			property bool NoiseFill
			{
				bool get();
				void set( bool value );
			}

			/// <summary>
			/// Operation mode.
			/// </summary>
			property AcousticEchoCancelMode Mode
			{
				AcousticEchoCancelMode get();
				void set( AcousticEchoCancelMode value );
			}

			/// <summary>
			/// True if the AEC algorithm is in the uninitialized state.
			/// </summary>
			property bool Uninitialized
			{
				bool get();
			}

			/// <summary>
			/// True if the AEC algorithm is currently converged.
			/// </summary>
			property bool CurrentlyConverged
			{
				bool get();
			}

			/// <summary>
			/// True if the AEC algorithm has converged.
			/// </summary>
			property bool ContinuouslyConverged
			{
				bool get();
			}

			/// <summary>
			/// True if the AEC algorithm ever loses convergence.
			/// </summary>
			property bool PreviouslyDiverged
			{
				bool get();
			}
		};
	}
}