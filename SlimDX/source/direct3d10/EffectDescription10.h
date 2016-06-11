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
		public value class EffectDescription : System::IEquatable<EffectDescription>
		{
		private:
			bool m_IsChildEffect;
			int m_ConstantBuffers;
			int m_SharedConstantBuffers;
			int m_GlobalVariables;
			int m_SharedGlobalVariables;
			int m_Techniques;

		internal:
			EffectDescription( const D3D10_EFFECT_DESC& native );
			
		public:
			property bool IsChildEffect
			{
				bool get();
			}
			
			property int ConstantBufferCount
			{
				int get();
			}
			
			property int SharedConstantBufferCount
			{
				int get();
			}
			
			property int GlobalVariableCount
			{
				int get();
			}
			
			property int SharedGlobalVariableCount
			{
				int get();
			}
			
			property int TechniqueCount
			{
				int get();
			}

			static bool operator == ( EffectDescription left, EffectDescription right );
			static bool operator != ( EffectDescription left, EffectDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( EffectDescription other );
			static bool Equals( EffectDescription% value1, EffectDescription% value2 );
		};
	}
};