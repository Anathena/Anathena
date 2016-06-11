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

#include <audiodefs.h>

namespace SlimDX
{
	namespace Multimedia
	{
		/// <summary>
		/// Represents possible usage identifiers for human interface devices.
		/// </summary>
		[System::Flags]
		public enum class UsageId : System::Int32
		{
			/// <summary>
			/// Represents a pointer device.
			/// </summary>
			Pointer = 0x01,

			/// <summary>
			/// Represents a mouse device.
			/// </summary>
			Mouse = 0x02,

			/// <summary>
			/// Represents a joystick device.
			/// </summary>
			Joystick = 0x04,

			/// <summary>
			/// Represents a gamepad device.
			/// </summary>
			Gamepad = 0x05,

			/// <summary>
			/// Represents a keyboard device.
			/// </summary>
			Keyboard = 0x06,

			/// <summary>
			/// Represents a keypad device.
			/// </summary>
			Keypad = 0x07,

			/// <summary>
			/// Represents a system control device.
			/// </summary>
			SystemControl = 0x80,

			/// <summary>
			/// Represents a device that provides X coordinates.
			/// </summary>
			X = 0x30,

			/// <summary>
			/// Represents a device that provides Y coordinates.
			/// </summary>
			Y = 0x31,

			/// <summary>
			/// Represents a device that provides Z coordinates.
			/// </summary>
			Z = 0x32,

			/// <summary>
			/// Represents a device that provides relative X coordinates.
			/// </summary>
			RelativeX = 0x33,

			/// <summary>
			/// Represents a device that provides relative Y coordinates.
			/// </summary>        
			RelativeY = 0x34,

			/// <summary>
			/// Represents a device that provides relative Z coordinates.
			/// </summary>
			RelativeZ = 0x35,

			/// <summary>
			/// Represents a device that provides a slider.
			/// </summary>
			Slider = 0x36,

			/// <summary>
			/// Represents a device that provides a dial.
			/// </summary>
			Dial = 0x37,

			/// <summary>
			/// Represents a wheel device.
			/// </summary>
			Wheel = 0x38,

			/// <summary>
			/// Represents a device that provides a hat switch.
			/// </summary>
			HatSwitch = 0x39,

			/// <summary>
			/// Represents a device that maintains a counted buffer.
			/// </summary>
			CountedBuffer = 0x3A,

			/// <summary>
			/// Represents a device that maintains a byte count.
			/// </summary>
			ByteCount = 0x3B,

			/// <summary>
			/// Represents a device that provides wakeup on motion support.
			/// </summary>
			MotionWakeup = 0x3C,

			/// <summary>
			/// Represents a device that provides VX controls.
			/// </summary>
			VX = 0x40,

			/// <summary>
			/// Represents a device that provides VY controls.
			/// </summary>
			VY = 0x41,

			/// <summary>
			/// Represents a device that provides VZ controls.
			/// </summary>
			VZ = 0x42,

			/// <summary>
			/// Represents a device that provides VBRX controls.
			/// </summary>
			VbrX = 0x43,

			/// <summary>
			/// Represents a device that provides VBRY controls.
			/// </summary>
			VbrY = 0x44,

			/// <summary>
			/// Represents a device that provides VBRZ controls.
			/// </summary>
			VbrZ = 0x45,

			/// <summary>
			/// Represents a device that provides VNO controls.
			/// </summary>
			Vno = 0x46,

			/// <summary>
			/// Represents a device that controls system power.
			/// </summary>
			SystemControlPower = 0x81,

			/// <summary>
			/// Represents a device that controls system sleep.
			/// </summary>
			SystemControlSleep = 0x82,

			/// <summary>
			/// Represents a device that controls system wake.
			/// </summary>
			SystemControlWake = 0x83,

			/// <summary>
			/// Represents a device that controls system context menus.
			/// </summary>
			SystemControlContextMenu = 0x84,

			/// <summary>
			/// Represents a device that controls system menus.
			/// </summary>
			SystemControlMainMenu = 0x85,

			/// <summary>
			/// Represents a device that controls system application menus.
			/// </summary>
			SystemControlApplicationMenu = 0x86,

			/// <summary>
			/// Represents a device that controls system help menus.
			/// </summary>
			SystemControlHelpMenu = 0x87,

			/// <summary>
			/// Represents a device that controls menu exit.
			/// </summary>
			SystemControlMenuExit = 0x88,

			/// <summary>
			/// Represents a device that controls menu select.
			/// </summary>
			SystemControlMenuSelect = 0x89,

