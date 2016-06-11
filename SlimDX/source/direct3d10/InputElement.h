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

#include "../dxgi/Enums.h"

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{
		public value class InputElement : System::IEquatable<InputElement>
		{
		private:
			System::String^ m_SemanticName;
			int m_SemanticIndex;
			DXGI::Format m_Format;
			int m_InputSlot;
			int m_AlignedByteOffset;
			InputClassification m_InputSlotClass;
			int m_InstanceDataStepRate;
	
		internal:
			D3D10_INPUT_ELEMENT_DESC CreateNativeVersion();
			
		public:
			property System::String^ SemanticName
			{
				System::String^ get();
				void set( System::String^ value );
			}

			property int SemanticIndex
			{
				int get();
				void set( int value );
			}

			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}

			property int Slot
			{
				int get();
				void set( int value );
			}

			property int AlignedByteOffset
			{
				int get();
				void set( int value );
			}

			property InputClassification Classification
			{
				InputClassification get();
				void set( InputClassification value );
			}

			property int InstanceDataStepRate
			{
				int get();
				void set( int value );
			}

			/// <summary>
			/// Returns a value that can be used for the offset parameter of an InputElement to indicate that the element
			/// should be aligned directly after the previous element, including any packing if neccessary.
			/// </summary>
			/// <returns>A value used to align input elements.</returns>
			/// <unmanaged>D3D10_APPEND_ALIGNED_ELEMENT</unmanaged>
			property static int AppendAligned {
				int get();
			}

			InputElement( System::String^ name, int index, DXGI::Format format, int offset, int slot );
			InputElement( System::String^ name, int index, DXGI::Format format, int offset, int slot, InputClassification slotClass, int stepRate );
		
			static bool operator == ( InputElement left, InputElement right );
			static bool operator != ( InputElement left, InputElement right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( InputElement other );
			static bool Equals( InputElement% value1, InputElement% value2 );
		};
	}
};