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
		[System::AttributeUsage(System::AttributeTargets::Class | System::AttributeTargets::Struct)]
		public ref class DataFormatAttribute sealed : public System::Attribute
		{
		private:
			DataFormatFlag flags;

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::DirectInput::DataFormatAttribute"/> class.
			/// </summary>
			DataFormatAttribute( DataFormatFlag flags ) : flags( flags ) { }

			/// <summary>
			/// Gets or sets the flags describing other attributes of the data format.
			/// </summary>
			property DataFormatFlag Flags
			{
				DataFormatFlag get() { return flags; }
			}
		};
	}
}