			/// <summary>
			/// Represents a device that controls menu right.
			/// </summary>
			SystemControlMenuRight = 0x8A,

			/// <summary>
			/// Represents a device that controls menu left.
			/// </summary>
			SystemControlMenuLeft = 0x8B,

			/// <summary>
			/// Represents a device that controls menu up.
			/// </summary>
			SystemControlMenuUp = 0x8C,

			/// <summary>
			/// Represents a device that controls menu down.
			/// </summary>
			SystemControlMenuDown = 0x8D,

			/// <summary>
			/// Represents a device that provides keyboard events.
			/// </summary>
			KeyboardNoEvent = 0x00,

			/// <summary>
			/// Represents a device that provides keyboard rollover events.
			/// </summary>
			KeyboardRollover = 0x01,

			/// <summary>
			/// Represents a device that provides keyboard post fail events.
			/// </summary>
			KeyboardPostFail = 0x02,

			/// <summary>
			/// Represents a device that provides keyboard undefined events.
			/// </summary>
			KeyboardUndefined = 0x03,

			/// <summary>
			/// Represents a device that provides keyboard alpha events.
			/// </summary>
			KeyboardaA = 0x04,

			/// <summary>
			/// Represents a device that provides keyboard alpha events.
			/// </summary>
			KeyboardzZ = 0x1D,

			/// <summary>
			/// Represents a generic keyboard device.
			/// </summary>
			Keyboard1 = 0x1E,

			/// <summary>
			/// Represents a generic keyboard device.
			/// </summary>
			Keyboard0 = 0x27,

			/// <summary>
			/// Represents a keyboard that provides a left control key.
			/// </summary>
			KeyboardLeftControl = 0xE0,

			/// <summary>
			/// Represents a keyboard that provides a left shift key.
			/// </summary>
			KeyboardLeftShift = 0xE1,

			/// <summary>
			/// Represents a keyboard that provides a left alt key.
			/// </summary>
			KeyboardLeftAlt = 0xE2,

			/// <summary>
			/// Represents a keyboard that provides a left GUI key.
			/// </summary>
			KeyboardLeftGui = 0xE3,

			/// <summary>
			/// Represents a keyboard that provides a right control key.
			/// </summary>
			KeyboardRightControl = 0xE4,

			/// <summary>
			/// Represents a keyboard that provides a right shift key.
			/// </summary>
			KeyboardRightShift = 0xE5,

			/// <summary>
			/// Represents a keyboard that provides a right alt key.
			/// </summary>
			KeyboardRightAlt = 0xE6,

			/// <summary>
			/// Represents a keyboard that provides a right GUI key.
			/// </summary>
			KeyboardRightGui = 0xE7,

			/// <summary>
			/// Represents a keyboard that provides a scroll lock key.
			/// </summary>
			KeyboardScrollLock = 0x47,

			/// <summary>
			/// Represents a keyboard that provides a number lock key.
			/// </summary>
			KeyboardNumLock = 0x53,

			/// <summary>
			/// Represents a keyboard that provides a caps lock key.
			/// </summary>
			KeyboardCapsLock = 0x39,

			/// <summary>
			/// Represents a keyboard that provides an F1 key.
			/// </summary>
			KeyboardF1 = 0x3A,

			/// <summary>
			/// Represents a keyboard that provides an F12 key.
			/// </summary>
			KeyboardF12 = 0x45,

			/// <summary>
			/// Represents a keyboard that provides a return key.
			/// </summary>
			KeyboardReturn = 0x28,

			/// <summary>
			/// Represents a keyboard that provides an escape key.
			/// </summary>
			KeyboardEscape = 0x29,

			/// <summary>
			/// Represents a keyboard that provides a delete key.
			/// </summary>
			KeyboardDelete = 0x2A,

			/// <summary>
			/// Represents a keyboard that provides a print screen key.
			/// </summary>
			KeyboardPrintScreen = 0x46,

			/// <summary>
			/// Represents a number lock LED.
			/// </summary>
			LedNumLock = 0x01,

			/// <summary>
			/// Represents a number lock LED.
			/// </summary>
			LedCapsLock = 0x02,

			/// <summary>
			/// Represents a scroll lock LED.
			/// </summary>
			LedScrollLock = 0x03,

			/// <summary>
			/// Represents a compose LED.
			/// </summary>
			LedCompose = 0x04,

			/// <summary>
			/// Represents a kana LED.
			/// </summary>
			LedKana = 0x05,

