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
	namespace Direct3D9
	{
		/// <summary>A description of a constant in a constant table.</summary>
		/// <unmanaged>D3DXCONSTANT_DESC</unmanaged>
		public value class ConstantDescription : System::IEquatable<ConstantDescription>
		{
		internal:
			void Initialize( const D3DXCONSTANT_DESC& description );

		public:
			property System::String^ Name;
			property RegisterSet RegisterSet;
			property int RegisterIndex;
			property int RegisterCount;
			property ParameterClass Class;
			property ParameterType Type;
			property int Rows;
			property int Columns;
			property int Elements;
			property int StructMembers;
			property int Bytes;

			static bool operator == ( ConstantDescription left, ConstantDescription right );
			static bool operator != ( ConstantDescription left, ConstantDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( ConstantDescription other );
			static bool Equals( ConstantDescription% value1, ConstantDescription% value2 );
		};
	}
}