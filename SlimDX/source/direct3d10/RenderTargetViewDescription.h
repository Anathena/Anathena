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
		public value class RenderTargetViewDescription : System::IEquatable<RenderTargetViewDescription>
		{
		private:
			DXGI::Format m_Format;
			RenderTargetViewDimension m_ViewDimension;
			int m_ElementOffset;
			int m_ElementWidth;
			int m_MipSlice;
			int m_FirstArraySlice;
			int m_ArraySize;
			int m_FirstDepthSlice;
			int m_DepthSliceCount;
				
		internal:
			RenderTargetViewDescription( const D3D10_RENDER_TARGET_VIEW_DESC& native );
			
			D3D10_RENDER_TARGET_VIEW_DESC CreateNativeVersion();
		
		public:
			property DXGI::Format Format
			{
				DXGI::Format get();
				void set( DXGI::Format value );
			}
			
			property RenderTargetViewDimension Dimension
			{
				RenderTargetViewDimension get();
				void set( RenderTargetViewDimension value );
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
			
			property int MipSlice
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
			
			property int FirstDepthSlice
			{
				int get();
				void set( int value );
			}
			
			property int DepthSliceCount
			{
				int get();
				void set( int value );
			}

			static bool operator == ( RenderTargetViewDescription left, RenderTargetViewDescription right );
			static bool operator != ( RenderTargetViewDescription left, RenderTargetViewDescription right );

			virtual int GetHashCode() override;
			virtual bool Equals( System::Object^ obj ) override;
			virtual bool Equals( RenderTargetViewDescription other );
			static bool Equals( RenderTargetViewDescription% value1, RenderTargetViewDescription% value2 );
		};
	}
};