			/// <summary>
			/// Represents a power LED.
			/// </summary>
			LedPower = 0x06,

			/// <summary>
			/// Represents a shift LED.
			/// </summary>
			LedShift = 0x07,

			/// <summary>
			/// Represents a do not disturb LED.
			/// </summary>
			LedDoNotDisturb = 0x08,

			/// <summary>
			/// Represents a mute LED.
			/// </summary>
			LedMute = 0x09,

			/// <summary>
			/// Represents a tone enable LED.
			/// </summary>
			LedToneEnable = 0x0A,

			/// <summary>
			/// Represents a high cut filter LED.
			/// </summary>
			LedHighCutFilter = 0x0B,

			/// <summary>
			/// Represents a low cut filter LED.
			/// </summary>
			LedLowCutFilter = 0x0C,

			/// <summary>
			/// Represents an equalizer enable LED.
			/// </summary>
			LedEqualizerEnable = 0x0D,

			/// <summary>
			/// Represents a sound field on LED.
			/// </summary>
			LedSoundFieldOn = 0x0E,

			/// <summary>
			/// Represents a surround field on LED.
			/// </summary>
			LedSurroundFieldOn = 0x0F,

			/// <summary>
			/// Represents a repeat LED.
			/// </summary>
			LedRepeat = 0x10,

			/// <summary>
			/// Represents a stereo LED.
			/// </summary>
			LedStereo = 0x11,

			/// <summary>
			/// Represents a direct sampling rate LED.
			/// </summary>
			LedSamplingRateDirect = 0x12,

			/// <summary>
			/// Represents a spinning LED.
			/// </summary>
			LedSpinning = 0x13,

			/// <summary>
			/// Represents a CAV LED.
			/// </summary>
			LedCav = 0x14,

			/// <summary>
			/// Represents a CLV LED.
			/// </summary>
			LedClv = 0x15,

			/// <summary>
			/// Represents a recording format LED.
			/// </summary>
			LedRecordingFormatDet = 0x16,

			/// <summary>
			/// Represents an off-the-hook LED.
			/// </summary>
			LedOffHook = 0x17,

			/// <summary>
			/// Represents a ring LED.
			/// </summary>
			LedRing = 0x18,

			/// <summary>
			/// Represents a message waiting LED.
			/// </summary>
			LedMessageWaiting = 0x19,

			/// <summary>
			/// Represents a data mode LED.
			/// </summary>
			LedDataMode = 0x1A,

			/// <summary>
			/// Represents a battery operation LED.
			/// </summary>
			LedBatteryOperation = 0x1B,

			/// <summary>
			/// Represents a battery OK LED.
			/// </summary>
			LedBatteryOk = 0x1C,

			/// <summary>
			/// Represents a battery low LED.
			/// </summary>
			LedBatteryLow = 0x1D,

			/// <summary>
			/// Represents a speaker LED.
			/// </summary>
			LedSpeaker = 0x1E,

			/// <summary>
			/// Represents a headset LED.
			/// </summary>
			LedHeadset = 0x1F,

			/// <summary>
			/// Represents a hold LED.
			/// </summary>
			LedHold = 0x20,

			/// <summary>
			/// Represents a microphone LED.
			/// </summary>
			LedMicrophone = 0x21,

			/// <summary>
			/// Represents a coverage LED.
			/// </summary>
			LedCoverage = 0x22,

			/// <summary>
			/// Represents a night mode LED.
			/// </summary>
			LedNightMode = 0x23,

			/// <summary>
			/// Represents a send calls LED.
			/// </summary>
			LedSendCalls = 0x24,

			/// <summary>
			/// Represents a call pickup LED.
			/// </summary>
			LedCallPickup = 0x25,

			/// <summary>
			/// Represents a conference LED.
			/// </summary>
			LedConference = 0x26,

			/// <summary>
			/// Represents a standby LED.
			/// </summary>
			LedStandby = 0x27,

			/// <summary>
			/// Represents a camera on LED.
			/// </summary>
			LedCameraOn = 0x28,

			/// <summary>
			/// Represents a camera off LED.
			/// </summary>
			LedCameraOff = 0x29,

			/// <summary>
			/// Represents an onlineLED.
			/// </summary>        
			LedOnline = 0x2A,

			/// <summary>
			/// Represents an offline LED.
			/// </summary>
			LedOffline = 0x2B,

			/// <summary>
			/// Represents a busy LED.
			/// </summary>
			LedBusy = 0x2C,

			/// <summary>
			/// Represents a ready LED.
			/// </summary>
			LedReady = 0x2D,

