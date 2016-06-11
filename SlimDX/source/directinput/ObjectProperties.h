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
#include "InputRange.h"

namespace SlimDX
{
	namespace DirectInput
	{
		/// <summary>
		/// Contains properties that define object behavior.
		/// </summary>
		public ref class ObjectProperties
		{
		private:
			IDirectInputDevice8W* pointer;
			int obj;
			int how;

		internal:
			[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
			ObjectProperties( IDirectInputDevice8W* pointer, System::String^ name, System::Type^ dataFormat );
			ObjectProperties( IDirectInputDevice8W* pointer, int value, bool isUsageCode );

		public:
			/// <summary>
			/// Sets the object's data range.
			/// </summary>
			void SetRange( int lowerRange, int upperRange );

			/// <summary>
			/// Gets or sets an application-defined value associated with the object.
			/// </summary>
			property System::Object^ ApplicationData
			{
				[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
				System::Object^ get();

				[System::Security::Permissions::SecurityPermission( System::Security::Permissions::SecurityAction::LinkDemand, Flags=System::Security::Permissions::SecurityPermissionFlag::UnmanagedCode )]
				void set( System::Object^ value );
			}

			/// <summary>
			/// Gets or sets the dead zone for the object.
			/// </summary>
			property int DeadZone
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets the granularity of the object.
			/// </summary>
			property int Granularity
			{
				int get();
			}

			/// <summary>
			/// Gets the upper range of values that the object can possibly report.
			/// </summary>
			property int UpperRange
			{
				int get();
			}

			/// <summary>
			/// Gets the lower range of values that the object can possibly report.
			/// </summary>
			property int LowerRange
			{
				int get();
			}

			/// <summary>
			/// Gets or sets the saturation zone of the object.
			/// </summary>
			property int Saturation
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Gets the range of raw data returned from the object.
			/// </summary>
			property InputRange LogicalRange
			{
				InputRange get();
			}

			/// <summary>
			/// Gets the range of data returned from axis as suggested by the manufacturer.
			/// </summary>
			property InputRange PhysicalRange
			{
				InputRange get();
			}
		};
	}
}