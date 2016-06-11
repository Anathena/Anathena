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
		/// <summary>
		/// Indicates that a type is a data format specification for DirectInput.
		/// </summary>
		[System::AttributeUsage(System::AttributeTargets::Field)]
		public ref class DataObjectAttribute sealed : public System::Attribute
		{
		private:
			System::Guid sourceGuid;
			int instanceNumber;
			ObjectDeviceType type;
			ObjectDataFormatFlags flags;
			int offset;

		internal:
			property int Offset
			{
				int get() { return offset; }
				void set( int value ) { offset = value; }
			}

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::DirectInput::DataObjectAttribute"/> class.
			/// </summary>
			DataObjectAttribute()
			{
				sourceGuid = System::Guid::Empty;
				instanceNumber = -1;
				type = ( ObjectDeviceType )0;
				flags = ObjectDataFormatFlags::None;
			}

			/// <summary>
			/// Gets or sets the unique identifier for the axis, button, or other input
			/// source. When requesting a data format, making this member equal to
			/// <see cref="System::Guid::Empty"/> indicates that any type of object
			/// is permissible.
		    /// </summary>
			property System::Guid SourceGuid
			{
				System::Guid get() { return sourceGuid; }
				void set( System::Guid value ) { sourceGuid = value; }
			}

			/// <summary>
			/// Gets or sets the object instance number. Setting this value to -1
			/// indicates that any object instance is permissible.
		    /// </summary>
			property int InstanceNumber
			{
				int get() { return instanceNumber; }
				void set( int value ) { instanceNumber = value; }
			}

			/// <summary>
			/// Gets or sets the device type that describes the object.
		    /// </summary>
			property ObjectDeviceType Type
			{
				ObjectDeviceType get() { return type; }
				void set( ObjectDeviceType value ) { type = value; }
			}

			/// <summary>
			/// Gets or sets extra flags for the object data format.
		    /// </summary>
			property ObjectDataFormatFlags Flags
			{
				ObjectDataFormatFlags get() { return flags; }
				void set( ObjectDataFormatFlags value ) { flags = value; }
			}
		};
	}
}