			/// <summary>
			/// Represents a paper out LED.
			/// </summary>
			LedPaperOut = 0x2E,

			/// <summary>
			/// Represents a paper jam LED.
			/// </summary>
			LedPaperJam = 0x2F,

			/// <summary>
			/// Represents a remote LED.
			/// </summary>
			LedRemote = 0x30,

			/// <summary>
			/// Represents a forward LED.
			/// </summary>
			LedForward = 0x31,

			/// <summary>
			/// Represents a reverse LED.
			/// </summary>
			LedReverse = 0x32,

			/// <summary>
			/// Represents a stop LED.
			/// </summary>
			LedStop = 0x33,

			/// <summary>
			/// Represents a rewind LED.
			/// </summary>
			LedRewind = 0x34,

			/// <summary>
			/// Represents a fast forward LED.
			/// </summary>
			LedFastForward = 0x35,

			/// <summary>
			/// Represents a play LED.
			/// </summary>
			LedPlay = 0x36,

			/// <summary>
			/// Represents a pause LED.
			/// </summary>
			LedPause = 0x37,

			/// <summary>
			/// Represents a record LED.
			/// </summary>
			LedRecord = 0x38,

			/// <summary>
			/// Represents an error LED.
			/// </summary>
			LedError = 0x39,

			/// <summary>
			/// Represents a selected indicator LED.
			/// </summary>
			LedSelectedIndicator = 0x3A,

			/// <summary>
			/// Represents an in-use indicator LED.
			/// </summary>
			LedInUseIndicator = 0x3B,

			/// <summary>
			/// Represents a nmultimode indicator LED.
			/// </summary>
			LedMultimodeIndicator = 0x3C,

			/// <summary>
			/// Represents an indicator on LED.
			/// </summary>
			LedIndicatorOn = 0x3D,

			/// <summary>
			/// Represents an indicator flash LED.
			/// </summary>
			LedIndicatorFlash = 0x3E,

			/// <summary>
			/// Represents an indicator slow blink LED.
			/// </summary>
			LedIndicatorSlowBlink = 0x3F,

			/// <summary>
			/// Represents an indicator fast blink LED.
			/// </summary>
			LedIndicatorFastBlink = 0x40,

			/// <summary>
			/// Represents an indicator off LED.
			/// </summary>
			LedIndicatorOff = 0x41,

			/// <summary>
			/// Represents a flash-on-time LED.
			/// </summary>
			LedFlashOnTime = 0x42,

			/// <summary>
			/// Represents a slow-blink-on-time LED.
			/// </summary>
			LedSlowBlinkOnTime = 0x43,

			/// <summary>
			/// Represents a slow-blink-off-time LED.
			/// </summary>
			LedSlowBlinkOffTime = 0x44,

			/// <summary>
			/// Represents a fast-blink-on-time LED.
			/// </summary>
			LedFastBlinkOnTime = 0x45,

			/// <summary>
			/// Represents a fast-blink-off-time LED.
			/// </summary>
			LedFastBlinkOffTime = 0x46,

			/// <summary>
			/// Represents an indicator color LED.
			/// </summary>
			LedIndicatorColor = 0x47,

			/// <summary>
			/// Represents a red LED.
			/// </summary>
			LedRed = 0x48,

			/// <summary>
			/// Represents a green LED.
			/// </summary>
			LedGreen = 0x49,

			/// <summary>
			/// Represents an amber LED.
			/// </summary>
			LedAmber = 0x4A,

			/// <summary>
			/// Represents a generic indicator LED.
			/// </summary>
			LedGenericIndicator = 0x3B,

			/// <summary>
			/// Represents a phone telephony device.
			/// </summary>
			TelephonyPhone = 0x01,

			/// <summary>
			/// Represents an answering machine telephony device.
			/// </summary>
			TelephonyAnsweringMachine = 0x02,

			/// <summary>
			/// Represents message controls for a telephony device.
			/// </summary>
			TelephonyMessageControls = 0x03,

			/// <summary>
			/// Represents a handset telephony device.
			/// </summary>
			TelephonyHandset = 0x04,

			/// <summary>
			/// Represents a headset telephony device.
			/// </summary>
			TelephonyHeadset = 0x05,

			/// <summary>
			/// Represents a keypad telephony device.
			/// </summary>
			TelephonyKeypad = 0x06,

			/// <summary>
			/// Represents programmable buttons for a telephony device.
			/// </summary>
			TelephonyProgrammableButton = 0x07,

