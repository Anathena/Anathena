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
		public value class Texture3DDescription : System::IEquatable<Texture3DDescription>
		{
		private:
			int m_Width;
			int m_Height;
			int m_Depth;
			int m_MipLevels;
			DXGI::Format m_Format;
			ResourceUsage m_Usage;
			BindFlags m_BindFlags;
			CpuAccessFlags m_CPUAccessFlags;
			ResourceOptionFlags m_MiscFlags;

		internal:
			Texture3DDescription( const D3D10_TEXTURE3D_DESC& native );
			
			D3D10_TEXTURE3D_DESC CreateNativeVersion();
			
		public:
			property int Width
			{
				int get();
				void set( int value );
			}
			
			property int Height
			{
				int get();
				void set( int value );
			}
			
			property int Depth
			{
				int get();
				void set( int value );
			}
			
			property int MipLevels
			{
				int get();
				void set( int value );
			}
			
			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}
			
			property ResourceUsage Usage
			{
				ResourceUsage get();
				void set( ResourceUsage value );
			}
			
			property Direct3D10::BindFlags BindFlags
			{
				Direct3D10::BindFlags get();
				void set( Direct3D10::BindFlags value );
			}
			
			property Direct3D10::CpuAccessFlags CpuAccessFlags
			{
				Direct3D10::CpuAccessFlags get();
				void set( Direct3D10::CpuAccessFlags value );
			}
			
			property ResourceOptionFlags OptionFlags
			{
				ResourceOptionFlags get();
				void set( ResourceOptionFlags value );
			}

			static bool operator == ( Texture3DDescription left, Texture3DDescription right );
			static bool operator != ( Texture3DDescription left, Texture3DDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( Texture3DDescription other );
			static bool Equals( Texture3DDescription% value1, Texture3DDescription% value2 );
		};
	}
};