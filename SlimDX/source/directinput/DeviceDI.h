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

#include "Enums.h"
#include "DeviceInstance.h"
#include "DeviceObjectInstance.h"
#include "DeviceProperties.h"
#include "ObjectProperties.h"
#include "CapabilitiesDI.h"
#include "DirectInput.h"
#include "EffectInfo.h"
#include "DeviceImageHeader.h"
#include "EffectFile.h"
#include "ObjectData.h"
#include "RawBufferedData.h"

namespace SlimDX
{
	namespace DirectInput
	{
		ref class Effect;

		public ref class Device abstract : public ComObject
		{
			COMOBJECT_BASE(IDirectInputDevice8);

		private:
			Capabilities^ caps;
			DeviceInstance^ information;
			DeviceProperties^ properties;

		internal:
			Device() { }

		protected:
			Device( DirectInput^ directInput, System::Guid subsystem );

		public:
			/// <summary>
			/// Obtains access to the input device.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Acquire();

			/// <summary>
			/// Releases access to the device.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Unacquire();
			
			/// <summary>
			/// Sets the device's cooperative level.
			/// </summary>
			/// <param name="handle">A handle of a window to associate with the device.</param>
			/// <param name="flags">Flags that describe the cooperative level of the device.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetCooperativeLevel( System::IntPtr handle, CooperativeLevel flags );

			/// <summary>
			/// Sets the device's cooperative level.
			/// </summary>
			/// <param name="control">A control to associate with the device.</param>
			/// <param name="flags">Flags that describe the cooperative level of the device.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetCooperativeLevel( System::Windows::Forms::Control^ control, CooperativeLevel flags );

			/// <summary>
			/// Runs the DirectInput control panel associated with this device. If the
			/// device does not have a control panel associated with it, the default
			/// device control panel is launched.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result RunControlPanel();

			/// <summary>
			/// Runs the DirectInput control panel associated with this device. If the
			/// device does not have a control panel associated with it, the default
			/// device control panel is launched.
			/// </summary>
			/// <param name="parent">The parent control.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result RunControlPanel( System::Windows::Forms::Control^ parent );

			/// <summary>
			/// Retrieves data from polled objects on a DirectInput device.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Poll();

			int GetDeviceData(array<RawBufferedData>^ data, bool peek);

			DeviceImageHeader GetDeviceImages();
			EffectInfo GetEffectInfo( System::Guid guid );
			Result SendData( array<ObjectData>^ data, bool overlay );

			DeviceObjectInstance GetObjectInfoByOffset(int offset);
			DeviceObjectInstance GetObjectInfoByUsage( int usageCode );
			DeviceObjectInstance GetObjectInfoById( int objectId );

			Result SetNotification( System::Threading::WaitHandle^ eventHandle );

			/// <summary>
			/// Retrieves a collection of objects on the device.
			/// </summary>
			/// <param name="objectType">A filter for the returned device objects collection.</param>
			/// <returns>A collection of device objects matching the specified filter.</returns>
			System::Collections::Generic::IList<DeviceObjectInstance>^ GetObjects( ObjectDeviceType objectType );

			/// <summary>
			/// Retrieves a collection of objects on the device.
			/// </summary>
			/// <returns>A collection of all device objects on the device.</returns>
			System::Collections::Generic::IList<DeviceObjectInstance>^ GetObjects();

			System::Collections::Generic::IList<EffectInfo>^ GetEffects();
			System::Collections::Generic::IList<EffectInfo>^ GetEffects( EffectType type );

			System::Collections::Generic::IList<EffectFile>^ GetEffectsInFile( System::String^ fileName );
			System::Collections::Generic::IList<EffectFile>^ GetEffectsInFile( System::String^ fileName, EffectFileFlags flags );

			Result WriteEffectsToFile( System::String^ fileName, array<EffectFile>^ effects );
			Result WriteEffectsToFile( System::String^ fileName, array<EffectFile>^ effects, bool includeNonstandardEffects );

			/// <summary>
			/// Gets properties about a single object on an input device.
			/// </summary>
			/// <param name="usageCode">The usageCode of the object whose properties are to be retrieved.</param>
			/// <returns>The properties of the desired object.</returns>
			ObjectProperties^ GetObjectPropertiesByUsage( int usageCode );

			/// <summary>
			/// Gets properties about a single object on an input device.
			/// </summary>
			/// <param name="objectId">The identifier of the object whose properties are to be retrieved.</param>
			/// <returns>The properties of the desired object.</returns>
			ObjectProperties^ GetObjectPropertiesById( int objectId );

			/// <summary>
			/// Sends a hardware-specific command to the force-feedback driver.
			/// </summary>
			/// <param name="command">The command to be sent.</param>
			/// <param name="data">The data to be sent to the device.</param>
			/// <param name="outputSize">The expected size of the output buffer.</param>
			/// <returns>The output data of the command.</returns>
			array<System::Byte>^ Escape( int command, array<System::Byte>^ data, int outputSize );

			property System::Collections::Generic::IList<Effect^>^ CreatedEffects
			{
				System::Collections::Generic::IList<Effect^>^ get();
			}

			/// <summary>
			/// Gets a set of properties that control the behavior of the device.
			/// </summary>
			property DeviceProperties^ Properties { DeviceProperties^ get(); }

			/// <summary>
			/// Gets the capabilities of the device.
			/// </summary>
			property Capabilities^ Capabilities { SlimDX::DirectInput::Capabilities^ get(); }

			/// <summary>
			/// Gets information about the device's identity.
			/// </summary>
			property DeviceInstance^ Information { DeviceInstance^ get(); }
		};
	}
}