			/// <summary>
			/// Represents a rudder for a simulation device.
			/// </summary>
			SimulationRudder = 0xBA,

			/// <summary>
			/// Represents a throttle for a simulation device.
			/// </summary>
			SimulationThrottle = 0xBB
		};

		/// <summary>
		/// Represents possible usage page identifiers for human interface devices.
		/// </summary>
		public enum class UsagePage : System::Int32
		{
			/// <summary>
			/// Represents undefined devices.
			/// </summary>
			Undefined = 0x00,

			/// <summary>
			/// Represents generic desktop controls.
			/// </summary>
			Generic = 0x01,

			/// <summary>
			/// Represents simulation controls.
			/// </summary>
			Simulation = 0x02,

			/// <summary>
			/// Represents virtual reality controls.
			/// </summary>
			VR = 0x03,

			/// <summary>
			/// Represents sports controls.
			/// </summary>
			Sport = 0x04,

			/// <summary>
			/// Represents games controls.
			/// </summary>
			Game = 0x05,

			/// <summary>
			/// Represents keyboard controls.
			/// </summary>
			Keyboard = 0x07,

			/// <summary>
			/// Represents LED controls.
			/// </summary>
			Led = 0x08,

			/// <summary>
			/// Represents button controls.
			/// </summary>
			Button = 0x09,

			/// <summary>
			/// Represents ordinal controls.
			/// </summary>
			Ordinal = 0x0A,

			/// <summary>
			/// Represents telephony controls.
			/// </summary>
			Telephony = 0x0B,

			/// <summary>
			/// Represents consumer controls.
			/// </summary>
			Consumer = 0x0C,

			/// <summary>
			/// Represents digitizer controls.
			/// </summary>
			Digitizer = 0x0D,

			/// <summary>
			/// Represents physical interface devices.
			/// </summary>
			Pid = 0x0F,

			/// <summary>
			/// Represents Unicode devices.
			/// </summary>
			Unicode = 0x10,

			/// <summary>
			/// Represents alphanumeric displays.
			/// </summary>
			Alphanumeric = 0x14,

			/// <summary>
			/// Represents medical instruments.
			/// </summary>
			Medical = 0x40,

			/// <summary>
			/// Represents monitor page 0.
			/// </summary>
			MonitorPage0 = 0x80,

			/// <summary>
			/// Represents monitor page 1.
			/// </summary>
			MonitorPage1 = 0x81,

			/// <summary>
			/// Represents monitor page 2.
			/// </summary>
			MonitorPage2 = 0x82,

			/// <summary>
			/// Represents monitor page 3.
			/// </summary>
			MonitorPage3 = 0x83,

			/// <summary>
			/// Represents power page 0.
			/// </summary>
			PowerPage0 = 0x84,

			/// <summary>
			/// Represents power page 1.
			/// </summary>
			PowerPage1 = 0x85,

			/// <summary>
			/// Represents power page 2.
			/// </summary>
			PowerPage2 = 0x86,

			/// <summary>
			/// Represents power page 3.
			/// </summary>
			PowerPage3 = 0x87,

			/// <summary>
			/// Represents bar code scanners.
			/// </summary>
			Barcode = 0x8C,

			/// <summary>
			/// Represents scale pages.
			/// </summary>
			Scale = 0x8D,

			/// <summary>
			/// Represents magnetic strip reading devices.
			/// </summary>
			Msr = 0x8E
		};

		/// <summary>
		/// Specifies possible speaker combinations for audio devices.
		/// </summary>
		[System::Flags]
		public enum class Speakers : System::Int32
		{
			None = 0,

			/// <summary>
			/// A front left speaker will be used.
			/// </summary>
			FrontLeft = SPEAKER_FRONT_LEFT,

			/// <summary>
			/// A front right speaker will be used.
			/// </summary>
			FrontRight = SPEAKER_FRONT_RIGHT,

			/// <summary>
			/// A front center speaker will be used.
			/// </summary>
			FrontCenter = SPEAKER_FRONT_CENTER,

			/// <summary>
			/// A low frequency speaker will be used.
			/// </summary>
			LowFrequency = SPEAKER_LOW_FREQUENCY,

			/// <summary>
			/// A back left speaker will be used.
			/// </summary>
			BackLeft = SPEAKER_BACK_LEFT,

			/// <summary>
			/// A back right speaker will be used.
			/// </summary>
			BackRight = SPEAKER_BACK_RIGHT,

