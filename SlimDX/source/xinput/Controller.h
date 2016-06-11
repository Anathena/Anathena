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

#include "../Result.h"

#include "CapabilitiesXI.h"
#include "BatteryInformation.h"
#include "Enums.h"
#include "Keystroke.h"
#include "State.h"
#include "Vibration.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace XInput
	{
		/// <summary>
		/// Represents an XInput controller.
		/// </summary>
		public ref class Controller
		{
		private:
			System::UInt32 m_UserIndex;
			
			static int ConvertError( int errorCode );
			
		public:
			/// <summary>
			/// Gets a value indicating whether or not the controller is connected.
			/// </summary>
			property bool IsConnected
			{
				bool get();
			}
			
			/// <summary>
			/// Gets the identifier for the sound renderer in the controller.
			/// </summary>
			property System::Guid SoundRenderGuid
			{
				System::Guid get();
			}

			/// <summary>
			/// Gets the identifier for the sound capture device in the controller.
			/// </summary>
			property System::Guid SoundCaptureGuid
			{
				System::Guid get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Controller"/> class.
			/// </summary>
			/// <param name="userIndex">The index of the player whose controller is being represented.</param>
			Controller( UserIndex userIndex );
			
			/// <summary>
			/// Retrieves battery information for the controller.
			/// </summary>
			/// <param name="battery">The type of battery whose information is being retrieved.</param>
			/// <returns>A <see cref="BatteryInformation"/> structure describing the battery in the controller.</returns>
			BatteryInformation GetBatteryInformation( BatteryDeviceType battery );

			/// <summary>
			/// Retrieves the capabilities of the controller.
			/// </summary>
			/// <param name="device">The type of the device whose capabilities are being retrieved.</param>
			/// <returns>A <see cref="XInput::Capabilities"/> structure describing the capabilities of the controller.</returns>
			Capabilities GetCapabilities( DeviceQueryType device );

			/// <summary>
			/// Gets the state of the controller.
			/// </summary>
			/// <returns>A <see cref="XInput::State"/> structure describing the state of the controller.</returns>
			State GetState();
			
			/// <summary>
			/// Gets keystroke information from the device.
			/// </summary>
			/// <param name="flag">The type of the device to query.</param>
			/// <param name="keystroke">Keystroke information from the device.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetKeystroke( DeviceQueryType flag, [Out] Keystroke% keystroke );
				
			/// <summary>
			/// Sets vibration information for the controller.
			/// </summary>
			/// <param name="vibration">Vibration settings for the controller.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetVibration( Vibration vibration );

			/// <summary>
			/// Enables or disables data reporting in XInput.
			/// </summary>
			/// <param name="value"><c>true</c> to enable XInput, <c>false</c> to disable it.</param>
			static void SetReporting( bool value );
		};
	}
}