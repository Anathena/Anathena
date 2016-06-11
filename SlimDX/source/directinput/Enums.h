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
	namespace DirectInput
	{
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.

		/// <summary>
		/// Specifies the cooperative level for a DirectInput device.
		/// </summary>
		/// <unmanaged>DISCL</unmanaged>
		[System::Flags]
		public enum class CooperativeLevel : System::Int32
		{
			/// <summary>
			/// Exclusive access to the device.
			/// </summary>
			Exclusive = DISCL_EXCLUSIVE,

			/// <summary>
			/// Non exclusive access to the device.
			/// </summary>
			Nonexclusive = DISCL_NONEXCLUSIVE,

			/// <summary>
			/// Foreground operation of the device.
			/// </summary>
			Foreground = DISCL_FOREGROUND,

			/// <summary>
			/// Background operation of the device.
			/// </summary>
			Background = DISCL_BACKGROUND,

			/// <summary>
			/// Disables the Windows logo key.
			/// </summary>
			NoWinKey = DISCL_NOWINKEY,
		};

		/// <summary>
		/// Specifies other attributes of a data format.
		/// </summary>
		/// <unmanaged>DIDF</unmanaged>
		public enum class DataFormatFlag : System::Int32
		{
			/// <summary>
			/// The axes are in absolute mode.
			/// </summary>
			AbsoluteAxis = DIDF_ABSAXIS,

			/// <summary>
			/// The axes are in relative mode.
			/// </summary>
			RelativeAxis = DIDF_RELAXIS
		};

		/// <summary>
		/// Specifies the axis mode for the device.
		/// </summary>
		/// <unmanaged>DIPROPAXISMODE</unmanaged>
		public enum class DeviceAxisMode : System::Int32
		{
			/// <summary>
			/// The axis works with absolute data.
			/// </summary>
			Absolute = DIPROPAXISMODE_ABS,

			/// <summary>
			/// The axis works with relative data.
			/// </summary>
			Relative = DIPROPAXISMODE_REL
		};

		/// <unmanaged>DI8DEVCLASS</unmanaged>
		public enum class DeviceClass : System::Int32
		{
			All = DI8DEVCLASS_ALL,
			Device = DI8DEVCLASS_DEVICE,
			GameController = DI8DEVCLASS_GAMECTRL,
			Keyboard = DI8DEVCLASS_KEYBOARD,
			Pointer = DI8DEVCLASS_POINTER
		};

		/// <summary>
		/// The data format for a DirectInput device.
		/// </summary>
		public enum class DeviceDataFormat : System::Int32
		{
			/// <summary>
			/// Keyboard data.
			/// </summary>
			Keyboard,

			/// <summary>
			/// Mouse data.
			/// </summary>
			Mouse,

			/// <summary>
			/// Joystick data.
			/// </summary>
			Joystick
		};

		/// <unmanaged>DIEDFL</unmanaged>
		[System::Flags]
		public enum class DeviceEnumerationFlags : System::Int32
		{
			AllDevices = DIEDFL_ALLDEVICES,
			AttachedOnly = DIEDFL_ATTACHEDONLY,
			ForceFeedback = DIEDFL_FORCEFEEDBACK,
			IncludeAliases = DIEDFL_INCLUDEALIASES,
			IncludeHidden = DIEDFL_INCLUDEHIDDEN,
			IncludePhantoms = DIEDFL_INCLUDEPHANTOMS
		};

		/// <summary>
		/// Specifies the flags that can be associated with a DirectInput device.
		/// </summary>
		/// <unmanaged>DIDC</unmanaged>
		[System::Flags]
		public enum class DeviceFlags : System::Int32
		{
			/// <summary>
			/// The device is a duplicate of another DirectInput device.
			/// </summary>
			Alias = DIDC_ALIAS,

			/// <summary>
			/// The device is physically attached to the user's computer.
			/// </summary>
			Attached = DIDC_ATTACHED,

			/// <summary>
			/// The device supports deadband for at least one force-feedback condition.
			/// </summary>
			DeadBand = DIDC_DEADBAND,

			/// <summary>
			/// Specifies that the device's data is coming from a user mode device interface.
			/// </summary>
			Emulated = DIDC_EMULATED,

			/// <summary>
			/// The device supports force-feedback.
			/// </summary>
			ForceFeedback = DIDC_FORCEFEEDBACK,

			/// <summary>
			/// The force-feedback system supports the fade parameter for at least one effect.
			/// </summary>
			ForceFeedbackFade = DIDC_FFFADE,

			/// <summary>
			/// The force-feedback system supports the attack parameter for at least one effect.
			/// </summary>
			ForceFeedbackAttack = DIDC_FFATTACK,

			/// <summary>
			/// The device is a fictious device created by a device driver so that is can generate
			/// mouse and keyboard events.
			/// </summary>
			Hidden = DIDC_HIDDEN,

			/// <summary>
			/// The device is a placeholder.
			/// </summary>
			Phantom = DIDC_PHANTOM,

			/// <summary>
			/// At least one object in the current data format is polled, rather than interrupt driven.
			/// </summary>
			PolledDataFormat = DIDC_POLLEDDATAFORMAT,

			/// <summary>
			/// At least one object on the device is polled, rather than interrupt driven.
			/// </summary>
			PolledDevice = DIDC_POLLEDDEVICE,

			/// <summary>
			/// The force-feedback system supports two coefficient values for conditions.
			/// </summary>
			PositiveNegativeCoefficients = DIDC_POSNEGCOEFFICIENTS,

			/// <summary>
			/// The force-feedback system supports a maximum saturation for both positive and negative
			/// force output for at least one condition.
			/// </summary>
			PositiveNegativeSaturation = DIDC_POSNEGSATURATION,

			/// <summary>
			/// The force-feedback system supports the saturation of condition effects for at least
			/// one condition.
			/// </summary>
			Saturation = DIDC_SATURATION,

			/// <summary>
			/// The force-feedback system supports the start delay parameter for at least one effect.
			/// </summary>
			StartDelay = DIDC_STARTDELAY
		};

		/// <summary>
		/// Specifies the main type of a DirectInput device.
		/// </summary>
		/// <unmanaged>DI8DEVTYPE</unmanaged>
		public enum class DeviceType : System::Int32
		{
			/// <summary>
			/// A first-person action game device.
			/// </summary>
			FirstPerson = DI8DEVTYPE_1STPERSON,

			/// <summary>
			/// A device that does not fall into any other category.
			/// </summary>
			Other = DI8DEVTYPE_DEVICE,

			/// <summary>
			/// Input device used to control another type of device from within the context of
			/// the application.
			/// </summary>
			ControlDevice = DI8DEVTYPE_DEVICECTRL,

			/// <summary>
			/// A device for steering.
			/// </summary>
			Driving = DI8DEVTYPE_DRIVING,

			/// <summary>
			/// Controller for a flight simulation.
			/// </summary>
			Flight = DI8DEVTYPE_FLIGHT,

			/// <summary>
			/// A console game pad.
			/// </summary>
			Gamepad = DI8DEVTYPE_GAMEPAD,

			/// <summary>
			/// A generic joystick.
			/// </summary>
			Joystick = DI8DEVTYPE_JOYSTICK,

			/// <summary>
			/// A keyboard or keyboard-like device.
			/// </summary>
			Keyboard = DI8DEVTYPE_KEYBOARD,

			/// <summary>
			/// A mouse or mouse-like device.
			/// </summary>
			Mouse = DI8DEVTYPE_MOUSE,

			/// <summary>
			/// A remote-control device.
			/// </summary>
			Remote = DI8DEVTYPE_REMOTE,

			/// <summary>
			/// A screen pointer device.
			/// </summary>
			ScreenPointer = DI8DEVTYPE_SCREENPOINTER,

			/// <summary>
			/// A specialized device with functionality unsuitable for main control of an application,
			/// such as pedals with a wheel.
			/// </summary>
			Supplemental = DI8DEVTYPE_SUPPLEMENTAL
		};

		[System::Flags]
		public enum class EffectFileFlags : System::Int32
		{
			None = 0,
			IncludeNonstandard = DIFEF_INCLUDENONSTANDARD,
			ModifyIfNeeded = DIFEF_MODIFYIFNEEDED
		};

		[System::Flags]
		public enum class EffectFlags : System::Int32
		{
			None = 0,
			Cartesian = DIEFF_CARTESIAN,
			ObjectIds = DIEFF_OBJECTIDS,
			ObjectOffsets = DIEFF_OBJECTOFFSETS,
			Polar = DIEFF_POLAR,
			Spherical = DIEFF_SPHERICAL
		};

		[System::Flags]
		public enum class EffectParameterFlags : System::Int32
		{
			None = 0,
			All = DIEP_ALLPARAMS,
			AllExceptStartDelay = DIEP_ALLPARAMS_DX5,
			Axes = DIEP_AXES,
			Direction = DIEP_DIRECTION,
			Duration = DIEP_DURATION,
			Envelope = DIEP_ENVELOPE,
			Gain = DIEP_GAIN,
			SamplePeriod = DIEP_SAMPLEPERIOD,
			StartDelay = DIEP_STARTDELAY,
			TriggerButton = DIEP_TRIGGERBUTTON,
			TriggerRepeatInterval = DIEP_TRIGGERREPEATINTERVAL,
			TypeSpecificParameters = DIEP_TYPESPECIFICPARAMS,

			Start = DIEP_START,
			NoRestart = DIEP_NORESTART,
			NoDownload = (int) DIEP_NODOWNLOAD
		};

#pragma warning(disable: 4369)
		[System::Flags]
		public enum class EffectPlayFlags : System::Int32
		{
			None = 0,
			Solo = DIES_SOLO,
			NoDownload = DIES_NODOWNLOAD
		};
#pragma warning(default: 4369)

		[System::Flags]
		public enum class EffectStatus : System::Int32
		{
			None = 0,
			Playing = DIEGES_PLAYING,
			Emulated = DIEGES_EMULATED
		};

		[System::Flags]
		public enum class EffectType : System::Int32
		{
			None = 0,
			All = DIEFT_ALL,
			Condition = DIEFT_CONDITION,
			ConstantForce = DIEFT_CONSTANTFORCE,
			CustomForce = DIEFT_CUSTOMFORCE,
			DeadBand = DIEFT_DEADBAND,
			Attack = DIEFT_FFATTACK,
			Fade = DIEFT_FFFADE,
			Hardware = DIEFT_HARDWARE,
			Periodic = DIEFT_PERIODIC,
			TwoCoefficients = DIEFT_POSNEGCOEFFICIENTS,
			TwoSaturations = DIEFT_POSNEGSATURATION,
			RampForce = DIEFT_RAMPFORCE,
			Saturation = DIEFT_SATURATION,
			StartDelay = DIEFT_STARTDELAY
		};

		public enum class ImageUsage : System::Int32
		{
			Configuration = DIDIFT_CONFIGURATION,
			Overlay = DIDIFT_OVERLAY
		};

		/// <summary>
		/// Defines possible keyboard key codes.
		/// </summary>
		public enum class Key : System::Int32
		{
			/// <summary>
			/// The number 0.
			/// </summary>
			D0,

			/// <summary>
			/// The number 1.
			/// </summary>
			D1,

			/// <summary>
			/// The number 2.
			/// </summary>
			D2,

			/// <summary>
			/// The number 3.
			/// </summary>
			D3,

			/// <summary>
			/// The number 4.
			/// </summary>
			D4,

			/// <summary>
			/// The number 5.
			/// </summary>
			D5,

			/// <summary>
			/// The number 6.
			/// </summary>
			D6,

			/// <summary>
			/// The number 7.
			/// </summary>
			D7,

			/// <summary>
			/// The number 8.
			/// </summary>
			D8,

			/// <summary>
			/// The number 9.
			/// </summary>
			D9,

			/// <summary>
			/// The letter A.
			/// </summary>
			A,

			/// <summary>
			/// The letter B.
			/// </summary>
			B,

			/// <summary>
			/// The letter C.
			/// </summary>
			C,

			/// <summary>
			/// The letter D.
			/// </summary>
			D,

			/// <summary>
			/// The letter E.
			/// </summary>
			E,

			/// <summary>
			/// The letter F.
			/// </summary>
			F,

			/// <summary>
			/// The letter G.
			/// </summary>
			G,

			/// <summary>
			/// The letter H.
			/// </summary>
			H,

			/// <summary>
			/// The letter I.
			/// </summary>
			I,

			/// <summary>
			/// The letter J.
			/// </summary>
			J,

			/// <summary>
			/// The letter K.
			/// </summary>
			K,

			/// <summary>
			/// The letter L.
			/// </summary>
			L,

			/// <summary>
			/// The letter M.
			/// </summary>
			M,

			/// <summary>
			/// The letter N.
			/// </summary>
			N,

			/// <summary>
			/// The letter O.
			/// </summary>
			O,

			/// <summary>
			/// The letter P.
			/// </summary>
			P,

			/// <summary>
			/// The letter Q.
			/// </summary>
			Q,

			/// <summary>
			/// The letter R.
			/// </summary>
			R,

			/// <summary>
			/// The letter S.
			/// </summary>
			S,

			/// <summary>
			/// The letter T.
			/// </summary>
			T,

			/// <summary>
			/// The letter U.
			/// </summary>
			U,

			/// <summary>
			/// The letter V.
			/// </summary>
			V,

			/// <summary>
			/// The letter W.
			/// </summary>
			W,

			/// <summary>
			/// The letter X.
			/// </summary>
			X,

			/// <summary>
			/// The letter Y.
			/// </summary>
			Y,

			/// <summary>
			/// The letter Z.
			/// </summary>
			Z,

			/// <summary>
			/// The AbntC1 key on Brazillian keyboards.
			/// </summary>
			AbntC1,

			/// <summary>
			/// The AbntC2 key on Brazillian keyboards.
			/// </summary>
			AbntC2,

			/// <summary>
			/// The apostrophe key.
			/// </summary>
			Apostrophe,

			/// <summary>
			/// The Applications key.
			/// </summary>
			Applications,

			/// <summary>
			/// The Japanese At key.
			/// </summary>
			AT,

			/// <summary>
			/// The Japanese Ax key.
			/// </summary>
			AX,

			/// <summary>
			/// The Backspace.
			/// </summary>
			Backspace,

			/// <summary>
			/// The back slash key.
			/// </summary>
			Backslash,

			/// <summary>
			/// The calculator key.
			/// </summary>
			Calculator,

			/// <summary>
			/// The Caps Lock key.
			/// </summary>
			CapsLock,

			/// <summary>
			/// The colon key.
			/// </summary>
			Colon,

			/// <summary>
			/// The comma key.
			/// </summary>
			Comma,

			/// <summary>
			/// The Japanese Convert key.
			/// </summary>
			Convert,

			/// <summary>
			/// The Delete key.
			/// </summary>
			Delete,

			/// <summary>
			/// The Down Arrow key.
			/// </summary>
			DownArrow,

			/// <summary>
			/// The End key.
			/// </summary>
			End,

			/// <summary>
			/// The equals key.
			/// </summary>
			Equals,

			/// <summary>
			/// The Escape key.
			/// </summary>
			Escape,

			/// <summary>
			/// The F1 key.
			/// </summary>
			F1,

			/// <summary>
			/// The F2 key.
			/// </summary>
			F2,

			/// <summary>
			/// The F3 key.
			/// </summary>
			F3,

			/// <summary>
			/// The F4 key.
			/// </summary>
			F4,

			/// <summary>
			/// The F5 key.
			/// </summary>
			F5,

			/// <summary>
			/// The F6 key.
			/// </summary>
			F6,

			/// <summary>
			/// The F7 key.
			/// </summary>
			F7,

			/// <summary>
			/// The F8 key.
			/// </summary>
			F8,

			/// <summary>
			/// The F9 key.
			/// </summary>
			F9,

			/// <summary>
			/// The F10 key.
			/// </summary>
			F10,

			/// <summary>
			/// The F11 key.
			/// </summary>
			F11,

			/// <summary>
			/// The F12 key.
			/// </summary>
			F12,

			/// <summary>
			/// The F13 key.
			/// </summary>
			F13,

			/// <summary>
			/// The F14 key.
			/// </summary>
			F14,

			/// <summary>
			/// The F15 key.
			/// </summary>
			F15,

			/// <summary>
			/// The grav accent (`) key.
			/// </summary>
			Grave,

			/// <summary>
			/// The Home key.
			/// </summary>
			Home,

			/// <summary>
			/// The Insert key.
			/// </summary>
			Insert,

			/// <summary>
			/// The Japanese Kana key.
			/// </summary>
			Kana,

			/// <summary>
			/// The Japanese Kanji key.
			/// </summary>
			Kanji,

			/// <summary>
			/// The left square bracket key.
			/// </summary>
			LeftBracket,

			/// <summary>
			/// The left Ctrl key.
			/// </summary>
			LeftControl,

			/// <summary>
			/// The Left Arrow key.
			/// </summary>
			LeftArrow,

			/// <summary>
			/// The left Alt key.
			/// </summary>
			LeftAlt,

			/// <summary>
			/// The left Shift key.
			/// </summary>
			LeftShift,

			/// <summary>
			/// The left Windows key.
			/// </summary>
			LeftWindowsKey,

			/// <summary>
			/// The Mail key.
			/// </summary>
			Mail,

			/// <summary>
			/// The Media Select key.
			/// </summary>
			MediaSelect,

			/// <summary>
			/// The Media Stop key.
			/// </summary>
			MediaStop,

			/// <summary>
			/// The minus key.
			/// </summary>
			Minus,

			/// <summary>
			/// The Mute key.
			/// </summary>
			Mute,

			/// <summary>
			/// The My Computer key.
			/// </summary>
			MyComputer,

			/// <summary>
			/// The Next Track key.
			/// </summary>
			NextTrack,

			/// <summary>
			/// The Japanese No Convert key.
			/// </summary>
			NoConvert,

			/// <summary>
			/// The NumberLock key.
			/// </summary>
			NumberLock,

			/// <summary>
			/// The number 0 on the NumberPad.
			/// </summary>
			NumberPad0,

			/// <summary>
			/// The number 1 on the NumberPad.
			/// </summary>
			NumberPad1,

			/// <summary>
			/// The number 2 on the NumberPad.
			/// </summary>
			NumberPad2,

			/// <summary>
			/// The number 3 on the NumberPad.
			/// </summary>
			NumberPad3,

			/// <summary>
			/// The number 4 on the NumberPad.
			/// </summary>
			NumberPad4,

			/// <summary>
			/// The number 5 on the NumberPad.
			/// </summary>
			NumberPad5,

			/// <summary>
			/// The number 6 on the NumberPad.
			/// </summary>
			NumberPad6,

			/// <summary>
			/// The number 7 on the NumberPad.
			/// </summary>
			NumberPad7,

			/// <summary>
			/// The number 8 on the NumberPad.
			/// </summary>
			NumberPad8,

			/// <summary>
			/// The number 9 on the NumberPad.
			/// </summary>
			NumberPad9,

			/// <summary>
			/// The comma key on the NumberPad.
			/// </summary>
			NumberPadComma,

			/// <summary>
			/// The Enter key on the NumberPad.
			/// </summary>
			NumberPadEnter,

			/// <summary>
			/// The equals key on the NumberPad.
			/// </summary>
			NumberPadEquals,

			/// <summary>
			/// The minus key on the NumberPad.
			/// </summary>
			NumberPadMinus,

			/// <summary>
			/// The period key on the NumberPad.
			/// </summary>
			NumberPadPeriod,

			/// <summary>
			/// The plus key on the NumberPad.
			/// </summary>
			NumberPadPlus,

			/// <summary>
			/// The slash key on the NumberPad.
			/// </summary>
			NumberPadSlash,

			/// <summary>
			/// The asterisk key on the NumberPad.
			/// </summary>
			NumberPadStar,

			/// <summary>
			/// The British and German OEM102 key.
			/// </summary>
			Oem102,

			/// <summary>
			/// The Page Down key.
			/// </summary>
			PageDown,

			/// <summary>
			/// The Page Up key.
			/// </summary>
			PageUp,

			/// <summary>
			/// The Pause key.
			/// </summary>
			Pause,

			/// <summary>
			/// The period key.
			/// </summary>
			Period,

			/// <summary>
			/// The Play/Pause key.
			/// </summary>
			PlayPause,

			/// <summary>
			/// The Power key.
			/// </summary>
			Power,

			/// <summary>
			/// The Previous Track key.
			/// </summary>
			PreviousTrack,

			/// <summary>
			/// The right square bracket key.
			/// </summary>
			RightBracket,

			/// <summary>
			/// The right Ctrl key.
			/// </summary>
			RightControl,

			/// <summary>
			/// The Return/Enter key.
			/// </summary>
			Return,

			/// <summary>
			/// The Right Arrow key.
			/// </summary>
			RightArrow,

			/// <summary>
			/// The right Alt key.
			/// </summary>
			RightAlt,

			/// <summary>
			/// The right Shift key.
			/// </summary>
			RightShift,

			/// <summary>
			/// The right Windows key.
			/// </summary>
			RightWindowsKey,

			/// <summary>
			/// The Scroll Lock key.
			/// </summary>
			ScrollLock,

			/// <summary>
			/// The semicolon key.
			/// </summary>
			Semicolon,

			/// <summary>
			/// The slash key.
			/// </summary>
			Slash,

			/// <summary>
			/// The Sleep key.
			/// </summary>
			Sleep,

			/// <summary>
			/// The Spacebar.
			/// </summary>
			Space,

			/// <summary>
			/// The Stop key.
			/// </summary>
			Stop,

			/// <summary>
			/// The Print Screen key.
			/// </summary>
			PrintScreen,

			/// <summary>
			/// The Tab key.
			/// </summary>
			Tab,

			/// <summary>
			/// The Japanese Underline key.
			/// </summary>
			Underline,

			/// <summary>
			/// An unlabeled key.
			/// </summary>
			Unlabeled,

			/// <summary>
			/// The Up Arrow key.
			/// </summary>
			UpArrow,

			/// <summary>
			/// The Volume Down key.
			/// </summary>
			VolumeDown,

			/// <summary>
			/// The Volume Up key.
			/// </summary>
			VolumeUp,

			/// <summary>
			/// The Wake key.
			/// </summary>
			Wake,

			/// <summary>
			/// The Web Backwards key.
			/// </summary>
			WebBack,

			/// <summary>
			/// The Web Favorites key.
			/// </summary>
			WebFavorites,

			/// <summary>
			/// The Web Forwards key.
			/// </summary>
			WebForward,

			/// <summary>
			/// The Web Home key.
			/// </summary>
			WebHome,

			/// <summary>
			/// The Web Refresh key.
			/// </summary>
			WebRefresh,

			/// <summary>
			/// The Web Search key.
			/// </summary>
			WebSearch,

			/// <summary>
			/// The Web Stop key.
			/// </summary>
			WebStop,

			/// <summary>
			/// The Japanese Yen key.
			/// </summary>
			Yen,

			Unknown
		};

		/// <summary>
		/// Defines possible mouse object codes.
		/// </summary>
		public enum class MouseObject : System::Int32
		{
			/// <summary>
			/// The first button.
			/// </summary>
			Button1,

			/// <summary>
			/// The second button.
			/// </summary>
			Button2,

			/// <summary>
			/// The third button.
			/// </summary>
			Button3,

			/// <summary>
			/// The fourth button.
			/// </summary>
			Button4,

			/// <summary>
			/// The fifth button.
			/// </summary>
			Button5,

			/// <summary>
			/// The sixth button.
			/// </summary>
			Button6,

			/// <summary>
			/// The seventh button.
			/// </summary>
			Button7,

			/// <summary>
			/// The eighth button.
			/// </summary>
			Button8,

			/// <summary>
			/// The X axis.
			/// </summary>
			XAxis,

			/// <summary>
			/// The Y axis.
			/// </summary>
			YAxis,

			/// <summary>
			/// The Z axis.
			/// </summary>
			ZAxis
		};

		/// <unmanaged>DIDOI</unmanaged>
		public enum class ObjectAspect : System::Int32
		{
			Acceleration = DIDOI_ASPECTACCEL,
			Force = DIDOI_ASPECTFORCE,
			Mask = DIDOI_ASPECTMASK,
			Position = DIDOI_ASPECTPOSITION,
			Velocity = DIDOI_ASPECTVELOCITY,
			ForceFeedbackActuator = DIDOI_FFACTUATOR,
			ForceFeedbackEffectTrigger = DIDOI_FFEFFECTTRIGGER,
			Polled = DIDOI_POLLED,
			GuidIsUsage = DIDOI_GUIDISUSAGE
		};

		/// <summary>
		/// Specifies other attributes of an object data format.
		/// </summary>
		[System::Flags]
		public enum class ObjectDataFormatFlags : System::Int32
		{
			/// <summary>
		    /// No extra flags specified.
		    /// </summary>
			None = 0,

			/// <summary>
		    /// The object must report acceleration information.
		    /// </summary>
			Acceleration,

			/// <summary>
		    /// The object must report force information.
		    /// </summary>
			Force,

			/// <summary>
		    /// The object must report position information.
		    /// </summary>
			Position,

			/// <summary>
		    /// The object must report velocity information.
		    /// </summary>
			Velocity
		};

		/// <summary>
		/// Specifies the device type of an object data format.
		/// </summary>
		/// <unmanaged>DIDFT</unmanaged>
		[System::Flags]
		public enum class ObjectDeviceType : System::Int32
		{
			/// <summary>
		    /// The object must be an absolute axis.
		    /// </summary>
			AbsoluteAxis = DIDFT_ABSAXIS,

			Alias = DIDFT_ALIAS,

			All = DIDFT_ALL,

			/// <summary>
		    /// The object must be an absolute or relative axis.
		    /// </summary>
			Axis = DIDFT_AXIS,

			/// <summary>
		    /// The object must be a relative axis.
		    /// </summary>
			RelativeAxis = DIDFT_RELAXIS,

			/// <summary>
		    /// The object must be a toggle or push button.
		    /// </summary>
			Button = DIDFT_BUTTON,

			Collection = DIDFT_COLLECTION,

			/// <summary>
		    /// The object must contain a force-feedback actuator.
		    /// </summary>
			ForceFeedbackActuator = DIDFT_FFACTUATOR,

			/// <summary>
		    /// The object must be a valid force-feedback effect trigger.
		    /// </summary>
			ForceFeedbackEffectTrigger = DIDFT_FFEFFECTTRIGGER,

			NoCollection = DIDFT_NOCOLLECTION,

			NoData = DIDFT_NODATA,

			Output = DIDFT_OUTPUT,

			/// <summary>
		    /// The object must be a Point-Of-View controller.
		    /// </summary>
			PointOfViewController = DIDFT_POV,

			/// <summary>
		    /// The object must be a push button.
		    /// </summary>
			PushButton = DIDFT_PSHBUTTON,

			/// <summary>
		    /// The object must be a toggle button.
		    /// </summary>
			ToggleButton = DIDFT_TGLBUTTON,

			/// <summary>
		    /// The object must be a type defined by the manufacturer.
		    /// </summary>
			VendorDefined = DIDFT_VENDORDEFINED
		};

		[System::Flags]
		public enum class TextAlignment : System::Int32
		{
			Left = DIDAL_LEFTALIGNED,
			Centered = DIDAL_CENTERED,
			Right = DIDAL_RIGHTALIGNED,
			Middle = DIDAL_MIDDLE,
			Top = DIDAL_TOPALIGNED,
			Bottom = DIDAL_BOTTOMALIGNED
		};
	}
}