			/// <summary>
			/// A front left-of-center speaker will be used.
			/// </summary>
			FrontLeftOfCenter = SPEAKER_FRONT_LEFT_OF_CENTER,

			/// <summary>
			/// A front right-of-center speaker will be used.
			/// </summary>
			FrontRightOfCenter = SPEAKER_FRONT_RIGHT_OF_CENTER,

			/// <summary>
			/// A back center speaker will be used.
			/// </summary>
			BackCenter = SPEAKER_BACK_CENTER,

			/// <summary>
			/// A left side speaker will be used.
			/// </summary>
			SideLeft = SPEAKER_SIDE_LEFT,

			/// <summary>
			/// A right side speaker will be used.
			/// </summary>
			SideRight = SPEAKER_SIDE_RIGHT,

			/// <summary>
			/// A top center speaker will be used.
			/// </summary>
			TopCenter = SPEAKER_TOP_CENTER,

			/// <summary>
			/// A top front left speaker will be used.
			/// </summary>
			TopFrontLeft = SPEAKER_TOP_FRONT_LEFT,

			/// <summary>
			/// A top front center speaker will be used.
			/// </summary>
			TopFrontCenter = SPEAKER_TOP_FRONT_CENTER,

			/// <summary>
			/// A top front right speaker will be used.
			/// </summary>
			TopFrontRight = SPEAKER_TOP_FRONT_RIGHT,

			/// <summary>
			/// A top back left speaker will be used.
			/// </summary>
			TopBackLeft = SPEAKER_TOP_BACK_LEFT,

			/// <summary>
			/// A top back center speaker will be used.
			/// </summary>
			TopBackCenter = SPEAKER_TOP_BACK_CENTER,

			/// <summary>
			/// A top back right speaker will be used.
			/// </summary>
			TopBackRight = SPEAKER_TOP_BACK_RIGHT,

			/// <summary>
			/// A reserved speaker will be used.
			/// </summary>
			Reserved = SPEAKER_RESERVED,

	#pragma warning(disable: 4369)
			/// <summary>
			/// All possible speakers will be used.
			/// </summary>
			All = SPEAKER_ALL,
	#pragma warning(default: 4369)

			/// <summary>
			/// A single speaker will be used.
			/// </summary>
			Mono = SPEAKER_MONO,

			/// <summary>
			/// Two or more speakers will be used.
			/// </summary>
			Stereo = SPEAKER_STEREO,

			/// <summary>
			/// A combination of speakers will be used that provides 2.1 sound output.
			/// </summary>
			TwoPointOne = SPEAKER_2POINT1,

			/// <summary>
			/// A combination of speakers will be used that provides surround sound output.
			/// </summary>
			Surround = SPEAKER_SURROUND,

			/// <summary>
			/// A combination of speakers will be used that provides quadrophonic sound output.
			/// </summary>
			Quadrophonic = SPEAKER_QUAD,

			/// <summary>
			/// A combination of speakers will be used that provides 4.1 sound output.
			/// </summary>
			FourPointOne = SPEAKER_4POINT1,

			/// <summary>
			/// A combination of speakers will be used that provides 5.1 sound output.
			/// </summary>
			FivePointOne = SPEAKER_5POINT1,

			/// <summary>
			/// A combination of speakers will be used that provides 7.1 sound output.
			/// </summary>
			SevenPointOne = SPEAKER_7POINT1,

			/// <summary>
			/// A combination of speakers will be used that provides 5.1 surround sound output.
			/// </summary>
			FivePointOneSurround = SPEAKER_5POINT1_SURROUND,

			/// <summary>
			/// A combination of speakers will be used that provides 7.1 surround sound output.
			/// </summary>
			SevenPointOneSurround = SPEAKER_7POINT1_SURROUND,
		};

	#pragma warning(disable: 4369)
		public enum class WaveFormatTag : System::Int32
		{
			Pcm = WAVE_FORMAT_PCM,
			AdPcm = WAVE_FORMAT_ADPCM,
			IeeeFloat = WAVE_FORMAT_IEEE_FLOAT,
			MpegLayer3 = WAVE_FORMAT_MPEGLAYER3,
			DolbyAC3Spdif = WAVE_FORMAT_DOLBY_AC3_SPDIF,
			WMAudio2 = WAVE_FORMAT_WMAUDIO2,
			WMAudio3 = WAVE_FORMAT_WMAUDIO3,
			WmaSpdif = WAVE_FORMAT_WMASPDIF,
			Extensible = WAVE_FORMAT_EXTENSIBLE,
		};
	#pragma warning(default: 4369)
	}
}