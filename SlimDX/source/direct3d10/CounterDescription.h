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
		public value class CounterDescription : System::IEquatable<CounterDescription>
		{
		private:
			Direct3D10::CounterKind m_Counter;

		internal:
			CounterDescription( const D3D10_COUNTER_DESC& native );
			
			D3D10_COUNTER_DESC CreateNativeVersion();
			
		public:
			property Direct3D10::CounterKind CounterKind
			{
				Direct3D10::CounterKind get();
				void set( Direct3D10::CounterKind value );
			}
			
			CounterDescription( Direct3D10::CounterKind kind );

			static bool operator == ( CounterDescription left, CounterDescription right );
			static bool operator != ( CounterDescription left, CounterDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( CounterDescription other );
			static bool Equals( CounterDescription% value1, CounterDescription% value2 );
		};
	}
};