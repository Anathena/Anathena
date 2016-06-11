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

namespace SlimDX
{
	namespace Direct3D10
	{	
		public value class CounterMetadata : System::IEquatable<CounterMetadata>
		{
		private:
			CounterType m_Type;
			int m_ActiveCounters;
			System::String^ m_Name;
			System::String^ m_Units;
			System::String^ m_Description;

		internal:
			CounterMetadata( CounterType type, int count, System::String^ name, System::String^ units, System::String^ description );
			
		public:
			property CounterType Type
			{
				CounterType get();
			}
			
			property int HardwareCounterCount
			{
				int get();
			}
			
			property System::String^ Name
			{
				System::String^ get();
			}
			
			property System::String^ Units
			{
				System::String^ get();
			}
			
			property System::String^ Description
			{
				System::String^ get();
			}

			static bool operator == ( CounterMetadata left, CounterMetadata right );
			static bool operator != ( CounterMetadata left, CounterMetadata right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( CounterMetadata other );
			static bool Equals( CounterMetadata% value1, CounterMetadata% value2 );
		};
	}
};