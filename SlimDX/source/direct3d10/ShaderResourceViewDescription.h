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
		public value class ShaderResourceViewDescription : System::IEquatable<ShaderResourceViewDescription>
		{
		private:
			DXGI::Format m_Format;
			ShaderResourceViewDimension m_ViewDimension;
			int m_ElementOffset;
			int m_ElementWidth;
			int m_MostDetailedMip;
			int m_MipLevels;
			int m_FirstArraySlice;
			int m_ArraySize;
				
		internal:
			ShaderResourceViewDescription( const D3D10_SHADER_RESOURCE_VIEW_DESC& native );
			
			D3D10_SHADER_RESOURCE_VIEW_DESC CreateNativeVersion();
		
		public:
			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}
			
			property ShaderResourceViewDimension Dimension
			{
				ShaderResourceViewDimension get();
				void set( ShaderResourceViewDimension value );
			}
			
			property int ElementOffset
			{
				int get();
				void set( int value );
			}
			
			property int ElementWidth
			{
				int get();
				void set( int value );
			}
			
			property int MostDetailedMip
			{
				int get();
				void set( int value );
			}
			
			property int MipLevels
			{
				int get();
				void set( int value );
			}
			
			property int FirstArraySlice
			{
				int get();
				void set( int value );
			}
			
			property int ArraySize
			{
				int get();
				void set( int value );
			}

			static bool operator == ( ShaderResourceViewDescription left, ShaderResourceViewDescription right );
			static bool operator != ( ShaderResourceViewDescription left, ShaderResourceViewDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( ShaderResourceViewDescription other );
			static bool Equals( ShaderResourceViewDescription% value1, ShaderResourceViewDescription% value2 );
		};
	}
};