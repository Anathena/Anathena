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
		/// Contains system-defined identifiers for DirectSound default playback and capture devices.
		/// </summary>
		/// <unmanaged>None</unmanaged>
		public ref class DirectSoundGuid sealed
		{
		private: 
			DirectSoundGuid() {}

		public:
			/// <summary>
			/// All objects.
			/// </summary>
			static property System::Guid AllObjects
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_All_Objects ); }
			}

			/// <summary>
			/// Default audio capture device.
			/// </summary>
			static property System::Guid DefaultCaptureDevice
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( DSDEVID_DefaultCapture ); }
			}

			/// <summary>
			/// Default audio playback device.
			/// </summary>
			static property System::Guid DefaultPlaybackDevice
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( DSDEVID_DefaultPlayback ); }
			}

			/// <summary>
			/// Voice capture device.
			/// </summary>
			static property System::Guid DefaultVoiceCaptureDevice
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( DSDEVID_DefaultVoiceCapture ); }
			}

			/// <summary>
			/// Voice playback device.
			/// </summary>
			static property System::Guid DefaultVoicePlaybackDevice
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( DSDEVID_DefaultVoicePlayback ); }
			}
		};

		/// <summary>
		/// Contains system-defined identifiers for DirectSound capture effects.
		/// </summary>
		/// <unmanaged>None</unmanaged>
		public ref class CaptureEffectGuid sealed
		{
		private:
			CaptureEffectGuid() {}

		public:
			/// <summary>
			/// Acoustic echo cancellation capture effect.
			/// </summary>
			static property System::Guid AcousticEchoCancellationCaptureEffect
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSCFX_CLASS_AEC ); }
			}

			/// <summary>
			/// Noise suppression capture effect
			/// </summary>
			static property System::Guid NoiseSuppressionCaptureEffect
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSCFX_CLASS_NS ); }
			}

			/// <summary>
			/// Microsoft acoustic echo cancellation capture effect
			/// </summary>
			static property System::Guid MicrosoftAcousticEchoCancellationCaptureEffect
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSCFX_MS_AEC ); }
			}

			/// <summary>
			/// Microsoft noise suppression capture effect
			/// </summary>
			static property System::Guid MicrosoftNoiseSuppressionCaptureEffect
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSCFX_MS_NS ); }
			}

			/// <summary>
			/// System default acoustic echo cancellation capture effect
			/// </summary>
			static property System::Guid SystemAcousticEchoCancellationCaptureEffect
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSCFX_SYSTEM_AEC ); }
			}

			/// <summary>
			/// System default noise suppression capture effect
			/// </summary>
			static property System::Guid SystemNoiseSuppressionCaptureEffect
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSCFX_SYSTEM_NS ); }
			}
		};

		/// <summary>
		/// Contains system-defined identifiers for DirectSound sound effects.
		/// </summary>
		/// <unmanaged>None</unmanaged>
		public ref class SoundEffectGuid sealed 
		{
		private:
			SoundEffectGuid() {}

		public:
			/// <summary>
			/// Standard Chorus effect
			/// </summary>
			static property System::Guid StandardChorus
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_STANDARD_CHORUS ); } 
			}

			/// <summary>
			/// Standard Compressor effect
			/// </summary>
			static property System::Guid StandardCompressor
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_STANDARD_COMPRESSOR ); }
			}

			/// <summary>
			/// Standard Distortion effect
			/// </summary>
			static property System::Guid StandardDistortion
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_STANDARD_DISTORTION ); }
			}

			/// <summary>
			/// Standard Echo effect
			/// </summary>
			static property System::Guid StandardEcho
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_STANDARD_ECHO ); }
			}

			/// <summary>
			/// Standard Flanger effect
			/// </summary>
			static property System::Guid StandardFlanger
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_STANDARD_FLANGER ); }
			}

			/// <summary>
			/// Standard Gargle effect
			/// </summary>
			static property System::Guid StandardGargle
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_STANDARD_GARGLE ); }
			}

			/// <summary>
			/// Standard Environmental reverberation effect
			/// </summary>
			static property System::Guid StandardInteractive3DLevel2Reverb
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_STANDARD_I3DL2REVERB ); }
			}

			/// <summary>
			/// Standard Parametric equalizer effect
			/// </summary>
			static property System::Guid StandardParametricEqualizer
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_STANDARD_PARAMEQ ); }
			}

			/// <summary>
			/// Waves reverberation effect 
			/// </summary>
			static property System::Guid WavesReverb
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( GUID_DSFX_WAVES_REVERB ); }
			}
		};

		/// <summary>
		/// Contains system-defined identifiers for DirectSound 3-D Algorithms.
		/// </summary>
		/// <unmanaged>None</unmanaged>
		public ref class DirectSound3DAlgorithmGuid sealed
		{
		private:
			DirectSound3DAlgorithmGuid() {}

		public:
			/// <summary>
			/// Default 3-D algorithm
			/// </summary>
			static property System::Guid Default3DAlgorithm
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( DS3DALG_DEFAULT ); }
			}

			/// <summary>
			/// The 3-D API is processed with the high quality 3-D audio algorithm. This algorithm gives the highest quality 3-D audio effect, but uses more CPU cycles.
			/// </summary>
			static property System::Guid FullHrt3DAlgorithm
			{ 
				System::Guid get() { return Utilities::ConvertNativeGuid( DS3DALG_HRTF_FULL ); } 
			}

			/// <summary>
			/// The 3-D API is processed with the efficient 3-D audio algorithm. This algorithm gives a good 3-D audio effect, but uses fewer CPU cycles than Guid3DAlgorithmHrtfFull
			/// </summary>
			static property System::Guid LightHrt3DAlgorithm
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( DS3DALG_HRTF_LIGHT ); }
			}

			/// <summary>
			/// 3-D output is mapped into normal left and right stereo panning. At 90 degrees to the left, the sound is coming out of only the left speaker; at 90 degrees to the right, sound is coming out of only the right speaker. The vertical axis is ignored except for scaling of volume due to distance. Doppler shift and volume scaling are still applied, but the 3-D filtering is not performed on this buffer. This is the most efficient software implementation, but provides no virtual 3-D audio effect.
			/// </summary>
			static property System::Guid NoVirtualization3DAlgorithm
			{
				System::Guid get() { return Utilities::ConvertNativeGuid( DS3DALG_NO_VIRTUALIZATION ); }
			}
		};
	}
}
