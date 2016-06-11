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
	namespace Direct3D9
	{
		ref class Direct3D;

		/// <summary>Contains information identifying an adapter.</summary>
		/// <unmanaged>D3DADAPTER_IDENTIFIER9</unmanaged>
		public ref class AdapterDetails
		{
		private:
			int adapter;
			System::String^ description;
			int deviceId;
			System::Guid deviceIdentifier;
			System::String^ deviceName;
			System::String^ driverName;
			System::Version^ driverVersion;
			int revision;
			int subsystemId;
			int vendorId;
			int whqlLevel;

		internal:
			AdapterDetails( IDirect3D9 *direct3D, unsigned int adapter, bool checkWhql );

		public:
			/// <summary>
			/// Gets the adapter ordinal.
			/// </summary>
			property int Adapter
			{
				int get() { return adapter; }
			}

			/// <summary>
			/// Gets a friendly description string for the adapter.
			/// </summary>
			property System::String^ Description
			{
				System::String^ get() { return description; }
			}

			/// <summary>
			/// Gets the identifier of the adapter chip set.
			/// </summary>
			property int DeviceId
			{
				int get() { return deviceId; }
			}

			/// <summary>
			/// Gets a unique identifier for the adapter/chip set pair.
			/// </summary>
			property System::Guid DeviceIdentifier
			{
				System::Guid get() { return deviceIdentifier; }
			}

			/// <summary>
			/// Gets the name of the adapter.
			/// </summary>
			property System::String^ DeviceName
			{
				System::String^ get() { return deviceName; }
			}

			/// <summary>
			/// Gets the name of the driver running on the adapter.
			/// </summary>
			property System::String^ DriverName
			{
				System::String^ get() { return driverName; }
			}

			/// <summary>
			/// Gets the version of the driver running on the adapter.
			/// </summary>
			property System::Version^ DriverVersion
			{
				System::Version^ get() { return driverVersion; }
			}

			/// <summary>
			/// Gets the revision level of the chip set.
			/// </summary>
			property int Revision
			{
				int get() { return revision; }
			}

			/// <summary>
			/// Gets the subsystem identifier for the chip set.
			/// </summary>
			property int SubsystemId
			{
				int get() { return subsystemId; }
			}

			/// <summary>
			/// Gets the manufacturer identifier.
			/// </summary>
			property int VendorId
			{
				int get() { return vendorId; }
			}

			/// <summary>
			/// The Windows Hardware Quality Labs validation level for the driver/device pair.
			/// </summary>
			property int WhqlLevel
			{
				int get() { return whqlLevel; }
			}

			/// <summary>
			/// Gets a value indicating whether the adapter is WHQL certified.
			/// </summary>
			property bool Certified
			{
				bool get() { return WhqlLevel != 0; }
			}
			
			/// <summary>
			/// Gets a value indicating when the adapter was WHQL certified.
			/// A value of DateTime.MinValue indicates that the adapter is certified, but no specific date
			/// information is available. A value of DateTime.MaxValue indicates that the driver is not
			/// WHQL certified.
			/// </summary>
			property System::DateTime CertificationDate 
			{
				System::DateTime get();
			}
		};
	}
}